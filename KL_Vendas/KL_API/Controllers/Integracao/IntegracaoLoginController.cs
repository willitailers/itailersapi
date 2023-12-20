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

            string token = Request.Headers.GetValues("kl-token").First();

            LoginResponse loginResponse = new LoginResponse()
            {
                autenticado = true,
                id_cliente = "16"
            };

            return Request.CreateResponse(HttpStatusCode.OK, loginResponse);
        }

        public class LoginRequest
        {
            public string email { get; set; }
            public string cpf_cpnj { get; set; }
        }

        public class LoginResponse
        {
            public string id_cliente { get; set; }
            public bool autenticado { get; set; }
            public string msg_retorno { get; set; }
        }
    }
}