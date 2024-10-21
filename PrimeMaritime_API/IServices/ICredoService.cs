using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.IServices
{
    public interface ICredoService
    {
      Response<CREDO> GetCredoDetails(string AGENT_CODE, string VESSEL_NAME, string VOYAGE_NO, string PORT_OF_LOADING, string PORT_OF_DISCHARGE);

    }
}
