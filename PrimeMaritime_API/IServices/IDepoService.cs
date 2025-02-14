using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityModel.OidcConstants;

namespace PrimeMaritime_API.IServices
{
    public interface IDepoService
    {
        Response<CommonResponse> InsertContainer(DEPO_CONTAINER request);
        Response<CommonResponse> InsertMRRequest(List<MR_LIST> request);
        Response<List<MNR_LIST>> GetMNRList(string OPERATION, string DEPO_CODE, string MR_NO, string STATUS, string FROMDATE, string TODATE);
        Response<MNR_TARIFF> GetMNRTariff(string COMPONENT, string REPAIR, string LENGTH, string WIDTH, string HEIGHT, string QUANTITY, string DEPO_CODE);
        Response<List<MR_LIST>> GetMNRDetails(string OPERATION, string MR_NO);
        Response<List<MR_LIST>> getMRDetailsByID(string OPERATION, string MR_NO, int ID);
        Response<string> ApproveRate(List<MR_LIST> request);
        Response<string> InsertNewMRRequest(List<MR_LIST> request);
        Response<string> DeleteMRRequest(string MR_NO, string LOCATION);


        //void InsertMNRFiles(List<MR_LIST> newMNRList, List<string> attachmentPaths);
        void InsertMNRFiles(List<MR_LIST> newMNRList, Dictionary<string, List<string>> attachmentPaths);
        Response<string> DeleteMRImage(int ID, int MR_ID);
        void updateMRRequest(List<MR_LIST> updateMNRList, Dictionary<string, List<string>> attachmentPaths);
        void InsertPrinMNRFiles(List<MR_LIST> newMNRList, List<string> attachmentPaths);
    }
}
