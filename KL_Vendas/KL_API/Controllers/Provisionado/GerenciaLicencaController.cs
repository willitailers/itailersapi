using KL_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace KL_API.Controllers.Provisionado
{
    public class Request
    {
        public List<string> users_resume { get; set; }
        public List<string> users_stop { get; set; }
        public List<string> users_cancel { get; set; }

    }

    public class Response
    {
        public List<UserValidation> users_resume { get; set; }
        public List<UserValidation> users_stop { get; set; }
        public List<UserValidation> users_cancel { get; set; }

        public Response()
        {
            users_resume = new List<UserValidation>();
            users_stop = new List<UserValidation>();
            users_cancel = new List<UserValidation>();
        }
    }

    public class UserValidation
    {
        public string user_login { get; set; }
        public List<string> mensagens { get; set; }

        public UserValidation()
        {
            mensagens = new List<string>();
        }
    }

    public class GerenciaLicencaController : ApiController
    {
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage GerenciaLicenca([FromBody] Request request)
        {
            if (request is null || (request.users_resume.Count == 0 && request.users_stop.Count == 0 && request.users_cancel.Count == 0))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "É necessario informar pelo menos um User Login.");

            if (!Request.Headers.Contains("kl-token"))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "O cabeçalho 'kl-token' é obrigatório.");

            int count = request.users_resume.Count + request.users_stop.Count + request.users_cancel.Count;
            if (count > 250)
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"A quantidade de usuários foi excedida. Quantidade enviada: {count}. Quantidade máxima: 250.");

            string klTokenHeaderValue = Request.Headers.GetValues("kl-token").FirstOrDefault();

            ClientInfo clientInfo = new Ativacao_Controle().ValidaToken(klTokenHeaderValue);

            if (clientInfo is null || clientInfo.valido is false)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "'kl-token' é inválido.");

            try
            {
                Response response = new Ativacao_Controle().GerenciaLicenca(request, clientInfo);
                return Request.CreateResponse<Response>(HttpStatusCode.OK, response);
            }
            catch
            {
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, "Ocorreu um erro ao processar o gerenciamento de licenças.");
            }
        }
    }
}
