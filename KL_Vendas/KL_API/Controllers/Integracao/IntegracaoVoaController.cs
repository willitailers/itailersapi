using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Configuration;
using System.Data;
using System.Linq;

namespace KL_API.Controllers.Integracao
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class IntegracaoVoaController : ApiController
    {
        public const string id_cliente = "4";

        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            Models.Integracao.Integracao integracao = new Models.Integracao.Integracao();
            string api_url = @ConfigurationManager.AppSettings["Voa_API_URL"];
            string token = @ConfigurationManager.AppSettings["Voa_TOKEN"];

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-auth-secret", token);

                HttpResponseMessage response =
                    await client.GetAsync(api_url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseVoa>(content);

                    foreach (var row in responseObj.rows)
                    {
                        var urn = row.produtos.ref_kaspersky.ToString();

                        //Buscar Produto pelo URN
                        DataRow data_produto = integracao.RetornaProdutoPeloUrn(urn).Select().FirstOrDefault();
                        var id_produto_kl = data_produto["id_produto_kl"].ToString(); 
                        var relacao_produto_id = integracao.RetornaIntegracaoRelacaoProdutos(id_cliente)
                            .Select($"id_produto_kl = {id_produto_kl}")
                            .Select(s => s["id"])
                            .FirstOrDefault()
                            .ToString();

                        //Voa integracao_cliente = id = 4
                        var data = integracao.AtualizaIntegracaoUsuarios(id_cliente, row.email, "", row.name, "", true);
                        string id_usuario = data.Rows[0]["Id"].ToString();
                        string id_subscriber = integracao.GenerateUniqueId(row.id);
                        integracao.AtualizaIntegracaoAtivacao(id_subscriber, id_cliente, id_usuario, relacao_produto_id, true);
                    }
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Ok");
        }

        public class ResponseVoa
        {
            public int rowCount { get; set; }
            public List<Row> rows { get; set; }

            public class Produtos
            {
                public string nome { get; set; }
                public int quantidade { get; set; }
                public string ref_kaspersky { get; set; }
            }

            public class Row
            {
                public string id { get; set; }
                public string name { get; set; }
                public string email { get; set; }
                public string tx_id { get; set; }
                public Produtos produtos { get; set; }
            }
        }
    }
}

