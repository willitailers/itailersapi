using KL_API.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KL_API.Controllers.New
{
    public class ActivationController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Activation activation)
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

                        if (activation.Products == null || activation.Products.Count <= 0)
                        {
                            return Request.CreateResponse<UserAdd_Retorno>(HttpStatusCode.NotAcceptable, new UserAdd_Retorno() { cod_retorno = -1, msg_retorno = "É obrigatório o envio de no mínimo um produto para ativação." });
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

                UserAdd userAdd = new UserAdd()
                {
                    Email = activation.Email,
                    ProductList = activation.Products,
                    UserID = activation.UserID,
                    StartDate = DateTime.Now
                };

                // passou na validação
                var dt_usuario = new Ativacao_Controle().addUser(userAdd, client);

                if (dt_usuario.Rows.Count <= 0)
                {
                    return Request.CreateResponse<UserAdd_Retorno>(HttpStatusCode.BadRequest, new UserAdd_Retorno() { cod_retorno = -1, msg_retorno = "Solicitação não pode ser processada" });
                }
                else
                {
                    id_cliente_usuario = dt_usuario.Rows[0]["id_cliente_usuario"].ToString();
                }

                var retorno = new Ativacao_Controle().LicenseActivation(activation, client, dt_usuario);

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
