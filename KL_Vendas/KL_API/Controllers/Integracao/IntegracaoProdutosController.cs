using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace KL_API.Controllers.Integracao
{
    public class IntegracaoProdutosController : ApiController
    {
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage Post([FromBody] ProdutosRequest produto)
        {
            if (!Request.Headers.Contains("kl-token"))
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, 
                    new ProdutosResponse() { msg_retorno = "Token é obrigatório" });

            if(produto.id_cliente is null || produto.id_cliente == "")
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, 
                    new ProdutosResponse() { msg_retorno = "id_cliente é obrigatório" });

            string token = Request.Headers.GetValues("kl-token").First();

            Models.Integracao.Integracao integracao = new Models.Integracao.Integracao();
            var produtos = integracao.RetornaIntegracaoProdutos(produto.id_cliente);

            if (produtos.Rows.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, 
                    new ProdutosResponse() { msg_retorno = "Nenhum produto ativado para este usuario" });
            }

            ProdutosResponse produtosResponse = new ProdutosResponse();
            if (produtos.Rows.Count > 0)
            {
                foreach (DataRow row in produtos.Rows)
                {
                    Produtos produtos1 = new Produtos()
                    {
                        chave_ativacao = row["chave_ativacao"].ToString(),
                        descricao_produto = row["descricao_produto"].ToString(),
                        imagem_produto = row["imagem_produto"].ToString(),
                        nome_produto = row["nome_produto"].ToString()
                    };

                    produtosResponse.produtos.Add(produtos1);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, produtosResponse);
        }

        public class ProdutosRequest
        {
            public string id_cliente { get; set; }
        }

        public class ProdutosResponse
        {
            public List<Produtos> produtos { get; set; } = new List<Produtos>();
            public string msg_retorno { get; set; }
        }

        public class Produtos
        {
            public string nome_produto { get; set; }
            public string chave_ativacao { get; set; }
            public string imagem_produto { get; set; }
            public string descricao_produto { get; set; }
        }   
    }
}