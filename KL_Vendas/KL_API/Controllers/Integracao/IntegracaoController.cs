using KL_API.Models.Integracao.cliente_contrato;
using KL_API.Models.Integracao;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using KL_API.Models.Integracao.cliente;
using System.Data;
using System.Web.Http.Description;
using KL_API.Models.Integracao.Entidades;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.Win32;
using System.Xml;

namespace KL_API.Controllers.Integracao
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class IntegracaoController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
           Models.Integracao.Integracao integracao = new Models.Integracao.Integracao();
           var integracao_clientes = integracao.RetornaIntegracaoClientes();

            RetornoIntegracao retornoIntegracao = new RetornoIntegracao();

            foreach (DataRow row in integracao_clientes.Rows)
            {
                string username = row["usuario_itailers"].ToString();
                string password = row["password_itailers"].ToString();
                string base_url = row["url"].ToString();
                string cliente_name = row["cliente"].ToString();
                string id_cliente = row["id"].ToString();

                string base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

                var relacao_produtos = integracao.RetornaIntegracaoRelacaoProdutos(id_cliente);

                if (relacao_produtos.Rows.Count == 0)
                {
                    continue;
                }

                List<t_integracao_relacao_produto> listProdutos = new List<t_integracao_relacao_produto>();
                foreach (DataRow produto in relacao_produtos.Rows)
                {
                    t_integracao_relacao_produto t_integracao_relacao_produto = new t_integracao_relacao_produto()
                    {
                        id = produto["id"].ToString(),
                        id_cliente = produto["id_cliente"].ToString(),
                        id_produto_cliente = produto["id_produto_cliente"].ToString(),
                        id_produto_kl = produto["id_produto_kl"].ToString()
                    };

                    view_vd_contratos_produtos_gen contratos = await GetContratos(base_url, base64Credentials, t_integracao_relacao_produto.id_produto_cliente);

                    if (!string.IsNullOrEmpty(contratos.message))
                    {
                        integracao.InsereIntegracaoLog(contratos.message);
                    }

                    if (contratos.rows is null || contratos.rows.Count == 0)
                    {
                        continue;
                    }

                    var cells = contratos.rows.Select(r => r.cell).ToList();
                    var id_contratos = cells.Select(s => s[12]).ToList();
                    string id_contratos_join = string.Join(",", id_contratos);

                    cliente_contrato cliente_contrato = await GetClienteContratos(base_url, base64Credentials, id_contratos_join);

                    if (cliente_contrato.registros.Count == 0)
                    {
                        continue;
                    }

                    string ids_clientes = string.Join(",", cliente_contrato.registros.Select(s => s.id_cliente));

                    cliente cliente_erp = await GetClientes(base_url, base64Credentials, ids_clientes);

                    if (cliente_erp.registros.Count == 0)
                    {
                        continue;
                    }

                    foreach (var registro in cliente_erp.registros)
                    {
                        bool contrato_ativo = false;
                        if (cliente_contrato.registros.Where(w => w.id_cliente == registro.id) != null)
                        {
                            cliente_contrato.registros.Where(w => w.id_cliente == registro.id).FirstOrDefault();

                            bool cliente_contrato_status = cliente_contrato.registros.Where(w => w.id_cliente == registro.id).FirstOrDefault().status == "A";
                            bool cliente_contrato_status_internet = cliente_contrato.registros.Where(w => w.id_cliente == registro.id).FirstOrDefault().status_internet == "A";

                            contrato_ativo = (cliente_contrato_status == true && cliente_contrato_status_internet == true);
                        }

                        bool ativo = registro.ativo == "S";

                        Regex regex = new Regex(@"[^\d]");
                        string cnpj_cpf_formatado = regex.Replace(registro.cnpj_cpf, "");

                        string nome = registro.fantasia != "" ? registro.fantasia + " - " + registro.razao : registro.razao + " - " + registro.fantasia;

                        try
                        {
                            var data = await integracao.AtualizaIntegracaoUsuarios(id_cliente, registro.email, cnpj_cpf_formatado,
                                nome, registro.telefone_celular, ativo);

                            string id_usuario = data.Rows[0]["Id"].ToString();
                            string id_subscriber = $"{id_cliente}_{id_usuario}";

                            await integracao.AtualizaIntegracaoAtivacao(id_subscriber, id_cliente, id_usuario, t_integracao_relacao_produto.id, contrato_ativo);
                        }
                        catch (Exception ex)
                        {
                            integracao.InsereIntegracaoLog($"ERRO, cliente_name: {cliente_name}, id_cliente: {id_cliente} " + $"EX {ex.Message} " +
                                $"CNPJ_CPF: {cnpj_cpf_formatado} Fantasia: {registro.fantasia} Razao: {registro.razao} Email: {registro.email}");
                        }
                    }
                }
            }

            string retorno = string.Empty;

            return Request.CreateResponse(HttpStatusCode.OK, retornoIntegracao);
        }

        public class RetornoIntegracao
        {
            public List<string> msg { get; set; } = new List<string>();
        }

        public async Task<view_vd_contratos_produtos_gen> GetContratos(string base_url, string base64Credentials, string id_produto)
        {
            var view_vd_contratos_produtos_gen = new view_vd_contratos_produtos_gen();
            
            var body = new
            {
                qtype = "view_vd_contratos_produtos_gen.id_produto",
                query = id_produto,
                oper = "=",
                page = 1,
                rp = 9999999
            };

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                client.DefaultRequestHeaders.Add("ixcsoft", "listar");

                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(body);
                HttpContent contentBody = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PostAsync($"{base_url}view_vd_contratos_produtos_gen",
                    contentBody);

                // Verifique o status da resposta
                if (response.IsSuccessStatusCode)
                {
                    // Processar a resposta aqui
                    string content = await response.Content.ReadAsStringAsync();

                    var responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<view_vd_contratos_produtos_gen>(content);

                    if (responseObj.rows.Count > 0)
                    {
                        view_vd_contratos_produtos_gen.rows.AddRange(responseObj.rows);
                    }
                }
            }

            if (view_vd_contratos_produtos_gen.rows != null && view_vd_contratos_produtos_gen.rows.Count > 0)
            {
                return view_vd_contratos_produtos_gen;
            }
            else
            {
                return new view_vd_contratos_produtos_gen();
            }
        }

        public async Task<cliente_contrato> GetClienteContratos(string base_url, string base64Credentials, string id_contrato)
        {
            var cliente_contrato = new cliente_contrato();

            var body = new
            {
                qtype = "cliente_contrato.id",
                query = id_contrato,
                oper = "IN",
                page = 1,
                rp = 9999999
            };

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                client.DefaultRequestHeaders.Add("ixcsoft", "listar");

                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(body);
                HttpContent contentBody = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PostAsync($"{base_url}cliente_contrato",
                    contentBody);

                // Verifique o status da resposta
                if (response.IsSuccessStatusCode)
                {
                    // Processar a resposta aqui
                    string content = await response.Content.ReadAsStringAsync();

                    var responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<cliente_contrato>(content);

                    if (responseObj.registros.Count > 0)
                    {
                        cliente_contrato.registros.AddRange(responseObj.registros);
                    }
                }
            }

            if (cliente_contrato.registros != null && cliente_contrato.registros.Count > 0)
            {
                return cliente_contrato;
            }
            else
            {
                return new cliente_contrato();
            }
        }

        public async Task<cliente> GetClientes(string base_url, string base64Credentials, string id_clientes)
        {
            var cliente = new cliente();

            var body = new
            {
                qtype = "cliente.id",
                query = id_clientes,
                oper = "IN",
                page = 1,
                rp = 9999999
            };

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                client.DefaultRequestHeaders.Add("ixcsoft", "listar");

                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(body);
                HttpContent contentBody = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PostAsync($"{base_url}cliente",
                    contentBody);

                if (response.IsSuccessStatusCode)
                {
                    // Processar a resposta aqui
                    string content = await response.Content.ReadAsStringAsync();

                    var responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<cliente>(content);

                    if (responseObj.registros.Count > 0)
                    {
                        cliente.registros.AddRange(responseObj.registros);
                    }
                }
            }

            if (cliente.registros != null && cliente.registros.Count > 0)
            {
                return cliente;
            }
            else
            {
                return new cliente();
            }
        }
    }
}

