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
using Org.BouncyCastle.Utilities;
using System.Numerics;
using System.Transactions;
using System.ComponentModel;
using System.Data.Common;
using Org.BouncyCastle.Asn1.Ocsp;

namespace PrimeMaritime_API.Repository
{
    public class BLRepo
    {
        public string InsertBL(string connstring, BL request)

        {

            string ExistingPOL;
            string ExistingPOD;
            string ExistingVESSEL;
            string ExistingVOYAGE;

            SqlParameter[] parameters4 =
                  {
                              new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "CHECK_EXISTINGDATA" },
                              new SqlParameter("@RETURNCONTAINERNO", SqlDbType.VarChar,100) { Direction = ParameterDirection.Output },
                              new SqlParameter("@RETURNPOL", SqlDbType.VarChar,100) { Direction = ParameterDirection.Output },
                              new SqlParameter("@RETURNPOD", SqlDbType.VarChar,100) { Direction = ParameterDirection.Output },
                              new SqlParameter("@RETURNVESSEL", SqlDbType.VarChar,100) { Direction = ParameterDirection.Output },
                              new SqlParameter("@RETURNVOYAGE", SqlDbType.VarChar,100) { Direction = ParameterDirection.Output },
                              new SqlParameter("@SRR_NO", SqlDbType.VarChar, 100) { Value = request.SRR_NO },

                             };


            SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_BL", parameters4);
            ExistingPOL = Convert.ToString(parameters4[2].Value);
            ExistingPOD = Convert.ToString(parameters4[3].Value);
            ExistingVESSEL = Convert.ToString(parameters4[4].Value);
            ExistingVOYAGE = Convert.ToString(parameters4[5].Value);

            // Split the PORT_OF_LOADING and PORT_OF_DISCHARGE strings and handle empty entries
            string[] POLParts = request.PORT_OF_LOADING.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            string[] PODParts = request.PORT_OF_DISCHARGE.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

            // Extract the desired values (the part inside the parentheses)
            string POL = POLParts.Length > 1 ? POLParts[1] : string.Empty;
            string POD = PODParts.Length > 1 ? PODParts[1] : string.Empty;

            if (ExistingPOL != POL || ExistingPOD != POD || ExistingVESSEL != request.VESSEL_NAME || ExistingVOYAGE != request.VOYAGE_NO)
            {
                return "failer";
            }



            string ContainerNoExists;
            foreach (var item in request.CONTAINER_LIST)
            {
                SqlParameter[] parameters3 =
                     {
                              new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "CHECK_CRO" },
                              new SqlParameter("@RETURNCONTAINERNO", SqlDbType.VarChar,100) { Direction = ParameterDirection.Output },
                              new SqlParameter("@CRO_NO", SqlDbType.VarChar, 100) { Value = request.CRO_NO },
                              new SqlParameter("@CONTAINER_NO", SqlDbType.VarChar, 50) { Value = item.CONTAINER_NO },

                             };


                 SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_BL", parameters3);
                ContainerNoExists = Convert.ToString(parameters3[1].Value);
                if (ContainerNoExists != item.CONTAINER_NO)
                {
                    return "failer";
                }
            }


