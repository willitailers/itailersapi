using KL_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KL_API.Controllers
{
    public class LicenseCancelController : ApiController
    {
        // POST: api/LicenseCancel
        /// <summary>
        /// Inclusão e usuario e ativação da licenca
        /// </summary>
        /// <param name="ativacao">Objeto de ativação KL</param>
        /// <response code="200">OK</response>
        /// <response code="406">Informações incorretas</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Erro ao realizar o processo</response>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] LicenseCancel ativacao)
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
                        if (string.IsNullOrEmpty(ativacao.UserID))
                        {
                            return Request.CreateResponse<LicenseCancel_Retorno>(HttpStatusCode.NotAcceptable, new LicenseCancel_Retorno() { cod_retorno = -3, msg_retorno = "Código do usuário é obrigatório"});
                        }

                        if (string.IsNullOrEmpty(ativacao.ProductID))
                        {
                            return Request.CreateResponse<LicenseCancel_Retorno>(HttpStatusCode.NotAcceptable, new LicenseCancel_Retorno() { cod_retorno = -3, msg_retorno = "ID de produto obrigatório"});
                        }
                    }
                    else
                    {
                        return Request.CreateResponse<LicenseCancel_Retorno>(HttpStatusCode.NotAcceptable, new LicenseCancel_Retorno() { cod_retorno = -1, msg_retorno = "Token Inválido" });
                    }
                }
                else
                {
                    return Request.CreateResponse<LicenseCancel_Retorno>(HttpStatusCode.NotAcceptable, new LicenseCancel_Retorno() { cod_retorno = -1, msg_retorno = "Token Inválido" });
                }

                // Passou da validação
                var retorno = new Ativacao_Controle().cancelar_assinatura(ativacao, client);

                if (retorno.cod_retorno == 0)
                    return Request.CreateResponse<LicenseCancel_Retorno>(HttpStatusCode.OK, retorno);
                else if(retorno.cod_retorno == -4 || retorno.cod_retorno == -3)
                    return Request.CreateResponse<LicenseCancel_Retorno>(HttpStatusCode.BadRequest, retorno);
                else
                    return Request.CreateResponse<LicenseCancel_Retorno>(HttpStatusCode.NotAcceptable, retorno);
            }
            catch (Exception ex)
            {
                new Ativacao_Controle().log_inserir("Erro cancelamento " + ex.Message, (int)Lista_Erro.license_cancel);
                return Request.CreateResponse<Ativacao_Retorno>(HttpStatusCode.BadRequest, new Ativacao_Retorno() { cod_retorno = -5, msg_retorno = "Solicitação não pode ser processada." });
            }
        }

    }
}
