using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Models
{
    public class INVOICE_BL
    {
        public string BL_NO { get; set; }
        public string SHIPPER { get; set; }
        public string ORG_NAME { get; set; }
        public string ORG_ADDRESS1 { get; set; }
        public string STATE { get; set; }
        public List<INVOICE_BL_CHARGE> FREIGHT { get; set; } = new List<INVOICE_BL_CHARGE>();
        public List<INVOICE_BL_CHARGE> POL { get; set; } = new List<INVOICE_BL_CHARGE>();
        public List<INVOICE_BL_CHARGE> POD { get; set; } = new List<INVOICE_BL_CHARGE>();
        public List<INVOICE_BL_CONTAINER> CONTAINERS { get; set; } = new List<INVOICE_BL_CONTAINER>();
        public List<INVOICE_BL_BRANCH> BRANCH { get; set; } = new List<INVOICE_BL_BRANCH>();
        public List<INVOICE_BL_BANK> BANK { get; set; } = new List<INVOICE_BL_BANK>();
        public List<INVOICE_RATE_CHECK> RATECHECK { get; set; } = new List<INVOICE_RATE_CHECK>();

    }

    public class INVOICE_BL_CHARGE
    {
        public string CHARGE_CODE { get; set; }
        public string CURRENCY { get; set; }
        public string PAYMENT_TERM { get; set; }
        public decimal STANDARD_RATE { get; set; }
        public decimal RATE_REQUESTED { get; set; }
        public decimal APPROVED_RATE { get; set; }
        public string HSN_CODE { get; set; }
        public decimal RATE { get; set; }
        public decimal IGST { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public string CHARGE_TYPE { get; set; }

        public decimal VAT { get; set; }
    }

    public class INVOICE_MASTER
    {
        public int ID { get; set; }
        public int INVOICE_ID { get; set; }
        public string INVOICE_NO { get; set; }
        public string INVOICE_TYPE { get; set; }
        public string BILL_TO { get; set; }
        public string BILL_FROM { get; set; }
        public string SHIPPER_NAME { get; set; }
        public string CONSIGNEE_NAME { get; set; }
        public string PAYMENT_TERM { get; set; }
        public string BL_NO { get; set; }
        public string AGENT_NAME { get; set; }
        public string AGENT_CODE { get; set; }
        public string CREATED_BY { get; set; }
        public string UPDATED_BY { get; set; }
        public string STATUS { get; set; }
        public string ADDRESS { get; set; }
        public string ORG_NAME { get; set; }
        public string ORG_ADDRESS1 { get; set; }
        public DateTime INVOICE_DATE { get; set; }
        public string TAX_NO { get; set; }
        public string PAN { get; set; }
        public string PLACE_OF_RECEIPT { get; set; }
        public string PLACE_OF_DELIVERY { get; set; }
        public string PORT_OF_LOADING { get; set; }
        public string PORT_OF_DISCHARGE { get; set; }
        public string VESSEL_NAME { get; set; }
        public string VOYAGE_NO { get; set; }
        public string CONTAINERS { get; set; }
        public string CONTAINER_NOS { get; set; }
        public int BRANCH_ID { get; set; }
        public int BANK_ID { get; set; }
        public string SHIPPER_REF { get; set; }
        public string FINAL_DESTINATION { get; set; }
        public string REMARKS { get; set; }
        public string STATE { get; set; }

        public decimal EX_RATE { get; set; }   //NEW ADDED
        public List<INVOICE_CHARGES> BL_LIST { get; set; } = new List<INVOICE_CHARGES>();
        public List<INVOICE_BL_CONTAINER> CONTAINER_LIST { get; set; } = new List<INVOICE_BL_CONTAINER>();
        public List<INVOICE_BL_CONTAINER> BL_CONTAINER_LIST { get; set; } = new List<INVOICE_BL_CONTAINER>();
        public List<INVOICE_BL_BRANCH> BRANCH { get; set; } = new List<INVOICE_BL_BRANCH>();
        public List<INVOICE_BL_BANK> BANK { get; set; } = new List<INVOICE_BL_BANK>();
        public List<INVOICE_BL_BRANCH> SELECTED_BRANCH { get; set; } = new List<INVOICE_BL_BRANCH>();
        public List<INVOICE_RATE_CHECK> RATECHECK { get; set; } = new List<INVOICE_RATE_CHECK>();
    }

    public class INVOICE_CHARGES
    {
        public int ID { get; set; }
        public int INVOICE_ID { get; set; }
        public string INVOICE_NO { get; set; }
        public string CHARGE_NAME { get; set; }
        public decimal EXCHANGE_RATE { get; set; }
        public decimal NEW_EXCHANGE_RATE { get; set; }     //NEW ADDED
        public int QUANTITY { get; set; }
        public decimal AMOUNT { get; set; }
        public string HSN_CODE { get; set; }
        public string CURRENCY { get; set; }
        public string EXEMPT_FLAG { get; set; }
        public bool IS_SRRCHARGE { get; set; }
        public decimal RATE_PER { get; set; }
        public decimal IGST { get; set; }
        public decimal SGST { get; set; }
        public decimal CGST { get; set; }

        public decimal VAT { get; set; }
        public decimal VAT_AMOUNT { get; set; }
        public decimal APPROVED_RATE { get; set; }
        public decimal TAXABLE_AMOUNT { get; set; }
        public decimal TAX_AMOUNT { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
        public string CHARGE_TYPE { get; set; }
        public decimal REMAINING_AMOUNT { get; set; }
        public decimal REMAINING { get; set; }
        public decimal CREDIT_AMOUNT { get; set; }
        public string CREDIT_NO { get; set; }
        public string AGENT_CODE { get; set; }
        public string AGENT_NAME { get; set; }
    }
    public class CREDIT_NOTE
    {
        public string CREDIT_NO { get; set; }
        public string INVOICE_NO { get; set; }
        public string CHARGE_NAME { get; set; }
        public decimal REMAINING_AMOUNT { get; set; }
        public decimal CREDIT_AMOUNT { get; set; }
        public string AGENT_CODE { get; set; }
        public string AGENT_NAME { get; set; }
        public DateTime? CREATED_DATE { get; set; }
    }
    public class INVOICE_BL_CONTAINER
    {
        public int ID { get; set; }
        public string CONTAINER_NO { get; set; }
    }

    public class INVOICE_BL_BRANCH
    {
        public int BRANCH_ID { get; set; }
        public int CUST_ID { get; set; }
        public string CUST_NAME { get; set; }
        public string BRANCH_NAME { get; set; }
        public string BRANCH_CODE { get; set; }
        public string COUNTRY { get; set; }
        public int STATECODE { get; set; }
        public string STATE { get; set; }
        public string TAX_NO { get; set; }
        public string TAX_TYPE { get; set; }
        public string ADDRESS { get; set; }
        public bool IS_SEZ { get; set; }
        public bool IS_TAX_APPLICABLE { get; set; }
    }

     public class INVOICE_FINALIZE
    {
        public int INVOICE_ID { get; set; }
        public string INVOICE_NO { get; set; }
    }

    public class INVOICE_BL_BANK
    {
        public int BANK_ID { get; set; }
        public string BRANCH_CODE { get; set; }
        public string BANK_NAME { get; set; }
        public string BANK_ACC_NO { get; set; }
        public string BANK_IFSC { get; set; }
        public string BANK_SWIFT { get; set; }
        public string BANK_REMARKS { get; set; }
    }
    public class INVOICE_BL_CHECK
    {
        public string BL_NO { get; set; }
        public string INVOICE_TYPE { get; set;}
    }

    public class INVOICE_PAYMENT_TERM_CHECK
    {
        public string BL_NO { get; set; }
        public string SRR_NO { get; set; }
        public string  PAYMENT_TERM { get; set; }

    }

    public class BL_FINALIZED
    {
        public string BL_NO { get; set; }
        public string BL_STATUS { get; set; }
        public string AGENT_CODE { get; set; }

    }
    public class CREDIT_NOTE_DETAILS
    {
        public string INVOICE_NO { get; set; }
        public string CREDIT_NO { get; set; }
        public string BILL_TO { get; set; }
        public string ORG_NAME { get; set; }
        public string ORG_ADDRESS1 { get; set; }
        public string SHIPPER_NAME { get; set; }
        public string ADDRESS { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string TAX_NO { get; set; }
        public string BL_NO { get; set; }
        public string PLACE_OF_RECEIPT { get; set; }
        public string VESSEL_NAME { get; set; }
        public string VOYAGE_NO { get; set; }
        public string PORT_OF_LOADING { get; set; }
        public string PORT_OF_DISCHARGE { get; set; }
        public string PLACE_OF_DELIVERY { get; set; }
        public string CONTAINERS { get; set; }
        public List<CREDIT_NOTE_CHARGE_DETAILS> CHARGE_LIST { get; set; } = new List<CREDIT_NOTE_CHARGE_DETAILS>();
    }

    public class CREDIT_NOTE_CHARGE_DETAILS
    {
        public string CREDIT_NO { get; set; }
        public string INVOICE_NO { get; set; }
        public string CHARGE_NAME { get; set; }
        public decimal REMAINING_AMOUNT { get; set; }
        public decimal CREDIT_AMOUNT { get; set; }
        public string HSN_CODE { get; set; }
        public int QUANTITY { get; set; }
        public string CURRENCY { get; set; }
        public decimal EXCHANGE_RATE { get; set; }
        public decimal APPROVED_RATE { get; set; }
        public decimal TAXABLE_AMOUNT { get; set; }
        public decimal RATE_PER { get; set; }
        public decimal SGST { get; set; }
        public decimal CGST { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
    }

    public class INVOICE_DETAILS_FOR_RECEIPT
    {
        public List<INVOICE_DETAILS_FOR_RECEIPT_INVOICES> INVOICE_LIST { get; set; } = new List<INVOICE_DETAILS_FOR_RECEIPT_INVOICES>();
        public List<CUSTOMER_BANK> BANK_LIST { get; set; } = new List<CUSTOMER_BANK>();
        public List<INVOICE_DETAILS_FOR_RECEIPT_CHARGES> CHARGE_LIST { get; set; } = new List<INVOICE_DETAILS_FOR_RECEIPT_CHARGES>();
    }
    public class INVOICE_DETAILS_FOR_RECEIPT_INVOICES
    {
        public int INVOICE_ID { get; set; }
        public string INVOICE_NO { get; set; }
        public string INVOICE_TYPE { get; set; }
        public DateTime INVOICE_DATE { get; set; }
        public string BILL_TO { get; set; }
        public string BILL_FROM { get; set; }
        public string BL_NO { get; set; }
        public string VESSEL_NAME { get; set; }
        public string VOYAGE_NO { get; set; }
        public string SHIPPER { get; set; }
        public string CONSIGNEE { get; set; }
        public string TAN_NO { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
        public string RECEIVED_FROM { get; set; }
        public string DEPOSIT_CASH_BANK { get; set; }
        public string RECEIPT_REMARKS { get; set; }
        public string RECEIPT_NO { get; set; }
        public string AGENT_NAME { get; set; }
        public string AGENT_CODE { get; set; }
        public decimal OUTSTANDING_AMOUNT { get; set; }
        public decimal INVOICE_AMOUNT { get; set; }
        public decimal RECEIVED_AMOUNT { get; set; }
        

    }

    public class INVOICE_DETAILS_FOR_RECEIPT_CHARGES
    {
        public string INVOICE_NO { get; set; }
        public string CHARGE_NAME { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
        public decimal RECEIPT_COLLECTED { get; set; }
        public decimal OUTSTANDING_AMOUNT { get; set; }
        public decimal RECEIPT_AMOUNT { get; set; }
        public string RECEIPT_NO { get; set; }
    }

    //NEW added siddhesh
    public class INVOICE_RATE_CHECK
    {
        public string CURRENCY_CODE { get; set; }
        public decimal RATE { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string IS_NEW_DATE { get; set; }
    }

    public class GET_CUST_LIST
    {
        //public string SHIPPER { get; set; }
        //public string CONSIGNEE { get; set; }
        //public string NOTIFY_PARTY { get; set; }
        //public string CUSTOMER_NAME { get; set; }

        public string KEY_NAME { get; set; }
        public string CODE { get; set; }
        public string CODE_DESC { get; set; }
    }

    public class GET_INVOICE_LIST
    {
        public string INVOICE_NO { get; set; }

    }
}
