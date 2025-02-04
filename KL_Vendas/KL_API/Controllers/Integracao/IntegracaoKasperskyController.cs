using BLL;
using BLL.KL_API;
using KL_API.Models;
using KL_API.Models.Integracao;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static KL_API.Controllers.Integracao.IntegracaoKasperskyController;

namespace KL_API.Controllers.Integracao
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class IntegracaoKasperskyController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            KL_API.Models.Ativacao_Controle ativacao_Controle = new Ativacao_Controle();

            ResponseIntegracaoKaspersky responseIntegracaoKaspersky = new ResponseIntegracaoKaspersky();

            Models.Integracao.Integracao integracao = new Models.Integracao.Integracao();

            integracao.InsereIntegracaoLog("Começando Integracao Kaspersky");

            DataTable usuarios_nao_ativados = new DataTable();
            usuarios_nao_ativados = await integracao.RetornaIntegracaoUsuariosNaoAtivados();

            usuarios_nao_ativados = usuarios_nao_ativados.AsEnumerable().Take(50).CopyToDataTable();

            try
            {
                List<UsuariosAtivar> integracao_usuarios = new List<UsuariosAtivar>();

                foreach (DataRow row in usuarios_nao_ativados.Rows)
                {
                    UsuariosAtivar t_integracao_usuario = new UsuariosAtivar()
                    {
                        id_cliente = Convert.ToInt32(row["id_cliente"]),
                        cd_produto_kl = row["cd_produto_kl"].ToString(),
                        chave_ativacao = row["chave_ativacao"].ToString(),
                        id_produto_kl = Convert.ToInt32(row["id_produto_kl"]),
                        id_produto_relacionado = Convert.ToInt32(row["id_produto_relacionado"]),
                        id_subscriber = row["id_subscriber"].ToString(),
                        id_usuario = Convert.ToInt32(row["id_usuario"]),
                        qtd_licencas = row["qtd_licencas"].ToString(),
                        id_cliente_it = Convert.ToInt32(row["id_cliente_it"])
                    };

                    integracao_usuarios.Add(t_integracao_usuario);
                }

                List<int> clientes = integracao_usuarios.Select(s => s.id_cliente_it).Distinct().ToList();

                foreach (var id_cliente_it in clientes)
                {
                    int count = 1;
                    List<object> comandos = new List<object>();
                    List<Controle_Envio> controle = new List<Controle_Envio>();
                    string TransactionId = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
                    ClientInfo client = integracao.RetornaClientInfo(id_cliente_it.ToString());

                    foreach (UsuariosAtivar integracao_usuario in integracao_usuarios.Where(w => w.id_cliente_it == id_cliente_it))
                    {
                        var ativacao_prod = new KL_Conexao().KL_retorna_ativacao(integracao_usuario.qtd_licencas, integracao_usuario.cd_produto_kl,
                            DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z"),
                            "indefinite",
                            count.ToString(),
                            false,
                            integracao_usuario.id_subscriber);

                        controle.Add(new Controle_Envio()
                        {
                            comando = comando_kl.ativar,
                            UnitId = count,
                            SubscribeId = integracao_usuario.id_subscriber,
                            id_produto_kl = integracao_usuario.id_produto_relacionado.ToString(),
                            id_cliente_usuario = "0",
                            urn_produto = "",
                            id_cliente_licenca = id_cliente_it.ToString()
                        });

                        comandos.Add((object)ativacao_prod);
                        count++;
                    }


                    try
                    {
                        string xmlRequest, xmlContainer;

                        SubscriptionResponseContainer container = new SubscriptionResponseContainer();

                        container = new KL_Conexao().Comando_KL(TransactionId, client.nm_usuario_certificado, client.nm_senha_certificado,
                            client.nm_thumbprint, comandos.ToArray(), out xmlContainer, out xmlRequest);

                        integracao.InsereIntegracaoLog(client.id_cliente.ToString() + "- RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer));
                        integracao.InsereIntegracaoLog(client.id_cliente.ToString() + "- RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest));
                        integracao.InsereIntegracaoLog(client.id_cliente.ToString() + "- RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container));
                        integracao.InsereIntegracaoLog("container retorno da kaspersky", JsonConvert.SerializeObject(container));

                        try
                        {
                            foreach (object obj in container.Items)
                            {
                                if (obj.GetType() == typeof(SubscriptionResponseItemCollection))
                                {
                                    SubscriptionResponseItemCollection itens = new SubscriptionResponseItemCollection();
                                    itens = (SubscriptionResponseItemCollection)obj;

                                    string id_provisionamento = string.Empty;

                                    foreach (var item in itens.Items)
                                    {
                                        if (item.GetType() == typeof(SubscriptionResponseItemCollectionActivate))
                                        {
                                            try
                                            {
                                                var controleEnvio = new Controle_Envio();

                                                var itemDetalhe = (SubscriptionResponseItemCollectionActivate)item;

                                                controleEnvio = controle.Where(x => x.UnitId.ToString() == itemDetalhe.UnitId).FirstOrDefault();

                                                await integracao.AtualizaIntegracaoAtivacaoChave(controleEnvio.id_cliente_licenca, controleEnvio.SubscribeId,
                                                    itemDetalhe.ActivationCode, controleEnvio.id_produto_kl);
                                            }
                                            catch (Exception ex)
                                            {
                                                integracao.InsereIntegracaoLog($"Erro: AtualizaIntegracaoAtivacaoChave: {client.id_cliente} {ex.Message}");
                                            }
                                        }
                                        else if (obj.GetType() == typeof(SubscriptionResponseErrorCollection))
                                        {
                                            SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                                            itemErro = (SubscriptionResponseErrorCollection)obj;

                                            foreach (var erro in itemErro.Items)
                                            {
                                                var licenca = controle.Where(x => x.UnitId.ToString() == erro.UnitId).FirstOrDefault();
                                                integracao.InsereIntegracaoLog($"Erro: SubscriptionResponseErrorCollection: {client.id_cliente} {erro.ErrorMessage}");
                                            }
                                        }
                                        else if (obj.GetType() == typeof(TransactionErrorType))
                                        {
                                            TransactionErrorType itemErro = new TransactionErrorType();

                                            itemErro = (TransactionErrorType)obj;
                                            integracao.InsereIntegracaoLog($"Erro: TransactionErrorType: {client.id_cliente} {itemErro.ErrorMessage}");
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            // Deserializa o JSON para um objeto C#
                            var root = System.Text.Json.JsonSerializer.Deserialize<Root>(JsonConvert.SerializeObject(container));

                            // Exibe os dados para verificar a deserialização
                            Console.WriteLine($"Timestamp: {root.Timestamp}");
                            Console.WriteLine($"TransactionId: {root.TransactionId}");

                            foreach (var item in root.Items)
                            {
                                if (item.Items != null)
                                {
                                    foreach (var error in item.Items)
                                    {
                                        var controleEnvio = new Controle_Envio();
                                        controleEnvio = controle.Where(x => x.UnitId.ToString() == error.UnitId).FirstOrDefault();

                                        integracao.InsereIntegracaoLog($"UnitId: {error.UnitId}, " +
                                            $"SubscriberId: {error.SubscriberId}, " +
                                            $"ErrorCode: {error.ErrorCode}, " +
                                            $"ErrorMessage: {error.ErrorMessage}", JsonConvert.SerializeObject(container));

                                        await integracao.AtualizaIntegracaoAtivacaoChave(controleEnvio.id_cliente_licenca, controleEnvio.SubscribeId,
                                                    "-", controleEnvio.id_produto_kl);
                                    }
                                }
                            }
                            
                            integracao.InsereIntegracaoLog("Fim Integracao Kaspersky");

                            return Request.CreateResponse(HttpStatusCode.BadRequest);
                        }
                    }
                    catch (Exception ex)
                    {
                        integracao.InsereIntegracaoLog($"Erro: id_cliente: {client.id_cliente} Error Message: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                integracao.InsereIntegracaoLog("Erro: " + ex.Message);
                usuarios_nao_ativados = null;
            }

            integracao.InsereIntegracaoLog("Fim Integracao Kaspersky");

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public class ResponseIntegracaoKaspersky
        {
            public List<string> log { get; set; } = new List<string>();
            public List<object> comandos { get; set; } = new List<object>();
        }

        public class Root
        {
            public DateTime Timestamp { get; set; }
            public string TransactionId { get; set; }
            public List<Item> Items { get; set; }
        }

        public class Item
        {
            public List<SubscriptionError> Items { get; set; }
            public List<int> ItemsElementName { get; set; }
        }

        public class SubscriptionError
        {
            public string UnitId { get; set; }
            public string SubscriberId { get; set; }
            public string ErrorCode { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}

