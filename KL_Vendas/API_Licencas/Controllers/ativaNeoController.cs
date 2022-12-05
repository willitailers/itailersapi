using API_Licencas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_Licencas.Controllers
{
    public class ativaNeoController : ApiController
    {
        // GET: api/ativaNeo
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/ativaNeo/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/ativaNeo
        public HttpResponseMessage Post([FromBody] object value)
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Ativacao_Neo>(value.ToString());
            Ativacao_Neo login = new Ativacao_Neo();
            login = (Ativacao_Neo)obj;

            var usuario = new KL_Neo().ativar(login);

            // var usuario = new Usuario_Neo();

            return Request.CreateResponse(HttpStatusCode.OK, usuario);
        }

        // PUT: api/ativaNeo/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/ativaNeo/5
        //public void Delete(int id)
        //{
        //}
    }
}
