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
    public class LoginCampSoftController : ApiController
    {
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<HttpResponseMessage> LoginCampSoft([FromBody] RequestLoginCampSoft requestLoginCampSoft)
        {
            Campsoft campsoft = new Campsoft();
            
            LoginResponse loginResponse = await campsoft.LoginAsync(requestLoginCampSoft);

            if (!loginResponse.access)
            {
                loginResponse.token = "";
                loginResponse.user_id = "";
            }

            return Request.CreateResponse(HttpStatusCode.OK, loginResponse);
        }
    }
}
