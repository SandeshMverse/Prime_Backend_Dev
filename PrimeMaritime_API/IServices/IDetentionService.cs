﻿using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Response;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.IServices
{
    public interface IDetentionService
    {
        Response<List<DETENTION_WAIVER_REQUEST>> GetDetentionListByDO(string DO_NO);
        Response<List<DETENTION_WAIVER_REQUEST>> GetDetentionListByBL(string BL_NO);

        Response<List<DETENTION_WAIVER_REQUEST>> GetDetentionListByLocation(string location);
        Response<List<DETENTION_WAIVER_REQUEST>> GetDetentionListByLocationAndDetentionType(string location, string DETENTION_TYPE);
        Response<List<DETENTION_WAIVER_REQUEST>> GetDetentionMasterList(string LOCATION, string MONTH, string YEAR);
        Response<string> InsertDetention(DETENTION Request);
        Response<string> UpdateDetention(DETENTION Request);

        Response<string> UpdateDetentionByBL(DETENTION Request);
        Response<decimal> GetTotalDetentionCost(string CONTAINER_NO);
        Response<List<CONTAINER_DETENTION>> GetContainerDetentionList();
        Response<DO_DETENTION_DETAILS> GetDODetailsForDetention(string DO_NO);
        Response<DO_DETENTION_DETAILS> GetBLDetailsForDetention(string BL_NO);
        Response<DETENTION_MASTER> GetDetentionCharges(string ACCEPTANCE_LOCATION, int DAYS, string CURRENCY_CODE, string CONTAINER_TYPE, string IS_JUMPING,int FREEDAYS);
    }
}
