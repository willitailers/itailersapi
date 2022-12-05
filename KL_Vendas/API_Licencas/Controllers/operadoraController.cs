using API_Licencas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data;

namespace API_Licencas.Controllers
{
    public class operadoraController : ApiController
    {
        // GET: api/operadora
        public HttpResponseMessage Get()
        {

            return Request.CreateResponse(HttpStatusCode.OK, new KL_Neo().consulta_operadora_dinamico());
        }

        // GET: api/operadora/5
        public HttpResponseMessage Get(string id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new KL_Neo().consulta_operadora_dinamico());
        }

        // POST: api/operadora
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/operadora/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/operadora/5
        //public void Delete(int id)
        //{
        //}
    }
}
