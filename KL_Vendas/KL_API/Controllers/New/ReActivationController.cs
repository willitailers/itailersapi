using KL_API.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KL_API.Controllers.New
{
    public class ReActivationController : ApiController
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
                            return Request.CreateResponse<string>(HttpStatusCode.NotAcceptable, "Solicitação não pode ser processada");
                        }
                    }
                    else 
                    {
                        return Request.CreateResponse<string>(HttpStatusCode.NotAcceptable, "Solicitação não pode ser processada");
                    }
                }
                else 
                {
                    return Request.CreateResponse<string>(HttpStatusCode.NotAcceptable, "Solicitação não pode ser processada");
                }

                var retorno = new Ativacao_Controle().LicenseReActivation(activation.UserID, client);

                if (retorno.cod_retorno == 0)
                    return Request.CreateResponse<string>(HttpStatusCode.OK, retorno.msg_retorno);
                else if (retorno.cod_retorno == -4 || retorno.cod_retorno == -3)
                    return Request.CreateResponse<string>(HttpStatusCode.BadRequest, retorno.msg_retorno);
                else
                    return Request.CreateResponse<string>(HttpStatusCode.NotAcceptable, retorno.msg_retorno);
            }
            catch (Exception ex)
            {
                if (id_cliente_usuario != "")
                    new Ativacao_Controle().ClienteDeletar(id_cliente_usuario, 1);
                new Ativacao_Controle().log_inserir("Erro ativacao " + ex.Message, (int)Lista_Erro.usar_add);
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Solicitação não pode ser processada");
            }
        }
    }
}
