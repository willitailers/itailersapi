using KL_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KL_API.Controllers.New
{
    public class HardCancelController : ApiController
    {
        // POST: api/HardCancel
        /// <summary>
        /// Deleção de Usuario e Cancelamento de licença
        /// </summary>
        /// <param name="ativacao">Objeto de ativação KL</param>
        /// <response code="200">OK</response>
        /// <response code="406">Informações incorretas</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Erro ao realizar o processo</response>
        [HttpPost]
        public HttpResponseMessage Post([FromBody]UserDelete userDelete)
        {
            try
            {
                var client = new ClientInfo();
                if (Request.Headers.Contains("kl-token"))
                {
                    string token = Request.Headers.GetValues("kl-token").First();
                    client = new Ativacao_Controle().ValidaToken(token);
                    if (client.valido)
                    {
                        if(string.IsNullOrEmpty(userDelete.UserID))
                        {
                            return Request.CreateResponse<UserDelete_Retorno>(HttpStatusCode.NotAcceptable, new UserDelete_Retorno() { cod_retorno = -1, msg_retorno = "Código UserID é obrigatório" });
                        }
                    }
                    else
                    {
                        return Request.CreateResponse<UserDelete_Retorno>(HttpStatusCode.NotAcceptable, new UserDelete_Retorno() { cod_retorno = -1, msg_retorno = "Token Inválido" });
                    }
                }
                else
                {
                    return Request.CreateResponse<UserDelete_Retorno>(HttpStatusCode.NotAcceptable, new UserDelete_Retorno() { cod_retorno = -1, msg_retorno = "Token Inválido" });
                }

                // Passou da validação
                var retorno = new Ativacao_Controle().deleteUser(userDelete, client);

                if (retorno.cod_retorno == 0)
                    return Request.CreateResponse<UserDelete_Retorno>(HttpStatusCode.OK, retorno);
                else if (retorno.cod_retorno == -4 || retorno.cod_retorno == -3)
                    return Request.CreateResponse<UserDelete_Retorno>(HttpStatusCode.BadRequest, retorno);
                else
                    return Request.CreateResponse<UserDelete_Retorno>(HttpStatusCode.NotAcceptable, retorno);
            }
            catch (Exception ex)
            {
                new Ativacao_Controle().log_inserir("HARD CANCEL - ERRO " + ex.Message, (int)Lista_Erro.user_delete);
                return Request.CreateResponse<UserDelete_Retorno>(HttpStatusCode.BadRequest, new UserDelete_Retorno() { cod_retorno = -1, msg_retorno = "Solicitação não pode ser processada" });
            }
        }

    }
}
