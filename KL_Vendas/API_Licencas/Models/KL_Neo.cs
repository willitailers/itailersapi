using DAL;
using Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Configuration;
using BLL;
using BLL.KL_API;
using System.Xml.Linq;
using System.ComponentModel;
using System.IO;
//using System.Data.Entity;
//using SqlKata.Execution;


namespace API_Licencas.Models
{
    public class KL_Neo
    {
        public enum Lista_Erro
        {
            erro_neo = -2,
            consulta_produto_neo = 1010,
            ativação_produto_neo = 1020,
            ativacao_neo = 2004,
            ativacao_neo_lote = 2005,
            cancelamento_neo = 2006,
            get_info_neo = 2007,
            cancelamento_neo_lote = 2008
        }

        #region Ações
        public Usuario_Neo login(Login_Neo login)
        {
            using (var client = new HttpClient())
            {
                var usuario_neo = new Usuario_Neo();
                usuario_neo.produto = new List<Produto_Neo>();
                string id_cliente = "";
                bool novo = false;
                var response = client.PostAsync(@ConfigurationManager.AppSettings["link_login_neo"], new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json")).Result;

                if (response.Content != null)
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;

                    try
                    {
                        var response_neo = Newtonsoft.Json.JsonConvert.DeserializeObject<response_auth_neo>(responseContent);

                        if (response_neo.access)
                        {
                            // consulta usuario
                            var cliente = consulta_cliente(response_neo.subscriber_id);
                            usuario_neo.dv_ativo = 1;
                            usuario_neo.nm_subscribe_id = response_neo.subscriber_id;
                            // insere se nao tiver
                            if (cliente.Rows.Count == 0)
                            {
                                var novo_cliente = inserir_cliente(response_neo.subscriber_id);


                                id_cliente = novo_cliente.Rows[0]["id_cliente_neo"].ToString();
                                novo = true;
                            }
                            else
                            {
                                id_cliente = cliente.Rows[0]["id_cliente_neo"].ToString();
                            }

                            // consulta produtos
                            if (novo)
                            {
                                var produto = consulta_produto("0");

                                foreach (DataRow dr in produto.Rows)
                                {
                                    Produto_Consulta_Neo prod = new Produto_Consulta_Neo() { subscriber_id = usuario_neo.nm_subscribe_id, resource_id = dr["nm_urn"].ToString() };
                                    var response_produto = client.PostAsync(@ConfigurationManager.AppSettings["link_produto_neo"], new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(prod), Encoding.UTF8, "application/json")).Result;

                                    if (response_produto.Content != null)
                                    {
                                        var response_produto_auth = response_produto.Content.ReadAsStringAsync().Result;

                                        var obj_response_produto_auth = Newtonsoft.Json.JsonConvert.DeserializeObject<response_auth_neo>(response_produto_auth);

                                        if (obj_response_produto_auth.access)
                                        {
                                            if (dr["id_combo"].ToString() == "0")
                                            {
                                                usuario_neo.produto.Add(new Produto_Neo()
                                                {
                                                    id_produto_kl = int.Parse(dr["id_produto_kl"].ToString()),
                                                    resource_id = dr["nm_urn"].ToString(),
                                                    id_status = "1",
                                                    nm_produto_kl = dr["nm_produto_kl"].ToString(),
                                                    link_image = "/images/" + dr["nm_imagem"].ToString(),
                                                    nm_descricao = dr["descricao"].ToString()
                                                });
                                            }
                                            else
                                            {
                                                // buscar os produtos do combo e monta o objeto

                                                var produtos_combo = consulta_combo(dr["id_combo"].ToString());

                                                foreach (DataRow dr_combo in produtos_combo.Rows)
                                                {
                                                    usuario_neo.produto.Add(new Produto_Neo()
                                                    {
                                                        combo_id = dr_combo["id_combo"].ToString(),
                                                        id_produto_kl = int.Parse(dr_combo["id_produto"].ToString()),
                                                        resource_id = dr["nm_urn"].ToString(),
                                                        id_status = "1",
                                                        nm_produto_kl = dr_combo["nm_produto"].ToString(),
                                                        link_image = "/images/" + dr_combo["nm_imagem"].ToString(),
                                                        nm_descricao = dr_combo["descricao"].ToString(),
                                                    });
                                                }

                                            }
                                        }
                                    }
                                }

                                return usuario_neo;
                            }
                            else
                            {
                                var produto_ativado = consulta_cliente_ativacao(id_cliente);

                                if (produto_ativado.Rows.Count == 0)
                                {
                                    var produto = consulta_produto("0");

                                    foreach (DataRow dr in produto.Rows)
                                    {
                                        Produto_Consulta_Neo prod = new Produto_Consulta_Neo() { subscriber_id = usuario_neo.nm_subscribe_id, resource_id = dr["nm_urn"].ToString() };
                                        var response_produto = client.PostAsync(@ConfigurationManager.AppSettings["link_produto_neo"], new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(prod), Encoding.UTF8, "application/json")).Result;

                                        if (response_produto.Content != null)
                                        {
                                            var response_produto_auth = response_produto.Content.ReadAsStringAsync().Result;

                                            var obj_response_produto_auth = Newtonsoft.Json.JsonConvert.DeserializeObject<response_auth_neo>(response_produto_auth);

                                            if (obj_response_produto_auth.access)
                                            {
                                                if (dr["id_combo"].ToString() == "0")
                                                {
                                                    usuario_neo.produto.Add(new Produto_Neo()
                                                    {
                                                        id_produto_kl = int.Parse(dr["id_produto_kl"].ToString()),
                                                        resource_id = dr["nm_urn"].ToString(),
                                                        id_status = "1",
                                                        nm_produto_kl = dr["nm_produto_kl"].ToString(),
                                                        link_image = "/images/" + dr["nm_imagem"].ToString(),
                                                        nm_descricao = dr["descricao"].ToString()
                                                    });
                                                }
                                                else
                                                {
                                                    // buscar os produtos do combo e monta o objeto

                                                    var produtos_combo = consulta_combo(dr["id_combo"].ToString());

                                                    foreach (DataRow dr_combo in produtos_combo.Rows)
                                                    {
                                                        usuario_neo.produto.Add(new Produto_Neo()
                                                        {
                                                            combo_id = dr_combo["id_combo"].ToString(),
                                                            id_produto_kl = int.Parse(dr_combo["id_produto"].ToString()),
                                                            resource_id = dr["nm_urn"].ToString(),
                                                            id_status = "1",
                                                            nm_produto_kl = dr_combo["nm_produto"].ToString(),
                                                            link_image = "/images/" + dr_combo["nm_imagem"].ToString(),
                                                            nm_descricao = dr_combo["descricao"].ToString(),
                                                        });
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    return usuario_neo;
                                }
                                else // retorna o produto ja cadastrado se tiver
                                {
                                    //verifica se produto ainda esta ativo
                                    var produto = consulta_produto(produto_ativado.Rows[0]["id_produto_kl"].ToString());
                                    Produto_Consulta_Neo prod = new Produto_Consulta_Neo() { subscriber_id = usuario_neo.nm_subscribe_id, resource_id = produto.Rows[0]["nm_urn"].ToString() };
                                    var response_produto = client.PostAsync(@ConfigurationManager.AppSettings["link_produto_neo"], new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(prod), Encoding.UTF8, "application/json")).Result;
                                    string nm_subrscribe_kl = produto_ativado.Rows[0]["nm_subrscribe_kl"].ToString();
                                    if (response_produto.Content != null)
                                    {
                                        var response_produto_auth = response_produto.Content.ReadAsStringAsync().Result;

                                        var obj_response_produto_auth = Newtonsoft.Json.JsonConvert.DeserializeObject<response_auth_neo>(response_produto_auth);

                                        if (obj_response_produto_auth.access)
                                        {
                                            usuario_neo.dv_ativo = 2;
                                            // se ainda tiver, pega o link de download
                                            //string link = @ConfigurationManager.AppSettings["link_download"] + produto_ativado.Rows[0]["activation_code"].ToString();
                                            //string link_android = link_download_neo(nm_subrscribe_kl, usuario_neo.nm_subscribe_id + "0" + DateTime.Now.ToString("yyyyMMddHHmmss"), PlatformEnum.Android);
                                            //string link_win = link_download_neo(nm_subrscribe_kl, usuario_neo.nm_subscribe_id + "1" + DateTime.Now.ToString("yyyyMMddHHmmss"), PlatformEnum.Windows);
                                            //string link_mac = link_download_neo(nm_subrscribe_kl, usuario_neo.nm_subscribe_id + "2" + DateTime.Now.ToString("yyyyMMddHHmmss"), PlatformEnum.macOS);
                                            //string link_ios = link_download_neo(nm_subrscribe_kl, usuario_neo.nm_subscribe_id + "3" + DateTime.Now.ToString("yyyyMMddHHmmss"), PlatformEnum.iOS);

                                            if (produto.Rows[0]["id_combo"].ToString() == "0")
                                            {
                                                usuario_neo.produto.Add(new Produto_Neo()
                                                {
                                                    id_produto_kl = int.Parse(produto.Rows[0]["id_produto_kl"].ToString()),
                                                    id_status = "2",
                                                    nm_produto_kl = produto.Rows[0]["nm_produto_kl"].ToString(),
                                                    //nm_link_dowload_android = @link_android,
                                                    //nm_link_dowload_windows = @link_win,
                                                    //nm_link_dowload_mac = @link_mac,
                                                    //nm_link_dowload_ios = @link_ios,
                                                    activation_code = produto_ativado.Rows[0]["activation_code"].ToString(),
                                                    dt_ativacao = retorna_data(DateTime.Parse(produto_ativado.Rows[0]["dt_ativacao"].ToString())),
                                                    link_image = "/images/" + produto.Rows[0]["nm_imagem"].ToString(),
                                                    resource_id = produto.Rows[0]["nm_urn"].ToString(),
                                                    nm_descricao = produto.Rows[0]["descricao"].ToString()
                                                });
                                            }
                                            else
                                            {
                                                var produtos_Combo = consulta_combo(produto.Rows[0]["id_combo"].ToString());

                                                if (produtos_Combo.Rows.Count <= 0)
                                                {
                                                    usuario_neo.msg_erro = "Erro durante a consulta de comnbo[5]";
                                                    return usuario_neo;
                                                }

                                                List<Lista_Produto> nm_subrscribe_kls = new List<Lista_Produto>();


                                                foreach (DataRow dr in produtos_Combo.Rows)
                                                {
                                                    nm_subrscribe_kls.Add(new Lista_Produto() { subscribe_id = nm_subrscribe_kl, id_produto = int.Parse(produtos_Combo.Rows[0]["id_produto"].ToString()) });

                                                    usuario_neo.produto.Add(new Produto_Neo()
                                                    {
                                                        combo_id = produto.Rows[0]["id_combo"].ToString(),
                                                        id_produto_kl = int.Parse(produto_ativado.Rows[0]["id_produto_kl"].ToString()),
                                                        id_produto = int.Parse(dr["id_produto"].ToString()),
                                                        id_status = "2",
                                                        nm_produto_kl = dr["nm_produto"].ToString(),
                                                        //nm_link_dowload_android = links.nm_link_dowload_android,
                                                        //nm_link_dowload_windows = links.nm_link_dowload_windows,
                                                        //nm_link_dowload_mac = links.nm_link_dowload_mac,
                                                        //nm_link_dowload_ios = links.nm_link_dowload_ios,
                                                        activation_code = produto_ativado.Rows[0]["activation_code"].ToString(),
                                                        dt_ativacao = retorna_data(DateTime.Parse(produto_ativado.Rows[0]["dt_ativacao"].ToString())),
                                                        resource_id = dr["nm_urn"].ToString(),
                                                        link_image = "/images/" + dr["nm_imagem"].ToString(),
                                                        nm_descricao = dr["descricao"].ToString()
                                                    });
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // cancel produto
                                        }
                                    }
                                    return usuario_neo;
                                }
                            }

                            return usuario_neo;
                        }
                        else
                        {
                            return new Usuario_Neo() { dv_ativo = 0 };
                        }
                    }
                    catch (Exception ex)
                    {
                        log_inserir("json nao convertido: " + responseContent + " - " + ex.Message, (int)Lista_Erro.erro_neo);
                        return new Usuario_Neo() { dv_ativo = 0 };
                    }
                }
                else
                {
                    return new Usuario_Neo() { dv_ativo = 0 };
                }
            }
        }

        public Usuario_Neo ativar(Ativacao_Neo ativacao)
        {
            var usuario_neo = new Usuario_Neo();
            usuario_neo.produto = new List<Produto_Neo>();
            usuario_neo.dv_ativo = 1;
            usuario_neo.nm_subscribe_id = ativacao.subscriber_id;
            string id_cliente_neo = "";

            try
            {
                // verifica se  existe um produto ativado pro cliente
                var cliente = consulta_cliente(ativacao.subscriber_id);

                if (cliente.Rows.Count == 0)
                {
                    usuario_neo.msg_erro = "usuario nao encontrado.";
                    return usuario_neo;
                }

                id_cliente_neo = cliente.Rows[0]["id_cliente_neo"].ToString();


                var cliente_ativacao = consulta_cliente_ativacao(cliente.Rows[0]["id_cliente_neo"].ToString());

                if (cliente_ativacao.Rows.Count > 0)
                {
                    usuario_neo.msg_erro = "usuario ja possui produto ativado.";
                    return usuario_neo;
                }

                var produto = consulta_produto("0");
                string id_produto_kl = "", cd_produto_kl = "", qtd_licenças = "", activation_code = "", nm_produto_kl = "", id_combo = "";

                foreach (DataRow dr in produto.Rows)
                    if (dr["nm_urn"].ToString() == ativacao.resource_id)
                    {
                        id_produto_kl = dr["id_produto_kl"].ToString();
                        cd_produto_kl = dr["cd_produto_kl"].ToString();
                        qtd_licenças = dr["qtd_licencas"].ToString();
                        nm_produto_kl = dr["nm_produto_kl"].ToString();
                        id_combo = dr["id_combo"].ToString();
                    }

                if (string.IsNullOrEmpty(cd_produto_kl) && id_combo == "0")
                {
                    usuario_neo.msg_erro = "Produto não encontrado.";
                    return usuario_neo;
                }
                // ativar
                KL_Conexao con = new KL_Conexao();
                bool ativado = true;

                // ativação

                if (id_combo == "0")
                {
                    SubscriptionResponseContainer container = new SubscriptionResponseContainer();
                    Random rand = new Random();

                    container = con.AtivacaoNeo(ativacao.subscriber_id + DateTime.Now.ToString("yyMMddHHmmss"), cd_produto_kl, rand.GetHashCode() + "_" + ativacao.subscriber_id, qtd_licenças, DateTime.Now.Date.AddDays(210), out string xmlContainer, out string xmlRequest);

                    log_inserir(id_cliente_neo + "- RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.ativacao_neo);
                    log_inserir(id_cliente_neo + "- RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.ativacao_neo);
                    log_inserir(id_cliente_neo + "- RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.ativacao_neo);

                    try
                    {
                        foreach (object obj in container.Items)
                        {

                            if (obj.GetType() == typeof(SubscriptionResponseItemCollection))
                            {
                                // ativado

                                SubscriptionResponseItemCollection item = new SubscriptionResponseItemCollection();
                                item = (SubscriptionResponseItemCollection)obj;

                                SubscriptionResponseItemCollectionActivate ativo = new SubscriptionResponseItemCollectionActivate();

                                ativo = (SubscriptionResponseItemCollectionActivate)item.Items[0];

                                activation_code = ativo.ActivationCode;

                                log_inserir("Codigo Ativado pela KL " + ativacao.subscriber_id + " - " + activation_code, (int)Lista_Erro.ativação_produto_neo);
                            }
                            else if (obj.GetType() == typeof(SubscriptionResponseErrorCollection))
                            {
                                SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                                itemErro = (SubscriptionResponseErrorCollection)obj;


                                foreach (var erro in itemErro.Items)
                                {
                                    log_inserir("Solicitacao de ativacao[2] - erro ativacao " + erro.ErrorMessage, (int)Lista_Erro.erro_neo);
                                }

                                usuario_neo.msg_erro = "Erro durante a ativação[1]";
                                return usuario_neo;
                            }
                            else if (obj.GetType() == typeof(TransactionErrorType))
                            {
                                TransactionErrorType itemErro = new TransactionErrorType();

                                itemErro = (TransactionErrorType)obj;

                                log_inserir("Solicitacao de ativacao[3] - erro ativacao " + itemErro.ErrorMessage, (int)Lista_Erro.erro_neo);

                                usuario_neo.msg_erro = "Erro durante a ativação[2]";
                                return usuario_neo;

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log_inserir("Solicitacao de ativacao[1] - erro ativacao " + ex.Message, (int)Lista_Erro.erro_neo);
                        usuario_neo.msg_erro = "Erro durante a ativação[3]";
                        return usuario_neo;
                    }

                    // link
                    //string link_android = link_download_neo(ativacao.subscriber_id, usuario_neo.nm_subscribe_id + "0" + DateTime.Now.ToString("yyyyMMddHHmmss"), PlatformEnum.Android);
                    //string link_win = link_download_neo(ativacao.subscriber_id, usuario_neo.nm_subscribe_id + "1" + DateTime.Now.ToString("yyyyMMddHHmmss"), PlatformEnum.Windows);
                    //string link_mac = link_download_neo(ativacao.subscriber_id, usuario_neo.nm_subscribe_id + "2" + DateTime.Now.ToString("yyyyMMddHHmmss"), PlatformEnum.macOS);
                    //string link_ios = link_download_neo(ativacao.subscriber_id, usuario_neo.nm_subscribe_id + "3" + DateTime.Now.ToString("yyyyMMddHHmmss"), PlatformEnum.iOS);

                    var cliente_ativar = inserir_cliente_ativacao(id_cliente_neo, "0", ativacao.subscriber_id, activation_code, id_produto_kl, ativacao.provedor_id);

                    usuario_neo.dv_ativo = 2;
                    usuario_neo.produto.Add(new Produto_Neo()
                    {
                        activation_code = activation_code,
                        //nm_link_dowload_android = @link_android,
                        //nm_link_dowload_windows = @link_win,
                        //nm_link_dowload_mac = @link_mac,
                        //nm_link_dowload_ios = @link_ios,
                        id_produto_kl = int.Parse(id_produto_kl),
                        id_status = "2",
                        qtd_licencas = qtd_licenças,
                        nm_produto_kl = nm_produto_kl,
                        dt_ativacao = retorna_data(DateTime.Now)
                    });
                    return usuario_neo;
                }
                else
                {
                    // consulta produtos combo
                    var produtos_Combo = consulta_combo(id_combo);

                    if (produtos_Combo.Rows.Count <= 0)
                    {
                        usuario_neo.msg_erro = "Erro durante a ativação[4]";
                        return usuario_neo;
                    }

                    List<Neo_Produto_Combo> produtos = new List<Neo_Produto_Combo>();

                    int UnitId = 1;
                    foreach (DataRow dr in produtos_Combo.Rows)
                    {
                        Random rand = new Random();

                        produtos.Add(new Neo_Produto_Combo() { _ProductId = dr["cd_produto"].ToString(),
                            _SubscriberId = rand.GetHashCode() + "_" + ativacao.subscriber_id + "-" + dr["id_produto"].ToString(),
                            qtd_licencas = dr["qtd_licencas"].ToString(),
                            UnitId = UnitId.ToString(),
                            id_produto = dr["id_produto"].ToString(),
                            id_produto_kl = id_produto_kl,
                            nm_produto = dr["nm_produto"].ToString()
                        });
                        UnitId++;
                    }

                    // ativa produtos
                    SubscriptionResponseContainer container = new SubscriptionResponseContainer();
                    container = con.AtivacaoNeoLote(ativacao.subscriber_id + DateTime.Now.ToString("yyMMddHHmmss"), produtos, DateTime.Now.Date.AddDays(210), out string xmlContainer, out string xmlRequest);

                    log_inserir(id_cliente_neo + "- RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.ativacao_neo_lote);
                    log_inserir(id_cliente_neo + "- RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.ativacao_neo_lote);
                    log_inserir(id_cliente_neo + "- RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.ativacao_neo_lote);

                    foreach (object obj in container.Items)
                    {

                        if (obj.GetType() == typeof(SubscriptionResponseItemCollection))
                        {
                            // faz um looping nas solicitações
                            SubscriptionResponseItemCollection itens = new SubscriptionResponseItemCollection();
                            itens = (SubscriptionResponseItemCollection)obj;

                            foreach (var item in itens.Items)
                            {
                                if (item.GetType() == typeof(SubscriptionResponseItemCollectionActivate))
                                {
                                    var itemDetalhe = (SubscriptionResponseItemCollectionActivate)item;
                                    // cancelou corretamente a licença
                                    var licenca = produtos.Where(x => x.UnitId.ToString() == itemDetalhe.UnitId).FirstOrDefault();

                                    var cliente_ativar = inserir_cliente_ativacao(id_cliente_neo, "0", itemDetalhe.SubscriberId, itemDetalhe.ActivationCode, id_produto_kl, licenca.id_produto, ativacao.provedor_id);

                                    usuario_neo.dv_ativo = 2;
                                    usuario_neo.produto.Add(new Produto_Neo()
                                    {
                                        activation_code = itemDetalhe.ActivationCode,
                                        //nm_link_dowload_android = @link_android,
                                        //nm_link_dowload_windows = @link_win,
                                        //nm_link_dowload_mac = @link_mac,
                                        //nm_link_dowload_ios = @link_ios,
                                        id_produto_kl = int.Parse(id_produto_kl),
                                        id_status = "2",
                                        qtd_licencas = qtd_licenças,
                                        nm_produto_kl = licenca.nm_produto,
                                        dt_ativacao = retorna_data(DateTime.Now),
                                        combo_id = id_combo
                                    });

                                    activation_code = itemDetalhe.ActivationCode;

                                    log_inserir("Codigo Ativado pela KL " + ativacao.subscriber_id + " - " + activation_code, (int)Lista_Erro.ativação_produto_neo);

                                }
                            }
                        }
                        else if (obj.GetType() == typeof(SubscriptionResponseErrorCollection))
                        {
                            SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                            itemErro = (SubscriptionResponseErrorCollection)obj;

                            foreach (var erro in itemErro.Items)
                            {
                                var licenca = produtos.Where(x => x.UnitId.ToString() == erro.UnitId).FirstOrDefault();

                                log_inserir("Solicitacao de ativacao[4] - erro ativacao " + erro.ErrorCode + "-" + erro.ErrorMessage, (int)Lista_Erro.erro_neo);
                                log_inserir(id_cliente_neo + "- RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.ativacao_neo_lote);
                                log_inserir(id_cliente_neo + "- RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.ativacao_neo_lote);
                                log_inserir(id_cliente_neo + "- RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.ativacao_neo_lote);

                            }
                        }
                        else if (obj.GetType() == typeof(TransactionErrorType))
                        {
                            TransactionErrorType itemErro = new TransactionErrorType();

                            itemErro = (TransactionErrorType)obj;

                            log_inserir("Solicitacao de ativacao[4] - erro ativacao " + itemErro.ErrorCode + "-" + itemErro.ErrorMessage, (int)Lista_Erro.erro_neo);
                            log_inserir(id_cliente_neo + "- RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.ativacao_neo_lote);
                            log_inserir(id_cliente_neo + "- RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.ativacao_neo_lote);
                            log_inserir(id_cliente_neo + "- RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.ativacao_neo_lote);

                            usuario_neo.msg_erro = "Erro durante a ativação[4]";
                            return usuario_neo;
                        }
                    }


                    return usuario_neo;
                }
            }
            catch (Exception ex)
            {
                log_inserir(ex.Message, (int)Lista_Erro.erro_neo);
                usuario_neo.msg_erro = "O processo de ativação não está disponivel no momento.";
                return usuario_neo;
            }
        }

        public string link_download_neo(string subscriber_id, string transaction_id, PlatformEnum plat)
        {
            string link = "";
            try
            {
                KL_Conexao con = new KL_Conexao();
                SubscriptionResponseContainer containerlink = new SubscriptionResponseContainer();

                containerlink = con.DownloadLinkNeo(plat, subscriber_id, transaction_id + "0" + DateTime.Now.ToString("yyMMddHHmmss"));

                bool ativado = true;

                foreach (object obj in containerlink.Items)
                {
                    if (obj.GetType() == typeof(SubscriptionResponseItemCollection))
                    {
                        // ativado

                        SubscriptionResponseItemCollection item = new SubscriptionResponseItemCollection();
                        item = (SubscriptionResponseItemCollection)obj;

                        SubscriptionResponseItemCollectionGetDownloadLinks ativo = new SubscriptionResponseItemCollectionGetDownloadLinks();

                        ativo = (SubscriptionResponseItemCollectionGetDownloadLinks)item.Items[0];

                        if (plat == PlatformEnum.Android)
                            link = ativo.DownloadLinks[0].Url.Replace("market://", "https://play.google.com/store/apps/");
                        else
                            link = ativo.DownloadLinks[0].Url;

                        log_inserir("Solicitacao de link - " + subscriber_id + " - link: " + link, (int)Lista_Erro.consulta_produto_neo);

                    }
                    else if (obj.GetType() == typeof(SubscriptionResponseErrorCollection))
                    {
                        SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                        itemErro = (SubscriptionResponseErrorCollection)obj;


                        foreach (var erro in itemErro.Items)
                        {
                            log_inserir("Solicitacao de ativacao - erro link " + erro.ErrorMessage, (int)Lista_Erro.erro_neo);
                        }

                        return link;
                    }
                    else if (obj.GetType() == typeof(TransactionErrorType))
                    {
                        TransactionErrorType itemErro = new TransactionErrorType();

                        itemErro = (TransactionErrorType)obj;

                        log_inserir("Solicitacao de ativacao[3] - erro link " + itemErro.ErrorMessage, (int)Lista_Erro.erro_neo);
                        return link;

                    }
                }
            }
            catch (Exception ex)
            {
                log_inserir("Erro link " + ex.Message, (int)Lista_Erro.erro_neo);
            }

            return link;
        }

        public List<Link_Produto> link_download_neoLote(List<Lista_Produto> subscriber_ids, string transaction_id)
        {
            List<Link_Produto> links = new List<Link_Produto>();
            try
            {
                KL_Conexao con = new KL_Conexao();
                SubscriptionResponseContainer containerlink = new SubscriptionResponseContainer();

                List<SubscriptionRequestGetDownloadLinksItem> Itens = new List<SubscriptionRequestGetDownloadLinksItem>();

                int contador = 1;
                foreach (var subscriber_id in subscriber_ids)
                {
                    links.Add(new Link_Produto() { subscribe_id = subscriber_id.subscribe_id });

                    SubscriptionRequestGetDownloadLinksItem Item = new SubscriptionRequestGetDownloadLinksItem();
                    Itens.Add(new SubscriptionRequestGetDownloadLinksItem()
                    {
                        UnitId = contador.ToString(),
                        SubscriberId = subscriber_id.subscribe_id, // "C8147E02-8A91-43B2-AD48-5D04DAFD38C2";
                        Language = "pt",
                        Platform = PlatformEnum.Android
                    });
                    contador++;
                    Itens.Add(new SubscriptionRequestGetDownloadLinksItem()
                    {
                        UnitId = contador.ToString(),
                        SubscriberId = subscriber_id.subscribe_id, // "C8147E02-8A91-43B2-AD48-5D04DAFD38C2";
                        Language = "pt",
                        Platform = PlatformEnum.Windows
                    });
                    contador++;
                    Itens.Add(new SubscriptionRequestGetDownloadLinksItem()
                    {
                        UnitId = contador.ToString(),
                        SubscriberId = subscriber_id.subscribe_id, // "C8147E02-8A91-43B2-AD48-5D04DAFD38C2";
                        Language = "pt",
                        Platform = PlatformEnum.iOS
                    });
                    contador++;
                    Itens.Add(new SubscriptionRequestGetDownloadLinksItem()
                    {
                        UnitId = contador.ToString(),
                        SubscriberId = subscriber_id.subscribe_id, // "C8147E02-8A91-43B2-AD48-5D04DAFD38C2";
                        Language = "pt",
                        Platform = PlatformEnum.macOS
                    });
                    contador++;
                }

                containerlink = con.DownloadLinkNeoLote(transaction_id + "0" + DateTime.Now.ToString("yyMMddHHmmss"), Itens.ToArray());


                foreach (object obj in containerlink.Items)
                {
                    if (obj.GetType() == typeof(SubscriptionResponseItemCollection))
                    {
                        // ativado

                        SubscriptionResponseItemCollection item = new SubscriptionResponseItemCollection();
                        item = (SubscriptionResponseItemCollection)obj;


                        foreach (var link in item.Items)
                        {
                            var itemDetalhe = (SubscriptionResponseItemCollectionGetDownloadLinks)link;

                            var produto = links.Where(x => x.subscribe_id == itemDetalhe.SubscriberId).FirstOrDefault();

                            switch (itemDetalhe.Platform)
                            {
                                case PlatformEnum.Android:
                                    produto.nm_link_dowload_android = itemDetalhe.DownloadLinks[0].Url.Replace("market://", "https://play.google.com/store/apps/");
                                    break;

                                case PlatformEnum.iOS:
                                    produto.nm_link_dowload_ios = itemDetalhe.DownloadLinks[0].Url;
                                    break;

                                case PlatformEnum.macOS:
                                    produto.nm_link_dowload_mac = itemDetalhe.DownloadLinks[0].Url;
                                    break;

                                case PlatformEnum.Windows:
                                    produto.nm_link_dowload_windows = itemDetalhe.DownloadLinks[0].Url;
                                    break;

                            }

                            log_inserir("Solicitacao de link - " + itemDetalhe.SubscriberId + " - link: " + Newtonsoft.Json.JsonConvert.SerializeObject(produto), 1010);
                        }



                    }
                    else if (obj.GetType() == typeof(SubscriptionResponseErrorCollection))
                    {
                        SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                        itemErro = (SubscriptionResponseErrorCollection)obj;


                        foreach (var erro in itemErro.Items)
                        {
                            log_inserir("Solicitacao de ativacao - erro link " + erro.ErrorMessage, -2);
                        }

                        //return link;
                    }
                    else if (obj.GetType() == typeof(TransactionErrorType))
                    {
                        TransactionErrorType itemErro = new TransactionErrorType();

                        itemErro = (TransactionErrorType)obj;

                        log_inserir("Solicitacao de ativacao[3] - erro link " + itemErro.ErrorMessage, -2);
                        return links;

                    }
                }
            }
            catch (Exception ex)
            {
                log_inserir("Erro link " + ex.Message, -2);
            }

            return links;
        }

        public Usuario_Neo CancelamentoNeo(string subscriber_id)
        {
            // cancela cliente Neo
            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = new KL_Conexao().CancelamentoNeo(subscriber_id + DateTime.Now.ToString("yyyyMMddHHmmss"), subscriber_id, out string xmlContainer, out string xmlRequest);

            log_inserir("CancelamentoNeo subscriber_id= " + subscriber_id + "- RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.cancelamento_neo);
            log_inserir("CancelamentoNeo subscriber_id= " + subscriber_id + "- RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.cancelamento_neo);
            log_inserir("CancelamentoNeo subscriber_id= " + subscriber_id + "- RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.cancelamento_neo);

            return cliente_neo(subscriber_id);
        }

        public Usuario_Neo CancelamentoNeoLote(string[] subscriber_id_lote)
        {
            string subscriber_id = string.Empty;
            if (subscriber_id_lote.Count() > 0)
            {
                subscriber_id = subscriber_id_lote[0].Substring(0, subscriber_id_lote[0].IndexOf("-"));
            }

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = new KL_Conexao().CancelamentoNeoLote(subscriber_id + DateTime.Now.ToString("yyyyMMddHHmmss"), subscriber_id_lote, out string xmlContainer, out string xmlRequest);

            string subscriber_id_lote_join = string.Join(" ", subscriber_id_lote);

            log_inserir("CancelamentoNeoLote subscriber_id= " + subscriber_id_lote_join + "- RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.cancelamento_neo_lote);
            log_inserir("CancelamentoNeoLote subscriber_id= " + subscriber_id_lote_join + "- RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.cancelamento_neo_lote);
            log_inserir("CancelamentoNeoLote subscriber_id= " + subscriber_id_lote_join + "- RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.cancelamento_neo_lote);

            return cliente_neo(subscriber_id);
        }

        public string DadosNeo(string[] subscriber_id)
        {
            // cancela cliente Neo
            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = new KL_Conexao().GetInfoNeo(subscriber_id[0] + DateTime.Now.ToString("yyyyMMddHHmmss"), subscriber_id, out string xmlContainer, out string xmlRequest);

            log_inserir("GetInfoNeo subscriber_id= " + subscriber_id + "- RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.get_info_neo);
            log_inserir("GetInfoNeo subscriber_id= " + subscriber_id + "- RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.get_info_neo);
            log_inserir("GetInfoNeo subscriber_id= " + subscriber_id + "- RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.get_info_neo);

            return "OK";
        }

        #endregion Ações

        #region Consultas

        public Usuario_Neo cliente_neo(string subscriber_id)
        {
            using (var client = new HttpClient())
            {
                var usuario_neo = new Usuario_Neo();
                usuario_neo.produto = new List<Produto_Neo>();
                string id_cliente = "";

                // consulta usuario
                var cliente = consulta_cliente(subscriber_id);
                usuario_neo.dv_ativo = 1;
                usuario_neo.nm_subscribe_id = subscriber_id;
                string nm_subrscribe_kl = "";


                id_cliente = cliente.Rows[0]["id_cliente_neo"].ToString();

                var produto_ativado = consulta_cliente_ativacao(id_cliente);

                if (produto_ativado.Rows.Count == 0)
                {
                    var produto = consulta_produto("0");

                    foreach (DataRow dr in produto.Rows)
                    {
                        Produto_Consulta_Neo prod = new Produto_Consulta_Neo() { subscriber_id = usuario_neo.nm_subscribe_id, resource_id = dr["nm_urn"].ToString() };
                        var response_produto = client.PostAsync(@ConfigurationManager.AppSettings["link_produto_neo"], new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(prod), Encoding.UTF8, "application/json")).Result;

                        if (response_produto.Content != null)
                        {
                            var response_produto_auth = response_produto.Content.ReadAsStringAsync().Result;

                            var obj_response_produto_auth = Newtonsoft.Json.JsonConvert.DeserializeObject<response_auth_neo>(response_produto_auth);

                            if (obj_response_produto_auth.access)
                            {
                                if (dr["id_combo"].ToString() == "0")
                                {
                                    usuario_neo.produto.Add(new Produto_Neo()
                                    {
                                        id_produto_kl = int.Parse(dr["id_produto_kl"].ToString()),
                                        resource_id = dr["nm_urn"].ToString(),
                                        id_status = "1",
                                        nm_produto_kl = dr["nm_produto_kl"].ToString(),
                                        link_image = "/images/" + produto.Rows[0]["nm_imagem"].ToString(),
                                        nm_descricao = dr["descricao"].ToString(),
                                    });
                                }
                                else // Se o produto for o combo, busca os produtos do combo
                                {
                                    var produtos_combo = consulta_combo(dr["id_combo"].ToString());

                                    foreach (DataRow dr_combo in produtos_combo.Rows)
                                    {
                                        usuario_neo.produto.Add(new Produto_Neo()
                                        {
                                            combo_id = dr_combo["id_combo"].ToString(),
                                            id_produto_kl = int.Parse(dr_combo["id_produto"].ToString()),
                                            resource_id = dr["nm_urn"].ToString(),
                                            id_status = "1",
                                            nm_produto_kl = dr_combo["nm_produto"].ToString(),
                                            link_image = "/images/" + dr_combo["nm_imagem"].ToString(),
                                            nm_descricao = dr_combo["descricao"].ToString(),
                                        });
                                    }

                                }
                            }
                        }
                    }

                    return usuario_neo;
                }
                else // retorna o produto ja cadastrado se tiver
                {
                    //verifica se produto ainda esta ativo
                    nm_subrscribe_kl = produto_ativado.Rows[0]["nm_subrscribe_kl"].ToString();
                    var produto = consulta_produto(produto_ativado.Rows[0]["id_produto_kl"].ToString());

                    Produto_Consulta_Neo prod = new Produto_Consulta_Neo() { subscriber_id = usuario_neo.nm_subscribe_id, resource_id = produto.Rows[0]["nm_urn"].ToString() };
                    string id_combo = produto.Rows[0]["id_combo"].ToString();

                    var response_produto = client.PostAsync(@ConfigurationManager.AppSettings["link_produto_neo"], new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(prod), Encoding.UTF8, "application/json")).Result;

                    if (response_produto.Content != null)
                    {
                        var response_produto_auth = response_produto.Content.ReadAsStringAsync().Result;

                        var obj_response_produto_auth = Newtonsoft.Json.JsonConvert.DeserializeObject<response_auth_neo>(response_produto_auth);

                        // se ainda tiver, pega o link de download
                        if (obj_response_produto_auth.access)
                        {
                            usuario_neo.dv_ativo = 2;

                            // se nao for combo, chama o link
                            if (id_combo == "0")
                            {
                                List<Lista_Produto> nm_subrscribe_kls = new List<Lista_Produto>();
                                nm_subrscribe_kls.Add(new Lista_Produto() { subscribe_id = nm_subrscribe_kl, id_produto = 0 });

                                var links = new List<Link_Produto>();


                                if (false) // Verifica se links estão salvos
                                {

                                }
                                else
                                {
                                    links = link_download_neoLote(nm_subrscribe_kls, usuario_neo.nm_subscribe_id);
                                }

                                usuario_neo.produto.Add(new Produto_Neo()
                                {
                                    id_produto_kl = int.Parse(produto.Rows[0]["id_produto_kl"].ToString()),
                                    id_status = "2",
                                    nm_produto_kl = produto.Rows[0]["nm_produto_kl"].ToString(),
                                    nm_link_dowload_android = links[0].nm_link_dowload_android,
                                    nm_link_dowload_windows = links[0].nm_link_dowload_windows,
                                    nm_link_dowload_mac = links[0].nm_link_dowload_mac,
                                    nm_link_dowload_ios = links[0].nm_link_dowload_ios,
                                    activation_code = produto_ativado.Rows[0]["activation_code"].ToString(),
                                    dt_ativacao = retorna_data(DateTime.Parse(produto_ativado.Rows[0]["dt_ativacao"].ToString())),
                                    resource_id = produto.Rows[0]["nm_urn"].ToString(),
                                    link_image = "/images/" + produto.Rows[0]["nm_imagem"].ToString(),
                                    nm_descricao = produto.Rows[0]["descricao"].ToString()
                                });
                            }
                            else // se for combo
                            {
                                // puxa a lista de produtos
                                var produtos_Combo = consulta_combo(id_combo);

                                if (produtos_Combo.Rows.Count <= 0)
                                {
                                    usuario_neo.msg_erro = "Erro durante a consulta de comnbo[5]";
                                    return usuario_neo;
                                }

                                List<Lista_Produto> nm_subrscribe_kls = new List<Lista_Produto>();

                                foreach (DataRow dr in produtos_Combo.Rows)
                                {
                                    foreach (DataRow produto_ativado_row in produto_ativado.Rows)
                                    {
                                        string subscribe_id_split = produto_ativado_row["nm_subrscribe_kl"].ToString().Split('-')[1];

                                        if (subscribe_id_split == dr["id_produto"].ToString())
                                        {
                                            nm_subrscribe_kls.Add(new Lista_Produto() { subscribe_id = produto_ativado_row["nm_subrscribe_kl"].ToString(), id_produto = int.Parse(dr["id_produto"].ToString()) });
                                        }
                                    }

                                    string activation_code = string.Empty;
                                    foreach (DataRow item in produto_ativado.Rows)
                                    {
                                        string nm_subscribe_kl = item["nm_subrscribe_kl"].ToString();
                                        string nm_subscribe_kl_cut = nm_subscribe_kl.Substring(nm_subscribe_kl.IndexOf('-') + 1, 2);

                                        if (nm_subscribe_kl_cut == dr["id_produto"].ToString())
                                        {
                                            activation_code = item["activation_code"].ToString();
                                        }
                                    }

                                    usuario_neo.produto.Add(new Produto_Neo()
                                    {
                                        combo_id = id_combo,
                                        id_produto_kl = int.Parse(produto_ativado.Rows[0]["id_produto_kl"].ToString()),
                                        id_produto = int.Parse(dr["id_produto"].ToString()),
                                        id_status = "2",
                                        nm_produto_kl = dr["nm_produto"].ToString(),
                                        //nm_link_dowload_android = links.nm_link_dowload_android,
                                        //nm_link_dowload_windows = links.nm_link_dowload_windows,
                                        //nm_link_dowload_mac = links.nm_link_dowload_mac,
                                        //nm_link_dowload_ios = links.nm_link_dowload_ios,
                                        activation_code = activation_code,
                                        dt_ativacao = retorna_data(DateTime.Parse(produto_ativado.Rows[0]["dt_ativacao"].ToString())),
                                        resource_id = dr["nm_urn"].ToString(),
                                        link_image = "/images/" + dr["nm_imagem"].ToString(),
                                        nm_descricao = dr["descricao"].ToString()
                                    });
                                }

                                var links = link_download_neoLote(nm_subrscribe_kls, usuario_neo.nm_subscribe_id);

                                foreach (var link in links)
                                {
                                    var id_produto = nm_subrscribe_kls.Where(x => x.subscribe_id == link.subscribe_id).FirstOrDefault();
                                    var produto_neo = usuario_neo.produto.Where(x => x.id_produto == id_produto.id_produto).FirstOrDefault();

                                    produto_neo.nm_link_dowload_android = link.nm_link_dowload_android;
                                    produto_neo.nm_link_dowload_ios = link.nm_link_dowload_ios;
                                    produto_neo.nm_link_dowload_mac = link.nm_link_dowload_mac;
                                    produto_neo.nm_link_dowload_windows = link.nm_link_dowload_windows;

                                    foreach (var item in usuario_neo.produto)
                                    {
                                        if (item.id_produto == produto_neo.id_produto)
                                        {
                                            item.nm_link_dowload_android = produto_neo.nm_link_dowload_android;
                                            item.nm_link_dowload_ios = produto_neo.nm_link_dowload_ios;
                                            item.nm_link_dowload_mac = produto_neo.nm_link_dowload_mac;
                                            item.nm_link_dowload_windows = produto_neo.nm_link_dowload_windows;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            // cancel produto
                        }
                    }

                    //StringBuilder sb = new StringBuilder();
                    //foreach (var item in usuario_neo.produto)
                    //{
                    //    sb.Append(String.Format("{0}, {1}, {2}" + Environment.NewLine, item.id_produto, item.resource_id, item.nm_link_dowload_android));
                    //}
                    //File.AppendAllText("C:\\arq\\Deploy\\2022-11-13\\" + "log.txt", sb.ToString());
                    //sb.Clear();
                    return usuario_neo;
                }
            }
        }
        public DataTable consulta_produto(string id_produto_kl)
        {
            DataBase db = new DataBase();
            db.procedure = "p_produto_lista";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@id_produto_kl", id_produto_kl));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable consulta_operadora(string id_operadora_neo)
        {
            DataBase db = new DataBase();
            db.procedure = "p_operadora_lista";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@id_operadora_neo", id_operadora_neo));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public List<Operadora_Neo> consulta_operadora_dinamico()
        {
            var operadoras = new List<Operadora_Neo>();
            using (var client = new HttpClient())
            {
                // looping produtos
                var produto = consulta_produto("0");

                foreach (DataRow dt in produto.Rows)
                {
                    var response_operadora = client.GetAsync(@ConfigurationManager.AppSettings["link_operadora"] + dt["nm_urn"].ToString()).Result;

                    if (response_operadora.Content != null)
                    {
                        var response_opera = response_operadora.Content.ReadAsStringAsync().Result;

                        var obj_response_produto_auth = Newtonsoft.Json.JsonConvert.DeserializeObject<List<operadora_neo>>(response_opera);

                        foreach (var operadora in obj_response_produto_auth)
                        {
                            var existe = operadoras.Where(check => check.nm_cod_operadora_neo.Equals(operadora.id));

                            if (existe.Count() == 0)
                                operadoras.Add(new Operadora_Neo() { nm_cod_operadora_neo = operadora.id, nm_operadora_neo = operadora.name });
                        }

                    }
                }

            }

            return operadoras;

        }


        public DataTable consulta_cliente(string nm_subrscribe_id)
        {
            DataBase db = new DataBase();
            db.procedure = "p_cliente_consulta";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@nm_subrscribe_id", nm_subrscribe_id));
            //par.Add(db.retorna_parametros("@dt_expiracao_kl", dt_expiracao_kl));
            //par.Add(db.retorna_parametros("@dt_encerramento", dt_encerramento));
            //par.Add(db.retorna_parametros("@nm_link_url", nm_link_url));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable consulta_combo(string id_combo)
        {
            DataBase db = new DataBase();
            db.procedure = "p_produto_combo";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@id_combo", id_combo));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable consulta_cliente_ativacao(string id_cliente_neo)
        {
            DataBase db = new DataBase();
            db.procedure = "p_cliente_ativacao_consulta";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@id_cliente_neo", id_cliente_neo));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        #endregion Consultas

        #region inserir
        public DataTable inserir_cliente(string nm_subrscribe_id)
        {
            DataBase db = new DataBase();
            db.procedure = "p_cliente_inserir";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@nm_subrscribe_id", nm_subrscribe_id));
            //par.Add(db.retorna_parametros("@dt_expiracao_kl", dt_expiracao_kl));
            //par.Add(db.retorna_parametros("@dt_encerramento", dt_encerramento));
            //par.Add(db.retorna_parametros("@nm_link_url", nm_link_url));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable inserir_cliente_ativacao(string id_cliente_neo, string id_operadora_neo, string nm_subrscribe_kl, string activation_code, string id_produto_kl, string id_produto = "0", string id_provedor = "0")
        {
            DataBase db = new DataBase();
            db.procedure = "p_cliente_ativacao_inserir";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@id_cliente_neo", id_cliente_neo));
            par.Add(db.retorna_parametros("@id_operadora_neo", id_operadora_neo));
            par.Add(db.retorna_parametros("@nm_subrscribe_kl", nm_subrscribe_kl));
            par.Add(db.retorna_parametros("@activation_code", activation_code));
            par.Add(db.retorna_parametros("@id_produto_kl", id_produto_kl));
            par.Add(db.retorna_parametros("@id_produto", id_produto));
            par.Add(db.retorna_parametros("@id_provedor", id_provedor));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        #endregion inserir

        public string retorna_data(DateTime data)
        {
            string retorno = data.Day.ToString() + " de ";

            switch (data.Month)
            {
                case 1:
                    retorno += "jan de " + data.Year.ToString();
                    break;
                case 2:
                    retorno += "fev de " + data.Year.ToString();
                    break;
                case 3:
                    retorno += "mar de " + data.Year.ToString();
                    break;
                case 4:
                    retorno += "abr de " + data.Year.ToString();
                    break;
                case 5:
                    retorno += "mai de " + data.Year.ToString();
                    break;
                case 6:
                    retorno += "jun de " + data.Year.ToString();
                    break;
                case 7:
                    retorno += "jul de " + data.Year.ToString();
                    break;
                case 8:
                    retorno += "ago de " + data.Year.ToString();
                    break;
                case 9:
                    retorno += "set de " + data.Year.ToString();
                    break;
                case 10:
                    retorno += "out de " + data.Year.ToString();
                    break;
                case 11:
                    retorno += "nov de " + data.Year.ToString();
                    break;
                case 12:
                    retorno += "dez de " + data.Year.ToString();
                    break;
            }

            return retorno;
        }

        #region Log
        public void log_inserir(string nm_log, int id_tipo_log)
        {
            DataBase db = new DataBase();
            db.procedure = "p_insere_log";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@nm_log", nm_log));
            par.Add(db.retorna_parametros("@id_tipo_log", id_tipo_log.ToString()));
            db.parametros = par;

            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_AV);
        }

        #endregion
    }

    public class Login_Neo
    {
        public string idp { set; get; }

        public string password { set; get; }

        public string username { set; get; }

        public string subscriber_id { set; get; }
    }

    public class Info_Neo
    {
        public List<string> subscriber_id { set; get; }
    }

    public class Ativacao_Neo
    {
        public string subscriber_id { set; get; }

        public string resource_id { set; get; }

        public string provedor_id { get; set; }
    }

    public class Produto_Consulta_Neo
    {
        public string subscriber_id { set; get; }

        public string resource_id { set; get; }
    }

    public class response_auth_neo
    {
        public bool access { set; get; }
        public string subscriber_id { set; get; }
        public error_neo error { set; get; }
    }

    public class error_neo
    {
        public string errorCode { set; get; }
        public string details { set; get; }
    }

    public class operadora_neo
    {
        public string id { set; get; }
        public string name { set; get; }
    }

    public class Usuario_Neo
    {
        public string nm_subscribe_id { set; get; }

        public int dv_ativo { set; get; }

        public string msg_erro { set; get; }

        public List<Produto_Neo> produto { set; get; }
    }

    public class Produto_Neo
    {
        public int id_produto_kl { set; get; }
        public int id_produto { set; get; }
        public string combo_id { set; get; }
        public string qtd_licencas { set; get; }
        public string nm_produto_kl { set; get; }
        public string nm_link_dowload_android { set; get; }
        public string nm_link_dowload_windows { set; get; }
        public string nm_link_dowload_mac { set; get; }
        public string nm_link_dowload_ios { set; get; }
        public string resource_id { set; get; }
        public string id_status { set; get; }
        public string activation_code { set; get; }
        public string dt_ativacao { set; get; }
        public string link_image { set; get; }
        public string nm_descricao { set; get; }
    }

    public class Operadora_Neo
    {
        public int id_operadora_neo { set; get; }

        public string nm_operadora_neo { set; get; }

        public string nm_cod_operadora_neo { set; get; }
    }

    public class Lista_Produto
    {
        public int id_produto { set; get; }

        public string subscribe_id { set; get; }
    }

    public class Link_Produto
    {
        public string nm_link_dowload_android { set; get; }
        public string nm_link_dowload_windows { set; get; }
        public string nm_link_dowload_mac { set; get; }
        public string nm_link_dowload_ios { set; get; }
        public string subscribe_id { set; get; }
    }

    public class CancelaSubscriberIdLote
    {
        public List<string> subscriber_id { set; get; }
    }

}
