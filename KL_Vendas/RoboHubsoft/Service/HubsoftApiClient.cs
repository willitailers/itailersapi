using Newtonsoft.Json;
using RoboHubsoft.Models;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace RoboHubsoft.Service
{
    public class HubsoftApiClient
    {
        private const string AuthUrl = "https://api.regionaltelecom.hubsoft.com.br/oauth/token";
        private const string ClientsBaseUrl = "https://api.regionaltelecom.hubsoft.com.br/api/v1/integracao/cliente/todos?itens_por_pagina=500&data_inicio=&data_fim=&cancelado=nao&possui_pacote=&codigo_pacote=&servico_status=&relacoes=&grupo_cliente_servico&aguardando_migracao";

        // Variáveis para cache do token
        private string _cachedAccessToken;
        private DateTime _tokenExpiration;

        /// <summary>
        /// Realiza o POST para obter um novo Access Token (Bearer) e armazena em cache.
        /// </summary>
        private async Task<string> RequestNewAccessToken(RoboHubsoft.Models.t_hubsoft_cliente cliente)
        {
            using (var client = new HttpClient())
            {
                var requestData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("client_id", cliente.client_id.ToString()),
                    new KeyValuePair<string, string>("client_secret", cliente.client_secret),
                    new KeyValuePair<string, string>("username", cliente.username),
                    new KeyValuePair<string, string>("password", cliente.password),
                    new KeyValuePair<string, string>("grant_type", "password")
                };

                using (var content = new FormUrlEncodedContent(requestData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                    var response = await client.PostAsync(AuthUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var tokenResponse = JsonConvert.DeserializeObject<OAuthTokenResponse>(json);

                        // Armazena o token e calcula o tempo de expiração
                        _cachedAccessToken = tokenResponse.AccessToken;
                        // O expires_in é em segundos. Subtrai 60 segundos para margem de segurança.
                        _tokenExpiration = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn).AddSeconds(-60);

                        return _cachedAccessToken;
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        throw new HttpRequestException($"Falha na autenticação. Status: {response.StatusCode}. Conteúdo: {errorContent}");
                    }
                }
            }
        }

        /// <summary>
        /// Verifica se o token em cache é válido.
        /// </summary>
        private bool IsTokenValid()
        {
            // Considera o token inválido se for nulo ou se o tempo de expiração for menor que o tempo atual
            // Subtrai 60 segundos (1 minuto) para ter uma margem de segurança antes da expiração real.
            return !string.IsNullOrEmpty(_cachedAccessToken) && _tokenExpiration > DateTime.Now.AddSeconds(60);
        }

        /// <summary>
        /// Obtém o Access Token do cache ou solicita um novo se expirado.
        /// </summary>
        public async Task<string> GetAccessToken(RoboHubsoft.Models.t_hubsoft_cliente cliente)
        {
            if (IsTokenValid())
            {
                return _cachedAccessToken;
            }

            return await RequestNewAccessToken(cliente);
        }

        /// <summary>
        /// Realiza o GET para a API de Clientes em uma página específica.
        /// </summary>
        public async Task<ClientesResponse> GetClientesPage(int page, RoboHubsoft.Models.t_hubsoft_cliente cliente)
        {
            string accessToken = await GetAccessToken(cliente);

            // Constrói a URL com o parâmetro de página
            var url = $"{ClientsBaseUrl}&pagina={page}";

            using (var client = new HttpClient())
            {
                // Adiciona o cabeçalho de Autorização Bearer
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ClientesResponse>(json);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Falha ao buscar clientes na página {page}. Status: {response.StatusCode}. Conteúdo: {errorContent}");
                }
            }
        }

        /// <summary>
        /// Busca todos os clientes, iterando por todas as páginas.
        /// </summary>
        public async Task<List<Cliente>> GetAllClientes(RoboHubsoft.Models.t_hubsoft_cliente cliente)
        {
            var allClientes = new List<Cliente>();
            int currentPage = 1;
            int totalPages = 1; // Inicializa com 1 para garantir a primeira execução

            try
            {
                while (currentPage <= totalPages)
                {
                    Console.WriteLine($"[Cliente {cliente.id}] Buscando dados - pagina {currentPage} de {totalPages} paginas.");
                    ClientesResponse response = await GetClientesPage(currentPage, cliente);

                    if (response?.Clientes != null)
                    {
                        allClientes.AddRange(response.Clientes);
                    }

                    // Atualiza o total de páginas após a primeira requisição
                    if (response?.Paginacao != null)
                    {
                        totalPages = response.Paginacao.UltimaPagina;
                    }

                    currentPage++;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"currentPage:{currentPage}, totalPages:{totalPages}, totalClientesBaixados:{allClientes.Count}, Ex. Message: {ex.Message}");
            }

            return allClientes;
        }
    }

    // Classes para o POST de autenticação (OAuth Token)
    public class OAuthTokenRequest
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }
    }

    public class OAuthTokenResponse
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }

    public class MaterialColor
    {
        [JsonProperty("palette")]
        public string Palette { get; set; }

        [JsonProperty("hue")]
        public string Hue { get; set; }

        [JsonProperty("class")]
        public string Class { get; set; }

        [JsonProperty("bgColorValue")]
        public string BgColorValue { get; set; }

        [JsonProperty("fgColorValue")]
        public string FgColorValue { get; set; }

        [JsonProperty("style")]
        public Dictionary<string, string> Style { get; set; }
    }

    public class Pivot
    {
        [JsonProperty("id_cliente")]
        public int IdCliente { get; set; }

        [JsonProperty("id_grupo_cliente")]
        public int IdGrupoCliente { get; set; }
    }

    public class Grupo
    {
        [JsonProperty("id_grupo_cliente")]
        public int IdGrupoCliente { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("data_cadastro")]
        public string DataCadastro { get; set; }

        [JsonProperty("ativo")]
        public bool Ativo { get; set; }

        [JsonProperty("material_color")]
        public MaterialColor MaterialColor { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("pivot")]
        public Pivot Pivot { get; set; }
    }

    public class Vendedor
    {
        [JsonProperty("id_vendedor")]
        public int? IdVendedor { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class Servico
    {
        [JsonProperty("id_cliente_servico")]
        public int IdClienteServico { get; set; }

        [JsonProperty("uuid_cliente_servico")]
        public string UuidClienteServico { get; set; }

        [JsonProperty("id_servico")]
        public int IdServico { get; set; }

        [JsonProperty("numero_plano")]
        public int NumeroPlano { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("valor")]
        public decimal Valor { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("status_prefixo")]
        public string StatusPrefixo { get; set; }

        [JsonProperty("tecnologia")]
        public string Tecnologia { get; set; }

        [JsonProperty("velocidade_download")]
        public string VelocidadeDownload { get; set; }

        [JsonProperty("velocidade_upload")]
        public string VelocidadeUpload { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("senha")]
        public string Senha { get; set; }

        [JsonProperty("referencia")]
        public string Referencia { get; set; }

        [JsonProperty("ipv4")]
        public string Ipv4 { get; set; }

        [JsonProperty("ipv6")]
        public string Ipv6 { get; set; }

        [JsonProperty("mac_addr")]
        public string MacAddr { get; set; }

        [JsonProperty("phy_addr")]
        public string PhyAddr { get; set; }

        [JsonProperty("vlan")]
        public string Vlan { get; set; }

        [JsonProperty("observacoes_autenticacao")]
        public string ObservacoesAutenticacao { get; set; }

        [JsonProperty("id_motivo_cancelamento")]
        public int? IdMotivoCancelamento { get; set; }

        [JsonProperty("data_cancelamento")]
        public string DataCancelamento { get; set; }

        [JsonProperty("motivo_cancelamento")]
        public string MotivoCancelamento { get; set; }

        [JsonProperty("motivo_cancelamento_prefixo")]
        public string MotivoCancelamentoPrefixo { get; set; }

        [JsonProperty("anotacoes")]
        public string Anotacoes { get; set; }

        [JsonProperty("data_cadastro")]
        public string DataCadastro { get; set; }

        [JsonProperty("data_habilitacao")]
        public string DataHabilitacao { get; set; }

        [JsonProperty("data_habilitacao_br")]
        public string DataHabilitacaoBr { get; set; }

        [JsonProperty("data_venda")]
        public string DataVenda { get; set; }

        [JsonProperty("data_atualizacao")]
        public string DataAtualizacao { get; set; }

        [JsonProperty("id_cliente_servico_antigo")]
        public int? IdClienteServicoAntigo { get; set; }

        [JsonProperty("id_prospecto")]
        public int? IdProspecto { get; set; }

        [JsonProperty("vendedor")]
        public Vendedor Vendedor { get; set; }
    }

    public class Cliente
    {
        [JsonProperty("id_cliente")]
        public int IdCliente { get; set; }

        [JsonProperty("uuid_cliente")]
        public string UuidCliente { get; set; }

        [JsonProperty("codigo_cliente")]
        public int CodigoCliente { get; set; }

        [JsonProperty("nome_razaosocial")]
        public string NomeRazaoSocial { get; set; }

        [JsonProperty("nome_fantasia")]
        public string NomeFantasia { get; set; }

        [JsonProperty("tipo_pessoa")]
        public string TipoPessoa { get; set; }

        [JsonProperty("cpf_cnpj")]
        public string CpfCnpj { get; set; }

        [JsonProperty("telefone_primario")]
        public string TelefonePrimario { get; set; }

        [JsonProperty("telefone_secundario")]
        public string TelefoneSecundario { get; set; }

        [JsonProperty("telefone_terciario")]
        public string TelefoneTerciario { get; set; }

        [JsonProperty("email_principal")]
        public string EmailPrincipal { get; set; }

        [JsonProperty("email_secundario")]
        public string EmailSecundario { get; set; }

        [JsonProperty("rg")]
        public string Rg { get; set; }

        [JsonProperty("rg_emissao")]
        public string RgEmissao { get; set; }

        [JsonProperty("inscricao_municipal")]
        public string InscricaoMunicipal { get; set; }

        [JsonProperty("inscricao_estadual")]
        public string InscricaoEstadual { get; set; }

        [JsonProperty("data_cadastro")]
        public string DataCadastro { get; set; }

        [JsonProperty("data_nascimento")]
        public string DataNascimento { get; set; }

        [JsonProperty("grupos")]
        public List<Grupo> Grupos { get; set; }

        [JsonProperty("ativo")]
        public bool Ativo { get; set; }

        [JsonProperty("origem_cliente")]
        public string OrigemCliente { get; set; }

        [JsonProperty("motivo_contratacao")]
        public string MotivoContratacao { get; set; }

        [JsonProperty("id_externo")]
        public string IdExterno { get; set; }

        [JsonProperty("data_atualizacao")]
        public string DataAtualizacao { get; set; }

        [JsonProperty("nome_pai")]
        public string NomePai { get; set; }

        [JsonProperty("nome_mae")]
        public string NomeMae { get; set; }

        [JsonProperty("estado_civil")]
        public string EstadoCivil { get; set; }

        [JsonProperty("genero")]
        public string Genero { get; set; }

        [JsonProperty("nacionalidade")]
        public string Nacionalidade { get; set; }

        [JsonProperty("profissao")]
        public string Profissao { get; set; }

        [JsonProperty("servicos")]
        public List<Servico> Servicos { get; set; }
    }

    public class Paginacao
    {
        [JsonProperty("primeira_pagina")]
        public int PrimeiraPagina { get; set; }

        [JsonProperty("ultima_pagina")]
        public int UltimaPagina { get; set; }

        [JsonProperty("pagina_atual")]
        public int PaginaAtual { get; set; }

        [JsonProperty("total_registros")]
        public int TotalRegistros { get; set; }
    }

    public class ClientesResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("paginacao")]
        public Paginacao Paginacao { get; set; }

        [JsonProperty("clientes")]
        public List<Cliente> Clientes { get; set; }
    }
}
