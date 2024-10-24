using Microsoft.Extensions.Configuration;
using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.IServices;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Repository;
using PrimeMaritime_API.Response;
using PrimeMaritime_API.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace PrimeMaritime_API.Services
{
    public class MasterService : IMasterService
    {
        private readonly IConfiguration _config;

        public MasterService(IConfiguration config)
        {
            _config = config;
        }

        #region "PARTY MASTER"
        public Response<CommonResponse> DeletePartyMasterDetails(int CUSTOMER_ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.DeletePartyMasterDetails(dbConn, CUSTOMER_ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<List<PARTY_MASTER>> GetPartyMasterList(string Agent_code, string CustName, string CustType, bool Status, string FROM_DATE, string TO_DATE, bool IS_VENDOR)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<PARTY_MASTER>> response = new Response<List<PARTY_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetPartyMasterList(dbConn, Agent_code, CustName, CustType, Status, FROM_DATE, TO_DATE,IS_VENDOR);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<PARTY_MASTER> GetPartyMasterDetails(string Agent_code, int CUSTOMER_ID, int VENDOR_AGREEMENT_ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<PARTY_MASTER> response = new Response<PARTY_MASTER>();
 
            var data = DbClientFactory<MasterRepo>.Instance.GetPartyMasterDetails(dbConn, Agent_code, CUSTOMER_ID, VENDOR_AGREEMENT_ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = MasterRepo.GetSingleDataFromDataSet<PARTY_MASTER>(data.Tables[0]);

                if (data.Tables.Contains("Table1"))
                {
                    response.Data.BRANCH_LIST = MasterRepo.GetListFromDataSet<CUSTOMER_BRANCH>(data.Tables[1]);
                }

                if (data.Tables.Contains("Table2"))
                {
                    response.Data.BANK_LIST = MasterRepo.GetListFromDataSet<CUSTOMER_BANK>(data.Tables[2]);
                }
                if (data.Tables.Contains("Table3"))
                {
                    response.Data.VENDOR_AGREEMENT_LIST = MasterRepo.GetListFromDataSet<VENDOR_AGREEMENT_LIST>(data.Tables[3]);
                }
                if (data.Tables.Contains("Table4"))
                {
                    response.Data.VENDOR_PICKUP_PORT_LIST = MasterRepo.GetListFromDataSet<VENDOR_PICKUP_PORT_LIST>(data.Tables[4]);
                }
                if (data.Tables.Contains("Table5"))
                {
                    response.Data.VENDOR_REDELIVERY_PORT_LIST = MasterRepo.GetListFromDataSet<VENDOR_REDELIVERY_PORT_LIST>(data.Tables[5]);
                }
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<CommonResponse> UpdatePartyMasterDetails(PARTY_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdatePartyMasterDetails(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<CommonResponse> InsertPartyMaster(PARTY_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            var ID = DbClientFactory<MasterRepo>.Instance.InsertPartyMaster(dbConn, request);

            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = ID;
            response.ResponseCode = 200;

            return response;
        }

        #endregion

        #region "CONTAINER MASTER"
        public Response<CommonResponse> InsertContainerMaster(CONTAINER_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<MasterRepo>.Instance.InsertContainerMaster(dbConn, request);


            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Master saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<List<CONTAINER_MASTER>> GetContainerMasterList(string ContainerNo, string ContType, string ContSize, bool Status, string ONHIRE_DATE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<CONTAINER_MASTER>> response = new Response<List<CONTAINER_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetContainerMasterList(dbConn, ContainerNo, ContType, ContSize, Status, ONHIRE_DATE);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<CONTAINER_MASTER> GetContainerMasterDetails(int ID, string CONTAINER_NO)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CONTAINER_MASTER> response = new Response<CONTAINER_MASTER>();
            var data = DbClientFactory<MasterRepo>.Instance.GetContainerMasterDetails(dbConn, ID, CONTAINER_NO);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<CommonResponse> UpdateContainerMasterList(CONTAINER_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateContainerMasterList(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<CommonResponse> DeleteContainerMasterList(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if ((ID == 0) || (ID == 0))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteContainerMaster(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        #endregion

        #region "COMMON MASTER"
        public Response<CommonResponse> InsertMaster(MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<MasterRepo>.Instance.InsertMaster(dbConn, request);


            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Master saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<List<MASTER>> GetMasterList(string key, string FROM_DATE, string TO_DATE, string STATUS)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<MASTER>> response = new Response<List<MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetMasterList(dbConn, key,FROM_DATE,TO_DATE,STATUS);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<MASTER> GetMasterDetails(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<MASTER> response = new Response<MASTER>();
            var data = DbClientFactory<MasterRepo>.Instance.GetMasterDetails(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<CommonResponse> UpdateMaster(MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateMaster(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<CommonResponse> DeleteMaster(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if (ID == 0)
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteMaster(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        #endregion

        #region "VESSEL MASTER
        public Response<CommonResponse> InsertVesselMaster(VESSEL_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<MasterRepo>.Instance.InsertVesselMaster(dbConn, request);


            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Master saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<List<VESSEL_MASTER>> GetVesselMasterList(string VESSEL_NAME, string IMO_NO, string STATUS, string FROM_DATE, string TO_DATE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<VESSEL_MASTER>> response = new Response<List<VESSEL_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetVesselMasterList(dbConn,VESSEL_NAME,IMO_NO,STATUS,FROM_DATE,TO_DATE);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<VESSEL_MASTER> GetVesselMasterDetails(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<VESSEL_MASTER> response = new Response<VESSEL_MASTER>();
            var data = DbClientFactory<MasterRepo>.Instance.GetVesselMasterDetails(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<CommonResponse> UpdateVesselMasterList(VESSEL_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateVesselMasterList(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<CommonResponse> DeleteVesselMasterList(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if ((ID == 0) || (ID == 0))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteVesselMasterList(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        #endregion

        #region "SERVICE MASTER"
        public Response<CommonResponse> InsertServiceMaster(SERVICE_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<MasterRepo>.Instance.InsertServiceMaster(dbConn, request);


            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Master saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<List<SERVICE_MASTER>> GetServiceMasterList()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<SERVICE_MASTER>> response = new Response<List<SERVICE_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetServiceMasterList(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<SERVICE_MASTER> GetServiceMasterDetails(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<SERVICE_MASTER> response = new Response<SERVICE_MASTER>();
            var data = DbClientFactory<MasterRepo>.Instance.GetServiceMasterDetails(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<CommonResponse> UpdateServiceMasterList(SERVICE_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateServiceMasterList(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }


        public Response<CommonResponse> DeleteServiceMasterList(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if ((ID == 0) || (ID == 0))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteServiceMasterList(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }



        #endregion

        #region "CONTAINER TYPE MASTER"
        public Response<CommonResponse> InsertContainerTypeMaster(CONTAINER_TYPE request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<MasterRepo>.Instance.InsertContainerTypeMaster(dbConn, request);


            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Master saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<List<CONTAINER_TYPE>> GetContainerTypeMasterList(string ContTypeCode, string ContType, string ContSize, bool Status, string FROM_DATE, string TO_DATE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<CONTAINER_TYPE>> response = new Response<List<CONTAINER_TYPE>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetContainerTypeMasterList( dbConn,  ContTypeCode,  ContType,  ContSize,  Status,  FROM_DATE,  TO_DATE);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<CONTAINER_TYPE> GetContainerTypeMasterDetails(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CONTAINER_TYPE> response = new Response<CONTAINER_TYPE>();
            var data = DbClientFactory<MasterRepo>.Instance.GetContainerTypeMasterDetails(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<CommonResponse> UpdateConatinerTypeMaster(CONTAINER_TYPE request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateConatinerTypeMaster(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<CommonResponse> DeleteContainerTypeMaster(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if ((ID == 0) || (ID == 0))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteContainerTypeMaster(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        #endregion

        #region "ICD MASTER"

        public Response<List<ICD_MASTER>> GetICDMasterList()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<ICD_MASTER>> response = new Response<List<ICD_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetICDMasterList(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }


        #endregion

        #region "DEPO MASTER"

        public Response<List<DEPO_MASTER>> GetDEPOMasterList()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<DEPO_MASTER>> response = new Response<List<DEPO_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetDEPOMasterList(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }



        #endregion

        #region "TERMINAL MASTER"


        public Response<List<TERMINAL_MASTER>> GetTerminalMasterList()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<TERMINAL_MASTER>> response = new Response<List<TERMINAL_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetTerminalMasterList(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        #endregion

        #region CLEARING_PARTY"

        public Response<List<CLEARING_PARTY>> GetClearingPartyList()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<CLEARING_PARTY>> response = new Response<List<CLEARING_PARTY>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetClearingPartyList(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<string> InsertCP(CLEARING_PARTY request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<MasterRepo>.Instance.InsertCP(dbConn, request);

            Response<string> response = new Response<string>();
            response.Succeeded = true;
            response.ResponseMessage = "Inserted Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        #endregion

        #region "LINER"
        public Response<CommonResponse> InsertLiner(LINER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<MasterRepo>.Instance.InsertLiner(dbConn, request);


            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Master saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<List<LINER>> GetLinerList(string Name,string Code,string Description,bool Status,string FROM_DATE,string TO_DATE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<LINER>> response = new Response<List<LINER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetLinerList(dbConn , Name,  Code,  Description,  Status,  FROM_DATE,  TO_DATE);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<LINER> GetLinerDetails(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<LINER> response = new Response<LINER>();
            var data = DbClientFactory<MasterRepo>.Instance.GetLinerDetails(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<CommonResponse> UpdateLinerList(LINER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateLinerList(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<CommonResponse> DeleteLinerList(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if ((ID == 0) || (ID == 0))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteLinerList(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        #endregion

        #region "LINERSERVICE"
        public Response<CommonResponse> InsertService(SERVICE request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<MasterRepo>.Instance.InsertService(dbConn, request);


            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Master saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<List<SERVICE>> GetServiceList(bool Status,string FROM_DATE,string TO_DATE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<SERVICE>> response = new Response<List<SERVICE>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetServiceList(dbConn, Status, FROM_DATE, TO_DATE);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<SERVICE> GetServiceDetails(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<SERVICE> response = new Response<SERVICE>();
            var data = DbClientFactory<MasterRepo>.Instance.GetServiceDetails(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<CommonResponse> UpdateService(SERVICE request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateService(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<CommonResponse> DeleteService(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if ((ID == 0) || (ID == 0))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteService(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }



        #endregion

        #region "VESSEL SCHEDULE"
        public Response<CommonResponse> InsertSchedule(SCHEDULE request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<MasterRepo>.Instance.InsertSchedule(dbConn, request);


            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Master saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<List<SCHEDULE>> GetScheduleList(string VESSEL_NAME, string PORT_CODE, bool STATUS, string ETA, string ETD)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<SCHEDULE>> response = new Response<List<SCHEDULE>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetScheduleList(dbConn , VESSEL_NAME,  PORT_CODE,  STATUS,    ETA,  ETD);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<SCHEDULE> GetScheduleDetails(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<SCHEDULE> response = new Response<SCHEDULE>();
            var data = DbClientFactory<MasterRepo>.Instance.GetScheduleDetails(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<CommonResponse> UpdateSchedule(SCHEDULE request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateSchedule(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<CommonResponse> DeleteSchedule(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if ((ID == 0) || (ID == 0))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteSchedule(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        #endregion

        #region "VESSEL VOYAGE"
        public Response<List<VOYAGE>> GetVoyageList(bool STATUS, string FROM_DATE, string TO_DATE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<VOYAGE>> response = new Response<List<VOYAGE>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetVoyageList(dbConn, STATUS, FROM_DATE, TO_DATE);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<VOYAGE> GetVoyageDetails(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<VOYAGE> response = new Response<VOYAGE>();
            var data = DbClientFactory<MasterRepo>.Instance.GetVoyageDetails(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<CommonResponse> UpdateVoyage(VOYAGE request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateVoyage(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<CommonResponse> DeleteVoyage(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if ((ID == 0) || (ID == 0))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteVoyage(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        #endregion

        #region "LOCATION MASTER"
        public Response<CommonResponse> InsertLocationMaster(LOCATION_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<MasterRepo>.Instance.InsertLocationMaster(dbConn, request);

            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Master saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<List<LOCATION_MASTER>> GetLocationMasterList(string LOC_NAME, string LOC_TYPE, bool STATUS, string FROM_DATE, string TO_DATE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<LOCATION_MASTER>> response = new Response<List<LOCATION_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetLocationMasterList(dbConn, LOC_NAME,LOC_TYPE,STATUS,FROM_DATE,TO_DATE);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<LOCATION_MASTER> GetLocationMasterDetails(string LOC_CODE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<LOCATION_MASTER> response = new Response<LOCATION_MASTER>();
            var data = DbClientFactory<MasterRepo>.Instance.GetLocationMasterDetails(dbConn, LOC_CODE);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<CommonResponse> UpdateLocationMasterList(LOCATION_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateLocationMasterList(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<CommonResponse> DeleteLocationMasterList(string LOC_CODE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if (String.IsNullOrEmpty(LOC_CODE))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteLocationMaster(dbConn, LOC_CODE);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        #endregion

        #region "FREIGHT MASTER"
        public Response<CommonResponse> InsertFreightMaster(FREIGHT_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<MasterRepo>.Instance.InsertFreightMaster(dbConn, request);

            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Master saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<List<FREIGHT_MASTER>> GetFreightMasterList()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<FREIGHT_MASTER>> response = new Response<List<FREIGHT_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetFreightMasterList(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<FREIGHT_MASTER> GetFreightMasterDetails(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<FREIGHT_MASTER> response = new Response<FREIGHT_MASTER>();
            var data = DbClientFactory<MasterRepo>.Instance.GetFreightMasterDetails(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<CommonResponse> UpdateFreightMasterList(FREIGHT_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateFreightMasterList(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<CommonResponse> DeleteFreightMasterList(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if (ID == 0)
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteFreightMaster(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        #endregion

        #region "CHARGE MASTER"
        public Response<List<CHARGE_MASTER>> GetChargeMasterList()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<CHARGE_MASTER>> response = new Response<List<CHARGE_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetChargeMasterList(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<CHARGE_MASTER> GetChargeMasterDetails(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CHARGE_MASTER> response = new Response<CHARGE_MASTER>();
            var data = DbClientFactory<MasterRepo>.Instance.GetChargeMasterDetails(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<CommonResponse> UpdateChargeMasterList(CHARGE_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateChargeMasterList(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<CommonResponse> DeleteChargeMasterList(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if (ID == 0)
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteChargeMaster(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        #endregion

        #region "STEVEDORING MASTER"
        public Response<List<STEV_MASTER>> GetStevedoringMasterList()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<STEV_MASTER>> response = new Response<List<STEV_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetStevedoringMasterList(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<STEV_MASTER> GetStevedoringMasterDetails(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<STEV_MASTER> response = new Response<STEV_MASTER>();
            var data = DbClientFactory<MasterRepo>.Instance.GetStevedoringMasterDetails(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<CommonResponse> UpdateStevedoringMasterList(STEV_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateStevedoringMasterList(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<CommonResponse> DeleteStevedoringMasterList(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if (ID == 0)
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteStevedoringMaster(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        #endregion

        #region "DETENTION MASTER"
        public Response<List<DETENTION_MASTER>> GetDetentionMasterList()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<DETENTION_MASTER>> response = new Response<List<DETENTION_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetDetentionMasterList(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<DETENTION_MASTER> GetDetentionMasterDetails(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<DETENTION_MASTER> response = new Response<DETENTION_MASTER>();
            var data = DbClientFactory<MasterRepo>.Instance.GetDetentionMasterDetails(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<CommonResponse> UpdateDetentionMasterList(DETENTION_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateDetentionMasterList(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<CommonResponse> DeleteDetentionMasterList(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if (ID == 0)
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteDetentionMaster(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        #endregion

        #region "MANDATORY MASTER"
        public Response<List<MANDATORY_MASTER>> GetMandatoryMasterList()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<MANDATORY_MASTER>> response = new Response<List<MANDATORY_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetMandatoryMasterList(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<MANDATORY_MASTER> GetMandatoryMasterDetails(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<MANDATORY_MASTER> response = new Response<MANDATORY_MASTER>();
            var data = DbClientFactory<MasterRepo>.Instance.GetMandatoryMasterDetails(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<CommonResponse> UpdateMandatoryMasterList(MANDATORY_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateMandatoryMasterList(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<CommonResponse> DeleteMandatoryMasterList(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if (ID == 0)
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteMandatoryMaster(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        #endregion

        #region "UPLOAD TARIFF"
        public Response<string> UploadFreightTariff(List<FREIGHT_MASTER> request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<string> response = new Response<string>();
            DbClientFactory<MasterRepo>.Instance.UploadFreightTariff(dbConn, request);

            response.Succeeded = true;
            response.ResponseCode = 200;
            response.ResponseMessage = "Success";
            response.Data = "Uploaded Successfully !";

            return response;
        }
        public Response<string> UploadChargeTariff(List<CHARGE_MASTER> request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<string> response = new Response<string>();
            DbClientFactory<MasterRepo>.Instance.UploadChargeTariff(dbConn, request);

            response.Succeeded = true;
            response.ResponseCode = 200;
            response.ResponseMessage = "Success";
            response.Data = "Uploaded Successfully !";

            return response;
        }

        public Response<string> UploadStevTariff(List<STEV_MASTER> request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<string> response = new Response<string>();
            DbClientFactory<MasterRepo>.Instance.UploadStevTariff(dbConn, request);

            response.Succeeded = true;
            response.ResponseCode = 200;
            response.ResponseMessage = "Success";
            response.Data = "Uploaded Successfully !";

            return response;
        }
        public Response<string> UploadDetentionTariff(List<DETENTION_MASTER> request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<string> response = new Response<string>();
            DbClientFactory<MasterRepo>.Instance.UploadDetentionTariff(dbConn, request);

            response.Succeeded = true;
            response.ResponseCode = 200;
            response.ResponseMessage = "Success";
            response.Data = "Uploaded Successfully !";

            return response;
        }
        public Response<string> UploadMandatoryTariff(List<MANDATORY_MASTER> request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<string> response = new Response<string>();
            DbClientFactory<MasterRepo>.Instance.UploadMandatoryTariff(dbConn, request);

            response.Succeeded = true;
            response.ResponseCode = 200;
            response.ResponseMessage = "Success";
            response.Data = "Uploaded Successfully !";

            return response;
        }

        public Response<string> UploadSlotRateTariff(List<SLOT_RATE_MASTER> request) //SIDDHESH
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<string> response = new Response<string>();
            DbClientFactory<MasterRepo>.Instance.UploadSlotRateTariff(dbConn, request);

            response.Succeeded = true;
            response.ResponseCode = 200;
            response.ResponseMessage = "Success";
            response.Data = "Uploaded Successfully !";

            return response;
        }
        #endregion

        #region "ORGANISATION MASTER"
        public Response<CommonResponse> InsertOrgMaster(ORG_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<MasterRepo>.Instance.InsertOrgMaster(dbConn, request);

            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Master saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<CommonResponse> ValidateOrgCode(string ORG_CODE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            var data = DbClientFactory<MasterRepo>.Instance.ValidateOrgCode(dbConn, ORG_CODE);

            if (String.IsNullOrEmpty(data))
            {
                Response<CommonResponse> response = new Response<CommonResponse>();
                response.Succeeded = true;
                response.ResponseMessage = "ORG CODE is valid !";
                response.ResponseCode = 200;

                return response;
            }
            else
            {
                Response<CommonResponse> response = new Response<CommonResponse>();
                response.Succeeded = true;
                response.ResponseMessage = "Already Exists";
                response.ResponseCode = 500;

                return response;
            }            
        }
        public Response<List<ORG_MASTER>> GetOrgMasterList()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<ORG_MASTER>> response = new Response<List<ORG_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetOrgMasterList(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<ORG_MASTER> GetOrgMasterDetails(string ORG_CODE) 
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<ORG_MASTER> response = new Response<ORG_MASTER>();
            var data = DbClientFactory<MasterRepo>.Instance.GetOrgMasterDetails(dbConn, ORG_CODE);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = MasterRepo.GetSingleDataFromDataSet<ORG_MASTER>(data.Tables[0]);

                if (data.Tables.Contains("Table1"))
                {
                    response.Data.BRANCH_LIST = MasterRepo.GetListFromDataSet<CUSTOMER_BRANCH>(data.Tables[1]);
                }

                if (data.Tables.Contains("Table2"))
                {
                    response.Data.BANK_LIST = MasterRepo.GetListFromDataSet<CUSTOMER_BANK>(data.Tables[2]);
                }
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<CommonResponse> UpdateOrgMasterList(ORG_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateOrgMasterList(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<CommonResponse> DeleteOrgMasterList(string ORG_CODE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if (String.IsNullOrEmpty(ORG_CODE))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteOrgMaster(dbConn, ORG_CODE);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        #endregion

        #region "SLOT MASTER"

        public Response<CommonResponse> InsertSlotMaster(SLOT_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<MasterRepo>.Instance.InsertSlotMaster(dbConn, request);

            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Master saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<List<SLOT_MASTER>> GetSlotMasterList(string SERVICE, string PORT)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<SLOT_MASTER>> response = new Response<List<SLOT_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetSlotMasterList(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<SLOT_MASTER> GetSlotMasterDetails(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<SLOT_MASTER> response = new Response<SLOT_MASTER>();
            var data = DbClientFactory<MasterRepo>.Instance.GetSlotMasterDetails(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<CommonResponse> UpdateSlotMasterList(SLOT_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateSlotMasterList(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<CommonResponse> DeleteSlotMasterList(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if (ID == 0)
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteSlotMaster(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        #endregion

        #region "Charges MASTER"
        public Response<CommonResponse> InsertChargeMaster(CHARGES_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<MasterRepo>.Instance.InsertChargeMaster(dbConn, request);

            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Charge Master saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<List<CHARGES_MASTER>> GetChargeMaster()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<CHARGES_MASTER>> response = new Response<List<CHARGES_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetChargeMaster(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }


        public Response<List<CHARGES_MASTER>> GetChargeMastersDetails(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<CHARGES_MASTER>> response = new Response<List<CHARGES_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetChargeMastersDetails(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }


        public Response<CommonResponse> UpdateChargesMaster(CHARGES_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateChargesMaster(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Charge Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<CommonResponse> DeleteChargesMaster(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if (ID == 0)
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteChargesMaster(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Charge Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        #endregion

        #region "HSN-CODE"
        public Response<CommonResponse> InsertHsnCode(HSN_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<MasterRepo>.Instance.InsertHsnCode(dbConn, request);

            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "HSN Code saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<List<HSN_MASTER>> GetHsnMaster()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<HSN_MASTER>> response = new Response<List<HSN_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetHsnMaster(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<CommonResponse> DeleteHsnMaster(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if (ID == 0)
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteHsnMaster(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Hsn Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        #endregion

        #region "COUNTRY MASTER"
        public Response<CommonResponse> InsertCountryMaster(COUNTRY_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<MasterRepo>.Instance.InsertCountryMaster(dbConn, request);

            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Master saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<List<COUNTRY_MASTER>> GetCountryMasterList()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<COUNTRY_MASTER>> response = new Response<List<COUNTRY_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetCountryMasterList(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<COUNTRY_MASTER> GetCountryMasterDetails(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<COUNTRY_MASTER> response = new Response<COUNTRY_MASTER>();
            var data = DbClientFactory<MasterRepo>.Instance.GetCountryMasterDetails(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<CommonResponse> UpdateCountryMasterList(COUNTRY_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateCountryMasterList(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<CommonResponse> DeleteCountryMasterList(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if ((ID == 0) || (ID == 0))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteCountryMaster(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        #endregion

        #region "STATE MASTER"
        public Response<CommonResponse> InsertStateMaster(STATE_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<MasterRepo>.Instance.InsertStateMaster(dbConn, request);

            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Master saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<List<STATE_MASTER>> GetStateMasterList()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<STATE_MASTER>> response = new Response<List<STATE_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetStateMasterList(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<STATE_MASTER> GetStateMasterDetails(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<STATE_MASTER> response = new Response<STATE_MASTER>();
            var data = DbClientFactory<MasterRepo>.Instance.GetStateMasterDetails(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<CommonResponse> UpdateStateMasterList(STATE_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateStateMasterList(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<CommonResponse> DeleteStateMasterList(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if ((ID == 0) || (ID == 0))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteStateMaster(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        #endregion


        #region "IAL"
        public Response<IAL> GetIALDetails(string AGENT_CODE, string VESSEL_NAME, string VOYAGE_NO, string PORT_OF_LOADING)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<IAL> response = new Response<IAL>();
            var data = DbClientFactory<MasterRepo>.Instance.GetIALDetails(dbConn, AGENT_CODE, VESSEL_NAME, VOYAGE_NO, PORT_OF_LOADING);

            if ((data != null) && (data.Tables[0].Rows.Count > 0))
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                IAL ial = new IAL();

                ial = MasterRepo.GetSingleDataFromDataSet<IAL>(data.Tables[0]);
                if (data.Tables.Contains("Table"))
                {
                    ial.CUSTOMER_LIST = MasterRepo.GetListFromDataSet<IALLIST>(data.Tables[0]);
                }
                response.Data = ial;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region "EAL"
        public Response<EAL> GetEALDetails(string AGENT_CODE, string VESSEL_NAME, string VOYAGE_NO, string PORT_OF_DISCHARGE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<EAL> response = new Response<EAL>();
            var data = DbClientFactory<MasterRepo>.Instance.GetEALDetails(dbConn, AGENT_CODE, VESSEL_NAME, VOYAGE_NO, PORT_OF_DISCHARGE);

            if ((data != null) && (data.Tables[0].Rows.Count > 0))
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                EAL eal = new EAL();

                eal = MasterRepo.GetSingleDataFromDataSet<EAL>(data.Tables[0]);
                if (data.Tables.Contains("Table"))
                {
                    eal.CUSTOMER_LISTS = MasterRepo.GetListFromDataSet<EALLIST>(data.Tables[0]);
                }
                response.Data = eal;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region "SLOT RATE MASTER"
        public Response<List<SLOT_RATE_MASTER>> GetSlotRateList()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<SLOT_RATE_MASTER>> response = new Response<List<SLOT_RATE_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetSlotRateList(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<SLOT_RATE_MASTER> GetSlotRateMasterDetails(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<SLOT_RATE_MASTER> response = new Response<SLOT_RATE_MASTER>();
            var data = DbClientFactory<MasterRepo>.Instance.GetSlotRateMasterDetails(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<CommonResponse> UpdateSlotRateMaster(SLOT_RATE_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdateSlotRateMaster(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Master updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<CommonResponse> DeleteSlotRateMasterList(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if (ID == 0)
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteSlotRateMasterList(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Master deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<List<SLOT_RATE_HISTORY_MASTER>> HistorySlotRateMasterList(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<SLOT_RATE_HISTORY_MASTER>> response = new Response<List<SLOT_RATE_HISTORY_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.HistorySlotRateMasterList(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region"FLEET COMPOSITION REPORT"
        public Response<List<FLEET_COMPOSITION_REPORT>> GetFleetReport(string LOCATION)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<FLEET_COMPOSITION_REPORT>> response = new Response<List<FLEET_COMPOSITION_REPORT>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetFleetReport(dbConn, LOCATION);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region"STOCK POSITION REPORT"
        public Response<List<STOCK_POSITION_REPORT>> GetStockReport(string LOCATION)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<STOCK_POSITION_REPORT>> response = new Response<List<STOCK_POSITION_REPORT>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetStockReport(dbConn, LOCATION);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region "CONTAINER TURN TIME REPORT"
        public Response<List<CONTAINER_TURN_TIME_REPORT>> GetContainerTurnReport(string LOCATION)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<CONTAINER_TURN_TIME_REPORT>> response = new Response<List<CONTAINER_TURN_TIME_REPORT>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetContainerTurnReport(dbConn, LOCATION);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region "CONTAINER REPAIRS"
        public Response<List<CONTAINER_REPAIRS>> GetContainerRepairs(string LOCATION)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<CONTAINER_REPAIRS>> response = new Response<List<CONTAINER_REPAIRS>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetContainerRepairs(dbConn, LOCATION);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region "SRR LIST"
        public Response<List<SRR_LIST>> GetSRRLIST(string LOCATION, string AGENT_NAME, string MONTH, string YEAR)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<SRR_LIST>> response = new Response<List<SRR_LIST>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetSRRLIST(dbConn, LOCATION, AGENT_NAME, MONTH , YEAR);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region "DOCS LIST"
        public Response<List<DOCS_LIST>> GetDOCSLIST(string LOCATION,  string MONTH, string YEAR)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<DOCS_LIST>> response = new Response<List<DOCS_LIST>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetDOCSLIST(dbConn, LOCATION, MONTH, YEAR);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region " AGENT NAME"
        public Response<List<AGENT_NAME>> GetAgent()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<AGENT_NAME>> response = new Response<List<AGENT_NAME>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetAgent(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region "SHIPPER LIST"
        public Response<List<SHIPPER_LIST>> GetShipperLIST(string LOCATION, string CUSTOMER_NAME, string MONTH, string YEAR)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<SHIPPER_LIST>> response = new Response<List<SHIPPER_LIST>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetShipperLIST(dbConn, LOCATION, CUSTOMER_NAME, MONTH, YEAR);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region "CONSIGNEE LIST"
        public Response<List<CONSIGNEE_LIST>> GetConsigneeLIST(string LOCATION, string CONSIGNEE_NAME, string MONTH, string YEAR)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<CONSIGNEE_LIST>> response = new Response<List<CONSIGNEE_LIST>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetConsigneeLIST(dbConn, LOCATION, CONSIGNEE_NAME, MONTH, YEAR);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region "DETENATION LIST"
        public Response<List<DETENATION_LIST>> GetDetenationList(string LOCATION, string CONSIGNEE_NAME, string MONTH, string YEAR)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<DETENATION_LIST>> response = new Response<List<DETENATION_LIST>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetDetenationList(dbConn, LOCATION, CONSIGNEE_NAME, MONTH, YEAR);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region " CONSIGNEE NAME"
        public Response<List<CONSIGNEE_NAME>> GetConsignee()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<CONSIGNEE_NAME>> response = new Response<List<CONSIGNEE_NAME>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetConsignee(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region "AGENCY LIST"
        public Response<List<AGENCY_LIST>> GetAgencyList(string LOCATION,  string MONTH, string YEAR)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<AGENCY_LIST>> response = new Response<List<AGENCY_LIST>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetAgencyList(dbConn, LOCATION, MONTH, YEAR);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region "SALES LIST"
        public Response<List<SALES_LIST>> GetSalesList(string LOCATION, string MONTH, string YEAR)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<SALES_LIST>> response = new Response<List<SALES_LIST>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetSalesList(dbConn, LOCATION, MONTH, YEAR);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region " SERVICE NAME"
        public Response<List<SERVICE_NAME>> GetService()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<SERVICE_NAME>> response = new Response<List<SERVICE_NAME>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetService(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region "VOLUME LIST"
        public Response<List<VOLUME_LIST>> GetVolume(string LOCATION, string MONTH, string YEAR, string SERVICE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<VOLUME_LIST>> response = new Response<List<VOLUME_LIST>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetVolume(dbConn, LOCATION, MONTH, YEAR, SERVICE);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region "VESSEL DETAILS"
        public Response<List<VESSEL_VOYAGE>> GetVessel(string LOCATION, string VESSEL_NAME, string VOYAGE_NO)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<VESSEL_VOYAGE>> response = new Response<List<VESSEL_VOYAGE>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetVessel(dbConn, LOCATION, VESSEL_NAME, VOYAGE_NO);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region "SERVICES DETAILS"
        public Response<List<VESSEL_VOYAGE>> GetServices(string LOCATION, string SERVICE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<VESSEL_VOYAGE>> response = new Response<List<VESSEL_VOYAGE>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetServices(dbConn, LOCATION, SERVICE);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        #endregion

        #region "EQUIPMENT_TYPE"
        public Response<CommonResponse> InsertEquipmentType(EQUIPMENT_TYPE_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<MasterRepo>.Instance.InsertEquipmentType(dbConn, request);

            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Equipment Type saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<List<EQUIPMENT_TYPE_MASTER>> GetEquipmentTypeList()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<EQUIPMENT_TYPE_MASTER>> response = new Response<List<EQUIPMENT_TYPE_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.GetEquipmentTypeList(dbConn);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<EQUIPMENT_TYPE_MASTER> GetEquipmentByID(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<EQUIPMENT_TYPE_MASTER> response = new Response<EQUIPMENT_TYPE_MASTER>();
            var data = DbClientFactory<MasterRepo>.Instance.GetEquipmentByID(dbConn, ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<CommonResponse> UpdatEquipmentTypeList(EQUIPMENT_TYPE_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();
            DbClientFactory<MasterRepo>.Instance.UpdatEquipmentTypeList(dbConn, request);

            response.Succeeded = true;
            response.ResponseMessage = "Equipment Type updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<CommonResponse> DeleteEquipmentTypeList(int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if ((ID == 0) || (ID == 0))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide ID ";
                return response;
            }

            DbClientFactory<MasterRepo>.Instance.DeleteEquipmentTypeList(dbConn, ID);

            response.Succeeded = true;
            response.ResponseMessage = "Equipment deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }

         public Response<List<EQUIPMENT_TYPE_MASTER>> SearchEquipment(Boolean IS_ACTIVE, string EQUIPMENT_TYPE, string FROM_DATE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<EQUIPMENT_TYPE_MASTER>> response = new Response<List<EQUIPMENT_TYPE_MASTER>>();
            var data = DbClientFactory<MasterRepo>.Instance.SearchEquipment(dbConn, IS_ACTIVE, EQUIPMENT_TYPE, FROM_DATE);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
           

        #endregion
    }
}
