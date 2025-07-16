using KL_API.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;
using KL_API.Models.Campsoft;
using KL_API.Controllers.CampSoft.Models;
using System.Threading.Tasks;

namespace KL_API.Controllers.CampSoft
{
    public class GetProdutosCampSoftController : ApiController
    {
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage GetProdutosCampSoft([FromBody] RequestProdutosCampSoft requestProdutosCampSoft)
        {
            Campsoft campsoft = new Campsoft();

            string token = string.Empty;

            if (!Request.Headers.TryGetValues("kl-token", out var kl_token))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Header kl-token é obrigatório.");
            }
            else
            {
                try
                {
                    token = kl_token.First();
                }
                catch (System.Exception)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Header kl-token é obrigatório.");
                }
            }

            ClientInfo client = new Ativacao_Controle().ValidaToken(token.ToString());

            if (!client.valido)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "O cliente não foi encontrado.");
            }

            var produtos = campsoft.GetProdutos(client, requestProdutosCampSoft.user_id);

            return Request.CreateResponse(HttpStatusCode.OK, produtos);
        }
    }
}
