using PrimeMaritime_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Response;
using PrimeMaritime_API.Translators;


namespace PrimeMaritime_API.Repository
{
    public class CredoRepo
    {
             public DataSet GetCredoDetails(string connstring, string AGENT_CODE, string VESSEL_NAME, string VOYAGE_NO, string PORT_OF_LOADING, string PORT_OF_DISCHARGE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                        new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "GET_CREDO_DETAILS" },
                        new SqlParameter("@AGENT_CODE", SqlDbType.VarChar,50) { Value = AGENT_CODE },
                        new SqlParameter("@VESSEL_NAME", SqlDbType.VarChar,50) { Value = VESSEL_NAME },
                        new SqlParameter("@VOYAGE_NO", SqlDbType.VarChar,50) { Value = VOYAGE_NO },
                        new SqlParameter("@PORT_OF_LOADING", SqlDbType.VarChar,50) { Value = PORT_OF_LOADING },
                        new SqlParameter("@PORT_OF_DISCHARGE", SqlDbType.VarChar,50) { Value = PORT_OF_DISCHARGE },

                };

                return SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_BL", parameters);

            }
            catch (Exception)
            {
                throw;
            }
        }
       
        public static T GetSingleDataFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateItemFromRow<T>(dataTable.Rows[0]);
        }
        public static List<T> GetListFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateListFromTable<T>(dataTable);
        }
    }

 
}
