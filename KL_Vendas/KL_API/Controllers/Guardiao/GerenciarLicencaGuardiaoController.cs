using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using KL_API.Models.Guardiao.Service;
using KL_API.Models.Guardiao;
using System.Web.Http.Description;
using KL_API.Models.GuardiaoDAL;
using Microsoft.Ajax.Utilities;

namespace KL_API.Controllers.New
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class GerenciarLicencaGuardiaoController : ApiController
    {
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<HttpResponseMessage> GerenciarLicencaGuardiao([FromBody] GuardiaoUsuarioRequest body)
        {
            var guardiao_cliente = new GuardiaoClientes();
            var guardiao = new GuardiaoDAL();

            var email = (body?.email ?? string.Empty).Trim().ToLowerInvariant();
            var action = (body?.action ?? string.Empty).Trim(); // mantém como veio; validamos case-insensitive e mapeamos p/ upstream
            var nome = body?.name ?? string.Empty;

            // --- valida token ---
            if (!Request.Headers.Contains("token_acesso"))
                return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { success = false, message = "Token Inválido" });

            var token = Request.Headers.GetValues("token_acesso").FirstOrDefault();
            guardiao_cliente = guardiao.ValidaToken(token);
            if (guardiao_cliente == null || guardiao_cliente.valido == false)
                return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { success = false, message = "Token Inválido" });

            // --- validações ---
            if (string.IsNullOrWhiteSpace(email))
                return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { success = false, message = "email inválido é obrigatório" });

            if (string.IsNullOrWhiteSpace(action))
                return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { success = false, message = "action inválido é obrigatório" });

            if (string.IsNullOrWhiteSpace(nome))
                return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { success = false, message = "nome inválido é obrigatório" });

            string[] allowed = {
                nameof(GuardiaoAction.InviteUser).ToLowerInvariant(),
                nameof(GuardiaoAction.BanUser).ToLowerInvariant(),
                nameof(GuardiaoAction.UnbanUser).ToLowerInvariant()
            };

            if (!allowed.Contains(action.ToLowerInvariant()))
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse 
                {
                    success = false,
                    message = "ação inválida. Possíveis valores: inviteUser, banUser, unbanUser"
                });
            }

            // --- prepara action no formato que o webhook espera (camelCase) ---
            var actionForUpstream = MapActionForUpstream(action);

            // --- chama API externa (POST com query string e body vazio) ---
            var apiClient = new GuardiaoApiClient();
            var upstream = await apiClient.SendActionAsync(email, actionForUpstream, nome);
            var bodyText = await upstream.Content.ReadAsStringAsync();

            string contentType = "";
            if (upstream.Content != null && upstream.Content.Headers != null && upstream.Content.Headers.ContentType != null)
                contentType = upstream.Content.Headers.ContentType.MediaType ?? "";

            // --- atualiza DB local somente se sucesso (pelo body) ---
            if (upstream.IsSuccessStatusCode)
            {
                var guardiaoUsuario = new GuardiaoUsuario
                {
                    guardiao_id_cliente = guardiao_cliente.guardiao_id_cliente,
                    guardiao_email = email,
                    guardiao_status = action.ToLowerInvariant(),
                    guardiao_nome = nome
                };

                var guardiao_usuario = guardiao.GetUsuario(guardiaoUsuario);

                if (guardiao_usuario == null && actionForUpstream == "inviteUser")
                    guardiao.InsereUsuario(guardiaoUsuario);
                else if (guardiao_usuario != null && guardiao_usuario.guardiao_id_usuario > 0 && actionForUpstream != "inviteUser")
                {
                    guardiaoUsuario.guardiao_status = action.ToLowerInvariant();
                    guardiao.AtualizaUsuario(guardiaoUsuario);
                }
            }

            // --- sempre 200 OK com JSON padronizado ---
            return Request.CreateResponse(HttpStatusCode.OK, bodyText);

        }

        public class GuardiaoUsuarioRequest
        {
            public string email { get; set; }
            public string action { get; set; }
            public string name { get; set; }
        }

        public enum GuardiaoAction
        {
            [EnumMember(Value = "inviteuser")]
            InviteUser,

            [EnumMember(Value = "banuser")]
            BanUser,

            [EnumMember(Value = "unbanuser")]
            UnbanUser
        }

        public class ApiResponse
        {
            public bool success { get; set; }
            public string message { get; set; }
        }

        public string MapActionForUpstream(string action)
        {
            if (action == null) return null;
            var a = action.Trim().ToLowerInvariant();
            if (a == "inviteuser") return "inviteUser";
            if (a == "banuser") return "banUser";
            if (a == "unbanuser") return "unbanUser";
            return action; // se já vier no formato correto, deixa passar
        }

    }
}
