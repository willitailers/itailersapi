using API_Licencas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_Licencas.Controllers
{
    public class StatusController : ApiController
    {
        // GET: api/Status
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Status/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Status
        public HttpResponseMessage Post([FromBody] object value)
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Info_Neo>(value.ToString());
            Info_Neo login = new Info_Neo();
            login = (Info_Neo)obj;

            var usuario = new KL_Neo().DadosNeo(login.subscriber_id.ToArray());

            // var usuario = new Usuario_Neo();

            return Request.CreateResponse(HttpStatusCode.OK, usuario);
        }
        // PUT: api/Status/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Status/5
        //public void Delete(int id)
        //{
        //}
    }
}
