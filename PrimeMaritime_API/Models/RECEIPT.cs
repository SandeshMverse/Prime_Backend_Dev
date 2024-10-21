using System;
using System.Collections.Generic;

namespace PrimeMaritime_API.Models
{
    public class RECEIPT
    {
        public List<RECEIPT_INVOICE> INVOICE_LIST { get; set; } = new List<RECEIPT_INVOICE>();
        public List<RECEIPT_BANK> BANK_LIST { get; set; } = new List<RECEIPT_BANK>();
        public List<RECEIPT_CHARGES> CHARGE_LIST { get; set; } = new List<RECEIPT_CHARGES>();
    }
    public class RECEIPT_INVOICE
    {
        public int ID { get; set; }
        public string RECEIPT_NO { get; set; }
        public string INVOICE_NO { get; set; }
        public decimal INVOICE_AMOUNT { get; set; }
        public decimal OUTSTANDING_AMOUNT { get; set; }
        public decimal RECEIVED_AMOUNT { get; set; }
        public string DEPOSIT_CASH_BANK { get; set; }
        public string RECEIPT_REMARKS { get; set; }
        public string AGENT_CODE { get; set; }
        public string AGENT_NAME { get; set; }
    }
    public class RECEIPT_BANK
    {
        public string RECEIPT_NO { get; set; }
        public string BANK_NAME { get; set; }
        public string INS_TYPE { get; set; }
        public string INS_NO { get; set; }
        public DateTime INS_DATE { get; set; }
        public decimal INS_AMOUNT { get; set; }
        public DateTime DEPOSIT_DATE { get; set; }
        public string BANK_LOCATION { get; set; }
    }
    public class RECEIPT_CHARGES
    {
        public string RECEIPT_NO { get; set; }
        public string CHARGE_NAME { get; set; }      
        public decimal INVOICE_AMOUNT { get; set; }
        public decimal RECEIPT_COLLECTED { get; set; }
        public decimal OUTSTANDING_AMOUNT { get; set; }
        public decimal RECEIPT_AMOUNT { get; set; }
    }
}
