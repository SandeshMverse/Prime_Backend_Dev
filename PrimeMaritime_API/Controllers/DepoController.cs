using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
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
        public ActionResult<Response<List<MNR_LIST>>> GetMNRList(string connstring, string OPERATION, string DEPO_CODE, string MR_NO, string STATUS, string FROMDATE, string TODATE)
        {
            return Ok(JsonConvert.SerializeObject(_depoService.GetMNRList(OPERATION, DEPO_CODE, MR_NO, STATUS, FROMDATE, TODATE)));
        }

        [HttpGet("GetMNRTariff")]
        public ActionResult<Response<MNR_TARIFF>> GetMNRTariff(string COMPONENT, string DAMAGE_LOCATION, string REPAIR, string LENGTH, string WIDTH, string HEIGHT, string QUANTITY,string DEPO_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_depoService.GetMNRTariff(COMPONENT, DAMAGE_LOCATION, REPAIR,LENGTH,WIDTH,HEIGHT,QUANTITY,DEPO_CODE)));
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
        public async Task<ActionResult<Response<string>>> InsertMNRFiles([FromBody] List<MR_LIST> newMNRList)
        {
            try
            {
                if (newMNRList == null || newMNRList.Count == 0)
                {
                    return BadRequest(new { status = "error", message = "Invalid MR List data." });
                }

                string uploadPath = Path.Combine(_environment.ContentRootPath, "Uploads", "NEWMNRFiles");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Process each MR_LIST record
                foreach (var mrItem in newMNRList)
                {
                    Dictionary<string, List<string>> mnrAttachments = new Dictionary<string, List<string>>();

                    string mrNo = mrItem.MR_NO ?? "UNKNOWN";

                    if (mrItem.IMAGES != null && mrItem.IMAGES.Count > 0)
                    {
                        foreach (var imageData in mrItem.IMAGES)
                        {
                            if (!string.IsNullOrEmpty(imageData.Base64))
                            {
                                // Get the file extension from the base64 string
                                string extension = GetFileExtensionFromBase64(imageData.Base64);

                                // Generate file name using original file name (or GUID) and extension
                                //string fileName = $"{mrNo}_{imageData.FileName ?? Guid.NewGuid().ToString()}.{extension}";
                                string fileName = $"{mrNo}_{Path.GetFileNameWithoutExtension(imageData.FileName) ?? Guid.NewGuid().ToString()}{extension}";

                                string fullFilePath = Path.Combine(uploadPath, fileName);

                                // Convert base64 to byte array
                                byte[] imageBytes = Convert.FromBase64String(imageData.Base64.Split(',')[1]);

                                // Save image file
                                await System.IO.File.WriteAllBytesAsync(fullFilePath, imageBytes);

                                string storedPath = Path.Combine("Uploads", "NEWMNRFiles", fileName).Replace('/', '\\');

                                if (!mnrAttachments.ContainsKey(mrNo))
                                {
                                    mnrAttachments[mrNo] = new List<string>();
                                }
                                mnrAttachments[mrNo].Add(storedPath);
                            }
                        }
                    }

                    // Insert the current MR record into the database (after saving images)
                    _depoService.InsertMNRFiles(new List<MR_LIST> { mrItem }, mnrAttachments);
                }

                return Ok(new { status = "success", message = "Records inserted successfully.", responseCode = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        // Helper method to extract file extension from Base64 string
        private string GetFileExtensionFromBase64(string base64Image)
        {
            if (base64Image.StartsWith("data:image/jpeg")) return ".jpg";
            if (base64Image.StartsWith("data:image/png")) return ".png";
            if (base64Image.StartsWith("data:image/gif")) return ".gif";
            return ".png"; // Default fallback
        }

        [HttpPost("DeleteMRImage")]
        public ActionResult<Response<string>> DeleteMRImage(int ID, int MR_ID)
        {
            return Ok(_depoService.DeleteMRImage(ID, MR_ID));
        }


        [HttpPost("updateMRRequest")]
        public async Task<ActionResult<Response<string>>> updateMRRequest([FromBody] List<MR_LIST> updateMNRList)
        {
            try
            {
                if (updateMNRList == null || updateMNRList.Count == 0)
                {
                    return BadRequest(new { status = "error", message = "Invalid MR List data." });
                }

                string uploadPath = Path.Combine(_environment.ContentRootPath, "Uploads", "NEWMNRFiles");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Dictionary to store multiple image paths for each MR_NO
                Dictionary<string, List<string>> mrImagePaths = new Dictionary<string, List<string>>();

                foreach (var mrItem in updateMNRList)
                {
                    string mrNo = mrItem.MR_NO ?? "UNKNOWN";

                    if (mrItem.IMAGES != null && mrItem.IMAGES.Count > 0)
                    {
                        foreach (var imageData in mrItem.IMAGES)
                        {
                            if (!string.IsNullOrEmpty(imageData.Base64))
                            {
                                // Get file extension
                                string extension = GetFileExtensionFromBase64(imageData.Base64);
                                string fileName = $"{mrNo}_{imageData.FileName ?? Guid.NewGuid().ToString()}{extension}";
                                string fullFilePath = Path.Combine(uploadPath, fileName);

                                // Convert base64 to byte array and save
                                byte[] imageBytes = Convert.FromBase64String(imageData.Base64.Split(',')[1]);
                                await System.IO.File.WriteAllBytesAsync(fullFilePath, imageBytes);

                                string storedPath = Path.Combine("Uploads", "NEWMNRFiles", fileName).Replace("\\", "/");

                                if (!mrImagePaths.ContainsKey(mrNo))
                                {
                                    mrImagePaths[mrNo] = new List<string>();
                                }
                                mrImagePaths[mrNo].Add(storedPath);
                            }
                        }
                    }
                }

                // Pass updated MR_LIST and image paths to the service
                _depoService.updateMRRequest(updateMNRList, mrImagePaths);

                return Ok(new { message = "Request updated successfully", responseCode = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
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

        [HttpGet("GetComponentList")]
        public ActionResult<Response<List<COMPONENT_DROPDOWN>>> GetComponentList(string DAMAGE_LOCATION, string DEPO_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_depoService.GetComponentList(DAMAGE_LOCATION, DEPO_CODE)));
        }

        [HttpGet("GetRepairDropdownData")]
        public ActionResult<Response<List<REPAIR_DROPDOWN>>> GetRepairDropdownData(string DAMAGE_LOCATION, string COMPONENT, string DEPO_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_depoService.GetRepairDropdownData(DAMAGE_LOCATION, COMPONENT, DEPO_CODE)));
        }

    }
}
