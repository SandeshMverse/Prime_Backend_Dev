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
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private IInvoiceService _invoiceService;
        private readonly IWebHostEnvironment _environment;
        public InvoiceController(IInvoiceService invoiceService, IWebHostEnvironment environment)
        {
            _invoiceService = invoiceService;
            _environment = environment;
        }

        [HttpGet("GetInvoiceBLDetails")]
        public ActionResult<Response<INVOICE_BL>> GetInvoiceBLDetails(string BL_NO, string PORT, string ORG_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_invoiceService.GetBLDetails(BL_NO, PORT, ORG_CODE)));
        }

        [HttpGet("GetCreditNoteDetails")]
        public ActionResult<Response<CREDIT_NOTE_DETAILS>> GetCreditNoteDetails(string CREDIT_NO, string PORT, string ORG_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_invoiceService.GetCreditNoteDetails(CREDIT_NO, PORT, ORG_CODE)));
        }

        [HttpPost("InsertInvoice")]
        public ActionResult<Response<CommonResponse>> InsertInvoice(INVOICE_MASTER request)
        {
            return Ok(_invoiceService.InsertInvoice(request));
        }

        [HttpPost("InsertCreditNote")]
        public ActionResult<Response<CommonResponse>> InsertCreditNote(List<CREDIT_NOTE> request)
        {
            return Ok(_invoiceService.InsertCreditNote(request));
        }

        [HttpPost("FinalizeInvoice")]
        public ActionResult<Response<CommonResponse>> FinalizeInvoice(INVOICE_FINALIZE request)
        {
            return Ok(_invoiceService.FinalizeInvoice(request));
        }

        [HttpGet("GetInvoiceList")]
        public ActionResult<Response<List<INVOICE_MASTER>>> GetInvoiceList(string FROM_DATE, string TO_DATE, string PORT, string ORG_CODE,string BL_NO)
        {
            return Ok(JsonConvert.SerializeObject(_invoiceService.GetInvoiceList(FROM_DATE, TO_DATE, PORT, ORG_CODE,BL_NO)));
        }

        [HttpGet("GetCreditList")]
        public ActionResult<Response<List<CREDIT_NOTE>>> GetCreditList(string FROM_DATE, string TO_DATE, string PORT, string ORG_CODE, string CREDIT_NO)
        {
            return Ok(JsonConvert.SerializeObject(_invoiceService.GetCreditList(FROM_DATE, TO_DATE, PORT, ORG_CODE, CREDIT_NO)));
        }

        [HttpGet("GetInvoiceDetails")]
        public ActionResult<Response<INVOICE_MASTER>> GetInvoiceDetails(int INVOICE_ID,string INVOICE_NO, string PORT, string ORG_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_invoiceService.GetInvoiceDetails(INVOICE_ID,INVOICE_NO, PORT, ORG_CODE)));
        }

        [HttpGet("GetInvoiceDetailsForReceipt")]
        public ActionResult<Response<INVOICE_DETAILS_FOR_RECEIPT>> GetInvoiceDetailsForReceipt(string INVOICE_NO, string PORT, string ORG_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_invoiceService.GetInvoiceDetailsForReceipt(INVOICE_NO,PORT, ORG_CODE)));
        }

        [HttpPost("GetBLExists")]
        public ActionResult<Response<INVOICE_BL_CHECK>> GetBLExists(string INVOICE_TYPE, string BL_NO)
        {
            return Ok(JsonConvert.SerializeObject(_invoiceService.GetBLExists(INVOICE_TYPE, BL_NO)));
        }
        //NEW ADDED SIDDHESH
        [HttpPost("GetRateExists")]
        public ActionResult<Response<INVOICE_RATE_CHECK>> GetRateExists()
        {
            return Ok(JsonConvert.SerializeObject(_invoiceService.GetRateExists()));
        }
        //NEW ADDED SIDDHESH
        [HttpPost("GetBLCustList")]
        public ActionResult<Response<GET_CUST_LIST>> GetBLCustList(string BL_NO)
        {
            return Ok(JsonConvert.SerializeObject(_invoiceService.GetBLCustList(BL_NO)));
        }
    }
}
