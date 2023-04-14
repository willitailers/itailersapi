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
    public class ProvisionarController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Provisionar([FromBody] int id_produto)
        {

            var clientInfo = new ClientInfo();
            if (Request.Headers.Contains("kl-token"))
            {
                string token = Request.Headers.GetValues("kl-token").First();
                clientInfo = new Ativacao_Controle().ValidaToken(token);

                new Ativacao_Controle().ProvisionarTodas(clientInfo, null, id_produto);

            }

            return Request.CreateResponse<string>(HttpStatusCode.NotAcceptable, "OK!!!");
        }
    }
}
