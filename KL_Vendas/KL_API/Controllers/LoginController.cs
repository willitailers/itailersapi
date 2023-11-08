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
    public class LoginController : ApiController
    {
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage Post([FromBody]Login login)
        {
            var clientInfo = new ClientInfo();
            if (!Request.Headers.Contains("kl-token"))
                return Request.CreateResponse<LoginRetorno>(HttpStatusCode.NotAcceptable, new LoginRetorno() { cod_retorno = -1, msg_retorno = "Token é obrigatório" });

            string token = Request.Headers.GetValues("kl-token").First();
            clientInfo = new Ativacao_Controle().ValidaToken(token);

            if (!clientInfo.valido)
                return Request.CreateResponse<LoginRetorno>(HttpStatusCode.NotAcceptable, new LoginRetorno() { cod_retorno = -1, msg_retorno = "Token Inválido" });

            LoginRetorno loginRetorno = new Ativacao_Controle().login(login, clientInfo);

            if (!loginRetorno.autenticado)
                return Request.CreateResponse<LoginRetorno>(HttpStatusCode.NotAcceptable, loginRetorno);

            return Request.CreateResponse<LoginRetorno>(HttpStatusCode.OK, loginRetorno);
        }
    }
}
