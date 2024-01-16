using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace KL_API.Controllers.Integracao
{
    public class IntegracaoLoginController : ApiController
    {
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage Post([FromBody] LoginRequest login)
        {       
            if (!Request.Headers.Contains("kl-token"))
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, new LoginResponse() { msg_retorno = "Token é obrigatório" });

            if (login.email is null || login.email == "")
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, new LoginResponse() { msg_retorno = "email é obrigatório" });

            if (login.cpf_cnpj is null || login.cpf_cnpj == "")
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, new LoginResponse() { msg_retorno = "cpf_cnpj é obrigatório" });

            string token = Request.Headers.GetValues("kl-token").First();

            Models.Integracao.Integracao integracao = new Models.Integracao.Integracao();

            var usuarioLogado = integracao.RetornaIntegracaoLogin(login.email, login.cpf_cnpj);

            LoginResponse loginResponse = new LoginResponse();
            if (usuarioLogado.Rows.Count > 0 && usuarioLogado.Rows[0]["email"].ToString() == login.email && usuarioLogado.Rows[0]["cpf_cnpj"].ToString() == login.cpf_cnpj)
            {
                loginResponse.autenticado = true;
                loginResponse.id_cliente = usuarioLogado.Rows[0]["id"].ToString();
            }

            return Request.CreateResponse(HttpStatusCode.OK, loginResponse);
        }

        public class LoginRequest
        {
            public string email { get; set; }
            public string cpf_cnpj { get; set; }
        }

        public class LoginResponse
        {
            public string id_cliente { get; set; }
            public bool autenticado { get; set; }
            public string msg_retorno { get; set; }
        }
    }
}