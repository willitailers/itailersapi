using KL_API.Models.Integracao.cliente_contrato;
using KL_API.Models.Integracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using KL_API.Models.Integracao.cliente;
using System.Data;
using System.Web.Http.Description;

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

            List<string> msg_erro = new List<string>();

            foreach (DataRow row in integracao_clientes.Rows)
            {
                string username = row["usuario_itailers"].ToString();
                string password = row["password_itailers"].ToString();
                //string id_produto = row["id_produto"].ToString();
                string base_url = row["url"].ToString();
                string cliente_name = row["cliente"].ToString();
                string id_cliente = row["id"].ToString();

                string base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

                var relacao_produtos = integracao.RetornaIntegracaoRelacaoProdutos(id_cliente);

                string id_produto = string.Empty;
                List<string> listProdutos = new List<string>();
                foreach (DataRow produto in relacao_produtos.Rows)
                {
                    listProdutos.Add(produto["id_produto_cliente"].ToString());
                }

                id_produto = string.Join(",", listProdutos);

                view_vd_contratos_produtos_gen contratos = await GetContratos(base_url, base64Credentials, id_produto);

                if (contratos.type == "error")
                {
                    msg_erro.Add($"{cliente_name}: {contratos.message}");
                    continue;
                }

                var cells = contratos.rows.Select(r => r.cell).ToList();
                var id_contratos = cells.Select(s => s[12]).ToList();

                if (string.IsNullOrEmpty(contratos.total) || contratos.total == "0")
                {
                    msg_erro.Add($"{cliente_name}: Nenhum contrato foi encontrado");
                }

                cliente_contrato cliente_contrato = await GetClienteContratos(base_url, base64Credentials, id_contratos);

                if (!cliente_contrato.registros.Any(w => w.status == "A" && (w.status_internet == "A" || w.status_internet == "FA")))
                {
                    msg_erro.Add($"{cliente_name}: Nenhum contrato ativo foi encontrado");
                }

                cliente_contrato.registros = cliente_contrato.registros.Where(w => w.status == "A" && (w.status_internet == "A" || w.status_internet == "FA")).ToList();

                string id_clientes = string.Join(",", cliente_contrato.registros.Select(s => s.id_cliente).ToList());

                cliente cliente = await GetClientes(base_url, base64Credentials, id_clientes);

                foreach (var registro in cliente.registros)
                {
                    integracao.AtualizaIntegracaoUsuarios(id_cliente, registro.email, registro.cnpj_cpf, registro.fantasia != "" ? registro.fantasia : registro.razao, 
                        registro.telefone_celular, registro.ativo == "S");
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, "OK!");
        }

        public async Task<view_vd_contratos_produtos_gen> GetContratos(string base_url, string base64Credentials, string id_produto)
        {
            var body = new
            {
                qtype = "view_vd_contratos_produtos_gen.id_produto",
                query = id_produto,
                oper = "IN",
                rp = "2000"
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

                    return responseObj;
                }
                else
                {
                    return new view_vd_contratos_produtos_gen();
                }
            }
        }

        public async Task<cliente_contrato> GetClienteContratos(string base_url, string base64Credentials, List<string> id_contratos)
        {
            var body = new
            {
                qtype = "cliente_contrato.id",
                query = string.Join(",", id_contratos),
                oper = "IN"
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

                    return responseObj;
                }
                else
                {
                    return new cliente_contrato();
                }
            }
        }

        public async Task<cliente> GetClientes(string base_url, string base64Credentials, string id_clientes)
        {
            var body = new
            {
                qtype = "cliente.id",
                query = id_clientes,
                oper = "IN",
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

                // Verifique o status da resposta
                if (response.IsSuccessStatusCode)
                {
                    // Processar a resposta aqui
                    string content = await response.Content.ReadAsStringAsync();

                    var responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<cliente>(content);

                    return responseObj;
                }
                else
                {
                    return new cliente();
                }
            }
        }
    }
}

