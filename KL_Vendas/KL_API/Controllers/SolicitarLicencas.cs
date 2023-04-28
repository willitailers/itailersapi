using KL_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static KL_API.Models.SolicitarLicencas;

namespace KL_API.Controllers
{
    public class SolicitarLicencasController : ApiController
    {
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage SolicitarLicencas([FromBody] SolicitarLicencasObj solicitacaoLicencas)
        {
            var clientInfo = new ClientInfo();
            if (Request.Headers.Contains("kl-token"))
            {
                string token = Request.Headers.GetValues("kl-token").First();
                clientInfo = new Ativacao_Controle().ValidaToken(token);
            }

            List<Produto_Ativacao_Retorno> produto_Ativacao_Retorno = SolicitarProvisionamento(solicitacaoLicencas.qtd_licencas_total, clientInfo, solicitacaoLicencas.id_produto);

            return Request.CreateResponse<List<Produto_Ativacao_Retorno>>(HttpStatusCode.OK, produto_Ativacao_Retorno);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public List<Produto_Ativacao_Retorno> SolicitarProvisionamento(int qtd_licencas_total, ClientInfo clientInfo, int id_produto)
        {

            try
            {
                int qtd_licencas = qtd_licencas_total;
                int count = 1;

                while (qtd_licencas > 0)
                {
                    if (count % 20 == 0)
                    {
                        Thread.Sleep(TimeSpan.FromHours(1));
                    }

                    var qtdLote = Math.Min(qtd_licencas, 50);

                    new Ativacao_Controle().Provisionar(clientInfo, id_produto, qtdLote);

                    qtd_licencas -= qtdLote;
                    count++;
                }

                var provisionamentoLista = new Ativacao_Controle().Retorna_provisionamento_por_id_cliente(clientInfo.id_cliente, id_produto);

                List<Produto_Ativacao_Retorno> list = new List<Produto_Ativacao_Retorno>();
                if (provisionamentoLista.Rows.Count > 0)
                {
                    foreach (DataRow row in provisionamentoLista.Rows)
                    {
                        Produto_Ativacao_Retorno produto_Ativacao_Retorno = new Produto_Ativacao_Retorno
                        {
                            ativado = true,
                            cd_produto = row["cd_produto_kl"].ToString(),
                            chave_ativacao = row["chaveAtivacao"].ToString(),
                            id_cliente = row["id_cliente"].ToString(),
                            id_produto = Convert.ToInt32(row["id_produto"]),
                            link_ativacao_android = row["linkAndroid"].ToString(),
                            link_ativacao_iphone = row["linkIOS"].ToString(),
                            link_ativacao_mac = row["linkMac"].ToString(),
                            link_ativacao_windows = row["linkWindows"].ToString(),
                            nome_produto = row["nm_produto_kl"].ToString(),
                            urn_produto = row["nm_urn"].ToString(),
                            nm_cliente = row["nm_cliente"].ToString()
                        };

                        list.Add(produto_Ativacao_Retorno);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                new Ativacao_Controle().log_inserir_provisionamento("Erro SolicitarLicencas - " + ex.Message, (int)Lista_Erro.license_ativation);
                return new List<Produto_Ativacao_Retorno>();
            }
        }
    }
}
