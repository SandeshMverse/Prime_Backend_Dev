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
    public class CredoController : ControllerBase
    {
        private ICredoService _credoService;
        private readonly IWebHostEnvironment _environment;
        public CredoController(ICredoService credoService, IWebHostEnvironment environment)
        {
            _credoService = credoService;
            _environment = environment;
        }

       [HttpGet("GetCredo")]
        public ActionResult<Response<CREDO>> GetCredoDetails(string AGENT_CODE, string VESSEL_NAME, string VOYAGE_NO, string PORT_OF_LOADING, string PORT_OF_DISCHARGE)
        {
            return Ok(JsonConvert.SerializeObject(_credoService.GetCredoDetails(AGENT_CODE, VESSEL_NAME, VOYAGE_NO, PORT_OF_LOADING, PORT_OF_DISCHARGE)));
        }

    }
}
