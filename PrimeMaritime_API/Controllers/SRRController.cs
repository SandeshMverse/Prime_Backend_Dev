using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.IServices;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Request;
using PrimeMaritime_API.Response;
using PrimeMaritime_API.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PrimeMaritime_API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class SRRController : ControllerBase
    {
        private ISRRService _srrService;
        private readonly IWebHostEnvironment _environment;
        public SRRController(ISRRService srrService, IWebHostEnvironment environment)
        {
            _srrService = srrService;
            _environment = environment;
        }

        [HttpGet("GetSRRBySRRNO")]
        public ActionResult<Response<SRR>> GetSRRBySRRNo(string SRR_NO, string AGENT_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_srrService.GetSRRBySRRNo(SRR_NO, AGENT_CODE)));
        }

        //[HttpGet("GetExcRates")]
        //public ActionResult<Response<EXC_RATE>> GetExcRates(string CURRENCY_CODE, string AGENT_CODE,string ORG_CODE, string PORT)
        //{
        //    return Ok(JsonConvert.SerializeObject(_srrService.GetExcRates(CURRENCY_CODE, AGENT_CODE,ORG_CODE,PORT)));
        //}

        [HttpGet("GetExcRateList")]
        public ActionResult<Response<List<EXC_RATE>>> GetExcRateList(string ORG_CODE, string PORT)
        {
            return Ok(JsonConvert.SerializeObject(_srrService.GetExcRateList(ORG_CODE, PORT)));
        }

        [HttpGet("GetPMExcRateList")]
        public ActionResult<Response<List<EXC_RATE>>> GetPMExcRateList()
        {
            return Ok(JsonConvert.SerializeObject(_srrService.GetPMExcRateList()));
        }

        [HttpGet("GetRate")]
        public ActionResult<Response<string>> GetRates(string POL, string POD, string CHARGE, string CONT_TYPE)
        {
            return Ok(JsonConvert.SerializeObject(_srrService.GetRate(POL, POD, CHARGE, CONT_TYPE)));
        }

        [HttpGet("GetCalRates")]
        public ActionResult<Response<RATES>> GetCalRates(string POL, string POD, string CONTAINER_TYPE, string SRR_NO, int NO_OF_CONTAINERS)
        {
            return Ok(JsonConvert.SerializeObject(_srrService.GetCalRates(POL, POD, CONTAINER_TYPE, SRR_NO, NO_OF_CONTAINERS)));
        }

        [HttpGet("GetInvoiceDetails")]
        public ActionResult<Response<INVOICE>> GetInvoiceDetails(string INVOICE_NO, string CONTAINER_TYPE)
        {
            return Ok(JsonConvert.SerializeObject(_srrService.GetInvoiceDetails(INVOICE_NO, CONTAINER_TYPE)));
        }

        [HttpGet("GetInvoiceList")]
        public ActionResult<Response<List<INVOICELIST>>> GetInvoiceList(string INVOICE_NO, string FROM_DATE, string TO_DATE, string AGENT_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_srrService.GetInvoiceList(INVOICE_NO,FROM_DATE,TO_DATE,AGENT_CODE)));
        }

        [HttpPost("InsertInvoice")]
        public ActionResult<Response<string>> InsertInvoice(INVOICELIST request)
        {
            return Ok(_srrService.InsertInvoice(request));
        }

        [HttpPost("InsertDestinationAgent")]
        public ActionResult<Response<string>> InsertDestinationAgent(string DESTINATION_AGENT_CODE, string SRR_NO)
        {
            return Ok(_srrService.InsertDestinationAgent(DESTINATION_AGENT_CODE, SRR_NO));
        }


        [HttpGet("GetSRRList")]
        public ActionResult<Response<List<SRRList>>> GetSRRList(string OPERATION, string SRR_NO, string CUSTOMER_NAME, string STATUS, string FROMDATE, string TODATE, string AGENT_CODE, string ORG_CODE, string PORT)
        {
            return Ok(JsonConvert.SerializeObject(_srrService.GetSRRList(OPERATION, SRR_NO, CUSTOMER_NAME, STATUS, FROMDATE, TODATE, AGENT_CODE,ORG_CODE,PORT)));
        }

        [HttpPost("InsertSRR")]
        public ActionResult<Response<SRR>> InsertSRR(SRRRequest request)
        {
            return Ok(_srrService.InsertSRR(request));
        }

        //[HttpPost("UploadDangerousFiles")]
        //public IActionResult UploadDangerousFiles(string SRRId)
        //{
        //    var formFile = Request.Form.Files;

        //    // Define base paths
        //    string uploadPath = Path.Combine(_environment.ContentRootPath, "Uploads", "SRRFiles");
        //    string attachmentPath = Path.Combine(uploadPath, "SRRDangerous");

        //    // Create directories if they do not exist
        //    if (!Directory.Exists(attachmentPath))
        //    {
        //        Directory.CreateDirectory(attachmentPath);
        //    }

        //    List<string> uploadedFiles = new List<string>();

        //    foreach (IFormFile postedFile in formFile)
        //    {
        //        string sanitizedFileName = Path.GetFileName(SRRId + "_" + postedFile.FileName);
        //        string fullFilePath = Path.Combine(attachmentPath, sanitizedFileName);

        //        using (FileStream stream = new FileStream(fullFilePath, FileMode.Create))
        //        {
        //            postedFile.CopyTo(stream);
        //        }

        //        // Construct relative path with forward slashes (standard convention)
        //        string relativePath = Path.Combine("Uploads", "SRRFiles", "SRRDangerous", sanitizedFileName);
        //        uploadedFiles.Add(relativePath);
        //    }

        //    // Use the first uploaded file's path
        //    string attachmentPathToSave = uploadedFiles.FirstOrDefault();

        //    // Pass only the relative path to the service
        //    _srrService.UploadDangerousFiles(SRRId, attachmentPathToSave);
        //    return Ok();
        //}

        [HttpPost("UploadDangerousFiles")]
        public IActionResult UploadDangerousFiles(int SRRId)
        {
            try
            {
                var formFiles = Request.Form.Files;

                if (formFiles == null || formFiles.Count == 0)
                {
                    return BadRequest("No files were uploaded.");
                }

                // Define base paths
                string uploadPath = Path.Combine(_environment.ContentRootPath, "Uploads", "SRRFiles");
                string attachmentPath = Path.Combine(uploadPath, "SRRDangerous");

                // Create directory if it doesn't exist
                if (!Directory.Exists(attachmentPath))
                {
                    Directory.CreateDirectory(attachmentPath);
                }

                List<string> uploadedFiles = new List<string>();

                foreach (IFormFile postedFile in formFiles)
                {
                    // Sanitize the file name
                    string sanitizedFileName = Path.GetFileName(SRRId + "_" + postedFile.FileName);
                    string fullFilePath = Path.Combine(attachmentPath, sanitizedFileName);

                    using (FileStream stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                    }

                    // Construct relative path using forward slashes
                    string relativePath = $"Uploads/SRRFiles/SRRDangerous/{sanitizedFileName}";
                    uploadedFiles.Add(relativePath);
                }

                // Use the first uploaded file's path
                string attachmentPathToSave = uploadedFiles.FirstOrDefault();

                if (!string.IsNullOrEmpty(attachmentPathToSave))
                {
                    _srrService.UploadDangerousFiles(SRRId, attachmentPathToSave);
                }

                return Ok(new { Message = "File(s) uploaded successfully", Paths = uploadedFiles });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPost("InsertExcRate")]
        public ActionResult<Response<EXC_RATE>> InsertExcRate(List<EXC_RATE> excRateList)
        {
            return Ok(_srrService.InsertExcRate(excRateList));
        }

        [HttpPost("UpdateSRR")]
        public ActionResult<Response<SRR>> UpdateSRR(List<SRR_RATES> request)
        {
            return Ok(_srrService.UpdateSRR(request));
        }

        [HttpPost("InsertContainer")]
        public ActionResult<Response<SRR>> InsertContainer(List<SRR_CONTAINERS> request)
        {
            return Ok(_srrService.InsertContainer(request));
        }

        [HttpPost("UploadFiles")]
        public IActionResult UploadFiles(string SRR_NO)
        {
            string upload = Path.Combine(_environment.ContentRootPath, "Uploads");

            if (!Directory.Exists(upload))
            {
                Directory.CreateDirectory(upload);
            }

            string path = Path.Combine(_environment.ContentRootPath, "Uploads", "SRRFiles");
            string HAZpath = Path.Combine(_environment.ContentRootPath, "Uploads", "SRRFiles/HAZFiles");
            string FLEXIBAGpath = Path.Combine(_environment.ContentRootPath, "Uploads", "SRRFiles/FLEXIBAGFiles");
            string SPpath = Path.Combine(_environment.ContentRootPath, "Uploads", "SRRFiles/SPFiles");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (!Directory.Exists(HAZpath))
            {
                Directory.CreateDirectory(HAZpath);
            }
            else if (!Directory.Exists(FLEXIBAGpath))
            {
                Directory.CreateDirectory(FLEXIBAGpath);
            }
            else if (!Directory.Exists(SPpath))
            {
                Directory.CreateDirectory(SPpath);
            }

            foreach (var key in Request.Form.Keys)
            {
                var data = Request.Form[key];

                for (int i = 0; i < data.Count; i++)
                {
                    string fileName = Path.GetFileName(SRR_NO + "_" + Request.Form.Files[i].FileName);
                    if (data[i] == "HAZ")
                    {
                        using (FileStream stream = new FileStream(Path.Combine(_environment.ContentRootPath, HAZpath, fileName), FileMode.Create))
                        {
                            Request.Form.Files[i].CopyTo(stream);
                        }
                    }
                    else if (data[i] == "FLEXIBAG")
                    {
                        using (FileStream stream = new FileStream(Path.Combine(_environment.ContentRootPath, FLEXIBAGpath, fileName), FileMode.Create))
                        {
                            Request.Form.Files[i].CopyTo(stream);
                        }
                    }
                    else if (data[i] == "SP")
                    {
                        using (FileStream stream = new FileStream(Path.Combine(_environment.ContentRootPath, SPpath, fileName), FileMode.Create))
                        {
                            Request.Form.Files[i].CopyTo(stream);
                        }
                    }
                }

            }

            return Ok();
        }



        [HttpPost("ApproveRate")]
        public ActionResult<Response<CommonResponse>> ApproveRate(List<SRR_RATES> request, int POL_FREE_DAYS, int POD_FREE_DAYS, string SLOT_OPERATOR_NAME, int RATE20, int RF20, int HAZ20)
        {
            return Ok(_srrService.ApproveRate(request, POL_FREE_DAYS, POD_FREE_DAYS, SLOT_OPERATOR_NAME, RATE20, RF20, HAZ20));
        }

        [HttpPost("CounterRate")]
        public ActionResult<Response<CommonResponse>> CounterRate(List<SRR_RATES> request,int POL_FREE_DAYS,int POD_FREE_DAYS)
        {
            return Ok(_srrService.CounterRate(request,POL_FREE_DAYS,POD_FREE_DAYS));
        }

        [HttpGet("GetSRRFiles")]
        public ActionResult<Response<List<ALL_FILE>>> GetSRRFiles([FromQuery(Name = "SRR_NO")] string SRR_NO, [FromQuery(Name = "COMM_TYPE")] string COMM_TYPE, [FromQuery(Name = "ID")] string ID)
        {
            string[] commodityList = COMM_TYPE.Split(",");

            List<List<string>> files = new List<List<string>>();

            foreach(var type in commodityList)
            {
                if (type == "HAZ")
                {
                    files.Add(Directory.GetFiles("Uploads/SRRFiles/HAZFiles/").Where(file => file.Contains(SRR_NO)).ToList());
                }
                if (type == "FLEXIBAG")
                {
                    files.Add(Directory.GetFiles("Uploads/SRRFiles/FLEXIBAGFiles/").Where(file => file.Contains(SRR_NO)).ToList());
                }
                if (type == "SP")
                {
                    files.Add(Directory.GetFiles("Uploads/SRRFiles/SPFiles/").Where(file => file.Contains(SRR_NO)).ToList());
                }
                if (type == "HAZ")
                {
                    files.Add(Directory.GetFiles("Uploads/SRRFiles/SRRDangerous/").Where(file => file.Contains(ID)).ToList());
                }
            }

            List<ALL_FILE> Files = new List<ALL_FILE>();
            Response<List<ALL_FILE>> response = new Response<List<ALL_FILE>>();

            foreach (var file in files)
            {
                if(file.Count > 0)
                {
                    foreach(var f in file)
                    {
                        ALL_FILE img = new ALL_FILE();
                        long length = new System.IO.FileInfo(f).Length / 1024;
                        img.FILE_NAME = f.Split('/')[3];
                        img.FILE_SIZE = length.ToString() + "KB";
                        img.FILE_PATH = f;

                        Files.Add(img);
                    }
                }
            }

            if (files.Count > 0)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = Files;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }
            return response;
        }

        [HttpGet("GetSRRRateList")]
        public ActionResult<Response<List<SRR_RATE_LIST>>> GetSRRRateList(string POL, string POD, string CONTAINER_TYPE, int NO_OF_CONTAINERS)
        {
            return Ok(JsonConvert.SerializeObject(_srrService.GetSRRRateList(POL,POD,CONTAINER_TYPE,NO_OF_CONTAINERS)));
        }

        //ADDED NEW 

        [HttpGet("GetExcRatesPOL")]
        public ActionResult<Response<EXC_RATE>> GetExcRatesPOL(string CURRENCY_CODE, string AGENT_CODE, string ORG_CODE, string PORT)
        {
            return Ok(JsonConvert.SerializeObject(_srrService.GetExcRatesPOL(CURRENCY_CODE, AGENT_CODE, ORG_CODE, PORT)));
        }

        [HttpGet("GetExcRatesPOD")]
        public ActionResult<Response<EXC_RATE>> GetExcRatesPOD(string CURRENCY_CODE, string AGENT_CODE, string ORG_CODE, string PORT)
        {
            return Ok(JsonConvert.SerializeObject(_srrService.GetExcRatesPOD(CURRENCY_CODE, AGENT_CODE, ORG_CODE, PORT)));
        }

        [HttpGet("GetExcRatesFRE")]
        public ActionResult<Response<EXC_RATE>> GetExcRatesFREIGHT(string CURRENCY_CODE, string AGENT_CODE, string ORG_CODE, string PORT)
        {
            return Ok(JsonConvert.SerializeObject(_srrService.GetExcRatesFREIGHT(CURRENCY_CODE, AGENT_CODE, ORG_CODE, PORT)));
        }

        [HttpPost("InsertSWITCHBLAgent")]
        public ActionResult<Response<string>> InsertSWITCHBLAgent(string SWITCHBL_AGENT_CODE, string SRR_NO)
        {
            return Ok(_srrService.InsertSWITCHBLAgent(SWITCHBL_AGENT_CODE, SRR_NO));
        }

        [HttpPost("RejectSwitchBL")]
        public ActionResult<Response<string>> RejectSwitchBL(string SRR_NO)
        {
            return Ok(_srrService.RejectSwitchBL(SRR_NO));
        }

    }
}
