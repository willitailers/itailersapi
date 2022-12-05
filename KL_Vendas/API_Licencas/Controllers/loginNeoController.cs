using API_Licencas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_Licencas.Controllers
{
    public class loginNeoController : ApiController
    {
        // GET: api/loginNeo
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/loginNeo/5
        [Route("api/loginNeo/{login}/{password}/{idp}")]
        public HttpResponseMessage Get(string login, string password, string idp)
        {
            var usuario = new KL_Neo().login(new Login_Neo() { idp = idp, password = password, username = login });

            // var usuario = new Usuario_Neo();

            return Request.CreateResponse(HttpStatusCode.OK, usuario);
        }

        // POST: api/loginNeo
        public HttpResponseMessage Post([FromBody]object value)
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Login_Neo>(value.ToString());
            Login_Neo login = new Login_Neo();
            login = (Login_Neo)obj;

            var usuario = new KL_Neo().login(login);

            // var usuario = new Usuario_Neo();

            return Request.CreateResponse(HttpStatusCode.OK, usuario);
        }

        
    }
}
