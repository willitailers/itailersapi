using KL_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace KL_API.Controllers.New
{
    public class HardCancelInternoController : ApiController
    {
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage Post([FromBody] RequestInterno requestInterno)
        {
            try
            {
                var retorno = new Ativacao_Controle().HardCancelInterno(requestInterno);
                return Request.CreateResponse<string>(HttpStatusCode.OK, retorno.msg_retorno);
            }
            catch (Exception ex)
            {
                new Ativacao_Controle().log_inserir("INTERNO - HARD CANCEL - ERRO " + ex.Message, (int)Lista_Erro.user_delete);
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Solicitação não pode ser processada | INTERNO - HARD CANCEL - ERRO");
            }
        }
    }
}
