using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using KL_API.Models.Kiddlepass;
using KL_API.Models.Kiddlepass.Service;
using System.Text.Json;

namespace KL_API.Controllers.Kiddlepass
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class InsertUserKiddlepassController : ApiController
    {
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<HttpResponseMessage> InsertUserKiddlepass([FromBody] KiddlePassInsertRequest body)
        {
            var kiddle_cliente = new KiddlepassCliente();
            var kiddlepass = new KiddlepassDAL();

            var userId = (body?.userId ?? string.Empty).Trim();
            var email = (body?.email ?? string.Empty).Trim().ToLowerInvariant();
            var name = body?.name ?? string.Empty;
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

            if (string.IsNullOrWhiteSpace(email))
                return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { success = false, message = "email é obrigatório" });

            if (string.IsNullOrWhiteSpace(name))
                return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { success = false, message = "name inválido é obrigatório" });

            // --- Chama API externa
            var apiClient = new KiddlepassApiClient();
            var upstream = await apiClient.PostInsertUserAsync(userId, kiddle_cliente.kiddlepass_id_cliente.ToString(), productType, email , name);
            var bodyText = await upstream.Content.ReadAsStringAsync();

            var kiddlepassUsuario = new KiddlepassUsuario
            {
                userid = userId,
                name = name,
                email = email,
                product_type = productType,
                id_cliente = kiddle_cliente.kiddlepass_id_cliente,
                status = "active"
            };

            var insertUserKiddlepassResponse = new InsertUserKiddlepassResponse
            {
                userid = userId,
                name = name,
                email = email,
                status = "active"
            };

            // --- atualiza DB local somente se sucesso (pelo body) ---
            if (upstream.IsSuccessStatusCode)
            {
                //Inserir usuario no banco de dados
                try
                {
                    kiddlepass.InsertUser(kiddlepassUsuario);
                }
                catch (System.Exception ex)
                {
                    kiddlepass.log_inserir(ex.Message, JsonSerializer.Serialize(kiddlepassUsuario));
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro ao tentar inserir um novo usuario");
                }
            }

            // --- sempre 200 OK com JSON padronizado ---
            return Request.CreateResponse(HttpStatusCode.OK, insertUserKiddlepassResponse);
        }

        public class ApiResponse
        {
            public bool success { get; set; }
            public string message { get; set; }
        }

        public sealed class KiddlePassInsertRequest
        {
            public string userId { get; set; }
            public string name { get; set; }
            public string email { get; set; }
        }

        private class InsertUserKiddlepassResponse
        {
            public string userid { get; set; }
            public string status { get; set; }
            public string email { get; set; }
            public string name { get; set; }
        }
    }
}
