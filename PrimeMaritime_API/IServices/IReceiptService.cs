using PrimeMaritime_API.Response;
using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Models;
using System.Collections.Generic;

namespace PrimeMaritime_API.IServices
{
    public interface IReceiptService
    {
        Response<CommonResponse> InsertReceipt(RECEIPT request);
        Response<List<RECEIPT_INVOICE>> GetReceiptList(string FROM_DATE, string TO_DATE, string PORT, string ORG_CODE, string AGENT_CODE);

        Response<RECEIPT_INVOICE> CheckReceiptExist(string BL_NO, string INVOICE_NO);
    }
}
