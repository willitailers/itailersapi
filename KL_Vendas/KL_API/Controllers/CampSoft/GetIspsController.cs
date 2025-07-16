using KL_API.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;
using KL_API.Models.Campsoft;
using KL_API.Controllers.CampSoft.Models;
using System.Threading.Tasks;

namespace KL_API.Controllers.CampSoft
{
    public class GetIspsController : ApiController
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<HttpResponseMessage> GetIsps()
        {
            Campsoft campsoft = new Campsoft();
            Isps isps = await campsoft.GetIspsAsync();

            if (isps == null || isps.data == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, isps);
        }
    }
}
