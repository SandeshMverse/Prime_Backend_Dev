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

namespace PrimeMaritime_API.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IConfiguration _config;
        public InvoiceService(IConfiguration config)
        {
            _config = config;
        }
        public Response<INVOICE_BL> GetBLDetails(string BL_NO, string PORT, string ORG_CODE, string AGENT_CODE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<INVOICE_BL> response = new Response<INVOICE_BL>();

            if ((BL_NO == "") || (BL_NO == null))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide BL No";
                return response;
            }

            var data = DbClientFactory<InvoiceRepo>.Instance.GetBLDetails(dbConn, BL_NO, PORT, ORG_CODE, AGENT_CODE);

            if ((data != null) && (data.Tables[0].Rows.Count > 0))
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                INVOICE_BL invoiceBL = new INVOICE_BL();

                invoiceBL = InvoiceRepo.GetSingleDataFromDataSet<INVOICE_BL>(data.Tables[0]);

                if (data.Tables.Contains("Table1"))
                {
                    invoiceBL.FREIGHT = InvoiceRepo.GetListFromDataSet<INVOICE_BL_CHARGE>(data.Tables[1]);
                }

                if (data.Tables.Contains("Table2"))
                {
                    invoiceBL.POL = InvoiceRepo.GetListFromDataSet<INVOICE_BL_CHARGE>(data.Tables[2]);
                }

                if (data.Tables.Contains("Table3"))
                {
                    invoiceBL.POD = InvoiceRepo.GetListFromDataSet<INVOICE_BL_CHARGE>(data.Tables[3]);
                }
                if (data.Tables.Contains("Table4"))
                {
                    invoiceBL.CONTAINERS = InvoiceRepo.GetListFromDataSet<INVOICE_BL_CONTAINER>(data.Tables[4]);
                }

                if (data.Tables.Contains("Table5"))
                {
                    invoiceBL.BRANCH = InvoiceRepo.GetListFromDataSet<INVOICE_BL_BRANCH>(data.Tables[5]);
                }
                if (data.Tables.Contains("Table6"))
                {
                    invoiceBL.BANK = InvoiceRepo.GetListFromDataSet<INVOICE_BL_BANK>(data.Tables[6]);
                }
                if (data.Tables.Contains("Table7"))
                {
                    invoiceBL.RATECHECK = InvoiceRepo.GetListFromDataSet<INVOICE_RATE_CHECK>(data.Tables[7]);
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
        public Response<CREDIT_NOTE_DETAILS> GetCreditNoteDetails(string CREDIT_NO, string PORT, string ORG_CODE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CREDIT_NOTE_DETAILS> response = new Response<CREDIT_NOTE_DETAILS>();

            if ((CREDIT_NO == "") || (CREDIT_NO == null))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide Credit No";
                return response;
            }

            var data = DbClientFactory<InvoiceRepo>.Instance.GetCreditNoteDetails(dbConn, CREDIT_NO, PORT, ORG_CODE);

            if ((data != null) && (data.Tables[0].Rows.Count > 0))
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                CREDIT_NOTE_DETAILS creditNote = new CREDIT_NOTE_DETAILS();

                creditNote = InvoiceRepo.GetSingleDataFromDataSet<CREDIT_NOTE_DETAILS>(data.Tables[0]);

                if (data.Tables.Contains("Table1"))
                {
                    creditNote.CHARGE_LIST = InvoiceRepo.GetListFromDataSet<CREDIT_NOTE_CHARGE_DETAILS>(data.Tables[1]);
                }

                response.Data = creditNote;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<CommonResponse> InsertInvoice(INVOICE_MASTER request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<InvoiceRepo>.Instance.InsertInvoice(dbConn, request);

            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Invoice saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<CommonResponse> InsertCreditNote(List<CREDIT_NOTE> request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<InvoiceRepo>.Instance.InsertCreditNote(dbConn, request);

            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Credit Note saved Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<CommonResponse> FinalizeInvoice(INVOICE_FINALIZE request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<InvoiceRepo>.Instance.FinalizeInvoice(dbConn, request);

            Response<CommonResponse> response = new Response<CommonResponse>();
            response.Succeeded = true;
            response.ResponseMessage = "Invoice finalized Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<INVOICE_MASTER> GetInvoiceDetails(int INVOICE_ID, string INVOICE_NO, string PORT, string ORG_CODE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<INVOICE_MASTER> response = new Response<INVOICE_MASTER>();

            if (INVOICE_ID == 0)
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide Invoice ID";
                return response;
            }

            var data = DbClientFactory<InvoiceRepo>.Instance.GetInvoiceDetails(dbConn, INVOICE_ID, INVOICE_NO, PORT, ORG_CODE);

            if ((data != null) && (data.Tables[0].Rows.Count > 0))
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                INVOICE_MASTER invoice = new INVOICE_MASTER();

                invoice = InvoiceRepo.GetSingleDataFromDataSet<INVOICE_MASTER>(data.Tables[0]);

                if (data.Tables.Contains("Table1"))
                {
                    invoice.BL_LIST = InvoiceRepo.GetListFromDataSet<INVOICE_CHARGES>(data.Tables[1]);
                }

                if (data.Tables.Contains("Table2"))
                {
                    invoice.CONTAINER_LIST = InvoiceRepo.GetListFromDataSet<INVOICE_BL_CONTAINER>(data.Tables[2]);
                }

                if (data.Tables.Contains("Table3"))
                {
                    invoice.BL_CONTAINER_LIST = InvoiceRepo.GetListFromDataSet<INVOICE_BL_CONTAINER>(data.Tables[3]);
                }

                if (data.Tables.Contains("Table4"))
                {
                    invoice.BRANCH = InvoiceRepo.GetListFromDataSet<INVOICE_BL_BRANCH>(data.Tables[4]);
                }

                if (data.Tables.Contains("Table5"))
                {
                    if (data.Tables[5].Rows.Count > 0)
                    {
                        invoice.BANK = InvoiceRepo.GetListFromDataSet<INVOICE_BL_BANK>(data.Tables[5]);
                    }
                }

                if (data.Tables.Contains("Table6"))
                {
                    if (data.Tables[6].Rows.Count > 0)
                    {
                        invoice.SELECTED_BRANCH = InvoiceRepo.GetListFromDataSet<INVOICE_BL_BRANCH>(data.Tables[6]);
                    }
                }
                if (data.Tables.Contains("Table7"))
                {
                    if (data.Tables[7].Rows.Count > 0)
                    {
                        invoice.RATECHECK = InvoiceRepo.GetListFromDataSet<INVOICE_RATE_CHECK>(data.Tables[7]);
                    }
                }

                response.Data = invoice;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<INVOICE_DETAILS_FOR_RECEIPT> GetInvoiceDetailsForReceipt(string INVOICE_NO, string PORT, string ORG_CODE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<INVOICE_DETAILS_FOR_RECEIPT> response = new Response<INVOICE_DETAILS_FOR_RECEIPT>();

            if (String.IsNullOrEmpty(INVOICE_NO))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide Invoice No";
                return response;
            }

            var data = DbClientFactory<InvoiceRepo>.Instance.GetInvoiceDetailsForReceipt(dbConn, INVOICE_NO, PORT, ORG_CODE);

            if ((data != null) && (data.Tables[0].Rows.Count > 0))
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                INVOICE_DETAILS_FOR_RECEIPT invoice = new INVOICE_DETAILS_FOR_RECEIPT();

                //invoice = InvoiceRepo.GetSingleDataFromDataSet<INVOICE_DETAILS_FOR_RECEIPT>(data.Tables[0]);

                if (data.Tables.Contains("Table"))
                {
                    invoice.INVOICE_LIST = InvoiceRepo.GetListFromDataSet<INVOICE_DETAILS_FOR_RECEIPT_INVOICES>(data.Tables[0]);
                }

                if (data.Tables.Contains("Table1"))
                {
                    invoice.BANK_LIST = InvoiceRepo.GetListFromDataSet<CUSTOMER_BANK>(data.Tables[1]);
                }

                if (data.Tables.Contains("Table2"))
                {
                    invoice.CHARGE_LIST = InvoiceRepo.GetListFromDataSet<INVOICE_DETAILS_FOR_RECEIPT_CHARGES>(data.Tables[2]);
                }

                response.Data = invoice;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<List<INVOICE_MASTER>> GetInvoiceList(string FROM_DATE, string TO_DATE, string PORT, string ORG_CODE, string BL_NO)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<INVOICE_MASTER>> response = new Response<List<INVOICE_MASTER>>();
            var data = DbClientFactory<InvoiceRepo>.Instance.GetInvoiceList(dbConn, FROM_DATE, TO_DATE, ORG_CODE, PORT, BL_NO);

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

        public Response<List<INVOICE_BL_CHECK>> GetBLExists(string INVOICE_TYPE, string BL_NO)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<INVOICE_BL_CHECK>> response = new Response<List<INVOICE_BL_CHECK>>();
            var data = DbClientFactory<InvoiceRepo>.Instance.GetBLExists(dbConn, INVOICE_TYPE, BL_NO);

            if (data.Count > 0)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "BL_NO is Already Exists ";
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


        public Response<List<INVOICE_PAYMENT_TERM_CHECK>> PaymentTerm(string BL_NO)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<INVOICE_PAYMENT_TERM_CHECK>> response = new Response<List<INVOICE_PAYMENT_TERM_CHECK>>();
            var data = DbClientFactory<InvoiceRepo>.Instance.PaymentTerm(dbConn, BL_NO);

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
        public Response<List<CREDIT_NOTE>> GetCreditList(string FROM_DATE, string TO_DATE, string PORT, string ORG_CODE, string CREDIT_NO)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<CREDIT_NOTE>> response = new Response<List<CREDIT_NOTE>>();
            var data = DbClientFactory<InvoiceRepo>.Instance.GetCreditList(dbConn, FROM_DATE, TO_DATE, ORG_CODE, PORT, CREDIT_NO);

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

        //new ADDED siddhesh
        public Response<List<INVOICE_RATE_CHECK>> GetRateExists()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<INVOICE_RATE_CHECK>> response = new Response<List<INVOICE_RATE_CHECK>>();
            var data = DbClientFactory<InvoiceRepo>.Instance.GetRateExists(dbConn);

            if (data.Count > 0)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "RATE is Already Exists ";
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

        public Response<List<GET_CUST_LIST>> GetBLCustList(string BL_NO)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<GET_CUST_LIST>> response = new Response<List<GET_CUST_LIST>>();
            var data = DbClientFactory<InvoiceRepo>.Instance.GetBLCustList(dbConn, BL_NO);

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

        public Response<List<GET_CUST_LIST>> GetPrimeDetails()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<GET_CUST_LIST>> response = new Response<List<GET_CUST_LIST>>();
            var data = DbClientFactory<InvoiceRepo>.Instance.GetPrimeDetails(dbConn);

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
