using KL_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KL_API.Controllers.New
{
    public class ActivationWithValidationController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Activation activation)
        {
            string id_cliente_usuario = "";
            try
            {
                Ativacao_Controle ativacao_Controle = new Ativacao_Controle();

                var client = new ClientInfo();
                if (Request.Headers.Contains("kl-token"))
                {
                    string token = Request.Headers.GetValues("kl-token").First();
                    client = ativacao_Controle.ValidaToken(token);
                    if (client.valido)
                    {
                        if (string.IsNullOrEmpty(activation.UserID))
                        {
                            return Request.CreateResponse<string>(HttpStatusCode.NotAcceptable, "Código UserID é obrigatório");
                        }

                        if (activation.Products == null || activation.Products.Count <= 0)
                        {
                            return Request.CreateResponse<string>(HttpStatusCode.NotAcceptable, "É obrigatório o envio de no mínimo um produto para ativação.");
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

                var productsAlreadyActivatedForUser = ativacao_Controle.ValidateUserProductExists(client.id_cliente.ToString(),
                    activation.UserID, activation.Products.Select(s => s.ProductID).ToList());

                if (productsAlreadyActivatedForUser != null && productsAlreadyActivatedForUser.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"O usuário {activation.UserID} já possui o produto ativado");
                }

                var retorno = ativacao_Controle.LicenseActivation(activation, client);

                if (retorno.Count > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, retorno);
                else
                    return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Não foi possível processar sua solicitação");
            }
            catch (Exception ex)
            {
                
                new Ativacao_Controle().log_inserir("Erro ativacao " + ex.Message, (int)Lista_Erro.usar_add);
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Solicitação não pode ser processada");
            }
        }
    }
}
