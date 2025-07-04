﻿using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.IServices
{
    public interface IBLService
    {
        Response<string> InsertBL(BL request);
        Response<string> MergeBL(BL request);
        Response<string> MergeBLBYBLNO(BL request);
        Response<string> UpdateBL(BL request);
        Response<BL> GetBLDetails(string BL_NO, string BOOKING_NO, string AGENT_CODE);
        Response<List<BL>> GetBLHistory(string AGENT_CODE, string ORG_CODE, string PORT);
        Response<List<BL>> GetBLHistoryMIS(string month, string year);
        Response<List<BL>> GetBLALLHistory(string AGENT_CODE, string ORG_CODE, string PORT);
        Response<List<BL>> GetBLSurrenderedList(string POD,string ORG_CODE);
        Response<List<BL>> GetBLFORMERGE(string PORT_OF_LOADING, string PORT_OF_DISCHARGE, string SHIPPER, string CONSIGNEE, string VESSEL_NAME, string VOYAGE_NO, string NOTIFY_PARTY);
        Response<List<CONTAINERS>> GetContainerList(string AGENT_CODE, string DEPO_CODE, string BOOKING_NO, string CRO_NO,string BL_NO,string DO_NO,bool fromDO);
        Response<SRR> GetSRRDetails(string BL_NO, string BOOKING_NO, string AGENT_CODE);
        Response<CargoManifest> GetCargoManifestList(string AGENT_CODE, string VESSEL_NAME, string VOYAGE_NO);
        Response<List<BL>> GetBLListPM();
        Response<Organisation> GetOrgDetails(string ORG_CODE, string ORG_LOC_CODE);
        void InsertSurrender(string BL_NO);
        void InsertUploadedSurrender(string BL_NO);
        //added new 
        Response<CargoManifest> getcargoBL(string AGENT_CODE, string VESSEL_NAME, string VOYAGE_NO, string BL_NO);
        Response<CargoManifest> GetCargoBLSOA(string AGENT_CODE, string VESSEL_NAME, string VOYAGE_NO, string BL_NO);

        #region "Invoice"
        //Response<INVOICE_MASTER> GetBL();
        #endregion

        //SWITCHBL TESTING
        Response<string> InsertSWITCHBL(BL request);

        //TESTING FOR SWITCHBL
        Response<BL> GetSwitchBLDetails(string BL_NO, string BOOKING_NO, string AGENT_CODE);

        Response<string> UpdateSwitchBL(BL request);

        Response<string> UnlockBL(string BL_NO, int ID, string AGENT_CODE);

        Response<ONLYBL> CheckBLSwitched(string BL_NO);

        Response<string> UpdateItemNo(BL request);
    }
}
