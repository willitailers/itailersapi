using KL_API.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;

namespace KL_API.Controllers.New
{
    public class GetUsersController : ApiController
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = false)]
        public HttpResponseMessage GetUsers([FromBody] User user)
        {
            var client = new ClientInfo();
            if (Request.Headers.Contains("kl-token"))
            {
                string token = Request.Headers.GetValues("kl-token").First();
                client = new Ativacao_Controle().ValidaToken(token);

                if (client.valido is false)
                {
                    return Request.CreateResponse<string>(HttpStatusCode.NotAcceptable, "Token Inválido" );
                }
            }
            else
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotAcceptable, "Token Inválido" );
            }

            Ativacao_Controle ativacao_Controle = new Ativacao_Controle();
            List<GetUsersReturn> getUsersReturn = ativacao_Controle.GetUsersReturn(client.id_cliente.ToString(), user.UserID);

            return Request.CreateResponse(HttpStatusCode.OK, getUsersReturn);
        }
    }
}
