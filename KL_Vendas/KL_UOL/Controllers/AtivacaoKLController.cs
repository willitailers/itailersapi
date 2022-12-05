using KL_UOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KL_UOL.Controllers
{
    public class AtivacaoKLController : ApiController
    {
        // POST: api/Ativacao
        /// <summary>
        /// Ativação de licença KL
        /// </summary>
        /// <param name="ativacao">Objeto de ativação KL</param>
        /// <response code="200">Sucesso na ativação</response>
        /// <response code="406">Informações incorretas</response>
        /// <response code="400">Bad Request</response>
        public HttpResponseMessage Post([FromBody]Ativacao_KL ativacao)
        {
            try
            {
                var retorno = new Ativacao_Controle().ativarKL(ativacao);
                
                if (retorno.cod_retorno == 0)
                    return Request.CreateResponse<Ativacao_Retorno>(HttpStatusCode.OK, retorno);
                else
                    return Request.CreateResponse<Ativacao_Retorno>(HttpStatusCode.NotAcceptable, retorno);
            }
            catch(Exception ex)
            {
                new Ativacao_Controle().log_inserir("Erro ativacao " + ex.Message, "12");
                return Request.CreateResponse<Ativacao_Retorno>(HttpStatusCode.BadRequest, new Ativacao_Retorno() { cod_retorno = -1, msg_retorno = "Solicitação não pode ser processada"});
            }
        }
    }
}