using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.IServices;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private IMasterService _masterService;
        private readonly IWebHostEnvironment _environment;
        public MasterController(IMasterService masterService, IWebHostEnvironment environment)
        {
            _masterService = masterService;
            _environment = environment;
        }

        #region "PARTY MASTER"
        [HttpPost("InsertPartyMaster")]
        public ActionResult<Response<CommonResponse>> InsertPartyMaster(PARTY_MASTER request)
        {
            return Ok(_masterService.InsertPartyMaster(request));
        }


        [HttpPost("UploadCustomerFiles")]
        public IActionResult UploadCustomerFiles(string CUSTID)
        {
            var formFile = Request.Form.Files;

            string path = Path.Combine(_environment.ContentRootPath, "Uploads", "CustomerFiles");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in formFile)
            {
                string fileName = Path.GetFileName(CUSTID + "_" + postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(_environment.ContentRootPath, path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                }
            }

            return Ok();
        }

        [HttpGet("GetPartyMasterList")]
        public ActionResult<Response<List<PARTY_MASTER>>> GetPartyMasterList(string AGENT_CODE, string CUST_NAME, string CUST_TYPE, bool STATUS, string FROM_DATE, string TO_DATE, bool IS_VENDOR)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetPartyMasterList(AGENT_CODE, CUST_NAME, CUST_TYPE, STATUS, FROM_DATE, TO_DATE, IS_VENDOR)));
        }

        [HttpGet("GetPartyMasterDetails")]
        public ActionResult<Response<PARTY_MASTER>> GetPartyMasterDetails(string AGENT_CODE, int CUSTOMER_ID, int VENDOR_AGREEMENT_ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetPartyMasterDetails(AGENT_CODE, CUSTOMER_ID, VENDOR_AGREEMENT_ID)));
        }

        [HttpDelete("DeletePartyMasterDetails")]
        public ActionResult<Response<CommonResponse>> DeletePartyMasterDetails(int CUSTOMER_ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeletePartyMasterDetails(CUSTOMER_ID)));
        }

        [HttpPost("UpdatePartyMasterDetails")]
        public ActionResult<Response<CommonResponse>> UpdatePartyMasterDetails(PARTY_MASTER request)
        {
            return Ok(_masterService.UpdatePartyMasterDetails(request));
        }
        #endregion

        #region "CONTAINER MASTER"
        [HttpPost("InsertContainerMaster")]
        public ActionResult<Response<CommonResponse>> InsertContainerMaster(CONTAINER_MASTER request)
        {
            return Ok(_masterService.InsertContainerMaster(request));
        }

        [HttpGet("GetContainerMasterList")]
        public ActionResult<Response<List<CONTAINER_MASTER>>> GET_CONTAINERLIST(string CONTAINER_NO, string CONTAINER_TYPE, string CONTAINER_SIZE, bool STATUS, string ONHIRE_DATE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetContainerMasterList(CONTAINER_NO, CONTAINER_TYPE, CONTAINER_SIZE, STATUS, ONHIRE_DATE)));
        }

        [HttpGet("GetContainerMasterDetails")]
        public ActionResult<Response<CONTAINER_MASTER>> GetContainerMasterDetails(int ID, string CONTAINER_NO, string DEPO_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetContainerMasterDetails(ID, CONTAINER_NO, DEPO_CODE)));
        }


        [HttpPost("UpdateContainerMasterList")]
        public ActionResult<Response<CommonResponse>> UpdateContainerMasterList(CONTAINER_MASTER request)
        {
            return Ok(_masterService.UpdateContainerMasterList(request));
        }

        [HttpDelete("DeleteContainerMasterList")]
        public ActionResult<Response<CommonResponse>> DeleteContainerMasterList(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteContainerMasterList(ID)));
        }
        #endregion

        #region "COMMON MASTER"
        [HttpPost("InsertMaster")]
        public ActionResult<Response<CommonResponse>> InsertMaster(MASTER request)
        {
            return Ok(_masterService.InsertMaster(request));
        }

        [HttpGet("GetMasterList")]
        public ActionResult<Response<List<MASTER>>> GetMasterList(string key, string FROM_DATE, string TO_DATE, string STATUS)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetMasterList(key, FROM_DATE, TO_DATE, STATUS)));
        }

        [HttpGet("GetMasterDetails")]
        public ActionResult<Response<MASTER>> GetMasterDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetMasterDetails(ID)));
        }

        [HttpPost("UpdateMaster")]
        public ActionResult<Response<CommonResponse>> UpdateMaster(MASTER request)
        {
            return Ok(_masterService.UpdateMaster(request));
        }

        [HttpDelete("DeleteMaster")]
        public ActionResult<Response<CommonResponse>> DeleteMaster(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteMaster(ID)));
        }
        #endregion

        #region "VESSEL MASTER"
        [HttpPost("InsertVesselMaster")]
        public ActionResult<Response<CommonResponse>> InsertVesselMaster(VESSEL_MASTER request)
        {
            return Ok(_masterService.InsertVesselMaster(request));
        }

        [HttpGet("GetVesselMasterList")]
        public ActionResult<Response<List<VESSEL_MASTER>>> GetVesselMasterList(string VESSEL_NAME, string IMO_NO, string STATUS, string FROM_DATE, string TO_DATE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetVesselMasterList(VESSEL_NAME, IMO_NO, STATUS, FROM_DATE, TO_DATE)));
        }

        [HttpGet("GetVesselMasterDetails")]
        public ActionResult<Response<VESSEL_MASTER>> GetVesselMasterDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetVesselMasterDetails(ID)));
        }

        [HttpPost("UpdateVesselMasterList")]
        public ActionResult<Response<CommonResponse>> UpdateVesselMasterList(VESSEL_MASTER request)
        {
            return Ok(_masterService.UpdateVesselMasterList(request));
        }

        [HttpDelete("DeleteVesselMasterList")]
        public ActionResult<Response<CommonResponse>> DeleteVesselMasterList(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteVesselMasterList(ID)));
        }
        #endregion

        #region "SERVICE MASTER"
        [HttpPost("InsertServiceMaster")]
        public ActionResult<Response<CommonResponse>> InsertServiceMaster(SERVICE_MASTER request)
        {
            return Ok(_masterService.InsertServiceMaster(request));
        }

        [HttpGet("GetServiceMasterList")]
        public ActionResult<Response<List<SERVICE_MASTER>>> GetServiceMasterList()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetServiceMasterList()));
        }

        [HttpGet("GetServiceMasterDetails")]
        public ActionResult<Response<SERVICE_MASTER>> GetServiceMasterDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetServiceMasterDetails(ID)));
        }

        [HttpPost("UpdateServiceMasterList")]
        public ActionResult<Response<CommonResponse>> UpdateServiceMasterList(SERVICE_MASTER request)
        {
            return Ok(_masterService.UpdateServiceMasterList(request));
        }

        [HttpDelete("DeleteServiceMasterList")]
        public ActionResult<Response<CommonResponse>> DeleteServiceMasterList(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteServiceMasterList(ID)));
        }
        #endregion

        #region "CONTAINER TYPE MASTER"
        [HttpPost("InsertContainerTypeMaster")]
        public ActionResult<Response<CommonResponse>> InsertContainerTypeMaster(CONTAINER_TYPE request)
        {
            return Ok(_masterService.InsertContainerTypeMaster(request));
        }

        [HttpGet("GetContainerTypeMasterList")]
        public ActionResult<Response<List<CONTAINER_TYPE>>> GetContainerTypeMasterList(string CONT_TYPE_CODE, string CONTAINER_TYPE, string CONTAINER_SIZE, bool STATUS, string FROM_DATE, string TO_DATE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetContainerTypeMasterList(CONT_TYPE_CODE, CONTAINER_TYPE, CONTAINER_SIZE, STATUS, FROM_DATE, TO_DATE)));
        }

        [HttpGet("GetContainerTypeMasterDetails")]
        public ActionResult<Response<CONTAINER_TYPE>> GetContainerTypeMasterDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetContainerTypeMasterDetails(ID)));
        }

        [HttpPost("UpdateConatinerTypeMaster")]
        public ActionResult<Response<CommonResponse>> UpdateConatinerTypeMaster(CONTAINER_TYPE request)
        {
            return Ok(_masterService.UpdateConatinerTypeMaster(request));
        }

        [HttpDelete("DeleteContainerTypeMaster")]
        public ActionResult<Response<CommonResponse>> DeleteContainerTypeMaster(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteContainerTypeMaster(ID)));
        }

        #endregion

        #region ICD MASTER"

        [HttpGet("GetMstICD")]
        public ActionResult<Response<List<ICD_MASTER>>> GetMstICD()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetICDMasterList()));
        }


        #endregion

        #region DEPO MASTER"

        [HttpGet("GetMstDEPO")]
        public ActionResult<Response<List<DEPO_MASTER>>> GetMstDEPO()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetDEPOMasterList()));
        }


        #endregion

        #region TERMINAL MASTER"

        [HttpGet("GetMstTerminal")]
        public ActionResult<Response<List<DEPO_MASTER>>> GetMstTerminal()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetTerminalMasterList()));
        }


        #endregion

        #region CLEARING_PARTY"

        [HttpGet("GetClearingPartyList")]
        public ActionResult<Response<List<CLEARING_PARTY>>> GetClearingPartyList()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetClearingPartyList()));
        }

        [HttpPost("InsertCP")]
        public ActionResult<Response<CommonResponse>> InsertCP(CLEARING_PARTY request)
        {
            return Ok(_masterService.InsertCP(request));
        }

        #endregion

        #region "LINER"
        [HttpPost("InsertLiner")]
        public ActionResult<Response<CommonResponse>> InsertLiner(LINER request)
        {
            return Ok(_masterService.InsertLiner(request));
        }

        [HttpGet("GetLinerList")]

        public ActionResult<Response<List<CommonResponse>>> GetLinerList(string NAME, string CODE, string DESCRIPTION, bool STATUS, string FROM_DATE, string TO_DATE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetLinerList(NAME, CODE, DESCRIPTION, STATUS, FROM_DATE, TO_DATE)));
        }

        [HttpPost("UpdateLinerList")]
        public ActionResult<Response<CommonResponse>> UpdateLinerList(LINER request)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.UpdateLinerList(request)));
        }

        [HttpDelete("DeleteLinerList")]
        public ActionResult<Response<CommonResponse>> DeleteLinerList(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteLinerList(ID)));
        }

        [HttpGet("GetLinerDetails")]
        public ActionResult<Response<LINER>> GetLinerDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetLinerDetails(ID)));
        }

        #endregion

        #region "LinerService"
        [HttpPost("InsertService")]
        public ActionResult<Response<CommonResponse>> InsertService(SERVICE request)
        {
            return Ok(_masterService.InsertService(request));
        }

        [HttpGet("GetServiceList")]
        public ActionResult<Response<List<CommonResponse>>> GetServiceList(bool STATUS, string FROM_DATE, string TO_DATE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetServiceList(STATUS, FROM_DATE, TO_DATE)));
        }

        [HttpGet("GetServiceDetails")]

        public ActionResult<Response<SERVICE>> GetServiceDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetServiceDetails(ID)));
        }

        [HttpPost("UpdateService")]
        public ActionResult<Response<CommonResponse>> UpdateService(SERVICE request)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.UpdateService(request)));
        }

        [HttpDelete("DeleteService")]
        public ActionResult<Response<CommonResponse>> DeleteService(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteService(ID)));
        }

        [HttpGet("LinerServiceHistory")]
        public ActionResult<Response<List<HISTORY_PORT>>> LinerServiceHistory(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.LinerServiceHistory(ID)));
        }

        #endregion

        #region "VESSELSCHEDULE"
        [HttpPost("InsertSchedule")]
        public ActionResult<Response<CommonResponse>> InsertSchedule(SCHEDULE request)
        {
            return Ok(_masterService.InsertSchedule(request));
        }

        [HttpGet("GetScheduleList")]

        public ActionResult<Response<List<CommonResponse>>> GetScheduleList(string VESSEL_NAME, string PORT_CODE, bool STATUS, string ETA, string ETD)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetScheduleList(VESSEL_NAME, PORT_CODE, STATUS, ETA, ETD)));
        }

        [HttpGet("getScheduleListWithFilter")]

        public ActionResult<Response<List<CommonResponse>>> getScheduleListWithFilter(string VESSEL_NAME, string PORT_CODE, bool STATUS, string ETA, string ETD)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.getScheduleListWithFilter(VESSEL_NAME, PORT_CODE, STATUS, ETA, ETD)));
        }

        [HttpGet("GetDetailsByVesselAndVoyage")]

        public ActionResult<Response<SCHEDULE>> GetDetailsByVesselAndVoyage(string VESSEL_NAME, string VOYAGE_NO)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetDetailsByVesselAndVoyage(VESSEL_NAME, VOYAGE_NO)));
        }

        [HttpGet("GetLinerServiceDetails")]

        public ActionResult<Response<LINER_SERVICE>> GetLinerServiceDetails(string SERVICE_NAME, string PORT_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetLinerServiceDetails(SERVICE_NAME, PORT_CODE)));
        }

        [HttpGet("getVesselScheduleDetails")]

        public ActionResult<Response<List<CommonResponse>>> getVesselScheduleDetails(string VESSEL_NAME, string VOYAGE_NO, string SERVICE_NAME)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.getVesselScheduleDetails(VESSEL_NAME, VOYAGE_NO, SERVICE_NAME)));
        }

        [HttpGet("GetScheduleDetails")]
        public ActionResult<Response<SCHEDULE>> GetScheduleDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetScheduleDetails(ID)));
        }

        [HttpPost("UpdateSchedule")]
        public ActionResult<Response<CommonResponse>> UpdateSchedule(SCHEDULE request)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.UpdateSchedule(request)));
        }

        [HttpDelete("DeleteSchedule")]
        public ActionResult<Response<CommonResponse>> DeleteSchedule(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteSchedule(ID)));
        }

        [HttpPost("uploadvesselschedule")]
        public ActionResult<Response<string>> uploadvesselschedule(List<SCHEDULE> schedule)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.uploadvesselschedule(schedule)));
        }

        [HttpPost("Updatevesselschedule")]
        public ActionResult<Response<string>> Updatevesselschedule(List<SCHEDULE> request)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.Updatevesselschedule(request)));
        }

        #endregion

        #region "VESSEL VOYAGE"
        [HttpGet("GetVoyageList")]

        public ActionResult<Response<List<CommonResponse>>> GetVoyageList(bool STATUS, string FROM_DATE, string TO_DATE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetVoyageList(STATUS, FROM_DATE, TO_DATE)));
        }

        [HttpGet("GetVoyageDetails")]
        public ActionResult<Response<VOYAGE>> GetVoyageDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetVoyageDetails(ID)));
        }

        [HttpPost("UpdateVoyage")]
        public ActionResult<Response<CommonResponse>> UpdateVoyage(VOYAGE request)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.UpdateVoyage(request)));
        }

        [HttpDelete("DeleteVoyage")]
        public ActionResult<Response<CommonResponse>> DeleteVoyage(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteVoyage(ID)));
        }

        #endregion

        #region "LOCATION MASTER"
        [HttpPost("InsertLocationMaster")]
        public ActionResult<Response<CommonResponse>> InsertLocationMaster(LOCATION_MASTER request)
        {
            return Ok(_masterService.InsertLocationMaster(request));
        }

        [HttpGet("GetLocationMasterList")]
        public ActionResult<Response<List<LOCATION_MASTER>>> GetLocationMasterList(string LOC_NAME, string LOC_TYPE, bool STATUS, string FROM_DATE, string TO_DATE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetLocationMasterList(LOC_NAME, LOC_TYPE, STATUS, FROM_DATE, TO_DATE)));
        }

        [HttpGet("GetLocationMasterDetails")]
        public ActionResult<Response<LOCATION_MASTER>> GetLocationMasterDetails(string LOC_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetLocationMasterDetails(LOC_CODE)));
        }

        [HttpPost("UpdateLocationMasterList")]
        public ActionResult<Response<CommonResponse>> UpdateLocationMasterList(LOCATION_MASTER request)
        {
            return Ok(_masterService.UpdateLocationMasterList(request));
        }

        [HttpPost("DeleteLocationMasterList")]
        public ActionResult<Response<CommonResponse>> DeleteLocationMasterList(string LOC_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteLocationMasterList(LOC_CODE)));
        }
        #endregion

        #region "FREIGHT MASTER"
        [HttpPost("InsertFreightMaster")]
        public ActionResult<Response<CommonResponse>> InsertFreightMaster(FREIGHT_MASTER request)
        {
            return Ok(_masterService.InsertFreightMaster(request));
        }

        [HttpGet("GetFreightMasterList")]
        public ActionResult<Response<List<FREIGHT_MASTER>>> GetFreightMasterList()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetFreightMasterList()));
        }

        [HttpGet("GetFreightMasterDetails")]
        public ActionResult<Response<FREIGHT_MASTER>> GetFreightMasterDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetFreightMasterDetails(ID)));
        }

        [HttpPost("UpdateFreightMasterList")]
        public ActionResult<Response<CommonResponse>> UpdateFreightMasterList(FREIGHT_MASTER request)
        {
            return Ok(_masterService.UpdateFreightMasterList(request));
        }

        [HttpDelete("DeleteFreightMasterList")]
        public ActionResult<Response<CommonResponse>> DeleteFreightMasterList(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteFreightMasterList(ID)));
        }
        #endregion

        #region "CHARGE MASTER"       
        [HttpGet("GetChargeMasterList")]
        public ActionResult<Response<List<CHARGE_MASTER>>> GetChargeMasterList()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetChargeMasterList()));
        }

        [HttpGet("GetChargeMasterDetails")]
        public ActionResult<Response<CHARGE_MASTER>> GetChargeMasterDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetChargeMasterDetails(ID)));
        }

        [HttpPost("UpdateChargeMasterList")]
        public ActionResult<Response<CommonResponse>> UpdateChargeMasterList(CHARGE_MASTER request)
        {
            return Ok(_masterService.UpdateChargeMasterList(request));
        }

        [HttpDelete("DeleteChargeMasterList")]
        public ActionResult<Response<CommonResponse>> DeleteChargeMasterList(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteChargeMasterList(ID)));
        }
        #endregion

        #region "STEVEDORING MASTER"       
        [HttpGet("GetStevedoringMasterList")]
        public ActionResult<Response<List<STEV_MASTER>>> GetStevedoringMasterList()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetStevedoringMasterList()));
        }

        [HttpGet("GetStevedoringMasterDetails")]
        public ActionResult<Response<STEV_MASTER>> GetStevedoringMasterDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetStevedoringMasterDetails(ID)));
        }

        [HttpPost("UpdateStevedoringMasterList")]
        public ActionResult<Response<CommonResponse>> UpdateStevedoringMasterList(STEV_MASTER request)
        {
            return Ok(_masterService.UpdateStevedoringMasterList(request));
        }

        [HttpDelete("DeleteStevedoringMasterList")]
        public ActionResult<Response<CommonResponse>> DeleteStevedoringMasterList(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteStevedoringMasterList(ID)));
        }
        #endregion

        #region "DETENTION MASTER"       
        [HttpGet("GetDetentionMasterList")]
        public ActionResult<Response<List<DETENTION_MASTER>>> GetDetentionMasterList()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetDetentionMasterList()));
        }

        [HttpGet("GetDetentionMasterDetails")]
        public ActionResult<Response<DETENTION_MASTER>> GetDetentionMasterDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetDetentionMasterDetails(ID)));
        }

        [HttpPost("UpdateDetentionMasterList")]
        public ActionResult<Response<CommonResponse>> UpdateDetentionMasterList(DETENTION_MASTER request)
        {
            return Ok(_masterService.UpdateDetentionMasterList(request));
        }

        [HttpDelete("DeleteDetentionMasterList")]
        public ActionResult<Response<CommonResponse>> DeleteDetentionMasterList(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteDetentionMasterList(ID)));
        }
        #endregion

        #region "MANDATORY MASTER"       
        [HttpGet("GetMandatoryMasterList")]
        public ActionResult<Response<List<MANDATORY_MASTER>>> GetMandatoryMasterList()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetMandatoryMasterList()));
        }

        [HttpGet("GetMandatoryMasterDetails")]
        public ActionResult<Response<MANDATORY_MASTER>> GetMandatoryMasterDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetMandatoryMasterDetails(ID)));
        }

        [HttpPost("UpdateMandatoryMasterList")]
        public ActionResult<Response<CommonResponse>> UpdateMandatoryMasterList(MANDATORY_MASTER request)
        {
            return Ok(_masterService.UpdateMandatoryMasterList(request));
        }

        [HttpDelete("DeleteMandatoryMasterList")]
        public ActionResult<Response<CommonResponse>> DeleteMandatoryMasterList(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteMandatoryMasterList(ID)));
        }
        #endregion

        #region "UPLOAD TARIFF"
        [HttpPost("UploadFreightTariff")] //ANAGHA
        public ActionResult<Response<string>> UploadFreightTariff(List<FREIGHT_MASTER> master)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.UploadFreightTariff(master)));
        }

        [HttpPost("UploadChargeTariff")] //ANAGHA
        public ActionResult<Response<string>> UploadChargeTariff(List<CHARGE_MASTER> master)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.UploadChargeTariff(master)));
        }

        [HttpPost("UploadStevTariff")] //ANAGHA
        public ActionResult<Response<string>> UploadStevTariff(List<STEV_MASTER> master)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.UploadStevTariff(master)));
        }

        [HttpPost("UploadDetentionTariff")] //ANAGHA
        public ActionResult<Response<string>> UploadDetentionTariff(List<DETENTION_MASTER> master)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.UploadDetentionTariff(master)));
        }

        [HttpPost("UploadMandatoryTariff")] //ANAGHA
        public ActionResult<Response<string>> UploadMandatoryTariff(List<MANDATORY_MASTER> master)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.UploadMandatoryTariff(master)));
        }

        [HttpPost("UploadSlotRateTariff")] //siddhesh
        public ActionResult<Response<string>> UploadSlotRateTariff(List<SLOT_RATE_MASTER> master)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.UploadSlotRateTariff(master)));
        }

        [HttpPost("UploadMNRTariff")] //siddhesh
        public ActionResult<Response<string>> UploadMNRTariff(List<MNR_TARIFF_LIST> master)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.UploadMNRTariff(master)));
        }
        #endregion

        #region "ORGANISATION MASTER"
        [HttpPost("InsertOrgMaster")]
        public ActionResult<Response<CommonResponse>> InsertOrgMaster(ORG_MASTER request)
        {
            return Ok(_masterService.InsertOrgMaster(request));
        }

        [HttpPost("ValidateOrgCode")]
        public ActionResult<Response<CommonResponse>> ValidateOrgCode(string ORG_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.ValidateOrgCode(ORG_CODE)));
        }

        [HttpGet("GetOrgMasterList")]
        public ActionResult<Response<List<ORG_MASTER>>> GetOrgMasterList()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetOrgMasterList()));
        }

        [HttpGet("GetOrgMasterDetails")]
        public ActionResult<Response<ORG_MASTER>> GetOrgMasterDetails(string ORG_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetOrgMasterDetails(ORG_CODE)));
        }

        [HttpPost("UpdateOrgMasterList")]
        public ActionResult<Response<CommonResponse>> UpdateOrgMasterList(ORG_MASTER request)
        {
            return Ok(_masterService.UpdateOrgMasterList(request));
        }

        [HttpPost("DeleteOrgMasterList")]
        public ActionResult<Response<CommonResponse>> DeleteOrgMasterList(string ORG_CODE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteOrgMasterList(ORG_CODE)));
        }
        #endregion

        #region "SLOT MASTER"
        [HttpPost("InsertSlotMaster")]
        public ActionResult<Response<CommonResponse>> InsertSlotMaster(SLOT_MASTER request)
        {
            return Ok(_masterService.InsertSlotMaster(request));
        }

        [HttpGet("GetSlotMasterList")]
        public ActionResult<Response<List<SLOT_MASTER>>> GetSlotMasterList(string SERVICE, string PORT)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetSlotMasterList(SERVICE, PORT)));
        }

        [HttpGet("GetSlotMasterDetails")]
        public ActionResult<Response<SLOT_MASTER>> GetSlotMasterDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetSlotMasterDetails(ID)));
        }

        [HttpPost("UpdateSlotMasterList")]
        public ActionResult<Response<CommonResponse>> UpdateSlotMasterList(SLOT_MASTER request)
        {
            return Ok(_masterService.UpdateSlotMasterList(request));
        }

        [HttpPost("DeleteSlotMasterList")]
        public ActionResult<Response<CommonResponse>> DeleteSlotMasterList(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteSlotMasterList(ID)));
        }
        #endregion

        #region "Charges MASTER"
        [HttpPost("InsertChargesMaster")]
        public ActionResult<Response<CommonResponse>> InsertChargeMaster(CHARGES_MASTER request)
        {
            return Ok(_masterService.InsertChargeMaster(request));
        }

        [HttpGet("GetChargeMaster")]
        public ActionResult<Response<List<CHARGES_MASTER>>> GetChargeMaster()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetChargeMaster()));
        }

        [HttpGet("GetChargeMastersDetails")]
        public ActionResult<Response<CHARGES_MASTER>> GetChargeMastersDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetChargeMastersDetails(ID)));
        }


        [HttpPost("UpdateChargesMaster")]
        public ActionResult<Response<CommonResponse>> UpdateChargesMaster(CHARGES_MASTER request)
        {
            return Ok(_masterService.UpdateChargesMaster(request));
        }

        [HttpDelete("DeleteChargesMaster")]
        public ActionResult<Response<CommonResponse>> DeleteChargesMaster(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteChargesMaster(ID)));
        }

        #endregion

        #region "HSN-CODE"
        [HttpPost("InsertHsnCode")]
        public ActionResult<Response<CommonResponse>> InsertHsnCode(HSN_MASTER request)
        {
            return Ok(_masterService.InsertHsnCode(request));
        }

        [HttpGet("GetHsnMaster")]
        public ActionResult<Response<List<HSN_MASTER>>> GetHsnMaster()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetHsnMaster()));
        }

        [HttpDelete("DeleteHsnMaster")]
        public ActionResult<Response<CommonResponse>> DeleteHsnMaster(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteHsnMaster(ID)));
        }
        #endregion

        #region "COUNTRY MASTER"
        [HttpPost("InsertCountryMaster")]
        public ActionResult<Response<CommonResponse>> InsertCountryMaster(COUNTRY_MASTER request)
        {
            return Ok(_masterService.InsertCountryMaster(request));
        }

        [HttpGet("GetCountryMasterList")]
        public ActionResult<Response<List<COUNTRY_MASTER>>> GetCountryMasterList()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetCountryMasterList()));
        }

        [HttpGet("GetCountryMasterDetails")]
        public ActionResult<Response<COUNTRY_MASTER>> GetCountryMasterDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetCountryMasterDetails(ID)));
        }


        [HttpPost("UpdateCountryMasterList")]
        public ActionResult<Response<CommonResponse>> UpdateCountryMasterList(COUNTRY_MASTER request)
        {
            return Ok(_masterService.UpdateCountryMasterList(request));
        }

        [HttpDelete("DeleteCountryMasterList")]
        public ActionResult<Response<CommonResponse>> DeleteCountryMasterList(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteCountryMasterList(ID)));
        }
        #endregion

        #region "STATE MASTER"
        [HttpPost("InsertStateMaster")]
        public ActionResult<Response<CommonResponse>> InsertStateMaster(STATE_MASTER request)
        {
            return Ok(_masterService.InsertStateMaster(request));
        }

        [HttpGet("GetStateMasterList")]
        public ActionResult<Response<List<STATE_MASTER>>> GetStateMasterList()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetStateMasterList()));
        }

        [HttpGet("GetStateMasterDetails")]
        public ActionResult<Response<STATE_MASTER>> GetStateMasterDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetStateMasterDetails(ID)));
        }


        [HttpPost("UpdateStateMasterList")]
        public ActionResult<Response<CommonResponse>> UpdateStateMasterList(STATE_MASTER request)
        {
            return Ok(_masterService.UpdateStateMasterList(request));
        }

        [HttpDelete("DeleteStateMasterList")]
        public ActionResult<Response<CommonResponse>> DeleteStateMasterList(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteStateMasterList(ID)));
        }
        #endregion

        #region "IAL"
        [HttpGet("GetIAL")]
        public ActionResult<Response<IAL>> GetIALDetails(string AGENT_CODE, string VESSEL_NAME, string VOYAGE_NO, string PORT_OF_LOADING)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetIALDetails(AGENT_CODE, VESSEL_NAME, VOYAGE_NO, PORT_OF_LOADING)));
        }

        #endregion

        #region "EAL"
        [HttpGet("GetEAL")]
        public ActionResult<Response<EAL>> GetEALDetails(string AGENT_CODE, string VESSEL_NAME, string VOYAGE_NO, string PORT_OF_DISCHARGE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetEALDetails(AGENT_CODE, VESSEL_NAME, VOYAGE_NO, PORT_OF_DISCHARGE)));
        }

        #endregion

        #region "SLOT RATE MASTER"       
        [HttpGet("GetSlotRateList")]
        public ActionResult<Response<List<SLOT_RATE_MASTER>>> GetSlotRateList()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetSlotRateList()));
        }
        [HttpGet("GetSlotRateDetails")]
        public ActionResult<Response<SLOT_RATE_MASTER>> GetSlotRateMasterDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetSlotRateMasterDetails(ID)));
        }
        [HttpPost("UpdateSlotRate")]
        public ActionResult<Response<CommonResponse>> UpdateSlotRateMaster(SLOT_RATE_MASTER request)
        {
            return Ok(_masterService.UpdateSlotRateMaster(request));
        }
        [HttpDelete("DeleteSlotRate")]
        public ActionResult<Response<CommonResponse>> DeleteSlotRateMasterList(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteSlotRateMasterList(ID)));
        }

        [HttpGet("HistorySlotRate")]
        public ActionResult<Response<List<SLOT_RATE_HISTORY_MASTER>>> HistorySlotRateMasterList(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.HistorySlotRateMasterList(ID)));
        }
        #endregion

        #region "FLEET-COMPOSITION REPORTS" 
        [HttpGet("GetfleetReport")]
        public ActionResult<Response<List<FLEET_COMPOSITION_REPORT>>> GetFleetReport(string LOCATION)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetFleetReport(LOCATION)));
        }
        #endregion

        #region"STOCK-POSITION REPORTS"

        [HttpGet("GetStockReport")]
        public ActionResult<Response<List<STOCK_POSITION_REPORT>>> GetStockReport(string LOCATION)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetStockReport(LOCATION)));
        }

        #endregion

        #region "CONATINER TURN TIME REPORT"

        [HttpGet("GetContainerTimeReport")]
        public ActionResult<Response<List<CONTAINER_TURN_TIME_REPORT>>> GetContainerTurnReport(string LOCATION)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetContainerTurnReport(LOCATION)));
        }
        #endregion

        #region "CONATINER REPAIRS"

        [HttpGet("GetContainerRepairs")]
        public ActionResult<Response<List<CONTAINER_REPAIRS>>> GetContainerRepairs(string LOCATION)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetContainerRepairs(LOCATION)));
        }
        #endregion

        #region "SRR REPORTS"

        [HttpGet("GetSRRList")]
        public ActionResult<Response<List<SRR_LIST>>> GetSRRLIST(string LOCATION, string AGENT_NAME, string MONTH, string YEAR)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetSRRLIST(LOCATION, AGENT_NAME, MONTH, YEAR)));
        }
        #endregion

        #region "DOCS REPORTS"

        [HttpGet("GetDOCSList")]
        public ActionResult<Response<List<DOCS_LIST>>> GetDOCSLIST(string LOCATION, string MONTH, string YEAR)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetDOCSLIST(LOCATION, MONTH, YEAR)));
        }
        #endregion

        #region "GET AGENT"

        [HttpGet("GetAgent")]
        public ActionResult<Response<List<AGENT_NAME>>> GetAgent()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetAgent()));
        }
        #endregion

        #region "GET SHIPPER-REPORT"

        [HttpGet("GetShipperList")]
        public ActionResult<Response<List<SHIPPER_LIST>>> GetShipperLIST(string LOCATION, string CUSTOMER_NAME, string MONTH, string YEAR)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetShipperLIST(LOCATION, CUSTOMER_NAME, MONTH, YEAR)));
        }
        #endregion

        #region "GET CONSIGNEE-REPORT"

        [HttpGet("GetConsigneeList")]
        public ActionResult<Response<List<CONSIGNEE_LIST>>> GetConsigneeLIST(string LOCATION, string CONSIGNEE_NAME, string MONTH, string YEAR)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetConsigneeLIST(LOCATION, CONSIGNEE_NAME, MONTH, YEAR)));
        }
        #endregion

        #region "GET DETENATION-REPORT"

        [HttpGet("GetDetenationList")]
        public ActionResult<Response<List<DETENATION_LIST>>> GetDetenationList(string LOCATION, string CONSIGNEE_NAME, string MONTH, string YEAR)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetDetenationList(LOCATION, CONSIGNEE_NAME, MONTH, YEAR)));
        }
        #endregion

        #region "GET CONSIGNEE"

        [HttpGet("GetConsignee")]
        public ActionResult<Response<List<CONSIGNEE_NAME>>> GetConsignee()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetConsignee()));
        }
        #endregion


        #region "GET AGENCY-REPORT"

        [HttpGet("GetAgencyList")]
        public ActionResult<Response<List<AGENCY_LIST>>> GetAgencyList(string LOCATION, string MONTH, string YEAR)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetAgencyList(LOCATION, MONTH, YEAR)));
        }
        #endregion

        #region "GET SALES-REPORT"

        [HttpGet("GetSalesList")]
        public ActionResult<Response<List<SALES_LIST>>> GetSalesList(string LOCATION, string MONTH, string YEAR)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetSalesList(LOCATION, MONTH, YEAR)));
        }
        #endregion

        #region "GET SERVICE"

        [HttpGet("GetService")]
        public ActionResult<Response<List<SERVICE_NAME>>> GetService()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetService()));
        }
        #endregion

        #region "GET VOLUME-REPORT"

        [HttpGet("GetVolume")]
        public ActionResult<Response<List<VOLUME_LIST>>> GetVolume(string LOCATION, string MONTH, string YEAR, string SERVICE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetVolume(LOCATION, MONTH, YEAR, SERVICE)));
        }
        #endregion

        #region "GET VESSEL-DETAIL"

        [HttpGet("GetVessel")]
        public ActionResult<Response<List<VESSEL_VOYAGE>>> GetVessel(string LOCATION, string VESSEL_NAME, string VOYAGE_NO)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetVessel(LOCATION, VESSEL_NAME, VOYAGE_NO)));
        }
        #endregion

        #region "GET SERVICE-DETAIL"

        [HttpGet("GetServices")]
        public ActionResult<Response<List<VESSEL_VOYAGE>>> GetServices(string LOCATION, string SERVICE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetServices(LOCATION, SERVICE)));
        }
        #endregion

        #region "EQUIPMENT_TYPE"

        [HttpPost("InsertEquipmentType")]
        public ActionResult<Response<CommonResponse>> InsertEquipmentType(EQUIPMENT_TYPE_MASTER request)
        {
            return Ok(_masterService.InsertEquipmentType(request));
        }

        [HttpGet("GetEquipmentTypeList")]
        public ActionResult<Response<List<EQUIPMENT_TYPE_MASTER>>> GetEquipmentTypeList(string IS_ACTIVE, string EQUIPMENT_TYPE, string FROM_DATE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetEquipmentTypeList(IS_ACTIVE, EQUIPMENT_TYPE, FROM_DATE)));
        }

        [HttpGet("GetEquipmentByID")]
        public ActionResult<Response<EQUIPMENT_TYPE_MASTER>> GetEquipmentByID(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetEquipmentByID(ID)));
        }

        [HttpPost("UpdatEquipmentTypeList")]
        public ActionResult<Response<CommonResponse>> UpdatEquipmentTypeList(EQUIPMENT_TYPE_MASTER request)
        {
            return Ok(_masterService.UpdatEquipmentTypeList(request));
        }

        [HttpDelete("DeleteEquipmentTypeList")]
        public ActionResult<Response<CommonResponse>> DeleteEquipmentTypeList(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteEquipmentTypeList(ID)));
        }

        #endregion

        #region"Agreement No"

        [HttpGet("GetAllAgreementNoList")]
        public ActionResult<Response<List<VENDOR_AGREEMENT>>> GetAllAgreementNoList()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetAllAgreementNoList()));
        }

        #endregion

        #region "VENDOR AGREEMENT"
        [HttpPost("InsertVendorAgreement")]
        public ActionResult<Response<CommonResponse>> InsertVendorAgreement(VENDOR_AGREEMENT_LIST request)
        {
            return Ok(_masterService.InsertVendorAgreement(request));
        }


        [HttpPost("UpdateVendorAgreement")]
        public ActionResult<Response<CommonResponse>> UpdateVendorAgreement(VENDOR_AGREEMENT_LIST request)
        {
            return Ok(_masterService.UpdateVendorAgreement(request));
        }

        [HttpGet("GetVendorAgreementById")]
        public ActionResult<Response<VENDOR_AGREEMENT_LIST>> GetVendorAgreementById( int VENDOR_AGREEMENT_ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetVendorAgreementById( VENDOR_AGREEMENT_ID)));
        }

        [HttpDelete("DeleteVendorAgreementById")]
        public ActionResult<Response<CommonResponse>> DeleteVendorAgreementById(int VENDOR_AGREEMENT_ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteVendorAgreementById(VENDOR_AGREEMENT_ID)));
        }
        [HttpGet("GetVendorAgreementList")]
        public ActionResult<Response<List<VENDOR_AGREEMENT_LIST>>> GetVendorAgreementList(string AGREEMENT_NO, string IS_ACTIVE, string START_DATE, string END_DATE)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetVendorAgreementList(AGREEMENT_NO, IS_ACTIVE, START_DATE, END_DATE)));
        }

        [HttpPost("UploadVendorAgreementFiles")]
        public IActionResult UploadVendorAgreementFiles(int VENDORID)
        {
            var formFile = Request.Form.Files;

            // Define base paths
            string uploadPath = Path.Combine(_environment.ContentRootPath, "Uploads");
            string attachmentPath = Path.Combine(uploadPath, "VendorAgreementFiles");

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
            foreach (IFormFile postedFile in formFile)
            {
                string fileName = Path.GetFileName(VENDORID + "_" + postedFile.FileName);
                // Construct full file path
                string fullFilePath = Path.Combine(attachmentPath, fileName);

                using (FileStream stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    // Convert path to use backslashes
                    string relativePath = Path.Combine("Uploads", "VendorAgreementFiles", fileName).Replace('/', '\\');
                    uploadedFiles.Add(relativePath); // Store relative path with backslashes
                }
            }
            // If multiple files are uploaded, handle them as needed. For this example, we'll use the first file.
            string attachmentpath = uploadedFiles.FirstOrDefault(); // Use the path of the first uploaded file

            // Pass only the relative path to the service
            _masterService.UpdateVendorAgreementPath(VENDORID, attachmentpath);
            return Ok();
        }

        [HttpGet("GetAllEquipmentTypeList")]
        public ActionResult<Response<List<EQUIPMENT_TYPE_LIST>>> GetAllEquipmentTypeList()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetAllEquipmentTypeList()));
        }


        #endregion

        #region "VENDOR AGREEMENT REPORT"

        [HttpGet("GetVendorAgreementReport")]
        public ActionResult<Response<List<VENDOR_AGREEMENT_REPORT>>> GetVendorAgreementReport(string VENDOR_ID, string MONTH)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetVendorAgreementReport(VENDOR_ID, MONTH)));
        }

        #endregion

        #region " GET ALL VENDOR NAME LIST"

        [HttpGet("GetAllVendorList")]
        public ActionResult<Response<List<VENDOR_LIST>>> GetAllVendorList()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetAllVendorList()));
        }

        #endregion

        #region"GET ALL LINER LIST"

        [HttpGet("GetAllLinerList")]
        public ActionResult<Response<List<LINER_NAME>>> GetAllLinerList()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetAllLinerList()));
        }
        #endregion

        #region"GET ALL SLOT OPERATOR LIST"

        [HttpGet("GetAllSlotOperatorList")]
        public ActionResult<Response<List<SLOT_OPERATOR_NAME>>> GetAllSlotOperatorList()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetAllSlotOperatorList()));
        }
        #endregion

        #region"GET ALL SLOT PURCHASE LIST"
        [HttpPost("uploadslotpurchase")]
        public ActionResult<Response<string>> InsertSlotPurchase(List<SLOT_PURCHASE_LIST> request)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.InsertSlotPurchase(request)));
        }

        [HttpPost("Updateslotpurchase")]
        public ActionResult<Response<string>> Updateslotpurchase(List<SLOT_PURCHASE_LIST> request)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.Updateslotpurchase(request)));
        }

        [HttpGet("GetAllSlotPurchase")] 
        public ActionResult<Response<List<SLOT_PURCHASE_LIST>>> GetAllSlotPurchase()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetAllSlotPurchase()));
        }

        [HttpGet("GetSlotpurchaseById")]
        public ActionResult<Response<List<SLOT_PURCHASE_LIST>>> GetSlotpurchaseById(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetSlotpurchaseById(ID)));
        }

        [HttpPost("UpdateSlotPurchaseMaster")]
        public ActionResult<Response<CommonResponse>> UpdateSlotPurchase(SLOT_PURCHASE_LIST request)
        {
            return Ok(_masterService.UpdateSlotPurchase(request));
        }

        #endregion

        #region "MNR TARIFF MASTER"
        [HttpGet("GetAllMnrList")]
        public ActionResult<Response<List<MNR_TARIFF_LIST>>> GetAllMnrList()
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetAllMnrList()));
        }

        [HttpGet("GetMNRListById")]
        public ActionResult<Response<MNR_TARIFF_LIST>> GetMNRListById(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.GetMNRListById(ID)));
        }

        [HttpPost("UpdateMNRTariff")]
        public ActionResult<Response<CommonResponse>> UpdateMNRTariffList(MNR_TARIFF_LIST request)
        {
            return Ok(_masterService.UpdateMNRTariffList(request));
        }

        [HttpGet("DeleteMNRListById")]
        public ActionResult<Response<CommonResponse>> DeleteMNRListById(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_masterService.DeleteMNRListById(ID)));
        }
        #endregion

    }
}
