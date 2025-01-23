using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Translators
{
    public static class MASTERTranslator
    {
        public static PARTY_MASTER TranslateAsCustomer(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new PARTY_MASTER();

            if (reader.IsColumnExists("CUST_ID"))
                item.CUST_ID = SqlHelper.GetNullableInt32(reader, "CUST_ID");

            if (reader.IsColumnExists("CUST_NAME"))
                item.CUST_NAME = SqlHelper.GetNullableString(reader, "CUST_NAME");

            if (reader.IsColumnExists("CUST_ADDRESS"))
                item.CUST_ADDRESS = SqlHelper.GetNullableString(reader, "CUST_ADDRESS");

            if (reader.IsColumnExists("CUST_EMAIL"))
                item.CUST_EMAIL = SqlHelper.GetNullableString(reader, "CUST_EMAIL");

            if (reader.IsColumnExists("CUST_CONTACT"))
                item.CUST_CONTACT = SqlHelper.GetNullableString(reader, "CUST_CONTACT");

            if (reader.IsColumnExists("CUST_TYPE"))
                item.CUST_TYPE = SqlHelper.GetNullableString(reader, "CUST_TYPE");

            if (reader.IsColumnExists("GSTIN"))
                item.GSTIN = SqlHelper.GetNullableString(reader, "GSTIN");

            if (reader.IsColumnExists("STATUS"))
                item.STATUS = SqlHelper.GetBoolean(reader, "STATUS");

            if (reader.IsColumnExists("REMARKS"))
                item.REMARKS = SqlHelper.GetNullableString(reader, "REMARKS");

            if (reader.IsColumnExists("AGENT_CODE"))
                item.AGENT_CODE = SqlHelper.GetNullableString(reader, "AGENT_CODE");

            if (reader.IsColumnExists("CREATED_BY"))
                item.CREATED_BY = SqlHelper.GetNullableString(reader, "CREATED_BY");

            return item;
        }
        //public static CONTAINER_MASTER TranslateAsContainer(this SqlDataReader reader, bool isList = false)
        //{
        //    if (!isList)
        //    {
        //        if (!reader.HasRows)
        //            return null;
        //        reader.Read();
        //    }

        //    var item = new CONTAINER_MASTER();

        //    if (reader.IsColumnExists("ID"))
        //        item.ID = SqlHelper.GetNullableInt32(reader, "ID");

        //    if (reader.IsColumnExists("CONTAINER_NO"))
        //        item.CONTAINER_NO = SqlHelper.GetNullableString(reader, "CONTAINER_NO");

        //    if (reader.IsColumnExists("CONTAINER_TYPE"))
        //        item.CONTAINER_TYPE = SqlHelper.GetNullableString(reader, "CONTAINER_TYPE");

        //    if (reader.IsColumnExists("ONHIRE_DATE"))
        //        item.ONHIRE_DATE = SqlHelper.GetDateTime(reader, "ONHIRE_DATE");

        //    if (reader.IsColumnExists("ONHIRE_LOCATION"))
        //        item.ONHIRE_LOCATION = SqlHelper.GetNullableString(reader, "ONHIRE_LOCATION");

        //    if (reader.IsColumnExists("LEASED_FROM"))
        //        item.LEASED_FROM = SqlHelper.GetNullableString(reader, "LEASED_FROM");

        //    if (reader.IsColumnExists("STATUS"))
        //        item.STATUS = SqlHelper.GetBoolean(reader, "STATUS");

        //    if (reader.IsColumnExists("vendor_agreement_id"))
        //        item.VENDOR_AGREEMENT_ID = SqlHelper.GetNullableInt32(reader, "vendor_agreement_id");

        //    if (reader.IsColumnExists("OFFHIRE_DATE"))
        //        item.OFFHIRE_DATE = SqlHelper.GetDateTime(reader, "OFFHIRE_DATE");

        //    if (reader.IsColumnExists("YEAR_OF_MANUFACTURE"))
        //        item.YEAR_OF_MANUFACTURE = SqlHelper.GetNullableString(reader, "YEAR_OF_MANUFACTURE");

        //    if (reader.IsColumnExists("TARE_WEIGHT"))
        //        item.TARE_WEIGHT = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "TARE_WEIGHT"));

        //    if (reader.IsColumnExists("PAYLOAD_CAPACITY"))
        //        item.PAYLOAD_CAPACITY = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "PAYLOAD_CAPACITY"));

        //    if (reader.IsColumnExists("GROSS_WEIGHT"))
        //        item.GROSS_WEIGHT = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "GROSS_WEIGHT"));

        //    if (reader.IsColumnExists("CSC_NO"))
        //        item.CSC_NO = SqlHelper.GetNullableString(reader, "CSC_NO");

        //    if (reader.IsColumnExists("ACEP_NO"))
        //        item.ACEP_NO = SqlHelper.GetNullableString(reader, "ACEP_NO");

        //    return item;
        //}
        public static CONTAINER_MASTER TranslateAsContainer(this SqlDataReader reader, bool isList = false)
        {
            if (!isList && !reader.HasRows)
                return null;

            if (!isList) reader.Read();

            var item = new CONTAINER_MASTER();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("CONTAINER_NO"))
                item.CONTAINER_NO = SqlHelper.GetNullableString(reader, "CONTAINER_NO");

            if (reader.IsColumnExists("CONTAINER_TYPE"))
                item.CONTAINER_TYPE = SqlHelper.GetNullableString(reader, "CONTAINER_TYPE");

            if (reader.IsColumnExists("ONHIRE_DATE"))
                item.ONHIRE_DATE = SqlHelper.GetNullableDateTime(reader, "ONHIRE_DATE");

            if (reader.IsColumnExists("ONHIRE_LOCATION"))
                item.ONHIRE_LOCATION = SqlHelper.GetNullableString(reader, "ONHIRE_LOCATION");

            if (reader.IsColumnExists("LEASED_FROM"))
                item.LEASED_FROM = SqlHelper.GetNullableString(reader, "LEASED_FROM");

            if (reader.IsColumnExists("STATUS"))
                item.STATUS = SqlHelper.GetBoolean(reader, "STATUS");

            if (reader.IsColumnExists("VENDOR_AGREEMENT_ID"))
                item.VENDOR_AGREEMENT_ID = SqlHelper.GetNullableInt32(reader, "VENDOR_AGREEMENT_ID");

            if (reader.IsColumnExists("OFFHIRE_DATE"))
                item.OFFHIRE_DATE = SqlHelper.GetNullableDateTime(reader, "OFFHIRE_DATE");

            if (reader.IsColumnExists("YEAR_OF_MANUFACTURE"))
                item.YEAR_OF_MANUFACTURE = SqlHelper.GetNullableString(reader, "YEAR_OF_MANUFACTURE");

            if (reader.IsColumnExists("TARE_WEIGHT"))
                item.TARE_WEIGHT = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "TARE_WEIGHT"));

            if (reader.IsColumnExists("PAYLOAD_CAPACITY"))
                item.PAYLOAD_CAPACITY = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "PAYLOAD_CAPACITY"));

            if (reader.IsColumnExists("GROSS_WEIGHT"))
                item.GROSS_WEIGHT = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "GROSS_WEIGHT"));

            if (reader.IsColumnExists("CSC_NO"))
                item.CSC_NO = SqlHelper.GetNullableString(reader, "CSC_NO");

            if (reader.IsColumnExists("ACEP_NO"))
                item.ACEP_NO = SqlHelper.GetNullableString(reader, "ACEP_NO");

            if (reader.IsColumnExists("AGREEMENT_NO"))
                item.AGREEMENT_NO = SqlHelper.GetNullableString(reader, "AGREEMENT_NO");

            if (reader.IsColumnExists("TURN_IN_DATE"))
                item.TURN_IN_DATE = SqlHelper.GetNullableString(reader, "TURN_IN_DATE");

            if (reader.IsColumnExists("LOCATION"))
                item.LOCATION = SqlHelper.GetNullableString(reader, "LOCATION");

            if (reader.IsColumnExists("CONTAINER_SIZE"))
                item.CONTAINER_SIZE = SqlHelper.GetNullableString(reader, "CONTAINER_SIZE");

            return item;
        }

        public static COUNTRY_MASTER TranslateAsCountry(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new COUNTRY_MASTER();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("CODE"))
                item.CODE = SqlHelper.GetNullableInt32(reader, "CODE");

            if (reader.IsColumnExists("NAME"))
                item.NAME = SqlHelper.GetNullableString(reader, "NAME");

            if (reader.IsColumnExists("SHORT_NAME"))
                item.SHORT_NAME = SqlHelper.GetNullableString(reader, "SHORT_NAME");

            if (reader.IsColumnExists("STATUS"))
                item.STATUS = SqlHelper.GetBoolean(reader, "STATUS");

            return item;
        }
        public static STATE_MASTER TranslateAsSate(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new STATE_MASTER();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("CODE"))
                item.CODE = SqlHelper.GetNullableInt32(reader, "CODE");

            if (reader.IsColumnExists("NAME"))
                item.NAME = SqlHelper.GetNullableString(reader, "NAME");

            if (reader.IsColumnExists("SHORT_NAME"))
                item.SHORT_NAME = SqlHelper.GetNullableString(reader, "SHORT_NAME");

            if (reader.IsColumnExists("COUNTRY_ID"))
                item.COUNTRY_ID = SqlHelper.GetNullableInt32(reader, "COUNTRY_ID");

            if (reader.IsColumnExists("IS_UT"))
                item.IS_UT = SqlHelper.GetBoolean(reader, "IS_UT");

            if (reader.IsColumnExists("STATUS"))
                item.STATUS = SqlHelper.GetBoolean(reader, "STATUS");

            return item;
        }
        public static MASTER TranslateAsMaster(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new MASTER();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("KEY_NAME"))
                item.KEY_NAME = SqlHelper.GetNullableString(reader, "KEY_NAME");


            if (reader.IsColumnExists("CODE"))
                item.CODE = SqlHelper.GetNullableString(reader, "CODE");

            if (reader.IsColumnExists("CODE_DESC"))
                item.CODE_DESC = SqlHelper.GetNullableString(reader, "CODE_DESC");

            if (reader.IsColumnExists("STATUS"))
                item.STATUS = SqlHelper.GetBoolean(reader, "STATUS");

            if (reader.IsColumnExists("PARENT_CODE"))
                item.PARENT_CODE = SqlHelper.GetNullableString(reader, "PARENT_CODE");

            if (reader.IsColumnExists("CREATED_BY"))
                item.CREATED_BY = SqlHelper.GetNullableString(reader, "CREATED_BY");

            if (reader.IsColumnExists("CREATED_DATE"))
                item.CREATED_DATE = SqlHelper.GetDateTime(reader, "CREATED_DATE");

            if (reader.IsColumnExists("UPDATED_BY"))
                item.UPDATED_BY = SqlHelper.GetNullableString(reader, "UPDATED_BY");

            if (reader.IsColumnExists("UPDATED_DATE"))
                item.UPDATED_DATE = SqlHelper.GetDateTime(reader, "UPDATED_DATE");

            return item;
        }
        public static VESSEL_MASTER TranslateAsVessel(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new VESSEL_MASTER();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("VESSEL_NAME"))
                item.VESSEL_NAME = SqlHelper.GetNullableString(reader, "VESSEL_NAME");

            if (reader.IsColumnExists("IMO_NO"))
                item.IMO_NO = SqlHelper.GetNullableString(reader, "IMO_NO");

            if (reader.IsColumnExists("COUNTRY_CODE"))
                item.COUNTRY_CODE = SqlHelper.GetNullableString(reader, "COUNTRY_CODE");

            if (reader.IsColumnExists("STATUS"))
                item.STATUS = SqlHelper.GetBoolean(reader, "STATUS");

            if (reader.IsColumnExists("VESSEL_CODE"))
                item.VESSEL_CODE = SqlHelper.GetNullableString(reader, "VESSEL_CODE");

            if (reader.IsColumnExists("CREATED_BY"))
                item.CREATED_BY = SqlHelper.GetNullableString(reader, "CREATED_BY");

            if (reader.IsColumnExists("CALL_SIGN"))
                item.CALL_SIGN = SqlHelper.GetNullableString(reader, "CALL_SIGN");

            if (reader.IsColumnExists("YEAR_OF_BUILT"))
                item.YEAR_OF_BUILT = SqlHelper.GetNullableInt32(reader, "YEAR_OF_BUILT");

            if (reader.IsColumnExists("CAPACITY"))
                item.CAPACITY = SqlHelper.GetNullableInt32(reader, "CAPACITY");

            if (reader.IsColumnExists("UOM"))
                item.UOM = SqlHelper.GetNullableString(reader, "UOM");

            //if (reader.IsColumnExists("CREATED_ON"))
            //    item.CREATED_ON = SqlHelper.GetDateTime(reader, "CREATED_ON");

            //if (reader.IsColumnExists("UPDATED_BY"))
            //    item.UPDATED_BY = SqlHelper.GetNullableString(reader, "UPDATED_BY");

            //if (reader.IsColumnExists("UPDATED_DATE"))
            //    item.UPDATED_DATE = SqlHelper.GetDateTime(reader, "UPDATED_DATE");

            return item;
        }
        public static SERVICE_MASTER TranslateAsService(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new SERVICE_MASTER();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("LINER_CODE"))
                item.LINER_CODE = SqlHelper.GetNullableString(reader, "LINER_CODE");

            if (reader.IsColumnExists("PORT_CODE"))
                item.PORT_CODE = SqlHelper.GetNullableString(reader, "PORT_CODE");

            if (reader.IsColumnExists("SERVICE_NAME"))
                item.SERVICE_NAME = SqlHelper.GetNullableString(reader, "SERVICE_NAME");



            if (reader.IsColumnExists("STATUS"))
                item.STATUS = SqlHelper.GetBoolean(reader, "STATUS");



            if (reader.IsColumnExists("CREATED_BY"))
                item.CREATED_BY = SqlHelper.GetNullableString(reader, "CREATED_BY");

            //if (reader.IsColumnExists("CREATED_ON"))
            //    item.CREATED_ON = SqlHelper.GetDateTime(reader, "CREATED_ON");

            //if (reader.IsColumnExists("UPDATED_BY"))
            //    item.UPDATED_BY = SqlHelper.GetNullableString(reader, "UPDATED_BY");

            //if (reader.IsColumnExists("UPDATED_DATE"))
            //    item.UPDATED_DATE = SqlHelper.GetDateTime(reader, "UPDATED_DATE");

            return item;
        }
        public static CONTAINER_TYPE TranslateAsContainerType(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new CONTAINER_TYPE();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("CONT_TYPE_CODE"))
                item.CONT_TYPE_CODE = SqlHelper.GetNullableString(reader, "CONT_TYPE_CODE");

            if (reader.IsColumnExists("CONT_TYPE"))
                item.CONT_TYPE = SqlHelper.GetNullableString(reader, "CONT_TYPE");

            if (reader.IsColumnExists("CONT_SIZE"))
                item.CONT_SIZE = SqlHelper.GetNullableInt32(reader, "CONT_SIZE");

            if (reader.IsColumnExists("ISO_CODE"))
                item.ISO_CODE = SqlHelper.GetNullableString(reader, "ISO_CODE");


            if (reader.IsColumnExists("TEUS"))
                item.TEUS = SqlHelper.GetNullableInt32(reader, "TEUS");

            if (reader.IsColumnExists("OUT_DIM"))
                item.OUT_DIM = SqlHelper.GetNullableString(reader, "OUT_DIM");

            if (reader.IsColumnExists("STATUS"))
                item.STATUS = SqlHelper.GetBoolean(reader, "STATUS");


            if (reader.IsColumnExists("CREATED_BY"))
                item.CREATED_BY = SqlHelper.GetNullableString(reader, "CREATED_BY");


            return item;
        }
        public static LINER TranslateAsLiner(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new LINER();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("CODE"))
                item.CODE = SqlHelper.GetNullableString(reader, "CODE");

            if (reader.IsColumnExists("NAME"))
                item.NAME = SqlHelper.GetNullableString(reader, "NAME");

            if (reader.IsColumnExists("DESCRIPTION"))
                item.DESCRIPTION = SqlHelper.GetNullableString(reader, "DESCRIPTION");


            if (reader.IsColumnExists("STATUS"))
                item.STATUS = SqlHelper.GetBoolean(reader, "STATUS");


            if (reader.IsColumnExists("CREATED_BY"))
                item.CREATED_BY = SqlHelper.GetNullableString(reader, "CREATED_BY");


            return item;
        }
        public static SERVICE TranslateAsLinerService(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new SERVICE();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("LINER_CODE"))
                item.LINER_NAME = SqlHelper.GetNullableString(reader, "LINER_CODE");

            if (reader.IsColumnExists("SERVICE_NAME"))
                item.SERVICE_NAME = SqlHelper.GetNullableString(reader, "SERVICE_NAME");

            if (reader.IsColumnExists("PORT_CODE"))
                item.ORIGIN_PORT_CODE = SqlHelper.GetNullableString(reader, "PORT_CODE");


            if (reader.IsColumnExists("STATUS"))
                item.STATUS = SqlHelper.GetBoolean(reader, "STATUS");


            if (reader.IsColumnExists("CREATED_BY"))
                item.CREATED_BY = SqlHelper.GetNullableString(reader, "CREATED_BY");


            return item;
        }
        public static SCHEDULE TranslateAsSchedule(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new SCHEDULE();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("VESSEL_NAME"))
                item.VESSEL_NAME = SqlHelper.GetNullableString(reader, "VESSEL_NAME");

            if (reader.IsColumnExists("SERVICE_NAME"))
                item.SERVICE_NAME = SqlHelper.GetNullableString(reader, "SERVICE_NAME");

            if (reader.IsColumnExists("PORT_CODE"))
                item.PORT_CODE = SqlHelper.GetNullableString(reader, "PORT_CODE");

            if (reader.IsColumnExists("VIA_NO"))
                item.VIA_NO = SqlHelper.GetNullableString(reader, "VIA_NO");

            if (reader.IsColumnExists("ETA"))
                item.ETA = SqlHelper.GetDateTime(reader, "ETA");

            if (reader.IsColumnExists("ETD"))
                item.ETD = SqlHelper.GetDateTime(reader, "ETD");

            if (reader.IsColumnExists("STATUS"))
                item.STATUS = SqlHelper.GetBoolean(reader, "STATUS");


            if (reader.IsColumnExists("CREATED_BY"))
                item.CREATED_BY = SqlHelper.GetNullableString(reader, "CREATED_BY");


            if (reader.IsColumnExists("VOYAGE_NO"))
                item.VOYAGE_NO = SqlHelper.GetNullableString(reader, "VOYAGE_NO");


            if (reader.IsColumnExists("TERMINAL_NO"))
                item.TERMINAL_NO = SqlHelper.GetNullableString(reader, "TERMINAL_NO");

            if (reader.IsColumnExists("VESSEL_SCHEDULE"))
                item.VESSEL_SCHEDULE = SqlHelper.GetNullableString(reader, "VESSEL_SCHEDULE");

            if (reader.IsColumnExists("TERMINAL_CODE"))
                item.TERMINAL_CODE = SqlHelper.GetNullableString(reader, "TERMINAL_CODE");

            return item;
        }
        public static VOYAGE TranslateAsVoyage(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new VOYAGE();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("VESSEL_NAME"))
                item.VESSEL_NAME = SqlHelper.GetNullableString(reader, "VESSEL_NAME");

            if (reader.IsColumnExists("VOYAGE_NO"))
                item.VOYAGE_NO = SqlHelper.GetNullableString(reader, "VOYAGE_NO");

            if (reader.IsColumnExists("ATA"))
                item.ATA = SqlHelper.GetDateTime(reader, "ATA");

            if (reader.IsColumnExists("ATD"))
                item.ATD = SqlHelper.GetDateTime(reader, "ATD");

            if (reader.IsColumnExists("TERMINAL_CODE"))
                item.TERMINAL_CODE = SqlHelper.GetNullableString(reader, "TERMINAL_CODE");

            if (reader.IsColumnExists("SERVICE_NAME"))
                item.SERVICE_NAME = SqlHelper.GetNullableString(reader, "SERVICE_NAME");

            if (reader.IsColumnExists("VIA_NO"))
                item.VIA_NO = SqlHelper.GetNullableString(reader, "VIA_NO");

            if (reader.IsColumnExists("PORT_CODE"))
                item.PORT_CODE = SqlHelper.GetNullableString(reader, "PORT_CODE");

            if (reader.IsColumnExists("ETA"))
                item.ETA = SqlHelper.GetDateTime(reader, "ETA");

            if (reader.IsColumnExists("ETD"))
                item.ETD = SqlHelper.GetDateTime(reader, "ETD");

            if (reader.IsColumnExists("STATUS"))
                item.STATUS = SqlHelper.GetBoolean(reader, "STATUS");

            return item;
        }
        public static FREIGHT_MASTER TranslateAsFreightMaster(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new FREIGHT_MASTER();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("POL"))
                item.POL = SqlHelper.GetNullableString(reader, "POL");

            if (reader.IsColumnExists("POD"))
                item.POD = SqlHelper.GetNullableString(reader, "POD");

            if (reader.IsColumnExists("Charge"))
                item.Charge = SqlHelper.GetNullableString(reader, "Charge");

            if (reader.IsColumnExists("Currency"))
                item.Currency = SqlHelper.GetNullableString(reader, "Currency");

            if (reader.IsColumnExists("LadenStatus"))
                item.LadenStatus = SqlHelper.GetNullableString(reader, "LadenStatus");

            if (reader.IsColumnExists("ServiceMode"))
                item.ServiceMode = SqlHelper.GetNullableString(reader, "ServiceMode");

            if (reader.IsColumnExists("DRY20"))
                item.DRY20 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "DRY20"));

            return item;
        }
        public static CHARGE_MASTER TranslateAsChargeMaster(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new CHARGE_MASTER();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("POL"))
                item.POL = SqlHelper.GetNullableString(reader, "POL");

            if (reader.IsColumnExists("CHARGE_CODE"))
                item.CHARGE_CODE = SqlHelper.GetNullableString(reader, "CHARGE_CODE");

            if (reader.IsColumnExists("IMPCOST20"))
                item.IMPCOST20 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "IMPCOST20"));

            if (reader.IsColumnExists("IMPCOST40"))
                item.IMPCOST40 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "IMPCOST40"));

            if (reader.IsColumnExists("IMPINCOME20"))
                item.IMPINCOME20 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "IMPINCOME20"));

            if (reader.IsColumnExists("IMPINCOME40"))
                item.IMPINCOME40 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "IMPINCOME40"));

            if (reader.IsColumnExists("EXPCOST20"))
                item.EXPCOST20 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "EXPCOST20"));

            if (reader.IsColumnExists("EXPCOST40"))
                item.EXPCOST40 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "EXPCOST40"));

            if (reader.IsColumnExists("EXPINCOME20"))
                item.EXPINCOME20 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "EXPINCOME20"));

            if (reader.IsColumnExists("EXPINCOME40"))
                item.EXPINCOME40 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "EXPINCOME40"));

            if (reader.IsColumnExists("CURRENCY"))
                item.CURRENCY = SqlHelper.GetNullableString(reader, "CURRENCY");

            if (reader.IsColumnExists("FROM_VAL"))
                item.FROM_VAL = SqlHelper.GetNullableInt32(reader, "FROM_VAL");

            if (reader.IsColumnExists("TO_VAL"))
                item.TO_VAL = SqlHelper.GetNullableInt32(reader, "TO_VAL");

            if (reader.IsColumnExists("STATUS"))
                item.STATUS = SqlHelper.GetBoolean(reader, "STATUS");

            return item;
        }
        public static STEV_MASTER TranslateAsStevMaster(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new STEV_MASTER();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("IE_TYPE"))
                item.IE_TYPE = SqlHelper.GetNullableString(reader, "IE_TYPE");

            if (reader.IsColumnExists("POL"))
                item.POL = SqlHelper.GetNullableString(reader, "POL");

            if (reader.IsColumnExists("CHARGE_CODE"))
                item.CHARGE_CODE = SqlHelper.GetNullableString(reader, "CHARGE_CODE");

            if (reader.IsColumnExists("TERMINAL"))
                item.TERMINAL = SqlHelper.GetNullableString(reader, "TERMINAL");

            if (reader.IsColumnExists("CURRENCY"))
                item.CURRENCY = SqlHelper.GetNullableString(reader, "CURRENCY");

            if (reader.IsColumnExists("LADEN_STATUS"))
                item.LADEN_STATUS = SqlHelper.GetNullableString(reader, "LADEN_STATUS");

            if (reader.IsColumnExists("SERVICE_MODE"))
                item.SERVICE_MODE = SqlHelper.GetNullableString(reader, "SERVICE_MODE");

            if (reader.IsColumnExists("DRY20"))
                item.DRY20 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "DRY20"));

            if (reader.IsColumnExists("DRY40"))
                item.DRY40 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "DRY40"));

            if (reader.IsColumnExists("DRY40HC"))
                item.DRY40HC = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "DRY40HC"));

            if (reader.IsColumnExists("DRY45"))
                item.DRY45 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "DRY45"));

            if (reader.IsColumnExists("RF20"))
                item.RF20 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "RF20"));

            if (reader.IsColumnExists("RF40"))
                item.RF40 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "RF40"));

            if (reader.IsColumnExists("RF40HC"))
                item.RF40HC = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "RF40HC"));

            if (reader.IsColumnExists("RF45"))
                item.RF45 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "RF45"));

            if (reader.IsColumnExists("HAZ20"))
                item.HAZ20 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "HAZ20"));

            if (reader.IsColumnExists("HAZ40"))
                item.HAZ40 = Convert.ToDecimal(SqlHelper.GetNullableInt32(reader, "HAZ40"));

            if (reader.IsColumnExists("HAZ40HC"))
                item.HAZ40HC = Convert.ToDecimal(SqlHelper.GetBoolean(reader, "HAZ40HC"));

            if (reader.IsColumnExists("HAZ45"))
                item.HAZ45 = Convert.ToDecimal(SqlHelper.GetBoolean(reader, "HAZ45"));

            if (reader.IsColumnExists("SEQ20"))
                item.SEQ20 = Convert.ToDecimal(SqlHelper.GetBoolean(reader, "SEQ20"));

            if (reader.IsColumnExists("SEQ40"))
                item.SEQ40 = Convert.ToDecimal(SqlHelper.GetBoolean(reader, "SEQ40"));

            return item;
        }
        public static DETENTION_MASTER TranslateAsDetentionMaster(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new DETENTION_MASTER();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("PORT_CODE"))
                item.PORT_CODE = SqlHelper.GetNullableString(reader, "PORT_CODE");

            if (reader.IsColumnExists("CONTAINER_TYPE"))
                item.CONTAINER_TYPE = SqlHelper.GetNullableString(reader, "CONTAINER_TYPE");

            if (reader.IsColumnExists("CURRENCY"))
                item.CURRENCY = SqlHelper.GetNullableString(reader, "CURRENCY");

            if (reader.IsColumnExists("FROM_DAYS"))
                item.FROM_DAYS = SqlHelper.GetNullableInt32(reader, "FROM_DAYS");

            if (reader.IsColumnExists("TO_DAYS"))
                item.TO_DAYS = SqlHelper.GetNullableInt32(reader, "TO_DAYS");

            if (reader.IsColumnExists("20FT_RATE"))
                item.RATE20 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "20FT_RATE"));

            if (reader.IsColumnExists("40FT_RATE"))
                item.RATE40 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "40FT_RATE"));

            if (reader.IsColumnExists("HC_RATE"))
                item.HC_RATE = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "HC_RATE"));

            if (reader.IsColumnExists("CREATED_BY"))
                item.CREATED_BY = SqlHelper.GetNullableString(reader, "CREATED_BY");

            if (reader.IsColumnExists("CREATED_DATE"))
                item.CREATED_DATE = SqlHelper.GetDateTime(reader, "CREATED_DATE");

            return item;
        }
        public static MANDATORY_MASTER TranslateAsMandatoryMaster(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new MANDATORY_MASTER();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("PORT_CODE"))
                item.PORT_CODE = SqlHelper.GetNullableString(reader, "PORT_CODE");

            if (reader.IsColumnExists("ORG_CODE"))
                item.ORG_CODE = SqlHelper.GetNullableString(reader, "ORG_CODE");

            if (reader.IsColumnExists("CHARGE_CODE"))
                item.CHARGE_CODE = SqlHelper.GetNullableString(reader, "CHARGE_CODE");

            if (reader.IsColumnExists("IE_TYPE"))
                item.IE_TYPE = SqlHelper.GetNullableString(reader, "IE_TYPE");

            if (reader.IsColumnExists("LADEN_STATUS"))
                item.LADEN_STATUS = SqlHelper.GetNullableString(reader, "LADEN_STATUS");

            if (reader.IsColumnExists("CURRENCY"))
                item.CURRENCY = SqlHelper.GetNullableString(reader, "CURRENCY");

            if (reader.IsColumnExists("RATE20"))
                item.RATE20 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "RATE20"));

            if (reader.IsColumnExists("RATE40"))
                item.RATE40 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "RATE40"));

            if (reader.IsColumnExists("IS_PERCENTAGE"))
                item.IS_PERCENTAGE = SqlHelper.GetBoolean(reader, "IS_PERCENTAGE");

            if (reader.IsColumnExists("PERCENTAGE_VALUE"))
                item.PERCENTAGE_VALUE = SqlHelper.GetNullableInt32(reader, "PERCENTAGE_VALUE");

            return item;
        }
        public static ORG_MASTER TranslateAsOrgMaster(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new ORG_MASTER();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("ORG_NAME"))
                item.ORG_NAME = SqlHelper.GetNullableString(reader, "ORG_NAME");

            if (reader.IsColumnExists("ORG_CODE"))
                item.ORG_CODE = SqlHelper.GetNullableString(reader, "ORG_CODE");

            if (reader.IsColumnExists("PAN"))
                item.PAN = SqlHelper.GetNullableString(reader, "PAN");

            if (reader.IsColumnExists("CONTACT_PERSON_NAME"))
                item.CONTACT_PERSON_NAME = SqlHelper.GetNullableString(reader, "CONTACT_PERSON_NAME");

            if (reader.IsColumnExists("CONTACT_PERSON_NO"))
                item.CONTACT_PERSON_NO = SqlHelper.GetNullableString(reader, "CONTACT_PERSON_NO");

            if (reader.IsColumnExists("IS_GROUP_COMPANIES"))
                item.IS_GROUP_COMPANIES = SqlHelper.GetBoolean(reader, "IS_GROUP_COMPANIES");

            if (reader.IsColumnExists("SALES_NAME"))
                item.SALES_NAME = SqlHelper.GetNullableString(reader, "SALES_NAME");

            if (reader.IsColumnExists("SALES_CODE"))
                item.SALES_CODE = SqlHelper.GetNullableString(reader, "SALES_CODE");

            if (reader.IsColumnExists("SALES_LOC"))
                item.SALES_LOC = SqlHelper.GetNullableString(reader, "SALES_LOC");

            if (reader.IsColumnExists("SALES_EFFECTIVE_DATE"))
                item.SALES_EFFECTIVE_DATE = SqlHelper.GetNullableString(reader, "SALES_EFFECTIVE_DATE");

            if (reader.IsColumnExists("STATUS"))
                item.STATUS = SqlHelper.GetBoolean(reader, "STATUS");

            return item;
        }
        public static SLOT_MASTER TranslateAsSlotMaster(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new SLOT_MASTER();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("SLOT_OPERATOR"))
                item.SLOT_OPERATOR = SqlHelper.GetNullableString(reader, "SLOT_OPERATOR");

            if (reader.IsColumnExists("SERVICES"))
                item.SERVICES = SqlHelper.GetNullableString(reader, "SERVICES");

            if (reader.IsColumnExists("PORT_CODE"))
                item.PORT_CODE = SqlHelper.GetNullableString(reader, "PORT_CODE");

            if (reader.IsColumnExists("LINER_CODE"))
                item.LINER_CODE = SqlHelper.GetNullableString(reader, "LINER_CODE");

            if (reader.IsColumnExists("TERM"))
                item.TERM = SqlHelper.GetNullableString(reader, "TERM");

            if (reader.IsColumnExists("STATUS"))
                item.STATUS = SqlHelper.GetBoolean(reader, "STATUS");

            if (reader.IsColumnExists("CREATED_BY"))
                item.CREATED_BY = SqlHelper.GetNullableString(reader, "CREATED_BY");

            if (reader.IsColumnExists("CREATED_DATE"))
                item.CREATED_DATE = SqlHelper.GetDateTime(reader, "CREATED_DATE");

            return item;
        }

        //added for slot rate master
        public static SLOT_RATE_MASTER TranslateAsSlotRateMaster(this SqlDataReader reader, bool isList = false)   /// slot rate master
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new SLOT_RATE_MASTER();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("SLOT_OPERATOR_NAME"))
                item.SLOT_OPERATOR_NAME = SqlHelper.GetNullableString(reader, "SLOT_OPERATOR_NAME");

            if (reader.IsColumnExists("SERVICE"))
                item.SERVICE = SqlHelper.GetNullableString(reader, "SERVICE");

            if (reader.IsColumnExists("SERVICE_MODE"))
                item.SERVICE_MODE = SqlHelper.GetNullableString(reader, "SERVICE_MODE");

            if (reader.IsColumnExists("LADEN_STATUS"))
                item.LADEN_STATUS = SqlHelper.GetNullableString(reader, "LADEN_STATUS");

            if (reader.IsColumnExists("POL"))
                item.POL = SqlHelper.GetNullableString(reader, "POL");

            if (reader.IsColumnExists("POD"))
                item.POD = SqlHelper.GetNullableString(reader, "POD");

            if (reader.IsColumnExists("CURRENCY"))
                item.CURRENCY = SqlHelper.GetNullableString(reader, "CURRENCY");

            if (reader.IsColumnExists("RATE20"))
                item.RATE20 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "RATE20"));

            if (reader.IsColumnExists("RATE40"))
                item.RATE40 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "RATE40"));

            if (reader.IsColumnExists("HC20"))
                item.HC20 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "HC20"));

            if (reader.IsColumnExists("RF20"))
                item.RF20 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "RF20"));

            if (reader.IsColumnExists("HAZ20"))
                item.HAZ20 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "HAZ20"));

            if (reader.IsColumnExists("RF40"))
                item.RF40 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "RF40"));

            if (reader.IsColumnExists("HC40"))
                item.HC40 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "HC40"));

            if (reader.IsColumnExists("HAZ40"))
                item.HAZ40 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "HAZ40"));

            if (reader.IsColumnExists("HAZ20"))
                item.HAZ20 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "HAZ20"));

            if (reader.IsColumnExists("HAZ40"))
                item.HAZ40 = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "HAZ40"));

            if (reader.IsColumnExists("FROM_DATE"))
                //item.FROM_DATE = Convert.ToDateTime(SqlHelper.GetNullableString(reader, "FROM_DATE"));
                item.FROM_DATE = SqlHelper.GetNullableString(reader, "FROM_DATE");

            if (reader.IsColumnExists("TO_DATE"))
                //item.TO_DATE = Convert.ToDateTime(SqlHelper.GetNullableString(reader, "TO_DATE"));
                item.TO_DATE = SqlHelper.GetNullableString(reader, "TO_DATE");


            return item;
        }

        //ADD NEW FOR EQUIPMENT_TYPE
        public static EQUIPMENT_TYPE_MASTER TranslateAsEQUIPMENT(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new EQUIPMENT_TYPE_MASTER();

            if (reader.IsColumnExists("EQUIPMENT_TYPE_ID"))
                item.EQUIPMENT_TYPE_ID = SqlHelper.GetNullableInt32(reader, "EQUIPMENT_TYPE_ID");

            if (reader.IsColumnExists("EQUIPMENT_TYPE"))
                item.EQUIPMENT_TYPE = SqlHelper.GetNullableString(reader, "EQUIPMENT_TYPE");

            if (reader.IsColumnExists("DESCRIPTION"))
                item.DESCRIPTION = SqlHelper.GetNullableString(reader, "DESCRIPTION");

            if (reader.IsColumnExists("IS_ACTIVE"))
                item.IS_ACTIVE = SqlHelper.GetBoolean(reader, "IS_ACTIVE");

            if (reader.IsColumnExists("CREATED_BY"))
                item.CREATED_BY = SqlHelper.GetNullableString(reader, "CREATED_BY");

            if (reader.IsColumnExists("CREATED_AT"))
                item.CREATED_AT = SqlHelper.GetDateTime(reader, "CREATED_AT");

            if (reader.IsColumnExists("MODIFIED_BY"))
                item.MODIFIED_BY = SqlHelper.GetNullableString(reader, "MODIFIED_BY");

            if (reader.IsColumnExists("MODIFIED_AT"))
                item.MODIFIED_AT = SqlHelper.GetDateTime(reader, "MODIFIED_AT");

            if (reader.IsColumnExists("DELETED_BY"))
                item.DELETED_BY = SqlHelper.GetNullableString(reader, "DELETED_BY");

            if (reader.IsColumnExists("DELETED_AT"))
                item.DELETED_AT = SqlHelper.GetDateTime(reader, "DELETED_AT");

            return item;
        }

        public static CRO_DETAILS TranslateAsCRODETAILS(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new CRO_DETAILS();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("CRO_NO"))
                item.CRO_NO = SqlHelper.GetNullableString(reader, "CRO_NO");

            if (reader.IsColumnExists("BOOKING_NO"))
                item.BOOKING_NO = SqlHelper.GetNullableString(reader, "BOOKING_NO");

            if (reader.IsColumnExists("VESSEL_NAME"))
                item.VESSEL_NAME = SqlHelper.GetNullableString(reader, "VESSEL_NAME");

            if (reader.IsColumnExists("VOYAGE_NO"))
                item.VOYAGE_NO = SqlHelper.GetNullableString(reader, "VOYAGE_NO");

            if (reader.IsColumnExists("POL"))
                item.POL = SqlHelper.GetNullableString(reader, "POL");

            if (reader.IsColumnExists("POD"))
                item.POD = SqlHelper.GetNullableString(reader, "POD");

            return item;
        }

        public static MNR_TARIFF_LIST TranslateAsMNRTariffDetails(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new MNR_TARIFF_LIST();

            if (reader.IsColumnExists("ID"))
                item.ID = SqlHelper.GetNullableInt32(reader, "ID");

            if (reader.IsColumnExists("PORT_CODE"))
                item.PORT_CODE = SqlHelper.GetNullableString(reader, "PORT_CODE");

            if (reader.IsColumnExists("DEPO_CODE"))
                item.DEPO_CODE = SqlHelper.GetNullableString(reader, "DEPO_CODE");

            if (reader.IsColumnExists("COMPONENT"))
                item.COMPONENT = SqlHelper.GetNullableString(reader, "COMPONENT");

            if (reader.IsColumnExists("DAMAGE_LOCATION"))
                item.DAMAGE_LOCATION = SqlHelper.GetNullableString(reader, "DAMAGE_LOCATION");

            if (reader.IsColumnExists("REPAIR"))
                item.REPAIR = SqlHelper.GetNullableString(reader, "REPAIR");

            if (reader.IsColumnExists("LENGTH"))
                item.LENGTH = SqlHelper.GetNullableString(reader, "LENGTH");

            if (reader.IsColumnExists("WIDTH"))
                item.WIDTH = SqlHelper.GetNullableString(reader, "WIDTH");

            if (reader.IsColumnExists("HEIGHT"))
                item.HEIGHT = SqlHelper.GetNullableString(reader, "HEIGHT");

            if (reader.IsColumnExists("QUANTITY"))
                item.QUANTITY = SqlHelper.GetNullableString(reader, "QUANTITY");

            if (reader.IsColumnExists("DESCRIPTION_OF_REPAIRS"))
                item.DESCRIPTION_OF_REPAIRS = SqlHelper.GetNullableString(reader, "DESCRIPTION_OF_REPAIRS");

            if (reader.IsColumnExists("MAN_HOUR"))
                item.MAN_HOUR = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "MAN_HOUR"));

            if (reader.IsColumnExists("LABOUR_CHARGE"))
                item.LABOUR_CHARGE = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "LABOUR_CHARGE"));

            if (reader.IsColumnExists("MATERIAL_COST"))
                item.MATERIAL_COST = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "MATERIAL_COST"));

            if (reader.IsColumnExists("TOTAL"))
                item.TOTAL = Convert.ToDecimal(SqlHelper.GetNullableString(reader, "TOTAL"));

            return item;
        }
    }
}
