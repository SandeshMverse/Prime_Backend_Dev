using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Models
{
    public class PARTY_MASTER
    {
        public int CUST_ID { get; set; }
        public string CUST_NAME { get; set; }
        public string CUST_ADDRESS { get; set; }
        public string CUST_EMAIL { get; set; }
        public string CUST_CONTACT { get; set; }
        public string CUST_TYPE { get; set; }
        public string GSTIN { get; set; }
        public string VAT_NO { get; set; }
        public bool STATUS { get; set; }
        public string REMARKS { get; set; }
        public string AGENT_CODE { get; set; }
        public string CREATED_BY { get; set; }
        public string COUNTRY { get; set; }
        public string STATE { get; set; }
        public string CITY { get; set; }
        public string PINCODE { get; set; }
        public string PAN { get; set; }
        public string CONTACT_PERSON_NAME { get; set; }
        public string CONTACT_PERSON_NO { get; set; }
        public bool IS_GROUP_COMPANIES { get; set; }
        public string SALES_NAME { get; set; }
        public string SALES_CODE { get; set; }
        public string SALES_LOC { get; set; }
        public string SALES_EFFECTIVE_DATE { get; set; }       
        public bool IS_VENDOR { get; set; }
        public List<CUSTOMER_BRANCH> BRANCH_LIST { get; set; } = new List<CUSTOMER_BRANCH>();
        public List<CUSTOMER_BANK> BANK_LIST { get; set; } = new List<CUSTOMER_BANK>();
        public List<VENDOR_AGREEMENT_LIST> VENDOR_AGREEMENT_LIST { get; set; } = new List<VENDOR_AGREEMENT_LIST>();
        public List<VENDOR_PORT_LIST> VENDOR_PORT_LIST { get; set; } = new List<VENDOR_PORT_LIST>();
        public List<VENDOR_PICKUP_LOCATION_LIST> VENDOR_PICKUP_LOCATION_LIST { get; set; } = new List<VENDOR_PICKUP_LOCATION_LIST>();
    }
    public class CUSTOMER_BRANCH
    {
        public int ID { get; set; }
        public int CUST_ID { get; set; }
        public string BRANCH_NAME { get; set; }
        public string BRANCH_CODE { get; set; }
        public string COUNTRY { get; set; }
        public string STATE { get; set; }
        public string CITY { get; set; }
        public string TAN { get; set; }
        public string TAX_NO { get; set; }
        public string TAX_TYPE { get; set; }
        public string PIC_NAME { get; set; }
        public string PIC_CONTACT { get; set; }
        public string PIC_EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public bool IS_SEZ { get; set; }
        public bool IS_TAX_APPLICABLE { get; set; }
        public string ORG_CODE { get; set; }
    }
    public class CUSTOMER_BANK
    {
        public int ID { get; set; }
        public int CUST_ID { get; set; }
        public string BRANCH_CODE { get; set; }
        public string BANK_NAME { get; set; }
        public string BANK_ACC_NO { get; set; }
        public string BANK_IFSC { get; set; }
        public string BANK_SWIFT { get; set; }
        public string BANK_REMARKS { get; set; }
    }
    public class CONTAINER_MASTER
    {
        public int ID { get; set; }
        public string CONTAINER_NO { get; set; }
        public string CONTAINER_TYPE { get; set; }
        public DateTime ONHIRE_DATE { get; set; }      
        public string ONHIRE_LOCATION { get; set; }      
        public string LEASED_FROM { get; set; }      
        public bool STATUS { get; set; }

        //add new field
        public string AGREEMENT_NO { get; set; }
        public DateTime OFFHIRE_DATE { get; set; }
        public DateTime YEAR_OF_MANUFACTURE { get; set; }
        public decimal TARE_WEIGHT { get; set; }
        public decimal PAYLOAD_CAPACITY { get; set; }
        public decimal GROSS_WEIGHT { get; set; }
        public string CSC_DETAILS { get; set; }
 
    }
    public class COUNTRY_MASTER
    {
        public int ID { get; set; }
        public int CODE { get; set; }
        public string NAME { get; set; }
        public string SHORT_NAME { get; set; }
        public bool STATUS { get; set; }
        public string CREATED_BY { get; set; }
    }
    public class STATE_MASTER
    {
        public int ID { get; set; }
        public int CODE { get; set; }
        public string NAME { get; set; }
        public string SHORT_NAME { get; set; }
        public int COUNTRY_ID { get; set; }
        public string COUNTRY_NAME { get; set; }
        List<COUNTRY_MASTER> COUNTRIES { get; set; } = new List<COUNTRY_MASTER>();
        public bool IS_UT { get; set; }
        public bool STATUS { get; set; }
        public string CREATED_BY { get; set; }
    }
    public class MASTER
    {
        public int ID { get; set; }
        public string KEY_NAME { get; set; }
        public string CODE { get; set; }
        public string CODE_DESC { get; set; }
        public Boolean STATUS { get; set; }
        public string PARENT_CODE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; } = null;
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; } = null;
    }
    public class VESSEL_MASTER
    {
        public int ID { get; set; }
        public string VESSEL_NAME { get; set; }
        public string IMO_NO { get; set; }
        public string COUNTRY_CODE { get; set; }
        public string VESSEL_CODE { get; set; }
        public Boolean STATUS { get; set; }
        public string CREATED_BY { get; set; }

        //public DateTime CREATED_ON { get; set; }

        //public string UPDATED_BY { get; set; }

        //public DateTime UPDATED_DATE { get; set; }
    }
    public class SERVICE_MASTER
    {
        public int ID { get; set; }
        public string LINER_CODE { get; set; }
        public string SERVICE_NAME { get; set; }
        public string PORT_CODE { get; set; }
        public Boolean STATUS { get; set; }
        public string CREATED_BY { get; set; }
    }
    public class CONTAINER_TYPE
    {
        public int ID { get; set; }
        public string CONT_TYPE_CODE { get; set; }
        public string CONT_TYPE { get; set; }
        public int CONT_SIZE { get; set; }
        public string ISO_CODE { get; set; }
        public int TEUS { get; set; }
        public string OUT_DIM { get; set; }
        public Boolean STATUS { get; set; }
        public string CREATED_BY { get; set; }
    }
    public class ICD_MASTER
    {
        public int ID { get; set; }
        public string LOC_NAME { get; set; }
        public string LOC_CODE { get; set; }
        public string LOC_TYPE { get; set; }
        public string ADDRESS { get; set; }
        public string COUNTRY_CODE { get; set; }
        public string ASSOCIATE_PORT_CODE { get; set; }
        public bool LOC_TYPE_STATUS { get; set; }
        public DateTime CREATED_DATE { get; set; }
    }
    public class DEPO_MASTER
    {
        public int ID { get; set; }
        public string LOC_NAME { get; set; }
        public string LOC_CODE { get; set; }
        public string LOC_TYPE { get; set; }
        public string ADDRESS { get; set; }
        public string COUNTRY_CODE { get; set; }
        public string ASSOCIATE_PORT_CODE { get; set; }
        public string LOC_TYPE_STATUS { get; set; }
        public DateTime CREATED_DATE { get; set; }
    }
    public class TERMINAL_MASTER
    {
        public int ID { get; set; }
        public string LOC_NAME { get; set; }
        public string LOC_CODE { get; set; }
        public string LOC_TYPE { get; set; }
        public string ADDRESS { get; set; }
        public string COUNTRY_CODE { get; set; }
        public string ASSOCIATE_PORT_CODE { get; set; }
        public bool LOC_TYPE_STATUS { get; set; }
        public DateTime CREATED_DATE { get; set; }
    }
    public class LINER
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public Boolean STATUS { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime UPDATED_DATE { get; set; }
    }
    public class SERVICE
    {
        public int ID { get; set; }
        public String LINER_CODE { get; set; }
        public String SERVICE_NAME { get; set; }
        public String PORT_CODE { get; set; }
        public Boolean STATUS { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime UPDATED_DATE { get; set; }
    }
    public class SCHEDULE
    {
        public int ID { get; set; }
        public string VESSEL_NAME { get; set; }
        public string SERVICE_NAME { get; set; }
        public string PORT_CODE { get; set; }
        public string VIA_NO { get; set; }
        public DateTime ETA { get; set; }
        public DateTime ETD { get; set; }
        public Boolean STATUS { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime UPDATED_DATE { get; set; }
    }
    public class LOCATION_MASTER
    {
        public int ID { get; set; }
        public string LOC_NAME { get; set; }
        public string LOC_CODE { get; set; }
        public bool IS_DEPO { get; set; }
        public bool IS_CFS { get; set; }
        public bool IS_TERMINAL { get; set; }
        public bool IS_YARD { get; set; }
        public bool IS_ICD { get; set; }
        public string ADDRESS { get; set; }
        public string COUNTRY_CODE { get; set; }
        public string PORT_CODE { get; set; }
        public bool STATUS { get; set; }
        public DateTime CREATED_DATE { get; set; }     
        public string CREATED_BY { get; set; }
    }
    public class FREIGHT_MASTER
    {
        public int ID { get; set; }
        public string POL { get; set; }
        public string POD { get; set; }
        public string Charge { get; set; }
        public string Currency { get; set; }
        public string LadenStatus { get; set; }
        public string ServiceMode { get; set; }
        public decimal DRY20 { get; set; }
        public decimal DRY40 { get; set; }
        public decimal DRY40HC { get; set; }
        public decimal DRY45 { get; set; }
        public decimal RF20 { get; set; }
        public decimal RF40 { get; set; }
        public decimal RF40HC { get; set; }
        public decimal RF45 { get; set; }
        public decimal HAZ20 { get; set; }
        public decimal HAZ40 { get; set; }
        public decimal HAZ40HC { get; set; }
        public decimal HAZ45 { get; set; }
        public decimal SEQ20 { get; set; }
        public decimal SEQ40 { get; set; }
        public List<CHARGE_MASTER> CHARGELIST { get; set; } = new List<CHARGE_MASTER>();
    }
    public class CHARGE_MASTER
    {
        public int ID { get; set; }
        public string POL { get; set; }
        public string POD { get; set; }
        public string CHARGE_CODE { get; set; }
        public string CURRENCY { get; set; }
        public decimal IMPCOST20 { get; set; }
        public decimal IMPCOST40 { get; set; }
        public decimal IMPINCOME20 { get; set; }
        public decimal IMPINCOME40 { get; set; }
        public decimal EXPCOST20 { get; set; }
        public decimal EXPCOST40 { get; set; }
        public decimal EXPINCOME20 { get; set; }
        public decimal EXPINCOME40 { get; set; }
        public int FROM_VAL { get; set; }
        public int TO_VAL { get; set; }
        public bool STATUS { get; set; }
    }
    public class STEV_MASTER
    {
        public int ID { get; set; }
        public string IE_TYPE { get; set; }
        public string POL { get; set; }
        public string TERMINAL { get; set; }
        public string CHARGE_CODE { get; set; }
        public string CURRENCY { get; set; }
        public string LADEN_STATUS { get; set; }
        public string SERVICE_MODE { get; set; }
        public decimal DRY20 { get; set; }
        public decimal DRY40 { get; set; }
        public decimal DRY40HC { get; set; }
        public decimal DRY45 { get; set; }
        public decimal RF20 { get; set; }
        public decimal RF40 { get; set; }
        public decimal RF40HC { get; set; }
        public decimal RF45 { get; set; }
        public decimal HAZ20 { get; set; }
        public decimal HAZ40 { get; set; }
        public decimal HAZ40HC { get; set; }
        public decimal HAZ45 { get; set; }
        public decimal SEQ20 { get; set; }
        public decimal SEQ40 { get; set; }
    }
    public class DETENTION_MASTER
    {
        public int ID { get; set; }
        public string PORT_CODE { get; set; }
        public string CONTAINER_TYPE { get; set; }
        public string CURRENCY { get; set; }
        public int FROM_DAYS { get; set; }
        public int TO_DAYS { get; set; }
        public decimal RATE20 { get; set; }
        public decimal RATE40 { get; set; }
        public decimal HC_RATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
    }
    public class MANDATORY_MASTER
    {
        public int ID { get; set; }
        public string PORT_CODE { get; set; }
        public string ORG_CODE { get; set; }
        public string CHARGE_CODE { get; set; }
        public string IE_TYPE { get; set; }
        public string LADEN_STATUS { get; set; }
        public string CURRENCY { get; set; }
        public decimal RATE20 { get; set; }
        public decimal RATE40 { get; set; }
        public bool IS_PERCENTAGE { get; set; }
        public int PERCENTAGE_VALUE { get; set; }
    }
    public class ORG_MASTER
    {
        public int ID { get; set; }
        public string ORG_NAME { get; set; }
        public string ORG_CODE { get; set; }
        public bool STATUS { get; set; }
        public string PAN { get; set; }
        public string CONTACT_PERSON_NAME { get; set; }
        public string CONTACT_PERSON_NO { get; set; }
        public bool IS_GROUP_COMPANIES { get; set; }
        public string SALES_NAME { get; set; }
        public string SALES_CODE { get; set; }
        public string SALES_LOC { get; set; }
        public string SALES_EFFECTIVE_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public string BRANCH { get; set; }
        public string BANK { get; set; }
        public List<CUSTOMER_BRANCH> BRANCH_LIST { get; set; } = new List<CUSTOMER_BRANCH>();
        public List<CUSTOMER_BANK> BANK_LIST { get; set; } = new List<CUSTOMER_BANK>();
    }
    public class SLOT_MASTER
    {
        public int ID { get; set; }
        public string SLOT_OPERATOR { get; set; }
        public string SERVICES { get; set; }
        public string LINER_CODE { get; set; }
        public string PORT_CODE { get; set; }
        public string TERM { get; set; }
        public bool STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }

        public string ADDRESS { get; set; }

        public string CONTACT { get; set; }

        public string EMAIL { get; set; }

        public string SLOT_CODE { get; set; }
    }
    public class CHARGES_MASTER
    {
        public int ID { get; set; }

        public string CHARGE_NAME { get; set; }

        public string CHARGE_HEADER { get; set; }

        public string APPLICABLE_FOR { get; set; }

        public int GST_PERCENTAGE { get; set; }

        public string CURRENCY { get; set; }

        public string HSN_CODE { get; set; }

        public int CHARGE_AMOUNT { get; set; }

        public string CHARGE_TYPE { get; set; }

        public bool IS_GST { get; set; }
    }
    public class HSN_MASTER
    {
        public int ID { get; set; }
        public string HSN_CODE { get; set; }
        public string HSN_DESC { get; set; }
        public string CREATED_BY { get; set; }
        public decimal RATE { get; set; }
        public decimal IGST { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public string EFFECTIVE_FROM { get; set; }
        public string EFFECTIVE_TO { get; set; }
    }

    public class IAL
    {
        public List<IALLIST> CUSTOMER_LIST { get; set; }
    }
    public class IALLIST
    {
        public string CONTAINER_NO { get; set; }
        public string SEAL_NO { get; set; }
        public decimal GROSS_WEIGHT { get; set; }
        public string BL_NO { get; set; }
        public string PORT_OF_LOADING { get; set; }
        public string IMO_CLASS { get; set; }
        public string UN_NO { get; set; }
        public string TEMPERATURE { get; set; }
    }
    public class EAL
    {
        public List<EALLIST> CUSTOMER_LISTS { get; set; }
    }
    public class EALLIST
    {
        public string CONTAINER_NO { get; set; }
        public string SEAL_NO { get; set; }
        public string ISO_CODE { get; set; }
        public string CATEGORY { get; set; }
        public decimal GROSS_WEIGHT { get; set; }
        public string PORT_OF_LOADING { get; set; }
        public string PORT_OF_DISCHARGE { get; set; }
        public string FPD { get; set; }
        public string IB_CARR { get; set; }
        public string IMO_CLASS { get; set; }
        public string UN_NO { get; set; }
        public string TEMPERATURE { get; set; }
        public string CONSIGNEE { get; set; }
        public string SHIPPER { get; set; }
        public string DO_NO { get; set; }
    }
    public class SLOT_RATE_MASTER
    {
        public int ID { get; set; }
        public string SLOT_OPERATOR_NAME { get; set; }
        public string SERVICE { get; set; }
        public string SERVICE_MODE { get; set; }
        public string LADEN_STATUS { get; set; }
        public string POL { get; set; }
        public string POD { get; set; }
        //public decimal SLOT_RATE { get; set; }
        public string CURRENCY { get; set; }
        public decimal RATE20 { get; set; }
        public decimal RATE40 { get; set; }
        public decimal HC20 { get; set; }
        public decimal RF20 { get; set; }
        public decimal HAZ20 { get; set; }
        public decimal HC40 { get; set; }
        public decimal RF40 { get; set; }
        public decimal HAZ40 { get; set; }
        public string FROM_DATE { get; set; }
        public string TO_DATE { get; set; }

    }

    public class SLOT_RATE_HISTORY_MASTER
    {
        public int ID { get; set; }
        public int SLOT_OPERATOR_ID { get; set; }
        public decimal PREVIOUS_RATE20 { get; set; }
        public decimal PREVIOUS_HAZ20 { get; set; }
        public decimal PREVIOUS_RF20 { get; set; }
        public string FROM_DATE { get; set; }

        public DateTime UPDATE_TIMESTAMP { get; set; }

    }

    public class FLEET_COMPOSITION_REPORT
    {
        //public int ID { get; set; }
        //public string LOCATION { get; set; }
        //public string LEASED_FROM { get; set; }
        //public int Total_20GP { get; set; }
        //public int Total_20HC { get; set; }
        //public int Total_40GP { get; set; }
        //public int Total_40HC { get; set; }
        //public int Total_45DHC { get; set; }

        // public int TOTAL { get; set; }
        public IDictionary<string, object> DynamicColumns { get; set; }

    }

    public class STOCK_POSITION_REPORT
    {
        public string LOCATION { get; set; }
        //public string CONTAINER_TYPE { get; set; }
        //public int IMPORT_DISCHARGE { get; set; }
        //public int IMPORT_CONSIGNEE { get; set; }
        //public int EXPORT_SHIPPER { get; set; }
        //public int EXPORT_TERMINAL { get; set; }
        //public int EXPORT_ONBOARD { get; set; }
        //public int EMPTY_AVAILABE { get; set; }
        //public int EMPTY_DAMAGE { get; set; }


        public int IMPORT_DISCHARGE_20_GP { get; set; }
        public int IMPORT_DISCHARGE_40_GP { get; set; }
        public int IMPORT_DISCHARGE_40_RF { get; set; }

        public int IMPORT_CONSIGNEE_20_GP { get; set; }
        public int IMPORT_CONSIGNEE_40_GP { get; set; }
        public int IMPORT_CONSIGNEE_40_RF { get; set; }

        public int EXPORT_SHIPPER_20_GP { get; set; }
        public int EXPORT_SHIPPER_40_GP { get; set; }
        public int EXPORT_SHIPPER_40_RF { get; set; }

        public int EXPORT_TERMINAL_20_GP { get; set; }
        public int EXPORT_TERMINAL_40_GP { get; set; }
        public int EXPORT_TERMINAL_40_RF { get; set; }

        public int EXPORT_ONBOARD_20_GP { get; set; }
        public int EXPORT_ONBOARD_40_GP { get; set; }
        public int EXPORT_ONBOARD_40_RF { get; set; }

        public int EMPTY_AVAILABE_20_GP { get; set; }
        public int EMPTY_AVAILABE_40_GP { get; set; }
        public int EMPTY_AVAILABE_40_RF { get; set; }

        public int EMPTY_DAMAGE_20_GP { get; set; }
        public int EMPTY_DAMAGE_40_GP { get; set; }
        public int EMPTY_DAMAGE_40_RF { get; set; }
        public int TOTAL_TEUS { get; set; }



        //public IDictionary<string, object> DynamicColumns { get; set; }


    }

    public class CONTAINER_TURN_TIME_REPORT
    {
        //public string LOCATION { get; set; }
        //public string CONTAINER_TYPE { get; set; }
        //public string CONTAINER_NO { get; set; }
        //public string ACTIVITY { get; set; }
        //public DateTime ACTIVITY_DATE { get; set; }
        //public int DAYS_TO_NEXT_ACTIVITY { get; set; }
        //public int EMPTY_ACTIVITY { get; set; }

        public string LOCATION { get; set; }
        public int DCHF_20_GP { get; set; }
        public int DCHF_40_GP { get; set; }
        public int DCHF_45_GP { get; set; }

        public int SNTC_20_GP { get; set; }
        public int SNTC_40_GP { get; set; }
        public int SNTC_45_GP { get; set; }

        public int LODE_20_GP { get; set; }
        public int LODE_40_GP { get; set; }
        public int LODE_45_GP { get; set; }

        public int SHOB_20_GP { get; set; }
        public int SHOB_40_GP { get; set; }
        public int SHOB_45_GP { get; set; }

        public int RCCN_20_GP { get; set; }
        public int RCCN_40_GP { get; set; }
        public int RCCN_45_GP { get; set; }

        public int SNTS_20_GP { get; set; }
        public int SNTS_40_GP { get; set; }
        public int SNTS_45_GP { get; set; }

        public int RCFL_20_GP { get; set; }
        public int RCFL_40_GP { get; set; }
        public int RCFL_45_GP { get; set; }

        public int LODF_20_GP { get; set; }
        public int LODF_40_GP { get; set; }
        public int LODF_45_GP { get; set; }

        public int Empty_Repo_Turn_Time { get; set; }

    }

    public class CONTAINER_REPAIRS
    {
        public string LOCATION { get; set; }
        public string CONTAINER_TYPE { get; set; }
        public string CONTAINER_NO { get; set; }
        public decimal FINAL_TOTAL { get; set; }
        public decimal WEAR_TEAR { get; set; }
        public decimal FRESH_DAMAGE { get; set; }



    }


    public class SRR_LIST
    {
        public string LOCATION { get; set; }
        public string Agent { get; set; }
        public int SRR_Received { get; set; }
        public int SRR_Approved { get; set; }
        public int SRR_Countered { get; set; }
        public int SRR_Rejected { get; set; }
        public int SRR_Pending { get; set; }
        public int Volume_Approved { get; set; }
        public int Volume_Executed { get; set; }
    }


    public class DOCS_LIST
    {
        public string LOCATION { get; set; }
        public string ORG_NAME { get; set; }
        public int SRR_APPROVED { get; set; }
        public int TOTAL_BOOKING { get; set; }
        public int TOTAL_CRO { get; set; }
        public int BL_FINALIZED { get; set; }
        public int TOTAL_CONTAINER { get; set; }
    }

    public class AGENT_NAME
    {
        public string ITEM_CODE { get; set; }
        public string CODE_DESC { get; set; }
        public string USER_CODE { get; set; }


    }

    public class SHIPPER_LIST
    {
        public string LOCATION { get; set; }
        public string POD { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string SHIPPER { get; set; }
        public decimal TOTAL_AMOUNT_FREIGHT { get; set; }
        public int TOTAL_CONTAINER { get; set; }
        public decimal FREIGHT_PER_CONTAINER { get; set; }
        public double CONTAINER_PER_FINALIZED_BL { get; set; }
   
    }

    public class CONSIGNEE_LIST
    {
        public string LOCATION { get; set; }
        public string BL_NO { get; set; }
        public int TOTAL_CONTAINER { get; set; }
        public string CONSIGNEE { get; set; }
        public int AVG_FREE_DAYS { get; set; }
        public int TOTAL_TEUS { get; set; }
        public decimal AVG_AMOUNT { get; set; }
    }

    public class DETENATION_LIST
    {
        public string LOCATION { get; set; }
        public string BL_NO { get; set; }
        public string PORT_OF_LOADING { get; set; }
        public string PORT_OF_DISCHARGE { get; set; }
        public string CONTAINER_NO { get; set; }
        public string SHIPPER { get; set; }
        public string CONSIGNEE { get; set; }
        public string COMMODITY_NAME { get; set; }
        public int FREE_DAYS { get; set; }
        public int DETENTION_DAYS { get; set; }
        public decimal DETENTION_AMOUNT { get; set; }

    }

    public class CONSIGNEE_NAME
    {
        public string CODE_DESC { get; set; }

    }

    public class AGENCY_LIST
    {
        public string BL_NO { get; set; }
        public string PORT_OF_LOADING { get; set; }
        public string PORT_OF_DISCHARGE { get; set; }
        public string FINAL_DESTINATION { get; set; }
        public string FIRST_CARRIER { get; set; }
        public string ETD_POL { get; set; }
        public string SECOND_CARRIER { get; set; }
        public string ETD_POLS { get; set; }


    }

    public class SALES_LIST
    {
        public string LOCATION { get; set; }
        public int GP20 { get; set; }
        public string TOTAL_TEUS { get; set; }

    }


    public class SERVICE_NAME
    {
        public string CODE_DESC { get; set; }

    }

    public class VOLUME_LIST
    {
        public string LOCATION { get; set; }
        public string SERVICE { get; set; }
        public string VESSEL_VOYAGE_NO { get; set; }
        public DateTime ETD { get; set; }
        public int GP20_CONATINER { get; set; }
        public int GP40_CONATINER { get; set; }
        public int TOTAL_TEUS { get; set; }
        public int FLEXI { get; set; }
        public int REEFER { get; set; }
        public int HAZ { get; set; }

    }

    public class VESSEL_VOYAGE
    {
        public string SHIPPER { get; set; }
        public string FORWARDER { get; set; }
        public string POL { get; set; }
        public string POD { get; set; }
        public string VESSEL_NAME { get; set; }
        public string VOYAGE_NO { get; set; }
        public int GP20_CONTAINER { get; set; }
        public string COMMODITY_NAME { get; set; }
        public decimal TOTAL_FREIGHT { get; set; }
        public string TOTAL_PREPAID { get; set; }
        public string ETD { get; set; }
        public int POL_FREE_DAYS { get; set; }
        public string BL_ISSUE_PLACE { get; set; }
        public decimal TOTAL_GROSS_WT { get; set; }
        public decimal GROSS_WT_PER_REQ_QUANTITY { get; set; }

    }

    public class VENDOR_AGREEMENT_LIST
    {
        public int VENDOR_AGREEMENT_ID { get; set; }
        public string AGREEMENT_NO { get; set; }
        public int VENDOR_ID { get; set; }
        public DateTime PROCUREMENT_DATE { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public int EQUIPMENT_TYPE_ID { get; set; }
        public string EQUIPMENT_TYPE { get; set; }
        public int EQUIPMENT_SIZE_ID { get; set; }
        public string CONTAINER_TYPE { get; set; }
        public decimal ON_HIRE_HANDLING { get; set; }
        public decimal OFF_HIRE_HANDLING { get; set; }
        public decimal DPP { get; set; }
        public decimal PICKUP_CREDIT { get; set; }
        public decimal DROP_OFF_CHARGE { get; set; }
        public decimal ANNUAL_DEPRECIATION_IN_PERCENTAGE { get; set; }
        public int RE_DELIVERY_CAP { get; set; }
        public decimal DEPRECIATED_REPLACEMENT_VALUE { get; set; }
        public decimal INSPECTION_CHARGES { get; set; }
        public int CURRENCY_ID { get; set; }
        public string CURRENCY_CODE { get; set; }
        public int MIN_RENTAL_PERIOD_IN_DAYS { get; set; }
        public decimal MIN_RESIDUAL_VALUE_IN_PERCENTAGE { get; set; }
        public decimal PRE_TRIP_INSPECTION_CHARGE { get; set; }
        public decimal POST_TRIP_INSPECTION_CHARGE { get; set; }
        public int REDELIVERY_NOTICE_PERIOD_IN_DAYS { get; set; }
        public decimal PICKUP_CHARGE { get; set; }
        public Boolean IS_ACTIVE { get; set; }
        public int? CREATED_BY { get; set; }
        public DateTime? CREATED_AT { get; set; }
        public int? MODIFIED_BY { get; set; }
        public DateTime? MODIFIED_AT { get; set; }
        public int? DELETED_BY { get; set; }
        public DateTime? DELETED_AT { get; set; }

        //public List<VENDOR_PORT_LIST> VENDOR_PORT_LIST { get; set; } = new List<VENDOR_PORT_LIST>();
        //public List<VENDOR_PICKUP_LOCATION_LIST> VENDOR_PICKUP_LOCATION_LIST { get; set; } = new List<VENDOR_PICKUP_LOCATION_LIST>();
    }

    public class VENDOR_PORT_LIST
    {
        public int VENDOR_AGREEMENT_ID { get; set; }
        public int VENDOR_AGR_PORT_ID { get; set; }
        public int PORT_ID { get; set; }
        public string PORT_CODE { get; set; }
        public Boolean IS_ACTIVE { get; set; }
        public int? CREATED_BY { get; set; }
        public DateTime? CREATED_AT { get; set; }
        public int? MODIFIED_BY { get; set; }
        public DateTime? MODIFIED_AT { get; set; }
        public int? DELETED_BY { get; set; }
        public DateTime? DELETED_AT { get; set; }
    }

    public class VENDOR_PICKUP_LOCATION_LIST
    {
        public int VENDOR_AGREEMENT_ID { get; set; }
        public int VENDOR_PICKUP_LOCATION_ID { get; set; }
        public int LOCATION_ID { get; set; }
        public string LOCATION_CODE { get; set; }
        public Boolean IS_ACTIVE { get; set; }
        public int? CREATED_BY { get; set; }
        public DateTime? CREATED_AT { get; set; }
        public int? MODIFIED_BY { get; set; }
        public DateTime? MODIFIED_AT { get; set; }
        public int? DELETED_BY { get; set; }
        public DateTime? DELETED_AT { get; set; }
    }

    public class EQUIPMENT_TYPE_MASTER
    {
        public int EQUIPMENT_TYPE_ID { get; set; }
        public string EQUIPMENT_TYPE { get; set; }
        public string DESCRIPTION { get; set; }
        public Boolean IS_ACTIVE { get; set; }
        public int? CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public int? MODIFIED_BY { get; set; }
        public DateTime? MODIFIED_AT { get; set; }
        public int? DELETED_BY { get; set; }
        public DateTime? DELETED_AT { get; set; }
    }
}
