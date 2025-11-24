using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using KL_API.Models.Kiddlepass;
using KL_API.Models.Kiddlepass.Service;
using System.Text.Json;

namespace KL_API.Controllers.Kiddlepass
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class GetUserKiddlepassController : ApiController
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<HttpResponseMessage> GetUserKiddlepass(string userid)
        {
            var kiddle_cliente = new KiddlepassCliente();
            var kiddlepass = new KiddlepassDAL();

            // --- valida token ---
            if (!Request.Headers.Contains("token_acesso"))
                return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { success = false, message = "Token Inválido" });

            var token = Request.Headers.GetValues("token_acesso").FirstOrDefault();

            kiddle_cliente = kiddlepass.ValidaToken(token);
            if (kiddle_cliente == null || kiddle_cliente.valido == false)
                return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { success = false, message = "Token Inválido" });

            // --- validações ---
            if (string.IsNullOrWhiteSpace(userid))
                return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { success = false, message = "userId é obrigatório" });

            var kiddlepassUsuario = new KiddlepassUsuario
            {
                userid = userid,
                id_cliente = kiddle_cliente.kiddlepass_id_cliente
            };

            var kiddle_user = kiddlepass.GetUsuario(kiddlepassUsuario);

            var updateUserKiddlepassResponse = new UserKiddlepassResponse
            {
                email = kiddle_user.email,
                name = kiddle_user.name,
                status = kiddle_user.status,
                userid = kiddle_user.userid
            };

            if (kiddle_user != null)
                return Request.CreateResponse(HttpStatusCode.OK, updateUserKiddlepassResponse);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ApiResponse { success = false, message = "Ocorreu um erro ao tentar retornar o usuario" });
        }

        public class ApiResponse
        {
            public bool success { get; set; }
            public string message { get; set; }
        }

        public sealed class KiddlePassUpdateRequest
        {
            public string userId { get; set; }
        }
    }
}