              SqlParameter[] parameters =
              {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "CREATE_BL" },
                  new SqlParameter("@BL_NO", SqlDbType.VarChar, 50) { Value = request.BL_NO },
                  new SqlParameter("@SRR_ID", SqlDbType.Int) { Value = request.SRR_ID },
                  new SqlParameter("@SRR_NO", SqlDbType.VarChar, 50) { Value = request.SRR_NO },
                  new SqlParameter("@SHIPPER", SqlDbType.VarChar, 50) { Value = request.SHIPPER },
                  new SqlParameter("@SHIPPER_ADDRESS", SqlDbType.VarChar, 255) { Value = request.SHIPPER_ADDRESS },
                  new SqlParameter("@CONSIGNEE", SqlDbType.VarChar, 50) { Value = request.CONSIGNEE },
                  new SqlParameter("@CONSIGNEE_ADDRESS", SqlDbType.VarChar, 255) { Value = request.CONSIGNEE_ADDRESS },
                  new SqlParameter("@NOTIFY_PARTY", SqlDbType.VarChar, 50) { Value = request.NOTIFY_PARTY },
                  new SqlParameter("@NOTIFY_PARTY_ADDRESS", SqlDbType.VarChar,255) { Value = request.NOTIFY_PARTY_ADDRESS },
                  new SqlParameter("@PRE_CARRIAGE_BY", SqlDbType.VarChar,50) { Value = request.PRE_CARRIAGE_BY },
                  new SqlParameter("@PLACE_OF_RECEIPT", SqlDbType.VarChar,255) { Value = request.PLACE_OF_RECEIPT },
                  new SqlParameter("@VESSEL_NAME", SqlDbType.VarChar,255) { Value = request.VESSEL_NAME },
                  new SqlParameter("@VOYAGE_NO", SqlDbType.VarChar,50) { Value = request.VOYAGE_NO },
                  new SqlParameter("@PORT_OF_LOADING", SqlDbType.VarChar,255) { Value = request.PORT_OF_LOADING },
                  new SqlParameter("@PORT_OF_DISCHARGE", SqlDbType.VarChar,255) { Value = request.PORT_OF_DISCHARGE },
                  new SqlParameter("@PLACE_OF_DELIVERY", SqlDbType.VarChar,255) { Value = request.PLACE_OF_DELIVERY },
                  new SqlParameter("@BL_ISSUE_PLACE", SqlDbType.VarChar,100) { Value = request.BL_ISSUE_PLACE },
                  new SqlParameter("@BL_ISSUE_DATE", SqlDbType.DateTime) { Value = request.BL_ISSUE_DATE },
                  new SqlParameter("@NO_OF_ORIGINAL_BL", SqlDbType.Int) { Value = request.NO_OF_ORIGINAL_BL },
                  new SqlParameter("@BL_STATUS", SqlDbType.VarChar,20) { Value = request.BL_STATUS },
                  new SqlParameter("@FINAL_DESTINATION", SqlDbType.VarChar, 255) { Value = request.FINAL_DESTINATION },
                  new SqlParameter("@PREPAID_AT", SqlDbType.VarChar, 255) { Value = request.PREPAID_AT },
                  new SqlParameter("@PAYABLE_AT", SqlDbType.VarChar, 255) { Value = request.PAYABLE_AT },
                  new SqlParameter("@TOTAL_PREPAID", SqlDbType.Decimal) { Value = request.TOTAL_PREPAID },
                  new SqlParameter("@AGENT_CODE", SqlDbType.VarChar,20) { Value = request.AGENT_CODE },
                  new SqlParameter("@AGENT_NAME", SqlDbType.VarChar,255) { Value = request.AGENT_NAME },
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar,255) { Value = request.CREATED_BY },
                  new SqlParameter("@DESTINATION_AGENT_CODE", SqlDbType.VarChar,20) { Value = request.DESTINATION_AGENT_CODE },
                  new SqlParameter("@ISGROSSCOMBINED",SqlDbType.Bit) {Value = request.ISGROSSCOMBINED},
                  new SqlParameter("@POL1",SqlDbType.VarChar, 50) {Value = request.POL1},
                  new SqlParameter("@POD1",SqlDbType.VarChar, 50) {Value = request.POD1},
                  new SqlParameter("COMMODITY",SqlDbType.VarChar, 500) {Value = request.COMMODITY},
                  new SqlParameter("@CARGO_MOVEMENT",SqlDbType.VarChar, 10) {Value = request.CARGO_MOVEMENT},
                  new SqlParameter("@IS_SWITCHBL",SqlDbType.VarChar, 50) {Value = request.IS_SWITCHBL},  //SWITCHBL
                  new SqlParameter("@SWITCHBL_AGENT_CODE",SqlDbType.VarChar, 50) {Value = request.SWITCHBL_AGENT_CODE},  //SWITCHBL
              };

                var BLNO = SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_BL", parameters);


                foreach (var i in request.CONTAINER_LIST)
                {
                    SqlParameter[] parameters2 =
                         {
                              new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_CONTAINER" },
                              new SqlParameter("@BL_NO", SqlDbType.VarChar, 50) { Value = BLNO },
                              new SqlParameter("@CRO_NO", SqlDbType.VarChar, 100) { Value = request.CRO_NO },
                              new SqlParameter("@BOOKING_NO", SqlDbType.VarChar, 50) { Value = request.BOOKING_NO },
                              new SqlParameter("@CONTAINER_NO", SqlDbType.VarChar, 50) { Value = i.CONTAINER_NO },
                              new SqlParameter("@CONTAINER_TYPE", SqlDbType.VarChar, 50) { Value = i.CONTAINER_TYPE },
                              new SqlParameter("@CONTAINER_SIZE", SqlDbType.Int) { Value = 0 },
                              new SqlParameter("@SEAL_NO", SqlDbType.VarChar, 50) { Value = i.SEAL_NO },
                              new SqlParameter("@MARKS_NOS", SqlDbType.VarChar, 200) { Value = request.MARKS_NOS },
                              new SqlParameter("@DESC_OF_GOODS", SqlDbType.VarChar, 200) { Value = request.DESC_OF_GOODS },
                              new SqlParameter("@PKG_COUNT", SqlDbType.Int) { Value = i.PKG_COUNT },
                              new SqlParameter("@PKG_DESC", SqlDbType.VarChar, 200) { Value = i.PKG_DESC },
                              new SqlParameter("@GROSS_WEIGHT", SqlDbType.Decimal) { Value = i.GROSS_WEIGHT },
                              new SqlParameter("@NET_WEIGHT", SqlDbType.Decimal) { Value = i.NET_WEIGHT },
                              new SqlParameter("@MEASUREMENT", SqlDbType.VarChar,100) { Value = i.MEASUREMENT },
                              new SqlParameter("@AGENT_SEAL_NO", SqlDbType.VarChar,100) { Value = i.AGENT_SEAL_NO }
                             };

                    SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_BL", parameters2);

                }
            return "sucess";

        }

        public string MergeBL(string connstring, BL request)

        {

            string ExistingPOL;
            string ExistingPOD;
            string ExistingVESSEL;
            string ExistingVOYAGE;

            SqlParameter[] parameters4 =
                  {
                              new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "CHECK_EXISTINGDATA" },
                              new SqlParameter("@RETURNCONTAINERNO", SqlDbType.VarChar,100) { Direction = ParameterDirection.Output },
                              new SqlParameter("@RETURNPOL", SqlDbType.VarChar,100) { Direction = ParameterDirection.Output },
                              new SqlParameter("@RETURNPOD", SqlDbType.VarChar,100) { Direction = ParameterDirection.Output },
                              new SqlParameter("@RETURNVESSEL", SqlDbType.VarChar,100) { Direction = ParameterDirection.Output },
                              new SqlParameter("@RETURNVOYAGE", SqlDbType.VarChar,100) { Direction = ParameterDirection.Output },
                              new SqlParameter("@SRR_NO", SqlDbType.VarChar, 100) { Value = request.SRR_NO },

                             };


            SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_BL", parameters4);
            ExistingPOL = Convert.ToString(parameters4[2].Value);
            ExistingPOD = Convert.ToString(parameters4[3].Value);
            ExistingVESSEL = Convert.ToString(parameters4[4].Value);
            ExistingVOYAGE = Convert.ToString(parameters4[5].Value);

            // Split the PORT_OF_LOADING and PORT_OF_DISCHARGE strings and handle empty entries
            string[] POLParts = request.PORT_OF_LOADING.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            string[] PODParts = request.PORT_OF_DISCHARGE.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

            // Extract the desired values (the part inside the parentheses)
            string POL = POLParts.Length > 1 ? POLParts[1] : string.Empty;
            string POD = PODParts.Length > 1 ? PODParts[1] : string.Empty;

            if (ExistingPOL != POL || ExistingPOD != POD || ExistingVESSEL != request.VESSEL_NAME || ExistingVOYAGE != request.VOYAGE_NO)
            {
                return "failer";
            }



            //string ContainerNoExists;
            //foreach (var item in request.CONTAINER_LIST)
            //{
            //    SqlParameter[] parameters3 =
            //         {
            //                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "CHECK_CRO" },
            //                  new SqlParameter("@RETURNCONTAINERNO", SqlDbType.VarChar,100) { Direction = ParameterDirection.Output },
            //                  new SqlParameter("@CRO_NO", SqlDbType.VarChar, 100) { Value = request.CRO_NO },
            //                  new SqlParameter("@CONTAINER_NO", SqlDbType.VarChar, 50) { Value = item.CONTAINER_NO },

            //                 };


            //    SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_BL", parameters3);
            //    ContainerNoExists = Convert.ToString(parameters3[1].Value);
            //    if (ContainerNoExists != item.CONTAINER_NO)
            //    {
            //        return "failer";
            //    }
            //}


            SqlParameter[] parameters =
            {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "CREATE_BL" },
                  new SqlParameter("@BL_NO", SqlDbType.VarChar, 50) { Value = request.BL_NO },
                  new SqlParameter("@SRR_ID", SqlDbType.Int) { Value = request.SRR_ID },
                  new SqlParameter("@SRR_NO", SqlDbType.VarChar, 50) { Value = request.SRR_NO },
                  new SqlParameter("@SHIPPER", SqlDbType.VarChar, 50) { Value = request.SHIPPER },
                  new SqlParameter("@SHIPPER_ADDRESS", SqlDbType.VarChar, 255) { Value = request.SHIPPER_ADDRESS },
                  new SqlParameter("@CONSIGNEE", SqlDbType.VarChar, 50) { Value = request.CONSIGNEE },
                  new SqlParameter("@CONSIGNEE_ADDRESS", SqlDbType.VarChar, 255) { Value = request.CONSIGNEE_ADDRESS },
                  new SqlParameter("@NOTIFY_PARTY", SqlDbType.VarChar, 50) { Value = request.NOTIFY_PARTY },
                  new SqlParameter("@NOTIFY_PARTY_ADDRESS", SqlDbType.VarChar,255) { Value = request.NOTIFY_PARTY_ADDRESS },
                  new SqlParameter("@PRE_CARRIAGE_BY", SqlDbType.VarChar,50) { Value = request.PRE_CARRIAGE_BY },
                  new SqlParameter("@PLACE_OF_RECEIPT", SqlDbType.VarChar,255) { Value = request.PLACE_OF_RECEIPT },
                  new SqlParameter("@VESSEL_NAME", SqlDbType.VarChar,255) { Value = request.VESSEL_NAME },
                  new SqlParameter("@VOYAGE_NO", SqlDbType.VarChar,50) { Value = request.VOYAGE_NO },
                  new SqlParameter("@PORT_OF_LOADING", SqlDbType.VarChar,255) { Value = request.PORT_OF_LOADING },
                  new SqlParameter("@PORT_OF_DISCHARGE", SqlDbType.VarChar,255) { Value = request.PORT_OF_DISCHARGE },
                  new SqlParameter("@PLACE_OF_DELIVERY", SqlDbType.VarChar,255) { Value = request.PLACE_OF_DELIVERY },
                  new SqlParameter("@BL_ISSUE_PLACE", SqlDbType.VarChar,100) { Value = request.BL_ISSUE_PLACE },
                  new SqlParameter("@BL_ISSUE_DATE", SqlDbType.DateTime) { Value = request.BL_ISSUE_DATE },
                  new SqlParameter("@NO_OF_ORIGINAL_BL", SqlDbType.Int) { Value = request.NO_OF_ORIGINAL_BL },
                  new SqlParameter("@BL_STATUS", SqlDbType.VarChar,20) { Value = request.BL_STATUS },
                  new SqlParameter("@FINAL_DESTINATION", SqlDbType.VarChar, 255) { Value = request.FINAL_DESTINATION },
                  new SqlParameter("@PREPAID_AT", SqlDbType.VarChar, 255) { Value = request.PREPAID_AT },
                  new SqlParameter("@PAYABLE_AT", SqlDbType.VarChar, 255) { Value = request.PAYABLE_AT },
                  new SqlParameter("@TOTAL_PREPAID", SqlDbType.Decimal) { Value = request.TOTAL_PREPAID },
                  new SqlParameter("@AGENT_CODE", SqlDbType.VarChar,20) { Value = request.AGENT_CODE },
                  new SqlParameter("@AGENT_NAME", SqlDbType.VarChar,255) { Value = request.AGENT_NAME },
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar,255) { Value = request.CREATED_BY },
                  new SqlParameter("@DESTINATION_AGENT_CODE", SqlDbType.VarChar,20) { Value = request.DESTINATION_AGENT_CODE },
                  new SqlParameter("@ISGROSSCOMBINED",SqlDbType.Bit) {Value = request.ISGROSSCOMBINED},
                  new SqlParameter("@POL1",SqlDbType.VarChar, 50) {Value = request.POL1},
                  new SqlParameter("@POD1",SqlDbType.VarChar, 50) {Value = request.POD1},
                  new SqlParameter("COMMODITY",SqlDbType.VarChar, 500) {Value = request.COMMODITY},
                  new SqlParameter("@CARGO_MOVEMENT",SqlDbType.VarChar, 10) {Value = request.CARGO_MOVEMENT},
                  new SqlParameter("@IS_SWITCHBL",SqlDbType.VarChar, 50) {Value = request.IS_SWITCHBL},  //SWITCHBL
                  new SqlParameter("@SWITCHBL_AGENT_CODE",SqlDbType.VarChar, 50) {Value = request.SWITCHBL_AGENT_CODE},  //SWITCHBL
              };

            var BLNO = SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_BL", parameters);


            foreach (var i in request.CONTAINER_LIST)
            {
                SqlParameter[] parameters2 =
                     {
                              new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_CONTAINER_FOR_Merge" },
                              new SqlParameter("@BL_NO", SqlDbType.VarChar, 50) { Value = BLNO },
                              new SqlParameter("@CRO_NO", SqlDbType.VarChar, 100) { Value = request.CRO_NO },
                              new SqlParameter("@BOOKING_NO", SqlDbType.VarChar, 50) { Value = request.BOOKING_NO },
                              new SqlParameter("@CONTAINER_NO", SqlDbType.VarChar, 50) { Value = i.CONTAINER_NO },
                              new SqlParameter("@CONTAINER_TYPE", SqlDbType.VarChar, 50) { Value = i.CONTAINER_TYPE },
                               new SqlParameter("@CRO_NO_MERGE", SqlDbType.VarChar, 50) { Value = i.CRO_NO_MERGE },
                              new SqlParameter("@CONTAINER_SIZE", SqlDbType.Int) { Value = 0 },
                              new SqlParameter("@SEAL_NO", SqlDbType.VarChar, 50) { Value = i.SEAL_NO },
                              new SqlParameter("@MARKS_NOS", SqlDbType.VarChar, 200) { Value = request.MARKS_NOS },
                              new SqlParameter("@DESC_OF_GOODS", SqlDbType.VarChar, 200) { Value = request.DESC_OF_GOODS },
                              new SqlParameter("@PKG_COUNT", SqlDbType.Int) { Value = i.PKG_COUNT },
                              new SqlParameter("@PKG_DESC", SqlDbType.VarChar, 200) { Value = i.PKG_DESC },
                              new SqlParameter("@GROSS_WEIGHT", SqlDbType.Decimal) { Value = i.GROSS_WEIGHT },
                              new SqlParameter("@NET_WEIGHT", SqlDbType.Decimal) { Value = i.NET_WEIGHT },
                              new SqlParameter("@MEASUREMENT", SqlDbType.VarChar,100) { Value = i.MEASUREMENT },
                              new SqlParameter("@AGENT_SEAL_NO", SqlDbType.VarChar,100) { Value = i.AGENT_SEAL_NO }
                             };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_BL", parameters2);

            }
            return "sucess";

        }

        public string MergeBLBYBLNO(string connstring, BL request)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                // Get the values of BOOKING_NO and CRO_NO for the main BL_NO
                string selectQuery = @"
            SELECT TOP 1 BOOKING_NO, CRO_NO 
            FROM TB_CONTAINER 
            WHERE BL_NO = @MainBL";

                string bookingNo = null;
                string croNo = null;

                using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@MainBL", request.BL_NO);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bookingNo = reader["BOOKING_NO"].ToString();
                            croNo = reader["CRO_NO"].ToString();
                        }
                    }
                }

                // Exit early if main BL_NO not found
                if (bookingNo == null || croNo == null)
                    return "Main BL_NO not found";

                // Loop through each BL in the list
                foreach (var bl in request.BL_LIST)
                {
                    if (bl.BL_NO == request.BL_NO) continue; // skip main BL

                    string updateQuery = @"
                UPDATE TB_CONTAINER
                SET 
                    BOOKING_NO = @BookingNo,
                    CRO_NO = @CRONo,
                    BL_NO = @MainBL
                WHERE BL_NO = @MergeBL";

                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@BookingNo", bookingNo);
                        updateCmd.Parameters.AddWithValue("@CRONo", croNo);
                        updateCmd.Parameters.AddWithValue("@MainBL", request.BL_NO);
                        updateCmd.Parameters.AddWithValue("@MergeBL", bl.BL_NO);
                        updateCmd.ExecuteNonQuery();
                    }
                }

                conn.Close();
            }

            return "sucess";
        }



        public void InsertSurrender(string connstring, string BL_NO)
        {
            SqlParameter[] parameters =
            {
              new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_SURRENDER" },
              new SqlParameter("@BL_NO", SqlDbType.VarChar, 50) { Value = BL_NO },             
            };

            SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_BL", parameters);
        }

        public void InsertUploadedSurrender(string connstring, string BL_NO)
        {
            SqlParameter[] parameters =
            {
              new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_UPLOADED_SURRENDER" },
              new SqlParameter("@BL_NO", SqlDbType.VarChar, 50) { Value = BL_NO },
            };

            SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_BL", parameters);
        }

        public DataSet GetBLData(string connstring, string BL_NO, string BOOKING_NO, string AGENT_CODE)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_BLDETAILS" },
                new SqlParameter("@BL_NO", SqlDbType.VarChar, 100) { Value = BL_NO },
                new SqlParameter("@BOOKING_NO", SqlDbType.VarChar, 100) { Value = BOOKING_NO },
                new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 50) { Value = AGENT_CODE },
            };

            return SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_BL", parameters);
        }

        public void UpdateBL(string connstring, BL request)
        {
            try
            {
                SqlParameter[] parameters =
                    {
              new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_BL" },
              new SqlParameter("@BL_NO", SqlDbType.VarChar, 50) { Value = request.BL_NO },
              new SqlParameter("@SHIPPER", SqlDbType.VarChar, 50) { Value = request.SHIPPER },
              new SqlParameter("@SHIPPER_ADDRESS", SqlDbType.VarChar, 255) { Value = request.SHIPPER_ADDRESS },
              new SqlParameter("@CONSIGNEE", SqlDbType.VarChar, 50) { Value = request.CONSIGNEE },
              new SqlParameter("@CONSIGNEE_ADDRESS", SqlDbType.VarChar, 255) { Value = request.CONSIGNEE_ADDRESS },
              new SqlParameter("@NOTIFY_PARTY", SqlDbType.VarChar, 50) { Value = request.NOTIFY_PARTY },
              new SqlParameter("@NOTIFY_PARTY_ADDRESS", SqlDbType.VarChar,255) { Value = request.NOTIFY_PARTY_ADDRESS },
              new SqlParameter("@PRE_CARRIAGE_BY", SqlDbType.VarChar,50) { Value = request.PRE_CARRIAGE_BY },
              new SqlParameter("@PLACE_OF_RECEIPT", SqlDbType.VarChar,255) { Value = request.PLACE_OF_RECEIPT },
              new SqlParameter("@VESSEL_NAME", SqlDbType.VarChar,255) { Value = request.VESSEL_NAME },
              new SqlParameter("@VOYAGE_NO", SqlDbType.VarChar,50) { Value = request.VOYAGE_NO },
              new SqlParameter("@PORT_OF_LOADING", SqlDbType.VarChar,255) { Value = request.PORT_OF_LOADING },
              new SqlParameter("@PORT_OF_DISCHARGE", SqlDbType.VarChar,255) { Value = request.PORT_OF_DISCHARGE },
              new SqlParameter("@PLACE_OF_DELIVERY", SqlDbType.VarChar,255) { Value = request.PLACE_OF_DELIVERY },
              new SqlParameter("@BL_ISSUE_PLACE", SqlDbType.VarChar,100) { Value = request.BL_ISSUE_PLACE },
              new SqlParameter("@BL_ISSUE_DATE", SqlDbType.DateTime) { Value = request.BL_ISSUE_DATE },
              new SqlParameter("@NO_OF_ORIGINAL_BL", SqlDbType.Int) { Value = request.NO_OF_ORIGINAL_BL },
              new SqlParameter("@BL_STATUS", SqlDbType.VarChar,20) { Value = request.BL_STATUS },
              new SqlParameter("@BL_TYPE", SqlDbType.VarChar,20) { Value = request.BL_TYPE },
              new SqlParameter("@OG_TYPE", SqlDbType.VarChar,20) { Value = request.OG_TYPE },
              new SqlParameter("@OGView", SqlDbType.Int) { Value = request.OGView },
              new SqlParameter("@NNView", SqlDbType.Int) { Value = request.NNView },
              new SqlParameter("@FINAL_DESTINATION", SqlDbType.VarChar, 255) { Value = request.FINAL_DESTINATION },
              new SqlParameter("@PREPAID_AT", SqlDbType.VarChar, 255) { Value = request.PREPAID_AT },
              new SqlParameter("@PAYABLE_AT", SqlDbType.VarChar, 255) { Value = request.PAYABLE_AT },
              new SqlParameter("@TOTAL_PREPAID", SqlDbType.Decimal) { Value = request.TOTAL_PREPAID },
              new SqlParameter("@DESTINATION_AGENT_CODE", SqlDbType.VarChar,20) { Value = request.DESTINATION_AGENT_CODE },
              new SqlParameter("@ISGROSSCOMBINED",SqlDbType.Bit) {Value = request.ISGROSSCOMBINED},
              new SqlParameter("@CARGO_MOVEMENT",SqlDbType.VarChar, 10) {Value = request.CARGO_MOVEMENT},
              new SqlParameter("@POL1",SqlDbType.VarChar, 50) {Value = request.POL1},
              new SqlParameter("@POD1",SqlDbType.VarChar, 50) {Value = request.POD1},
              new SqlParameter("@COMMODITY",SqlDbType.VarChar, 500) {Value = request.COMMODITY},
              new SqlParameter("@AGENT_CODE", SqlDbType.VarChar,100) { Value = request.AGENT_CODE }
            };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_BL", parameters);


                foreach (var i in request.CONTAINER_LIST2)
                {
                    i.BL_NO = request.BL_NO;
                    i.MARKS_NOS = request.MARKS_NOS;
                    i.DESC_OF_GOODS = request.DESC_OF_GOODS;
                }

                string[] columns2 = new string[12];
                columns2[0] = "BL_NO";
                columns2[1] = "CONTAINER_NO";
                columns2[2] = "CONTAINER_TYPE";
                columns2[3] = "SEAL_NO";
                columns2[4] = "MARKS_NOS";
                columns2[5] = "DESC_OF_GOODS";
                columns2[6] = "PKG_COUNT";
                columns2[7] = "PKG_DESC";
                columns2[8] = "GROSS_WEIGHT";
                columns2[9] = "NET_WEIGHT";
                columns2[10] = "MEASUREMENT";
                columns2[11] = "AGENT_SEAL_NO";

                SqlHelper.UpdateContainerDataForBL<CONTAINERS>(request.CONTAINER_LIST2, "TB_CONTAINER", connstring, columns2);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BL> GetBLHistory(string connstring,string AGENT_CODE, string ORG_CODE, string PORT)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_BL_HISTORY" },
                   new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 20) { Value = AGENT_CODE },
                   new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 20) { Value = ORG_CODE },
                   new SqlParameter("@PORT", SqlDbType.VarChar, 100) { Value = PORT }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_BL", parameters);
                List<BL> blList = SqlHelper.CreateListFromTable<BL>(dataTable);
                return blList;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BL> GetBLALLHistory(string connstring, string AGENT_CODE, string ORG_CODE, string PORT)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_ALL_BL_HISTORY" },
                   new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 20) { Value = AGENT_CODE },
                   new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 20) { Value = ORG_CODE },
                   new SqlParameter("@PORT", SqlDbType.VarChar, 100) { Value = PORT }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_BL", parameters);
                List<BL> blList = SqlHelper.CreateListFromTable<BL>(dataTable);
                return blList;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BL> GetBLSurrenderedList(string connstring, string POD, string ORG_CODE)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_BL_SURRENDEREDLIST" },
                   new SqlParameter("@POD", SqlDbType.VarChar, 20) { Value = POD },
                   new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 20) { Value = ORG_CODE }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_BL", parameters);
                List<BL> blList = SqlHelper.CreateListFromTable<BL>(dataTable);
                return blList;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<BL> GetBLListPM(string connstring)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_BLLIST_PM" },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_BL", parameters);
                List<BL> blList = SqlHelper.CreateListFromTable<BL>(dataTable);
                return blList;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BL> GetBLFORMERGE(string connstring, string PORT_OF_LOADING,string PORT_OF_DISCHARGE,string SHIPPER,string CONSIGNEE,string VESSEL_NAME,string VOYAGE_NO,string NOTIFY_PARTY)
        {
            try
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_BL_FORMERGE" },
                   new SqlParameter("@PORT_OF_LOADING", SqlDbType.VarChar, 255) { Value = PORT_OF_LOADING },
                   new SqlParameter("@PORT_OF_DISCHARGE", SqlDbType.VarChar, 255) { Value = PORT_OF_DISCHARGE },
                   new SqlParameter("@SHIPPER", SqlDbType.VarChar, 50) { Value = SHIPPER },
                   new SqlParameter("@CONSIGNEE", SqlDbType.VarChar, 50) { Value = CONSIGNEE },
                   new SqlParameter("@VESSEL_NAME", SqlDbType.VarChar, 255) { Value = VESSEL_NAME },
                   new SqlParameter("@VOYAGE_NO", SqlDbType.VarChar, 50) { Value = VOYAGE_NO },
                   new SqlParameter("@NOTIFY_PARTY", SqlDbType.VarChar, 50) { Value = NOTIFY_PARTY }
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_BL", parameters);
                List<BL> blList = SqlHelper.CreateListFromTable<BL>(dataTable);
                return blList;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetSRRDetails(string connstring, string BL_NO, string BOOKING_NO, string AGENT_CODE)
        {
            SqlParameter[] parameters =
            {
              new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "GET_SRRDETAILS" },
              new SqlParameter("@BL_NO", SqlDbType.VarChar, 100) { Value = BL_NO },
              new SqlParameter("@BOOKING_NO", SqlDbType.VarChar, 100) { Value = BOOKING_NO },
              new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 50) { Value = AGENT_CODE },

            };

            return SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_BL", parameters);
        }


        public static T GetSingleDataFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateItemFromRow<T>(dataTable.Rows[0]);
        }

        public static List<T> GetListFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateListFromTable<T>(dataTable);
        }

        public List<CONTAINERS> GetContainerList(string connstring, string AGENT_CODE, string DEPO_CODE, string BOOKING_NO, string CRO_NO, string BL_NO, string DO_NO, bool fromDO)
        {

            if (fromDO == true)
            {
                SqlParameter[] parameters =
                {
                   new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "GET_CONTAINERLISTFORDO" },
                   new SqlParameter("@AGENT_CODE", SqlDbType.VarChar,50) { Value = AGENT_CODE },
                   new SqlParameter("@BL_NO", SqlDbType.VarChar,50) { Value = BL_NO }
                 };
                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_BL", parameters);
                List<CONTAINERS> containerList = SqlHelper.CreateListFromTable<CONTAINERS>(dataTable);
                return containerList;
            }
            else
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "GET_CONTAINERLIST" },
                    new SqlParameter("@AGENT_CODE", SqlDbType.VarChar,50) { Value = AGENT_CODE },
                    new SqlParameter("@DEPO_CODE", SqlDbType.VarChar,50) { Value = DEPO_CODE },
                    new SqlParameter("@BOOKING_NO", SqlDbType.VarChar,100) { Value = BOOKING_NO },
                    new SqlParameter("@CRO_NO", SqlDbType.VarChar,100) { Value = CRO_NO },
                    new SqlParameter("@BL_NO", SqlDbType.VarChar,50) { Value = BL_NO },
                    new SqlParameter("@DO_NO", SqlDbType.VarChar,100) { Value = DO_NO }
                };
                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_BL", parameters);
                List<CONTAINERS> containerList = SqlHelper.CreateListFromTable<CONTAINERS>(dataTable);
                return containerList;

            }

        }

        public DataSet CargoManifestData(string connstring,string AGENT_CODE, string VESSEL_NAME, string VOYAGE_NO)
        {
            try
            {
                SqlParameter[] parameters =
{
                   new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "GET_CARGO_MANIFEST_LIST" },
                   new SqlParameter("@AGENT_CODE", SqlDbType.VarChar,50) { Value = AGENT_CODE },
                   new SqlParameter("@VESSEL_NAME", SqlDbType.VarChar,50) { Value = VESSEL_NAME },
                   new SqlParameter("@VOYAGE_NO", SqlDbType.VarChar,50) { Value = VOYAGE_NO }
                 };
        
                return SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_BL", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Organisation GetOrgDetails(string connstring, string ORG_CODE, string ORG_LOC_CODE)
        {
            try
            {
                SqlParameter[] parameters =
            {
                new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_ORG_DETAILS" },
                new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 100) { Value = ORG_CODE },
                new SqlParameter("@ORG_LOC_CODE", SqlDbType.VarChar, 100) { Value = ORG_LOC_CODE },
            };

                return SqlHelper.ExtecuteProcedureReturnData<Organisation>(connstring, "SP_CRUD_BL", r => r.TranslateOrganisation(), parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //added new
        public DataSet getcargoBL(string connstring, string AGENT_CODE, string VESSEL_NAME, string VOYAGE_NO, string BL_NO)
        {
            try
            {
                SqlParameter[] parameters =
{
                   new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "GET_CARGO_MANIFEST_BL" },
                   new SqlParameter("@AGENT_CODE", SqlDbType.VarChar,50) { Value = AGENT_CODE },
                   new SqlParameter("@VESSEL_NAME", SqlDbType.VarChar,50) { Value = VESSEL_NAME },
                   new SqlParameter("@VOYAGE_NO", SqlDbType.VarChar,50) { Value = VOYAGE_NO },
                   new SqlParameter("@BL_NO", SqlDbType.VarChar,500) { Value = BL_NO }
                 };

                return SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_BL", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }


        #region "Invoice"
        //public List<INVOICE_MASTER>GetBL(string dbConn)
        //{
        //    try
        //    {
        //        SqlParameter[] parameters =
        //        {
        //          new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_BL_LIST" }
        //        };

        //        DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_INOVICE2", parameters);
        //        List<INVOICE_MASTER> master = SqlHelper.CreateListFromTable<INVOICE_MASTER>(dataTable);

        //        return master;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        #endregion

        //SWITCHBL TESTING
        public string InsertSWITCHBL(string connstring, BL request)
        {
            string ParentBLNoExists;
                    SqlParameter[] parameters3 =
                                 {
                                      new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "CHECK_SWITCHBLNO" },
                                      new SqlParameter("@RETURNPARENTBLNO", SqlDbType.VarChar,100) { Direction = ParameterDirection.Output },
                                      new SqlParameter("@PARENTBL_NO", SqlDbType.VarChar, 50) { Value = request.BL_ID },
                                      new SqlParameter("@BL_NO", SqlDbType.VarChar, 50) { Value = request.BL_NO },

                                  };


                    SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_BL", parameters3);
                    ParentBLNoExists = Convert.ToString(parameters3[1].Value);
                    if (ParentBLNoExists == Convert.ToString(request.BL_NO))
                    {
                        return "failer";
                    }

            SqlParameter[] parameters =
            {
              new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "CREATE_SWITCHBL" },
              new SqlParameter("@BL_NO", SqlDbType.VarChar, 100) { Value = request.BL_NO },
              new SqlParameter("@PARENTBL_NO", SqlDbType.VarChar, 100) { Value = request.BL_ID },
              new SqlParameter("@SRR_ID", SqlDbType.Int) { Value = request.SRR_ID },
              new SqlParameter("@SRR_NO", SqlDbType.VarChar, 50) { Value = request.SRR_NO },
              new SqlParameter("@SHIPPER", SqlDbType.VarChar, 50) { Value = request.SHIPPER },
              new SqlParameter("@SHIPPER_ADDRESS", SqlDbType.VarChar, 255) { Value = request.SHIPPER_ADDRESS },
              new SqlParameter("@CONSIGNEE", SqlDbType.VarChar, 50) { Value = request.CONSIGNEE },
              new SqlParameter("@CONSIGNEE_ADDRESS", SqlDbType.VarChar, 255) { Value = request.CONSIGNEE_ADDRESS },
              new SqlParameter("@NOTIFY_PARTY", SqlDbType.VarChar, 50) { Value = request.NOTIFY_PARTY },
              new SqlParameter("@NOTIFY_PARTY_ADDRESS", SqlDbType.VarChar,255) { Value = request.NOTIFY_PARTY_ADDRESS },
              new SqlParameter("@PRE_CARRIAGE_BY", SqlDbType.VarChar,50) { Value = request.PRE_CARRIAGE_BY },
              new SqlParameter("@PLACE_OF_RECEIPT", SqlDbType.VarChar,255) { Value = request.PLACE_OF_RECEIPT },
              new SqlParameter("@VESSEL_NAME", SqlDbType.VarChar,255) { Value = request.VESSEL_NAME },
              new SqlParameter("@VOYAGE_NO", SqlDbType.VarChar,50) { Value = request.VOYAGE_NO },
              new SqlParameter("@PORT_OF_LOADING", SqlDbType.VarChar,255) { Value = request.PORT_OF_LOADING },
              new SqlParameter("@PORT_OF_DISCHARGE", SqlDbType.VarChar,255) { Value = request.PORT_OF_DISCHARGE },
              new SqlParameter("@PLACE_OF_DELIVERY", SqlDbType.VarChar,255) { Value = request.PLACE_OF_DELIVERY },
              new SqlParameter("@BL_ISSUE_PLACE", SqlDbType.VarChar,100) { Value = request.BL_ISSUE_PLACE },
              new SqlParameter("@BL_ISSUE_DATE", SqlDbType.DateTime) { Value = request.BL_ISSUE_DATE },
              new SqlParameter("@NO_OF_ORIGINAL_BL", SqlDbType.Int) { Value = request.NO_OF_ORIGINAL_BL },
              new SqlParameter("@BL_STATUS", SqlDbType.VarChar,20) { Value = request.BL_STATUS },
              new SqlParameter("@FINAL_DESTINATION", SqlDbType.VarChar, 255) { Value = request.FINAL_DESTINATION },
              new SqlParameter("@PREPAID_AT", SqlDbType.VarChar, 255) { Value = request.PREPAID_AT },
              new SqlParameter("@PAYABLE_AT", SqlDbType.VarChar, 255) { Value = request.PAYABLE_AT },
              new SqlParameter("@TOTAL_PREPAID", SqlDbType.Decimal) { Value = request.TOTAL_PREPAID },
              new SqlParameter("@AGENT_CODE", SqlDbType.VarChar,20) { Value = request.AGENT_CODE },
              new SqlParameter("@AGENT_NAME", SqlDbType.VarChar,255) { Value = request.AGENT_NAME },
              new SqlParameter("@CREATED_BY", SqlDbType.VarChar,255) { Value = request.CREATED_BY },
              new SqlParameter("@DESTINATION_AGENT_CODE", SqlDbType.VarChar,20) { Value = request.DESTINATION_AGENT_CODE },
              new SqlParameter("@ISGROSSCOMBINED",SqlDbType.Bit) {Value = request.ISGROSSCOMBINED},
              new SqlParameter("@CARGO_MOVEMENT",SqlDbType.VarChar, 10) {Value = request.CARGO_MOVEMENT},
              new SqlParameter("@POL1",SqlDbType.VarChar, 50) {Value = request.POL1},
              new SqlParameter("@POD1",SqlDbType.VarChar, 50) {Value = request.POD1},
              new SqlParameter("COMMODITY",SqlDbType.VarChar, 500) {Value = request.COMMODITY},
              new SqlParameter("@SWITCHBL_STATUS",SqlDbType.Bit) {Value = request.SWITCHBL_STATUS},
              new SqlParameter("@IS_POL",SqlDbType.Bit) {Value = request.IS_POL},


             };

            var BLNO = SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_BL", parameters);

            return "sucess";
            //string[] columns2 = new string[17];
            //columns2[0] = "BL_NO";
            //columns2[1] = "BOOKING_NO";
            //columns2[2] = "CRO_NO";
            //columns2[3] = "CONTAINER_NO";
            //columns2[4] = "CONTAINER_TYPE";
            //columns2[5] = "CONTAINER_SIZE";
            //columns2[6] = "SEAL_NO";
            //columns2[7] = "MARKS_NOS";
            //columns2[8] = "DESC_OF_GOODS";
            //columns2[9] = "PKG_COUNT";
            //columns2[10] = "PKG_DESC";
            //columns2[11] = "GROSS_WEIGHT";
            //columns2[12] = "NET_WEIGHT";
            //columns2[13] = "MEASUREMENT";
            //columns2[14] = "AGENT_CODE";
            //columns2[15] = "AGENT_NAME";
            //columns2[16] = "CREATED_BY";

            //DataTable tbl = new DataTable();

            //tbl.columns2.Add(new DataColumn("BL_NO", typeof(string)));
            //tbl.columns2.Add(new DataColumn("BOOKING_NO", typeof(string)));
            //tbl.columns2.Add(new DataColumn("CRO_NO", typeof(string)));
            //tbl.columns2.Add(new DataColumn("CONTAINER_NO", typeof(string)));
            //tbl.columns2.Add(new DataColumn("CONTAINER_TYPE", typeof(string)));
            //tbl.columns2.Add(new DataColumn("CONTAINER_SIZE", typeof(int)));
            //tbl.columns2.Add(new DataColumn("SEAL_NO", typeof(string)));
            //tbl.columns2.Add(new DataColumn("MARKS_NOS", typeof(string)));
            //tbl.columns2.Add(new DataColumn("DESC_OF_GOODS", typeof(string)));
            //tbl.columns2.Add(new DataColumn("PKG_COUNT", typeof(int)));
            //tbl.columns2.Add(new DataColumn("PKG_DESC", typeof(string)));
            //tbl.columns2.Add(new DataColumn("GROSS_WEIGHT", typeof(decimal)));
            //tbl.columns2.Add(new DataColumn("NET_WEIGHT", typeof(decimal)));
            //tbl.columns2.Add(new DataColumn("MEASUREMENT", typeof(string)));
            //tbl.columns2.Add(new DataColumn("AGENT_CODE", typeof(string)));
            //tbl.columns2.Add(new DataColumn("AGENT_NAME", typeof(string)));
            //tbl.columns2.Add(new DataColumn("CREATED_BY", typeof(string)));


            //foreach (var i in request.CONTAINER_LIST)
            //{
            //    DataRow dr = tbl.NewRow();

            //    dr["BL_NO"] = BLNO;
            //    dr["BOOKING_NO"] = request.BOOKING_NO;
            //    dr["CRO_NO"] = request.CRO_NO;
            //    dr["CONTAINER_NO"] = i.CONTAINER_NO;
            //    dr["CONTAINER_TYPE"] = i.CONTAINER_TYPE;
            //    dr["CONTAINER_SIZE"] = 0;
            //    dr["SEAL_NO"] = i.SEAL_NO;
            //    dr["MARKS_NOS"] = i.MARKS_NOS;
            //    dr["DESC_OF_GOODS"] = i.DESC_OF_GOODS;
            //    dr["PKG_COUNT"] = i.PKG_COUNT;
            //    dr["PKG_DESC"] = i.PKG_DESC;
            //    dr["GROSS_WEIGHT"] = i.GROSS_WEIGHT;
            //    dr["NET_WEIGHT"] = i.NET_WEIGHT;
            //    dr["MEASUREMENT"] = i.MEASUREMENT;
            //    dr["AGENT_CODE"] = i.AGENT_CODE;
            //    dr["AGENT_NAME"] = i.AGENT_NAME;
            //    dr["CREATED_BY"] = i.CREATED_BY;

            //    tbl.Rows.Add(dr);
            //}

            //SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl, "TB_CONTAINER", columns2);
        }

        //TESTING FOR GETSWITCHBL
        public DataSet GetSwitchBLData(string connstring, string BL_NO, string BOOKING_NO, string AGENT_CODE)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@OPERATION", SqlDbType.VarChar, 100) { Value = "GET_SWITCHBL" },
                new SqlParameter("@BL_NO", SqlDbType.VarChar, 100) { Value = BL_NO },
                new SqlParameter("@BOOKING_NO", SqlDbType.VarChar, 100) { Value = BOOKING_NO },
                new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 50) { Value = AGENT_CODE },
                //new SqlParameter("@SWITCHBL_STATUS", SqlDbType.Int) { Value = SWITCHBL_STATUS },
            };

            return SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_BL", parameters);
        }

        public void UpdateSwitchBL(string connstring, BL request)
        {
            try
            {
                SqlParameter[] parameters =
                    {
              new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "UPDATE_SwitchBL" },
              new SqlParameter("@BL_NO", SqlDbType.VarChar, 50) { Value = request.BL_NO },
              new SqlParameter("@SHIPPER", SqlDbType.VarChar, 50) { Value = request.SHIPPER },
              new SqlParameter("@SHIPPER_ADDRESS", SqlDbType.VarChar, 255) { Value = request.SHIPPER_ADDRESS },
              new SqlParameter("@CONSIGNEE", SqlDbType.VarChar, 50) { Value = request.CONSIGNEE },
              new SqlParameter("@CONSIGNEE_ADDRESS", SqlDbType.VarChar, 255) { Value = request.CONSIGNEE_ADDRESS },
              new SqlParameter("@NOTIFY_PARTY", SqlDbType.VarChar, 50) { Value = request.NOTIFY_PARTY },
              new SqlParameter("@NOTIFY_PARTY_ADDRESS", SqlDbType.VarChar,255) { Value = request.NOTIFY_PARTY_ADDRESS },
              new SqlParameter("@PRE_CARRIAGE_BY", SqlDbType.VarChar,50) { Value = request.PRE_CARRIAGE_BY },
              new SqlParameter("@PLACE_OF_RECEIPT", SqlDbType.VarChar,255) { Value = request.PLACE_OF_RECEIPT },
              new SqlParameter("@VESSEL_NAME", SqlDbType.VarChar,255) { Value = request.VESSEL_NAME },
              new SqlParameter("@VOYAGE_NO", SqlDbType.VarChar,50) { Value = request.VOYAGE_NO },
              new SqlParameter("@PORT_OF_LOADING", SqlDbType.VarChar,255) { Value = request.PORT_OF_LOADING },
              new SqlParameter("@PORT_OF_DISCHARGE", SqlDbType.VarChar,255) { Value = request.PORT_OF_DISCHARGE },
              new SqlParameter("@PLACE_OF_DELIVERY", SqlDbType.VarChar,255) { Value = request.PLACE_OF_DELIVERY },
              new SqlParameter("@BL_ISSUE_PLACE", SqlDbType.VarChar,100) { Value = request.BL_ISSUE_PLACE },
              new SqlParameter("@BL_ISSUE_DATE", SqlDbType.DateTime) { Value = request.BL_ISSUE_DATE },
              new SqlParameter("@NO_OF_ORIGINAL_BL", SqlDbType.Int) { Value = request.NO_OF_ORIGINAL_BL },
              new SqlParameter("@BL_STATUS", SqlDbType.VarChar,20) { Value = request.BL_STATUS },
              new SqlParameter("@BL_TYPE", SqlDbType.VarChar,20) { Value = request.BL_TYPE },
              new SqlParameter("@OG_TYPE", SqlDbType.VarChar,20) { Value = request.OG_TYPE },
              new SqlParameter("@OGView", SqlDbType.Int) { Value = request.OGView },
              new SqlParameter("@NNView", SqlDbType.Int) { Value = request.NNView },
              new SqlParameter("@FINAL_DESTINATION", SqlDbType.VarChar, 255) { Value = request.FINAL_DESTINATION },
              new SqlParameter("@PREPAID_AT", SqlDbType.VarChar, 255) { Value = request.PREPAID_AT },
              new SqlParameter("@PAYABLE_AT", SqlDbType.VarChar, 255) { Value = request.PAYABLE_AT },
              new SqlParameter("@TOTAL_PREPAID", SqlDbType.Decimal) { Value = request.TOTAL_PREPAID },
              new SqlParameter("@DESTINATION_AGENT_CODE", SqlDbType.VarChar,20) { Value = request.DESTINATION_AGENT_CODE },
              new SqlParameter("@ISGROSSCOMBINED",SqlDbType.Bit) {Value = request.ISGROSSCOMBINED},
              new SqlParameter("@CARGO_MOVEMENT",SqlDbType.VarChar, 10) {Value = request.CARGO_MOVEMENT},
              new SqlParameter("@POL1",SqlDbType.VarChar, 50) {Value = request.POL1},
              new SqlParameter("@POD1",SqlDbType.VarChar, 50) {Value = request.POD1},
              new SqlParameter("COMMODITY",SqlDbType.VarChar, 500) {Value = request.COMMODITY},
              new SqlParameter("@AGENT_CODE", SqlDbType.VarChar,100) { Value = request.AGENT_CODE }
            };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_BL", parameters);

                foreach (var i in request.CONTAINER_LIST2)
                {
                    i.BL_NO = request.BL_NO;
                    i.MARKS_NOS = request.MARKS_NOS;
                    i.DESC_OF_GOODS = request.DESC_OF_GOODS;
                }

                string[] columns2 = new string[12];
                columns2[0] = "BL_NO";
                columns2[1] = "CONTAINER_NO";
                columns2[2] = "CONTAINER_TYPE";
                columns2[3] = "SEAL_NO";
                columns2[4] = "MARKS_NOS";
                columns2[5] = "DESC_OF_GOODS";
                columns2[6] = "PKG_COUNT";
                columns2[7] = "PKG_DESC";
                columns2[8] = "GROSS_WEIGHT";
                columns2[9] = "NET_WEIGHT";
                columns2[10] = "MEASUREMENT";
                columns2[11] = "AGENT_SEAL_NO";

                SqlHelper.UpdateContainerDataForBL<CONTAINERS>(request.CONTAINER_LIST2, "TB_CONTAINER", connstring, columns2);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UnlockBL(string connstring, string BL_NO, int ID, string AGENT_CODE)
        {
            SqlParameter[] parameters =
            {
              new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "GET_UNLOCK_BLPM" },
              new SqlParameter("@BL_NO", SqlDbType.VarChar, 100) { Value = BL_NO },
               new SqlParameter("@ID", SqlDbType.Int) { Value = ID },
                new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 100) { Value = AGENT_CODE },
            };

            SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_BL", parameters);
        }


        public ONLYBL CheckBLSwitched(string connstring, string BL_NO)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,100) { Value = "CHECK_BL_SWITCHED" },
                  new SqlParameter("@BL_NO", SqlDbType.VarChar, 100) { Value = BL_NO },
                };

                return SqlHelper.ExtecuteProcedureReturnData<ONLYBL>(connstring, "SP_CRUD_BL", r => r.TranslateAsBLDETAILS(), parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string UpdateItemNo(string connstring,BL request)
        {
            SqlParameter[] parameters =
            {
            new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "UPDATE_ITEM_NO" },
            new SqlParameter("@BL_NO", SqlDbType.VarChar, 50) { Value = request.BL_NO },
            new SqlParameter("@LINE_ITEM_NO", SqlDbType.VarChar, 50) { Value = request.LINE_ITEM_NO }
        };

            var result = SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_BL", parameters);
            return result;
        }

    }
}
