﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Models
{
    public class EMPTY_REPO
    {
        public int ID { get; set; }
		public string REPO_NO { get; set; }
		public string CRO_NO { get; set; }
		public DateTime CRO_VALIDITY_DATE { get; set; }
		public string LOAD_DEPOT { get; set; }
		public string DISCHARGE_DEPOT { get; set; }
		public string LOAD_PORT { get; set; }
		public string DISCHARGE_PORT { get; set; }
		public string VESSEL_NAME { get; set; }
		public string VOYAGE_NO { get; set; }
		public DateTime MOVEMENT_DATE { get; set; }
		public decimal LIFT_ON_CHARGE { get; set; }
		public decimal LIFT_OFF_CHARGE { get; set; }
		public string CURRENCY { get; set; }
		public int NO_OF_CONTAINER { get; set; }
		public string MODE_OF_TRANSPORT { get; set; }
		public string REASON { get; set; }
		public string REMARKS { get; set; }
		public int STATUS { get; set; }
		public string AGENT_CODE { get; set; }
		public string AGENT_NAME { get; set; }
		public string DEPO_CODE { get; set; }
		public string CREATED_BY { get; set; }
		public string ORG_NAME { get; set; }
		public string ORG_ADDRESS1 { get; set; }
		public DateTime CREATED_DATE { get; set; }
		public string EMPTY_CONT_PCKP { get; set; }
		public string SERVICE_NAME { get; set; }
		public DateTime ETA { get; set; }
		public DateTime ETD { get; set; }
		public string POL { get; set; }
		public string POD { get; set; }
        public decimal EXPORT_DRY20 { get; set; }
        public decimal IMPORT_DRY20 { get; set; }

        public decimal Monitor_Charge { get; set; }
        public string FINAL_DESTINATION { get; set; }
		public int REQ_QUANTITY { get; set; }
		public List<SLOT_DETAILS> SLOT_LIST { get; set; }
		public List<ER_CONTAINER> CONTAINER_LIST { get; set; } = new List<ER_CONTAINER>();
		public List<ER_RATES> CONTAINER_RATES { get; set; } = new List<ER_RATES>();
	}

}
