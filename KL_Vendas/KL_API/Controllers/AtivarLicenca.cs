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

                var provisionamento = new Ativacao_Controle().Retorna_provisionamento(ativacao.id_cliente_usuario, ativacao.id_produto);

                if (provisionamento.Rows.Count <= 5)
                {
                    try
                    {
                        new Ativacao_Controle().Provisionar(clientInfo, provisionamento, ativacao.id_produto);
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        new Ativacao_Controle().log_inserir_provisionamento(msg, 0);
                        return Request.CreateResponse<LoginRetorno>(HttpStatusCode.NotAcceptable,
                            new LoginRetorno() { cod_retorno = -1, msg_retorno = "Não foi possivel realizar o provisionamento para o produto: " + ativacao.id_produto });
                        throw;
                    }
                }

                AtivacaoRetorno ativacaoRetorno = new AtivacaoRetorno();
                if (clientInfo.valido)
                {
                    ativacaoRetorno = new Ativacao_Controle().AtivarLicenca(ativacao, clientInfo);

                    if (ativacaoRetorno.cod_retorno < 0)
                    {
                        return Request.CreateResponse<AtivacaoRetorno>(HttpStatusCode.NotAcceptable, ativacaoRetorno);
                    }
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
