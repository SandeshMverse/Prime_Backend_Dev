﻿using Microsoft.Extensions.Configuration;
using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.IServices;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Repository;
using PrimeMaritime_API.Response;
using PrimeMaritime_API.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Services
{
    public class DetentionService : IDetentionService
    {
        private readonly IConfiguration _config;

        public DetentionService(IConfiguration config)
        {
            _config = config;
        }

        public Response<List<DETENTION_WAIVER_REQUEST>> GetDetentionListByDO(string DO_NO)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<DETENTION_WAIVER_REQUEST>> response = new Response<List<DETENTION_WAIVER_REQUEST>>();
            var data = DbClientFactory<DetentionRepo>.Instance.GetDetentionList(dbConn, DO_NO);

            if (data.Count > 0)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<List<DETENTION_WAIVER_REQUEST>> GetDetentionListByBL(string BL_NO)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<DETENTION_WAIVER_REQUEST>> response = new Response<List<DETENTION_WAIVER_REQUEST>>();
            var data = DbClientFactory<DetentionRepo>.Instance.GetDetentionListBL(dbConn, BL_NO);

            if (data.Count > 0)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<List<DETENTION_WAIVER_REQUEST>> GetDetentionListByLocation(string location)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<DETENTION_WAIVER_REQUEST>> response = new Response<List<DETENTION_WAIVER_REQUEST>>();
            var data = DbClientFactory<DetentionRepo>.Instance.GetDetentionListByLocation(dbConn, location);

            if (data.Count > 0)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<List<DETENTION_WAIVER_REQUEST>> GetDetentionListByLocationAndDetentionType(string location, string DETENTION_TYPE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<DETENTION_WAIVER_REQUEST>> response = new Response<List<DETENTION_WAIVER_REQUEST>>();
            var data = DbClientFactory<DetentionRepo>.Instance.GetDetentionListByLocationAndDetentionType(dbConn, location , DETENTION_TYPE);

            if (data.Count > 0)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<List<DETENTION_WAIVER_REQUEST>> GetDetentionMasterList(string LOCATION, string MONTH, string YEAR)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<DETENTION_WAIVER_REQUEST>> response = new Response<List<DETENTION_WAIVER_REQUEST>>();
            var data = DbClientFactory<DetentionRepo>.Instance.GetDetentionMasterList(dbConn, LOCATION, MONTH, YEAR);

            if (data.Count > 0)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<decimal> GetTotalDetentionCost(string CONTAINER_NO)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            decimal Total = Convert.ToDecimal(DbClientFactory<DetentionRepo>.Instance.GetTotalDetentionCost(dbConn, CONTAINER_NO));

            Response<decimal> response = new Response<decimal>();

            response.Succeeded = true;
            response.ResponseCode = 200;
            response.Data = Total;

            return response;
        }

        public Response<string> InsertDetention(DETENTION Request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<DetentionRepo>.Instance.InsertDetention(dbConn, Request);

            Response<string> response = new Response<string>();
            response.Succeeded = true;
            response.ResponseMessage = "Inserted Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<string> UpdateDetention(DETENTION Request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<DetentionRepo>.Instance.UpdateDetention(dbConn, Request);

            Response<string> response = new Response<string>();
            response.Succeeded = true;
            response.ResponseMessage = "Updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<string> UpdateDetentionByBL(DETENTION Request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<DetentionRepo>.Instance.UpdateDetentionByBL(dbConn, Request);

            Response<string> response = new Response<string>();
            response.Succeeded = true;
            response.ResponseMessage = "Updated Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public Response<List<CONTAINER_DETENTION>> GetContainerDetentionList()
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<CONTAINER_DETENTION>> response = new Response<List<CONTAINER_DETENTION>>();
            var data = DbClientFactory<DetentionRepo>.Instance.GetContainerDetentionList(dbConn);

            if (data.Count > 0)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<DO_DETENTION_DETAILS> GetDODetailsForDetention(string DO_NO, string AGENT_CODE)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<DO_DETENTION_DETAILS> response = new Response<DO_DETENTION_DETAILS>();


            if ((DO_NO == "") || (DO_NO == null))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide DO No";
                return response;
            }

            var data = DbClientFactory<DetentionRepo>.Instance.GetDODetailsForDetention(dbConn, DO_NO, AGENT_CODE);

            if ((data != null) && (data.Tables[0].Rows.Count > 0))
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";

                DO_DETENTION_DETAILS doDetails = new DO_DETENTION_DETAILS();

                doDetails = DetentionRepo.GetSingleDataFromDataSet<DO_DETENTION_DETAILS>(data.Tables[0]);

                if (data.Tables.Contains("Table1"))
                {
                    doDetails.CONTAINER_LIST = DORepo.GetListFromDataSet<CONTAINERS>(data.Tables[1]);
                }

                response.Data = doDetails;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<DO_DETENTION_DETAILS> GetBLDetailsForDetention(string BL_NO, string AGENT_CODE, string ORG_CODE, string PORT)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<DO_DETENTION_DETAILS> response = new Response<DO_DETENTION_DETAILS>();


            if ((BL_NO == "") || (BL_NO == null))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide DO No";
                return response;
            }

            var data = DbClientFactory<DetentionRepo>.Instance.GetBLDetailsForDetention(dbConn, BL_NO, AGENT_CODE, ORG_CODE, PORT);

            if ((data != null) && (data.Tables[0].Rows.Count > 0))
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";

                DO_DETENTION_DETAILS doDetails = new DO_DETENTION_DETAILS();

                doDetails = DetentionRepo.GetSingleDataFromDataSet<DO_DETENTION_DETAILS>(data.Tables[0]);

                if (data.Tables.Contains("Table1"))
                {
                    doDetails.CONTAINER_LIST = DORepo.GetListFromDataSet<CONTAINERS>(data.Tables[1]);
                }

                response.Data = doDetails;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<DO_DETENTION_DETAILS> GetBLDetailsForImportDetention(string BL_NO)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<DO_DETENTION_DETAILS> response = new Response<DO_DETENTION_DETAILS>();


            if ((BL_NO == "") || (BL_NO == null))
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide DO No";
                return response;
            }

            var data = DbClientFactory<DetentionRepo>.Instance.GetBLDetailsForImportDetention(dbConn, BL_NO);

            if ((data != null) && (data.Tables[0].Rows.Count > 0))
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";

                DO_DETENTION_DETAILS doDetails = new DO_DETENTION_DETAILS();

                doDetails = DetentionRepo.GetSingleDataFromDataSet<DO_DETENTION_DETAILS>(data.Tables[0]);

                if (data.Tables.Contains("Table1"))
                {
                    doDetails.CONTAINER_LIST = DORepo.GetListFromDataSet<CONTAINERS>(data.Tables[1]);
                }

                response.Data = doDetails;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }

        public Response<DETENTION_MASTER> GetDetentionCharges(string ACCEPTANCE_LOCATION, int DAYS, string CURRENCY_CODE, string CONTAINER_TYPE , string IS_JUMPING, int FREEDAYS)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<DETENTION_MASTER> response = new Response<DETENTION_MASTER>();
            var data = DbClientFactory<DetentionRepo>.Instance.GetDetentionCharges(dbConn, ACCEPTANCE_LOCATION, DAYS, CURRENCY_CODE, CONTAINER_TYPE, IS_JUMPING, FREEDAYS);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
    }
}
