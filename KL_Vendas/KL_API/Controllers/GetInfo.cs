using KL_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace KL_API.Controllers
{
    public class GetInfoController : ApiController
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage GetInfo()
        {
            return Request.CreateResponse<string>(HttpStatusCode.NotAcceptable, "OK!!!");
        }
    }
}
