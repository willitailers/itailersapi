using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace KL_API.Controllers.Integracao
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class IntegracaoRelatorioAtivacoes2Controller : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            Models.Integracao.Integracao integracao = new Models.Integracao.Integracao();
            var relatorio_ativacoes = integracao.RelatorioAtivacoes();

            List<RelatorioAtivacoesResponse> relatorioAtivacoesResponse = new List<RelatorioAtivacoesResponse>();

            foreach (DataRow row_relatorio_ativacoes in relatorio_ativacoes.Rows)
            {
                RelatorioAtivacoesResponse ativacao = new RelatorioAtivacoesResponse()
                {
                    NomeConta = row_relatorio_ativacoes["Nome Conta"].ToString(),
                    NomeCliente = row_relatorio_ativacoes["Nome Cliente"].ToString(),
                    NomeProduto = row_relatorio_ativacoes["Nome Produto"].ToString(),
                    CodigoProduto = row_relatorio_ativacoes["Codigo Produto"].ToString(),
                    UrnProduto = row_relatorio_ativacoes["Urn Produto"].ToString(),
                    QtdLicenca = row_relatorio_ativacoes["Qtd Licencas"].ToString(),
                    ChaveAtivacao = row_relatorio_ativacoes["Chave Ativacao"].ToString(),
                    DataAtualizacao = row_relatorio_ativacoes["Data Atualizacao"].ToString(),
                    DataCriacao = row_relatorio_ativacoes["Data Criacao"].ToString()
                };

                relatorioAtivacoesResponse.Add(ativacao);
            }

            return Request.CreateResponse(HttpStatusCode.OK, relatorioAtivacoesResponse);
        }

        public class RelatorioAtivacoesResponse
        {
            public string NomeConta { get; set; }
            public string NomeCliente { get; set; }
            public string NomeProduto { get; set; }
            public string CodigoProduto { get; set; }
            public string UrnProduto { get; set; }
            public string QtdLicenca { get; set; }
            public string ChaveAtivacao { get; set; }
            public string DataAtualizacao { get; set; }
            public string DataCriacao { get; set; }
        }
    }
}