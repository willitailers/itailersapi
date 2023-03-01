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
            string id_cliente_usuario = "";
            try
            {
                LoginRetorno loginRetorno = new Ativacao_Controle().login(login);

                return Request.CreateResponse<LoginRetorno>(HttpStatusCode.OK, loginRetorno);
            }
            catch (Exception ex)
            {
                if (id_cliente_usuario != "")
                    new Ativacao_Controle().ClienteDeletar(id_cliente_usuario, 1);
                new Ativacao_Controle().log_inserir("Erro login " + ex.Message, (int)Lista_Erro.login);
                return Request.CreateResponse<LoginRetorno>(HttpStatusCode.BadRequest, new LoginRetorno() { cod_retorno = -1, msg_retorno = "Solicitação não pode ser processada" });
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
