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
    public class AtivarLicencaController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage AtivarLicenca([FromBody]Ativacao ativacao)
        {
            var clientInfo = new ClientInfo();
            if (Request.Headers.Contains("kl-token"))
            {
                string token = Request.Headers.GetValues("kl-token").First();
                clientInfo = new Ativacao_Controle().ValidaToken(token);
                AtivacaoRetorno ativacaoRetorno = new AtivacaoRetorno();

                if (clientInfo.valido)
                {
                    ativacaoRetorno = new Ativacao_Controle().AtivarLicenca(ativacao, clientInfo);
                }

                return Request.CreateResponse<AtivacaoRetorno>(HttpStatusCode.OK, ativacaoRetorno);
            }
            else
            {
                return Request.CreateResponse<LoginRetorno>(HttpStatusCode.NotAcceptable, new LoginRetorno() { cod_retorno = -1, msg_retorno = "Token é obrigatório" });
            }
        }
    }
}
