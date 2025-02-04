using KL_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace KL_API.Controllers.New
{
    public class IntegracaoHardCancelLoteController : ApiController
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
        public async Task<HttpResponseMessage> Post([FromBody] IntegracaoUserDeleteLote userDelete)
        {
            Models.Integracao.Integracao integracao = new Models.Integracao.Integracao();

            integracao.InsereIntegracaoLog("Começando Integracao Kaspersky");

            try
            {
                var client = new ClientInfo();
                if (Request.Headers.Contains("kl-token"))
                {
                    string token = Request.Headers.GetValues("kl-token").First();
                    client = new Ativacao_Controle().ValidaToken(token);
                    if (client.valido)
                    {
                        if(userDelete.SubscriberId.Count() == 0)
                        {
                            return Request.CreateResponse<string>(HttpStatusCode.NotAcceptable, "Código SubscriberId é obrigatório");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse<string>(HttpStatusCode.NotAcceptable, "Token Inválido");
                    }
                }
                else
                {
                    return Request.CreateResponse<string>(HttpStatusCode.NotAcceptable, "Token Inválido");
                }

                // Passou da validação
                var retorno = new Ativacao_Controle().IntegracaoDeleteUserLote(userDelete, client);

                if (retorno.Result.cod_retorno == 0)
                    return Request.CreateResponse(HttpStatusCode.OK);
                else if (retorno.Result.cod_retorno == -4 || retorno.Result.cod_retorno == -3)
                    return Request.CreateResponse<string>(HttpStatusCode.BadRequest, retorno.Result.msg_retorno);
                else
                    return Request.CreateResponse<string>(HttpStatusCode.NotAcceptable, retorno.Result.msg_retorno);
            }
            catch (Exception ex)
            {
                integracao.InsereIntegracaoLog("HARD CANCEL - ERRO " + ex.Message);
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Solicitação não pode ser processada");
            }
        }

    }
}
