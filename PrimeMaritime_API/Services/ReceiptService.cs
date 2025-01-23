using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Ocsp;
using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.IServices;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Repository;
using PrimeMaritime_API.Response;
using PrimeMaritime_API.Utility;
using System.Collections.Generic;

namespace PrimeMaritime_API.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IConfiguration _config;
        public ReceiptService(IConfiguration config)
        {
            _config = config;
        }

        public Response<List<RECEIPT_INVOICE>> GetReceiptList(string FROM_DATE, string TO_DATE, string PORT, string ORG_CODE, string AGENT_CODE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<RECEIPT_INVOICE>> response = new Response<List<RECEIPT_INVOICE>>();
            var data = DbClientFactory<ReceiptRepo>.Instance.GetReceiptList(dbConn, FROM_DATE, TO_DATE, PORT, ORG_CODE, AGENT_CODE);

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
        public Response<CommonResponse> InsertReceipt(RECEIPT request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<ReceiptRepo>.Instance.InsertReceipt(dbConn, request);

            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Invoice saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }
    }
}
