using Microsoft.Extensions.Configuration;
using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.IServices;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Repository;
using PrimeMaritime_API.Request;
using PrimeMaritime_API.Response;
using PrimeMaritime_API.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Services
{
    public class CROService : ICROService
    {
        private readonly IConfiguration _config;
        public CROService(IConfiguration config)
        {
            _config = config;
        }

        public Response<List<CRO>> GetCROList(string AGENT_CODE, string FROM_DATE, string TO_DATE, string CRO_NO, string ORG_CODE, string PORT)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<CRO>> response = new Response<List<CRO>>();
            var data = DbClientFactory<CRORepo>.Instance.GetCROList(dbConn, AGENT_CODE,FROM_DATE,TO_DATE,CRO_NO,ORG_CODE,PORT);

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
        public Response<List<CRO>> GetCROListPM(string FROM_DATE, string TO_DATE, string CRO_NO)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<CRO>> response = new Response<List<CRO>>();
            var data = DbClientFactory<CRORepo>.Instance.GetCROListPM(dbConn, FROM_DATE, TO_DATE, CRO_NO);

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

        public Response<string> InsertCRO(CRORequest CRORequest)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            string CRONo = DbClientFactory<CRORepo>.Instance.InsertCRO(dbConn, CRORequest);

            Response<string> response = new Response<string>();
            response.Succeeded = true;
            response.ResponseMessage = "Inserted Successfully.";
            response.ResponseCode = 200;
            response.Data = CRONo;

            return response;
        }

        public Response<CRODetails> GetCRODetails(string CRO_NO, string AGENT_CODE, string ORG_CODE, string PORT)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CRODetails> response = new Response<CRODetails>();

            if ((CRO_NO == "") || (CRO_NO == null))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide CRO No";
                return response;
            }

            var data = DbClientFactory<CRORepo>.Instance.GetCRODetails(dbConn, CRO_NO, AGENT_CODE,ORG_CODE,PORT);

            if ((data != null) && (data.Tables[0].Rows.Count > 0))
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                CRODetails cro = new CRODetails();

                cro = BookingRepo.GetSingleDataFromDataSet<CRODetails>(data.Tables[0]);

                if (data.Tables.Contains("Table1"))
                {
                    cro.ContainerList = SRRRepo.GetListFromDataSet<SRR_CONTAINERS>(data.Tables[1]);
                }

                if (data.Tables.Contains("Table2"))
                {
                    cro.BookingDetails = SRRRepo.GetSingleDataFromDataSet<BOOKING>(data.Tables[2]);
                }

                response.Data = cro;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<List<CRO>> GetAllCRONo()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<CRO>> response = new Response<List<CRO>>();
            var data = DbClientFactory<CRORepo>.Instance.GetAllCRONo(dbConn);

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

        public Response<CRO_DETAILS> GetCRONoDetail(string CRO_NO)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CRO_DETAILS> response = new Response<CRO_DETAILS>();
            var data = DbClientFactory<CRORepo>.Instance.GetCRONoDetail(dbConn,CRO_NO);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                CRO_DETAILS cro = new CRO_DETAILS();

                cro = CRORepo.GetSingleDataFromDataSet<CRO_DETAILS>(data.Tables[0]);

                if (data.Tables.Contains("Table1"))
                {
                    cro.CONTAINER_LIST2 = SRRRepo.GetListFromDataSet<CONTAINERS_DETAILS>(data.Tables[1]);
                }
                response.Data = cro;
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
