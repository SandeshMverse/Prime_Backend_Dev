using Microsoft.AspNetCore.Http;
using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.IServices
{
    public interface IMasterService
    {
        #region "PARTY MASTER"
        Response<CommonResponse> InsertPartyMaster(PARTY_MASTER request);
        Response<List<PARTY_MASTER>> GetPartyMasterList(string Agent_code, string CustName, string CustType, bool Status, string FROM_DATE, string TO_DATE, bool IS_VENDOR);

        Response<PARTY_MASTER> GetPartyMasterDetails(string Agent_code, int CUSTOMER_ID, int VENDOR_AGREEMENT_ID);

        Response<CommonResponse> DeletePartyMasterDetails(int CUSTOMER_ID);

        Response<CommonResponse> UpdatePartyMasterDetails(PARTY_MASTER request);
        #endregion

        #region "CONTAINER MASTER"
        Response<CommonResponse> InsertContainerMaster(CONTAINER_MASTER request);

        Response<List<CONTAINER_MASTER>> GetContainerMasterList(string ContainerNo, string ContType, string ContSize, bool Status, string ONHIRE_DATE);

        Response<CONTAINER_MASTER> GetContainerMasterDetails(int ID, string CONTAINER_NO, string DEPO_CODE);

        Response<CommonResponse> UpdateContainerMasterList(CONTAINER_MASTER request);

        Response<CommonResponse> DeleteContainerMasterList(int ID);
        #endregion

        #region "COMMON MASTER"
        Response<CommonResponse> InsertMaster(MASTER request);

        Response<List<MASTER>> GetMasterList(string key, string FROM_DATE, string TO_DATE, string STATUS);

        Response<MASTER> GetMasterDetails(int ID);

        Response<CommonResponse> UpdateMaster(MASTER request);

        Response<CommonResponse> DeleteMaster(int ID);
        #endregion

        #region "VESSEL MASTER"
        Response<CommonResponse> InsertVesselMaster(VESSEL_MASTER request);
        Response<List<VESSEL_MASTER>> GetVesselMasterList(string VESSEL_NAME, string IMO_NO, string STATUS, string FROM_DATE, string TO_DATE);
        Response<VESSEL_MASTER> GetVesselMasterDetails(int ID);
        Response<CommonResponse> UpdateVesselMasterList(VESSEL_MASTER request);
        Response<CommonResponse> DeleteVesselMasterList(int ID);
        #endregion

        #region "SERVICE MASTER"
        Response<CommonResponse> InsertServiceMaster(SERVICE_MASTER request);
        Response<List<SERVICE_MASTER>> GetServiceMasterList();
        Response<SERVICE_MASTER> GetServiceMasterDetails(int ID);

        Response<CommonResponse> UpdateServiceMasterList(SERVICE_MASTER request);

        Response<CommonResponse> DeleteServiceMasterList(int ID);


        #endregion

        #region "CONTAINER TYPE MASTER"

        Response<CommonResponse> InsertContainerTypeMaster(CONTAINER_TYPE request);

        Response<List<CONTAINER_TYPE>> GetContainerTypeMasterList(string ContTypeCode, string ContType, string ContSize, bool Status, string FROM_DATE, string TO_DATE);

        Response<CONTAINER_TYPE> GetContainerTypeMasterDetails(int ID);

        Response<CommonResponse> UpdateConatinerTypeMaster(CONTAINER_TYPE request);

        Response<CommonResponse> DeleteContainerTypeMaster(int ID);


        #endregion

        #region "ICD MASTER"

        Response<List<ICD_MASTER>> GetICDMasterList();


        #endregion

        #region "DEPO MASTER"

        Response<List<DEPO_MASTER>> GetDEPOMasterList();


        #endregion

        #region "TERMINAL MASTER"

        Response<List<TERMINAL_MASTER>> GetTerminalMasterList();


        #endregion

        #region "CLEARING PARTY"

        Response<List<CLEARING_PARTY>> GetClearingPartyList();
        Response<string> InsertCP(CLEARING_PARTY request);

        #endregion

        #region "LINER"

        Response<CommonResponse> InsertLiner(LINER request);

        Response<List<LINER>> GetLinerList(string Name, string Code, string Description, bool Status, string FROM_DATE, string TO_DATE);

        Response<LINER> GetLinerDetails(int ID);
        Response<CommonResponse> UpdateLinerList(LINER request);

        Response<CommonResponse> DeleteLinerList(int ID);

        #endregion

        #region "LinerService"
        Response<CommonResponse> InsertService(SERVICE request);

        Response<List<SERVICE>> GetServiceList(bool Status, string FROM_DATE, string TO_CODE);

        Response<SERVICE> GetServiceDetails(int ID);

        Response<CommonResponse> UpdateService(SERVICE request);

        Response<CommonResponse> DeleteService(int ID);

        Response<List<HISTORY_PORT>> LinerServiceHistory(int ID);
        #endregion

        #region "VESSELSCHEDULE"
        Response<CommonResponse> InsertSchedule(SCHEDULE request);

        Response<List<SCHEDULE>> GetScheduleList(string VESSEL_NAME, string PORT_CODE, bool STATUS, string ETA, string ETD);
        Response<List<SCHEDULE>> getScheduleListWithFilter(string VESSEL_NAME, string PORT_CODE, bool STATUS, string ETA, string ETD);

        Response<SCHEDULE> GetDetailsByVesselAndVoyage(string VESSEL_NAME, string VOYAGE_NO);
        Response<List<SCHEDULE>> getVesselScheduleDetails(string VESSEL_NAME, string VOYAGE_NO, string SERVICE_NAME);

        Response<LINER_SERVICE> GetLinerServiceDetails(string SERVICE_NAME, string PORT_CODE);

        Response<SCHEDULE> GetScheduleDetails(int ID);

        Response<CommonResponse> UpdateSchedule(SCHEDULE request);

        Response<CommonResponse> DeleteSchedule(int ID);

        Response<string> uploadvesselschedule(List<SCHEDULE> schedule);
        Response<string> Updatevesselschedule(List<SCHEDULE> request);

        #endregion

        #region "VESSEL VOYAGE"
        Response<List<VOYAGE>> GetVoyageList(bool STATUS, string FROM_DATE, string TO_DATE);

        Response<VOYAGE> GetVoyageDetails(int ID);

        Response<CommonResponse> UpdateVoyage(VOYAGE request);

        Response<CommonResponse> DeleteVoyage(int ID);

        #endregion

        #region "LOCATION MASTER"
        Response<CommonResponse> InsertLocationMaster(LOCATION_MASTER request);

        Response<List<LOCATION_MASTER>> GetLocationMasterList(string LOC_NAME, string LOC_TYPE, bool STATUS, string FROM_DATE, string TO_DATE);

        Response<LOCATION_MASTER> GetLocationMasterDetails(string LOC_CODE);

        Response<CommonResponse> UpdateLocationMasterList(LOCATION_MASTER request);

        Response<CommonResponse> DeleteLocationMasterList(string LOC_CODE);
        #endregion

        #region "FREIGHT MASTER"
        Response<CommonResponse> InsertFreightMaster(FREIGHT_MASTER request);
        Response<List<FREIGHT_MASTER>> GetFreightMasterList();
        Response<FREIGHT_MASTER> GetFreightMasterDetails(int ID);
        Response<CommonResponse> UpdateFreightMasterList(FREIGHT_MASTER request);
        Response<CommonResponse> DeleteFreightMasterList(int ID);
        #endregion

        #region "CHARGE MASTER"
        Response<List<CHARGE_MASTER>> GetChargeMasterList();
        Response<CHARGE_MASTER> GetChargeMasterDetails(int ID);
        Response<CommonResponse> UpdateChargeMasterList(CHARGE_MASTER request);
        Response<CommonResponse> DeleteChargeMasterList(int ID);
        #endregion

        #region "STEVEDORING MASTER"
        Response<List<STEV_MASTER>> GetStevedoringMasterList();
        Response<STEV_MASTER> GetStevedoringMasterDetails(int ID);
        Response<CommonResponse> UpdateStevedoringMasterList(STEV_MASTER request);
        Response<CommonResponse> DeleteStevedoringMasterList(int ID);
        #endregion

        #region "DETENTION MASTER"
        Response<List<DETENTION_MASTER>> GetDetentionMasterList();
        Response<DETENTION_MASTER> GetDetentionMasterDetails(int ID);
        Response<CommonResponse> UpdateDetentionMasterList(DETENTION_MASTER request);
        Response<CommonResponse> DeleteDetentionMasterList(int ID);
        #endregion

        #region "MANDATORY MASTER"
        Response<List<MANDATORY_MASTER>> GetMandatoryMasterList();
        Response<MANDATORY_MASTER> GetMandatoryMasterDetails(int ID);
        Response<CommonResponse> UpdateMandatoryMasterList(MANDATORY_MASTER request);
        Response<CommonResponse> DeleteMandatoryMasterList(int ID);
        #endregion

        #region "UPLOAD TARIFF"
        Response<string> UploadFreightTariff(List<FREIGHT_MASTER> request);
        Response<string> UploadChargeTariff(List<CHARGE_MASTER> request);
        Response<string> UploadStevTariff(List<STEV_MASTER> request);
        Response<string> UploadDetentionTariff(List<DETENTION_MASTER> request);
        Response<string> UploadMandatoryTariff(List<MANDATORY_MASTER> request);
        Response<string> UploadSlotRateTariff(List<SLOT_RATE_MASTER> request);  //SIDDHESH
        Response<string> UploadMNRTariff(List<MNR_TARIFF_LIST> request);  //SIDDHESH
        #endregion

        #region "ORGANISATION MASTER"
        Response<CommonResponse> InsertOrgMaster(ORG_MASTER request);
        Response<CommonResponse> ValidateOrgCode(string ORG_CODE);
        Response<List<ORG_MASTER>> GetOrgMasterList();
        Response<ORG_MASTER> GetOrgMasterDetails(string ORG_CODE);
        Response<CommonResponse> UpdateOrgMasterList(ORG_MASTER request);
        Response<CommonResponse> DeleteOrgMasterList(string ORG_CODE);
        #endregion

        #region "SLOT MASTER"
        Response<CommonResponse> InsertSlotMaster(SLOT_MASTER request);
        Response<List<SLOT_MASTER>> GetSlotMasterList(string SERVICE, string PORT);
        Response<SLOT_MASTER> GetSlotMasterDetails(int ID);
        Response<CommonResponse> UpdateSlotMasterList(SLOT_MASTER request);
        Response<CommonResponse> DeleteSlotMasterList(int ID);
        #endregion

        #region "CHARGES MASTER"
        Response<CommonResponse> InsertChargeMaster(CHARGES_MASTER request);

        Response<List<CHARGES_MASTER>> GetChargeMaster();

        Response<List<CHARGES_MASTER>> GetChargeMastersDetails(int ID);

        Response<CommonResponse> UpdateChargesMaster(CHARGES_MASTER request);

        Response<CommonResponse> DeleteChargesMaster(int ID);
        #endregion

        #region "HSN-CODE"

        Response<CommonResponse> InsertHsnCode(HSN_MASTER request);

        Response<List<HSN_MASTER>> GetHsnMaster();

        Response<CommonResponse> DeleteHsnMaster(int ID);
        #endregion

        #region "COUNTRY MASTER"
        Response<CommonResponse> InsertCountryMaster(COUNTRY_MASTER request);

        Response<List<COUNTRY_MASTER>> GetCountryMasterList();

        Response<COUNTRY_MASTER> GetCountryMasterDetails(int ID);

        Response<CommonResponse> UpdateCountryMasterList(COUNTRY_MASTER request);

        Response<CommonResponse> DeleteCountryMasterList(int ID);
        #endregion

        #region "STATE MASTER"
        Response<CommonResponse> InsertStateMaster(STATE_MASTER request);

        Response<List<STATE_MASTER>> GetStateMasterList();

        Response<STATE_MASTER> GetStateMasterDetails(int ID);

        Response<CommonResponse> UpdateStateMasterList(STATE_MASTER request);

        Response<CommonResponse> DeleteStateMasterList(int ID);
        #endregion

        #region "IAL"
        Response<IAL> GetIALDetails(string AGENT_CODE, string VESSEL_NAME, string VOYAGE_NO, string PORT_OF_LOADING);

        #endregion

        #region "EAL"
        Response<EAL> GetEALDetails(string AGENT_CODE, string VESSEL_NAME, string VOYAGE_NO, string PORT_OF_DISCHARGE);
        #endregion

        #region "SLOT RATE MASTER"
        Response<List<SLOT_RATE_MASTER>> GetSlotRateList();
        Response<SLOT_RATE_MASTER> GetSlotRateMasterDetails(int ID);
        Response<CommonResponse> UpdateSlotRateMaster(SLOT_RATE_MASTER request);
        Response<CommonResponse> DeleteSlotRateMasterList(int ID);
        Response<List<SLOT_RATE_HISTORY_MASTER>> HistorySlotRateMasterList(int ID);
        #endregion

        #region"FLEET COMPOSITION REPORT"
        Response<List<FLEET_COMPOSITION_REPORT>> GetFleetReport(string LOCATION);

        #endregion

        #region"STOCK POSITION REPORT"
        Response<List<STOCK_POSITION_REPORT>> GetStockReport(string LOCATION);

        #endregion

        #region "CONTAINER TURN TIME REPORT"

        Response<List<CONTAINER_TURN_TIME_REPORT>> GetContainerTurnReport(string LOCATION);
        #endregion

        #region "CONTAINER REPAIRS"

        Response<List<CONTAINER_REPAIRS>> GetContainerRepairs(string LOCATION);
        #endregion

        #region "SRR LIST"
        Response<List<SRR_LIST>> GetSRRLIST(string LOCATION, string AGENT_NAME, string MONTH, string YEAR);
        #endregion

        #region "DOCS LIST"
        Response<List<DOCS_LIST>> GetDOCSLIST(string LOCATION, string MONTH, string YEAR);
        #endregion

        #region "AGENT NAME"
        Response<List<AGENT_NAME>> GetAgent();
        #endregion

        #region "SHIPPER-REPORT"
        Response<List<SHIPPER_LIST>> GetShipperLIST(string LOCATION, string CUSTOMER_NAME, string MONTH, string YEAR);
        #endregion

        #region "CONSIGNEE-REPORT"
        Response<List<CONSIGNEE_LIST>> GetConsigneeLIST(string LOCATION, string CONSIGNEE_NAME, string MONTH, string YEAR);
        #endregion

        #region "DETENATION-REPORT"
        Response<List<DETENATION_LIST>> GetDetenationList(string LOCATION, string CONSIGNEE_NAME, string MONTH, string YEAR);
        #endregion

        #region "CONSIGNEE NAME"
        Response<List<CONSIGNEE_NAME>> GetConsignee();
        #endregion

        #region "AGENCY-REPORT"
        Response<List<AGENCY_LIST>> GetAgencyList(string LOCATION, string MONTH, string YEAR);
        #endregion

        #region "SALES-REPORT"
        Response<List<SALES_LIST>> GetSalesList(string LOCATION, string MONTH, string YEAR);
        #endregion

        #region "SERVICE NAME"
        Response<List<SERVICE_NAME>> GetService();
        #endregion

        #region "VOLUME-REPORT"
        Response<List<VOLUME_LIST>> GetVolume(string LOCATION, string MONTH, string YEAR, string SERVICE);
        #endregion

        #region "VESSEL-DETAILS"
        Response<List<VESSEL_VOYAGE>> GetVessel(string LOCATION, string VESSEL_NAME, string VOYAGE_NO);
        #endregion

        #region "SERVICE-DETAILS"
        Response<List<VESSEL_VOYAGE>> GetServices(string LOCATION, string SERVICE);
        #endregion

        #region "EQUIPMENT_TYPE"

        Response<CommonResponse> InsertEquipmentType(EQUIPMENT_TYPE_MASTER request);
        Response<List<EQUIPMENT_TYPE_MASTER>> GetEquipmentTypeList(string IS_ACTIVE, string EQUIPMENT_TYPE, string FROM_DATE);
        Response<EQUIPMENT_TYPE_MASTER> GetEquipmentByID(int ID);
        Response<CommonResponse> UpdatEquipmentTypeList(EQUIPMENT_TYPE_MASTER request);
        Response<CommonResponse> DeleteEquipmentTypeList(int ID);

        void UpdateVendorAgreementPath(int VENDORID, string attachmentpath);

        #endregion

        #region"Agreement No"
        Response<List<VENDOR_AGREEMENT>> GetAllAgreementNoList();
        #endregion

        #region "VEDNOR AGREEMENT"

        Response<CommonResponse> InsertVendorAgreement(VENDOR_AGREEMENT_LIST request);
        Response<CommonResponse> UpdateVendorAgreement(VENDOR_AGREEMENT_LIST request);
        Response<VENDOR_AGREEMENT_LIST> GetVendorAgreementById( int VENDOR_AGREEMENT_ID);
        Response<CommonResponse> DeleteVendorAgreementById(int VENDOR_AGREEMENT_ID);
        Response<List<VENDOR_AGREEMENT_LIST>> GetVendorAgreementList(string AGREEMENT_NO, string IS_ACTIVE, string START_DATE, string END_DATE);

        Response<List<EQUIPMENT_TYPE_LIST>> GetAllEquipmentTypeList();
        #endregion

        #region " VENDOR AGREEMENT REPORT"
        Response<List<VENDOR_AGREEMENT_REPORT>> GetVendorAgreementReport(string VENDOR_ID, string MONTH, int YEAR);

        #endregion

        #region "VENDOR NAME LIST"
        Response<List<VENDOR_LIST>> GetAllVendorList();
        #endregion

        #region "GET ALL LINER LIST"
        Response<List<LINER_NAME>> GetAllLinerList();
        #endregion

        #region "GET ALL SLOT OPERATOR LIST"
        Response<List<SLOT_OPERATOR_NAME>> GetAllSlotOperatorList();
        #endregion

        #region "GET ALL SLOT PURCHASE LIST"
        Response<string> InsertSlotPurchase(List<SLOT_PURCHASE_LIST> request);
        Response<string> Updateslotpurchase(List<SLOT_PURCHASE_LIST> request);
        Response<List<SLOT_PURCHASE_LIST>> GetAllSlotPurchase();
        Response<List<SLOT_PURCHASE_LIST>> GetSlotpurchaseById(int ID);
        Response<CommonResponse> UpdateSlotPurchase(SLOT_PURCHASE_LIST request);

        #endregion

        #region "MNR TARIFF MASTER"
        Response<List<MNR_TARIFF_LIST>> GetAllMnrList();
        Response<MNR_TARIFF_LIST> GetMNRListById(int ID);
        Response<CommonResponse> UpdateMNRTariffList(MNR_TARIFF_LIST request);
        Response<CommonResponse> DeleteMNRListById(int ID);
        #endregion
    }
}
