using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Response
{
    public class CargoManifest
    {
        public List<BL_CUSTOMERLIST> CUSTOMER_LIST { get; set; }
        public List<BL_CONTAINERS> CONTAINER_LIST { get; set; }
        public List<FREIGHT_DETAILS> FREIGHT_DETAILS { get; set; }
        public List<SUMMARY> REPORT_SUMMARY { get; set; }

    }

    public class BL_CUSTOMERLIST
    {

        public string KEY1 { get; set; } //BL_NO_DATE
        public string KEY2 { get; set; } // SHIPPER
        public string KEY3 { get; set; } // CONSIGNEE
        public string KEY4 { get; set; } // NOTIFY
        public string KEY5 { get; set; } // MARKS_NOS
        public string KEY6 { get; set; } // DESC_OF_GOODS
        public decimal KEY7 { get; set; } // TOTAL_GROSS_WT
        public decimal KEY8 { get; set; } // TOTAL_NET_WT
        public int KEY9 { get; set; } // TOTAL_PACKAGE
        public string KEY10 { get; set; } // SERVICE_MODE(CY/CY)
        public string KEY11 { get; set; } // EMPTY VALUE(VOLUME)
        public int KEY12 { get; set; } // 20FT
        public int KEY13 { get; set; } // 40FT
        public int ID { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public DateTime? BL_ISSUE_DATE { get; set; }
        public string CONSIGNEE_ADDRESS { get; set; }
        public string VESSEL_NAME { get; set; }
        public string VOYAGE_NO { get; set; }
        public string SERVICE_NAME { get; set; }
        public string PLACE_OF_RECEIPT { get; set; }
        public string POL { get; set; }
        public string BL_NO { get; set; }
        public string POD { get; set; }
        public string PLACE_OF_DELIVERY { get; set; }
        public string NOTIFY_PARTY { get; set; }


        //public string KEY1 { get; set; } //BL_NO_DATE
        //public string KEY2 { get; set; } // SHIPPER
        //public string KEY3 { get; set; } // CONSIGNEE
        //public string KEY4 { get; set; } // NOTIFY
        //public string KEY5 { get; set; } // MARKS_NOS
        //public string KEY6 { get; set; } // DESC_OF_GOODS
        //public decimal KEY7 { get; set; } // GROSS_WT
        //public string KEY8 { get; set; } // SERVICE_MODE
        //public string KEY9 { get; set; } // EMPTY VALUE
        //public int KEY10 { get; set; } // 20FT
        //public int KEY11 { get; set; } // 40FT
        //public decimal KEY12 { get; set; } // NET_WT
        //public int ID { get; set; }
        //public string CUSTOMER_NAME { get; set; }
        //public string CONSIGNEE_ADDRESS { get; set; }
        //public string VESSEL_NAME { get; set; }
        //public string VOYAGE_NO { get; set; }
        //public string SERVICE_NAME { get; set; }
        //public string PLACE_OF_RECEIPT { get; set; }
        //public string POL { get; set; }
        //public string BL_NO { get; set; }
        //public string POD { get; set; }
        //public string PLACE_OF_DELIVERY { get; set; }
        //public string NOTIFY_PARTY { get; set; }
    }

    public class BL_CONTAINERS
    {

        public string KEY1 { get; set; } // CONTAINER_NO
        public string KEY2 { get; set; } // CONTAINER_SIZE
        public string KEY3 { get; set; } // CONTAINER_TYPE
        public string KEY4 { get; set; } // SEAL_NO
        public string KEY5 { get; set; } // NO_OF_PACKAGES + PACKAGES
        public decimal KEY6 { get; set; } // GROSS_WEIGHT
        public decimal KEY7 { get; set; } // NET_WEIGHT
        public string KEY8 { get; set; } // EMPTY VALUE(VOLUME)
        public string KEY9 { get; set; } // IMO_CLASS
        public string KEY10 { get; set; } // UN_NO
        public string KEY11 { get; set; } // TEMPERATURE
        public string KEY12 { get; set; } // EMPTY VALUE(SOC)
        public string KEY13 { get; set; } // EMPTY VALUE
        public decimal RATE20 { get; set; } // EMPTY VALUE
        public int ID { get; set; }
        public string BL_NO { get; set; }
        public string STATUS { get; set; }


        //public string KEY1 { get; set; } // CONTAINER_NO
        //public string KEY2 { get; set; } // CONTAINER_TYPE
        //public string KEY3 { get; set; } // CONTAINER_TYPE
        //public string KEY4 { get; set; } // SEAL_NO
        //public string KEY5 { get; set; } // NO_OF_PACKAGES + PACKAGES
        //public decimal KEY6 { get; set; } // GROSS_WEIGHT
        //public string KEY7 { get; set; } // EMPTY VALUE
        //public string KEY8 { get; set; } // IMO_CLASS
        //public string KEY9 { get; set; } // UN_NO
        //public string KEY10 { get; set; } // TEMPERATURE
        //public string KEY11 { get; set; } // EMPTY VALUE
        //public int KEY12 { get; set; } // TOTAL_PACKAGE
        //public int ID { get; set; }
        //public string BL_NO { get; set; }
        //public string STATUS { get; set; }
    }

    public class FREIGHT_DETAILS
    {
        public string KEY1 { get; set; } // CHARGE_CODE
        public string KEY2 { get; set; } // EMPTY VALUE
        public string KEY3 { get; set; } // CURRENCY
        public decimal KEY4 { get; set; } // STANDARD_RATE
        public string KEY5 { get; set; } // TRANSPORT_TYPE
        public decimal KEY6 { get; set; } // APPROVED_RATE
        public string KEY7 { get; set; } // EMPTY VALUE
        public string KEY8 { get; set; } // EMPTY VALUE
        public string KEY9 { get; set; } // EMPTY VALUE
        public string KEY10 { get; set; } // EMPTY VALUE
        public string KEY11 { get; set; } // EMPTY VALUE
        public string KEY12 { get; set; } // EMPTY VALUE
        public string KEY13 { get; set; } // EMPTY VALUE
        public int ID { get; set; }
        public string BL_NO { get; set; }

        //public string KEY1 { get; set; } // CHARGE_CODE
        //public string KEY2 { get; set; } // EMPTY VALUE
        //public string KEY3 { get; set; } // CURRENCY
        //public decimal KEY4 { get; set; } // STANDARD_RATE
        //public string KEY5 { get; set; } // TRANSPORT_TYPE
        //public decimal KEY6 { get; set; } // APPROVED_RATE
        //public string KEY7 { get; set; } // EMPTY VALUE
        //public string KEY8 { get; set; } // EMPTY VALUE
        //public string KEY9 { get; set; } // EMPTY VALUE
        //public string KEY10 { get; set; } // EMPTY VALUE
        //public string KEY11 { get; set; } // EMPTY VALUE
        //public string KEY12 { get; set; } // EMPTY VALUE
        //public int ID { get; set; }
        //public string BL_NO { get; set; }
    }

    public class SUMMARY
    {
        public string KEY17 { get; set; }   //CHARGE_CODE
        public string KEY18 { get; set; } //CURRENCY
        public decimal KEY19 { get; set; }  //APPROVED_RATE
        public string KEY20 { get; set; }  //PAYMENT_TERM
    }
}
