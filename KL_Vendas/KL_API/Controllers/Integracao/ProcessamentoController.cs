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

namespace KL_API.Controllers.Integracao
{
    public class ProcessamentoController : ApiController
    {
        //Signet
        //public readonly string username = "91";
        //public readonly string password = "be8a53e7090d0c42fc61b155b24bb7eb71b8da2f499c810249ccc5bfef5fe641";
        //public readonly string base_url = "https://central.signets.com.br/webservice/v1/";
        //public readonly string id_produto = "2717";

        //LocalNet
        public readonly string username = "33";
        public readonly string password = "37d30e12c4a2d986672ffd361aaca2fa5312175f8b7bf1f6f7febcbf145e55d4";
        public readonly string base_url = "https://sac.localnet.srv.br/webservice/v1/";
        public readonly string id_produto = "1076";

        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            //string id_produto = "1076";
            string base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

            // Defina seu nome de usuário e senha
            view_vd_contratos_produtos_gen contratos = await GetContratos(base_url, base64Credentials, id_produto);

            var cells = contratos.rows.Select(r => r.cell).ToList();
            var id_contratos = cells.Select(s => s[12]).ToList();

            if (string.IsNullOrEmpty(contratos.total) || contratos.total == "0")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Nenhum contrato foi encontrado");
            }

            cliente_contrato cliente_contrato = await GetClienteContratos(base_url, base64Credentials, id_contratos);

            if (!cliente_contrato.registros.Any(w => w.status == "A" && (w.status_internet == "A" || w.status_internet == "FA")))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Nenhum contrato ativo foi encontrado");
            }

            cliente_contrato.registros = cliente_contrato.registros.Where(w => w.status == "A" && (w.status_internet == "A" || w.status_internet == "FA")).ToList();

            string id_clientes = string.Join(",", cliente_contrato.registros.Select(s => s.id_cliente).ToList());

            cliente cliente = await GetClientes(base_url, base64Credentials, id_clientes);

            return Request.CreateResponse(HttpStatusCode.OK, cliente);
        }

        public async Task<view_vd_contratos_produtos_gen> GetContratos(string base_url, string base64Credentials, string id_produto)
        {
            var body = new
            {
                qtype = "view_vd_contratos_produtos_gen.id_produto",
                query = id_produto,
                oper = "=",
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
