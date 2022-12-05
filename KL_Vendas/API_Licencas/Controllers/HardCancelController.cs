using API_Licencas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_Licencas.Controllers
{
    public class HardCancelController : ApiController
    {
        // GET: api/HardCancel
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/HardCancel/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/HardCancel
        public void Post([FromBody]KL_Licenca_Hard_Cancel licenca)
        {
            try
            {

            }
            catch(Exception ex)
            {

            }
        }

        // PUT: api/HardCancel/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: api/HardCancel/5
        //public void Delete(int id)
        //{
        //}
    }
}
