using System.Collections.Generic;
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
        public HttpResponseMessage Post([FromBody] ProdutosRequest produtos)
        {
            if (!Request.Headers.Contains("kl-token"))
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, new ProdutosResponse() { msg_retorno = "Token é obrigatório" });

            string token = Request.Headers.GetValues("kl-token").First();

            ProdutosResponse produtosResponse = new ProdutosResponse();
            Produtos produtos1 = new Produtos()
            {
                chave_ativacao = "AAAAb-BABASBD-NASDIOADNS-DNASIO",
                descricao_produto = "Mais que um Antivírus. Nova proteção para múltiplos dispositivos...",
                imagem_produto = "images/kaspersky-logo.svg",
                nome_produto = "Kaspersky Standard"
            };

            Produtos produtos2 = new Produtos()
            {
                chave_ativacao = "1123-435345-6456456-7567567",
                descricao_produto = "Mais que um Antivírus. Nova proteção para múltiplos dispositivos...",
                imagem_produto = "images/kaspersky-logo.svg",
                nome_produto = "Kaspersky Standard"
            };

            produtosResponse.produtos.Add(produtos1);
            produtosResponse.produtos.Add(produtos2);

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
            public string chave_ativacao { get; set; }
            public string imagem_produto { get; set; }
            public string descricao_produto { get; set; }
            public string nome_produto { get; set; }
        }
    }
}