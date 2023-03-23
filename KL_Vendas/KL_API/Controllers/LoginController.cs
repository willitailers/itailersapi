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
    public class LoginController : ApiController
    {
        //GET: api/Login
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Login/5
        /// <summary>
        /// Ativação de licença KL
        /// </summary>
        /// <param name="ativacao">Objeto de ativação KL</param>
        /// <response code="200">OK</response>
        /// <response code="406">Informações incorretas</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Erro ao realizar o processo</response>
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Login
        /// <summary>
        /// Inclusão e usuario e ativação da licenca
        /// </summary>
        /// <param name="ativacao">Objeto de ativação KL</param>
        /// <response code="200">OK</response>
        /// <response code="406">Informações incorretas</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Erro ao realizar o processo</response>
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Login login)
        {
            var clientInfo = new ClientInfo();
            if (Request.Headers.Contains("kl-token"))
            {
                string token = Request.Headers.GetValues("kl-token").First();
                clientInfo = new Ativacao_Controle().ValidaToken(token);
                if (clientInfo.valido)
                {
                    var provisionamento = new Ativacao_Controle().Retorna_provisionamento(0);

                    if (provisionamento.Rows.Count <= 5)
                    {
                        new Ativacao_Controle().Provisionar(clientInfo, provisionamento);
                    }

                    LoginRetorno loginRetorno = new Ativacao_Controle().login(login, clientInfo);

                    if (!loginRetorno.autenticado)
                    {
                        return Request.CreateResponse<LoginRetorno>(HttpStatusCode.NotAcceptable, loginRetorno);
                    }

                    return Request.CreateResponse<LoginRetorno>(HttpStatusCode.OK, loginRetorno);
                }
                else
                {
                    return Request.CreateResponse<LoginRetorno>(HttpStatusCode.NotAcceptable, new LoginRetorno() { cod_retorno = -1, msg_retorno = "Token Inválido" });
                }
            }
            else
            {
                return Request.CreateResponse<LoginRetorno>(HttpStatusCode.NotAcceptable, new LoginRetorno() { cod_retorno = -1, msg_retorno = "Token é obrigatório" });
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
