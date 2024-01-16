using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace KL_API.Controllers.Integracao
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class IntegracaoRelatorioAtivacoesController : ApiController
    {
        [HttpPost]
        
        public HttpResponseMessage Post()
        {
            RelatorioAtivacoesResponse response = new RelatorioAtivacoesResponse();

            Models.Integracao.Integracao integracao = new Models.Integracao.Integracao();
            var relatorio_ativacoes = integracao.RelatorioAtivacoes();
            var templates = integracao.RetornaIntegracaoTemplate();

            string relatorio_email_template = string.Empty;
            foreach (DataRow row_template in templates.Rows)
            {
                string id_cliente = row_template["id_cliente"].ToString();

                if (id_cliente == "0")
                {
                    relatorio_email_template = row_template["conteudo"].ToString();
                }
            }

            string dados_relatorio_ativacoes = string.Empty;
            foreach (DataRow row_relatorio_ativacoes in relatorio_ativacoes.Rows)
            {
                dados_relatorio_ativacoes += $"<tr>" +
                    $"<td style='border: 1px solid black'>{row_relatorio_ativacoes["cd_produto"]}</td>" +
                    $"<td style='border: 1px solid black'>{row_relatorio_ativacoes["nome_produto"]}</td>" +
                    $"<td style='border: 1px solid black'>{row_relatorio_ativacoes["qtd_licencas"]}</td>" +
                    $"<td style='border: 1px solid black'>{row_relatorio_ativacoes["chave_ativacao"]}</td>" +
                    $"<td style='border: 1px solid black'>{row_relatorio_ativacoes["cliente"]}</td>" +
                    $"<td style='border: 1px solid black'>{row_relatorio_ativacoes["email"]}</td>" +
                    $"<td style='border: 1px solid black'>{row_relatorio_ativacoes["nome"]}</td>" +
                    $"<td style='border: 1px solid black'>{row_relatorio_ativacoes["cel"]}</td>" +
                    $"<td style='border: 1px solid black'>{row_relatorio_ativacoes["ativo"]}</td>" +
                    $"<td style='border: 1px solid black' >{row_relatorio_ativacoes["email_enviado"]}</td>" +
                    $"</tr>";
            }

            response.email = relatorio_email_template.Replace("[[relatorio_ativacoes]]", dados_relatorio_ativacoes);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        public class RelatorioAtivacoesResponse
        {
            public string email { get; set; }
        }
    }
}