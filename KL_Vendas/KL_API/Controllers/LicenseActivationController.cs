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
    public class LicenseActivationController : ApiController
    {
        //// GET: api/LicenseActivation
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/LicenseActivation/5
        //public string Get(int id)
        //{
        //    return "value";
        //}
        // POST: api/Ativacao
        /// <summary>
        /// Ativação de licença KL
        /// </summary>
        /// <param name="ativacao">Objeto de ativação KL</param>
        /// <response code="200">OK</response>
        /// <response code="406">Informações incorretas</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        public HttpResponseMessage Post([FromBody]LicenseActivation ativacao)
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
                            return Request.CreateResponse<Ativacao_Retorno>(HttpStatusCode.NotAcceptable, new Ativacao_Retorno() { cod_retorno = -1, msg_retorno = "Campo UserID é obrigatório"});
                        }

                        if (string.IsNullOrEmpty(ativacao.ProductID))
                        {
                            return Request.CreateResponse<Ativacao_Retorno>(HttpStatusCode.NotAcceptable, new Ativacao_Retorno() { cod_retorno = -1, msg_retorno = "Campo ProductID é obrigatório"});
                        }

                        //if (string.IsNullOrEmpty(ativacao.LicenseCount))
                        //{
                        //    return Request.CreateResponse<Ativacao_Retorno>(HttpStatusCode.NotAcceptable, new Ativacao_Retorno() { cod_retorno = -3, msg_retorno = "Licença obrigatório", link_ativacao = "" });
                        //}

                        if (ativacao.StartDate == DateTime.MinValue)
                        {
                            return Request.CreateResponse<Ativacao_Retorno>(HttpStatusCode.NotAcceptable, new Ativacao_Retorno() { cod_retorno = -1, msg_retorno = "campo StartDate é obrigatório" });
                        }

                    }
                    else
                    {
                        return Request.CreateResponse<Ativacao_Retorno>(HttpStatusCode.NotAcceptable, new Ativacao_Retorno() { cod_retorno = -1, msg_retorno = "Token Inválido" });
                    }
                }
                else
                {
                    return Request.CreateResponse<Ativacao_Retorno>(HttpStatusCode.NotAcceptable, new Ativacao_Retorno() { cod_retorno = -1, msg_retorno = "Token Inválido" });
                }

                var retorno = new Ativacao_Controle().ativarLicense(ativacao, client);

                if (retorno.cod_retorno == 0)
                    return Request.CreateResponse<Ativacao_Retorno>(HttpStatusCode.OK, retorno);
                else
                    return Request.CreateResponse<Ativacao_Retorno>(HttpStatusCode.NotAcceptable, retorno);
            }
            catch (Exception ex)
            {
                new Ativacao_Controle().log_inserir("Erro ativacao " + ex.Message, (int)Lista_Erro.license_ativation);
                return Request.CreateResponse<Ativacao_Retorno>(HttpStatusCode.BadRequest, new Ativacao_Retorno() { cod_retorno = -1, msg_retorno = "Solicitação não pode ser processada" });
            }
        }

        //// PUT: api/LicenseActivation/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/LicenseActivation/5
        //public void Delete(int id)
        //{
        //}
    }
}
