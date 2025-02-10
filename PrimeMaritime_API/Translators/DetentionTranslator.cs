using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace PrimeMaritime_API.Translators
{
    public static class DetentionTranslator
    {

        //public static DETENTION_CHARGES TranslateAsDetentionMaster(this SqlDataReader reader, bool isList = false)
        //{
        //    if (!isList)
        //    {
        //        if (!reader.HasRows)
        //            return null;
        //        reader.Read();
        //    }

        //    var item = new DETENTION_CHARGES();

        //    if (reader.IsColumnExists("ID"))
        //        item.ID = SqlHelper.GetNullableInt32(reader, "ID");

        //    if (reader.IsColumnExists("PORT_CODE"))
        //        item.PORT_CODE = SqlHelper.GetNullableString(reader, "PORT_CODE");

        //    if (reader.IsColumnExists("CONTAINER_TYPE"))
        //        item.CONTAINER_TYPE = SqlHelper.GetNullableString(reader, "CONTAINER_TYPE");

        //    if (reader.IsColumnExists("CURRENCY"))
        //        item.CURRENCY = SqlHelper.GetNullableString(reader, "CURRENCY");

        //    if (reader.IsColumnExists("FROM_DAYS"))
        //        item.FROM_DAYS = SqlHelper.GetNullableInt32(reader, "FROM_DAYS");

        //    if (reader.IsColumnExists("TO_DAYS"))
        //        item.TO_DAYS = SqlHelper.GetNullableInt32(reader, "TO_DAYS");

        //    if (reader.IsColumnExists("20FT_RATE"))
        //        item.RATE20 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "20FT_RATE"));

        //    if (reader.IsColumnExists("40FT_RATE"))
        //        item.RATE40 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "40FT_RATE"));

        //    if (reader.IsColumnExists("HC_RATE"))
        //        item.HC_RATE = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "HC_RATE"));

        //    if (reader.IsColumnExists("CREATED_BY"))
        //        item.CREATED_BY = SqlHelper.GetNullableString(reader, "CREATED_BY");

        //    if (reader.IsColumnExists("CREATED_DATE"))
        //        item.CREATED_DATE = SqlHelper.GetDateTime(reader, "CREATED_DATE");

        //    return item;
        //}

    }
}
