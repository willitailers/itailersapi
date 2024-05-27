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
    public class GetInfoController : ApiController
    {
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage GetInfo([FromBody] string subscriber_id)
        {
            var client = new ClientInfo();
            if (Request.Headers.Contains("kl-token"))
            {
                string token = Request.Headers.GetValues("kl-token").First();
                client = new Ativacao_Controle().ValidaToken(token);
                if (client.valido)
                {
                    if (string.IsNullOrEmpty(subscriber_id))
                    {
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Código do usuário é obrigatório");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Token Inválido");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Token Inválido");
            }

            var retorno = new Ativacao_Controle().GetInfoKaspersky(subscriber_id, client.nm_usuario_certificado, 
                client.nm_senha_certificado, client.nm_thumbprint);

            return Request.CreateResponse(HttpStatusCode.OK, retorno);
        }
    }
}
