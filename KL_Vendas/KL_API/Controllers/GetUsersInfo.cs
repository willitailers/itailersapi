using KL_API.Models;
using System.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;
using System.Web.Services.Protocols;

namespace KL_API.Controllers
{
    public class GetUsersInfoController : ApiController
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = false)]
        public HttpResponseMessage GetUsersInfo()
        {
            var client = new ClientInfo();
            if (Request.Headers.Contains("kl-token"))
            {
                string token = Request.Headers.GetValues("kl-token").First();
                client = new Ativacao_Controle().ValidaToken(token);

                if (client.valido is false)
                {
                    return Request.CreateResponse<UserAdd_Retorno>(HttpStatusCode.NotAcceptable, new UserAdd_Retorno() { cod_retorno = -1, msg_retorno = "Token Inválido" });
                }
            }
            else
            {
                return Request.CreateResponse<UserAdd_Retorno>(HttpStatusCode.NotAcceptable, new UserAdd_Retorno() { cod_retorno = -1, msg_retorno = "Token Inválido" });
            }

            Ativacao_Controle ativacao_Controle = new Ativacao_Controle();
            List<GetUsersInfoReturn> getUsersInfoReturn = ativacao_Controle.GetUsersInfoReturn(client.id_cliente.ToString());

            return Request.CreateResponse(HttpStatusCode.OK, getUsersInfoReturn);
        }
    }
}
