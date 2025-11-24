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
    public class GetAllUsersKiddlepassController : ApiController
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<HttpResponseMessage> GetAllUsersKiddlepass()
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

            var kiddlepassUsuario = new KiddlepassUsuario
            {
                id_cliente = kiddle_cliente.kiddlepass_id_cliente
            };

            var kiddle_users = kiddlepass.GetAllUsuario(kiddlepassUsuario);

            if (kiddle_users.Count > 0)
                return Request.CreateResponse(HttpStatusCode.OK, kiddle_users);
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
