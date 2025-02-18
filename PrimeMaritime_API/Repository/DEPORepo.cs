using PrimeMaritime_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Response;
using System.IO;
using PrimeMaritime_API.Translators;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Reflection;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities;

namespace PrimeMaritime_API.Repository
{
    public class DEPORepo
    {
        public void InsertContainer(string connstring, DEPO_CONTAINER request)
        {
            foreach (var items in request.CONTAINER_LIST)
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "DELETE_CONTAINER" },
                   new SqlParameter("@CONTAINER_NO", SqlDbType.VarChar,50) { Value = items.CONTAINER_NO },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CONTAINER_MOVEMENT", parameters);
            }

            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("BOOKING_NO", typeof(string)));
            tbl.Columns.Add(new DataColumn("CRO_NO", typeof(string)));
            tbl.Columns.Add(new DataColumn("CONTAINER_NO", typeof(string)));
            tbl.Columns.Add(new DataColumn("CREATED_BY", typeof(string)));
            tbl.Columns.Add(new DataColumn("DEPO_CODE", typeof(string)));

            foreach (var i in request.CONTAINER_LIST)
            {
                DataRow dr = tbl.NewRow();

                dr["BOOKING_NO"] = request.BOOKING_NO;
                dr["CRO_NO"] = request.CRO_NO;
                dr["CONTAINER_NO"] = i.CONTAINER_NO;
                dr["CREATED_BY"] = request.CREATED_BY;
                dr["DEPO_CODE"] = request.DEPO_CODE;

                tbl.Rows.Add(dr);
            }

            string[] columns = new string[5];
            columns[0] = "BOOKING_NO";
            columns[1] = "CRO_NO";
            columns[2] = "CONTAINER_NO";
            columns[3] = "CREATED_BY";
            columns[4] = "DEPO_CODE";

            SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl, "TB_CONTAINER", columns);

            DataTable tbl1 = new DataTable();
            tbl1.Columns.Add(new DataColumn("BOOKING_NO", typeof(string)));
            tbl1.Columns.Add(new DataColumn("CRO_NO", typeof(string)));
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

                dr["BOOKING_NO"] = request.BOOKING_NO;
                dr["CRO_NO"] = request.CRO_NO;
                dr["CONTAINER_NO"] = i.CONTAINER_NO;
                dr["ACTIVITY"] = "SNTS";
                dr["ACTIVITY_DATE"] = i.MOVEMENT_DATE;
                dr["LOCATION"] = i.TO_LOCATION;
                dr["STATUS"] = "Empty";
                dr["DEPO_CODE"] = request.DEPO_CODE;
                dr["CREATED_BY"] = request.CREATED_BY;

                tbl1.Rows.Add(dr);
            }

            string[] columns1 = new string[9];
            columns1[0] = "BOOKING_NO";
            columns1[1] = "CRO_NO";
            columns1[2] = "CONTAINER_NO";
            columns1[3] = "ACTIVITY";
            columns1[4] = "ACTIVITY_DATE";
            columns1[5] = "LOCATION";
            columns1[6] = "STATUS";
            columns1[7] = "DEPO_CODE";
            columns1[8] = "CREATED_BY";

            SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl1, "TB_CONTAINER_MOVEMENT", columns1);          
            SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl1, "TB_CONTAINER_TRACKING", columns1);
        }

        public void InsertMRRequest(string connstring, List<MR_LIST> request)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "INSERT_MNR" },
                new SqlParameter("@MR_NO", SqlDbType.VarChar, 100) { Value = request[0].MR_NO },
                new SqlParameter("@DEPO_CODE", SqlDbType.VarChar, 50) { Value = request[0].DEPO_CODE },
                new SqlParameter("@STATUS", SqlDbType.VarChar, 50) { Value = "Requested" },
                new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 255) { Value = request[0].CREATED_BY },
            };

            SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_MNR", parameters);

            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("MR_NO", typeof(string)));
            tbl.Columns.Add(new DataColumn("CONTAINER_NO", typeof(string)));
            tbl.Columns.Add(new DataColumn("LOCATION", typeof(string)));
            tbl.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            tbl.Columns.Add(new DataColumn("DAMAGE", typeof(string)));
            tbl.Columns.Add(new DataColumn("REPAIR", typeof(string)));
            tbl.Columns.Add(new DataColumn("DESC", typeof(string)));
            tbl.Columns.Add(new DataColumn("LENGTH", typeof(string)));
            tbl.Columns.Add(new DataColumn("WIDTH", typeof(string)));
            tbl.Columns.Add(new DataColumn("HEIGHT", typeof(string)));
            tbl.Columns.Add(new DataColumn("UNIT", typeof(string)));
            tbl.Columns.Add(new DataColumn("RESPONSIBILITY", typeof(string)));
            tbl.Columns.Add(new DataColumn("MAN_HOUR", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("LABOUR", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("MATERIAL", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("TOTAL", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("TAX", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("FINAL_TOTAL", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("REMARKS", typeof(string)));
            tbl.Columns.Add(new DataColumn("STATUS", typeof(string)));
            tbl.Columns.Add(new DataColumn("CREATED_BY", typeof(string)));

            foreach (var i in request)
            {
                DataRow dr = tbl.NewRow();

                dr["MR_NO"] = request[0].MR_NO;
                dr["CONTAINER_NO"] = i.CONTAINER_NO;
                dr["LOCATION"] = i.LOCATION;
                dr["COMPONENT"] = i.COMPONENT;
                dr["DAMAGE"] = i.DAMAGE;
                dr["REPAIR"] = i.REPAIR;
                dr["DESC"] = i.DESC;
                dr["LENGTH"] = i.LENGTH;
                dr["WIDTH"] = i.WIDTH;
                dr["HEIGHT"] = i.HEIGHT;
                dr["UNIT"] = i.UNIT;
                dr["RESPONSIBILITY"] = i.RESPONSIBILITY;
                dr["MAN_HOUR"] = i.MAN_HOUR;
                dr["LABOUR"] = i.LABOUR;
                dr["MATERIAL"] = i.MATERIAL;
                dr["TOTAL"] = i.LABOUR + i.MATERIAL;
                dr["TAX"] = i.TAX;
                dr["FINAL_TOTAL"] = i.FINAL_TOTAL;
                dr["REMARKS"] = i.REMARKS;
                dr["STATUS"] = "Requested";
                dr["CREATED_BY"] = i.CREATED_BY;

                tbl.Rows.Add(dr);
            }

            string[] columns = new string[21];
            columns[0] = "LOCATION";
            columns[1] = "COMPONENT";
            columns[2] = "DAMAGE";
            columns[3] = "REPAIR";
            columns[4] = "DESC";
            columns[5] = "LENGTH";
            columns[6] = "WIDTH";
            columns[7] = "HEIGHT";
            columns[8] = "UNIT";
            columns[9] = "RESPONSIBILITY";
            columns[10] = "MAN_HOUR";
            columns[11] = "LABOUR";
            columns[12] = "MATERIAL";
            columns[13] = "TOTAL";
            columns[14] = "CONTAINER_NO";
            columns[15] = "TAX";
            columns[16] = "FINAL_TOTAL";
            columns[17] = "STATUS";
            columns[18] = "MR_NO";
            columns[19] = "REMARKS";
            columns[20] = "CREATED_BY";

            SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl, "TB_MR_REQUEST", columns);
        }

        public List<MNR_LIST> GetMNRList(string connstring, string OPERATION, string DEPO_CODE, string MR_NO, string STATUS, string FROMDATE, string TODATE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  //new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = OPERATION },
                  //new SqlParameter("@DEPO_CODE", SqlDbType.VarChar, 50) { Value = DEPO_CODE },

                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = OPERATION },
                  new SqlParameter("@DEPO_CODE", SqlDbType.VarChar, 50) { Value = DEPO_CODE },
                  new SqlParameter("@MR_NO", SqlDbType.VarChar, 100) { Value = MR_NO },
                  new SqlParameter("@STATUS", SqlDbType.VarChar, 50) { Value = STATUS },
                  new SqlParameter("@FROMDATE", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(FROMDATE) ? null : Convert.ToDateTime(FROMDATE) },
                  new SqlParameter("@TODATE", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(TODATE) ? null : Convert.ToDateTime(TODATE) },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_MNR", parameters);
                List<MNR_LIST> Responses = SqlHelper.CreateListFromTable<MNR_LIST>(dataTable);

                return Responses;
            }
            catch (Exception)
            {
                throw;
            }
        }


        //public List<MR_LIST> GetMRREQDetails(string connstring, string OPERATION, string MR_NO)
        //{
        //    try
        //    {
        //        SqlParameter[] parameters =
        //        {
        //    new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = OPERATION },
        //    new SqlParameter("@MR_NO", SqlDbType.VarChar, 50) { Value = MR_NO },
        //};

        //        DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_MNR", parameters);
        //        List<MR_LIST> Responses = SqlHelper.CreateListFromTable<MR_LIST>(dataTable);

        //        // **Manually deserialize IMAGE_DETAILS**
        //        foreach (var response in Responses)
        //        {
        //            if (!string.IsNullOrWhiteSpace(response.IMAGE_DETAILS_JSON))
        //            {
        //                try
        //                {
        //                    string json = response.IMAGE_DETAILS_JSON.Trim();

        //                    // Fix JSON escape sequences by replacing double backslashes
        //                    json = json.Replace("\\", "/");  // Converts Windows-style paths

        //                    // Debugging Log
        //                    Console.WriteLine("Normalized JSON: " + json);

        //                    // Deserialize JSON
        //                    response.IMAGE_DETAILS = JsonConvert.DeserializeObject<List<ImageDetail>>(json) ?? new List<ImageDetail>();

        //                    Console.WriteLine("Deserialized IMAGE_DETAILS Count: " + response.IMAGE_DETAILS.Count);
        //                }
        //                catch (JsonException ex)
        //                {
        //                    Console.WriteLine("JSON Deserialization Error: " + ex.Message);
        //                    response.IMAGE_DETAILS = new List<ImageDetail>();
        //                }
        //            }
        //            else
        //            {
        //                response.IMAGE_DETAILS = new List<ImageDetail>();
        //            }
        //        }



        //        return Responses;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error: " + ex.Message);
        //        throw;
        //    }
        //}

        public List<MR_LIST> GetMRREQDetails(string connstring, string OPERATION, string MR_NO)
        {
            try
            {
                SqlParameter[] parameters =
                {
            new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = OPERATION },
            new SqlParameter("@MR_NO", SqlDbType.VarChar, 50) { Value = MR_NO },
        };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_MNR", parameters);
                List<MR_LIST> Responses = SqlHelper.CreateListFromTable<MR_LIST>(dataTable);

                // **Manually deserialize IMAGE_DETAILS**
                foreach (var response in Responses)
                {
                    if (!string.IsNullOrWhiteSpace(response.IMAGE_DETAILS_JSON))
                    {
                        try
                        {
                            string json = response.IMAGE_DETAILS_JSON.Trim();

                            // Debugging Log: Log original JSON before any changes
                            Console.WriteLine("Raw JSON: " + json);

                            // Fix JSON escape sequences by replacing double backslashes (Windows paths)
                            json = json.Replace("\\", "/");

                            // Debugging Log: Log normalized JSON
                            Console.WriteLine("Normalized JSON: " + json);

                            // Deserialize JSON
                            response.IMAGE_DETAILS = JsonConvert.DeserializeObject<List<ImageDetail>>(json) ?? new List<ImageDetail>();

                            // Debugging Log: Check the count after deserialization
                            Console.WriteLine("Deserialized IMAGE_DETAILS Count: " + response.IMAGE_DETAILS.Count);
                        }
                        catch (JsonException ex)
                        {
                            // Log the deserialization error
                            Console.WriteLine("JSON Deserialization Error: " + ex.Message);
                            response.IMAGE_DETAILS = new List<ImageDetail>();
                        }
                    }
                    else
                    {
                        // Ensure that the list is initialized even if the JSON is missing or empty
                        response.IMAGE_DETAILS = new List<ImageDetail>();
                    }
                }

                return Responses;
            }
            catch (Exception ex)
            {
                // Log any other errors
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }


        public List<MR_LIST> getMRDetailsByID(string connstring, string OPERATION, string MR_NO, int ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = OPERATION },
                  new SqlParameter("@MR_NO", SqlDbType.VarChar, 50) { Value = MR_NO },
                  new SqlParameter("ID", SqlDbType.VarChar, 50) { Value = ID },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_MNR", parameters);
                List<MR_LIST> Responses = SqlHelper.CreateListFromTable<MR_LIST>(dataTable);

                return Responses;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void ApproveRate(string connstring, List<MR_LIST> request)
        {
            try
            {
                string[] columns = new string[6];
                columns[0] = "MR_NO";
                columns[1] = "LOCATION";
                columns[2] = "FINAL_TOTAL";
                columns[3] = "TAX";
                columns[4] = "REMARKS";
                columns[5] = "STATUS";

                SqlHelper.UpdateMRData<MR_LIST>(request, "TB_MR_REQUEST", connstring, columns);

                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "APPROVE_RATE" },
                  new SqlParameter("@MR_NO", SqlDbType.VarChar, 100) { Value = request[0].MR_NO },
                  new SqlParameter("@STATUS", SqlDbType.VarChar, 100) { Value = request[0].STATUS },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_MNR", parameters);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertNewMRRequest(string connstring, List<MR_LIST> request)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("MR_NO", typeof(string)));
            tbl.Columns.Add(new DataColumn("CONTAINER_NO", typeof(string)));
            tbl.Columns.Add(new DataColumn("LOCATION", typeof(string)));
            tbl.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            tbl.Columns.Add(new DataColumn("DAMAGE", typeof(string)));
            tbl.Columns.Add(new DataColumn("REPAIR", typeof(string)));
            tbl.Columns.Add(new DataColumn("DESC", typeof(string)));
            tbl.Columns.Add(new DataColumn("LENGTH", typeof(string)));
            tbl.Columns.Add(new DataColumn("WIDTH", typeof(string)));
            tbl.Columns.Add(new DataColumn("HEIGHT", typeof(string)));
            tbl.Columns.Add(new DataColumn("UNIT", typeof(string)));
            tbl.Columns.Add(new DataColumn("RESPONSIBILITY", typeof(string)));
            tbl.Columns.Add(new DataColumn("MAN_HOUR", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("LABOUR", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("MATERIAL", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("TOTAL", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("TAX", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("FINAL_TOTAL", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("REMARKS", typeof(string)));
            tbl.Columns.Add(new DataColumn("STATUS", typeof(string)));
            tbl.Columns.Add(new DataColumn("CREATED_BY", typeof(string)));

            foreach (var i in request)
            {
                DataRow dr = tbl.NewRow();

                dr["MR_NO"] = request[0].MR_NO;
                dr["CONTAINER_NO"] = i.CONTAINER_NO;
                dr["LOCATION"] = i.LOCATION;
                dr["COMPONENT"] = i.COMPONENT;
                dr["DAMAGE"] = i.DAMAGE;
                dr["REPAIR"] = i.REPAIR;
                dr["DESC"] = i.DESC;
                dr["LENGTH"] = i.LENGTH;
                dr["WIDTH"] = i.WIDTH;
                dr["HEIGHT"] = i.HEIGHT;
                dr["UNIT"] = i.UNIT;
                dr["RESPONSIBILITY"] = i.RESPONSIBILITY;
                dr["MAN_HOUR"] = i.MAN_HOUR;
                dr["LABOUR"] = i.LABOUR;
                dr["MATERIAL"] = i.MATERIAL;
                dr["TOTAL"] = i.LABOUR + i.MATERIAL;
                dr["TAX"] = i.TAX;
                dr["FINAL_TOTAL"] = i.FINAL_TOTAL;
                dr["REMARKS"] = i.REMARKS;
                dr["STATUS"] = "Requested";
                dr["CREATED_BY"] = i.CREATED_BY;

                tbl.Rows.Add(dr);
            }

            string[] columns = new string[21];
            columns[0] = "LOCATION";
            columns[1] = "COMPONENT";
            columns[2] = "DAMAGE";
            columns[3] = "REPAIR";
            columns[4] = "DESC";
            columns[5] = "LENGTH";
            columns[6] = "WIDTH";
            columns[7] = "HEIGHT";
            columns[8] = "UNIT";
            columns[9] = "RESPONSIBILITY";
            columns[10] = "MAN_HOUR";
            columns[11] = "LABOUR";
            columns[12] = "MATERIAL";
            columns[13] = "TOTAL";
            columns[14] = "CONTAINER_NO";
            columns[15] = "TAX";
            columns[16] = "FINAL_TOTAL";
            columns[17] = "STATUS";
            columns[18] = "MR_NO";
            columns[19] = "REMARKS";
            columns[20] = "CREATED_BY";

            SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl, "TB_MR_REQUEST", columns);
        }

        public List<string> GetFiles(string MR_NO)
        {
            string[] array1 = Directory.GetFiles("Uploads\\MNRFiles");
            List<string> array2 = new List<string>();

            // Get list of files.
            List<string> filesList = array1.ToList();

            foreach (var file in filesList)
            {
                if (file.Contains(MR_NO))
                {
                    array2.Add(file);

                }
            }
            return array2;
        }

        public void DeleteMRequest(string connstring, string MR_NO, string LOCATION)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "DELETE_MR_REQUEST" },
                new SqlParameter("@MR_NO", SqlDbType.VarChar, 100) { Value = MR_NO },
                new SqlParameter("@LOCATION", SqlDbType.VarChar, 100) { Value = LOCATION },
            };

            SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_MNR", parameters);
        }

        public MNR_TARIFF GetMNRTariff(string connstring, string COMPONENT, string DAMAGE_LOCATION, string REPAIR, string LENGTH, string WIDTH, string HEIGHT, string QUANTITY, string DEPO_CODE)
        {
            try
            {
                SqlParameter[] parameters =
            {
                new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_MNR_TARIFF" },
                new SqlParameter("@COMPONENT", SqlDbType.VarChar, 50) { Value = COMPONENT },
                new SqlParameter("@DAMAGE_LOCATION", SqlDbType.VarChar, 50) { Value = DAMAGE_LOCATION },
                new SqlParameter("@REPAIR", SqlDbType.VarChar, 50) { Value = REPAIR },
                new SqlParameter("@LENGTH", SqlDbType.VarChar, 50) { Value = LENGTH },
                new SqlParameter("@WIDTH", SqlDbType.VarChar, 50) { Value = WIDTH },
                new SqlParameter("@HEIGHT", SqlDbType.VarChar, 50) { Value = HEIGHT },
                new SqlParameter("@QUANTITY", SqlDbType.VarChar, 50) { Value = QUANTITY },
                new SqlParameter("@DEPO_CODE", SqlDbType.VarChar, 50) { Value = DEPO_CODE },
            };

                return SqlHelper.ExtecuteProcedureReturnData<MNR_TARIFF>(connstring, "SP_CRUD_MNR", r => r.TranslateMNR(), parameters);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void InsertMNRFiles(string connstring, List<MR_LIST> newMNR, Dictionary<string, List<string>> attachmentPaths)
        {
            try
            {

                // Check if container exists in TB_MR_REQUEST
                string ExistingMRNo;

                SqlParameter[] parameters4 =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "EXISTS_MRNO" },
                   new SqlParameter("@RETURN_MRNO", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output },
                   new SqlParameter("@MR_NO", SqlDbType.VarChar, 100) { Value = newMNR[0].MR_NO },
                 };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_MNR", parameters4);
                ExistingMRNo = Convert.ToString(parameters4[1].Value);

                // If not found in TB_MR_REQUEST
                if (string.IsNullOrEmpty(ExistingMRNo))
                {
                    SqlParameter[] parameters =
                    {
                      new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "INSERT_MNR" },
                      new SqlParameter("@MR_NO", SqlDbType.VarChar, 100) { Value = newMNR[0].MR_NO },
                      new SqlParameter("@DEPO_CODE", SqlDbType.VarChar, 50) { Value = newMNR[0].DEPO_CODE },
                      new SqlParameter("@STATUS", SqlDbType.VarChar, 50) { Value = "Requested" },
                      new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 255) { Value = newMNR[0].CREATED_BY },
                    };

                    SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_MNR", parameters);

                }

                foreach (var items in newMNR)
                {

                    //Insert MR Request data
                    SqlParameter[] updateMRData =
                       {
                           new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "INSERT_MR_REQUEST" },
                           new SqlParameter("@MR_NO", SqlDbType.VarChar, 100) { Value = items.MR_NO },
                           new SqlParameter("@CONTAINER_NO", SqlDbType.VarChar, 50) { Value = items.CONTAINER_NO },
                           new SqlParameter("@LOCATION", SqlDbType.VarChar, 50) { Value = items.LOCATION },
                           new SqlParameter("@COMPONENT", SqlDbType.VarChar, 50) { Value = items.COMPONENT },
                           new SqlParameter("@DAMAGE", SqlDbType.VarChar, 50) { Value = items.DAMAGE },
                           new SqlParameter("@REPAIR", SqlDbType.VarChar, 50) { Value = items.REPAIR },
                           new SqlParameter("@DESC", SqlDbType.VarChar, 255) { Value = items.DESC },
                           new SqlParameter("@LENGTH", SqlDbType.VarChar, 50) { Value = items.LENGTH },
                           new SqlParameter("@WIDTH", SqlDbType.VarChar, 50) { Value = items.WIDTH },
                           new SqlParameter("@HEIGHT", SqlDbType.VarChar, 50) { Value = items.HEIGHT },
                           new SqlParameter("@UNIT", SqlDbType.VarChar, 10) { Value = items.UNIT },
                           new SqlParameter("@RESPONSIBILITY", SqlDbType.VarChar, 50) { Value = items.RESPONSIBILITY },
                           new SqlParameter("@MAN_HOUR", SqlDbType.Decimal) { Value = items.MAN_HOUR, Precision =18, Scale=2 },
                           new SqlParameter("@LABOUR", SqlDbType.Decimal) { Value = items.LABOUR, Precision =18, Scale=2 },
                           new SqlParameter("@MATERIAL", SqlDbType.Decimal) { Value = items.MATERIAL, Precision =18, Scale=2 },
                           new SqlParameter("@TOTAL", SqlDbType.Decimal) { Value = items.TOTAL, Precision =18, Scale=2 },
                           new SqlParameter("@TAX", SqlDbType.Decimal) { Value = items.TAX, Precision =18, Scale=2 },
                           new SqlParameter("@FINAL_TOTAL", SqlDbType.Decimal) { Value = items.FINAL_TOTAL, Precision =18, Scale=2 },
                           new SqlParameter("@REMARKS", SqlDbType.VarChar, 255) { Value = items.REMARKS },
                           new SqlParameter("@STATUS", SqlDbType.VarChar, 50) { Value = "Requested" },
                           new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 255) { Value = items.CREATED_BY },
                          //new SqlParameter("@MNRFILE_PATH", SqlDbType.VarChar, 255) { Value = filePath }
                          new SqlParameter("@DAMAGE_LOCATION", SqlDbType.VarChar, 50) { Value = items.DAMAGE_LOCATION }
                        };

                    var ID = SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_MNR", updateMRData);



                    DataTable tblImages = new DataTable();
                    tblImages.Columns.Add(new DataColumn("MR_ID", typeof(int)));
                    tblImages.Columns.Add(new DataColumn("MR_NO", typeof(string)));
                    tblImages.Columns.Add(new DataColumn("IMAGE_PATH", typeof(string)));


                    if (attachmentPaths.ContainsKey(items.MR_NO))
                    {
                        foreach (var path in attachmentPaths[items.MR_NO])
                        {
                            DataRow drImg = tblImages.NewRow();
                            drImg["MR_ID"] = Convert.ToInt32(ID);
                            drImg["MR_NO"] = items.MR_NO;  
                            drImg["IMAGE_PATH"] = path;
                            tblImages.Rows.Add(drImg);
                        }
                    }

                    string[] imageColumns = new string[] { "MR_ID", "MR_NO", "IMAGE_PATH" };
                    SqlHelper.ExecuteProcedureBulkInsert(connstring, tblImages, "TB_MR_IMAGES", imageColumns);


                }

            }
            catch (Exception ex)
            {
                // Log the error and rethrow
                //throw new Exception($"Error fetching container details: {ex.Message}", ex);
                throw new Exception(ex.Message);
            }
        }

        public void DeleteMRImage(string connstring, int ID, int MR_ID)
        {
            try 
            {

                SqlParameter[] parameters =
               {
                 new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "DELETE_MR_IMAGE" },
                 new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                 new SqlParameter("@MR_ID", SqlDbType.Int) { Value = MR_ID },

               };

                SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_MNR", parameters);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void updateMRRequest(string connstring, List<MR_LIST> updateMNRList, Dictionary<string, List<string>> attachmentPaths)
        {
            //SqlParameter[] updateMRParameters =
            //             {
            //        new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "UPDATE_MNR" },
            //        new SqlParameter("@MR_NO", SqlDbType.VarChar, 100) { Value = updateMNRList[0].MR_NO },
            //        new SqlParameter("@DEPO_CODE", SqlDbType.VarChar, 50) { Value = updateMNRList[0].DEPO_CODE },
            //        new SqlParameter("@STATUS", SqlDbType.VarChar, 50) { Value = "Requested" },
            //        new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 255) { Value = updateMNRList[0].CREATED_BY },
            //    };

            //SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_MNR", updateMRParameters);


            for (int i = 0; i < updateMNRList.Count; i++)
            {
                var mrList = updateMNRList[i];

                SqlParameter[] updateMRData =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "UPDATE_MR_REQUEST" },
                  new SqlParameter("@ID", SqlDbType.VarChar, 10) { Value = mrList.ID },
                  new SqlParameter("@MR_NO", SqlDbType.VarChar, 100) { Value = mrList.MR_NO },
                  new SqlParameter("@CONTAINER_NO", SqlDbType.VarChar, 50) { Value = mrList.CONTAINER_NO },
                  new SqlParameter("@LOCATION", SqlDbType.VarChar, 50) { Value = mrList.LOCATION },
                  new SqlParameter("@COMPONENT", SqlDbType.VarChar, 50) { Value = mrList.COMPONENT },
                  new SqlParameter("@DAMAGE", SqlDbType.VarChar, 50) { Value = mrList.DAMAGE },
                  new SqlParameter("@REPAIR", SqlDbType.VarChar, 50) { Value = mrList.REPAIR },
                  new SqlParameter("@DESC", SqlDbType.VarChar, 255) { Value = mrList.DESC },
                  new SqlParameter("@LENGTH", SqlDbType.VarChar, 50) { Value = mrList.LENGTH },
                  new SqlParameter("@WIDTH", SqlDbType.VarChar, 50) { Value = mrList.WIDTH },
                  new SqlParameter("@HEIGHT", SqlDbType.VarChar, 50) { Value = mrList.HEIGHT },
                  new SqlParameter("@UNIT", SqlDbType.VarChar, 10) { Value = mrList.UNIT },
                  new SqlParameter("@RESPONSIBILITY", SqlDbType.VarChar, 50) { Value = mrList.RESPONSIBILITY },
                  new SqlParameter("@MAN_HOUR", SqlDbType.Decimal) { Value = mrList.MAN_HOUR, Precision =18, Scale=2 },
                  new SqlParameter("@LABOUR", SqlDbType.Decimal) { Value = mrList.LABOUR, Precision =18, Scale=2 },
                  new SqlParameter("@MATERIAL", SqlDbType.Decimal) { Value = mrList.MATERIAL, Precision =18, Scale=2 },
                  new SqlParameter("@TOTAL", SqlDbType.Decimal) { Value = mrList.TOTAL, Precision =18, Scale=2 },
                  new SqlParameter("@TAX", SqlDbType.Decimal) { Value = mrList.TAX, Precision =18, Scale=2 },
                  new SqlParameter("@FINAL_TOTAL", SqlDbType.Decimal) { Value = mrList.FINAL_TOTAL, Precision =18, Scale=2 },
                  new SqlParameter("@REMARKS", SqlDbType.VarChar, 255) { Value = mrList.REMARKS },
                  //new SqlParameter("@STATUS", SqlDbType.VarChar, 50) { Value = "Requested" },
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 255) { Value = mrList.CREATED_BY },
                  //new SqlParameter("@MNRFILE_PATH", SqlDbType.VarChar, 255) { Value = filePath },
                  new SqlParameter("@DAMAGE_LOCATION", SqlDbType.VarChar, 50) { Value = mrList.DAMAGE_LOCATION }
                };

                // Execute stored procedure
                SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_MNR", updateMRData);

                DataTable tblImages = new DataTable();
                tblImages.Columns.Add(new DataColumn("MR_ID", typeof(int)));
                tblImages.Columns.Add(new DataColumn("MR_NO", typeof(string)));
                tblImages.Columns.Add(new DataColumn("IMAGE_PATH", typeof(string)));


                if (attachmentPaths.ContainsKey(mrList.MR_NO))
                {
                    foreach (var path in attachmentPaths[mrList.MR_NO])
                    {
                        DataRow drImg = tblImages.NewRow();
                        drImg["MR_ID"] = mrList.ID;
                        drImg["MR_NO"] = mrList.MR_NO;
                        drImg["IMAGE_PATH"] = path;
                        tblImages.Rows.Add(drImg);
                    }
                }

                string[] imageColumns = new string[] { "MR_ID", "MR_NO", "IMAGE_PATH" };
                SqlHelper.ExecuteProcedureBulkInsert(connstring, tblImages, "TB_MR_IMAGES", imageColumns);
            }

        }

        public void InsertPrinMNRFiles(string connstring, List<MR_LIST> newMNR, List<string> attachmentPaths)
        {

            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("MR_NO", typeof(string)));
            tbl.Columns.Add(new DataColumn("CONTAINER_NO", typeof(string)));
            tbl.Columns.Add(new DataColumn("LOCATION", typeof(string)));
            tbl.Columns.Add(new DataColumn("COMPONENT", typeof(string)));
            tbl.Columns.Add(new DataColumn("DAMAGE", typeof(string)));
            tbl.Columns.Add(new DataColumn("REPAIR", typeof(string)));
            tbl.Columns.Add(new DataColumn("DESC", typeof(string)));
            tbl.Columns.Add(new DataColumn("LENGTH", typeof(string)));
            tbl.Columns.Add(new DataColumn("WIDTH", typeof(string)));
            tbl.Columns.Add(new DataColumn("HEIGHT", typeof(string)));
            tbl.Columns.Add(new DataColumn("UNIT", typeof(string)));
            tbl.Columns.Add(new DataColumn("RESPONSIBILITY", typeof(string)));
            tbl.Columns.Add(new DataColumn("MAN_HOUR", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("LABOUR", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("MATERIAL", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("TOTAL", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("TAX", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("FINAL_TOTAL", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("REMARKS", typeof(string)));
            tbl.Columns.Add(new DataColumn("STATUS", typeof(string)));
            tbl.Columns.Add(new DataColumn("CREATED_BY", typeof(string)));
            tbl.Columns.Add(new DataColumn("MNRFILE_PATH", typeof(string))); // Add the column for file path
            tbl.Columns.Add(new DataColumn("DAMAGE_LOCATION", typeof(string)));

            for (int index = 0; index < newMNR.Count; index++)
            {
                DataRow dr = tbl.NewRow();

                dr["MR_NO"] = newMNR[index].MR_NO;
                dr["CONTAINER_NO"] = newMNR[index].CONTAINER_NO;
                dr["LOCATION"] = newMNR[index].LOCATION;
                dr["COMPONENT"] = newMNR[index].COMPONENT;
                dr["DAMAGE"] = newMNR[index].DAMAGE;
                dr["REPAIR"] = newMNR[index].REPAIR;
                dr["DESC"] = newMNR[index].DESC;
                dr["LENGTH"] = newMNR[index].LENGTH;
                dr["WIDTH"] = newMNR[index].WIDTH;
                dr["HEIGHT"] = newMNR[index].HEIGHT;
                dr["UNIT"] = newMNR[index].UNIT;
                dr["RESPONSIBILITY"] = newMNR[index].RESPONSIBILITY;
                dr["MAN_HOUR"] = newMNR[index].MAN_HOUR;
                dr["LABOUR"] = newMNR[index].LABOUR;
                dr["MATERIAL"] = newMNR[index].MATERIAL;
                dr["TOTAL"] = newMNR[index].LABOUR + newMNR[index].MATERIAL;
                dr["TAX"] = newMNR[index].TAX;
                dr["FINAL_TOTAL"] = newMNR[index].FINAL_TOTAL;
                dr["REMARKS"] = newMNR[index].REMARKS;
                dr["STATUS"] = "Requested";
                dr["CREATED_BY"] = newMNR[index].CREATED_BY;

                // Check if there is an attachment for the current index
                if (index < attachmentPaths.Count)
                {
                    dr["MNRFILE_PATH"] = attachmentPaths[index]; // Associate the correct file path
                }
                else
                {
                    dr["MNRFILE_PATH"] = DBNull.Value; // No attachment available
                }

                dr["DAMAGE_LOCATION"] = newMNR[index].DAMAGE_LOCATION;

                tbl.Rows.Add(dr);
            }

            string[] columns = new string[23];
            columns[0] = "LOCATION";
            columns[1] = "COMPONENT";
            columns[2] = "DAMAGE";
            columns[3] = "REPAIR";
            columns[4] = "DESC";
            columns[5] = "LENGTH";
            columns[6] = "WIDTH";
            columns[7] = "HEIGHT";
            columns[8] = "UNIT";
            columns[9] = "RESPONSIBILITY";
            columns[10] = "MAN_HOUR";
            columns[11] = "LABOUR";
            columns[12] = "MATERIAL";
            columns[13] = "TOTAL";
            columns[14] = "CONTAINER_NO";
            columns[15] = "TAX";
            columns[16] = "FINAL_TOTAL";
            columns[17] = "STATUS";
            columns[18] = "MR_NO";
            columns[19] = "REMARKS";
            columns[20] = "CREATED_BY";
            columns[21] = "MNRFILE_PATH";
            columns[22] = "DAMAGE_LOCATION";

            SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl, "TB_MR_REQUEST", columns);
        }
    }
}
