using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Models
{
    public class BL
    {
        public int ID { get; set; }
        public string BL_NO { get; set; }
        public string BOOKING_NO { get; set; }
        public string CRO_NO { get; set; }
        public int SRR_ID { get; set; }
        public string SRR_NO { get; set; }
        public string SHIPPER { get; set; }
        public string SHIPPER_ADDRESS { get; set; }
        public string CONSIGNEE { get; set; }
        public string CONSIGNEE_ADDRESS { get; set; }
        public string NOTIFY_PARTY { get; set; }
        public string NOTIFY_PARTY_ADDRESS { get; set; }
        public string PRE_CARRIAGE_BY { get; set; }
        public string PLACE_OF_RECEIPT { get; set; }
        public string VESSEL_NAME { get; set; }
        public string VOYAGE_NO { get; set; }
        public string? MOTHER_VESSEL_NAME { get; set; }
        public string? MOTHER_VOYAGE_NO { get; set; }
        public string? LINE_ITEM_NO { get; set; }
        public string PORT_OF_LOADING { get; set; }
        public string PORT_OF_DISCHARGE { get; set; }
        public string PLACE_OF_DELIVERY { get; set; }
        public string FINAL_DESTINATION { get; set; }
        public string PREPAID_AT { get; set; }
        public string PAYABLE_AT { get; set; }
        public string BL_ISSUE_PLACE { get; set; }
        public DateTime? BL_ISSUE_DATE { get; set; }
        public decimal TOTAL_PREPAID { get; set; }
        public int NO_OF_ORIGINAL_BL { get; set; }
        public string BL_STATUS { get; set; }
        public string BL_TYPE { get; set; }
        public string OG_TYPE { get; set; }
        public int? OGView { get; set; }
        public int? NNView { get; set; }
        public bool? IS_SURRENDERED { get; set; }
        public bool? IS_UPLOADED { get; set; }
        public string PAYMENT_TERM { get; set; }
        public string MARKS_NOS { get; set; }
        public string DESC_OF_GOODS { get; set; }
        public string AGENT_CODE { get; set; }
        public string AGENT_NAME { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public List<CONTAINERS> CONTAINER_LIST { get; set; } = new List<CONTAINERS>();
        public List<BL> BL_LIST { get; set; } = new List<BL>();
        public List<CONTAINERS> CONTAINER_LIST2 { get; set; } = new List<CONTAINERS>();
        public string CONTAINERS { get; set; }
        public string COMMODITY { get; set; }
        public string DESTINATION_AGENT_CODE { get; set; }
        public bool ISGROSSCOMBINED { get; set; }
        public string POL1 { get; set; }
        public string POD1 { get; set; }
        public string CARGO_MOVEMENT { get; set; }
        public string IS_SWITCHBL { get; set; } //SWITCHBL
        public string SWITCHBL_AGENT_CODE { get; set; } //SWITCHBL
        public bool SWITCHBL_STATUS { get; set; } //SWITCHBL
        public bool IS_POL { get; set; } //SWITCHBL
        public string SLOT_OPERATOR_NAME { get; set; }
        public decimal TOTAL_RATE20 { get; set; }
        public decimal EXPORT_DRY20 { get; set; }
        public decimal IMPORT_DRY20 { get; set; }
        public string EXPORT_DRY20_CURRENCY { get; set; }
        public string IMPORT_DRY20_CURRENCY { get; set; }
        public decimal IMPORT_DRY20_BGT20 { get; set; }
        public string M2_IMPORT_BGT20_CURRENCY { get; set; }
        public decimal PODCOM { get; set; }
        public decimal POLCOM { get; set; }
        public decimal Monitor_Charge { get; set; }
        public decimal Export_Detention_Charges { get; set; }
        public string POD_COM_TYPE { get; set; }
        public string POL_COM_TYPE { get; set; }
        public string POL_LOC_CURR { get; set; }
        public string POD_LOC_CURR { get; set; }
        public decimal PODCOMTAX { get; set; }
        public decimal POLCOMTAX { get; set; }
        public string CurrencyRatePairs { get; set; }
        public string DestinationCurrencyRatePairs { get; set; }
        public int BL_ID { get; set; } //SWITCHBL ADDED
        public bool PARENTBL_STATUS { get; set; } //SWITCHBL ADDE
        public DateTime ARRIVAL_DATE { get; set; }
        public int NO_OF_PACKAGES { get; set; }
        public int POD_FREE_DAYS { get; set; }
        public string AGENT_ORG_NAME { get; set; }
        public string isActivity { get; set; }
        public string activity { get; set; }
        public string CONTAINER_NO_ACTIVITY { get; set; }
        public string isEXC { get; set; }

    }

    public class CONTAINERS
    {
        public int ID { get; set; }
        public string BOOKING_NO { get; set; }
        public string CRO_NO { get; set; }

        public string? CRO_NO_MERGE { get; set; }
        public string BL_NO { get; set; }
        public string DO_NO { get; set; }
        public string CONTAINER_NO { get; set; }
        public string CONTAINER_TYPE { get; set; }
        public int CONTAINER_SIZE { get; set; }
        public string SEAL_NO { get; set; }
        public string AGENT_SEAL_NO { get; set; }
        public string MARKS_NOS { get; set; }
        public string DESC_OF_GOODS { get; set; }
        public decimal GROSS_WEIGHT { get; set; }
        public string MEASUREMENT { get; set; }
        public string AGENT_CODE { get; set; }
        public string AGENT_NAME { get; set; }
        public string CREATED_BY { get; set; }
        public string TO_LOCATION { get; set; }
        public DateTime MOVEMENT_DATE { get; set; }
        public DateTime RCFL_ACTIVITY_DATE { get; set; }
        public DateTime DCHF_ACTIVITY_DATE { get; set; }
        public DateTime SNTS_ACTIVITY_DATE { get; set; }
        public int PKG_COUNT { get; set; }
        public string PKG_DESC { get; set; }
        public decimal NET_WEIGHT { get; set; }
        public DateTime BL_ISSUE_DATE { get; set; }
        public decimal LOCAL_DETENTION_RATE { get; set; }
        public string LOC_CURR { get; set; }
    }
    public class Organisation
    {
        public string ORG_NAME { get; set; }
        public string ORG_ADDRESS1 { get; set; }
        public string ORG_ADDRESS2 { get; set; }
        public string CONTACT { get; set; }
        public string EMAIL { get; set; }
        public string FAX { get; set; }

    }

    public class ONLYBL
    {
        public string BL_NO { get; set; }
        public string AGENT_CODE { get; set; }
        public bool SWITCHBL_STATUS { get; set; } 

    }
}
