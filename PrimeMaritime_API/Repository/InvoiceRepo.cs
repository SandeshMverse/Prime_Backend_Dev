using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Repository
{
    public class InvoiceRepo
    {
        public DataSet GetBLDetails(string connstring, string BL_NO, string PORT, string ORG_CODE, string AGENT_CODE)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_BL_DETAILS" },
                new SqlParameter("@BL_NO", SqlDbType.VarChar, 100) { Value = BL_NO },
                new SqlParameter("@PORT", SqlDbType.VarChar, 100) { Value = PORT },
                new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 50) { Value = ORG_CODE },
                new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 50) { Value = AGENT_CODE },
            };

            return SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_INVOICE", parameters);
        }
        public DataSet GetCreditNoteDetails(string connstring, string CREDIT_NO, string PORT, string ORG_CODE)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_CREDIT_DETAILS" },
                new SqlParameter("@CREDIT_NO", SqlDbType.VarChar, 100) { Value = CREDIT_NO },
                new SqlParameter("@PORT", SqlDbType.VarChar, 100) { Value = PORT },
                new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 50) { Value = ORG_CODE },
            };

            return SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_INVOICE", parameters);
        }

        public static T GetSingleDataFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateItemFromRow<T>(dataTable.Rows[0]);
        }

        public static List<T> GetListFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateListFromTable<T>(dataTable);
        }

        public void InsertInvoice(string connstring, INVOICE_MASTER master)
        {
            try
            {
                 SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION",    SqlDbType.VarChar,50) { Value = "INSERT_INVOICES" },
                  new SqlParameter("@INVOICE_NO",   SqlDbType.VarChar,100) { Value = master.INVOICE_NO},
                  new SqlParameter("@INVOICE_ID",   SqlDbType.Int) { Value = master.INVOICE_ID},
                  new SqlParameter("@INVOICE_TYPE", SqlDbType.VarChar, 100) { Value = master.INVOICE_TYPE },
                  new SqlParameter("@BILL_TO",      SqlDbType.VarChar, 255) { Value = master.BILL_TO },
                  new SqlParameter("@BILL_FROM",    SqlDbType.VarChar, 255) { Value = master.BILL_FROM },
                  new SqlParameter("@SHIPPER_NAME", SqlDbType.NVarChar, 255) { Value = master.SHIPPER_NAME },
                  new SqlParameter("@CONSIGNEE_NAME", SqlDbType.NVarChar, 255) { Value = master.CONSIGNEE_NAME },
                  new SqlParameter("@PAYMENT_TERM", SqlDbType.VarChar, 50) { Value = master.PAYMENT_TERM },
                  new SqlParameter("@ADDRESS",      SqlDbType.VarChar) { Value = master.ADDRESS },
                  new SqlParameter("@BRANCH_ID",     SqlDbType.Int ) { Value = master.BRANCH_ID },
                  new SqlParameter("@BANK_ID", SqlDbType.Int) { Value = string.IsNullOrEmpty(master.BANK_ID.ToString()) ? (object)DBNull.Value : master.BANK_ID },
                  //new SqlParameter("@BANK_ID",     SqlDbType.Int ) { Value = master.BANK_ID },
                  new SqlParameter("@INVOICE_DATE",  SqlDbType.DateTime ) { Value = master.INVOICE_DATE },
                  new SqlParameter("@BL_NO",        SqlDbType.VarChar, 50) { Value = master.BL_NO },
                  new SqlParameter("@AGENT_NAME",   SqlDbType.VarChar, 50) { Value = master.AGENT_NAME},
                  new SqlParameter("@AGENT_CODE",   SqlDbType.VarChar, 50) { Value = master.AGENT_CODE},
                  new SqlParameter("@CREATED_BY",   SqlDbType.VarChar, 50) { Value = master.CREATED_BY},
                  new SqlParameter("@UPDATED_BY",   SqlDbType.VarChar, 50) { Value = master.UPDATED_BY},
                  new SqlParameter("@STATUS",        SqlDbType.VarChar, 50) { Value = master.STATUS},
                  new SqlParameter("@CONTAINERS",    SqlDbType.VarChar) { Value = master.CONTAINERS},
                  new SqlParameter("@SHIPPER_REF",    SqlDbType.VarChar,100) { Value = master.SHIPPER_REF},
                  new SqlParameter("@REMARKS",    SqlDbType.VarChar,255) { Value = master.REMARKS},
                };

                var ID = SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_INVOICE", parameters);

                string[] columns = new string[21];
                columns[0] = "INVOICE_NO";
                columns[1] = "CHARGE_NAME";
                columns[2] = "EXCHANGE_RATE";
                columns[3] = "QUANTITY";
                columns[4] = "AMOUNT";
                columns[5] = "HSN_CODE";
                columns[6] = "APPROVED_RATE";
                columns[7] = "CURRENCY";
                columns[8] = "IS_SRRCHARGE";
                columns[9] = "INVOICE_ID";
                columns[10] = "ID";
                columns[11] = "TAXABLE_AMOUNT";
                columns[12] = "RATE_PER";
                columns[13] = "SGST";
                columns[14] = "CGST";
                columns[15] = "IGST";
                columns[16] = "TAX_AMOUNT";
                columns[17] = "TOTAL_AMOUNT";
                columns[18] = "NEW_EXCHANGE_RATE";
                columns[19] = "VAT";
                columns[20] = "VAT_AMOUNT";


                if (ID != "NULL")
                {
                    DataTable tbl = new DataTable();

                    tbl.Columns.Add(new DataColumn("INVOICE_NO", typeof(string)));
                    tbl.Columns.Add(new DataColumn("CHARGE_NAME", typeof(string)));
                    tbl.Columns.Add(new DataColumn("QUANTITY", typeof(int)));
                    tbl.Columns.Add(new DataColumn("HSN_CODE", typeof(string)));
                    tbl.Columns.Add(new DataColumn("CURRENCY", typeof(string)));
                    tbl.Columns.Add(new DataColumn("IS_SRRCHARGE", typeof(string)));
                    tbl.Columns.Add(new DataColumn("INVOICE_ID", typeof(int)));
                    tbl.Columns.Add(new DataColumn("ID", typeof(int)));
                    tbl.Columns.Add(new DataColumn("APPROVED_RATE", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("AMOUNT", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("EXCHANGE_RATE", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("TAXABLE_AMOUNT", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("RATE_PER", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("SGST", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("CGST", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("IGST", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("TAX_AMOUNT", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("TOTAL_AMOUNT", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("NEW_EXCHANGE_RATE", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("VAT", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("VAT_AMOUNT", typeof(decimal)));



                    foreach (var i in master.BL_LIST)
                    {
                        DataRow dr = tbl.NewRow();

                        dr["INVOICE_NO"] = master.INVOICE_NO;
                        dr["INVOICE_ID"] = Convert.ToInt32(ID);
                        dr["CHARGE_NAME"] = i.CHARGE_NAME;
                        dr["QUANTITY"] = i.QUANTITY;
                        dr["HSN_CODE"] = i.HSN_CODE;
                        dr["CURRENCY"] = i.CURRENCY;
                        dr["IS_SRRCHARGE"] = i.IS_SRRCHARGE;
                        dr["ID"] = i.ID;
                        dr["APPROVED_RATE"] = i.APPROVED_RATE;
                        dr["AMOUNT"] = i.AMOUNT;
                        dr["EXCHANGE_RATE"] = i.EXCHANGE_RATE;
                        dr["TAXABLE_AMOUNT"] = i.TAXABLE_AMOUNT;
                        dr["RATE_PER"] = i.RATE_PER;
                        dr["SGST"] = i.SGST;
                        dr["CGST"] = i.CGST;
                        dr["IGST"] = i.IGST;
                        dr["TAX_AMOUNT"] = i.TAX_AMOUNT;
                        dr["TOTAL_AMOUNT"] = i.TOTAL_AMOUNT;
                        dr["NEW_EXCHANGE_RATE"] = master.EX_RATE;
                        dr["VAT"] = i.VAT;
                        dr["VAT_AMOUNT"] = i.VAT_AMOUNT;


                        tbl.Rows.Add(dr);
                    }

                    SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl, "INVOICE_CHARGES", columns);
                }
                else
                {
                    SqlHelper.UpdateInvoiceCharges<INVOICE_CHARGES>(master.BL_LIST, "INVOICE_CHARGES", connstring, columns);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateInvoice(string connstring, INVOICE_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "UPDATE_INVOICE" },
                    new SqlParameter("@INVOICE_NO", SqlDbType.VarChar, 100) { Value = master.INVOICE_NO},
                    new SqlParameter("@INVOICE_ID", SqlDbType.Int) { Value = master.INVOICE_ID},
                    new SqlParameter("@INVOICE_TYPE", SqlDbType.VarChar, 100) { Value = master.INVOICE_TYPE },
                    new SqlParameter("@BILL_TO", SqlDbType.VarChar, 255) { Value = master.BILL_TO },
                    new SqlParameter("@BILL_FROM", SqlDbType.VarChar, 255) { Value = master.BILL_FROM },
                    new SqlParameter("@SHIPPER_NAME", SqlDbType.NVarChar, 255) { Value = master.SHIPPER_NAME },
                    new SqlParameter("@CONSIGNEE_NAME", SqlDbType.NVarChar, 255) { Value = master.CONSIGNEE_NAME },
                    new SqlParameter("@PAYMENT_TERM", SqlDbType.VarChar, 50) { Value = master.PAYMENT_TERM },
                    new SqlParameter("@ADDRESS", SqlDbType.VarChar) { Value = master.ADDRESS },
                    new SqlParameter("@BRANCH_ID", SqlDbType.Int ) { Value = master.BRANCH_ID },
                    new SqlParameter("@BANK_ID", SqlDbType.Int ) { Value = master.BANK_ID },
                    new SqlParameter("@INVOICE_DATE", SqlDbType.DateTime ) { Value = master.INVOICE_DATE },
                    new SqlParameter("@BL_NO", SqlDbType.VarChar, 50) { Value = master.BL_NO },
                    new SqlParameter("@AGENT_NAME", SqlDbType.VarChar, 50) { Value = master.AGENT_NAME},
                    new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 50) { Value = master.AGENT_CODE},
                    new SqlParameter("@UPDATED_BY", SqlDbType.VarChar, 50) { Value = master.UPDATED_BY},
                    new SqlParameter("@STATUS", SqlDbType.VarChar, 50) { Value = master.STATUS},
                    new SqlParameter("@CONTAINERS", SqlDbType.VarChar) { Value = master.CONTAINERS},
                    new SqlParameter("@SHIPPER_REF", SqlDbType.VarChar, 100) { Value = master.SHIPPER_REF},
                    new SqlParameter("@REMARKS", SqlDbType.VarChar, 255) { Value = master.REMARKS},
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_INVOICE", parameters);

 

                foreach (var charge in master.BL_LIST)
                {
                    SqlParameter[] param1 =
                    {
                        new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_INVOICE_CHARGES" },
                        new SqlParameter("@ID", SqlDbType.Int) { Value = charge.ID },
                        new SqlParameter("@INVOICE_NO", SqlDbType.VarChar, 100) { Value = charge.INVOICE_NO },
                        new SqlParameter("@INVOICE_ID", SqlDbType.Int) { Value = charge.INVOICE_ID },
                        new SqlParameter("@CHARGE_NAME", SqlDbType.VarChar, 100) { Value = charge.CHARGE_NAME },
                        new SqlParameter("@QUANTITY", SqlDbType.Int) { Value = charge.QUANTITY },
                        new SqlParameter("@HSN_CODE", SqlDbType.VarChar, 100) { Value = charge.HSN_CODE },
                        new SqlParameter("@CURRENCY", SqlDbType.VarChar, 100) { Value = charge.CURRENCY },
                        new SqlParameter("@IS_SRRCHARGE", SqlDbType.Bit) { Value = charge.IS_SRRCHARGE },
                        new SqlParameter("@APPROVED_RATE", SqlDbType.Decimal) { Value = charge.APPROVED_RATE },
                        new SqlParameter("@AMOUNT", SqlDbType.Decimal) { Value = charge.AMOUNT },
                        new SqlParameter("@EXCHANGE_RATE", SqlDbType.Decimal) { Value = charge.EXCHANGE_RATE },
                        new SqlParameter("@NEW_EXCHANGE_RATE", SqlDbType.Decimal) { Value = charge.NEW_EXCHANGE_RATE },
                        new SqlParameter("@TAXABLE_AMOUNT", SqlDbType.Decimal) { Value = charge.TAXABLE_AMOUNT },
                        new SqlParameter("@RATE_PER", SqlDbType.Decimal) { Value = charge.RATE_PER },
                        new SqlParameter("@SGST", SqlDbType.Decimal) { Value = charge.SGST },
                        new SqlParameter("@CGST", SqlDbType.Decimal) { Value = charge.CGST },
                        new SqlParameter("@IGST", SqlDbType.Decimal) { Value = charge.IGST },
                        new SqlParameter("@TAX_AMOUNT", SqlDbType.Decimal) { Value = charge.TAX_AMOUNT },
                        new SqlParameter("@TOTAL_AMOUNT", SqlDbType.Decimal) { Value = charge.TOTAL_AMOUNT },
                        new SqlParameter("@VAT", SqlDbType.Decimal) { Value = charge.VAT },
                        new SqlParameter("@VAT_AMOUNT", SqlDbType.Decimal) { Value = charge.VAT_AMOUNT },

                    };

                    SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_INVOICE", param1);

                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error updating invoice: " + ex.Message);
            }
        }


        public void InsertCreditNote(string connstring, List<CREDIT_NOTE> master)
        {
            try
            {
                string[] columns = new string[7];
                columns[0] = "INVOICE_NO";
                columns[1] = "CHARGE_NAME";
                columns[2] = "REMAINING_AMOUNT";
                columns[3] = "CREDIT_AMOUNT";
                columns[4] = "CREDIT_NO";
                columns[5] = "AGENT_CODE";
                columns[6] = "AGENT_NAME";

                DataTable tbl = new DataTable();

                tbl.Columns.Add(new DataColumn("INVOICE_NO", typeof(string)));
                tbl.Columns.Add(new DataColumn("CHARGE_NAME", typeof(string)));
                tbl.Columns.Add(new DataColumn("REMAINING_AMOUNT", typeof(decimal)));
                tbl.Columns.Add(new DataColumn("CREDIT_AMOUNT", typeof(decimal)));
                tbl.Columns.Add(new DataColumn("CREDIT_NO", typeof(string)));
                tbl.Columns.Add(new DataColumn("AGENT_CODE", typeof(string)));
                tbl.Columns.Add(new DataColumn("AGENT_NAME", typeof(string)));

                foreach (var i in master)
                {
                    DataRow dr = tbl.NewRow();

                    dr["INVOICE_NO"] = i.INVOICE_NO;
                    dr["CHARGE_NAME"] = i.CHARGE_NAME;
                    dr["REMAINING_AMOUNT"] = i.REMAINING_AMOUNT;
                    dr["CREDIT_AMOUNT"] = i.CREDIT_AMOUNT;
                    dr["CREDIT_NO"] = i.CREDIT_NO;
                    dr["AGENT_CODE"] = i.AGENT_CODE;
                    dr["AGENT_NAME"] = i.AGENT_NAME;

                    tbl.Rows.Add(dr);
                }

                SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl, "TB_CREDIT_NOTE", columns);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void FinalizeInvoice(string connstring, INVOICE_FINALIZE master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION",    SqlDbType.VarChar,50) { Value = "FINALIZE_INVOICE" },
                  new SqlParameter("@INVOICE_NO",   SqlDbType.VarChar,100) { Value = master.INVOICE_NO},
                  new SqlParameter("@INVOICE_ID", SqlDbType.Int) { Value = master.INVOICE_ID },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_INVOICE", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetInvoiceDetails(string connstring, int INVOICE_ID, string INVOICE_NO, string PORT, string ORG_CODE)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_INVOICE_DETAILS" },
                new SqlParameter("@INVOICE_NO", SqlDbType.VarChar, 100) { Value = INVOICE_NO },
                new SqlParameter("@PORT", SqlDbType.VarChar, 100) { Value = PORT },
                new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 50) { Value = ORG_CODE },
                new SqlParameter("@INVOICE_ID", SqlDbType.Int) { Value = INVOICE_ID },
            };

            return SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_INVOICE", parameters);
        }

        public List<INVOICE_MASTER> GetInvoiceList(string connstring, string FROM_DATE, string TO_DATE, string ORG_CODE, string PORT, string BL_NO ,string PAYMENT_TERM)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_INVOICE_LIST" },
                  new SqlParameter("@FROMDATE", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(FROM_DATE) ? null : Convert.ToDateTime(FROM_DATE) },
                  new SqlParameter("@TODATE", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(TO_DATE) ? null : Convert.ToDateTime(TO_DATE) },
                  new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 50) { Value = ORG_CODE },
                  new SqlParameter("@PORT", SqlDbType.VarChar, 100) { Value = PORT },
                  new SqlParameter("@BL_NO", SqlDbType.VarChar, 100) { Value = BL_NO },
                  new SqlParameter("@PAYMENT_TERM", SqlDbType.VarChar, 100) { Value = PAYMENT_TERM },

                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_INVOICE", parameters);
                List<INVOICE_MASTER> invoiceList = SqlHelper.CreateListFromTable<INVOICE_MASTER>(dataTable);

                return invoiceList;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<CREDIT_NOTE> GetCreditList(string connstring, string FROM_DATE, string TO_DATE, string ORG_CODE, string PORT, string CREDIT_NO)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_CREDIT_LIST" },
                  new SqlParameter("@FROMDATE", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(FROM_DATE) ? null : Convert.ToDateTime(FROM_DATE) },
                  new SqlParameter("@TODATE", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(TO_DATE) ? null : Convert.ToDateTime(TO_DATE) },
                  new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 50) { Value = ORG_CODE },
                  new SqlParameter("@PORT", SqlDbType.VarChar, 100) { Value = PORT },
                  new SqlParameter("@CREDIT_NO", SqlDbType.VarChar, 100) { Value = CREDIT_NO },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_INVOICE", parameters);
                List<CREDIT_NOTE> invoiceList = SqlHelper.CreateListFromTable<CREDIT_NOTE>(dataTable);

                return invoiceList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<INVOICE_BL_CHECK> GetBLExists(string dbConn, string INVOICE_TYPE, string BL_NO)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "CHECK_BL_EXISTS" },
                  new SqlParameter("@INVOICE_TYPE", SqlDbType.VarChar, 100) { Value = INVOICE_TYPE },
                  new SqlParameter("@BL_NO", SqlDbType.VarChar, 100) { Value = BL_NO },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_INVOICE", parameters);

                List<INVOICE_BL_CHECK> master = SqlHelper.CreateListFromTable<INVOICE_BL_CHECK>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<INVOICE_PAYMENT_TERM_CHECK> PaymentTerm(string dbConn, string BL_NO)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "CHECK_PAYMENT_TERM" },
                  new SqlParameter("@BL_NO", SqlDbType.VarChar, 100) { Value = BL_NO },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_INVOICE", parameters);

                List<INVOICE_PAYMENT_TERM_CHECK> master = SqlHelper.CreateListFromTable<INVOICE_PAYMENT_TERM_CHECK>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BL_FINALIZED> CheckBlFinalized(string dbConn, string BL_NO, string AGENT_CODE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "CHECK_BL_FINALIZED" },
                  new SqlParameter("@BL_NO", SqlDbType.VarChar, 100) { Value = BL_NO },
                  new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 100) { Value = AGENT_CODE },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_INVOICE", parameters);

                List<BL_FINALIZED> master = SqlHelper.CreateListFromTable<BL_FINALIZED>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetInvoiceDetailsForReceipt(string connstring, string INVOICE_NO, string PORT, string ORG_CODE, string USER_CODE)
        {
            using (var table = new DataTable())
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_INVOICE_DETAILS_FOR_RECEIPT" },
                    new SqlParameter("@INVOICE_NOLIST", SqlDbType.VarChar,255) { Value = INVOICE_NO },
                    new SqlParameter("@PORT", SqlDbType.VarChar, 100) { Value = PORT },
                    new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 50) { Value = ORG_CODE },
                    new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 50) { Value = USER_CODE },
                };

                return SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_INVOICE", parameters);
            }
        }

        //New ADDED siddhesh
        public List<INVOICE_RATE_CHECK> GetRateExists(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "CHECK_RATE_EXISTS" },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_INVOICE", parameters);

                List<INVOICE_RATE_CHECK> master = SqlHelper.CreateListFromTable<INVOICE_RATE_CHECK>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GET_CUST_LIST> GetBLCustList(string dbConn, string BL_NO)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_CUST_LIST" },
                  new SqlParameter("@BL_NO", SqlDbType.VarChar, 50) { Value = BL_NO },

                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_INVOICE", parameters);

                List<GET_CUST_LIST> master = SqlHelper.CreateListFromTable<GET_CUST_LIST>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GET_CUST_LIST> GetPrimeDetails(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_PRIME_DETAILS" },

                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_INVOICE", parameters);

                List<GET_CUST_LIST> master = SqlHelper.CreateListFromTable<GET_CUST_LIST>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GET_INVOICE_LIST> GetInvoicesByBLNo(string dbConn, string BL_NO)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_INVOICE_LIST_BY_BL" },
                  new SqlParameter("@BL_NO", SqlDbType.VarChar, 50) { Value = BL_NO },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_INVOICE", parameters);

                List<GET_INVOICE_LIST> master = SqlHelper.CreateListFromTable<GET_INVOICE_LIST>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
