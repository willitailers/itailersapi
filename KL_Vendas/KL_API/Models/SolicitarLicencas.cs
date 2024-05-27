using BLL;
using BLL.KL_API;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KL_API.Models
{
    public class SolicitarLicencas
    {
        public class SolicitarLicencasObj
        {
            public int id_produto { get; set; }
            public int qtd_licencas_total { get; set; }
        }

        public class SolicitarLicencasRetorno
        {
            public int id_cliente { get; set; }
            public int nome_cliente { get; set; }
            public List<Produto_Ativacao_Retorno> produtos { get; set; }
        }

        public SolicitarLicencasRetorno Get(ClientInfo client, string id_produto, int qtd_ativacoes)
        {
            var dt_produto_cliente = new Ativacao_Controle().seleciona_produto_cliente(client.id_cliente, client.id_cliente_certificado);

            DataRow dt_produto_cliente_row = dt_produto_cliente.Select("id_produto_kl = " + id_produto)[0];

            string urn = dt_produto_cliente_row["nm_urn"].ToString();
            string produto_kl = dt_produto_cliente_row["nm_produto_kl"].ToString();
            string id_produto_kl = dt_produto_cliente_row["id_produto_kl"].ToString();
            string cd_produto_kl = dt_produto_cliente_row["cd_produto_kl"].ToString();
            string qtd_licencas = dt_produto_cliente_row["qtd_licencas"].ToString();
            string id_cliente = client.id_cliente.ToString();

            List<object> comandos = new List<object>();
            List<Controle_Envio> controle = new List<Controle_Envio>();
            var produto_ativado = new List<Produto_Ativacao_Retorno>();
            
            string TransactionId = DateTime.Now.ToString("yyyyMMddHHmmssffffff");

            int count = 1;
            List<string> subscriberIDsLista = new List<string>();
            for (int i = 0; i < qtd_ativacoes; i++)
            {
                string id = Guid.NewGuid().ToString("N");
                string subscription_id = string.Concat("prov-sea-", id);
                subscriberIDsLista.Add(subscription_id);

                var ativacao_prod = new KL_Conexao().KL_retorna_ativacao(qtd_licencas, cd_produto_kl,
                    DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z"),
                    "indefinite",
                    count.ToString(),
                    false,
                    subscription_id);

                controle.Add(new Controle_Envio()
                {
                    comando = comando_kl.ativar,
                    UnitId = count,
                    SubscribeId = subscription_id,
                    id_produto_kl = id_produto_kl,
                    id_cliente_usuario = "0",
                    urn_produto = urn,
                    id_cliente_licenca = id_cliente
                });

                comandos.Add((object)ativacao_prod);
                count++;

                produto_ativado.Add(new Produto_Ativacao_Retorno()
                {
                    ativado = false,
                    nome_produto = produto_kl,
                    urn_produto = urn
                });
            }

            foreach (var subscriberID in subscriberIDsLista)
            {
                var link_android = new KL_Conexao().KL_retorna_link(subscriberID,
                                         count.ToString(),
                                        "pt",
                                        PlatformEnum.Android);

                controle.Add(new Controle_Envio() { comando = comando_kl.link_android, UnitId = count, SubscribeId = subscriberID, id_cliente_usuario = string.Empty, urn_produto = urn, nm_produto = produto_kl, id_cliente_licenca = id_cliente, id_produto_kl = id_produto_kl });
                comandos.Add((object)link_android);
                count++;

                var link_windows = new KL_Conexao().KL_retorna_link(subscriberID,
                                         count.ToString(),
                                        "pt",
                                        PlatformEnum.Windows);

                controle.Add(new Controle_Envio() { comando = comando_kl.link_windows, UnitId = count, SubscribeId = subscriberID, id_cliente_usuario = string.Empty, urn_produto = urn, nm_produto = produto_kl, id_cliente_licenca = id_cliente, id_produto_kl = id_produto_kl });
                comandos.Add((object)link_windows);
                count++;

                var link_ios = new KL_Conexao().KL_retorna_link(subscriberID,
                                        count.ToString(),
                                       "pt",
                                       PlatformEnum.iOS);

                controle.Add(new Controle_Envio() { comando = comando_kl.link_iphone, UnitId = count, SubscribeId = subscriberID, id_cliente_usuario = string.Empty, urn_produto = urn, nm_produto = produto_kl, id_cliente_licenca = id_cliente, id_produto_kl = id_produto_kl });
                comandos.Add((object)link_ios);
                count++;

                var link_mac = new KL_Conexao().KL_retorna_link(subscriberID,
                                        count.ToString(),
                                       "pt",
                                       PlatformEnum.macOS);

                controle.Add(new Controle_Envio() { comando = comando_kl.link_mac, UnitId = count, SubscribeId = subscriberID, id_cliente_usuario = string.Empty, urn_produto = urn, nm_produto = produto_kl, id_cliente_licenca = id_cliente, id_produto_kl = id_produto_kl });
                comandos.Add((object)link_mac);
                count++;
            }

            try
            {
                string xmlRequest, xmlContainer;

                SubscriptionResponseContainer container = new SubscriptionResponseContainer();
                container = new KL_Conexao().Comando_KL(TransactionId, client.nm_usuario_certificado, client.nm_senha_certificado, client.nm_thumbprint, comandos.ToArray(), out xmlContainer, out xmlRequest);

                new Ativacao_Controle().log_inserir_provisionamento(client.id_cliente.ToString() + "- RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.license_ativation);
                new Ativacao_Controle().log_inserir_provisionamento(client.id_cliente.ToString() + "- RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.license_ativation);
                new Ativacao_Controle().log_inserir_provisionamento(client.id_cliente.ToString() + "- RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.license_ativation);

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

                                    id_provisionamento = new Ativacao_Controle().InserirProvisionamento(controleEnvio.id_cliente_licenca, controleEnvio.id_cliente_usuario, controleEnvio.SubscribeId,
                                    controleEnvio.id_produto_kl, itemDetalhe.ActivationCode, "", "", "", "", true, DateTime.Now);
                                }
                                catch (Exception ex)
                                {
                                    new Ativacao_Controle().log_inserir_provisionamento("Erro Provisionamento InserirProvisionamento - " + ex.Message, (int)Lista_Erro.license_ativation);
                                }
                            }

                            if (item.GetType() == typeof(SubscriptionResponseItemCollectionGetDownloadLinks))
                            {
                                try
                                {
                                    var itemDetalhe = (SubscriptionResponseItemCollectionGetDownloadLinks)item;
                                    var licenca = controle.Where(x => x.UnitId.ToString() == itemDetalhe.UnitId).FirstOrDefault();

                                    switch (licenca.comando)
                                    {
                                        case comando_kl.link_android:
                                            string link_ativacao_android = itemDetalhe.DownloadLinks[0].Url.Replace("market://", "https://play.google.com/store/apps/");
                                            new Ativacao_Controle().AtualizarLinksProvisionamento(itemDetalhe.SubscriberId, linkAndroid: link_ativacao_android);
                                            break;

                                        case comando_kl.link_iphone:
                                            string link_ativacao_iphone = itemDetalhe.DownloadLinks[0].Url;
                                            new Ativacao_Controle().AtualizarLinksProvisionamento(itemDetalhe.SubscriberId, linkIOS: link_ativacao_iphone);
                                            break;

                                        case comando_kl.link_mac:
                                            string link_ativacao_mac = itemDetalhe.DownloadLinks[0].Url;
                                            new Ativacao_Controle().AtualizarLinksProvisionamento(itemDetalhe.SubscriberId, linkMac: link_ativacao_mac);
                                            break;

                                        case comando_kl.link_windows:
                                            string link_ativacao_windows = itemDetalhe.DownloadLinks[0].Url;
                                            new Ativacao_Controle().AtualizarLinksProvisionamento(itemDetalhe.SubscriberId, linkWindows: link_ativacao_windows);
                                            break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    new Ativacao_Controle().log_inserir_provisionamento("Erro Provisionamento AtualizarLinksProvisionamento - " + ex.Message, (int)Lista_Erro.license_ativation);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new Ativacao_Controle().log_inserir(client.id_cliente.ToString() + " - Erro Provisionamento - " + controle[0].id_cliente_usuario.ToString() + " - " + ex.Message, (int)Lista_Erro.license_ativation);
                throw;
            }

            return new SolicitarLicencasRetorno();
        }
    }
}