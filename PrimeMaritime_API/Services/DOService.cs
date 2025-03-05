using Microsoft.Extensions.Configuration;
using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.IServices;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Repository;
using PrimeMaritime_API.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Services
{
    public class DOService:IDOService
    {
        private readonly IConfiguration _config;
        public DOService(IConfiguration config)
        {
            _config = config;
        }
        
        public Response<string> InsertDO(DO doRequest)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<DORepo>.Instance.InsertDO(dbConn, doRequest);

            Response<string> response = new Response<string>();
            response.Succeeded = true;
            response.ResponseMessage = "Inserted Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<string> UpdateDO(DO doRequest)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<DORepo>.Instance.UpdateDO(dbConn, doRequest);

            Response<string> response = new Response<string>();
            response.Succeeded = true;
            response.ResponseMessage = "Updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<string> EditLetterValidity(DO doRequest)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<DORepo>.Instance.EditLetterValidity(dbConn, doRequest);

            Response<string> response = new Response<string>();
            response.Succeeded = true;
            response.ResponseMessage = "Updated Status for Edit Empty Letter Validity Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<List<DO>> GetDOList(string DO_NO, string FROM_DATE, string TO_DATE, string AGENT_CODE, string ORG_CODE, string PORT)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<DO>> response = new Response<List<DO>>();
            var data = DbClientFactory<DORepo>.Instance.GetDOList(dbConn, DO_NO, FROM_DATE, TO_DATE,AGENT_CODE,ORG_CODE,PORT);

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
        public Response<List<DO>> GetDOListPM(string DO_NO, string FROM_DATE, string TO_DATE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<DO>> response = new Response<List<DO>>();
            var data = DbClientFactory<DORepo>.Instance.GetDOListPM(dbConn, DO_NO, FROM_DATE, TO_DATE);

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

        public Response<DO> GetDODetails(string DO_NO, string AGENT_CODE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<DO> response = new Response<DO>();

            if ((DO_NO == "") || (DO_NO == null))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide BL No";
                return response;
            }

            
            var data = DbClientFactory<DORepo>.Instance.GetDODetails(dbConn, DO_NO, AGENT_CODE);

            if ((data != null))
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

        public Response<DODETAILS> GetDOByDONo(string DO_NO)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<DODETAILS> response = new Response<DODETAILS>();

            if ((DO_NO == "") || (DO_NO == null))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide BL No";
                return response;
            }


            var data = DbClientFactory<DORepo>.Instance.GetDOByDONo(dbConn, DO_NO);

            if ((data != null))
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

        public Response<DODETAILS> GetDOExists(string BL_NO)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<DODETAILS> response = new Response<DODETAILS>();
            var data = DbClientFactory<DORepo>.Instance.GetDOExists(dbConn, BL_NO);

            if ((data != null))
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "DO is Already generate for this BL ";
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

        public Response<INVOICE_DETAILS_FOR_DO> CheckPaymentPaid(string BL_NO)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<INVOICE_DETAILS_FOR_DO> response = new Response<INVOICE_DETAILS_FOR_DO>();

            if ((BL_NO == "") || (BL_NO == null))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide BL No";
                return response;
            }

            var data = DbClientFactory<DORepo>.Instance.CheckPaymentPaid(dbConn, BL_NO);

            if ((data != null) && (data.Tables[0].Rows.Count > 0))
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                INVOICE_DETAILS_FOR_DO invoiceBL = new INVOICE_DETAILS_FOR_DO();

                invoiceBL = DORepo.GetSingleDataFromDataSet<INVOICE_DETAILS_FOR_DO>(data.Tables[0]);

                if (data.Tables.Contains("Table1"))
                {
                    invoiceBL.ICHARGES = DORepo.GetListFromDataSet<INVOICE_CHARGES>(data.Tables[1]);
                }

                if (data.Tables.Contains("Table2"))
                {
                    invoiceBL.RECEIPT = DORepo.GetListFromDataSet<RECEIPT_INVOICE>(data.Tables[2]);
                }

                if (data.Tables.Contains("Table3"))
                {
                    invoiceBL.RBANK = DORepo.GetListFromDataSet<RECEIPT_BANK>(data.Tables[3]);
                }
                if (data.Tables.Contains("Table4"))
                {
                    invoiceBL.RCHARGES = DORepo.GetListFromDataSet<RECEIPT_CHARGES>(data.Tables[4]);
                }

                response.Data = invoiceBL;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<RECEIPT_INVOICE> CheckReceiptGenerate(string INVOICE_NO)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<RECEIPT_INVOICE> response = new Response<RECEIPT_INVOICE>();
            var data = DbClientFactory<DORepo>.Instance.CheckReceiptGenerate(dbConn, INVOICE_NO);

            if ((data != null))
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
