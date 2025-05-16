using Microsoft.AspNetCore.Authorization;
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
    //[Authorize]
    [ApiController]
    public class DetentionController : Controller
    {
        private IDetentionService _detentionService;
        public DetentionController(IDetentionService detentionService)
        {
            _detentionService = detentionService;
        }

        [HttpGet("GetDetentionListByDO")]
        public ActionResult<Response<List<DETENTION_WAIVER_REQUEST>>> GetDetentionListByDO(string DO_NO)
        {
            return Ok(JsonConvert.SerializeObject(_detentionService.GetDetentionListByDO(DO_NO)));
        }

        [HttpGet("GetDetentionListByBL")]
        public ActionResult<Response<List<DETENTION_WAIVER_REQUEST>>> GetDetentionListByBL(string BL_NO)
        {
            return Ok(JsonConvert.SerializeObject(_detentionService.GetDetentionListByBL(BL_NO)));
        }

        [HttpGet("GetDetentionListByLocation")]
        public ActionResult<Response<List<DETENTION_WAIVER_REQUEST>>> GetDetentionListByLocation(string location)
        {
            return Ok(JsonConvert.SerializeObject(_detentionService.GetDetentionListByLocation(location)));
        }


        [HttpGet("GetDetentionListByLocationAndDetentionType")]
        public ActionResult<Response<List<DETENTION_WAIVER_REQUEST>>> GetDetentionListByLocationAndDetentionType(string location,string DETENTION_TYPE)
        {
            return Ok(JsonConvert.SerializeObject(_detentionService.GetDetentionListByLocationAndDetentionType(location, DETENTION_TYPE)));
        }

        [HttpPost("InsertDetention")]
        public ActionResult<Response<DETENTION_WAIVER_REQUEST>> InsertDetention(DETENTION request)
        {
            return Ok(_detentionService.InsertDetention(request));
        }

        [HttpPost("UpdateDetention")]
        public ActionResult<Response<DETENTION_WAIVER_REQUEST>> UpdateDetention(DETENTION request)
        {
            return Ok(_detentionService.UpdateDetention(request));
        }

        [HttpPost("UpdateDetentionByBL")]
        public ActionResult<Response<DETENTION_WAIVER_REQUEST>> UpdateDetentionByBL(DETENTION request)
        {
            return Ok(_detentionService.UpdateDetentionByBL(request));
        }

        [HttpGet("GetTotalDetentionCost")]
        public ActionResult<Response<decimal>> GetTotalDetentionCost(string CONTAINER_NO)
        {
            return Ok(JsonConvert.SerializeObject(_detentionService.GetTotalDetentionCost(CONTAINER_NO)));
        }

        [HttpGet("GetContainerDetentionList")]
        public ActionResult<Response<List<CONTAINER_DETENTION>>> GetContainerDetentionList()
        {
            return Ok(JsonConvert.SerializeObject(_detentionService.GetContainerDetentionList()));
        }

        [HttpGet("GetDODetailsForDetention")]
        public ActionResult<Response<DO_DETENTION_DETAILS>> GetDODetailsForDetention(string DO_NO)
        {
            return Ok(JsonConvert.SerializeObject(_detentionService.GetDODetailsForDetention(DO_NO)));
        }

        [HttpGet("GetBLDetailsForDetention")]
        public ActionResult<Response<DO_DETENTION_DETAILS>> GetBLDetailsForDetention(string BL_NO)
        {
            return Ok(JsonConvert.SerializeObject(_detentionService.GetBLDetailsForDetention(BL_NO)));
        }

        [HttpGet("GetDetentionCharges")]
        public ActionResult<Response<DETENTION_MASTER>> GetDetentionCharges(string ACCEPTANCE_LOCATION, int DAYS, string CURRENCY_CODE, string CONTAINER_TYPE, string IS_JUMPING, int FREEDAYS)
        {
            return Ok(JsonConvert.SerializeObject(_detentionService.GetDetentionCharges(ACCEPTANCE_LOCATION, DAYS, CURRENCY_CODE, CONTAINER_TYPE, IS_JUMPING, FREEDAYS)));
        }

        [HttpGet("GetDetentionMasterList")]
        public ActionResult<Response<List<PARTY_MASTER>>> GetDetentionMasterList(string LOCATION, string MONTH, string YEAR)
        {
            return Ok(JsonConvert.SerializeObject(_detentionService.GetDetentionMasterList(LOCATION, MONTH, YEAR)));
        }
    }
}
