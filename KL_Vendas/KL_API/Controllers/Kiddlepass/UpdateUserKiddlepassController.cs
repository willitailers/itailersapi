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
    public class UpdateUserKiddlepassController : ApiController
    {
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<HttpResponseMessage> UpdateUserKiddlepass([FromBody] KiddlePassUpdateRequest body)
        {
            var kiddle_cliente = new KiddlepassCliente();
            var kiddlepass = new KiddlepassDAL();

            var userId = (body?.userId ?? string.Empty).Trim();
            var action = body?.action ?? string.Empty;
            var productType = "default";

            // --- valida token ---
            if (!Request.Headers.Contains("token_acesso"))
                return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { success = false, message = "Token Inválido" });

            var token = Request.Headers.GetValues("token_acesso").FirstOrDefault();

            kiddle_cliente = kiddlepass.ValidaToken(token);
            if (kiddle_cliente == null || kiddle_cliente.valido == false)
                return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { success = false, message = "Token Inválido" });

            // --- validações ---
            if (string.IsNullOrWhiteSpace(userId))
                return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { success = false, message = "userId é obrigatório" });

            if (string.IsNullOrWhiteSpace(action))
                return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { success = false, message = "action é obrigatório" });

            string status = string.Empty;

            if (action == "reactivate")
                status = "active";
            else if (action == "suspend")
                status = "suspended";
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, new ApiResponse { success = false, message = @"Falha ao localizar action, utilize reactivate ou suspend" });

            // --- Chama API externa
            var apiClient = new KiddlepassApiClient();
            var upstream = await apiClient.PutUpdateUserAsync(userId, action, kiddle_cliente.kiddlepass_id_cliente.ToString(), productType);
            var bodyText = await upstream.Content.ReadAsStringAsync();

            var kiddlepassUsuario = new KiddlepassUsuario
            {
                userid = userId,
                product_type = productType,
                id_cliente = kiddle_cliente.kiddlepass_id_cliente,
                status = status
            };

            // --- atualiza DB local somente se sucesso ---
            if (upstream.IsSuccessStatusCode)
            {
                try
                {
                    kiddlepass.UpdateUser(kiddlepassUsuario);
                }
                catch (System.Exception ex)
                {
                    kiddlepass.log_inserir(ex.Message, JsonSerializer.Serialize(kiddlepassUsuario));
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro ao tentar atualizar o usuario");
                }
            }

            var kiddle_user = kiddlepass.GetUsuario(kiddlepassUsuario);

            var updateUserKiddlepassResponse = new UpdateUserKiddlepassResponse
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
            public string action { get; set; }
        }

        private class UpdateUserKiddlepassResponse
        {
            public string userid { get; set; }
            public string status { get; set; }
            public string email { get; set; }
            public string name { get; set; }
        }
    }
}
