using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Translators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrimeMaritime_API.Repository
{
    public class DetentionRepo
    {

        public static T GetSingleDataFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateItemFromRow<T>(dataTable.Rows[0]);
        }


        public List<DETENTION_WAIVER_REQUEST> GetDetentionList(string dbConn, string DO_NO)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "GET_DETENTION_WAIVER_BY_DO_NO" },
                   new SqlParameter("@DO_NO", SqlDbType.VarChar,100) { Value = DO_NO},
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_DETENTION", parameters);
                List<DETENTION_WAIVER_REQUEST> detention_Request = SqlHelper.CreateListFromTable<DETENTION_WAIVER_REQUEST>(dataTable);

                return detention_Request;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<DETENTION_WAIVER_REQUEST> GetDetentionListBL(string dbConn, string BL_NO)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "GET_DETENTION_WAIVER_BY_BL_NO" },
                   new SqlParameter("@BL_NO", SqlDbType.VarChar,100) { Value = BL_NO},
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_DETENTION", parameters);
                List<DETENTION_WAIVER_REQUEST> detention_Request = SqlHelper.CreateListFromTable<DETENTION_WAIVER_REQUEST>(dataTable);

                return detention_Request;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<DETENTION_WAIVER_REQUEST> GetDetentionListByLocation(string dbConn, string location)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "GET_DETENTION_LIST_BYLOCATION" },
                   new SqlParameter("@LOCATION", SqlDbType.VarChar,100) { Value = location},
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_DETENTION", parameters);
                List<DETENTION_WAIVER_REQUEST> detention_Request = SqlHelper.CreateListFromTable<DETENTION_WAIVER_REQUEST>(dataTable);

                return detention_Request;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<DETENTION_WAIVER_REQUEST> GetDetentionListByLocationAndDetentionType(string dbConn, string location, string DETENTION_TYPE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "GET_DETENTION_LIST_BYLOCATION_BYDETENTIONTYPE" },
                   new SqlParameter("@LOCATION", SqlDbType.VarChar,100) { Value = location},
                   new SqlParameter("@DETENTION_TYPE", SqlDbType.VarChar,100) { Value = DETENTION_TYPE},
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_DETENTION", parameters);
                List<DETENTION_WAIVER_REQUEST> detention_Request = SqlHelper.CreateListFromTable<DETENTION_WAIVER_REQUEST>(dataTable);

                return detention_Request;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<DETENTION_WAIVER_REQUEST> GetDetentionMasterList(string dbConn, string LOCATION, string MONTH, string YEAR)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "GET_DETENTION_MASTER_LIST" },
                   new SqlParameter("@LOCATION", SqlDbType.VarChar,100) { Value = LOCATION},
                   new SqlParameter("@MONTH", SqlDbType.VarChar,100) { Value = MONTH},
                   new SqlParameter("@YEAR", SqlDbType.VarChar,100) { Value = YEAR},
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_DETENTION", parameters);
                List<DETENTION_WAIVER_REQUEST> detention_Request = SqlHelper.CreateListFromTable<DETENTION_WAIVER_REQUEST>(dataTable);

                return detention_Request;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void InsertDetention(string connstr, DETENTION request)
        {
            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add(new DataColumn("DO_NO", typeof(string)));
                tbl.Columns.Add(new DataColumn("CONTAINER_NO", typeof(string)));
                tbl.Columns.Add(new DataColumn("LOCATION", typeof(string)));
                tbl.Columns.Add(new DataColumn("IMPORTER", typeof(string)));
                tbl.Columns.Add(new DataColumn("CLEARING_PARTY", typeof(string)));
                tbl.Columns.Add(new DataColumn("DETENTION_DAYS", typeof(int)));
                tbl.Columns.Add(new DataColumn("DETENTION_RATE", typeof(decimal)));
                tbl.Columns.Add(new DataColumn("CURRENCY", typeof(string)));
                tbl.Columns.Add(new DataColumn("REMARKS", typeof(string)));
                tbl.Columns.Add(new DataColumn("CREATED_BY", typeof(string)));
                tbl.Columns.Add(new DataColumn("return_date", typeof(string)));
                tbl.Columns.Add(new DataColumn("IS_JUMPING", typeof(string)));
                tbl.Columns.Add(new DataColumn("STATUS", typeof(string)));
                tbl.Columns.Add(new DataColumn("discount", typeof(string)));
                tbl.Columns.Add(new DataColumn("CONTAINER_TYPE", typeof(string)));
                tbl.Columns.Add(new DataColumn("DETENTION_TYPE", typeof(string)));
                tbl.Columns.Add(new DataColumn("BL_NO", typeof(string)));
                tbl.Columns.Add(new DataColumn("discharge_date", typeof(string)));



                foreach (var i in request.DETENTION_LIST)
                {
                    DataRow dr = tbl.NewRow();

                    dr["DO_NO"] = request.DO_NO;
                    dr["CONTAINER_NO"] = i.CONTAINER_NO;
                    dr["LOCATION"] = i.PORT_OF_DISCHARGE;
                    dr["IMPORTER"] = i.CONSIGNEE;
                    dr["CLEARING_PARTY"] = i.CLEARING_PARTY;
                    dr["DETENTION_DAYS"] = i.DETENTION_DAYS;
                    dr["DETENTION_RATE"] = i.DETENTION_RATE;
                    dr["CURRENCY"] = i.CURRENCY;
                    dr["REMARKS"] = i.REMARK;
                    dr["CREATED_BY"] = i.CREATED_BY;
                    dr["return_date"] = i.return_date;
                    dr["IS_JUMPING"] = i.IS_JUMPING;
                    dr["STATUS"] = null;
                    dr["discount"] = i.discount;
                    dr["CONTAINER_TYPE"] = i.CONTAINER_TYPE;
                    dr["DETENTION_TYPE"] = i.DETENTION_TYPE;
                    dr["BL_NO"] = request.BL_NO;
                    dr["discharge_date"] = i.discharge_date;


                    tbl.Rows.Add(dr);
                }

                string[] columns = new string[17];
                columns[0] = "DO_NO";
                columns[1] = "CONTAINER_NO";
                columns[2] = "LOCATION";
                columns[3] = "IMPORTER";
                columns[4] = "CLEARING_PARTY";
                columns[5] = "DETENTION_DAYS";
                columns[6] = "DETENTION_RATE";
                columns[7] = "CURRENCY";
                columns[8] = "REMARKS";
                columns[9] = "CREATED_BY";
                columns[10] = "return_date";
                columns[11] = "IS_JUMPING";
                columns[12] = "STATUS";
                columns[13] = "discount";
                columns[14] = "CONTAINER_TYPE";
                columns[14] = "DETENTION_TYPE";
                columns[15] = "BL_NO";
                columns[16] = "discharge_date";

                SqlHelper.ExecuteProcedureBulkInsert(connstr, tbl, "TB_DETENTION", columns);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateDetention(string connstr, DETENTION request)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                {
                    conn.Open();

                    foreach (var i in request.DETENTION_LIST)
                    {
                        using (SqlCommand cmd = new SqlCommand(@"
                    UPDATE TB_DETENTION 
                    SET 
                        LOCATION = @LOCATION,
                        IMPORTER = @IMPORTER,
                        CLEARING_PARTY = @CLEARING_PARTY,
                        DETENTION_DAYS = @DETENTION_DAYS,
                        DETENTION_RATE = @DETENTION_RATE,
                        CURRENCY = @CURRENCY,
                        REMARKS = @REMARKS,
                        CREATED_BY = @CREATED_BY,
                        return_date = @return_date,
                        IS_JUMPING = @IS_JUMPING,
                        discount = @discount,
                        CONTAINER_TYPE = @CONTAINER_TYPE,
                        STATUS = 'Finalized'
                    WHERE DO_NO = @DO_NO AND CONTAINER_NO = @CONTAINER_NO", conn))
                        {
                            cmd.Parameters.AddWithValue("@DO_NO", request.DO_NO);
                            cmd.Parameters.AddWithValue("@CONTAINER_NO", i.CONTAINER_NO);
                            cmd.Parameters.AddWithValue("@LOCATION", i.PORT_OF_DISCHARGE);
                            cmd.Parameters.AddWithValue("@IMPORTER", i.CONSIGNEE);
                            cmd.Parameters.AddWithValue("@CLEARING_PARTY", i.CLEARING_PARTY);
                            cmd.Parameters.AddWithValue("@DETENTION_DAYS", i.DETENTION_DAYS);
                            cmd.Parameters.AddWithValue("@DETENTION_RATE", i.DETENTION_RATE);
                            cmd.Parameters.AddWithValue("@CURRENCY", i.CURRENCY);
                            cmd.Parameters.AddWithValue("@REMARKS", i.REMARK);
                            cmd.Parameters.AddWithValue("@CREATED_BY", i.CREATED_BY);
                            cmd.Parameters.AddWithValue("@return_date", i.return_date);
                            cmd.Parameters.AddWithValue("@IS_JUMPING", i.IS_JUMPING);
                            cmd.Parameters.AddWithValue("@discount", i.discount);
                            cmd.Parameters.AddWithValue("@CONTAINER_TYPE", i.CONTAINER_TYPE);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateDetentionByBL(string connstr, DETENTION request)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                {
                    conn.Open();

                    foreach (var i in request.DETENTION_LIST)
                    {
                        using (SqlCommand cmd = new SqlCommand(@"
                    UPDATE TB_DETENTION 
                    SET 
                        DETENTION_DAYS = @DETENTION_DAYS,
                        DETENTION_RATE = @DETENTION_RATE,
                        return_date = @return_date,
                        IS_JUMPING = @IS_JUMPING,
                        discount = @discount,
                        STATUS = 'Finalized'
                    WHERE BL_NO = @BL_NO AND CONTAINER_NO = @CONTAINER_NO", conn))
                        {
                            cmd.Parameters.AddWithValue("@BL_NO", request.BL_NO);
                            cmd.Parameters.AddWithValue("@DETENTION_DAYS", i.DETENTION_DAYS);
                            cmd.Parameters.AddWithValue("@CONTAINER_NO", i.CONTAINER_NO);
                            cmd.Parameters.AddWithValue("@DETENTION_RATE", i.DETENTION_RATE);
                            cmd.Parameters.AddWithValue("@return_date", i.return_date);
                            cmd.Parameters.AddWithValue("@IS_JUMPING", i.IS_JUMPING);
                            cmd.Parameters.AddWithValue("@discount", i.discount);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GetTotalDetentionCost(string connstring, string CONTAINER_NO)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "DETENTION_CALCULATION" },
                  new SqlParameter("@CONTAINER_NO", SqlDbType.VarChar, 100) { Value = CONTAINER_NO },
                };

                return SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_DETENTION", parameters);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<CONTAINER_DETENTION> GetContainerDetentionList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "CONTAINER_DETENTION_CALCULATION" },
                   
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_DETENTION", parameters);
                List<CONTAINER_DETENTION> detention_Request = SqlHelper.CreateListFromTable<CONTAINER_DETENTION>(dataTable);

                return detention_Request;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public DataSet GetDODetailsForDetention(string dbConn, string DO_NO)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "GET_DO_DETAILS_FOR_DETEINTION" },
                   new SqlParameter("@DO_NO", SqlDbType.VarChar,100) { Value = DO_NO},
                };

                return SqlHelper.ExtecuteProcedureReturnDataSet(dbConn, "SP_CRUD_DETENTION", parameters);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public DataSet GetBLDetailsForDetention(string dbConn, string BL_NO)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "GET_BL_DETAILS_FOR_DETEINTION" },
                   new SqlParameter("@BL_NO", SqlDbType.VarChar,100) { Value = BL_NO},
                };

                return SqlHelper.ExtecuteProcedureReturnDataSet(dbConn, "SP_CRUD_DETENTION", parameters);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public DETENTION_MASTER GetDetentionCharges(string connstring, string ACCEPTANCE_LOCATION, int DAYS, string CURRENCY_CODE, string CONTAINER_TYPE, string IS_JUMPING,int FREEDAYS)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "GET_DETENTION_CHARGES" },
                   new SqlParameter("@ACCEPTANCE_LOCATION", SqlDbType.VarChar, 100) { Value = ACCEPTANCE_LOCATION },
                   new SqlParameter("@DAYS", SqlDbType.Int) { Value = DAYS },
                   new SqlParameter("@CURRENCY_CODE", SqlDbType.VarChar, 50) { Value = CURRENCY_CODE },
                   new SqlParameter("@CONTAINER_TYPE", SqlDbType.VarChar, 100) { Value = CONTAINER_TYPE },
                   new SqlParameter("@IS_JUMPING", SqlDbType.VarChar, 50) { Value = IS_JUMPING },
                   new SqlParameter("@FREEDAYS", SqlDbType.VarChar, 50) { Value = FREEDAYS },
                   new SqlParameter("@OUTPUT", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output },
                };

                var result = SqlHelper.ExtecuteProcedureReturnData<DETENTION_MASTER>(connstring, "SP_CRUD_DETENTION", r => r.TranslateAsDetentionMaster(), parameters);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

