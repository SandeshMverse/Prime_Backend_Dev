using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Models
{
    public class DO
    {
        public int ID { get; set; }
		public int BL_ID { get; set; }
		public string BL_NO { get; set; }

		//BL RELATED
		public string SHIPPER { get; set; }
		public string CONSIGNEE { get; set; }
		public string VESSEL_NAME { get; set; }
		public string VOYAGE_NO { get; set; }
		public string PORT_OF_LOADING { get; set; }
		public string PORT_OF_DISCHARGE { get; set; }
		public string PLACE_OF_DELIVERY { get; set; }

		//DO RELATED
		public string DO_NO { get; set; }
		public DateTime? DO_DATE { get; set; }
		public DateTime ARRIVAL_DATE { get; set; }
		public DateTime DO_VALIDITY { get; set; }
		public string IGM_NO { get; set; }
		public string IGM_ITEM_NO { get; set; }
		public DateTime? IGM_DATE { get; set; }
		public string CLEARING_PARTY { get; set; }
		public string ACCEPTANCE_LOCATION { get; set; }
		public DateTime LETTER_VALIDITY { get; set; }
		public string SHIPPING_TERMS { get; set; }
		public string AGENT_CODE { get; set; }
		public string AGENT_NAME { get; set; }
		public string CREATED_BY { get; set; }
		public DateTime CREATED_DATE { get; set; }
		public string POL { get; set; }
		public string POD { get; set; }
		public string CONTAINERS { get; set; }
		public string COMMODITY { get; set; }
		public string DESTINATION_AGENT { get; set; }
		public string ORG_NAME { get; set; }
		public string ORG_ADDRESS1 { get; set; }
		public List<CONTAINERS> CONTAINER_LIST { get; set; } = new List<CONTAINERS>();
        public string DELIVERY_PARTY { get; set; }
        public string LINE_NO { get; set; }
        public string CFS_DETAILS { get; set; }
        public string DO_STATUS { get; set; }
        public bool EDIT_EMPTY_LETTER { get; set; }
        

    }

	public class DODETAILS 
	{
        public int ID { get; set; }
        public int BL_ID { get; set; }
        public string BL_NO { get; set; }
        public string DO_NO { get; set; }
        public DateTime? DO_DATE { get; set; }
        public DateTime ARRIVAL_DATE { get; set; }
        public DateTime DO_VALIDITY { get; set; }
        public string IGM_NO { get; set; }
        public string IGM_ITEM_NO { get; set; }
        public DateTime? IGM_DATE { get; set; }
        public string CLEARING_PARTY { get; set; }
        public string ACCEPTANCE_LOCATION { get; set; }
        public DateTime LETTER_VALIDITY { get; set; }
        public string SHIPPING_TERMS { get; set; }
        public string AGENT_CODE { get; set; }
        public string AGENT_NAME { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string VESSEL_NAME { get; set; }
        public string VOYAGE_NO { get; set; }
        public string DELIVERY_PARTY { get; set; }
        public string LINE_NO { get; set; }
        public string CFS_DETAILS { get; set; }
        public string DO_STATUS { get; set; }



    }

    public class INVOICE_DETAILS_FOR_DO
    {
        public string INVOICE_NO { get; set; }
        public string INVOICE_TYPE { get; set; }
        public string BILL_TO { get; set; }
        public string BILL_FROM { get; set; }
        public string SHIPPER_NAME { get; set; }
        public string CONSIGNEE_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string PAYMENT_TERM { get; set; }
        public string BL_NO { get; set; }
        public DateTime INVOICE_DATE { get; set; }
        public int BRANCH_ID { get; set; }
        public int BANK_ID { get; set; }
        public string CONTAINERS { get; set; }
        public string SHIPPER_REF { get; set; }
        public string REMARKS { get; set; }
        public string AGENT_NAME { get; set; }
        public string AGENT_CODE { get; set; }
        public string STATUS { get; set; }

        public List<INVOICE_CHARGES> ICHARGES { get; set; } = new List<INVOICE_CHARGES>();
        public List<RECEIPT_INVOICE> RECEIPT { get; set; } = new List<RECEIPT_INVOICE>();
        public List<RECEIPT_BANK> RBANK { get; set; } = new List<RECEIPT_BANK>();
        public List<RECEIPT_CHARGES> RCHARGES { get; set; } = new List<RECEIPT_CHARGES>();

    }
}
