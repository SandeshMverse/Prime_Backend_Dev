using Microsoft.Extensions.Configuration;
using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.IServices;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Repository;
using PrimeMaritime_API.Response;
using PrimeMaritime_API.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityModel.OidcConstants;

namespace PrimeMaritime_API.Services
{
    public class DepoService : IDepoService
    {
        private readonly IConfiguration _config;
        public DepoService(IConfiguration config)
        {
            _config = config;
        }

        public Response<CommonResponse> InsertContainer(DEPO_CONTAINER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<DEPORepo>.Instance.InsertContainer(dbConn, request);

            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Container is alloted Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<CommonResponse> InsertMRRequest(List<MR_LIST> request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<DEPORepo>.Instance.InsertMRRequest(dbConn, request);

            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "M&R Request is inserted Successfully.";
            response.ResponseCode = 200;

            return response;
        }


        public Response<List<MNR_LIST>> GetMNRList(string OPERATION, string DEPO_CODE, string MR_NO, string STATUS, string FROMDATE, string TODATE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<MNR_LIST>> response = new Response<List<MNR_LIST>>();
            var data = DbClientFactory<DEPORepo>.Instance.GetMNRList(dbConn, OPERATION, DEPO_CODE, MR_NO, STATUS, FROMDATE, TODATE);

            if (data.Count > 0)
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

        public Response<string> ApproveRate(List<MR_LIST> request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<DEPORepo>.Instance.ApproveRate(dbConn, request);

            Response<string> response = new Response<string>();
            response.Succeeded = true;
            response.ResponseMessage = "Rate Approved Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<string> InsertNewMRRequest(List<MR_LIST> request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<DEPORepo>.Instance.InsertNewMRRequest(dbConn, request);

            Response<string> response = new Response<string>();
            response.Succeeded = true;
            response.ResponseMessage = "M&R Request is inserted Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<string> DeleteMRRequest(string MR_NO, string LOCATION)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<DEPORepo>.Instance.DeleteMRequest(dbConn, MR_NO, LOCATION);

            Response<string> response = new Response<string>();
            response.Succeeded = true;
            response.ResponseMessage = "M&R Request is deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<List<MR_LIST>> GetMNRDetails(string OPERATION, string MR_NO)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<MR_LIST>> response = new Response<List<MR_LIST>>();
            var data = DbClientFactory<DEPORepo>.Instance.GetMRREQDetails(dbConn, OPERATION, MR_NO);

            if (data.Count > 0)
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

        public Response<List<MR_LIST>> getMRDetailsByID(string OPERATION, string MR_NO, int ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<MR_LIST>> response = new Response<List<MR_LIST>>();
            var data = DbClientFactory<DEPORepo>.Instance.getMRDetailsByID(dbConn, OPERATION, MR_NO, ID);

            if (data.Count > 0)
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
        public Response<MNR_TARIFF> GetMNRTariff(string COMPONENT, string DAMAGE_LOCATION, string REPAIR, string LENGTH, string WIDTH, string HEIGHT, string QUANTITY, string DEPO_CODE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");
            Response<MNR_TARIFF> response = new Response<MNR_TARIFF>();

            var data = DbClientFactory<DEPORepo>.Instance.GetMNRTariff(dbConn, COMPONENT, DAMAGE_LOCATION, REPAIR,LENGTH,WIDTH,HEIGHT,QUANTITY,DEPO_CODE);

            if(data != null)
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


        public void InsertMNRFiles(List<MR_LIST> newMNRList, Dictionary<string, List<string>> attachmentPaths)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            // Pass the correct dictionary type to the repository
            DbClientFactory<DEPORepo>.Instance.InsertMNRFiles(dbConn, newMNRList, attachmentPaths);
        }

        public Response<string> DeleteMRImage(int ID, int MR_ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<DEPORepo>.Instance.DeleteMRImage(dbConn, ID, MR_ID);

            Response<string> response = new Response<string>();
            response.Succeeded = true;
            response.ResponseMessage = "Image deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }


        public void updateMRRequest(List<MR_LIST> updateMNRList, Dictionary<string, List<string>> attachmentPaths)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            // Pass the list directly to the repository
            DbClientFactory<DEPORepo>.Instance.updateMRRequest(dbConn, updateMNRList, attachmentPaths);

        }

        public void InsertPrinMNRFiles(List<MR_LIST> newMNRList, List<string> attachmentPaths)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            // Pass the list directly to the repository
            DbClientFactory<DEPORepo>.Instance.InsertPrinMNRFiles(dbConn, newMNRList, attachmentPaths);

        }


        public Response<List<COMPONENT_DROPDOWN>> GetComponentList(string DAMAGE_LOCATION, string DEPO_CODE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<COMPONENT_DROPDOWN>> response = new Response<List<COMPONENT_DROPDOWN>>();
            var data = DbClientFactory<DEPORepo>.Instance.GetComponentList(dbConn, DAMAGE_LOCATION, DEPO_CODE);

            if (data.Count > 0)
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

        public Response<List<REPAIR_DROPDOWN>> GetRepairDropdownData(string DAMAGE_LOCATION, string COMPONENT, string DEPO_CODE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<REPAIR_DROPDOWN>> response = new Response<List<REPAIR_DROPDOWN>>();
            var data = DbClientFactory<DEPORepo>.Instance.GetRepairDropdownData(dbConn, DAMAGE_LOCATION, COMPONENT, DEPO_CODE);

            if (data.Count > 0)
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
    }
}
