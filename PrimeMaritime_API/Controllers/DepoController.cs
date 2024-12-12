using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.IServices;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Response;
using PrimeMaritime_API.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class DepoController : ControllerBase
    {
        private IDepoService _depoService;
        private readonly IWebHostEnvironment _environment;
        public DepoController(IDepoService depoService, IWebHostEnvironment environment)
        {
            _depoService = depoService;
            _environment = environment;
        }

        [HttpPost("InsertContainer")]
        public ActionResult<Response<CommonResponse>> InsertContainer(DEPO_CONTAINER request)
        {
            return Ok(_depoService.InsertContainer(request));
        }

        [HttpPost("InsertMRRequest")]
        public ActionResult<Response<CommonResponse>> InsertMRRequest(List<MR_LIST> request)
        {
            return Ok(_depoService.InsertMRRequest(request));
        }

        [HttpPost("InsertNewMRRequest")]
        public ActionResult<Response<string>> InsertNewMRRequest(List<MR_LIST> request)
        {
            return Ok(_depoService.InsertNewMRRequest(request));
        }

        [HttpGet("GetMNRList")]
        public ActionResult<Response<List<MNR_LIST>>> GetMNRList(string OPERATION, string DEPO_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_depoService.GetMNRList(OPERATION, DEPO_CODE)));
        }

        [HttpGet("GetMNRTariff")]
        public ActionResult<Response<MNR_TARIFF>> GetMNRTariff(string COMPONENT, string REPAIR, string LENGTH, string WIDTH, string HEIGHT, string QUANTITY,string DEPO_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_depoService.GetMNRTariff(COMPONENT,REPAIR,LENGTH,WIDTH,HEIGHT,QUANTITY,DEPO_CODE)));
        }

        [HttpGet("GetMRDetails")]
        public ActionResult<Response<List<MR_LIST>>> GetMRDetails(string OPERATION, string MR_NO)
        {
            return Ok(JsonConvert.SerializeObject(_depoService.GetMNRDetails(OPERATION, MR_NO)));
        }

        
        [HttpGet("getMRDetailsByID")]
        public ActionResult<Response<List<MR_LIST>>> getMRDetailsByID(string OPERATION, string MR_NO, int ID)
        {
            return Ok(JsonConvert.SerializeObject(_depoService.getMRDetailsByID(OPERATION, MR_NO, ID)));
        }

        [HttpPost("ApproveRate")]
        public ActionResult<Response<CommonResponse>> ApproveRate(List<MR_LIST> request)
        {
            return Ok(_depoService.ApproveRate(request));
        }

        [HttpPost("DeleteMRequest")]
        public ActionResult<Response<string>> DeleteMRequest(string MR_NO, string LOCATION)
        {
            return Ok(_depoService.DeleteMRRequest(MR_NO, LOCATION));
        }

        [HttpPost("UploadMNRFiles")]
        public IActionResult UploadMNRFiles(string MR_NO)
        {
            var formFile = Request.Form.Files;

            string path = Path.Combine(_environment.ContentRootPath, "Uploads", "MNRFiles");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in formFile)
            {
                string fileName = Path.GetFileName(MR_NO + "_" + postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(_environment.ContentRootPath, path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                }
            }

            return Ok();
        }


        [HttpGet("GetImage")]
        public ActionResult<Response<List<ALL_FILE>>> GetImage(string MR_NO)
        {
            string[] array1 = Directory.GetFiles("Uploads/MNRFiles/");
           
            List<ALL_FILE> imgFiles = new List<ALL_FILE>();
            Response<List<ALL_FILE>> response = new Response<List<ALL_FILE>>();

            // Get list of files.
            List<string> filesList = array1.ToList();

            foreach (var file in filesList)
            {
                if (file.Contains(MR_NO))
                {
                    ALL_FILE img = new ALL_FILE();
                    long length = new System.IO.FileInfo(file).Length / 1024;
                    img.FILE_NAME = file.Split('/')[2];
                    img.FILE_SIZE = length.ToString() +"KB";
                    img.FILE_PATH = file;

                    imgFiles.Add(img);

                }
            }

            if (imgFiles.Count > 0)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = imgFiles;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }
            return response;
        }


        [HttpPost("InsertMNRFiles")]
        public async Task<ActionResult<Response<string>>> InsertMNRFiles()
        {
            try
            {
                // Check if there are files in the request
                if (Request.Form.Files.Count == 0)
                {
                    return BadRequest(new
                    {
                        status = "error",
                        code = 400,
                        message = "No files uploaded."
                    });
                }

                // Retrieve the payload as a list of MR_LIST items
                string payload = Request.Form["PAYLOAD2"];
                var newMNRList = JsonConvert.DeserializeObject<List<MR_LIST>>(payload);

                // Define base paths
                string uploadPath = Path.Combine(_environment.ContentRootPath, "Uploads");
                string attachmentPath = Path.Combine(uploadPath, "NEWMNRFiles");

                // Create directories if they do not exist
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                if (!Directory.Exists(attachmentPath))
                {
                    Directory.CreateDirectory(attachmentPath);
                }

                List<string> uploadedFiles = new List<string>();

                // Save each file with an associated MR_NO
                foreach (IFormFile postedFile in Request.Form.Files)
                {
                    // Generate file name with MR_NO and original file name
                    string fileName = $"{newMNRList[0].MR_NO}_{Path.GetFileName(postedFile.FileName)}";
                    string fullFilePath = Path.Combine(attachmentPath, fileName);

                    using (FileStream stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                        string relativePath = Path.Combine("Uploads", "NEWMNRFiles", fileName).Replace('/', '\\');
                        uploadedFiles.Add(relativePath);
                    }
                }

                // Pass the list of MR_LIST items and the list of file paths to the service
                _depoService.InsertMNRFiles(newMNRList, uploadedFiles);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("updateMRRequest")]
        public async Task<ActionResult<Response<string>>> updateMRRequest()
        {
            try
            {
                // Retrieve the payload as a list of MR_LIST items
                string payload = Request.Form["PAYLOAD2"];
                var updateMNRList = JsonConvert.DeserializeObject<List<MR_LIST>>(payload);

                // Define base paths
                string uploadPath = Path.Combine(_environment.ContentRootPath, "Uploads");
                string attachmentPath = Path.Combine(uploadPath, "NEWMNRFiles");

                // Create directories if they do not exist
                if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);
                if (!Directory.Exists(attachmentPath)) Directory.CreateDirectory(attachmentPath);

                string newFilePath = null;

                // Check if files are uploaded
                if (Request.Form.Files.Count > 0)
                {
                    foreach (IFormFile postedFile in Request.Form.Files)
                    {
                        // Generate a unique file name with MR_NO and original file name
                        string fileName = $"{updateMNRList[0].MR_NO}_{Path.GetFileName(postedFile.FileName)}";
                        string fullFilePath = Path.Combine(attachmentPath, fileName);

                        using (FileStream stream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            postedFile.CopyTo(stream);

                            // Convert the file path to a relative URL
                            newFilePath = Path.Combine("Uploads", "NEWMNRFiles", fileName).Replace("\\", "/");
                        }
                    }
                }

                // If no new file uploaded, keep the old file path
                if (string.IsNullOrEmpty(newFilePath))
                {
                    newFilePath = GetExistingImagePath(updateMNRList[0].MR_NO);
                }

                // Convert the file path to a List<string>
                List<string> filePaths = new List<string>();
                if (!string.IsNullOrEmpty(newFilePath))
                {
                    filePaths.Add(newFilePath);
                }

                // Pass the updated MR_LIST and new file path to the service
                _depoService.updateMRRequest(updateMNRList, filePaths);

                return Ok(new { message = "Request updated successfully", responseCode = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        // Helper method to get the existing image path based on MR_NO
        private string GetExistingImagePath(string mrNo)
        {
            string uploadPath = Path.Combine(_environment.ContentRootPath, "Uploads", "NEWMNRFiles");
            string fileNamePattern = $"{mrNo}_*";

            // Ensure the directory exists before attempting to get files
            if (!Directory.Exists(uploadPath)) return null;

            // Search for files matching the pattern
            string[] files = Directory.GetFiles(uploadPath, fileNamePattern);

            // Return the first matching file path (if any)
            if (files.Length > 0)
            {
                return Path.Combine("Uploads", "NEWMNRFiles", Path.GetFileName(files[0])).Replace("\\", "/");
            }

            return null; // No files found
        }





        [HttpPost("InsertPrinMNRFiles")]
        public async Task<ActionResult<Response<string>>> InsertPrinMNRFiles()
        {
            try
            {
                // Check if there are files in the request
                if (Request.Form.Files.Count == 0)
                {
                    return BadRequest(new { message = "No files uploaded." });
                }

                // Retrieve the payload as a list of MR_LIST items
                string payload = Request.Form["PAYLOAD2"];
                var newMNRList = JsonConvert.DeserializeObject<List<MR_LIST>>(payload);

                // Define base paths
                string uploadPath = Path.Combine(_environment.ContentRootPath, "Uploads");
                string attachmentPath = Path.Combine(uploadPath, "NEWMNRFiles");

                // Create directories if they do not exist
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                if (!Directory.Exists(attachmentPath))
                {
                    Directory.CreateDirectory(attachmentPath);
                }

                List<string> uploadedFiles = new List<string>();

                // Save each file with an associated MR_NO
                foreach (IFormFile postedFile in Request.Form.Files)
                {
                    // Generate file name with MR_NO and original file name
                    string fileName = $"{newMNRList[0].MR_NO}_{Path.GetFileName(postedFile.FileName)}";
                    string fullFilePath = Path.Combine(attachmentPath, fileName);

                    using (FileStream stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                        string relativePath = Path.Combine("Uploads", "NEWMNRFiles", fileName).Replace('/', '\\');
                        uploadedFiles.Add(relativePath);
                    }
                }

                // Pass the list of MR_LIST items and the list of file paths to the service
                _depoService.InsertPrinMNRFiles(newMNRList, uploadedFiles);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

    }
}
