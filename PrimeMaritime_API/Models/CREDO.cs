using PrimeMaritime_API.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace PrimeMaritime_API.Models
{
    public class CREDO
    {
        public List<CUSTOMERLIST> CUSTOMER_LIST { get; set; }
        public List<CONTAINER> CONTAINER { get; set; }
    }
    public class CUSTOMERLIST
    { 
        public int ID { get; set; }
        public string BL_NO { get; set; }
        public string VESSEL_NAME { get; set; }
        public string VOYAGE_NO { get; set; }
        public string BL_ISSUE_DATE { get; set; }
        public string BL_STATUS { get; set; }
        public string BL_TYPE { get; set; }
        public string PRE_CARRIAGE_BY { get; set; }
        public string PLACE_OF_RECEIPT { get; set; }
        public decimal GROSS_WEIGHT { get; set; }
        public decimal NET_WEIGHT { get; set; }
        public int NO_OF_ORIGINAL_BL { get; set; }
        public string CARGO_MOVEMENT { get; set; }
        public string SHIPPER { get; set; }
        public string SHIPPER_ADDRESS { get; set; }
        public string CONSIGNEE { get; set; }
        public string CONSIGNEE_ADDRESS { get; set; }
        public string NOTIFY_PARTY { get; set; }
        public string NOTIFY_PARTY_ADDRESS { get; set; }
        public string AGENT_CODE { get; set; }
        public string AGENT_NAME { get; set; }
        public string PORT_OF_LOADING { get; set; }
        public string PORT_OF_DISCHARGE { get; set; }
        public string FINAL_DESTINATION { get; set; }
        public string PLACE_OF_DELIVERY { get; set; }
        public string TOTAL_PREPAID { get; set; }
        public string PAYABLE_AT { get; set; }
        public string BL_ISSUE_PLACE { get; set; }
        public int PACKAGES { get; set; }
        public string PACKAGE_TYPE { get; set; }
        public string DESC_OF_GOODS { get; set; }
        public string MARKS_NOS { get; set; }
        public string CREATED_BY { get; set; }

    }

    public class CONTAINER
    {
   
        public int ID { get; set; }
        public string CONTAINER_NO { get; set; }
        public string CONTAINER_TYPE { get; set; }
        public int CONTAINER_SIZE { get; set; }
        public string BL_NO { get; set; }
        public string VESSEL_NAME { get; set; }
        public string VOYAGE_NO { get; set; }
        public string PORT_OF_LOADING { get; set; }
        public string PORT_OF_DISCHARGE { get; set; }
        public string FINAL_DESTINATION { get; set; }
        public string SEAL_NO { get; set; }
        public decimal WEIGHT { get; set; }
        public string TEMPERATURE { get; set; }
        public string IMO_CLASS { get; set; }
        public string MARKS_NOS { get; set; }
        public int PACKAGES { get; set; }
        public string COMMODITY_TYPE { get; set; }
        public string BL_ISSUE_DATE { get; set; }

        public string AGENT_SEAL_NO { get; set; }
        public string DESC_OF_GOODS { get; set; }
        public decimal GROSS_WEIGHT { get; set; }
        public string MEASUREMENT { get; set; }
        public string AGENT_CODE { get; set; }
        public string AGENT_NAME { get; set; }
        public string CREATED_BY { get; set; }
        public string TO_LOCATION { get; set; }
        public DateTime MOVEMENT_DATE { get; set; }
        public string PACKAGE_TYPE { get; set; }
        public decimal NET_WEIGHT { get; set; }
        public int TOTAL_VOLUME_EXPECTED { get; set; }
        public string PAYABLE_AT { get; set; }

    }

}
