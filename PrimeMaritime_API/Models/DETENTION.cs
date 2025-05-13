using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PrimeMaritime_API.Models;

namespace PrimeMaritime_API.Models
{
    public class DETENTION_WAIVER_REQUEST
    {
        public string CONTAINER_NO { get; set; }
        public string PORT_OF_DISCHARGE { get; set; }
        public string CONSIGNEE { get; set; }
        public string CLEARING_PARTY { get; set; }
        public int DETENTION_DAYS { get; set; }
        public decimal DETENTION_RATE { get; set; }
        public string CURRENCY { get; set; }
        public string  REMARK { get; set; }
        public DateTime? return_date { get; set; }
        public string IS_JUMPING { get; set; }
        public string CREATED_BY { get; set; }
        public int POD_FREE_DAYS { get; set; }
        public string BL_NO { get; set; }
        public string DO_NO { get; set; }
        public string STATUS { get; set; }
        public string discount { get; set; }
        public string CONTAINER_TYPE { get; set; }
        public string DETENTION_TYPE { get; set; }

    }

    public class DETENTION
    {
        public string DO_NO { get; set; }
        public string BL_NO { get; set; }
        public List<DETENTION_WAIVER_REQUEST> DETENTION_LIST { get; set; } = new List<DETENTION_WAIVER_REQUEST>();
    }

    public class CONTAINER_DETENTION
    {
        public string CONTAINER_NO { get; set; }
        public string BL_NO { get; set; }
        public string POD_FREE_DAYS { get; set; }
        public string VESSEL_NAME { get; set; }
        public string DCHF_DATE { get; set; }
        public string RCCN_DATE { get; set; }
        public decimal POL_DETENTION { get; set; }
        public decimal POD_DETENTION { get; set; }
        public string RCCN_MONTH { get; set; }
    }

    public class DO_DETENTION_DETAILS 
    {

        public string BL_NO { get; set; }
        public string DO_NO { get; set; }
        public string PORT_OF_DISCHARGE { get; set; }
        public string CONSIGNEE { get; set; }
        public string CLEARING_PARTY { get; set; }
        public int POD_FREE_DAYS { get; set; }
        public int POL_FREE_DAYS { get; set; }
        public DateTime DO_VALIDITY { get; set; }
        public DateTime LETTER_VALIDITY { get; set; }
        public string ACCEPTANCE_LOCATION { get; set; }
        public Boolean is_detention { get; set; }
        public string PORT_OF_LOADING { get; set; }
        public string VOYAGE_NO { get; set; }
        public string VESSEL_NAME { get; set; }

        public List<CONTAINERS> CONTAINER_LIST { get; set; } = new List<CONTAINERS>();

    }

    public class DETENTION_CHARGES 
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


}
