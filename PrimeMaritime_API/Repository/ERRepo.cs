﻿using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Translators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Repository
{
    public class ERRepo
    {
        public static T GetSingleDataFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateItemFromRow<T>(dataTable.Rows[0]);
        }

        public static List<T> GetListFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateListFromTable<T>(dataTable);
        }

        public void InsertER(string connstring, EMPTY_REPO request,bool isVessel)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "CREATE_EMPTY_REPO" },
                  new SqlParameter("@REPO_NO", SqlDbType.VarChar,100) { Value = request.REPO_NO },
                  new SqlParameter("@LOAD_DEPOT", SqlDbType.VarChar,100) { Value = request.LOAD_DEPOT },
                  new SqlParameter("@DISCHARGE_DEPOT", SqlDbType.VarChar,100) { Value = request.DISCHARGE_DEPOT },
                  new SqlParameter("@LOAD_PORT", SqlDbType.VarChar,100) { Value = request.LOAD_PORT },
                  new SqlParameter("@DISCHARGE_PORT", SqlDbType.VarChar,100) { Value = request.DISCHARGE_PORT },
                  new SqlParameter("@VESSEL_NAME", SqlDbType.VarChar,255) { Value = request.VESSEL_NAME },
                  new SqlParameter("@VOYAGE_NO", SqlDbType.VarChar,50) { Value = request.VOYAGE_NO },
                  new SqlParameter("@MOVEMENT_DATE", SqlDbType.DateTime) { Value = request.MOVEMENT_DATE },
                  new SqlParameter("@LIFT_ON_CHARGE", SqlDbType.Decimal) { Value = request.LIFT_ON_CHARGE },
                  new SqlParameter("@LIFT_OFF_CHARGE", SqlDbType.Decimal) { Value = request.LIFT_OFF_CHARGE },
                  new SqlParameter("@CURRENCY", SqlDbType.VarChar, 50) { Value = request.CURRENCY },
                  new SqlParameter("@NO_OF_CONTAINER", SqlDbType.Int) { Value = request.NO_OF_CONTAINER },
                  new SqlParameter("@MODE_OF_TRANSPORT", SqlDbType.VarChar,50) { Value = request.MODE_OF_TRANSPORT },
                  new SqlParameter("@REASON", SqlDbType.VarChar, 255) { Value = request.REASON },
                  new SqlParameter("@REMARKS", SqlDbType.VarChar, 255) { Value = request.REMARKS },
                  new SqlParameter("@STATUS", SqlDbType.Int) { Value = request.STATUS },
                  new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 50) { Value = request.AGENT_CODE },
                  new SqlParameter("@AGENT_NAME", SqlDbType.VarChar, 255) { Value = request.AGENT_NAME },
                  new SqlParameter("@DEPO_CODE", SqlDbType.VarChar, 50) { Value = request.DEPO_CODE },
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 255) { Value = request.CREATED_BY }

                };

                var repoNO = SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_EMPTY_REPO", parameters);

                DataTable tbl = new DataTable();
                tbl.Columns.Add(new DataColumn("REPO_NO", typeof(string)));
                tbl.Columns.Add(new DataColumn("CONTAINER_NO", typeof(string)));
                tbl.Columns.Add(new DataColumn("CONTAINER_TYPE", typeof(string)));
                tbl.Columns.Add(new DataColumn("CONTAINER_SIZE", typeof(string)));
                tbl.Columns.Add(new DataColumn("AGENT_CODE", typeof(string)));
                tbl.Columns.Add(new DataColumn("AGENT_NAME", typeof(string)));
                tbl.Columns.Add(new DataColumn("DEPO_CODE", typeof(string)));
                tbl.Columns.Add(new DataColumn("CREATED_BY", typeof(string)));

                foreach (var i in request.CONTAINER_LIST)
                {
                    DataRow dr = tbl.NewRow();

                    dr["REPO_NO"] = request.REPO_NO;
                    dr["CONTAINER_NO"] = i.CONTAINER_NO;
                    dr["CONTAINER_TYPE"] = i.CONTAINER_TYPE;
                    dr["CONTAINER_SIZE"] = i.CONTAINER_SIZE;
                    //dr["SEAL_NO"] = i.SEAL_NO;
                    //dr["MARKS_NOS"] = i.MARKS_NOS;
                    //dr["DESC_OF_GOODS"] = i.DESC_OF_GOODS;
                    //dr["GROSS_WEIGHT"] = i.GROSS_WEIGHT;
                    //dr["MEASUREMENT"] = i.MEASUREMENT;
                    dr["AGENT_CODE"] = request.AGENT_CODE;
                    dr["AGENT_NAME"] = request.AGENT_NAME;
                    dr["DEPO_CODE"] = request.DEPO_CODE;
                    dr["CREATED_BY"] = request.CREATED_BY;

                    tbl.Rows.Add(dr);
                }

                string[] columns = new string[8];
                columns[0] = "REPO_NO";
                columns[1] = "CONTAINER_NO";
                columns[2] = "CONTAINER_TYPE";
                columns[3] = "CONTAINER_SIZE";
                columns[4] = "AGENT_CODE";
                columns[5] = "AGENT_NAME";
                columns[6] = "DEPO_CODE";
                columns[7] = "CREATED_BY";

                SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl, "TB_ER_CONTAINER", columns);

                DataTable tbl2 = new DataTable();
                tbl2.Columns.Add(new DataColumn("REPO_NO", typeof(string)));
                tbl2.Columns.Add(new DataColumn("CHARGE_CODE", typeof(string)));
                tbl2.Columns.Add(new DataColumn("CURRENCY", typeof(string)));
                tbl2.Columns.Add(new DataColumn("STANDARD_RATE", typeof(decimal)));
                tbl2.Columns.Add(new DataColumn("CREATED_BY", typeof(string)));

                foreach (var i in request.CONTAINER_RATES)
                {
                    DataRow dr = tbl2.NewRow();

                    dr["REPO_NO"] = request.REPO_NO;
                    dr["CHARGE_CODE"] = i.CHARGE_CODE;
                    dr["CURRENCY"] = i.CURRENCY;
                    dr["STANDARD_RATE"] = i.STANDARD_RATE;
                    dr["CREATED_BY"] = request.CREATED_BY;

                    tbl2.Rows.Add(dr);
                }

                string[] columns2 = new string[5];
                columns2[0] = "REPO_NO";
                columns2[1] = "CHARGE_CODE";
                columns2[2] = "CURRENCY";
                columns2[3] = "STANDARD_RATE";
                columns2[4] = "CREATED_BY";

                SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl2, "TB_ER_RATES", columns2);

                foreach (var items in request.CONTAINER_LIST)
                {
                    SqlParameter[] param =
                    {
                        new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "DELETE_CONTAINER" },
                        new SqlParameter("@CONTAINER_NO", SqlDbType.VarChar,50) { Value = items.CONTAINER_NO },
                    };

                    SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CONTAINER_MOVEMENT", param);
                }

                DataTable tbl1 = new DataTable();
                tbl1.Columns.Add(new DataColumn("CONTAINER_NO", typeof(string)));
                tbl1.Columns.Add(new DataColumn("ACTIVITY", typeof(string)));
                tbl1.Columns.Add(new DataColumn("ACTIVITY_DATE", typeof(DateTime)));
                tbl1.Columns.Add(new DataColumn("LOCATION", typeof(string)));
                tbl1.Columns.Add(new DataColumn("STATUS", typeof(string)));
                tbl1.Columns.Add(new DataColumn("DEPO_CODE", typeof(string)));
                tbl1.Columns.Add(new DataColumn("CREATED_BY", typeof(string)));

                foreach (var i in request.CONTAINER_LIST)
                {
                    DataRow dr = tbl1.NewRow();

                    dr["CONTAINER_NO"] = i.CONTAINER_NO;
                    dr["ACTIVITY"] = "LODE";
                    dr["ACTIVITY_DATE"] = request.MOVEMENT_DATE;
                    //dr["LOCATION"] = request.DISCHARGE_DEPOT;
                    dr["LOCATION"] = request.LOAD_PORT;  // new added
                    dr["STATUS"] = "Empty";
                    dr["CREATED_BY"] = request.CREATED_BY;

                    tbl1.Rows.Add(dr);
                }

                string[] columns1 = new string[6];
                columns1[0] = "CONTAINER_NO";
                columns1[1] = "ACTIVITY";
                columns1[2] = "ACTIVITY_DATE";
                columns1[3] = "LOCATION";
                columns1[4] = "STATUS";
                columns1[5] = "CREATED_BY";

                SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl1, "TB_CONTAINER_MOVEMENT", columns1);
                SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl1, "TB_CONTAINER_TRACKING", columns1);

                //SLOTENTRY
                if (isVessel)
                {
                    DataTable tbl3 = new DataTable();
                    tbl3.Columns.Add(new DataColumn("REPO_NO", typeof(string)));
                    tbl3.Columns.Add(new DataColumn("SLOT_OPERATOR", typeof(string)));
                    tbl3.Columns.Add(new DataColumn("NO_OF_SLOTS", typeof(int)));
                    tbl3.Columns.Add(new DataColumn("CREATED_BY", typeof(string)));

                    foreach (var i in request.SLOT_LIST)
                    {
                        DataRow dr = tbl3.NewRow();

                        dr["REPO_NO"] = request.REPO_NO;
                        dr["SLOT_OPERATOR"] = i.SLOT_OPERATOR;
                        dr["NO_OF_SLOTS"] = i.NO_OF_SLOTS;
                        dr["CREATED_BY"] = request.CREATED_BY;

                        tbl3.Rows.Add(dr);
                    }

                    string[] columns3 = new string[4];
                    columns3[0] = "REPO_NO";
                    columns3[1] = "SLOT_OPERATOR";
                    columns3[2] = "NO_OF_SLOTS";
                    columns3[3] = "CREATED_BY";

                    SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl3, "TB_SLOT_DETAILS", columns3);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<EMPTY_REPO> GetERList(string connstring, string AGENT_CODE,string DEPO_CODE, string ORG_CODE, string PORT)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_EMPTY_REPO_LIST" },
                  new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 50) { Value = AGENT_CODE },
                  new SqlParameter("@DEPO_CODE", SqlDbType.VarChar, 50) { Value = DEPO_CODE },
                  new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 20) { Value = ORG_CODE },
                  new SqlParameter("@PORT", SqlDbType.VarChar, 100) { Value = PORT }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_EMPTY_REPO", parameters);
                List<EMPTY_REPO> emptyRepoList = SqlHelper.CreateListFromTable<EMPTY_REPO>(dataTable);

                return emptyRepoList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EMPTY_REPO GetERDetails(string connstring, string REPO_NO, string ORG_CODE, string PORT)
        {
            try
            {
                SqlParameter[] parameters =
            {
                new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_EMPTY_REPO_DETAILS" },
                new SqlParameter("@REPO_NO", SqlDbType.VarChar, 100) { Value = REPO_NO },
                new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 20) { Value = ORG_CODE },
                new SqlParameter("@PORT", SqlDbType.VarChar, 100) { Value = PORT }
            };

                return SqlHelper.ExtecuteProcedureReturnData<EMPTY_REPO>(connstring, "SP_CRUD_EMPTY_REPO", r => r.TranslateER(), parameters);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<EMPTY_REPO> GetERDetailsBYMonthYear(string connstring, string MONTH, string YEAR)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_EMPTY_REPO_LIST_BY_MONTH_YEAR" },
                new SqlParameter("@MONTH", SqlDbType.VarChar, 100) { Value = MONTH },
                new SqlParameter("@YEAR", SqlDbType.VarChar, 20) { Value = YEAR }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_EMPTY_REPO", parameters);
                List<EMPTY_REPO> emptyRepoList = SqlHelper.CreateListFromTable<EMPTY_REPO>(dataTable);

                return emptyRepoList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ER_CONTAINER> GetERContainerDetails(string connstring, string REPO_NO, string AGENT_CODE, string DEPO_CODE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_ERCONTAINER_BY_REPO" },
                  new SqlParameter("@REPO_NO", SqlDbType.VarChar, 100) { Value = REPO_NO },
                  new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 50) { Value = AGENT_CODE },
                  new SqlParameter("@DEPO_CODE", SqlDbType.VarChar, 50) { Value = DEPO_CODE }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_EMPTY_REPO", parameters);
                List<ER_CONTAINER> erContList = SqlHelper.CreateListFromTable<ER_CONTAINER>(dataTable);

                return erContList;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<ER_RATES> GetERRateDetails(string connstring, string REPO_NO)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_ERRATES_BY_REPO" },
                  new SqlParameter("@REPO_NO", SqlDbType.VarChar, 100) { Value = REPO_NO }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_EMPTY_REPO", parameters);
                List<ER_RATES> erRateList = SqlHelper.CreateListFromTable<ER_RATES>(dataTable);

                return erRateList;
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
