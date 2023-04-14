using KL_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KL_API.Controllers
{
    public class GetInfoController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetInfo()
        {
            return Request.CreateResponse<string>(HttpStatusCode.NotAcceptable, "OK!!!");
        }
    }
}
