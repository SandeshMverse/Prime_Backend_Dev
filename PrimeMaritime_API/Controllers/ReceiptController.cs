using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.IServices;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Response;
using PrimeMaritime_API.Services;
using System.Collections.Generic;

namespace PrimeMaritime_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private IReceiptService _receiptService;
        private readonly IWebHostEnvironment _environment;
        public ReceiptController(IReceiptService receiptService, IWebHostEnvironment environment)
        {
            _receiptService = receiptService;
            _environment = environment;
        }

        [HttpPost("InsertReceipt")]
        public ActionResult<Response<CommonResponse>> InsertInvoice(RECEIPT request)
        {
             return Ok(_receiptService.InsertReceipt(request));
        }

        [HttpGet("GetReceiptList")]
        public ActionResult<Response<List<RECEIPT_INVOICE>>> GetReceiptList(string FROM_DATE, string TO_DATE, string PORT, string ORG_CODE, string AGENT_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_receiptService.GetReceiptList(FROM_DATE, TO_DATE, PORT, ORG_CODE, AGENT_CODE)));
        }

        [HttpGet("CheckReceiptExist")]
        public ActionResult<Response<RECEIPT_INVOICE>> CheckReceiptExist(string BL_NO, string INVOICE_NO)
        {
            return Ok(JsonConvert.SerializeObject(_receiptService.CheckReceiptExist(BL_NO, INVOICE_NO)));
        }
    }
}
