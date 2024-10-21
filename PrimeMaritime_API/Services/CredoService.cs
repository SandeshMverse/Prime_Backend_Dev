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
using static Org.BouncyCastle.Math.EC.ECCurve;


namespace PrimeMaritime_API.Services
{
    public class CredoService : ICredoService
    {
        private readonly IConfiguration _config;
        public CredoService(IConfiguration config)
        {
            _config = config;
        }

        public Response<CREDO> GetCredoDetails(string AGENT_CODE, string VESSEL_NAME, string VOYAGE_NO, string PORT_OF_LOADING, string PORT_OF_DISCHARGE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CREDO> response = new Response<CREDO>();

            var data = DbClientFactory<CredoRepo>.Instance.GetCredoDetails(dbConn, AGENT_CODE, VESSEL_NAME, VOYAGE_NO, PORT_OF_LOADING, PORT_OF_DISCHARGE);

            if ((data != null) && (data.Tables[0].Rows.Count > 0))
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                CREDO credo = new CREDO();

                credo = CredoRepo.GetSingleDataFromDataSet<CREDO>(data.Tables[0]);
                if (data.Tables.Contains("Table"))
                {
                    credo.CUSTOMER_LIST = CredoRepo.GetListFromDataSet<CUSTOMERLIST>(data.Tables[0]);
                }

                if (data.Tables.Contains("Table1"))
                {
                    credo.CONTAINER = CredoRepo.GetListFromDataSet<CONTAINER>(data.Tables[1]);
                }

                response.Data = credo;
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

