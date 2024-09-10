using KL_API.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KL_API.Controllers.New
{
    public class RectivationController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody]ReActivation activation)
        {
            string id_cliente_usuario = "";
            try
            {
                var client = new ClientInfo();
                if (Request.Headers.Contains("kl-token"))
                {
                    string token = Request.Headers.GetValues("kl-token").First();
                    client = new Ativacao_Controle().ValidaToken(token);
                    if (client.valido)
                    {
                        if (string.IsNullOrEmpty(activation.UserID))
                        {
                            return Request.CreateResponse<UserAdd_Retorno>(HttpStatusCode.NotAcceptable, new UserAdd_Retorno() { cod_retorno = -1, msg_retorno = "Campo UserID é obrigatório" });
                        }
                    }
                    else 
                    {
                        return Request.CreateResponse<UserAdd_Retorno>(HttpStatusCode.NotAcceptable, new UserAdd_Retorno() { cod_retorno = -1, msg_retorno = "Token Inválido" });
                    }
                }
                else 
                {
                    return Request.CreateResponse<UserAdd_Retorno>(HttpStatusCode.NotAcceptable, new UserAdd_Retorno() { cod_retorno = -1, msg_retorno = "Token Inválido" });
                }

                var retorno = new Ativacao_Controle().LicenseReActivation(activation.UserID, client);

                if (retorno.cod_retorno == 0)
                    return Request.CreateResponse<UserAdd_Retorno>(HttpStatusCode.OK, retorno);
                else if (retorno.cod_retorno == -4 || retorno.cod_retorno == -3)
                    return Request.CreateResponse<UserAdd_Retorno>(HttpStatusCode.BadRequest, retorno);
                else
                    return Request.CreateResponse<UserAdd_Retorno>(HttpStatusCode.NotAcceptable, retorno);
            }
            catch (Exception ex)
            {
                if (id_cliente_usuario != "")
                    new Ativacao_Controle().ClienteDeletar(id_cliente_usuario, 1);
                new Ativacao_Controle().log_inserir("Erro ativacao " + ex.Message, (int)Lista_Erro.usar_add);
                return Request.CreateResponse<UserAdd_Retorno>(HttpStatusCode.BadRequest, new UserAdd_Retorno() { cod_retorno = -1, msg_retorno = "Solicitação não pode ser processada" });
            }
        }

        /* PUT: api/UserAdd/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UserAdd/5
        public void Delete(int id)
        {
        }*/
    }
}
