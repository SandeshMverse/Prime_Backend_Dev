using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Models;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;

namespace PrimeMaritime_API.Repository
{
    public class ReceiptRepo
    {
        public void InsertReceipt(string connstring, RECEIPT request)
        {
            try
            {
                //SqlParameter[] parameters =
                //{
                //  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_RECEIPT" },
                //  new SqlParameter("@RECEIPT_NO", SqlDbType.VarChar,100) { Value = request.RECEIPT_NO},
                //  new SqlParameter("@INVOICE_NO", SqlDbType.VarChar,100) { Value = request.INVOICE_NO},
                //  new SqlParameter("@INVOICE_AMOUNT", SqlDbType.Decimal) { Value = request.INVOICE_AMOUNT },
                //  new SqlParameter("@OUTSTANDING_AMOUNT", SqlDbType.Decimal) { Value = request.OUTSTANDING_AMOUNT },
                //  new SqlParameter("@RECEIVED_AMOUNT", SqlDbType.Decimal) { Value = request.RECEIVED_AMOUNT },
                //  new SqlParameter("@DEPOSIT_CASH_BANK", SqlDbType.VarChar, 255) { Value = request.DEPOSIT_CASH_BANK },
                //  new SqlParameter("@RECEIPT_REMARKS", SqlDbType.VarChar, 255) { Value = request.RECEIPT_REMARKS },                 
                //};

                //var ID = SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_RECEIPT", parameters);

                string[] column = new string[9];
                column[0] = "RECEIPT_NO";
                column[1] = "INVOICE_NO";
                column[2] = "INVOICE_AMOUNT";
                column[3] = "OUTSTANDING_AMOUNT";
                column[4] = "RECEIVED_AMOUNT";
                column[5] = "DEPOSIT_CASH_BANK";
                column[6] = "RECEIPT_REMARKS";
                column[7] = "AGENT_CODE";
                column[8] = "AGENT_NAME";
 
                DataTable tb = new DataTable();

                tb.Columns.Add(new DataColumn("RECEIPT_NO", typeof(string)));
                tb.Columns.Add(new DataColumn("INVOICE_NO", typeof(string)));
                tb.Columns.Add(new DataColumn("INVOICE_AMOUNT", typeof(decimal)));
                tb.Columns.Add(new DataColumn("OUTSTANDING_AMOUNT", typeof(decimal)));
                tb.Columns.Add(new DataColumn("RECEIVED_AMOUNT", typeof(decimal)));
                tb.Columns.Add(new DataColumn("DEPOSIT_CASH_BANK", typeof(string)));
                tb.Columns.Add(new DataColumn("RECEIPT_REMARKS", typeof(string)));
                tb.Columns.Add(new DataColumn("AGENT_CODE", typeof(string)));
                tb.Columns.Add(new DataColumn("AGENT_NAME", typeof(string)));

                foreach (var i in request.INVOICE_LIST)
                {
                    DataRow dr = tb.NewRow();

                    dr["RECEIPT_NO"] = i.RECEIPT_NO;
                    dr["INVOICE_NO"] = i.INVOICE_NO;
                    dr["INVOICE_AMOUNT"] = i.INVOICE_AMOUNT;
                    dr["OUTSTANDING_AMOUNT"] = i.OUTSTANDING_AMOUNT;
                    dr["RECEIVED_AMOUNT"] = i.RECEIVED_AMOUNT;
                    dr["DEPOSIT_CASH_BANK"] = i.DEPOSIT_CASH_BANK;
                    dr["RECEIPT_REMARKS"] = i.RECEIPT_REMARKS;
                    dr["AGENT_CODE"] = i.AGENT_CODE;
                    dr["AGENT_NAME"] = i.AGENT_NAME;
                    
                    tb.Rows.Add(dr);
                }

                SqlHelper.ExecuteProcedureBulkInsert(connstring, tb, "TB_RECEIPT", column);

                string[] columns = new string[8];
                columns[0] = "RECEIPT_NO";
                columns[1] = "BANK_NAME";
                columns[2] = "INS_TYPE";
                columns[3] = "INS_NO";
                columns[4] = "INS_DATE";
                columns[5] = "INS_AMOUNT";
                columns[6] = "DEPOSIT_DATE";
                columns[7] = "BANK_LOCATION";

                DataTable tbl = new DataTable();

                tbl.Columns.Add(new DataColumn("RECEIPT_NO", typeof(string)));
                tbl.Columns.Add(new DataColumn("BANK_NAME", typeof(string)));
                tbl.Columns.Add(new DataColumn("INS_TYPE", typeof(string)));
                tbl.Columns.Add(new DataColumn("INS_NO", typeof(string)));
                tbl.Columns.Add(new DataColumn("INS_DATE", typeof(DateTime)));
                tbl.Columns.Add(new DataColumn("INS_AMOUNT", typeof(decimal)));
                tbl.Columns.Add(new DataColumn("DEPOSIT_DATE", typeof(DateTime)));
                tbl.Columns.Add(new DataColumn("BANK_LOCATION", typeof(string)));

                foreach (var i in request.BANK_LIST)
                {
                    DataRow dr = tbl.NewRow();

                    dr["RECEIPT_NO"] = request.INVOICE_LIST[0].RECEIPT_NO;
                    dr["BANK_NAME"] = i.BANK_NAME;
                    dr["INS_TYPE"] = i.INS_TYPE;
                    dr["INS_NO"] = i.INS_NO;
                    dr["INS_DATE"] = i.INS_DATE;
                    dr["INS_AMOUNT"] = i.INS_AMOUNT;
                    dr["DEPOSIT_DATE"] = i.DEPOSIT_DATE;
                    dr["BANK_LOCATION"] = i.BANK_LOCATION;

                    tbl.Rows.Add(dr);
                }

                SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl, "TB_RECEIPT_BANK", columns);

                string[] columns1 = new string[6];
                columns1[0] = "RECEIPT_NO";
                columns1[1] = "CHARGE_NAME";
                columns1[2] = "INVOICE_AMOUNT";
                columns1[3] = "RECEIPT_COLLECTED";
                columns1[4] = "OUTSTANDING_AMOUNT";
                columns1[5] = "RECEIPT_AMOUNT";

                DataTable tbl1 = new DataTable();

                tbl1.Columns.Add(new DataColumn("RECEIPT_NO", typeof(string)));
                tbl1.Columns.Add(new DataColumn("CHARGE_NAME", typeof(string)));
                tbl1.Columns.Add(new DataColumn("INVOICE_AMOUNT", typeof(decimal)));
                tbl1.Columns.Add(new DataColumn("RECEIPT_COLLECTED", typeof(decimal)));
                tbl1.Columns.Add(new DataColumn("OUTSTANDING_AMOUNT", typeof(decimal)));
                tbl1.Columns.Add(new DataColumn("RECEIPT_AMOUNT", typeof(decimal)));

                foreach (var i in request.CHARGE_LIST)
                {
                    DataRow dr = tbl1.NewRow();

                    dr["RECEIPT_NO"] = request.INVOICE_LIST[0].RECEIPT_NO;
                    dr["CHARGE_NAME"] = i.CHARGE_NAME;
                    dr["INVOICE_AMOUNT"] = i.INVOICE_AMOUNT;
                    dr["RECEIPT_COLLECTED"] = i.RECEIPT_COLLECTED;
                    dr["OUTSTANDING_AMOUNT"] = i.OUTSTANDING_AMOUNT;
                    dr["RECEIPT_AMOUNT"] = i.RECEIPT_AMOUNT;

                    tbl1.Rows.Add(dr);
                }

                SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl1, "TB_RECEIPT_CHARGES", columns1);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<RECEIPT_INVOICE> GetReceiptList(string connstring, string FROM_DATE, string TO_DATE, string PORT, string ORG_CODE, string AGENT_CODE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_RECEIPT_LIST" },
                  new SqlParameter("@FROMDATE", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(FROM_DATE) ? null : Convert.ToDateTime(FROM_DATE) },
                  new SqlParameter("@TODATE", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(TO_DATE) ? null : Convert.ToDateTime(TO_DATE) },
                  new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 50) { Value = ORG_CODE },
                  new SqlParameter("@PORT", SqlDbType.VarChar, 100) { Value = PORT },
                  new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 50) { Value = AGENT_CODE },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_RECEIPT", parameters);
                List<RECEIPT_INVOICE> receiptList = SqlHelper.CreateListFromTable<RECEIPT_INVOICE>(dataTable);

                return receiptList;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
