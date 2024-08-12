using KL_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace KL_API.Controllers
{
    public class GetProdutosClienteController : ApiController
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = false)]
        public HttpResponseMessage GetProdutosCliente()
        {
            var client = new ClientInfo();
            if (Request.Headers.Contains("kl-token"))
            {
                string token = Request.Headers.GetValues("kl-token").First();
                client = new Ativacao_Controle().ValidaToken(token);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Token Inválido");
            }

            var produtosClienteDatatable = new Ativacao_Controle().ConsultaProdutosCliente(client.id_cliente);

            List<ProdutosCliente> prods = new List<ProdutosCliente>();

            foreach (DataRow produtosClienteRow in produtosClienteDatatable.Rows)
            {
                ProdutosCliente produtosCliente = new ProdutosCliente()
                {
                    NomeProduto = produtosClienteRow["nm_produto_kl"].ToString(),
                    QuantidadeLicencas = produtosClienteRow["qtd_licencas"].ToString(),
                    Urn = produtosClienteRow["nm_urn"].ToString(),
                };

                prods.Add(produtosCliente);
            }

            return Request.CreateResponse(HttpStatusCode.OK, prods);
        }
    }
}
