using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Translators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Repository
{
    public class MasterRepo
    {

        #region "PARTY MASTER"
        public string InsertPartyMaster(string connstring, PARTY_MASTER master)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction(); // Begin transaction
                try
                {
                    SqlParameter[] parameters =
                    {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_CUSTOMER" },
                  new SqlParameter("@CUST_NAME", SqlDbType.VarChar,500) { Value = master.CUST_NAME},
                  new SqlParameter("@CUST_ADDRESS", SqlDbType.VarChar, 500) { Value = master.CUST_ADDRESS },
                  new SqlParameter("@CUST_EMAIL", SqlDbType.VarChar, 50) { Value = master.CUST_EMAIL },
                  new SqlParameter("@CUST_CONTACT", SqlDbType.VarChar, 20) { Value = master.CUST_CONTACT },
                  new SqlParameter("@CUST_TYPE", SqlDbType.VarChar,100) { Value = master.CUST_TYPE },
                  new SqlParameter("@GSTIN", SqlDbType.VarChar,15) { Value = master.GSTIN },
                  new SqlParameter("@VAT_NO", SqlDbType.VarChar,30) { Value = master.VAT_NO },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS},
                  new SqlParameter("@REMARKS", SqlDbType.VarChar, 200) { Value = master.REMARKS },
                  new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 100) { Value = master.AGENT_CODE },
                  //new SqlParameter("@CREATED_BY", SqlDbType.VarChar,100) { Value = master.CREATED_BY },
                  new SqlParameter("@COUNTRY", SqlDbType.VarChar,20) { Value = master.COUNTRY },
                  new SqlParameter("@STATE", SqlDbType.VarChar,255) { Value = master.STATE },
                  new SqlParameter("@CITY", SqlDbType.VarChar,255) { Value = master.CITY },
                  new SqlParameter("@PINCODE", SqlDbType.VarChar,50) { Value = master.PINCODE },
                  new SqlParameter("@PAN", SqlDbType.VarChar,50) { Value = master.PAN },
                  new SqlParameter("@CONTACT_PERSON_NAME", SqlDbType.VarChar,255) { Value = master.CONTACT_PERSON_NAME },
                  new SqlParameter("@CONTACT_PERSON_NO", SqlDbType.VarChar,50) { Value = master.CONTACT_PERSON_NO },
                  new SqlParameter("@IS_GROUP_COMPANIES", SqlDbType.Bit) { Value = master.IS_GROUP_COMPANIES },
                  new SqlParameter("@SALES_NAME", SqlDbType.VarChar,255) { Value = master.SALES_NAME },
                  new SqlParameter("@SALES_CODE", SqlDbType.VarChar,50) { Value = master.SALES_CODE },
                  new SqlParameter("@SALES_LOC", SqlDbType.VarChar,255) { Value = master.SALES_LOC },
                  new SqlParameter("@SALES_EFFECTIVE_DATE", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(master.SALES_EFFECTIVE_DATE) ? null : Convert.ToDateTime(master.SALES_EFFECTIVE_DATE)  },
                  new SqlParameter("@IS_VENDOR", SqlDbType.Bit) { Value = master.IS_VENDOR },
                };

                    var ID = SqlHelper.ExecuteProcedureReturnStrings(conn, transaction, "SP_CRUD_MASTER", parameters);

                    DataTable tbl = new DataTable();
                    tbl.Columns.Add(new DataColumn("CUST_ID", typeof(int)));
                    tbl.Columns.Add(new DataColumn("BRANCH_NAME", typeof(string)));
                    tbl.Columns.Add(new DataColumn("BRANCH_CODE", typeof(string)));
                    tbl.Columns.Add(new DataColumn("COUNTRY", typeof(string)));
                    tbl.Columns.Add(new DataColumn("STATE", typeof(string)));
                    tbl.Columns.Add(new DataColumn("CITY", typeof(string)));
                    tbl.Columns.Add(new DataColumn("TAN", typeof(string)));
                    tbl.Columns.Add(new DataColumn("TAX_NO", typeof(string)));
                    tbl.Columns.Add(new DataColumn("TAX_TYPE", typeof(string)));
                    tbl.Columns.Add(new DataColumn("PIC_NAME", typeof(string)));
                    tbl.Columns.Add(new DataColumn("PIC_CONTACT", typeof(string)));
                    tbl.Columns.Add(new DataColumn("PIC_EMAIL", typeof(string)));
                    tbl.Columns.Add(new DataColumn("ADDRESS", typeof(string)));
                    tbl.Columns.Add(new DataColumn("IS_SEZ", typeof(bool)));
                    tbl.Columns.Add(new DataColumn("IS_TAX_APPLICABLE", typeof(bool)));

                    foreach (var i in master.BRANCH_LIST)
                    {
                        DataRow dr = tbl.NewRow();

                        dr["CUST_ID"] = Convert.ToInt32(ID);
                        dr["BRANCH_NAME"] = i.BRANCH_NAME;
                        dr["BRANCH_CODE"] = i.BRANCH_CODE;
                        dr["COUNTRY"] = i.COUNTRY;
                        dr["STATE"] = i.STATE;
                        dr["CITY"] = i.CITY;
                        dr["TAN"] = i.TAN;
                        dr["PIC_NAME"] = i.PIC_NAME;
                        dr["PIC_CONTACT"] = i.PIC_CONTACT;
                        dr["PIC_EMAIL"] = i.PIC_EMAIL;
                        dr["ADDRESS"] = i.ADDRESS;
                        dr["TAX_NO"] = i.TAX_NO;
                        dr["TAX_TYPE"] = i.TAX_TYPE;
                        dr["IS_SEZ"] = i.IS_SEZ;
                        dr["IS_TAX_APPLICABLE"] = i.IS_TAX_APPLICABLE;

                        tbl.Rows.Add(dr);
                    }

                    string[] columns = new string[15];
                    columns[0] = "CUST_ID";
                    columns[1] = "BRANCH_NAME";
                    columns[2] = "COUNTRY";
                    columns[3] = "STATE";
                    columns[4] = "CITY";
                    columns[5] = "TAN";
                    columns[6] = "PIC_NAME";
                    columns[7] = "PIC_CONTACT";
                    columns[8] = "PIC_EMAIL";
                    columns[9] = "ADDRESS";
                    columns[10] = "TAX_NO";
                    columns[11] = "TAX_TYPE";
                    columns[12] = "IS_SEZ";
                    columns[13] = "IS_TAX_APPLICABLE";
                    columns[14] = "BRANCH_CODE";

                    SqlHelper.ExecuteProcedureBulkInserts(conn, transaction, tbl, "MST_CUSTOMER_BRANCH", columns);

                    DataTable tbl1 = new DataTable();
                    tbl1.Columns.Add(new DataColumn("CUST_ID", typeof(int)));
                    tbl1.Columns.Add(new DataColumn("BRANCH_CODE", typeof(string)));
                    tbl1.Columns.Add(new DataColumn("BANK_NAME", typeof(string)));
                    tbl1.Columns.Add(new DataColumn("BANK_ACC_NO", typeof(string)));
                    tbl1.Columns.Add(new DataColumn("BANK_IFSC", typeof(string)));
                    tbl1.Columns.Add(new DataColumn("BANK_SWIFT", typeof(string)));
                    tbl1.Columns.Add(new DataColumn("BANK_REMARKS", typeof(string)));

                    foreach (var i in master.BANK_LIST)
                    {
                        DataRow dr = tbl1.NewRow();

                        dr["CUST_ID"] = Convert.ToInt32(ID);
                        dr["BRANCH_CODE"] = i.BRANCH_CODE;
                        dr["BANK_NAME"] = i.BANK_NAME;
                        dr["BANK_ACC_NO"] = i.BANK_ACC_NO;
                        dr["BANK_IFSC"] = i.BANK_IFSC;
                        dr["BANK_SWIFT"] = i.BANK_SWIFT;
                        dr["BANK_REMARKS"] = i.BANK_REMARKS;

                        tbl1.Rows.Add(dr);
                    }

                    string[] columns1 = new string[7];
                    columns1[0] = "CUST_ID";
                    columns1[1] = "BANK_NAME";
                    columns1[2] = "BANK_ACC_NO";
                    columns1[3] = "BANK_IFSC";
                    columns1[4] = "BANK_SWIFT";
                    columns1[5] = "BANK_REMARKS";
                    columns1[6] = "BRANCH_CODE";

                    SqlHelper.ExecuteProcedureBulkInserts(conn, transaction, tbl1, "MST_CUSTOMER_BANK", columns1);


                    //vednor agreement list
                    foreach (var agreement in master.VENDOR_AGREEMENT_LIST)
                    {

                        SqlParameter[] vendorParams =
                     {
                    new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_VENDOR_AGREEMENT" },
                    new SqlParameter("@agreement_no", SqlDbType.VarChar, 50) { Value = agreement.AGREEMENT_NO },
                    new SqlParameter("@vendor_id", SqlDbType.Int) { Value = Convert.ToInt32(ID) },
                    new SqlParameter("@procurement_date", SqlDbType.DateTime) { Value = agreement.PROCUREMENT_DATE },
                    new SqlParameter("@start_date", SqlDbType.DateTime) { Value = agreement.START_DATE },
                    new SqlParameter("@end_date", SqlDbType.DateTime) { Value = agreement.END_DATE },
                    new SqlParameter("@equipment_type_id", SqlDbType.Int) { Value = agreement.EQUIPMENT_TYPE_ID },
                    new SqlParameter("@equipment_size_id", SqlDbType.Int) { Value = agreement.EQUIPMENT_SIZE_ID },
                    new SqlParameter("@on_hire_handling", SqlDbType.Decimal) { Value = agreement.ON_HIRE_HANDLING },
                    new SqlParameter("@off_hire_handling", SqlDbType.Decimal) { Value = agreement.OFF_HIRE_HANDLING },
                    new SqlParameter("@dpp", SqlDbType.Decimal) { Value = agreement.DPP },
                    new SqlParameter("@pickup_credit", SqlDbType.Decimal) { Value = agreement.PICKUP_CREDIT },
                    new SqlParameter("@drop_off_charge", SqlDbType.Decimal) { Value = agreement.DROP_OFF_CHARGE },
                    new SqlParameter("@annual_depreciation_in_percentage", SqlDbType.Decimal) { Value = agreement.ANNUAL_DEPRECIATION_IN_PERCENTAGE },
                    new SqlParameter("@re_delivery_cap", SqlDbType.Int) { Value = agreement.RE_DELIVERY_CAP },
                    new SqlParameter("@depreciated_replacement_value", SqlDbType.Decimal) { Value = agreement.DEPRECIATED_REPLACEMENT_VALUE },
                    new SqlParameter("@inspection_charges", SqlDbType.Decimal) { Value = agreement.INSPECTION_CHARGES },
                    new SqlParameter("@currency_id", SqlDbType.Int) { Value = agreement.CURRENCY_ID },
                    new SqlParameter("@min_rental_period_in_days", SqlDbType.Int) { Value = agreement.MIN_RENTAL_PERIOD_IN_DAYS },
                    new SqlParameter("@min_residual_value_in_percentage", SqlDbType.Decimal) { Value = agreement.MIN_RESIDUAL_VALUE_IN_PERCENTAGE },
                    new SqlParameter("@pre_trip_inspection_charge", SqlDbType.Decimal) { Value = agreement.PRE_TRIP_INSPECTION_CHARGE },
                    new SqlParameter("@post_trip_inspection_charge", SqlDbType.Decimal) { Value = agreement.POST_TRIP_INSPECTION_CHARGE },
                    new SqlParameter("@redelivery_notice_period_in_days", SqlDbType.Int) { Value = agreement.REDELIVERY_NOTICE_PERIOD_IN_DAYS },
                    new SqlParameter("@pickup_charge", SqlDbType.Decimal) { Value = agreement.PICKUP_CHARGE },
                    new SqlParameter("@is_active", SqlDbType.Bit) { Value = agreement.IS_ACTIVE },
                    new SqlParameter("@created_by", SqlDbType.Int) { Value = agreement.CREATED_BY },
                    new SqlParameter("@created_at", SqlDbType.DateTime) { Value = agreement.CREATED_AT },
                    new SqlParameter("@modified_by", SqlDbType.Int) { Value = agreement.MODIFIED_BY },
                    new SqlParameter("@modified_at", SqlDbType.DateTime) { Value = agreement.MODIFIED_AT },

                };

                        var vendorAgreementId = SqlHelper.ExecuteProcedureReturnStrings(conn, transaction, "SP_CRUD_MASTER", vendorParams);

                        DataTable portTable = new DataTable();
                        portTable.Columns.Add(new DataColumn("vendor_agreement_id", typeof(int)));
                        portTable.Columns.Add(new DataColumn("port_id", typeof(int)));
                        portTable.Columns.Add(new DataColumn("is_active", typeof(Boolean)));
                        portTable.Columns.Add(new DataColumn("created_by", typeof(int)));
                        portTable.Columns.Add(new DataColumn("created_at", typeof(DateTime)));
                        portTable.Columns.Add(new DataColumn("modified_by", typeof(int)));
                        portTable.Columns.Add(new DataColumn("modified_at", typeof(DateTime)));
                        portTable.Columns.Add(new DataColumn("deleted_by", typeof(int)));
                        portTable.Columns.Add(new DataColumn("deleted_at", typeof(DateTime)));

                        foreach (var port in master.VENDOR_PICKUP_PORT_LIST)
                        {
                            DataRow portRow = portTable.NewRow();
                            portRow["vendor_agreement_id"] = Convert.ToInt32(vendorAgreementId);
                            portRow["port_id"] = port.PORT_ID;
                            portRow["is_active"] = port.IS_ACTIVE;
                            portRow["created_by"] = port.CREATED_BY.HasValue ? port.CREATED_BY.Value : (object)DBNull.Value;
                            portRow["created_at"] = port.CREATED_AT.HasValue ? port.CREATED_AT.Value : (object)DBNull.Value;
                            portRow["modified_by"] = port.MODIFIED_BY.HasValue ? port.MODIFIED_BY.Value : (object)DBNull.Value;
                            portRow["modified_at"] = port.MODIFIED_AT.HasValue ? port.MODIFIED_AT.Value : (object)DBNull.Value;
                            portRow["deleted_by"] = port.DELETED_BY.HasValue ? port.DELETED_BY.Value : (object)DBNull.Value; // Handle nullable int
                            portRow["deleted_at"] = port.DELETED_AT.HasValue ? port.DELETED_AT.Value : (object)DBNull.Value; // Handle nullable DateTime

                            portTable.Rows.Add(portRow);
                        }
                        string[] columns2 = new string[9];
                        columns2[0] = "vendor_agreement_id";
                        columns2[1] = "port_id";
                        columns2[2] = "is_active";
                        columns2[3] = "created_by";
                        columns2[4] = "created_at";
                        columns2[5] = "modified_by";
                        columns2[6] = "modified_at";
                        columns2[7] = "deleted_by";
                        columns2[8] = "deleted_at";

                        SqlHelper.ExecuteProcedureBulkInserts(conn, transaction, portTable, "vendor_agreement_pickup_port", columns2);


                        //redelivery
                        DataTable locationTable = new DataTable();
                        locationTable.Columns.Add(new DataColumn("vendor_agreement_id", typeof(int)));
                        locationTable.Columns.Add(new DataColumn("redelivery_port_id", typeof(int)));
                        locationTable.Columns.Add(new DataColumn("is_active", typeof(Boolean)));
                        locationTable.Columns.Add(new DataColumn("created_by", typeof(int)));
                        locationTable.Columns.Add(new DataColumn("created_at", typeof(DateTime)));
                        locationTable.Columns.Add(new DataColumn("modified_by", typeof(int)));
                        locationTable.Columns.Add(new DataColumn("modified_at", typeof(DateTime)));
                        locationTable.Columns.Add(new DataColumn("deleted_by", typeof(int)));
                        locationTable.Columns.Add(new DataColumn("deleted_at", typeof(DateTime)));


                        foreach (var location in master.VENDOR_REDELIVERY_PORT_LIST)
                        {
                            DataRow locationRow = locationTable.NewRow();
                            locationRow["vendor_agreement_id"] = Convert.ToInt32(vendorAgreementId);
                            locationRow["redelivery_port_id"] = location.REDELIVERY_PORT_ID;
                            locationRow["is_active"] = location.IS_ACTIVE;
                            locationRow["created_by"] = location.CREATED_BY.HasValue ? location.CREATED_BY.Value : (object)DBNull.Value;
                            locationRow["created_at"] = location.CREATED_AT.HasValue ? location.CREATED_AT.Value : (object)DBNull.Value;
                            locationRow["modified_by"] = location.MODIFIED_BY.HasValue ? location.MODIFIED_BY.Value : (object)DBNull.Value;
                            locationRow["modified_at"] = location.MODIFIED_AT.HasValue ? location.MODIFIED_AT.Value : (object)DBNull.Value;
                            locationRow["deleted_by"] = location.DELETED_BY.HasValue ? location.DELETED_BY.Value : (object)DBNull.Value; // Handle nullable int
                            locationRow["deleted_at"] = location.DELETED_AT.HasValue ? location.DELETED_AT.Value : (object)DBNull.Value; // Handle nullable DateTime


                            locationTable.Rows.Add(locationRow);
                        }

                        string[] columns3 = new string[9];
                        columns3[0] = "vendor_agreement_id";
                        columns3[1] = "redelivery_port_id";
                        columns3[2] = "is_active";
                        columns3[3] = "created_by";
                        columns3[4] = "created_at";
                        columns3[5] = "modified_by";
                        columns3[6] = "modified_at";
                        columns3[7] = "deleted_by";
                        columns3[8] = "deleted_at";

                        SqlHelper.ExecuteProcedureBulkInserts(conn, transaction, locationTable, "vendor_agreement_redelivery_port", columns3);
                    }
                    transaction.Commit();
                    return ID;
                }

                catch (Exception ex)
                {

                    // Rollback the transaction on error
                    transaction.Rollback();
                    throw new Exception("An error occurred while inserting the party master: " + ex.Message);
                }
            }
        }
        public List<PARTY_MASTER> GetPartyMasterList(string dbConn, string AgentCode, string CustName, string CustType, bool Status, string FROM_DATE, string TO_DATE, bool IS_VENDOR)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_CUSTOMERLIST" },
                  new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 50) { Value = AgentCode },
                  new SqlParameter("@CUST_NAME", SqlDbType.VarChar, 255) { Value = CustName },
                  new SqlParameter("@CUST_TYPE", SqlDbType.VarChar, 10) { Value = CustType },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = Status },
                  new SqlParameter("@FROM_DATE", SqlDbType.DateTime) { Value = FROM_DATE },
                  new SqlParameter("@TO_DATE", SqlDbType.DateTime) { Value = TO_DATE },
                  new SqlParameter("@IS_VENDOR", SqlDbType.Bit) { Value = IS_VENDOR },

                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_MASTER", parameters);
                List<PARTY_MASTER> master = SqlHelper.CreateListFromTable<PARTY_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public DataSet GetPartyMasterDetails(string connstring, string AGENT_CODE, int CUSTOMER_ID, int VENDOR_AGREEMENT_ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 50) { Value = AGENT_CODE },
                  new SqlParameter("@CUST_ID", SqlDbType.Int) { Value = CUSTOMER_ID },
                  new SqlParameter("@vendor_agreement_id", SqlDbType.Int) { Value = VENDOR_AGREEMENT_ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "GET_CUSTOMERDETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeletePartyMasterDetails(string connstring, int CUSTOMER_ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@CUST_ID", SqlDbType.Int) { Value = CUSTOMER_ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "DELETE_CUSTOMER" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdatePartyMasterDetails(string connstring, PARTY_MASTER master)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction(); // Begin transaction
                try
                {
                    SqlParameter[] parameters =
                    {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_CUSTOMER" },
                  new SqlParameter("@CUST_ID", SqlDbType.Int) { Value = master.CUST_ID },
                  new SqlParameter("@CUST_NAME", SqlDbType.VarChar,50) { Value = master.CUST_NAME},
                  new SqlParameter("@CUST_ADDRESS", SqlDbType.VarChar, 100) { Value = master.CUST_ADDRESS },
                  new SqlParameter("@CUST_EMAIL", SqlDbType.VarChar, 50) { Value = master.CUST_EMAIL },
                  new SqlParameter("@CUST_CONTACT", SqlDbType.VarChar, 20) { Value = master.CUST_CONTACT },
                  new SqlParameter("@CUST_TYPE", SqlDbType.VarChar,100) { Value = master.CUST_TYPE },
                  new SqlParameter("@GSTIN", SqlDbType.VarChar,15) { Value = master.GSTIN },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS},
                  new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 50) { Value = master.AGENT_CODE },
                  new SqlParameter("@COUNTRY", SqlDbType.VarChar,20) { Value = master.COUNTRY },
                  new SqlParameter("@STATE", SqlDbType.VarChar,255) { Value = master.STATE },
                  new SqlParameter("@CITY", SqlDbType.VarChar,255) { Value = master.CITY },
                  new SqlParameter("@PINCODE", SqlDbType.VarChar,50) { Value = master.PINCODE },
                  new SqlParameter("@PAN", SqlDbType.VarChar,50) { Value = master.PAN },
                  new SqlParameter("@CONTACT_PERSON_NAME", SqlDbType.VarChar,255) { Value = master.CONTACT_PERSON_NAME },
                  new SqlParameter("@CONTACT_PERSON_NO", SqlDbType.VarChar,50) { Value = master.CONTACT_PERSON_NO },
                  new SqlParameter("@IS_GROUP_COMPANIES", SqlDbType.Bit) { Value = master.IS_GROUP_COMPANIES },
                  new SqlParameter("@SALES_NAME", SqlDbType.VarChar,255) { Value = master.SALES_NAME },
                  new SqlParameter("@SALES_CODE", SqlDbType.VarChar,50) { Value = master.SALES_CODE },
                  new SqlParameter("@SALES_LOC", SqlDbType.VarChar,255) { Value = master.SALES_LOC },
                  new SqlParameter("@SALES_EFFECTIVE_DATE", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(master.SALES_EFFECTIVE_DATE) ? null : Convert.ToDateTime(master.SALES_EFFECTIVE_DATE)  },
                };

                    SqlHelper.ExecuteProcedureReturnStrings(conn, transaction, "SP_CRUD_MASTER", parameters);

                    foreach (var items in master.BRANCH_LIST)
                    {
                        SqlParameter[] parameters1 =
                        {
                      new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_CUSTOMER_BRANCH" },
                      new SqlParameter("@BRANCH_ID", SqlDbType.Int) { Value = items.ID},
                      new SqlParameter("@CUST_ID", SqlDbType.Int) { Value = master.CUST_ID },
                      new SqlParameter("@BRANCH_NAME", SqlDbType.VarChar, 255) { Value = items.BRANCH_NAME },
                      new SqlParameter("@BRANCH_CODE", SqlDbType.VarChar, 20) { Value = items.BRANCH_CODE },
                      new SqlParameter("@COUNTRY", SqlDbType.VarChar, 50) { Value = items.COUNTRY },
                      new SqlParameter("@STATE", SqlDbType.VarChar,255) { Value = items.STATE },
                      new SqlParameter("@CITY", SqlDbType.VarChar,255) { Value = items.CITY },
                      new SqlParameter("@TAN", SqlDbType.VarChar,50) { Value = items.TAN },
                      new SqlParameter("@TAX_NO", SqlDbType.VarChar,50) { Value = items.TAX_NO },
                      new SqlParameter("@TAX_TYPE", SqlDbType.VarChar,20) { Value = items.TAX_TYPE },
                      new SqlParameter("@PIC_NAME", SqlDbType.VarChar,255) { Value = items.PIC_NAME },
                      new SqlParameter("@PIC_CONTACT", SqlDbType.VarChar,50) { Value = items.PIC_CONTACT },
                      new SqlParameter("@PIC_EMAIL", SqlDbType.VarChar,255) { Value = items.PIC_EMAIL },
                      new SqlParameter("@ADDRESS", SqlDbType.VarChar,255) { Value = items.ADDRESS },
                      new SqlParameter("@IS_SEZ", SqlDbType.Bit) { Value = items.IS_SEZ },
                      new SqlParameter("@IS_TAX_APPLICABLE", SqlDbType.Bit) { Value = items.IS_TAX_APPLICABLE },
                    };

                        SqlHelper.ExecuteProcedureReturnStrings(conn, transaction, "SP_CRUD_MASTER", parameters1);
                    }

                    foreach (var items in master.BANK_LIST)
                    {
                        SqlParameter[] parameters2 =
                        {
                      new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_CUSTOMER_BANK" },
                      new SqlParameter("@BANK_ID", SqlDbType.Int) { Value = items.ID},
                      new SqlParameter("@CUST_ID", SqlDbType.Int) { Value = master.CUST_ID },
                      new SqlParameter("@BANK_NAME", SqlDbType.VarChar, 255) { Value = items.BANK_NAME },
                      new SqlParameter("@BRANCH_CODE", SqlDbType.VarChar, 20) { Value = items.BRANCH_CODE },
                      new SqlParameter("@BANK_ACC_NO", SqlDbType.VarChar, 50) { Value = items.BANK_ACC_NO },
                      new SqlParameter("@BANK_IFSC", SqlDbType.VarChar,50) { Value = items.BANK_IFSC },
                      new SqlParameter("@BANK_SWIFT", SqlDbType.VarChar,50) { Value = items.BANK_SWIFT },
                      new SqlParameter("@BANK_REMARKS", SqlDbType.VarChar,255) { Value = items.BANK_REMARKS },
                    };

                        SqlHelper.ExecuteProcedureReturnStrings(conn, transaction, "SP_CRUD_MASTER", parameters2);
                    }

                    //vednor agreement list
                    foreach (var agreement in master.VENDOR_AGREEMENT_LIST)
                    {

                        SqlParameter[] vendorParams =
                     {
                        new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_VENDOR_AGREEMENT" },
                        new SqlParameter("@vendor_agreement_id", SqlDbType.VarChar, 50) { Value = agreement.VENDOR_AGREEMENT_ID },
                        new SqlParameter("@agreement_no", SqlDbType.VarChar, 50) { Value = agreement.AGREEMENT_NO },
                        new SqlParameter("@procurement_date", SqlDbType.DateTime) { Value = agreement.PROCUREMENT_DATE },
                        new SqlParameter("@start_date", SqlDbType.DateTime) { Value = agreement.START_DATE },
                        new SqlParameter("@end_date", SqlDbType.DateTime) { Value = agreement.END_DATE },
                        new SqlParameter("@equipment_type_id", SqlDbType.Int) { Value = agreement.EQUIPMENT_TYPE_ID },
                        new SqlParameter("@equipment_size_id", SqlDbType.Int) { Value = agreement.EQUIPMENT_SIZE_ID },
                        new SqlParameter("@on_hire_handling", SqlDbType.Decimal) { Value = agreement.ON_HIRE_HANDLING },
                        new SqlParameter("@off_hire_handling", SqlDbType.Decimal) { Value = agreement.OFF_HIRE_HANDLING },
                        new SqlParameter("@dpp", SqlDbType.Decimal) { Value = agreement.DPP },
                        new SqlParameter("@pickup_credit", SqlDbType.Decimal) { Value = agreement.PICKUP_CREDIT },
                        new SqlParameter("@drop_off_charge", SqlDbType.Decimal) { Value = agreement.DROP_OFF_CHARGE },
                        new SqlParameter("@annual_depreciation_in_percentage", SqlDbType.Decimal) { Value = agreement.ANNUAL_DEPRECIATION_IN_PERCENTAGE },
                        new SqlParameter("@re_delivery_cap", SqlDbType.Int) { Value = agreement.RE_DELIVERY_CAP },
                        new SqlParameter("@depreciated_replacement_value", SqlDbType.Decimal) { Value = agreement.DEPRECIATED_REPLACEMENT_VALUE },
                        new SqlParameter("@inspection_charges", SqlDbType.Decimal) { Value = agreement.INSPECTION_CHARGES },
                        new SqlParameter("@currency_id", SqlDbType.Int) { Value = agreement.CURRENCY_ID },
                        new SqlParameter("@min_rental_period_in_days", SqlDbType.Int) { Value = agreement.MIN_RENTAL_PERIOD_IN_DAYS },
                        new SqlParameter("@min_residual_value_in_percentage", SqlDbType.Decimal) { Value = agreement.MIN_RESIDUAL_VALUE_IN_PERCENTAGE },
                        new SqlParameter("@pre_trip_inspection_charge", SqlDbType.Decimal) { Value = agreement.PRE_TRIP_INSPECTION_CHARGE },
                        new SqlParameter("@post_trip_inspection_charge", SqlDbType.Decimal) { Value = agreement.POST_TRIP_INSPECTION_CHARGE },
                        new SqlParameter("@redelivery_notice_period_in_days", SqlDbType.Int) { Value = agreement.REDELIVERY_NOTICE_PERIOD_IN_DAYS },
                        new SqlParameter("@pickup_charge", SqlDbType.Decimal) { Value = agreement.PICKUP_CHARGE },
                        new SqlParameter("@modified_by", SqlDbType.Int) { Value = agreement.MODIFIED_BY },
                        new SqlParameter("@modified_at", SqlDbType.DateTime) { Value = agreement.MODIFIED_AT },
                    };

                        SqlHelper.ExecuteProcedureReturnStrings(conn, transaction, "SP_CRUD_MASTER", vendorParams);


                        foreach (var items in master.VENDOR_PICKUP_PORT_LIST)
                        {
                            SqlParameter[] vendorparameters =
                             {
                              new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_VENDOR_PICKUP_PORT" },
                              new SqlParameter("@vendor_agreement_id", SqlDbType.Int) { Value = items.VENDOR_AGREEMENT_ID},
                              new SqlParameter("@vendor_agr_port_id", SqlDbType.Int) { Value = items.VENDOR_AGR_PORT_ID},
                              new SqlParameter("@port_id", SqlDbType.Int) { Value = items.PORT_ID},
                              new SqlParameter("@modified_by", SqlDbType.Int) { Value = items.MODIFIED_BY },
                              new SqlParameter("@modified_at", SqlDbType.DateTime) { Value = items.MODIFIED_AT },
                         };

                            SqlHelper.ExecuteProcedureReturnStrings(conn, transaction, "SP_CRUD_MASTER", vendorparameters);

                        }



                        foreach (var items in master.VENDOR_REDELIVERY_PORT_LIST)
                        {
                            SqlParameter[] vendor_pickup_parameters =
                                     {
                              new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_VENDOR_REDELIVERY_PORT" },
                              new SqlParameter("@vendor_agreement_id", SqlDbType.Int) { Value = items.VENDOR_AGREEMENT_ID},
                              new SqlParameter("@vendor_agr_port_id", SqlDbType.Int) { Value = items.VENDOR_AGR_PORT_ID},
                              new SqlParameter("@redelivery_port_id", SqlDbType.Int) { Value = items.REDELIVERY_PORT_ID},
                              new SqlParameter("@modified_by", SqlDbType.Int) { Value = items.MODIFIED_BY },
                              new SqlParameter("@modified_at", SqlDbType.DateTime) { Value = items.MODIFIED_AT },
                         };

                            SqlHelper.ExecuteProcedureReturnStrings(conn, transaction, "SP_CRUD_MASTER", vendor_pickup_parameters);
                        }
                        transaction.Commit();

                    }
                }
                catch (Exception ex)
                {
                    // Rollback the transaction on error
                    transaction.Rollback();
                    throw new Exception("An error occurred while inserting the party master: " + ex.Message);
                }
            }
        }
        #endregion

        #region "CONTAINER MASTER"
        public void InsertContainerMaster(string connstring, CONTAINER_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
               {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_CONTAINER" },
                  new SqlParameter("@ID", SqlDbType.Int) { Value = master.ID},
                  new SqlParameter("@CONTAINER_NO", SqlDbType.VarChar,20) { Value = master.CONTAINER_NO},
                  new SqlParameter("@CONTAINER_TYPE", SqlDbType.VarChar, 50) { Value = master.CONTAINER_TYPE },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS },
                  new SqlParameter("@ONHIRE_DATE", SqlDbType.DateTime) { Value = master.ONHIRE_DATE },
                  new SqlParameter("@ONHIRE_LOCATION", SqlDbType.VarChar, 255) { Value = master.ONHIRE_LOCATION },
                  new SqlParameter("@LEASED_FROM", SqlDbType.VarChar,255) { Value = master.LEASED_FROM },

                //ADD NEW FIELD

                  new SqlParameter("@AGREEMENT_NO", SqlDbType.VarChar,100) { Value = master.AGREEMENT_NO },
                  new SqlParameter("@OFFHIRE_DATE", SqlDbType.DateTime) { Value = master.OFFHIRE_DATE },
                  new SqlParameter("@YEAR_OF_MANUFACTURE", SqlDbType.Decimal) { Value = master.YEAR_OF_MANUFACTURE },
                  new SqlParameter("@TARE_WEIGHT", SqlDbType.Decimal) { Value = master.TARE_WEIGHT },
                  new SqlParameter("@PAYLOAD_CAPACITY", SqlDbType.Decimal) { Value = master.PAYLOAD_CAPACITY },
                  new SqlParameter("@GROSS_WEIGHT", SqlDbType.Decimal) { Value = master.GROSS_WEIGHT },
                  new SqlParameter("@CSC_DETAILS", SqlDbType.VarChar,100) { Value = master.CSC_DETAILS },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CONTAINER_MASTER", parameters);

            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<CONTAINER_MASTER> GetContainerMasterList(string dbConn, string ContainerNo, string ContType, string ContSize, bool Status, string ONHIRE_DATE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_CONTAINERLIST" },
                  new SqlParameter("@CONTAINER_NO", SqlDbType.VarChar, 20) { Value = ContainerNo },
                  new SqlParameter("@CONTAINER_TYPE", SqlDbType.VarChar, 50) { Value = ContType },
                  new SqlParameter("@CONTAINER_SIZE", SqlDbType.VarChar, 20) { Value = ContSize },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = Status },
                  new SqlParameter("@ONHIRE_DATE", SqlDbType.DateTime) { Value = ONHIRE_DATE },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_CONTAINER_MASTER", parameters);
                List<CONTAINER_MASTER> master = SqlHelper.CreateListFromTable<CONTAINER_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public CONTAINER_MASTER GetContainerMasterDetails(string connstring, int ID, string CONTAINER_NO)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@CONTAINER_NO", SqlDbType.VarChar, 20) { Value = CONTAINER_NO },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "GET_CONTAINERDETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<CONTAINER_MASTER>(connstring, "SP_CRUD_CONTAINER_MASTER", r => r.TranslateAsContainer(), parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateContainerMasterList(string connstring, CONTAINER_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,255) { Value = "UPDATE_CONTAINER" },
                  new SqlParameter("@ID", SqlDbType.Int) { Value = master.ID},
                  new SqlParameter("@CONTAINER_NO", SqlDbType.VarChar,20) { Value = master.CONTAINER_NO},
                  new SqlParameter("@CONTAINER_TYPE", SqlDbType.VarChar, 50) { Value = master.CONTAINER_TYPE },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS },
                  new SqlParameter("@ONHIRE_DATE", SqlDbType.DateTime) { Value = master.ONHIRE_DATE },
                  new SqlParameter("@ONHIRE_LOCATION", SqlDbType.VarChar, 255) { Value = master.ONHIRE_LOCATION },
                  new SqlParameter("@LEASED_FROM", SqlDbType.VarChar,255) { Value = master.LEASED_FROM },
                  
                //ADD NEW FIELD

                  new SqlParameter("@AGREEMENT_NO", SqlDbType.VarChar,100) { Value = master.AGREEMENT_NO },
                  new SqlParameter("@OFFHIRE_DATE", SqlDbType.DateTime) { Value = master.OFFHIRE_DATE },
                  new SqlParameter("@YEAR_OF_MANUFACTURE", SqlDbType.Decimal) { Value = master.YEAR_OF_MANUFACTURE },
                  new SqlParameter("@TARE_WEIGHT", SqlDbType.Decimal) { Value = master.TARE_WEIGHT },
                  new SqlParameter("@PAYLOAD_CAPACITY", SqlDbType.Decimal) { Value = master.PAYLOAD_CAPACITY },
                  new SqlParameter("@GROSS_WEIGHT", SqlDbType.Decimal) { Value = master.GROSS_WEIGHT },
                  new SqlParameter("@CSC_DETAILS", SqlDbType.VarChar,100) { Value = master.CSC_DETAILS },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CONTAINER_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteContainerMaster(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "DELETE_CONTAINER" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CONTAINER_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region "COMMON MASTER"
        public void InsertMaster(string connstring, MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,255) { Value = "INSERT_MASTER" },
                  new SqlParameter("@ID", SqlDbType.Int) { Value = master.ID},
                  new SqlParameter("@KEY_NAME", SqlDbType.VarChar, 100) { Value = master.KEY_NAME },
                  new SqlParameter("@CODE", SqlDbType.VarChar, 255) { Value = master.CODE },
                  new SqlParameter("@CODE_DESC", SqlDbType.VarChar, 255) { Value = master.CODE_DESC },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS },
                  new SqlParameter("@PARENT_CODE", SqlDbType.VarChar, 100) { Value = master.PARENT_CODE },
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar,255) { Value = master.CREATED_BY },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<MASTER> GetMasterList(string dbConn, string key, string FROM_DATE, string TO_DATE, string STATUS)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_MASTER_LIST" },
                  new SqlParameter("@KEY_NAME", SqlDbType.VarChar, 100) { Value =  key},
                  new SqlParameter("@FROM_DATE", SqlDbType.DateTime) { Value =  FROM_DATE},
                  new SqlParameter("@TO_DATE", SqlDbType.DateTime) { Value =  TO_DATE},
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value =  STATUS},

                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_MASTER", parameters);
                List<MASTER> master = SqlHelper.CreateListFromTable<MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MASTER GetMasterDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {

                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_MASTER_DETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<MASTER>(connstring, "SP_MASTER", r => r.TranslateAsMaster(), parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateMaster(string connstring, MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
               {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,255) { Value = "UPDATE_MASTER" },
                  new SqlParameter("@ID", SqlDbType.Int) { Value = master.ID},
                  new SqlParameter("@KEY_NAME", SqlDbType.VarChar, 100) { Value = master.KEY_NAME},
                  new SqlParameter("@CODE", SqlDbType.VarChar, 255) { Value = master.CODE },
                  new SqlParameter("@CODE_DESC", SqlDbType.VarChar, 255) { Value = master.CODE_DESC },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS },
                  new SqlParameter("@PARENT_CODE", SqlDbType.VarChar, 100) { Value = master.PARENT_CODE },
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar,255) { Value = master.CREATED_BY },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteMaster(string connstring, int ID)
        {
            SqlParameter[] parameters =
            {
              new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
               new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "DELETE_MASTER" }
            };

            SqlHelper.ExecuteProcedureReturnString(connstring, "SP_MASTER", parameters);
        }
        #endregion

        #region "VESSEL_MASTER"
        public void InsertVesselMaster(string connstring, VESSEL_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,255) { Value = "INSERT_VESSEEL" },
                  new SqlParameter("@VESSEL_NAME", SqlDbType.VarChar,255) { Value = master.VESSEL_NAME},
                  new SqlParameter("@IMO_NO", SqlDbType.VarChar,11) { Value = master.IMO_NO},
                  new SqlParameter("@COUNTRY_CODE  ", SqlDbType.VarChar, 5) { Value = master.COUNTRY_CODE   },
                  new SqlParameter("@VESSEL_CODE", SqlDbType.VarChar, 8) { Value = master.VESSEL_CODE },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS},
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar,255) { Value = master.CREATED_BY },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_VESSEL_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<VESSEL_MASTER> GetVesselMasterList(string dbConn, string VESSEL_NAME, string IMO_NO, string STATUS, string FROM_DATE, string TO_DATE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_VESSELLIST" },
                  new SqlParameter("@VESSEL_NAME", SqlDbType.VarChar, 255) { Value = VESSEL_NAME },
                  new SqlParameter("@IMO_NO", SqlDbType.VarChar, 11) { Value = IMO_NO },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = STATUS },
                  new SqlParameter("@FROM_DATE", SqlDbType.DateTime) { Value = FROM_DATE },
                  new SqlParameter("@TO_DATE", SqlDbType.DateTime) { Value = TO_DATE },

                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_VESSEL_MASTER", parameters);
                List<VESSEL_MASTER> master = SqlHelper.CreateListFromTable<VESSEL_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public VESSEL_MASTER GetVesselMasterDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_VESSELDETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<VESSEL_MASTER>(connstring, "SP_CRUD_VESSEL_MASTER", r => r.TranslateAsVessel(), parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateVesselMasterList(string connstring, VESSEL_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,255) { Value = "UPDATE_VESSEL" },
                  new SqlParameter("@ID", SqlDbType.Int) { Value = master.ID},
                  new SqlParameter("@VESSEL_NAME", SqlDbType.VarChar,255) { Value = master.VESSEL_NAME},
                  new SqlParameter("@IMO_NO", SqlDbType.VarChar, 11) { Value = master.IMO_NO },
                   new SqlParameter("@COUNTRY_CODE", SqlDbType.VarChar, 5) { Value = master.COUNTRY_CODE },
                  new SqlParameter("@VESSEL_CODE", SqlDbType.VarChar,8) { Value = master.VESSEL_CODE },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_VESSEL_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void DeleteVesselMasterList(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "DELETE_VESSEL" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_VESSEL_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region "SERVICE MASTER"
        public void InsertServiceMaster(string connstring, SERVICE_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,255) { Value = "INSERT_SERVICE" },
                  new SqlParameter("@LINER_CODE", SqlDbType.VarChar,100) { Value = master.LINER_CODE},
                  new SqlParameter("@SERVICE_NAME", SqlDbType.VarChar,255) { Value = master.SERVICE_NAME},
                  new SqlParameter("@PORT_CODE  ", SqlDbType.VarChar, 100) { Value = master.PORT_CODE   },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS},
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar,255) { Value = master.CREATED_BY },

                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_SERVICE_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<SERVICE_MASTER> GetServiceMasterList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_SERVICELIST" },

                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_SERVICE_MASTER", parameters);
                List<SERVICE_MASTER> master = SqlHelper.CreateListFromTable<SERVICE_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public SERVICE_MASTER GetServiceMasterDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_SERVICEDETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<SERVICE_MASTER>(connstring, "SP_CRUD_SERVICE_MASTER", r => r.TranslateAsService(), parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateServiceMasterList(string connstring, SERVICE_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
               {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,255) { Value = "UPDATE_SERVICE" },
                  new SqlParameter("@ID", SqlDbType.Int) { Value = master.ID },
                  new SqlParameter("@LINER_CODE", SqlDbType.VarChar,100) { Value = master.LINER_CODE},
                  new SqlParameter("@SERVICE_NAME", SqlDbType.VarChar,255) { Value = master.SERVICE_NAME},
                  new SqlParameter("@PORT_CODE  ", SqlDbType.VarChar, 100) { Value = master.PORT_CODE   },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS},
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_SERVICE_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteServiceMasterList(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "DELETE_SERVICE" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_SERVICE_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region "CONTAINER TYPE MASTER"
        public void InsertContainerTypeMaster(string connstring, CONTAINER_TYPE master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,255) { Value = "INSERT_CONT_TYPE" },
                  new SqlParameter("@CONT_TYPE_CODE", SqlDbType.VarChar,15) { Value = master.CONT_TYPE_CODE},
                  new SqlParameter("@CONT_TYPE  ", SqlDbType.VarChar, 50) { Value = master.CONT_TYPE   },
                  new SqlParameter("@CONT_SIZE",SqlDbType.Int){Value=master.CONT_SIZE},
                  new SqlParameter("@ISO_CODE",SqlDbType.VarChar,50){Value=master.ISO_CODE},
                  new SqlParameter("@TEUS",SqlDbType.Int){Value=master.TEUS},
                  new SqlParameter("@OUT_DIM",SqlDbType.VarChar,100){Value=master.OUT_DIM},
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS},
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar,255) { Value = master.CREATED_BY },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_MST_CONT_TYPE", parameters);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<CONTAINER_TYPE> GetContainerTypeMasterList(string dbConn, string ContTypeCode, string ContType, string ContSize, bool Status, string FROM_DATE, string TO_DATE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "GET_CONT_TYPE_LIST" },
                  new SqlParameter("@CONT_TYPE_CODE", SqlDbType.VarChar, 15) { Value = ContTypeCode },
                  new SqlParameter("@CONT_TYPE", SqlDbType.VarChar, 50) { Value = ContType },
                  new SqlParameter("@CONT_SIZE", SqlDbType.Int) { Value = ContSize },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = Status },
                  new SqlParameter("@FROM_DATE", SqlDbType.DateTime) { Value = FROM_DATE },
                  new SqlParameter("@TO_DATE", SqlDbType.DateTime, 20) { Value = TO_DATE },







                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_MST_CONT_TYPE", parameters);
                List<CONTAINER_TYPE> master = SqlHelper.CreateListFromTable<CONTAINER_TYPE>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public CONTAINER_TYPE GetContainerTypeMasterDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_CONT_TYPE_DETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<CONTAINER_TYPE>(connstring, "SP_MST_CONT_TYPE", r => r.TranslateAsContainerType(), parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateConatinerTypeMaster(string connstring, CONTAINER_TYPE master)
        {
            try
            {
                SqlParameter[] parameters =
               {
                 new SqlParameter("@OPERATION", SqlDbType.VarChar,255) { Value = "UPDATE_CONT_TYPE" },
                  new SqlParameter("@ID", SqlDbType.Int) { Value = master.ID },
                  new SqlParameter("@CONT_TYPE_CODE", SqlDbType.VarChar,15) { Value = master.CONT_TYPE_CODE},
                  new SqlParameter("@CONT_TYPE  ", SqlDbType.VarChar, 50) { Value = master.CONT_TYPE   },
                  new SqlParameter("@CONT_SIZE",SqlDbType.Int){Value=master.CONT_SIZE},
                  new SqlParameter("@ISO_CODE",SqlDbType.VarChar,50){Value=master.ISO_CODE},
                  new SqlParameter("@TEUS",SqlDbType.Int){Value=master.TEUS},
                  new SqlParameter("@OUT_DIM",SqlDbType.VarChar,100){Value=master.OUT_DIM},
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS},

                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_MST_CONT_TYPE", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteContainerTypeMaster(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "DELETE_CONT_TYPE" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_MST_CONT_TYPE", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region ICD MASTER"
        public List<ICD_MASTER> GetICDMasterList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_MST_ICD" },

                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_MASTER", parameters);
                List<ICD_MASTER> master = SqlHelper.CreateListFromTable<ICD_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        #region DEPO MASTER"
        public List<DEPO_MASTER> GetDEPOMasterList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_MST_DEPO" },

                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_MASTER", parameters);
                List<DEPO_MASTER> master = SqlHelper.CreateListFromTable<DEPO_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        #region TERMINAL MASTER"
        public List<TERMINAL_MASTER> GetTerminalMasterList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_MST_TERMINAL" },

                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_MASTER", parameters);
                List<TERMINAL_MASTER> master = SqlHelper.CreateListFromTable<TERMINAL_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        #region CLEARING PARTY"
        public List<CLEARING_PARTY> GetClearingPartyList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_CLEARING_PARTY_LIST" },

                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_CLEARING_PARTY", parameters);
                List<CLEARING_PARTY> master = SqlHelper.CreateListFromTable<CLEARING_PARTY>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void InsertCP(string connstring, CLEARING_PARTY request)
        {
            try
            {
                SqlParameter[] parameters =
                {

                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "CREATE_CLEARING_PARTY" },
                  new SqlParameter("@NAME", SqlDbType.VarChar,100) { Value = request.NAME },
                  new SqlParameter("@EMAIL_ID", SqlDbType.VarChar,255) { Value = request.EMAIL_ID },
                  new SqlParameter("@ADDRESS", SqlDbType.VarChar, 255) { Value = request.ADDRESS },
                  new SqlParameter("@CONTACT", SqlDbType.VarChar, 20) { Value = request.CONTACT },
                  new SqlParameter("@LOCATION", SqlDbType.VarChar,50) { Value = request.LOCATION },
                  new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 50) { Value = request.AGENT_CODE },
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 100) { Value = request.CREATED_BY }

                };

                //SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_DO", parameters);

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CLEARING_PARTY", parameters);


            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "LINER"
        public void InsertLiner(string connstring, LINER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,255) { Value = "INSERT_LINER" },
                  new SqlParameter("@NAME",SqlDbType.VarChar,255){Value=master.NAME},
                  new SqlParameter("@CODE",SqlDbType.VarChar,50){Value=master.CODE},
                  new SqlParameter("@DESCRIPTION",SqlDbType.VarChar,255){Value=master.DESCRIPTION},

                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS},

                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar,255) { Value = master.CREATED_BY },

                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_LINER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<LINER> GetLinerList(string dbConn, string Name, string Code, string Description, bool Status, string FROM_DATE, string TO_DATE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_LINER_LIST" },
                  new SqlParameter("@NAME", SqlDbType.VarChar, 255) { Value = Name },
                  new SqlParameter("@CODE", SqlDbType.VarChar, 50) { Value = Code },
                  new SqlParameter("@DESCRIPTION", SqlDbType.VarChar, 255) { Value = Description },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = Status },
                  new SqlParameter("@FROM_DATE", SqlDbType.DateTime) { Value = FROM_DATE },
                  new SqlParameter("@TO_DATE", SqlDbType.DateTime) { Value = TO_DATE },


                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_LINER", parameters);
                List<LINER> master = SqlHelper.CreateListFromTable<LINER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public LINER GetLinerDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_LINER_DETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<LINER>(connstring, "SP_LINER", r => r.TranslateAsLiner(), parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateLinerList(string connstring, LINER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,255) { Value = "UPDATE_LINER" },
                  new SqlParameter("@ID",SqlDbType.Int){Value=master.ID},
                  new SqlParameter("@NAME",SqlDbType.VarChar,255){Value=master.NAME},
                  new SqlParameter("@CODE",SqlDbType.VarChar,50){Value=master.CODE},
                  new SqlParameter("@DESCRIPTION",SqlDbType.VarChar,255){Value=master.DESCRIPTION},

                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS},


                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_LINER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteLinerList(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "DELETE_LINER" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_LINER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion

        #region "LINERSERVICE"
        public void InsertService(string connstring, SERVICE master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,255) { Value = "INSERT_SERVICE" },
                  new SqlParameter("@LINER_CODE",SqlDbType.VarChar,100){Value=master.LINER_CODE},
                  new SqlParameter("SERVICE_NAME",SqlDbType.VarChar,255){Value=master.SERVICE_NAME},
                  new SqlParameter("@PORT_CODE",SqlDbType.VarChar,100){Value=master.PORT_CODE},

                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS},

                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar,255) { Value = master.CREATED_BY },

                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_SERVICE_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<SERVICE> GetServiceList(string dbConn, bool Status, string FROM_DATE, string TO_DATE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                 new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_SERVICELIST" },
                 new SqlParameter("@STATUS", SqlDbType.Bit) { Value = Status },
                 new SqlParameter("@FROM_DATE", SqlDbType.DateTime) { Value = FROM_DATE },
                 new SqlParameter("@TO_DATE", SqlDbType.DateTime) { Value = TO_DATE },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_SERVICE_MASTER", parameters);
                List<SERVICE> master = SqlHelper.CreateListFromTable<SERVICE>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public SERVICE GetServiceDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_SERVICEDETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<SERVICE>(connstring, "SP_CRUD_SERVICE_MASTER", r => r.TranslateAsLinerService(), parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateService(string connstring, SERVICE master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,255) { Value = "UPDATE_SERVICE" },
                  new SqlParameter("@ID",SqlDbType.Int){Value=master.ID},
                  new SqlParameter("@LINER_CODE",SqlDbType.VarChar,100){Value=master.LINER_CODE},
                  new SqlParameter("@SERVICE_NAME",SqlDbType.VarChar,255){Value=master.SERVICE_NAME},
                  new SqlParameter("@PORT_CODE",SqlDbType.VarChar,100){Value=master.PORT_CODE},

                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS},


                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_SERVICE_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteService(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "DELETE_SERVICE" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_SERVICE_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }



        #endregion

        #region "VESSEL SCHEDULE"
        public void InsertSchedule(string connstring, SCHEDULE master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,255) { Value = "INSERT_VESSEL_SCHEDULE" },
                  new SqlParameter("@VESSEL_NAME",SqlDbType.VarChar,255){Value=master.VESSEL_NAME},
                  new SqlParameter("SERVICE_NAME",SqlDbType.VarChar,255){Value=master.SERVICE_NAME},
                  new SqlParameter("@PORT_CODE",SqlDbType.VarChar,100){Value=master.PORT_CODE},
                  new SqlParameter("@VIA_NO",SqlDbType.VarChar,100){Value=master.VIA_NO},
                  new SqlParameter("@ETA",SqlDbType.DateTime){Value=master.ETA},
                  new SqlParameter("@ETD",SqlDbType.DateTime){Value=master.ETD},
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS},
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar,255) { Value = master.CREATED_BY },

                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_VESSEL_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<SCHEDULE> GetScheduleList(string dbConn, string VesselName, string PortCode, bool status, string ETA, string ETD)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_VESSEL_SCHEDULELIST" },
                  new SqlParameter("@VESSEL_NAME", SqlDbType.VarChar, 255) { Value = VesselName },
                  new SqlParameter("@PORT_CODE", SqlDbType.VarChar, 100) { Value = PortCode },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = status },
                  new SqlParameter("@ETA", SqlDbType.DateTime) { Value = ETA },
                   new SqlParameter("@ETD", SqlDbType.DateTime) { Value = ETD },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_VESSEL_MASTER", parameters);
                List<SCHEDULE> master = SqlHelper.CreateListFromTable<SCHEDULE>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public SCHEDULE GetScheduleDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_VESSEL_SCHEDULEDETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<SCHEDULE>(connstring, "SP_CRUD_VESSEL_MASTER", r => r.TranslateAsSchedule(), parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateSchedule(string connstring, SCHEDULE master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,255) { Value = "UPDATE_VESSEL_SCHEDULE" },
                  new SqlParameter("@ID",SqlDbType.Int){Value=master.ID},
                  new SqlParameter("@VESSEL_NAME",SqlDbType.VarChar,255){Value=master.VESSEL_NAME},
                  new SqlParameter("@SERVICE_NAME",SqlDbType.VarChar,255){Value=master.SERVICE_NAME},
                  new SqlParameter("@PORT_CODE",SqlDbType.VarChar,100){Value=master.PORT_CODE},
                  new SqlParameter("@VIA_NO",SqlDbType.VarChar,100){Value=master.VIA_NO},
                  new SqlParameter("@ETA",SqlDbType.DateTime){Value=master.ETA},
                  new SqlParameter("@ETD",SqlDbType.DateTime){Value=master.ETD},
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS},
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_VESSEL_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteSchedule(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "DELETE_VESSEL_SCHEDULE" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_VESSEL_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region "VESSEL VOYAGE"
        public List<VOYAGE> GetVoyageList(string dbConn, bool status, string FROM_DATE, string TO_DATE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_VESSEL_VOYAGELIST" },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = status },
                  new SqlParameter("@FROM_DATE", SqlDbType.DateTime) { Value = FROM_DATE },
                   new SqlParameter("@TO_DATE", SqlDbType.DateTime) { Value = TO_DATE },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_VESSEL_MASTER", parameters);
                List<VOYAGE> master = SqlHelper.CreateListFromTable<VOYAGE>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public VOYAGE GetVoyageDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_VESSEL_VOYAGEDETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<VOYAGE>(connstring, "SP_CRUD_VESSEL_MASTER", r => r.TranslateAsVoyage(), parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateVoyage(string connstring, VOYAGE master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,255) { Value = "UPDATE_VESSEL_VOYAGEDETAILS" },
                  new SqlParameter("@ID",SqlDbType.Int){Value=master.ID},
                  new SqlParameter("@VESSEL_NAME",SqlDbType.VarChar,255){Value=master.VESSEL_NAME},
                  new SqlParameter("@VOYAGE_NO",SqlDbType.VarChar,20){Value=master.VOYAGE_NO},
                  new SqlParameter("@ATA",SqlDbType.DateTime){Value=master.ATA},
                  new SqlParameter("@ATD",SqlDbType.DateTime){Value=master.ATD},
                  //new SqlParameter("@IMM_CURR",SqlDbType.VarChar,50){Value=master.IMM_CURR},
                  //new SqlParameter("@IMM_CURR_RATE", SqlDbType.Decimal) { Value = master.IMM_CURR_RATE},
                  //new SqlParameter("@EXP_CURR",SqlDbType.VarChar,50){Value=master.EXP_CURR},
                  //new SqlParameter("@EXP_CURR_RATE", SqlDbType.Decimal) { Value = master.EXP_CURR_RATE},
                  new SqlParameter("@TERMINAL_CODE", SqlDbType.VarChar,255) { Value = master.TERMINAL_CODE},
                  new SqlParameter("@SERVICE_NAME", SqlDbType.VarChar,255) { Value = master.SERVICE_NAME},
                  new SqlParameter("@VIA_NO", SqlDbType.VarChar,100) { Value = master.VIA_NO},
                  new SqlParameter("@PORT_CODE", SqlDbType.VarChar,255) { Value = master.PORT_CODE},
                  new SqlParameter("@ETA", SqlDbType.DateTime) { Value = master.ETA},
                  new SqlParameter("@ETD", SqlDbType.DateTime) { Value = master.ETD},
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS},
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_VESSEL_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteVoyage(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "DELETE_VESSEL_VOYAGE" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_VESSEL_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region "LOCATION MASTER"
        public void InsertLocationMaster(string connstring, LOCATION_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
               {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_LOCATION" },
                  new SqlParameter("@LOC_NAME", SqlDbType.VarChar,500) { Value = master.LOC_NAME},
                  new SqlParameter("@LOC_CODE", SqlDbType.VarChar, 50) { Value = master.LOC_CODE },
                  new SqlParameter("@IS_DEPO", SqlDbType.Bit) { Value = master.IS_DEPO },
                  new SqlParameter("@IS_CFS", SqlDbType.Bit) { Value = master.IS_CFS },
                  new SqlParameter("@IS_TERMINAL", SqlDbType.Bit) { Value = master.IS_TERMINAL },
                  new SqlParameter("@IS_YARD", SqlDbType.Bit) { Value = master.IS_YARD },
                  new SqlParameter("@IS_ICD", SqlDbType.Bit) { Value = master.IS_ICD },
                  new SqlParameter("@ADDRESS", SqlDbType.VarChar,500) { Value = master.ADDRESS },
                  new SqlParameter("@COUNTRY_CODE", SqlDbType.VarChar, 10) { Value = master.COUNTRY_CODE },
                  new SqlParameter("@PORT_CODE", SqlDbType.VarChar,255) { Value = master.PORT_CODE },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS },
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar,50) { Value = master.CREATED_BY },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_LOCATION_MASTER", parameters);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<LOCATION_MASTER> GetLocationMasterList(string dbConn, string LOC_NAME, string LOC_TYPE, bool STATUS, string FROM_DATE, string TO_DATE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_LOCATIONLIST" },
                  new SqlParameter("@LOC_NAME", SqlDbType.VarChar, 255) { Value = LOC_NAME },
                  new SqlParameter("@LOC_TYPE", SqlDbType.VarChar, 50) { Value = LOC_TYPE },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = STATUS },
                  new SqlParameter("@FROM_DATE", SqlDbType.DateTime) { Value = FROM_DATE },
                  new SqlParameter("@TO_DATE", SqlDbType.DateTime) { Value = TO_DATE },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_LOCATION_MASTER", parameters);
                List<LOCATION_MASTER> master = SqlHelper.CreateListFromTable<LOCATION_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public LOCATION_MASTER GetLocationMasterDetails(string connstring, string LOC_CODE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@LOC_CODE", SqlDbType.VarChar, 20) { Value = LOC_CODE },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "GET_LOCATIONDETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<LOCATION_MASTER>(connstring, "SP_CRUD_LOCATION_MASTER", r => r.TranslateAsLocation(), parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateLocationMasterList(string connstring, LOCATION_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,255) { Value = "UPDATE_LOCATION" },
                  new SqlParameter("@LOC_NAME", SqlDbType.VarChar,500) { Value = master.LOC_NAME},
                  new SqlParameter("@LOC_CODE", SqlDbType.VarChar, 20) { Value = master.LOC_CODE },
                  new SqlParameter("@IS_DEPO", SqlDbType.Bit) { Value = master.IS_DEPO },
                  new SqlParameter("@IS_CFS", SqlDbType.Bit) { Value = master.IS_CFS },
                  new SqlParameter("@IS_TERMINAL", SqlDbType.Bit) { Value = master.IS_TERMINAL },
                  new SqlParameter("@IS_YARD", SqlDbType.Bit) { Value = master.IS_YARD },
                  new SqlParameter("@IS_ICD", SqlDbType.Bit) { Value = master.IS_ICD },
                  new SqlParameter("@ADDRESS", SqlDbType.VarChar,500) { Value = master.ADDRESS },
                  new SqlParameter("@PORT_CODE", SqlDbType.VarChar,255) { Value = master.PORT_CODE },
                  new SqlParameter("@COUNTRY_CODE", SqlDbType.VarChar,20) { Value = master.COUNTRY_CODE },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_LOCATION_MASTER", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteLocationMaster(string connstring, string LOC_CODE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@LOC_CODE", SqlDbType.VarChar,20) { Value = LOC_CODE },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "DELETE_LOCATION" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_LOCATION_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region "FREIGHT MASTER"
        public void InsertFreightMaster(string connstring, FREIGHT_MASTER master)
        {
            try
            {
                if (master.CHARGELIST.Count > 0)
                {
                    foreach (var i in master.CHARGELIST)
                    {
                        SqlParameter[] param1 =
                        {
                           new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_CHARGE" },
                           new SqlParameter("@POL", SqlDbType.VarChar,100) { Value = i.POL},
                           new SqlParameter("@CHARGE_CODE", SqlDbType.VarChar,100) { Value = i.CHARGE_CODE },
                           new SqlParameter("@IMPCOST20", SqlDbType.Decimal) { Value = i.IMPCOST20 },
                           new SqlParameter("@IMPCOST40", SqlDbType.Decimal) { Value = i.IMPCOST40 },
                           new SqlParameter("@IMPREVENUE20", SqlDbType.Decimal) { Value = i.IMPINCOME20 },
                           new SqlParameter("@IMPREVENUE40", SqlDbType.Decimal) { Value = i.IMPINCOME40 },
                           new SqlParameter("@EXPCOST20", SqlDbType.Decimal) { Value = i.EXPCOST20 },
                           new SqlParameter("@EXPCOST40", SqlDbType.Decimal) { Value = i.EXPCOST40 },
                           new SqlParameter("@EXPREVENUE20", SqlDbType.Decimal) { Value = i.EXPINCOME20 },
                           new SqlParameter("@EXPREVENUE40", SqlDbType.Decimal) { Value = i.EXPINCOME40 },
                           new SqlParameter("@Currency", SqlDbType.VarChar,10) { Value = i.CURRENCY },
                           new SqlParameter("@FROM_VAL", SqlDbType.Int) { Value = i.FROM_VAL },
                           new SqlParameter("@TO_VAL", SqlDbType.Int) { Value = i.TO_VAL },
                        };

                        SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", param1);
                    }
                }
                else
                {
                    SqlParameter[] parameters =
                    {
                      new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_FREIGHT" },
                      new SqlParameter("@POL", SqlDbType.VarChar,100) { Value = master.POL},
                      new SqlParameter("@POD", SqlDbType.VarChar, 100) { Value = master.POD },
                      new SqlParameter("@Charge", SqlDbType.VarChar,100) { Value = master.Charge },
                      new SqlParameter("@Currency", SqlDbType.VarChar, 10) { Value = master.Currency },
                      new SqlParameter("@LadenStatus", SqlDbType.Char,1) { Value = master.LadenStatus },
                      new SqlParameter("@ServiceMode", SqlDbType.VarChar,20) { Value = master.ServiceMode },
                      new SqlParameter("@DRY20", SqlDbType.Decimal) { Value = master.DRY20 },
                    };

                    SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<FREIGHT_MASTER> GetFreightMasterList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_FREIGHTLIST" },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_CHARGE_MASTER", parameters);
                List<FREIGHT_MASTER> master = SqlHelper.CreateListFromTable<FREIGHT_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public FREIGHT_MASTER GetFreightMasterDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "GET_FREIGHTDETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<FREIGHT_MASTER>(connstring, "SP_CRUD_CHARGE_MASTER", r => r.TranslateAsFreightMaster(), parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateFreightMasterList(string connstring, FREIGHT_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_FREIGHT" },
                  new SqlParameter("@ID", SqlDbType.Int) { Value = master.ID },
                  new SqlParameter("@POL", SqlDbType.VarChar,100) { Value = master.POL},
                  new SqlParameter("@POD", SqlDbType.VarChar, 100) { Value = master.POD },
                  new SqlParameter("@Charge", SqlDbType.VarChar,100) { Value = master.Charge },
                  new SqlParameter("@Currency", SqlDbType.VarChar, 10) { Value = master.Currency },
                  new SqlParameter("@LadenStatus", SqlDbType.Char,1) { Value = master.LadenStatus },
                  new SqlParameter("@ServiceMode", SqlDbType.VarChar,20) { Value = master.ServiceMode },
                  new SqlParameter("@DRY20", SqlDbType.Decimal) { Value = master.DRY20 },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteFreightMaster(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "DELETE_FREIGHT" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region "CHARGE MASTER"
        public List<CHARGE_MASTER> GetChargeMasterList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_CHARGELIST" },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_CHARGE_MASTER", parameters);
                List<CHARGE_MASTER> master = SqlHelper.CreateListFromTable<CHARGE_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public CHARGE_MASTER GetChargeMasterDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "GET_CHARGEDETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<CHARGE_MASTER>(connstring, "SP_CRUD_CHARGE_MASTER", r => r.TranslateAsChargeMaster(), parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateChargeMasterList(string connstring, CHARGE_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_CHARGE" },
                  new SqlParameter("@ID", SqlDbType.Int) { Value = master.ID },
                  new SqlParameter("@POL", SqlDbType.VarChar,100) { Value = master.POL},
                  new SqlParameter("@CHARGE_CODE", SqlDbType.VarChar,100) { Value = master.CHARGE_CODE },
                  new SqlParameter("@Currency", SqlDbType.VarChar,100) { Value = master.CURRENCY },
                  new SqlParameter("@IMPCOST20", SqlDbType.Decimal) { Value = master.IMPCOST20 },
                  new SqlParameter("@IMPCOST40", SqlDbType.Decimal) { Value = master.IMPCOST40 },
                  new SqlParameter("@IMPREVENUE20", SqlDbType.Decimal) { Value = master.IMPINCOME20 },
                  new SqlParameter("@IMPREVENUE40", SqlDbType.Decimal) { Value = master.IMPINCOME40 },
                  new SqlParameter("@EXPCOST20", SqlDbType.Decimal) { Value = master.EXPCOST20 },
                  new SqlParameter("@EXPCOST40", SqlDbType.Decimal) { Value = master.EXPCOST40 },
                  new SqlParameter("@EXPREVENUE20", SqlDbType.Decimal) { Value = master.EXPINCOME20 },
                  new SqlParameter("@EXPREVENUE40", SqlDbType.Decimal) { Value = master.EXPINCOME40 },
                  new SqlParameter("@FROM_VAL", SqlDbType.Int) { Value = master.FROM_VAL },
                  new SqlParameter("@TO_VAL", SqlDbType.Int) { Value = master.TO_VAL },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteChargeMaster(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "DELETE_CHARGE" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region "STEVEDORING MASTER"
        public List<STEV_MASTER> GetStevedoringMasterList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_STEVLIST" },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_CHARGE_MASTER", parameters);
                List<STEV_MASTER> master = SqlHelper.CreateListFromTable<STEV_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public STEV_MASTER GetStevedoringMasterDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "GET_STEVDETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<STEV_MASTER>(connstring, "SP_CRUD_CHARGE_MASTER", r => r.TranslateAsStevMaster(), parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateStevedoringMasterList(string connstring, STEV_MASTER i)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_STEV" },
                  new SqlParameter("@ID", SqlDbType.Int) { Value = i.ID},
                  new SqlParameter("@IE_TYPE", SqlDbType.VarChar,255) { Value = i.IE_TYPE},
                  new SqlParameter("@POL", SqlDbType.VarChar,100) { Value = i.POL},
                  new SqlParameter("@TERMINAL", SqlDbType.VarChar, 255) { Value = i.TERMINAL },
                  new SqlParameter("@CHARGE_CODE", SqlDbType.VarChar,100) { Value = i.CHARGE_CODE },
                  new SqlParameter("@CURRENCY", SqlDbType.VarChar, 10) { Value = i.CURRENCY },
                  new SqlParameter("@LadenStatus", SqlDbType.Char,1) { Value = i.LADEN_STATUS },
                  new SqlParameter("@ServiceMode", SqlDbType.VarChar,20) { Value = i.SERVICE_MODE },
                  new SqlParameter("@DRY20", SqlDbType.Decimal) { Value = i.DRY20 },
                  new SqlParameter("@DRY40", SqlDbType.Decimal) { Value = i.DRY40 },
                  new SqlParameter("@DRY40HC", SqlDbType.Decimal) { Value = i.DRY40HC },
                  new SqlParameter("@DRY45", SqlDbType.Decimal) { Value = i.DRY45 },
                  new SqlParameter("@RF20", SqlDbType.Decimal) { Value = i.RF20 },
                  new SqlParameter("@RF40", SqlDbType.Decimal) { Value = i.RF40 },
                  new SqlParameter("@RF40HC", SqlDbType.Decimal) { Value = i.RF40HC },
                  new SqlParameter("@RF45", SqlDbType.Decimal) { Value = i.RF45 },
                  new SqlParameter("@HAZ20", SqlDbType.Decimal) { Value = i.HAZ20 },
                  new SqlParameter("@HAZ40", SqlDbType.Decimal) { Value = i.HAZ40 },
                  new SqlParameter("@HAZ40HC", SqlDbType.Decimal) { Value = i.HAZ40HC },
                  new SqlParameter("@HAZ45", SqlDbType.Decimal) { Value = i.HAZ45 },
                  new SqlParameter("@SEQ20", SqlDbType.Decimal) { Value = i.SEQ20 },
                  new SqlParameter("@SEQ40", SqlDbType.Decimal) { Value = i.SEQ40 },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteStevedoringMaster(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "DELETE_STEV" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region "DETENTION MASTER"
        public List<DETENTION_MASTER> GetDetentionMasterList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_DETENTIONLIST" },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_CHARGE_MASTER", parameters);
                List<DETENTION_MASTER> master = SqlHelper.CreateListFromTable<DETENTION_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DETENTION_MASTER GetDetentionMasterDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "GET_DETENTIONDETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<DETENTION_MASTER>(connstring, "SP_CRUD_CHARGE_MASTER", r => r.TranslateAsDetentionMaster(), parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateDetentionMasterList(string connstring, DETENTION_MASTER i)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_DETENTION" },
                    new SqlParameter("@ID", SqlDbType.Int) { Value = i.ID },
                    new SqlParameter("@PORT_CODE", SqlDbType.VarChar,50) { Value = i.PORT_CODE},
                    new SqlParameter("@CONTAINER_TYPE", SqlDbType.VarChar,20) { Value = i.CONTAINER_TYPE},
                    new SqlParameter("@CURRENCY", SqlDbType.VarChar, 10) { Value = i.CURRENCY },
                    new SqlParameter("@FROM_DAYS", SqlDbType.Int) { Value = i.FROM_DAYS },
                    new SqlParameter("@TO_DAYS", SqlDbType.Int) { Value = i.TO_DAYS },
                    new SqlParameter("@RATE20", SqlDbType.Decimal) { Value = i.RATE20 },
                    new SqlParameter("@RATE40", SqlDbType.Decimal) { Value = i.RATE40 },
                    new SqlParameter("@HC_RATE", SqlDbType.Decimal) { Value = i.HC_RATE },
                    new SqlParameter("@CREATED_BY", SqlDbType.VarChar,255) { Value = i.CREATED_BY },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteDetentionMaster(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "DELETE_DETENTION" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region "MANDATORY MASTER"
        public List<MANDATORY_MASTER> GetMandatoryMasterList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_MANDATORYLIST" },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_CHARGE_MASTER", parameters);
                List<MANDATORY_MASTER> master = SqlHelper.CreateListFromTable<MANDATORY_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public MANDATORY_MASTER GetMandatoryMasterDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "GET_MANDATORYDETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<MANDATORY_MASTER>(connstring, "SP_CRUD_CHARGE_MASTER", r => r.TranslateAsMandatoryMaster(), parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateMandatoryMasterList(string connstring, MANDATORY_MASTER i)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_MANDATORY" },
                    new SqlParameter("@ID", SqlDbType.Int) { Value = i.ID },
                    new SqlParameter("@ORG_CODE", SqlDbType.VarChar,50) { Value = i.ORG_CODE},
                    new SqlParameter("@PORT_CODE", SqlDbType.VarChar,100) { Value = i.PORT_CODE},
                    new SqlParameter("@CHARGE_CODE", SqlDbType.VarChar,100) { Value = i.CHARGE_CODE},
                    new SqlParameter("@IE_TYPE", SqlDbType.VarChar, 50) { Value = i.IE_TYPE },
                    new SqlParameter("@LadenStatus", SqlDbType.Char,1) { Value = i.LADEN_STATUS },
                    new SqlParameter("@CURRENCY", SqlDbType.VarChar,50) { Value = i.CURRENCY },
                    new SqlParameter("@RATE20", SqlDbType.Decimal) { Value = i.RATE20 },
                    new SqlParameter("@RATE40", SqlDbType.Decimal) { Value = i.RATE40 },
                    new SqlParameter("@IS_PERCENTAGE", SqlDbType.Bit) { Value = i.IS_PERCENTAGE },
                    new SqlParameter("@PERCENTAGE_VALUE", SqlDbType.Int) { Value = i.PERCENTAGE_VALUE },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteMandatoryMaster(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "DELETE_MANDATORY" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region "UPLOAD TARIFF"
        public void UploadFreightTariff(string connstring, List<FREIGHT_MASTER> master)
        {
            try
            {
                foreach (var i in master)
                {
                    SqlParameter[] parameters =
                    {
                      new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_FREIGHT" },
                      new SqlParameter("@POL", SqlDbType.VarChar,100) { Value = i.POL},
                      new SqlParameter("@POD", SqlDbType.VarChar, 100) { Value = i.POD },
                      new SqlParameter("@Charge", SqlDbType.VarChar,100) { Value = i.Charge },
                      new SqlParameter("@Currency", SqlDbType.VarChar, 10) { Value = i.Currency },
                      new SqlParameter("@LadenStatus", SqlDbType.Char,1) { Value = i.LadenStatus },
                      new SqlParameter("@ServiceMode", SqlDbType.VarChar,20) { Value = i.ServiceMode },
                      new SqlParameter("@DRY20", SqlDbType.Decimal) { Value = i.DRY20 },
                      new SqlParameter("@DRY40", SqlDbType.Decimal) { Value = i.DRY40 },
                      new SqlParameter("@DRY40HC", SqlDbType.Decimal) { Value = i.DRY40HC },
                      new SqlParameter("@DRY45", SqlDbType.Decimal) { Value = i.DRY45 },
                      new SqlParameter("@RF20", SqlDbType.Decimal) { Value = i.RF20 },
                      new SqlParameter("@RF40", SqlDbType.Decimal) { Value = i.RF40 },
                      new SqlParameter("@RF40HC", SqlDbType.Decimal) { Value = i.RF40HC },
                      new SqlParameter("@RF45", SqlDbType.Decimal) { Value = i.RF45 },
                      new SqlParameter("@HAZ20", SqlDbType.Decimal) { Value = i.HAZ20 },
                      new SqlParameter("@HAZ40", SqlDbType.Decimal) { Value = i.HAZ40 },
                      new SqlParameter("@HAZ40HC", SqlDbType.Decimal) { Value = i.HAZ40HC },
                      new SqlParameter("@HAZ45", SqlDbType.Decimal) { Value = i.HAZ45 },
                      new SqlParameter("@SEQ20", SqlDbType.Decimal) { Value = i.SEQ20 },
                      new SqlParameter("@SEQ40", SqlDbType.Decimal) { Value = i.SEQ40 },
                    };

                    SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UploadChargeTariff(string connstring, List<CHARGE_MASTER> master)
        {
            try
            {
                foreach (var i in master)
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_CHARGE" },
                        new SqlParameter("@POL", SqlDbType.VarChar,100) { Value = i.POL},
                        new SqlParameter("@CHARGE_CODE", SqlDbType.VarChar,100) { Value = i.CHARGE_CODE },
                        new SqlParameter("@IMPCOST20", SqlDbType.Decimal) { Value = i.IMPCOST20 },
                        new SqlParameter("@IMPCOST40", SqlDbType.Decimal) { Value = i.IMPCOST40 },
                        new SqlParameter("@IMPREVENUE20", SqlDbType.Decimal) { Value = i.IMPINCOME20 },
                        new SqlParameter("@IMPREVENUE40", SqlDbType.Decimal) { Value = i.IMPINCOME40 },
                        new SqlParameter("@EXPCOST20", SqlDbType.Decimal) { Value = i.EXPCOST20 },
                        new SqlParameter("@EXPCOST40", SqlDbType.Decimal) { Value = i.EXPCOST40 },
                        new SqlParameter("@EXPREVENUE20", SqlDbType.Decimal) { Value = i.EXPINCOME20 },
                        new SqlParameter("@EXPREVENUE40", SqlDbType.Decimal) { Value = i.EXPINCOME40 },
                        new SqlParameter("@Currency", SqlDbType.VarChar,10) { Value = i.CURRENCY },
                        new SqlParameter("@FROM_VAL", SqlDbType.Int) { Value = i.FROM_VAL },
                        new SqlParameter("@TO_VAL", SqlDbType.Int) { Value = i.TO_VAL },
                    };

                    SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UploadStevTariff(string connstring, List<STEV_MASTER> master)
        {
            try
            {
                foreach (var i in master)
                {
                    SqlParameter[] parameters =
                   {
                      new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_STEV" },
                      new SqlParameter("@IE_TYPE", SqlDbType.VarChar,255) { Value = i.IE_TYPE},
                      new SqlParameter("@POL", SqlDbType.VarChar,100) { Value = i.POL},
                      new SqlParameter("@TERMINAL", SqlDbType.VarChar, 255) { Value = i.TERMINAL },
                      new SqlParameter("@CHARGE_CODE", SqlDbType.VarChar,100) { Value = i.CHARGE_CODE },
                      new SqlParameter("@CURRENCY", SqlDbType.VarChar, 10) { Value = i.CURRENCY },
                      new SqlParameter("@LadenStatus", SqlDbType.Char,1) { Value = i.LADEN_STATUS },
                      new SqlParameter("@ServiceMode", SqlDbType.VarChar,20) { Value = i.SERVICE_MODE },
                      new SqlParameter("@DRY20", SqlDbType.Decimal) { Value = i.DRY20 },
                      new SqlParameter("@DRY40", SqlDbType.Decimal) { Value = i.DRY40 },
                      new SqlParameter("@DRY40HC", SqlDbType.Decimal) { Value = i.DRY40HC },
                      new SqlParameter("@DRY45", SqlDbType.Decimal) { Value = i.DRY45 },
                      new SqlParameter("@RF20", SqlDbType.Decimal) { Value = i.RF20 },
                      new SqlParameter("@RF40", SqlDbType.Decimal) { Value = i.RF40 },
                      new SqlParameter("@RF40HC", SqlDbType.Decimal) { Value = i.RF40HC },
                      new SqlParameter("@RF45", SqlDbType.Decimal) { Value = i.RF45 },
                      new SqlParameter("@HAZ20", SqlDbType.Decimal) { Value = i.HAZ20 },
                      new SqlParameter("@HAZ40", SqlDbType.Decimal) { Value = i.HAZ40 },
                      new SqlParameter("@HAZ40HC", SqlDbType.Decimal) { Value = i.HAZ40HC },
                      new SqlParameter("@HAZ45", SqlDbType.Decimal) { Value = i.HAZ45 },
                      new SqlParameter("@SEQ20", SqlDbType.Decimal) { Value = i.SEQ20 },
                      new SqlParameter("@SEQ40", SqlDbType.Decimal) { Value = i.SEQ40 },
                    };

                    SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UploadDetentionTariff(string connstring, List<DETENTION_MASTER> master)
        {
            try
            {
                foreach (var i in master)
                {
                    SqlParameter[] parameters =
                   {
                      new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_DETENTION" },
                      new SqlParameter("@PORT_CODE", SqlDbType.VarChar,50) { Value = i.PORT_CODE},
                      new SqlParameter("@CONTAINER_TYPE", SqlDbType.VarChar,20) { Value = i.CONTAINER_TYPE},
                      new SqlParameter("@CURRENCY", SqlDbType.VarChar, 10) { Value = i.CURRENCY },
                      new SqlParameter("@FROM_DAYS", SqlDbType.Int) { Value = i.FROM_DAYS },
                      new SqlParameter("@TO_DAYS", SqlDbType.Int) { Value = i.TO_DAYS },
                      new SqlParameter("@RATE20", SqlDbType.Decimal) { Value = i.RATE20 },
                      new SqlParameter("@RATE40", SqlDbType.Decimal) { Value = i.RATE40 },
                      new SqlParameter("@HC_RATE", SqlDbType.Decimal) { Value = i.HC_RATE },
                      new SqlParameter("@CREATED_BY", SqlDbType.VarChar,255) { Value = i.CREATED_BY },
                    };

                    SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UploadMandatoryTariff(string connstring, List<MANDATORY_MASTER> master)
        {
            try
            {
                foreach (var i in master)
                {
                    SqlParameter[] parameters =
                   {
                      new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_MANDATORY" },
                      new SqlParameter("@ORG_CODE", SqlDbType.VarChar,50) { Value = i.ORG_CODE},
                      new SqlParameter("@PORT_CODE", SqlDbType.VarChar,100) { Value = i.PORT_CODE},
                      new SqlParameter("@CHARGE_CODE", SqlDbType.VarChar,100) { Value = i.CHARGE_CODE},
                      new SqlParameter("@IE_TYPE", SqlDbType.VarChar, 50) { Value = i.IE_TYPE },
                      new SqlParameter("@LadenStatus", SqlDbType.Char,1) { Value = i.LADEN_STATUS },
                      new SqlParameter("@CURRENCY", SqlDbType.VarChar,50) { Value = i.CURRENCY },
                      new SqlParameter("@RATE20", SqlDbType.Decimal) { Value = i.RATE20 },
                      new SqlParameter("@RATE40", SqlDbType.Decimal) { Value = i.RATE40 },
                      new SqlParameter("@IS_PERCENTAGE", SqlDbType.Bit) { Value = i.IS_PERCENTAGE },
                      new SqlParameter("@PERCENTAGE_VALUE", SqlDbType.Int) { Value = i.PERCENTAGE_VALUE },
                    };

                    SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UploadSlotRateTariff(string connstring, List<SLOT_RATE_MASTER> master)   //SIDDHESH
        {
            try
            {
                foreach (var i in master)
                {
                    SqlParameter[] parameters =
                   {
                      new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_SLOT_RATE" },
                      new SqlParameter("@SLOT_OPERATOR_NAME", SqlDbType.VarChar,100) { Value = i.SLOT_OPERATOR_NAME},
                      new SqlParameter("@SERVICE", SqlDbType.VarChar,100) { Value = i.SERVICE},
                      new SqlParameter("@SERVICE_MODE", SqlDbType.VarChar,100) { Value = i.SERVICE_MODE},
                      new SqlParameter("@LADEN_STATUS", SqlDbType.VarChar, 50) { Value = i.LADEN_STATUS },
                      new SqlParameter("@POL", SqlDbType.VarChar,50) { Value = i.POL },
                      new SqlParameter("@POD", SqlDbType.VarChar,50) { Value = i.POD },
                      new SqlParameter("@CURRENCY", SqlDbType.VarChar,50) { Value = i.CURRENCY },
                      new SqlParameter("@RATE20", SqlDbType.Decimal) { Value = i.RATE20 },
                      new SqlParameter("@RATE40", SqlDbType.Decimal) { Value = i.RATE40 },
                      new SqlParameter("@HC20", SqlDbType.Decimal) { Value = i.HC20 },
                      new SqlParameter("@RF20", SqlDbType.Decimal) { Value = i.RF20 },
                      new SqlParameter("@HAZ20", SqlDbType.Decimal) { Value = i.HAZ20 },
                      new SqlParameter("@HC40", SqlDbType.Decimal) { Value = i.HC40 },
                      new SqlParameter("@RF40", SqlDbType.Decimal) { Value = i.RF40 },
                      new SqlParameter("@HAZ40", SqlDbType.Decimal) { Value = i.HAZ40 },
                      new SqlParameter("@FROM_DATE", SqlDbType.VarChar,50) { Value = i.FROM_DATE },
                      new SqlParameter("@TO_DATE", SqlDbType.VarChar,50) { Value = i.TO_DATE },
                    };

                    SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "ORGANISATION MASTER"
        public void InsertOrgMaster(string connstring, ORG_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_ORG" },
                  new SqlParameter("@ORG_NAME", SqlDbType.VarChar,255) { Value = master.ORG_NAME},
                  new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 50) { Value = master.ORG_CODE },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS },
                  new SqlParameter("@PAN", SqlDbType.VarChar, 50) { Value = master.PAN },
                  new SqlParameter("@CONTACT_PERSON_NAME", SqlDbType.VarChar, 255) { Value = master.CONTACT_PERSON_NAME },
                  new SqlParameter("@CONTACT_PERSON_NO", SqlDbType.VarChar, 50) { Value = master.CONTACT_PERSON_NO },
                  new SqlParameter("@IS_GROUP_COMPANIES", SqlDbType.Bit) { Value = master.IS_GROUP_COMPANIES },
                  new SqlParameter("@SALES_NAME", SqlDbType.VarChar, 255) { Value = master.SALES_NAME },
                  new SqlParameter("@SALES_CODE", SqlDbType.VarChar, 50) { Value = master.SALES_CODE },
                  new SqlParameter("@SALES_LOC", SqlDbType.VarChar,255) { Value = master.SALES_LOC },
                  new SqlParameter("@SALES_EFFECTIVE_DATE", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(master.SALES_EFFECTIVE_DATE) ? null : Convert.ToDateTime(master.SALES_EFFECTIVE_DATE)  },
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar,50) { Value = master.CREATED_BY },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_ORGANISATION", parameters);

                DataTable tbl = new DataTable();
                tbl.Columns.Add(new DataColumn("ORG_CODE", typeof(string)));
                tbl.Columns.Add(new DataColumn("BRANCH_NAME", typeof(string)));
                tbl.Columns.Add(new DataColumn("BRANCH_CODE", typeof(string)));
                tbl.Columns.Add(new DataColumn("COUNTRY", typeof(string)));
                tbl.Columns.Add(new DataColumn("STATE", typeof(string)));
                tbl.Columns.Add(new DataColumn("CITY", typeof(string)));
                tbl.Columns.Add(new DataColumn("TAN", typeof(string)));
                tbl.Columns.Add(new DataColumn("TAX_NO", typeof(string)));
                tbl.Columns.Add(new DataColumn("TAX_TYPE", typeof(string)));
                tbl.Columns.Add(new DataColumn("PIC_NAME", typeof(string)));
                tbl.Columns.Add(new DataColumn("PIC_CONTACT", typeof(string)));
                tbl.Columns.Add(new DataColumn("PIC_EMAIL", typeof(string)));
                tbl.Columns.Add(new DataColumn("ADDRESS", typeof(string)));
                tbl.Columns.Add(new DataColumn("IS_SEZ", typeof(bool)));
                tbl.Columns.Add(new DataColumn("IS_TAX_APPLICABLE", typeof(bool)));

                foreach (var i in master.BRANCH_LIST)
                {
                    DataRow dr = tbl.NewRow();

                    dr["ORG_CODE"] = master.ORG_CODE;
                    dr["BRANCH_NAME"] = i.BRANCH_NAME;
                    dr["BRANCH_CODE"] = i.BRANCH_CODE;
                    dr["COUNTRY"] = i.COUNTRY;
                    dr["STATE"] = i.STATE;
                    dr["CITY"] = i.CITY;
                    dr["TAN"] = i.TAN;
                    dr["PIC_NAME"] = i.PIC_NAME;
                    dr["PIC_CONTACT"] = i.PIC_CONTACT;
                    dr["PIC_EMAIL"] = i.PIC_EMAIL;
                    dr["ADDRESS"] = i.ADDRESS;
                    dr["TAX_NO"] = i.TAX_NO;
                    dr["TAX_TYPE"] = i.TAX_TYPE;
                    dr["IS_SEZ"] = i.IS_SEZ;
                    dr["IS_TAX_APPLICABLE"] = i.IS_TAX_APPLICABLE;

                    tbl.Rows.Add(dr);
                }

                string[] columns = new string[15];
                columns[0] = "ORG_CODE";
                columns[1] = "BRANCH_NAME";
                columns[2] = "COUNTRY";
                columns[3] = "STATE";
                columns[4] = "CITY";
                columns[5] = "TAN";
                columns[6] = "PIC_NAME";
                columns[7] = "PIC_CONTACT";
                columns[8] = "PIC_EMAIL";
                columns[9] = "ADDRESS";
                columns[10] = "TAX_NO";
                columns[11] = "TAX_TYPE";
                columns[12] = "IS_SEZ";
                columns[13] = "IS_TAX_APPLICABLE";
                columns[14] = "BRANCH_CODE";

                SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl, "MST_ORG_BRANCH", columns);

                DataTable tbl1 = new DataTable();
                tbl1.Columns.Add(new DataColumn("ORG_CODE", typeof(string)));
                tbl1.Columns.Add(new DataColumn("BRANCH_CODE", typeof(string)));
                tbl1.Columns.Add(new DataColumn("BANK_NAME", typeof(string)));
                tbl1.Columns.Add(new DataColumn("BANK_ACC_NO", typeof(string)));
                tbl1.Columns.Add(new DataColumn("BANK_IFSC", typeof(string)));
                tbl1.Columns.Add(new DataColumn("BANK_SWIFT", typeof(string)));
                tbl1.Columns.Add(new DataColumn("BANK_REMARKS", typeof(string)));

                foreach (var i in master.BANK_LIST)
                {
                    DataRow dr = tbl1.NewRow();

                    dr["ORG_CODE"] = master.ORG_CODE;
                    dr["BRANCH_CODE"] = i.BRANCH_CODE;
                    dr["BANK_NAME"] = i.BANK_NAME;
                    dr["BANK_ACC_NO"] = i.BANK_ACC_NO;
                    dr["BANK_IFSC"] = i.BANK_IFSC;
                    dr["BANK_SWIFT"] = i.BANK_SWIFT;
                    dr["BANK_REMARKS"] = i.BANK_REMARKS;

                    tbl1.Rows.Add(dr);
                }

                string[] columns1 = new string[7];
                columns1[0] = "ORG_CODE";
                columns1[1] = "BANK_NAME";
                columns1[2] = "BANK_ACC_NO";
                columns1[3] = "BANK_IFSC";
                columns1[4] = "BANK_SWIFT";
                columns1[5] = "BANK_REMARKS";
                columns1[6] = "BRANCH_CODE";

                SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl1, "MST_ORG_BANK", columns1);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ValidateOrgCode(string connstring, string ORG_CODE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "VALIDATE_ORG_CODE" },
                  new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 50) { Value = ORG_CODE },
                };

                return SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_ORGANISATION", parameters);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ORG_MASTER> GetOrgMasterList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_ORG_LIST" }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_ORGANISATION", parameters);
                List<ORG_MASTER> master = SqlHelper.CreateListFromTable<ORG_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetOrgMasterDetails(string connstring, string ORG_CODE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 20) { Value = ORG_CODE },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_ORG_DETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_ORGANISATION", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateOrgMasterList(string connstring, ORG_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_ORG" },
                  new SqlParameter("@ORG_NAME", SqlDbType.VarChar,255) { Value = master.ORG_NAME},
                  new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 50) { Value = master.ORG_CODE },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS },
                  new SqlParameter("@PAN", SqlDbType.VarChar, 50) { Value = master.PAN },
                  new SqlParameter("@CONTACT_PERSON_NAME", SqlDbType.VarChar, 255) { Value = master.CONTACT_PERSON_NAME },
                  new SqlParameter("@CONTACT_PERSON_NO", SqlDbType.VarChar, 50) { Value = master.CONTACT_PERSON_NO },
                  new SqlParameter("@IS_GROUP_COMPANIES", SqlDbType.Bit) { Value = master.IS_GROUP_COMPANIES },
                  new SqlParameter("@SALES_NAME", SqlDbType.VarChar, 255) { Value = master.SALES_NAME },
                  new SqlParameter("@SALES_CODE", SqlDbType.VarChar, 50) { Value = master.SALES_CODE },
                  new SqlParameter("@SALES_LOC", SqlDbType.VarChar,255) { Value = master.SALES_LOC },
                  new SqlParameter("@SALES_EFFECTIVE_DATE", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(master.SALES_EFFECTIVE_DATE) ? null : Convert.ToDateTime(master.SALES_EFFECTIVE_DATE)  },
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar,50) { Value = master.CREATED_BY },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_ORGANISATION", parameters);

                foreach (var items in master.BRANCH_LIST)
                {
                    SqlParameter[] parameters1 =
                    {
                      new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_ORG_BRANCH" },
                      new SqlParameter("@BRANCH_ID", SqlDbType.Int) { Value = items.ID},
                      new SqlParameter("@ORG_CODE", SqlDbType.VarChar,50) { Value = master.ORG_CODE },
                      new SqlParameter("@BRANCH_NAME", SqlDbType.VarChar, 255) { Value = items.BRANCH_NAME },
                      new SqlParameter("@BRANCH_CODE", SqlDbType.VarChar, 20) { Value = items.BRANCH_CODE },
                      new SqlParameter("@COUNTRY", SqlDbType.VarChar, 50) { Value = items.COUNTRY },
                      new SqlParameter("@STATE", SqlDbType.VarChar,255) { Value = items.STATE },
                      new SqlParameter("@CITY", SqlDbType.VarChar,255) { Value = items.CITY },
                      new SqlParameter("@TAN", SqlDbType.VarChar,50) { Value = items.TAN },
                      new SqlParameter("@TAX_NO", SqlDbType.VarChar,50) { Value = items.TAX_NO },
                      new SqlParameter("@TAX_TYPE", SqlDbType.VarChar,20) { Value = items.TAX_TYPE },
                      new SqlParameter("@PIC_NAME", SqlDbType.VarChar,255) { Value = items.PIC_NAME },
                      new SqlParameter("@PIC_CONTACT", SqlDbType.VarChar,50) { Value = items.PIC_CONTACT },
                      new SqlParameter("@PIC_EMAIL", SqlDbType.VarChar,255) { Value = items.PIC_EMAIL },
                      new SqlParameter("@ADDRESS", SqlDbType.VarChar,255) { Value = items.ADDRESS },
                      new SqlParameter("@IS_SEZ", SqlDbType.Bit) { Value = items.IS_SEZ },
                      new SqlParameter("@IS_TAX_APPLICABLE", SqlDbType.Bit) { Value = items.IS_TAX_APPLICABLE },
                    };

                    SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_ORGANISATION", parameters1);
                }

                foreach (var items in master.BANK_LIST)
                {
                    SqlParameter[] parameters2 =
                    {
                      new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_ORG_BANK" },
                      new SqlParameter("@BANK_ID", SqlDbType.Int) { Value = items.ID},
                      new SqlParameter("@ORG_CODE", SqlDbType.VarChar,50) { Value = master.ORG_CODE },
                      new SqlParameter("@BANK_NAME", SqlDbType.VarChar, 255) { Value = items.BANK_NAME },
                      new SqlParameter("@BRANCH_CODE", SqlDbType.VarChar, 20) { Value = items.BRANCH_CODE },
                      new SqlParameter("@BANK_ACC_NO", SqlDbType.VarChar, 50) { Value = items.BANK_ACC_NO },
                      new SqlParameter("@BANK_IFSC", SqlDbType.VarChar,50) { Value = items.BANK_IFSC },
                      new SqlParameter("@BANK_SWIFT", SqlDbType.VarChar,50) { Value = items.BANK_SWIFT },
                      new SqlParameter("@BANK_REMARKS", SqlDbType.VarChar,255) { Value = items.BANK_REMARKS },
                    };

                    SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_ORGANISATION", parameters2);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteOrgMaster(string connstring, string ORG_CODE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@ORG_CODE", SqlDbType.VarChar,20) { Value = ORG_CODE },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "DELETE_ORG" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_ORGANISATION", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region "SLOT MASTER"
        public void InsertSlotMaster(string connstring, SLOT_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_SLOT" },
                  new SqlParameter("@SLOT_OPERATOR", SqlDbType.VarChar,255) { Value = master.SLOT_OPERATOR},
                  new SqlParameter("@SERVICES", SqlDbType.VarChar, 100) { Value = master.SERVICES },
                  new SqlParameter("@LINER_CODE", SqlDbType.VarChar, 100) { Value = master.LINER_CODE },
                  new SqlParameter("@PORT_CODE", SqlDbType.VarChar, 100) { Value = master.PORT_CODE },
                  new SqlParameter("@TERM", SqlDbType.VarChar, 50) { Value = master.TERM },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS },
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar,50) { Value = master.CREATED_BY },
                  new SqlParameter("@ADDRESS", ((long)SqlDbType.VarChar) ) { Value = master.ADDRESS},
                  new SqlParameter("@CONTACT", SqlDbType.VarChar, 50) { Value = master.CONTACT},
                  new SqlParameter("@EMAIL", SqlDbType.VarChar, 255) { Value = master.EMAIL },
                  new SqlParameter("@SLOT_CODE", SqlDbType.VarChar, 20) { Value = master.SLOT_CODE },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_SLOT", parameters);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<SLOT_MASTER> GetSlotMasterList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_SLOT_LIST" }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_SLOT", parameters);
                List<SLOT_MASTER> master = SqlHelper.CreateListFromTable<SLOT_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public SLOT_MASTER GetSlotMasterDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "GET_SLOT_DETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<SLOT_MASTER>(connstring, "SP_CRUD_SLOT", r => r.TranslateAsSlotMaster(), parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateSlotMasterList(string connstring, SLOT_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_SLOT" },
                  new SqlParameter("@ID", SqlDbType.Int) { Value = master.ID},
                  new SqlParameter("@SLOT_OPERATOR", SqlDbType.VarChar,255) { Value = master.SLOT_OPERATOR},
                  new SqlParameter("@SERVICES", SqlDbType.VarChar, 100) { Value = master.SERVICES },
                  new SqlParameter("@LINER_CODE", SqlDbType.VarChar, 100) { Value = master.LINER_CODE },
                  new SqlParameter("@PORT_CODE", SqlDbType.VarChar, 100) { Value = master.PORT_CODE },
                  new SqlParameter("@TERM", SqlDbType.VarChar, 50) { Value = master.TERM },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS },
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar,50) { Value = master.CREATED_BY },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_SLOT", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteSlotMaster(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "DELETE_SLOT" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_SLOT", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region "CHARGES MASTER"
        public void InsertChargeMaster(string connstring, CHARGES_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_CHARGE_MASTER" },
                  new SqlParameter("@CHARGE_NAME", SqlDbType.VarChar,100) { Value = master.CHARGE_NAME},
                  new SqlParameter("@CHARGE_HEADER", SqlDbType.VarChar, 100) { Value = master.CHARGE_HEADER },
                  new SqlParameter("@APPLICABLE_FOR", SqlDbType.VarChar, 50) { Value = master.APPLICABLE_FOR },
                  new SqlParameter("@GST_PERCENTAGE", SqlDbType.Int) { Value = master.GST_PERCENTAGE },
                  new SqlParameter("@CURRENCY", SqlDbType.NVarChar, 50) { Value = master.CURRENCY },
                  new SqlParameter("@HSN_CODE", SqlDbType.VarChar, 50) { Value = master.HSN_CODE },
                  new SqlParameter("@CHARGE_AMOUNT", SqlDbType.Int) { Value = master.CHARGE_AMOUNT },
                  new SqlParameter("@CHARGE_TYPE", SqlDbType.VarChar, 50) { Value = master.CHARGE_TYPE},
                  new SqlParameter("@IS_GST", SqlDbType.Bit) { Value = master.IS_GST},


                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "CHARGE_MASTER", parameters);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CHARGES_MASTER> GetChargeMaster(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_CHARGE_MASTER_LIST" }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "CHARGE_MASTER", parameters);
                List<CHARGES_MASTER> master = SqlHelper.CreateListFromTable<CHARGES_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CHARGES_MASTER> GetChargeMastersDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {

                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_CHARGES_MASTER_DETAILS" }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "CHARGE_MASTER", parameters);
                List<CHARGES_MASTER> master = SqlHelper.CreateListFromTable<CHARGES_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public void UpdateChargesMaster(string connstring, CHARGES_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
               {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,255) { Value = "UPDATE_CHARGES_MASTER" },
                  new SqlParameter("@ID", SqlDbType.Int) { Value = master.ID},
                   new SqlParameter("@CHARGE_NAME", SqlDbType.VarChar,100) { Value = master.CHARGE_NAME},
                  new SqlParameter("@CHARGE_HEADER", SqlDbType.VarChar, 100) { Value = master.CHARGE_HEADER },
                  new SqlParameter("@APPLICABLE_FOR", SqlDbType.VarChar, 50) { Value = master.APPLICABLE_FOR },
                  new SqlParameter("@GST_PERCENTAGE", SqlDbType.Int) { Value = master.GST_PERCENTAGE },
                  new SqlParameter("@CURRENCY", SqlDbType.NVarChar, 50) { Value = master.CURRENCY },
                  new SqlParameter("@HSN_CODE", SqlDbType.VarChar, 50) { Value = master.HSN_CODE },
                  new SqlParameter("@CHARGE_AMOUNT", SqlDbType.Int) { Value = master.CHARGE_AMOUNT },
                  new SqlParameter("@CHARGE_TYPE", SqlDbType.VarChar, 50) { Value = master.CHARGE_TYPE},
                  new SqlParameter("@IS_GST", SqlDbType.Bit) { Value = master.IS_GST},
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "CHARGE_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteChargesMaster(string connstring, int ID)
        {
            SqlParameter[] parameters =
            {
              new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
               new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "DELETE_CHARGES_MASTER" }
            };

            SqlHelper.ExecuteProcedureReturnString(connstring, "CHARGE_MASTER", parameters);
        }
        #endregion

        #region "COUNTRY MASTER"
        public void InsertCountryMaster(string connstring, COUNTRY_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_COUNTRY" },
                  new SqlParameter("@CODE", SqlDbType.Int) { Value = master.CODE },
                  new SqlParameter("@NAME", SqlDbType.VarChar,255) { Value = master.NAME},
                  new SqlParameter("@SHORT_NAME", SqlDbType.VarChar, 50) { Value = master.SHORT_NAME },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS },
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar,255) { Value = master.CREATED_BY },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_COUNTRY", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<COUNTRY_MASTER> GetCountryMasterList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_COUNTRYLIST" },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_COUNTRY", parameters);
                List<COUNTRY_MASTER> master = SqlHelper.CreateListFromTable<COUNTRY_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public COUNTRY_MASTER GetCountryMasterDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_COUNTRYDETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<COUNTRY_MASTER>(connstring, "SP_CRUD_COUNTRY", r => r.TranslateAsCountry(), parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateCountryMasterList(string connstring, COUNTRY_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_COUNTRY" },
                  new SqlParameter("@ID", SqlDbType.Int) { Value = master.ID},
                  new SqlParameter("@CODE", SqlDbType.Int) { Value = master.CODE },
                  new SqlParameter("@NAME", SqlDbType.VarChar,255) { Value = master.NAME},
                  new SqlParameter("@SHORT_NAME", SqlDbType.VarChar, 50) { Value = master.SHORT_NAME },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_COUNTRY", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteCountryMaster(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "DELETE_COUNTRY" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_COUNTRY", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region "STATE MASTER"
        public void InsertStateMaster(string connstring, STATE_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_STATE" },
                  new SqlParameter("@CODE", SqlDbType.Int) { Value = master.CODE },
                  new SqlParameter("@NAME", SqlDbType.VarChar,255) { Value = master.NAME},
                  new SqlParameter("@SHORT_NAME", SqlDbType.VarChar, 50) { Value = master.SHORT_NAME },
                  new SqlParameter("@COUNTRY_ID", SqlDbType.Int) { Value = master.COUNTRY_ID },
                  new SqlParameter("@IS_UT", SqlDbType.Bit) { Value = master.IS_UT },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS },
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar,255) { Value = master.CREATED_BY },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_STATE", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<STATE_MASTER> GetStateMasterList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_STATELIST" },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_STATE", parameters);
                List<STATE_MASTER> master = SqlHelper.CreateListFromTable<STATE_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public STATE_MASTER GetStateMasterDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_STATEDETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<STATE_MASTER>(connstring, "SP_CRUD_STATE", r => r.TranslateAsSate(), parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateStateMasterList(string connstring, STATE_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_STATE" },
                  new SqlParameter("@ID", SqlDbType.Int) { Value = master.ID},
                  new SqlParameter("@CODE", SqlDbType.Int) { Value = master.CODE },
                  new SqlParameter("@NAME", SqlDbType.VarChar,255) { Value = master.NAME},
                  new SqlParameter("@SHORT_NAME", SqlDbType.VarChar, 50) { Value = master.SHORT_NAME },
                  new SqlParameter("@COUNTRY_ID", SqlDbType.Int) { Value = master.COUNTRY_ID },
                  new SqlParameter("@IS_UT", SqlDbType.Bit) { Value = master.IS_UT },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_STATE", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteStateMaster(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "DELETE_STATE" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_STATE", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        public static T GetSingleDataFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateItemFromRow<T>(dataTable.Rows[0]);
        }

        public static List<T> GetListFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateListFromTable<T>(dataTable);
        }

        #region "HSN-CODE"

        public void InsertHsnCode(string connstring, HSN_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_HSN_MASTER" },
                  new SqlParameter("@HSN_CODE", SqlDbType.VarChar,100) { Value = master.HSN_CODE},
                  new SqlParameter("@HSN_DESC", SqlDbType.VarChar, 100) { Value = master.HSN_DESC },
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 100) { Value = master.CREATED_BY},
                  new SqlParameter("@EFFECTIVE_FROM", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(master.EFFECTIVE_FROM) ? null : Convert.ToDateTime(master.EFFECTIVE_FROM)},
                  new SqlParameter("@EFFECTIVE_TO", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(master.EFFECTIVE_TO) ? null : Convert.ToDateTime(master.EFFECTIVE_TO)},
                  new SqlParameter("@CGST", SqlDbType.Decimal) { Value = master.CGST},
                  new SqlParameter("@IGST", SqlDbType.Decimal) { Value = master.IGST},
                  new SqlParameter("@SGST", SqlDbType.Decimal) { Value = master.SGST},
                  new SqlParameter("@RATE", SqlDbType.Decimal) { Value = master.RATE},
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_HSN_MASTER", parameters);

            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<HSN_MASTER> GetHsnMaster(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_HSN_MASTER_LIST" }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_HSN_MASTER", parameters);
                List<HSN_MASTER> master = SqlHelper.CreateListFromTable<HSN_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteHsnMaster(string connstring, int ID)
        {
            SqlParameter[] parameters =
            {
              new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
               new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "DELETE_HSN_MASTER" }
            };

            SqlHelper.ExecuteProcedureReturnString(connstring, "SP_HSN_MASTER", parameters);
        }
        #endregion

        #region"IAL"
        public DataSet GetIALDetails(string connstring, string AGENT_CODE, string VESSEL_NAME, string VOYAGE_NO, string PORT_OF_LOADING)
        {
            try
            {
                SqlParameter[] parameters =
                              {
                        new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "GET_IAL" },
                        new SqlParameter("@AGENT_CODE", SqlDbType.VarChar,50) { Value = AGENT_CODE },
                        new SqlParameter("@VESSEL_NAME", SqlDbType.VarChar,50) { Value = VESSEL_NAME },
                        new SqlParameter("@VOYAGE_NO", SqlDbType.VarChar,50) { Value = VOYAGE_NO },
                        new SqlParameter("@PORT_OF_LOADING", SqlDbType.VarChar,50) { Value = PORT_OF_LOADING },

                };

                return SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_BL", parameters);

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region"EAL"
        public DataSet GetEALDetails(string connstring, string AGENT_CODE, string VESSEL_NAME, string VOYAGE_NO, string PORT_OF_DISCHARGE)
        {
            try
            {
                SqlParameter[] parameters =
                              {
                        new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "GET_EAL" },
                        new SqlParameter("@AGENT_CODE", SqlDbType.VarChar,50) { Value = AGENT_CODE },
                        new SqlParameter("@VESSEL_NAME", SqlDbType.VarChar,50) { Value = VESSEL_NAME },
                        new SqlParameter("@VOYAGE_NO", SqlDbType.VarChar,50) { Value = VOYAGE_NO },
                        new SqlParameter("@PORT_OF_DISCHARGE", SqlDbType.VarChar,50) { Value = PORT_OF_DISCHARGE },

                };

                return SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_BL", parameters);

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "SLOT RATE MASTER"
        public List<SLOT_RATE_MASTER> GetSlotRateList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_SLOT_RATE_LIST" },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_CHARGE_MASTER", parameters);
                List<SLOT_RATE_MASTER> master = SqlHelper.CreateListFromTable<SLOT_RATE_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public SLOT_RATE_MASTER GetSlotRateMasterDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_SLOT_RATE_DETAILS" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<SLOT_RATE_MASTER>(connstring, "SP_CRUD_CHARGE_MASTER", r => r.TranslateAsSlotRateMaster(), parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateSlotRateMaster(string connstring, SLOT_RATE_MASTER i)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_SLOT_RATE" },
                  new SqlParameter("@ID", SqlDbType.Int) { Value = i.ID},
                      new SqlParameter("@SLOT_OPERATOR_NAME", SqlDbType.VarChar,100) { Value = i.SLOT_OPERATOR_NAME},
                      new SqlParameter("@SERVICE", SqlDbType.VarChar,100) { Value = i.SERVICE},
                      new SqlParameter("@SERVICE_MODE", SqlDbType.VarChar,100) { Value = i.SERVICE_MODE},
                      new SqlParameter("@LADEN_STATUS", SqlDbType.VarChar, 50) { Value = i.LADEN_STATUS },
                      new SqlParameter("@POL", SqlDbType.VarChar,50) { Value = i.POL },
                      new SqlParameter("@POD", SqlDbType.VarChar,50) { Value = i.POD },
                      new SqlParameter("@CURRENCY", SqlDbType.VarChar,50) { Value = i.CURRENCY },
                      new SqlParameter("@RATE20", SqlDbType.Decimal) { Value = i.RATE20 },
                      new SqlParameter("@RATE40", SqlDbType.Decimal) { Value = i.RATE40 },
                      new SqlParameter("@HC20", SqlDbType.Decimal) { Value = i.HC20 },
                      new SqlParameter("@RF20", SqlDbType.Decimal) { Value = i.RF20 },
                      new SqlParameter("@HAZ20", SqlDbType.Decimal) { Value = i.HAZ20 },
                      new SqlParameter("@HC40", SqlDbType.Decimal) { Value = i.HC40 },
                      new SqlParameter("@RF40", SqlDbType.Decimal) { Value = i.RF40 },
                      new SqlParameter("@HAZ40", SqlDbType.Decimal) { Value = i.HAZ40 }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteSlotRateMasterList(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "DELETE_SLOT_RATE" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SLOT_RATE_HISTORY_MASTER> HistorySlotRateMasterList(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_SLOT_RATE_HISTORY" }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_CHARGE_MASTER", parameters);
                List<SLOT_RATE_HISTORY_MASTER> master = SqlHelper.CreateListFromTable<SLOT_RATE_HISTORY_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region"FLEET COMPOSITION REPORT"
        public List<FLEET_COMPOSITION_REPORT> GetFleetReport(string connstring, string LOCATION)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@LOCATION", SqlDbType.VarChar, 50) { Value = LOCATION },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_FLEET" }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_FLEET", parameters);
                //List<FLEET_COMPOSITION_REPORT> result = SqlHelper.CreateListFromTable<FLEET_COMPOSITION_REPORT>(dataTable);

                List<FLEET_COMPOSITION_REPORT> result = new List<FLEET_COMPOSITION_REPORT>();

                foreach (DataRow row in dataTable.Rows)
                {
                    var dynamicReport = new FLEET_COMPOSITION_REPORT
                    {
                        DynamicColumns = new ExpandoObject() as IDictionary<string, object>
                    };

                    // Add dynamically generated columns to the dynamic report
                    foreach (DataColumn column in dataTable.Columns)
                    {

                        dynamicReport.DynamicColumns[column.ColumnName] = row[column.ColumnName];

                    }

                    result.Add(dynamicReport);
                }

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region"STOCK POSITION REPORT"
        public List<STOCK_POSITION_REPORT> GetStockReport(string connstring, string LOCATION)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@LOCATION", SqlDbType.VarChar, 50) { Value = LOCATION },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_STOCK" }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_FLEET", parameters);
                List<STOCK_POSITION_REPORT> result = SqlHelper.CreateListFromTable<STOCK_POSITION_REPORT>(dataTable);

                //List<STOCK_POSITION_REPORT> result = new List<STOCK_POSITION_REPORT>();

                //foreach (DataRow row in dataTable.Rows)
                //{
                //    var dynamicReport = new STOCK_POSITION_REPORT
                //    {
                //        DynamicColumns = new ExpandoObject() as IDictionary<string, object>
                //    };

                //    // Add dynamically generated columns to the dynamic report
                //    foreach (DataColumn column in dataTable.Columns)
                //    {

                //        dynamicReport.DynamicColumns[column.ColumnName] = row[column.ColumnName];

                //    }

                //    result.Add(dynamicReport);
                //}

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region"CONTAINER TURN TIME REPORT"
        public List<CONTAINER_TURN_TIME_REPORT> GetContainerTurnReport(string connstring, string LOCATION)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@LOCATION", SqlDbType.VarChar, 50) { Value = LOCATION },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_CONTAINERTIME" }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_FLEET", parameters);
                List<CONTAINER_TURN_TIME_REPORT> result = SqlHelper.CreateListFromTable<CONTAINER_TURN_TIME_REPORT>(dataTable);

                //List<STOCK_POSITION_REPORT> result = new List<STOCK_POSITION_REPORT>();

                //foreach (DataRow row in dataTable.Rows)
                //{
                //    var dynamicReport = new STOCK_POSITION_REPORT
                //    {
                //        DynamicColumns = new ExpandoObject() as IDictionary<string, object>
                //    };

                //    // Add dynamically generated columns to the dynamic report
                //    foreach (DataColumn column in dataTable.Columns)
                //    {

                //        dynamicReport.DynamicColumns[column.ColumnName] = row[column.ColumnName];

                //    }

                //    result.Add(dynamicReport);
                //}

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region"CONTAINER REPAIRS"
        public List<CONTAINER_REPAIRS> GetContainerRepairs(string connstring, string LOCATION)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@LOCATION", SqlDbType.VarChar, 50) { Value = LOCATION },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_CONTAINER_REPAIRS" }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_FLEET", parameters);
                List<CONTAINER_REPAIRS> result = SqlHelper.CreateListFromTable<CONTAINER_REPAIRS>(dataTable);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region"SRR LIST"
        public List<SRR_LIST> GetSRRLIST(string connstring, string LOCATION, string AGENT_NAME, string MONTH, string YEAR)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_SRR_LIST" },
                   new SqlParameter("@LOCATIONS", SqlDbType.VarChar, 50) { Value = LOCATION },
                   new SqlParameter("@AGENT_NAME", SqlDbType.VarChar, 50) { Value = AGENT_NAME },
                   new SqlParameter("@MONTH", SqlDbType.VarChar, 50) { Value = MONTH },
                   new SqlParameter("@YEAR", SqlDbType.VarChar, 50) { Value = YEAR },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_FLEET", parameters);
                List<SRR_LIST> result = SqlHelper.CreateListFromTable<SRR_LIST>(dataTable);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region"DOCS LIST"
        public List<DOCS_LIST> GetDOCSLIST(string connstring, string LOCATION, string MONTH, string YEAR)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_DOCS_LIST" },
                   new SqlParameter("@LOCATION", SqlDbType.VarChar, 50) { Value = LOCATION },
                   new SqlParameter("@MONTH", SqlDbType.VarChar, 50) { Value = MONTH },
                   new SqlParameter("@YEAR", SqlDbType.VarChar, 50) { Value = YEAR },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_FLEET", parameters);
                List<DOCS_LIST> result = SqlHelper.CreateListFromTable<DOCS_LIST>(dataTable);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


        #region " AGENT NAME"
        public List<AGENT_NAME> GetAgent(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_AGENT_LIST" }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_FLEET", parameters);
                List<AGENT_NAME> master = SqlHelper.CreateListFromTable<AGENT_NAME>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region"SHIPPER LIST"
        public List<SHIPPER_LIST> GetShipperLIST(string connstring, string LOCATION, string CUSTOMER_NAME, string MONTH, string YEAR)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_SHIPPER_LIST" },
                   new SqlParameter("@LOCATION", SqlDbType.VarChar, 50) { Value = LOCATION },
                   new SqlParameter("@CUSTOMER_NAME", SqlDbType.VarChar, 50) { Value = CUSTOMER_NAME },
                   //new SqlParameter("@SHIPPER", SqlDbType.VarChar, 50) { Value = SHIPPER },
                   new SqlParameter("@MONTH", SqlDbType.VarChar, 50) { Value = MONTH },
                   new SqlParameter("@YEAR", SqlDbType.VarChar, 50) { Value = YEAR },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_FLEET", parameters);
                List<SHIPPER_LIST> result = SqlHelper.CreateListFromTable<SHIPPER_LIST>(dataTable);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region"CONSGINEE LIST"
        public List<CONSIGNEE_LIST> GetConsigneeLIST(string connstring, string LOCATION, string CONSIGNEE_NAME, string MONTH, string YEAR)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 100) { Value = "GET_CONSGINEE_LIST" },
                   new SqlParameter("@LOCATION1", SqlDbType.VarChar, 50) { Value = LOCATION },
                   new SqlParameter("@CONSIGNEE", SqlDbType.VarChar, 500) { Value = CONSIGNEE_NAME },
                   new SqlParameter("@MONTH", SqlDbType.VarChar, 50) { Value = MONTH },
                   new SqlParameter("@YEAR", SqlDbType.VarChar, 50) { Value = YEAR },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_FLEET", parameters);
                List<CONSIGNEE_LIST> result = SqlHelper.CreateListFromTable<CONSIGNEE_LIST>(dataTable);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region"DETENATION LIST"
        public List<DETENATION_LIST> GetDetenationList(string connstring, string LOCATION, string CONSIGNEE_NAME, string MONTH, string YEAR)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_DETENTION_LIST" },
                   new SqlParameter("@LOCATION2", SqlDbType.VarChar, 50) { Value = LOCATION },
                   new SqlParameter("@CONSIGNEE", SqlDbType.VarChar, 50) { Value = CONSIGNEE_NAME },
                   new SqlParameter("@MONTH", SqlDbType.VarChar, 50) { Value = MONTH },
                   new SqlParameter("@YEAR", SqlDbType.VarChar, 50) { Value = YEAR },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_FLEET", parameters);
                List<DETENATION_LIST> result = SqlHelper.CreateListFromTable<DETENATION_LIST>(dataTable);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region " CONSGINEE NAME"
        public List<CONSIGNEE_NAME> GetConsignee(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_CONSIGNEE_NAME" }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_FLEET", parameters);
                List<CONSIGNEE_NAME> master = SqlHelper.CreateListFromTable<CONSIGNEE_NAME>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region"AGENCY LIST"
        public List<AGENCY_LIST> GetAgencyList(string connstring, string LOCATION,  string MONTH, string YEAR)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_AGENCY_LIST" },
                   new SqlParameter("@LOCATION2", SqlDbType.VarChar, 50) { Value = LOCATION },
                   new SqlParameter("@MONTH", SqlDbType.VarChar, 50) { Value = MONTH },
                   new SqlParameter("@YEAR", SqlDbType.VarChar, 50) { Value = YEAR },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_FLEET", parameters);
                List<AGENCY_LIST> result = SqlHelper.CreateListFromTable<AGENCY_LIST>(dataTable);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region"SALES LIST"
        public List<SALES_LIST> GetSalesList(string connstring, string LOCATION, string MONTH, string YEAR)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_SALES_LIST" },
                   new SqlParameter("@LOCATION4", SqlDbType.VarChar, 500) { Value = LOCATION },
                   new SqlParameter("@MONTH", SqlDbType.VarChar, 50) { Value = MONTH },
                   new SqlParameter("@YEAR", SqlDbType.VarChar, 50) { Value = YEAR },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_FLEET", parameters);
                List<SALES_LIST> result = SqlHelper.CreateListFromTable<SALES_LIST>(dataTable);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region " SERVICE NAME"
        public List<SERVICE_NAME> GetService(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_SERVICE_NAME" }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_FLEET", parameters);
                List<SERVICE_NAME> master = SqlHelper.CreateListFromTable<SERVICE_NAME>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region"VOLUME LIST"
        public List<VOLUME_LIST> GetVolume(string connstring, string LOCATION, string MONTH, string YEAR, string SERVICE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_VOLUME" },
                   new SqlParameter("@POL", SqlDbType.VarChar, 500) { Value = LOCATION },
                   new SqlParameter("@MONTH", SqlDbType.VarChar, 50) { Value = MONTH },
                   new SqlParameter("@YEAR", SqlDbType.VarChar, 50) { Value = YEAR },
                   new SqlParameter("@SERVICE", SqlDbType.VarChar, 50) { Value = SERVICE },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_FLEET", parameters);
                List<VOLUME_LIST> result = SqlHelper.CreateListFromTable<VOLUME_LIST>(dataTable);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region"VESSEL DETAILS"
        public List<VESSEL_VOYAGE> GetVessel(string connstring, string LOCATION, string VESSEL_NAME, string VOYAGE_NO)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_VESSEL_VOYAGE_DETAILS" },
                   new SqlParameter("@POL", SqlDbType.VarChar, 500) { Value = LOCATION },
                   new SqlParameter("@VESSEL_NAME", SqlDbType.VarChar, 50) { Value = VESSEL_NAME },
                   new SqlParameter("@VOYAGE_NO", SqlDbType.VarChar, 50) { Value = VOYAGE_NO },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_FLEET", parameters);
                List<VESSEL_VOYAGE> result = SqlHelper.CreateListFromTable<VESSEL_VOYAGE>(dataTable);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region"SERVICE DETAILS"
        public List<VESSEL_VOYAGE> GetServices(string connstring, string LOCATION, string SERVICE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_SERVICE_DETAILS" },
                   new SqlParameter("@POL", SqlDbType.VarChar, 500) { Value = LOCATION },
                   new SqlParameter("@SERVICE", SqlDbType.VarChar, 50) { Value = SERVICE },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_FLEET", parameters);
                List<VESSEL_VOYAGE> result = SqlHelper.CreateListFromTable<VESSEL_VOYAGE>(dataTable);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region "EQUIPMENT_TYPE"
        public void InsertEquipmentType(string connstring, EQUIPMENT_TYPE_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_EQUIPMENT" },
                  new SqlParameter("@EQUIPMENT_TYPE", SqlDbType.VarChar,255) { Value = master.EQUIPMENT_TYPE },
                  new SqlParameter("@DESCRIPTION", SqlDbType.VarChar,255) { Value = master.DESCRIPTION},
                  new SqlParameter("@IS_ACTIVE", SqlDbType.Bit) { Value = master.IS_ACTIVE },
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar,50) { Value = master.CREATED_BY },
                  new SqlParameter("@CREATED_DATE", SqlDbType.DateTime) { Value = master.CREATED_DATE },
                  new SqlParameter("@MODIFIED_BY", SqlDbType.VarChar,50) { Value = master.MODIFIED_BY },
                  new SqlParameter("@MODIFIED_AT", SqlDbType.DateTime) { Value = master.MODIFIED_AT },
                  new SqlParameter("@DELETED_BY", SqlDbType.VarChar,50) { Value = master.DELETED_BY },
                  new SqlParameter("@DELETED_AT", SqlDbType.DateTime) { Value = master.DELETED_AT },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_EQUIPMENT", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<EQUIPMENT_TYPE_MASTER> GetEquipmentTypeList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_EQUIPMENTLIST" },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_EQUIPMENT", parameters);
                List<EQUIPMENT_TYPE_MASTER> master = SqlHelper.CreateListFromTable<EQUIPMENT_TYPE_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }
     
        public List<EQUIPMENT_TYPE_MASTER> GetEquipmentTypeDetails(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@EQUIPMENT_TYPE_ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_EQUIPMENTDETAILS" }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_EQUIPMENT", parameters);
                List<EQUIPMENT_TYPE_MASTER> result = SqlHelper.CreateListFromTable<EQUIPMENT_TYPE_MASTER>(dataTable);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public void UpdatEquipmentTypeList(string connstring, EQUIPMENT_TYPE_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_EQUIPMENT" },
                  new SqlParameter("@EQUIPMENT_TYPE", SqlDbType.VarChar,255) { Value = master.EQUIPMENT_TYPE },
                  new SqlParameter("@DESCRIPTION", SqlDbType.VarChar,255) { Value = master.DESCRIPTION},
                  new SqlParameter("@IS_ACTIVE", SqlDbType.Bit) { Value = master.IS_ACTIVE },
                  new SqlParameter("@MODIFIED_BY", SqlDbType.VarChar,50) { Value = master.MODIFIED_BY },
                  new SqlParameter("@MODIFIED_AT", SqlDbType.DateTime) { Value = master.MODIFIED_AT },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_EQUIPMENT", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteEquipmentTypeList(string connstring, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@EQUIPMENT_TYPE_ID", SqlDbType.Int) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "DELETE_EQUIPMENT" }
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_EQUIPMENT", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<EQUIPMENT_TYPE_MASTER> SearchEquipment(string connstring, Boolean IS_ACTIVE, string EQUIPMENT_TYPE, string FROM_DATE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "SEARCH_EQUIPMENT_LIST" },
                   new SqlParameter("@IS_ACTIVE", SqlDbType.Bit) { Value = IS_ACTIVE },
                   new SqlParameter("@EQUIPMENT_TYPE", SqlDbType.VarChar, 100) { Value = EQUIPMENT_TYPE },
                   new SqlParameter("@CREATED_AT", SqlDbType.VarChar, 100) { Value = FROM_DATE },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_EQUIPMENT", parameters);
                List<EQUIPMENT_TYPE_MASTER> result = SqlHelper.CreateListFromTable<EQUIPMENT_TYPE_MASTER>(dataTable);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

    }
}


