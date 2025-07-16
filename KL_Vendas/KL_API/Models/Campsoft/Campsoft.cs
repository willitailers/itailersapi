using KL_API.Controllers.CampSoft.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static KL_API.Controllers.Integracao.IntegracaoLoginController;
using static KL_API.Models.Campsoft.Login;
using static KL_API.Models.Campsoft.ValidationResponse.Success;

namespace KL_API.Models.Campsoft
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; } // Timestamp UNIX
    }

    public class Campsoft
    {
        private static readonly HttpClient client = new HttpClient();
        private static TokenResponse _token;
        public readonly string _tokenItailersCampsoft = "ATd497n08d7n9ah0adwe2";

        private static Dictionary<string, string> produtosDePara = new Dictionary<string, string>
        {
            { "1192001", "urn:sva:kaspersky:standard1" }, // 200
            { "1192002", "urn:sva:kaspersky:standard3" }, // 201
            { "1192003", "urn:sva:kaspersky:plus5" }, // 302
            { "1192004", "urn:sva:kaspersky:plus10" }, // 303
            { "1192005", "urn:sva:kaspersky:smallofficesecurity5" }, // 100
            { "1192006", "urn:sva:kaspersky:smallofficesecurity10" }, // 110
            { "1192007", "urn:sva:kaspersky:smallofficesecurity20" }, // 130
            { "1192008", "urn:sva:kaspersky:smallofficesecurity50" }, // 150
            { "1192009", "urn:sva:kaspersky:safekids" }, // 30"
            { "1192010", "urn:sva:kaspersky:passwordmanager" } // 20"
        };

        public async Task<LoginResponse> LoginAsync(RequestLoginCampSoft requestLoginCampSoft)
        {
            LoginResponse loginResponse = new LoginResponse();

            try
            {
                await EnsureAuthenticatedAsync();

                string validationUrl = "https://api2.campsoft.com.br/svas/v1/validation";

                // Adiciona o token Bearer no header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.AccessToken);

                LoginValidation loginValidation = new LoginValidation
                {
                    isp_identifier = requestLoginCampSoft.isp_identifier,
                    login = requestLoginCampSoft.login,
                    password = requestLoginCampSoft.password
                };

                var json = JsonConvert.SerializeObject(loginValidation);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Chamada para o endpoint de produtos
                var response = await client.PostAsync(validationUrl, content);
                
                loginResponse.token = _tokenItailersCampsoft;

                if (response.IsSuccessStatusCode)
                {
                    Ativacao_Controle ativacao_Controle = new Ativacao_Controle();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    UsuarioResposta usuarioResponse = JsonConvert.DeserializeObject<UsuarioResposta>(responseContent);
                    

                    var campsoft_produtos_ativos =
                        produtosDePara.Where(w => usuarioResponse.products.Any(c => c.code.Contains(w.Key) && c.status == "active")).ToList();

                    var campsoft_produtos_inativos =
                        produtosDePara.Where(w => usuarioResponse.products.Any(c => c.code.Contains(w.Key) && c.status != "active")).ToList();

                    loginResponse.access = campsoft_produtos_ativos != null && campsoft_produtos_ativos.Any();

                    if (!loginResponse.access)
                    {
                        loginResponse.message = "Este usuário não possui produtos Kaspersky autorizados.";
                    }

                    ClientInfo clientInfo = ativacao_Controle.ValidaToken(_tokenItailersCampsoft);

                    UserAdd userAdd = new UserAdd()
                    {
                        Email = usuarioResponse.email.data,
                        UserID = usuarioResponse.userkey,
                        StartDate = DateTime.Now
                    };

                    loginResponse.user_id = userAdd.UserID;

                    var dt_licencas = ativacao_Controle.seleciona_licenca_produto(clientInfo.id_cliente, userAdd.UserID, "all");
                    var urns_ativados = dt_licencas
                        .AsEnumerable()
                        .Where(w => w["dt_cancelamento"] == null || w["dt_cancelamento"].ToString() == "")
                        .Select(s => s["nm_urn"]).ToList();

                    var produtos_ativar = campsoft_produtos_ativos.Select(s => s.Value).ToList();

                    if (produtos_ativar.Any()) { CampsoftAtivacao(clientInfo, produtos_ativar, userAdd, dt_licencas); }

                    if (campsoft_produtos_ativos.Any(campsoft_urn_ativo => urns_ativados.Contains(campsoft_urn_ativo)))
                    {
                        UserDelete userDelete = new UserDelete()
                        {
                            UserID = userAdd.UserID
                        };

                        ativacao_Controle.deleteUser(userDelete, clientInfo);

                        loginResponse.message = "Este usuário não possui produtos Kaspersky autorizados.";
                        loginResponse.access = false;
                        loginResponse.token = string.Empty;
                    }
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ValidationResponse.ErroResposta erroUsuarioResponse = JsonConvert.DeserializeObject<ValidationResponse.ErroResposta>(responseContent);
                    loginResponse.message = erroUsuarioResponse.message;
                    loginResponse.access = false;
                }

                return loginResponse;
            }
            catch (Exception)
            {
                loginResponse.message = "Ops! Algo deu errado no login. Por favor, tente novamente ou contate o administrador do sistema.";
                loginResponse.access = false;
                return loginResponse;
            }
        }

        public async Task<Isps> GetIspsAsync()
        {
            await EnsureAuthenticatedAsync();

            string ispsUrl = "https://api2.campsoft.com.br/svas/v1/isps";

            // Adiciona o token Bearer no header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.AccessToken);

            // Chamada para o endpoint de produtos
            var ispsResponse = await client.GetAsync(ispsUrl);

            if (ispsResponse.IsSuccessStatusCode)
            {
                var ispsJson = await ispsResponse.Content.ReadAsStringAsync();
                Isps isps = JsonConvert.DeserializeObject<Isps>(ispsJson);
                return isps;
            }
            else
            {
                return null;
            }
        }

        public Produto_Retorno GetProdutos(ClientInfo clientInfo, string user_id)
        {
            Produto_Retorno produtos_retorno = new Produto_Retorno();

            try
            {
                Ativacao_Controle ativacao_Controle = new Ativacao_Controle();
                var dt_licencas = ativacao_Controle.seleciona_licenca_produto(clientInfo.id_cliente, user_id, "all");

                var licencas_ativas = dt_licencas
                    .AsEnumerable()
                    .Where(w => w["dt_cancelamento"] == null || w["dt_cancelamento"].ToString() == "")
                    .ToList();
                
                foreach (var item in licencas_ativas)
                { 
                    Produtos produtos = new Produtos()
                    {
                        chave_ativacao = item["cd_ativacao_kl"].ToString(),
                        nm_produto = item["nm_produto_kl"].ToString(),
                        imagem_produto = item["imagem_produto"].ToString(),
                        descricao = item["descricao"].ToString(),
                        dt_ativacao = item["dt_ativacao"].ToString(),
                    };

                    produtos_retorno.produtos.Add(produtos);
                }

                return produtos_retorno;
            }
            catch (Exception)
            {
                produtos_retorno.produtos = null;
                produtos_retorno.message = "Não foi possível carregar a lista de produtos no momento. Tente novamente mais tarde ou contate o administrador do sistema.";
                return produtos_retorno;
            }
        }

        public void CampsoftAtivacao(ClientInfo clientInfo, List<string> produtosAtivar, UserAdd userAdd, DataTable dt_licencas)
        {
            Ativacao_Controle ativacao_Controle = new Ativacao_Controle();
            var dt_usuario = ativacao_Controle.seleciona_cliente_usuario(clientInfo.id_cliente, userAdd.UserID);

            if (!dt_usuario.AsEnumerable().Any())
            {
                dt_usuario = new Ativacao_Controle().addUser(userAdd, clientInfo);
            }

            List<Produto_UserAdd> lista_produtos_ativar = new List<Produto_UserAdd>();

            var urns_ativados = dt_licencas
                .AsEnumerable()
                .Where(w => w["dt_cancelamento"] == null || w["dt_cancelamento"].ToString() == "")
                .Select(s => s["nm_urn"]).ToList();
            
            var urns_ativar = produtosAtivar
                .Where(w => !urns_ativados.Contains(w))
                .ToList();

            foreach (var produto in urns_ativar)
            {
                Produto_UserAdd produto_UserAdd = new Produto_UserAdd()
                {
                    ProductID = produto,
                    Qtd = 1
                };

                lista_produtos_ativar.Add(produto_UserAdd);
            }

            if (lista_produtos_ativar.Any())
            {
                Activation activation = new Activation()
                {
                    Email = userAdd.Email,
                    UserID = userAdd.UserID,
                    Products = lista_produtos_ativar
                };

                var retorno = new Ativacao_Controle().LicenseActivation(activation, clientInfo);
            }
        }

        private static async Task EnsureAuthenticatedAsync()
        {
            if (_token == null || IsTokenExpired())
            {
                await AuthenticateAsync();
            }
        }

        private static bool IsTokenExpired()
        {
            // Converte o timestamp UNIX para DateTime
            DateTime expireDate = DateTimeOffset.FromUnixTimeSeconds(_token.ExpiresIn).UtcDateTime;
            return expireDate <= DateTime.UtcNow;
        }

        private static async Task AuthenticateAsync()
        {
            string authUrl = "https://api2.campsoft.com.br/svas/v1/authentication";

            var authData = new
            {
                ApiKey = "1465347",
                ApiSecret = "8dde25b56eb097f8316796bcba6ee07a"
            };

            var content = new StringContent(JsonConvert.SerializeObject(authData), Encoding.UTF8, "application/json");

            var authResponse = await client.PostAsync(authUrl, content);

            if (!authResponse.IsSuccessStatusCode)
            {
                throw new Exception("Falha na autenticação");
            }

            var authResult = await authResponse.Content.ReadAsStringAsync();
            _token = JsonConvert.DeserializeObject<TokenResponse>(authResult);

            Console.WriteLine($"Novo Token: {_token.AccessToken}");
            Console.WriteLine($"Expira em: {DateTimeOffset.FromUnixTimeSeconds(_token.ExpiresIn).UtcDateTime}");
        }
    }
}