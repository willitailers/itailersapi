using KL_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KL_API.Controllers
{
    public class GetProdutosController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetProdutos([FromBody] GetProdutosObj getProdutosObj)
        {
            var clientInfo = new ClientInfo();
            GetProdutosRetorno getProdutos = new GetProdutosRetorno();
            if (Request.Headers.Contains("kl-token"))
            {
                string token = Request.Headers.GetValues("kl-token").First();
                clientInfo = new Ativacao_Controle().ValidaToken(token);
                if (clientInfo.valido)
                {
                    getProdutos = new Ativacao_Controle().GetProdutos(getProdutosObj, clientInfo);

                    if (getProdutos.cod_retorno < 0)
                    {
                        return Request.CreateResponse<GetProdutosRetorno>(HttpStatusCode.NotAcceptable, getProdutos);
                    }

                    return Request.CreateResponse<GetProdutosRetorno>(HttpStatusCode.OK, getProdutos);
                }
                else
                {
                    return Request.CreateResponse<GetProdutosRetorno>(HttpStatusCode.NotAcceptable, new GetProdutosRetorno() { cod_retorno = -1, msg_retorno = "Token Inválido" });
                }
            }
            else
            {
                return Request.CreateResponse<GetProdutosRetorno>(HttpStatusCode.NotAcceptable, new GetProdutosRetorno() { cod_retorno = -1, msg_retorno = "Token é obrigatório" });
            }
        }
    }
}
