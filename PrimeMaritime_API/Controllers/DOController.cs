﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.IServices;
using PrimeMaritime_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class DOController : ControllerBase
    {
        private IDOService _doService;
        public DOController(IDOService doService)
        {
            _doService = doService;
        }

        [HttpGet("GetDOList")]
        public ActionResult<Response<List<DO>>> GetDOList(string DO_NO, string FROM_DATE, string TO_DATE, string AGENT_CODE,string ORG_CODE,string PORT)
        {
            return Ok(JsonConvert.SerializeObject(_doService.GetDOList(DO_NO, FROM_DATE, TO_DATE, AGENT_CODE,ORG_CODE,PORT)));
        }

        [HttpGet("GetDOListPM")]
        public ActionResult<Response<List<DO>>> GetDOListPM(string DO_NO, string FROM_DATE, string TO_DATE)
        {
            return Ok(JsonConvert.SerializeObject(_doService.GetDOListPM(DO_NO, FROM_DATE, TO_DATE)));
        }

        [HttpPost("InsertDO")]
        public ActionResult<Response<DO>> InsertDO(DO request)
        {
            return Ok(_doService.InsertDO(request));
        }

        [HttpPost("UpdateDO")]
        public ActionResult<Response<DO>> UpdateDO(DO request)
        {
            return Ok(_doService.UpdateDO(request));
        }

        [HttpPost("EditLetterValidity")]
        public ActionResult<Response<DO>> EditLetterValidity(DO request)
        {
            return Ok(_doService.EditLetterValidity(request));
        }
        

        [HttpGet("GetDODetails")]
        public ActionResult<Response<DO>> GetDODetails(string DO_NO, string AGENT_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_doService.GetDODetails(DO_NO, AGENT_CODE)));
        }

        [HttpGet("GetDOByDONo")]
        public ActionResult<Response<DODETAILS>> GetDOByDONo(string DO_NO)
        {
            return Ok(JsonConvert.SerializeObject(_doService.GetDOByDONo(DO_NO)));
        }

        [HttpPost("GetDOExists")]
        public ActionResult<Response<DODETAILS>> GetDOExists(string BL_NO)
        {
            return Ok(JsonConvert.SerializeObject(_doService.GetDOExists(BL_NO)));
        }

        [HttpPost("CheckPaymentPaid")]
        public ActionResult<Response<INVOICE_DETAILS_FOR_DO>> CheckPaymentPaid(string BL_NO)
        {
            return Ok(JsonConvert.SerializeObject(_doService.CheckPaymentPaid(BL_NO)));
        }

        [HttpPost("CheckReceiptGenerate")]
        public ActionResult<Response<RECEIPT_INVOICE>> CheckReceiptGenerate(string INVOICE_NO)
        {
            return Ok(JsonConvert.SerializeObject(_doService.CheckReceiptGenerate(INVOICE_NO)));
        }



    }
}
