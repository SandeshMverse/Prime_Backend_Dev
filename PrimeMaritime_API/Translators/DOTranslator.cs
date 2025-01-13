using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Translators
{
    public static class DOTranslator
    {
        public static DO TranslateDO(this SqlDataReader reader,bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new DO();
            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("BL_ID"))
                item.BL_ID = SqlHelper.GetNullableInt32(reader, "BL_ID");

            if (reader.IsColumnExists("BL_NO"))
                item.BL_NO = SqlHelper.GetNullableString(reader, "BL_NO");

            if (reader.IsColumnExists("DO_NO"))
                item.DO_NO = SqlHelper.GetNullableString(reader, "DO_NO");

            if (reader.IsColumnExists("DO_DATE"))
                item.DO_DATE = SqlHelper.GetDateTime(reader, "DO_DATE");

            if (reader.IsColumnExists("ARRIVAL_DATE"))
                item.ARRIVAL_DATE = SqlHelper.GetDateTime(reader, "ARRIVAL_DATE");

            if (reader.IsColumnExists("DO_VALIDITY"))
                item.DO_VALIDITY = SqlHelper.GetDateTime(reader, "DO_VALIDITY");

            if (reader.IsColumnExists("IGM_NO"))
                item.IGM_NO = SqlHelper.GetNullableString(reader, "IGM_NO");

            if (reader.IsColumnExists("IGM_ITEM_NO"))
                item.IGM_ITEM_NO = SqlHelper.GetNullableString(reader, "IGM_ITEM_NO");

            if (reader.IsColumnExists("IGM_DATE"))
                item.IGM_DATE = SqlHelper.GetDateTime(reader, "IGM_DATE");

            if (reader.IsColumnExists("CLEARING_PARTY"))
                item.CLEARING_PARTY = SqlHelper.GetNullableString(reader, "CLEARING_PARTY");

            if (reader.IsColumnExists("ACCEPTANCE_LOCATION"))
                item.ACCEPTANCE_LOCATION = SqlHelper.GetNullableString(reader, "ACCEPTANCE_LOCATION");

            if (reader.IsColumnExists("LETTER_VALIDITY"))
                item.LETTER_VALIDITY = SqlHelper.GetDateTime(reader, "LETTER_VALIDITY");

            if (reader.IsColumnExists("SHIPPING_TERMS"))
                item.SHIPPING_TERMS = SqlHelper.GetNullableString(reader, "SHIPPING_TERMS");

            if (reader.IsColumnExists("AGENT_CODE"))
                item.AGENT_CODE = SqlHelper.GetNullableString(reader, "AGENT_CODE");

            if (reader.IsColumnExists("AGENT_NAME"))
                item.AGENT_NAME = SqlHelper.GetNullableString(reader, "AGENT_NAME");

            if (reader.IsColumnExists("CREATED_BY"))
                item.CREATED_BY = SqlHelper.GetNullableString(reader, "CREATED_BY");

            return item;
        }

        public static DODETAILS TranslateDODETAILS(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new DODETAILS();
            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("BL_ID"))
                item.BL_ID = SqlHelper.GetNullableInt32(reader, "BL_ID");

            if (reader.IsColumnExists("BL_NO"))
                item.BL_NO = SqlHelper.GetNullableString(reader, "BL_NO");

            if (reader.IsColumnExists("DO_NO"))
                item.DO_NO = SqlHelper.GetNullableString(reader, "DO_NO");

            if (reader.IsColumnExists("DO_DATE"))
                item.DO_DATE = SqlHelper.GetDateTime(reader, "DO_DATE");

            if (reader.IsColumnExists("ARRIVAL_DATE"))
                item.ARRIVAL_DATE = SqlHelper.GetDateTime(reader, "ARRIVAL_DATE");

            if (reader.IsColumnExists("DO_VALIDITY"))
                item.DO_VALIDITY = SqlHelper.GetDateTime(reader, "DO_VALIDITY");

            if (reader.IsColumnExists("IGM_NO"))
                item.IGM_NO = SqlHelper.GetNullableString(reader, "IGM_NO");

            if (reader.IsColumnExists("IGM_ITEM_NO"))
                item.IGM_ITEM_NO = SqlHelper.GetNullableString(reader, "IGM_ITEM_NO");

            if (reader.IsColumnExists("IGM_DATE"))
                item.IGM_DATE = SqlHelper.GetDateTime(reader, "IGM_DATE");

            if (reader.IsColumnExists("CLEARING_PARTY"))
                item.CLEARING_PARTY = SqlHelper.GetNullableString(reader, "CLEARING_PARTY");

            if (reader.IsColumnExists("ACCEPTANCE_LOCATION"))
                item.ACCEPTANCE_LOCATION = SqlHelper.GetNullableString(reader, "ACCEPTANCE_LOCATION");

            if (reader.IsColumnExists("LETTER_VALIDITY"))
                item.LETTER_VALIDITY = SqlHelper.GetDateTime(reader, "LETTER_VALIDITY");

            if (reader.IsColumnExists("SHIPPING_TERMS"))
                item.SHIPPING_TERMS = SqlHelper.GetNullableString(reader, "SHIPPING_TERMS");

            if (reader.IsColumnExists("AGENT_CODE"))
                item.AGENT_CODE = SqlHelper.GetNullableString(reader, "AGENT_CODE");

            if (reader.IsColumnExists("AGENT_NAME"))
                item.AGENT_NAME = SqlHelper.GetNullableString(reader, "AGENT_NAME");

            if (reader.IsColumnExists("CREATED_BY"))
                item.CREATED_BY = SqlHelper.GetNullableString(reader, "CREATED_BY");

            if (reader.IsColumnExists("CREATED_DATE"))
                item.CREATED_DATE = SqlHelper.GetDateTime(reader, "CREATED_DATE");

            if (reader.IsColumnExists("VESSEL_NAME"))
                item.VESSEL_NAME = SqlHelper.GetNullableString(reader, "VESSEL_NAME");

            if (reader.IsColumnExists("VOYAGE_NO"))
                item.VOYAGE_NO = SqlHelper.GetNullableString(reader, "VOYAGE_NO");

            if (reader.IsColumnExists("DELIVERY_PARTY"))
                item.DELIVERY_PARTY = SqlHelper.GetNullableString(reader, "DELIVERY_PARTY");

            if (reader.IsColumnExists("LINE_NO"))
                item.LINE_NO = SqlHelper.GetNullableString(reader, "LINE_NO");

            if (reader.IsColumnExists("CFS_DETAILS"))
                item.CFS_DETAILS = SqlHelper.GetNullableString(reader, "CFS_DETAILS");

            if (reader.IsColumnExists("DO_STATUS"))
                item.DO_STATUS = SqlHelper.GetNullableString(reader, "DO_STATUS");

            return item;
        }
    }
}
