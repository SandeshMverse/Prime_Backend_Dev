using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.IServices
{
    public interface IInvoiceService
    {
        Response<INVOICE_BL> GetBLDetails(string BL_NO, string PORT, string ORG_CODE, string AGENT_CODE);
        Response<CREDIT_NOTE_DETAILS> GetCreditNoteDetails(string CREDIT_NOTE, string PORT, string ORG_CODE);
        Response<CommonResponse> InsertInvoice(INVOICE_MASTER request);
        Response<CommonResponse> UpdateInvoice(INVOICE_MASTER request);
        Response<CommonResponse> InsertCreditNote(List<CREDIT_NOTE> request);
        Response<CommonResponse> FinalizeInvoice(INVOICE_FINALIZE request);
        Response<List<CREDIT_NOTE>> GetCreditList(string FROM_DATE, string TO_DATE, string PORT, string ORG_CODE, string CREDIT_NO);
        Response<List<INVOICE_MASTER>> GetInvoiceList(string FROM_DATE, string TO_DATE, string PORT, string ORG_CODE, string BL_NO, string PAYMENT_TERM);
        Response<List<INVOICE_MASTER>> GetInvoiceListImport(string FROM_DATE, string TO_DATE, string PORT, string ORG_CODE, string BL_NO, string PAYMENT_TERM);
        Response<INVOICE_MASTER> GetInvoiceDetails(int INVOICE_ID,string INVOICE_NO, string PORT, string ORG_CODE);
        Response<List<INVOICE_BL_CHECK>> GetBLExists(string INVOICE_TYPE, string BL_NO);
        Response<List<INVOICE_PAYMENT_TERM_CHECK>> PaymentTerm(string BL_NO);

        Response<List<BL_FINALIZED>> CheckBlFinalized(string BL_NO, string AGENT_CODE);
        Response<INVOICE_DETAILS_FOR_RECEIPT> GetInvoiceDetailsForReceipt(string INVOICE_NO, string PORT, string ORG_CODE, string USER_CODE);
        
        //new ADDED siddhesh
        Response<List<INVOICE_RATE_CHECK>> GetRateExists();
        Response<List<GET_CUST_LIST>> GetBLCustList(string BL_NO);

        Response<List<GET_CUST_LIST>> GetPrimeDetails();

        Response<List<GET_INVOICE_LIST>> GetInvoicesByBLNo(string BL_NO);
    }
}
