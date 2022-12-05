using API_Licencas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_Licencas.Controllers
{
    public class clienteNeoController : ApiController
    {
        [Route("api/clienteNeo/{subscriber_id}")]
        public HttpResponseMessage Get(string subscriber_id)
        {
            var usuario = new KL_Neo().cliente_neo(subscriber_id);

            // var usuario = new Usuario_Neo();

            return Request.CreateResponse(HttpStatusCode.OK, usuario);
        }

        // POST: api/loginNeo
        public HttpResponseMessage Post([FromBody] object value)
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Login_Neo>(value.ToString());
            Login_Neo login = new Login_Neo();
            login = (Login_Neo)obj;

            var usuario = new KL_Neo().cliente_neo(login.subscriber_id);

            // var usuario = new Usuario_Neo();

            return Request.CreateResponse(HttpStatusCode.OK, usuario);
        }
    }
}
