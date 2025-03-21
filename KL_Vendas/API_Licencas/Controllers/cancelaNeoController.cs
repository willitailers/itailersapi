﻿using API_Licencas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_Licencas.Controllers
{
    public class cancelaNeoController : ApiController
    {
        // GET: api/cancelaNeo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/cancelaNeo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/cancelaNeo
        public HttpResponseMessage Post([FromBody] object value)
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Login_Neo>(value.ToString());
            Login_Neo login = new Login_Neo();
            login = (Login_Neo)obj;

            var usuario = new KL_Neo().CancelamentoNeo(login.subscriber_id);

            // var usuario = new Usuario_Neo();

            return Request.CreateResponse(HttpStatusCode.OK, usuario);
        }

        // PUT: api/cancelaNeo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/cancelaNeo/5
        public void Delete(int id)
        {
        }
    }
}
