using BLL;
using BLL.KL_API;
using DAL;
using Newtonsoft.Json;
using Objetos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Xml.Linq;

namespace KL_API.Models
{
    public enum Lista_Erro
    {
        license_ativation = 2000,
        license_cancel = 2001,
        usar_add = 2002,
        user_delete = 2003,
        login = 2004
            

    }
    public class Ativacao_Envio
    {
        public string api_token { set; get; }

        public string cd_validacao { set; get; }
    }

    public class LicenseActivation
    {
        public string UserID { set; get; }
        public string ProductID { set; get; }
        public DateTime StartDate { set; get; }
        public string EndDate { get; set; }
        public bool IgnoreRegister { set; get; } = false;
    }

    public class LicenseCancel
    {
        public string UserID { set; get; }
        public string ProductID { set; get; }
    }

    public class UserAdd
    {
        public string UserID { set; get; }
        public string Email { set; get; }
        public DateTime StartDate { set; get; }
        public string EndDate { set; get; }
        public string UserDocument { set; get; }
        public string UserPlan { set; get; }
        public List<Produto_UserAdd> ProductList { set; get; }
    }

    public class Produto_UserAdd
    {
        public string ProductID { set; get; }
    }

    public class UserAdd_Retorno
    {
        public int cod_retorno { set; get; }
        public string msg_retorno { set; get; }
        public List<Produto_Ativacao_Retorno> produtos { set; get; }
    }

    public class UserDelete
    {
        public string UserID { set; get; }
    }

    public class UserDelete_Retorno
    {
        public int cod_retorno { set; get; }
        public string msg_retorno { set; get; }
    }

    public class Ativacao_Retorno
    {
        public int cod_retorno { set; get; }

        public int dv_ativado { set; get; }

        public string msg_retorno { set; get; }

        public List<Produto_Ativacao_Retorno> produtos { set; get; }
    }

    public class Produto_Ativacao_Retorno
    {
        public bool ativado { set; get; }
        public string nome_produto { set; get; }
        public string urn_produto { set; get; }
        public string link_ativacao_android { set; get; }
        public string link_ativacao_windows { set; get; }
        public string link_ativacao_iphone { set; get; }
        public string link_ativacao_mac { set; get; }
        public string chave_ativacao { set; get; }
        public string cd_produto { set; get; }
    }

    public class LicenseCancel_Retorno
    {
        public int cod_retorno { set; get; }

        public string msg_retorno { set; get; }
    }


    public class ClientInfo
    {
        public bool valido { set; get; }
        public int id_cliente { set; get; }
        public int id_cliente_certificado { set; get; }
        public string nm_thumbprint { set; get; }
        public string nm_usuario_certificado { set; get; }
        public string nm_senha_certificado { set; get; }
    }

    public class Controle_Envio
    {
        public string id_cliente_usuario { set; get; }
        public int UnitId { set; get; }
        public comando_kl comando { set; get; }
        public string SubscribeId { set; get; }
        public string id_produto_kl { set; get; }
        public string id_cliente_licenca { set; get; }
        public string nm_produto { set; get; }
        public string urn_produto { set; get; }
    }

    public class LoginRetorno
    {
        public int cod_retorno { get; set; }
        public string nm_subscribe_id { set; get; }

        public int dv_ativo { set; get; }

        public string msg_retorno { set; get; }

        public List<Produto_Ativacao_Retorno> produtos { set; get; }
    }

    public class Login
    {
        public string username { set; get; }

        public string password { set; get; }
    }

    public class LoginInterno
    {
        public string sys { get; set; }
        public string token { set; get; }
        public string password { get; set; }
        public int cd_servico { get; set; }
    }

    public class MKWSAutenticacao
    {
        public string Expire { get; set; }
        public int LimiteUso { get; set; }
        public int[] ServicosAutorizados { get; set; }
        public string token { get; set; }
        public string status { get; set; }
    }

    public class ContratosAtivo
    {
        public string adesao { get; set; }
        public object cd_empresa { get; set; }
        public int codcontrato { get; set; }
        public string nome_empresa { get; set; }
        public string plano_acesso { get; set; }
        public string previsao_vencimento { get; set; }
    }

    public class WSMKContratosPorCliente
    {
        public int CodigoPessoa { get; set; }
        public List<ContratosAtivo> ContratosAtivos { get; set; }
        public string Nome { get; set; }
        public string status { get; set; }
    }

    public class WSMKUserSenhaSAC
    {
        public string AcessoSAC { get; set; }
        public int CodigoPessoa { get; set; }
        public string Nome { get; set; }
        public string status { get; set; }
    }


    public enum comando_kl
    {
        ativar = 1,
        cancelar_hard = 2,
        cancelar_soft = 3,
        link_android = 4,
        link_windows = 5,
        link_iphone = 6,
        link_mac = 7,
        renovar = 8
    }

    public class Ativacao_Controle
    {
        public UserDelete_Retorno deleteUser(UserDelete usuario, ClientInfo client)
        {
            // consulta de cliente existe e/ou o produto que ele te esta ativado
            var dt_produto_cliente = seleciona_licenca_produto(client.id_cliente, usuario.UserID, "all");

            if (dt_produto_cliente.Rows.Count == 0)
            {
                return new UserDelete_Retorno() { cod_retorno = -1, msg_retorno = "Usuario não encontrado ou sem licenca ativa." };
            }

            List<object> comandos = new List<object>();
            int count = 1;
            List<Controle_Envio> controle = new List<Controle_Envio>();
            string TransactionId = dt_produto_cliente.Rows[0]["nm_transaction_id"].ToString();
            // cancela os produtos em lote
            foreach (DataRow dr in dt_produto_cliente.Rows)
            {
                var cancelamento_licenca = new KL_Conexao().KL_retorna_cancelamento_hard(dr["nm_subscriber_id"].ToString(),
                                        DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z"),
                                        count.ToString());

                controle.Add(new Controle_Envio() { comando = comando_kl.cancelar_hard, UnitId = count, SubscribeId = dr["nm_subscriber_id"].ToString(), id_cliente_licenca = dr["id_cliente_licenca"].ToString(), id_cliente_usuario = dr["id_cliente_usuario"].ToString() });
                comandos.Add((object)cancelamento_licenca);
                count++;
            }

            string xmlRequest, xmlContainer;

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = new KL_Conexao().Comando_KL(TransactionId + DateTime.Now.ToString("yyyyMMddHHmmssffffff"), client.nm_usuario_certificado, client.nm_senha_certificado, client.nm_thumbprint, comandos.ToArray(), out xmlContainer, out xmlRequest);

            log_inserir(client.id_cliente.ToString() + "- RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.user_delete);
            log_inserir(client.id_cliente.ToString() + "- RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.user_delete);
            log_inserir(client.id_cliente.ToString() + "- RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.user_delete);

            try
            {
                foreach (object obj in container.Items)
                {

                    if (obj.GetType() == typeof(SubscriptionResponseItemCollection))
                    {
                        // faz um looping nas solicitações
                        SubscriptionResponseItemCollection itens = new SubscriptionResponseItemCollection();
                        itens = (SubscriptionResponseItemCollection)obj;

                        foreach (var item in itens.Items)
                        {
                            if (item.GetType() == typeof(BaseResponseItemType))
                            {
                                var itemDetalhe = (BaseResponseItemType)item;
                                // cancelou corretamente a licença
                                var licenca = controle.Where(x => x.UnitId.ToString() == itemDetalhe.UnitId).FirstOrDefault();

                                cancela_licenca_produto(licenca.id_cliente_licenca);

                                log_inserir(client.id_cliente.ToString() + " - Licença cancelada - " + licenca.id_cliente_licenca, (int)Lista_Erro.user_delete);
                            }
                        }

                    }
                    else if (obj.GetType() == typeof(SubscriptionResponseErrorCollection))
                    {
                        SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                        itemErro = (SubscriptionResponseErrorCollection)obj;

                        foreach (var erro in itemErro.Items)
                        {
                            var licenca = controle.Where(x => x.UnitId.ToString() == erro.UnitId).FirstOrDefault();
                            log_inserir(client.id_cliente.ToString() + " - ERRO ao cancelada Licença - " + licenca.id_cliente_licenca + " - " + erro.ErrorCode + "-" + erro.ErrorMessage, (int)Lista_Erro.user_delete);
                        }

                        return new UserDelete_Retorno() { cod_retorno = -2, msg_retorno = "Não foi possivel cancelar todas as licenças." };
                    }
                    else if (obj.GetType() == typeof(TransactionErrorType))
                    {
                        TransactionErrorType itemErro = new TransactionErrorType();

                        itemErro = (TransactionErrorType)obj;
                        log_inserir(client.id_cliente.ToString() + " - ERRO ao cancelada Licença - " + controle[0].id_cliente_usuario.ToString() + " - " + itemErro.ErrorCode + "-" + itemErro.ErrorMessage, (int)Lista_Erro.license_cancel);

                        return new UserDelete_Retorno() { cod_retorno = -3, msg_retorno = "Ocorreu um erro na solicitação de cancelamento." };

                    }

                }
            }
            catch (Exception ex)
            {
                log_inserir(client.id_cliente.ToString() + " - ERRO ao cancelada Licença - " + controle[0].id_cliente_usuario.ToString() + " - " + ex.Message, (int)Lista_Erro.user_delete);
                return new UserDelete_Retorno() { cod_retorno = -4, msg_retorno = "Ocorreu um erro na solicitação de cancelamento." };
            }

            // Deleta usuario 
            ClienteDeletar(dt_produto_cliente.Rows[0]["id_cliente_licenca"].ToString());
            log_inserir(client.id_cliente.ToString() + " - USUARIO EXCLUIDO - " + controle[0].id_cliente_usuario.ToString(), (int)Lista_Erro.user_delete);

            return new UserDelete_Retorno() { cod_retorno = 0, msg_retorno = "" };

        }

        public void Activation(UserAdd usuario, ClientInfo client) 
        { 
            
        }

        public DataTable addUser(UserAdd usuario, ClientInfo client)
        {
            // Cadastra usuario
            var dt_usuario = ClienteUsuarioInserir(client.id_cliente, usuario.UserID, usuario.Email, usuario.StartDate, usuario.EndDate, usuario.UserDocument, usuario.UserPlan);

            return dt_usuario;
        }

        public UserAdd_Retorno addUserActivate(UserAdd usuario, ClientInfo client, DataTable dt_usuario)
        {
            List<Produto_Ativacao_Retorno> produtos = new List<Produto_Ativacao_Retorno>();

            string id_cliente_usuario = dt_usuario.Rows[0]["id_cliente_usuario"].ToString();

            if (id_cliente_usuario == "-1")
            {
                return new UserAdd_Retorno() { cod_retorno = -1, msg_retorno = "Usuario já cadastrado." };
            }
            string TransactionId = dt_usuario.Rows[0]["nm_transaction_id"].ToString() + DateTime.Now.ToString("yyyyMMddHHmmssffffff");

            // looping do produto recebido
            var dt_produto_ativacao = seleciona_produto_cliente(client.id_cliente, client.id_cliente_certificado);

            List<object> comandos = new List<object>();
            int count = 1;
            List<Controle_Envio> controle = new List<Controle_Envio>();
            var produto_ativado = new List<Produto_Ativacao_Retorno>();
            // ativa o produto, sem os links
            foreach (Produto_UserAdd produto in usuario.ProductList)
            {
                var dt_produto = dt_produto_ativacao.Select("nm_urn = '" + produto.ProductID + "'");

                if (dt_produto == null || dt_produto.Length == 0)
                {
                    // se nao achou, deleta o usuario
                    ClienteDeletar(id_cliente_usuario, 1);
                    return new UserAdd_Retorno() { cod_retorno = -1, msg_retorno = "Produto " + produto.ProductID + " não encontrado." };
                }

                string subscription_id = "cad-" + client.id_cliente.ToString() + "-" + id_cliente_usuario + "-" + dt_produto[0]["id_produto_kl"].ToString();

                string endTimeParam = "indefinite";

                if (!String.IsNullOrEmpty(usuario.EndDate))
                {
                    endTimeParam = usuario.EndDate;
                }

                var ativacao = new KL_Conexao().KL_retorna_ativacao(dt_produto[0]["qtd_licencas"].ToString(),
                                                                    dt_produto[0]["cd_produto_kl"].ToString(),
                                                                    DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z"),
                                                                    endTimeParam,
                                                                    count.ToString(),
                                                                    false,
                                                                    subscription_id);

                controle.Add(new Controle_Envio()
                {
                    comando = comando_kl.ativar,
                    UnitId = count,
                    SubscribeId = subscription_id,
                    id_produto_kl = dt_produto[0]["id_produto_kl"].ToString(),
                    id_cliente_usuario = id_cliente_usuario,
                    urn_produto = produto.ProductID
                });

                comandos.Add((object)ativacao);
                count++;

                var link_android = new KL_Conexao().KL_retorna_link(subscription_id,
                                         count.ToString(),
                                        "pt",
                                        PlatformEnum.Android);

                controle.Add(new Controle_Envio() { comando = comando_kl.link_android, UnitId = count, SubscribeId = subscription_id, id_cliente_usuario = id_cliente_usuario, urn_produto = produto.ProductID, nm_produto = dt_produto[0]["nm_produto_kl"].ToString() });
                comandos.Add((object)link_android);
                count++;

                var link_windows = new KL_Conexao().KL_retorna_link(subscription_id,
                                         count.ToString(),
                                        "pt",
                                        PlatformEnum.Windows);

                controle.Add(new Controle_Envio() { comando = comando_kl.link_windows, UnitId = count, SubscribeId = subscription_id, id_cliente_usuario = id_cliente_usuario, urn_produto = produto.ProductID, nm_produto = dt_produto[0]["nm_produto_kl"].ToString() });
                comandos.Add((object)link_windows);
                count++;

                var link_ios = new KL_Conexao().KL_retorna_link(subscription_id,
                                        count.ToString(),
                                       "pt",
                                       PlatformEnum.iOS);

                controle.Add(new Controle_Envio() { comando = comando_kl.link_iphone, UnitId = count, SubscribeId = subscription_id, id_cliente_usuario = id_cliente_usuario, urn_produto = produto.ProductID, nm_produto = dt_produto[0]["nm_produto_kl"].ToString() });
                comandos.Add((object)link_ios);
                count++;

                var link_mac = new KL_Conexao().KL_retorna_link(subscription_id,
                                        count.ToString(),
                                       "pt",
                                       PlatformEnum.macOS);

                controle.Add(new Controle_Envio() { comando = comando_kl.link_mac, UnitId = count, SubscribeId = subscription_id, id_cliente_usuario = id_cliente_usuario, urn_produto = produto.ProductID, nm_produto = dt_produto[0]["nm_produto_kl"].ToString() });
                comandos.Add((object)link_mac);
                count++;

                produto_ativado.Add(new Produto_Ativacao_Retorno()
                {
                    ativado = false,
                    nome_produto = dt_produto[0]["nm_produto_kl"].ToString(),
                    urn_produto = produto.ProductID,
                    link_ativacao_android = "",
                    link_ativacao_iphone = "",
                    link_ativacao_mac = "",
                    link_ativacao_windows = ""
                });
            }

            string xmlRequest, xmlContainer;

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = new KL_Conexao().Comando_KL(TransactionId, client.nm_usuario_certificado, client.nm_senha_certificado, client.nm_thumbprint, comandos.ToArray(), out xmlContainer, out xmlRequest);

            log_inserir(client.id_cliente.ToString() + "- RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.usar_add);
            log_inserir(client.id_cliente.ToString() + "- RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.usar_add);
            log_inserir(client.id_cliente.ToString() + "- RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.usar_add);

            try
            {
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
                                var licenca = controle.Where(x => x.UnitId.ToString() == itemDetalhe.UnitId).FirstOrDefault();

                                string id_cliente_licenca = ClienteLicencaInserir("0",
                                    licenca.id_cliente_usuario,
                                    licenca.id_produto_kl,
                                    itemDetalhe.ActivationCode,
                                    licenca.SubscribeId,
                                    "",
                                    "",
                                    "",
                                    "",
                                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                    "",
                                    2);

                                var produto = produto_ativado.Where(x => x.urn_produto == licenca.urn_produto).FirstOrDefault();

                                produto.ativado = true;
                                produto.chave_ativacao = itemDetalhe.ActivationCode;
                                produto.cd_produto = id_cliente_licenca;

                                log_inserir(client.id_cliente.ToString() + " - Licença ativada (novo usuario) - " + id_cliente_licenca, (int)Lista_Erro.usar_add);
                            }

                            if (item.GetType() == typeof(SubscriptionResponseItemCollectionGetDownloadLinks))
                            {
                                var itemDetalhe = (SubscriptionResponseItemCollectionGetDownloadLinks)item;
                                // cancelou corretamente a licença
                                var licenca = controle.Where(x => x.UnitId.ToString() == itemDetalhe.UnitId).FirstOrDefault();

                                var produto = produto_ativado.Where(x => x.urn_produto == licenca.urn_produto).FirstOrDefault();

                                switch (itemDetalhe.Platform)
                                {
                                    case PlatformEnum.Android:
                                        produto.link_ativacao_android = itemDetalhe.DownloadLinks[0].Url.Replace("market://", "https://play.google.com/store/apps/");
                                        break;

                                    case PlatformEnum.iOS:
                                        produto.link_ativacao_iphone = itemDetalhe.DownloadLinks[0].Url;
                                        break;

                                    case PlatformEnum.macOS:
                                        produto.link_ativacao_mac = itemDetalhe.DownloadLinks[0].Url;
                                        break;

                                    case PlatformEnum.Windows:
                                        produto.link_ativacao_windows = itemDetalhe.DownloadLinks[0].Url;
                                        break;

                                }

                                //log_inserir(client.id_cliente.ToString() + " - Licença ativada (novo usuario) - " + licenca.id, (int)Lista_Erro.usar_add);
                            }
                        }

                    }
                    else if (obj.GetType() == typeof(SubscriptionResponseErrorCollection))
                    {
                        SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                        itemErro = (SubscriptionResponseErrorCollection)obj;

                        foreach (var erro in itemErro.Items)
                        {
                            var licenca = controle.Where(x => x.UnitId.ToString() == erro.UnitId).FirstOrDefault();
                            log_inserir(client.id_cliente.ToString() + " - ERRO " + licenca.comando.ToString() + " - " + erro.ErrorCode + "-" + erro.ErrorMessage, (int)Lista_Erro.usar_add);
                        }

                        //return new UserAdd_Retorno() { cod_retorno = -2, msg_retorno = "Não foi possivel ativar todas as licenças." };
                    }
                    else if (obj.GetType() == typeof(TransactionErrorType))
                    {
                        TransactionErrorType itemErro = new TransactionErrorType();

                        itemErro = (TransactionErrorType)obj;
                        log_inserir(client.id_cliente.ToString() + " - ERRO ao ativar Licença - " + controle[0].id_cliente_usuario.ToString() + " - " + itemErro.ErrorCode + "-" + itemErro.ErrorMessage, (int)Lista_Erro.usar_add);
                        ClienteDeletar(id_cliente_usuario, 1);
                        return new UserAdd_Retorno() { cod_retorno = -3, msg_retorno = "Ocorreu um erro na solicitação de ativacao." };
                    }
                }
            }
            catch (Exception ex)
            {
                log_inserir(client.id_cliente.ToString() + " - ERRO ao ativar - " + controle[0].id_cliente_usuario.ToString() + " - " + ex.Message, (int)Lista_Erro.usar_add);
                ClienteDeletar(id_cliente_usuario, 1);
                return new UserAdd_Retorno() { cod_retorno = -4, msg_retorno = "Ocorreu um erro ao cadastrar o usuario." };
            }

            log_inserir(client.id_cliente.ToString() + "- ATIVOU " + Newtonsoft.Json.JsonConvert.SerializeObject(produto_ativado), (int)Lista_Erro.usar_add);


            // Atualiza os links
            foreach (var produto in produto_ativado)
            {
                string id_cliente_licenca = ClienteLicencaInserir(produto.cd_produto,
                                   "",
                                   "",
                                   "",
                                   "",
                                   produto.link_ativacao_android,
                                   produto.link_ativacao_iphone,
                                   produto.link_ativacao_windows,
                                   produto.link_ativacao_windows,
                                   "",
                                   "",
                                   2);
            }

            return new UserAdd_Retorno { cod_retorno = 0, msg_retorno = "", produtos = produto_ativado };
        }

        public LicenseCancel_Retorno cancelar_assinatura(LicenseCancel licenseCancel, ClientInfo client)
        {
            // consulta de cliente existe e/ou o produto que ele te esta ativado
            var dt_produto_cliente = seleciona_licenca_produto(client.id_cliente, licenseCancel.UserID, licenseCancel.ProductID);

            if (dt_produto_cliente.Rows.Count == 0)
            {
                return new LicenseCancel_Retorno() { cod_retorno = -1, msg_retorno = "Usuario ou produto não encontrado." };
            }

            List<object> comandos = new List<object>();
            int count = 1;
            List<Controle_Envio> controle = new List<Controle_Envio>();
            string TransactionId = dt_produto_cliente.Rows[0]["nm_transaction_id"].ToString() + DateTime.Now.ToString("HHmmssfff");
            // cancela os produtos em lote
            foreach (DataRow dr in dt_produto_cliente.Rows)
            {
                var cancelamento_licenca = new KL_Conexao().KL_retorna_cancelamento_hard(dr["nm_subscriber_id"].ToString(),
                                        DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z"),
                                        count.ToString());

                controle.Add(new Controle_Envio() { comando = comando_kl.cancelar_hard, UnitId = count, SubscribeId = dr["nm_subscriber_id"].ToString(), id_cliente_licenca = dr["id_cliente_licenca"].ToString(), id_cliente_usuario = dr["id_cliente_usuario"].ToString() });
                comandos.Add((object)cancelamento_licenca);
                count++;
            }

            string xmlRequest, xmlContainer;

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = new KL_Conexao().Comando_KL(TransactionId, client.nm_usuario_certificado, client.nm_senha_certificado, client.nm_thumbprint, comandos.ToArray(), out xmlContainer, out xmlRequest);

            log_inserir(client.id_cliente.ToString() + "- RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.license_cancel);
            log_inserir(client.id_cliente.ToString() + "- RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.license_cancel);
            log_inserir(client.id_cliente.ToString() + "- RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.license_cancel);

            try
            {
                foreach (object obj in container.Items)
                {

                    if (obj.GetType() == typeof(SubscriptionResponseItemCollection))
                    {
                        // faz um looping nas solicitações
                        SubscriptionResponseItemCollection itens = new SubscriptionResponseItemCollection();
                        itens = (SubscriptionResponseItemCollection)obj;

                        foreach (var item in itens.Items)
                        {
                            if (item.GetType() == typeof(BaseResponseItemType))
                            {
                                var itemDetalhe = (BaseResponseItemType)item;
                                // cancelou corretamente a licença
                                var licenca = controle.Where(x => x.UnitId.ToString() == itemDetalhe.UnitId).FirstOrDefault();

                                cancela_licenca_produto(licenca.id_cliente_licenca);

                                log_inserir(client.id_cliente.ToString() + " - Licença cancelada - " + licenca.id_cliente_licenca, (int)Lista_Erro.license_cancel);
                            }
                        }

                    }
                    else if (obj.GetType() == typeof(SubscriptionResponseErrorCollection))
                    {
                        SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                        itemErro = (SubscriptionResponseErrorCollection)obj;

                        foreach (var erro in itemErro.Items)
                        {
                            var licenca = controle.Where(x => x.UnitId.ToString() == erro.UnitId).FirstOrDefault();
                            log_inserir(client.id_cliente.ToString() + " - ERRO ao cancelada Licença - " + licenca.id_cliente_licenca + " - " + erro.ErrorCode + "-" + erro.ErrorMessage, (int)Lista_Erro.license_cancel);
                        }

                        return new LicenseCancel_Retorno() { cod_retorno = -2, msg_retorno = "Não foi possivel cancelar todas as licenças." };
                    }
                    else if (obj.GetType() == typeof(TransactionErrorType))
                    {
                        TransactionErrorType itemErro = new TransactionErrorType();

                        itemErro = (TransactionErrorType)obj;
                        log_inserir(client.id_cliente.ToString() + " - ERRO ao cancelada Licença - " + controle[0].id_cliente_usuario.ToString() + " - " + itemErro.ErrorCode + "-" + itemErro.ErrorMessage, (int)Lista_Erro.license_cancel);

                        return new LicenseCancel_Retorno() { cod_retorno = -3, msg_retorno = "Ocorreu um erro na solicitação de cancelamento." };

                    }

                }
            }
            catch (Exception ex)
            {
                log_inserir(client.id_cliente.ToString() + " - ERRO ao cancelada Licença - " + controle[0].id_cliente_usuario.ToString() + " - " + ex.Message, (int)Lista_Erro.license_cancel);
                return new LicenseCancel_Retorno() { cod_retorno = -4, msg_retorno = "Ocorreu um erro na solicitação de cancelamento." };
            }


            return new LicenseCancel_Retorno() { cod_retorno = 0, msg_retorno = "" };
        }

        public Ativacao_Retorno ativarLicense(LicenseActivation ativacao, ClientInfo client)
        {

            // consulta se cliente existe, e se é pra cadastrar na hora
            var dt_usuario = seleciona_cliente_usuario(client.id_cliente, ativacao.UserID);

            // Se nao houver usuario e nao for uma venda avulsa
            if (dt_usuario.Rows.Count == 0 && !ativacao.IgnoreRegister)
            {
                return new Ativacao_Retorno() { cod_retorno = -1, msg_retorno = "Usuario não cadastrado." };
            }
            string id_cliente_usuario = dt_usuario.Rows[0]["id_cliente_usuario"].ToString();

            // Se existir, verifica se ja nao foi ativado esse produto
            var dt_produto_cliente = seleciona_licenca_produto(client.id_cliente, ativacao.UserID, ativacao.ProductID);

            // O produto ja esta ativado, retorna os dados do produto
            if (dt_produto_cliente.Rows.Count > 0)
            {
                var produtos = new List<Produto_Ativacao_Retorno>();
                produtos.Add(new Produto_Ativacao_Retorno()
                {
                    ativado = true,
                    cd_produto = dt_produto_cliente.Rows[0]["id_cliente_licenca"].ToString(),
                    chave_ativacao = dt_produto_cliente.Rows[0]["cd_ativacao_kl"].ToString(),
                    link_ativacao_android = dt_produto_cliente.Rows[0]["nm_ativacao_android"].ToString(),
                    link_ativacao_iphone = dt_produto_cliente.Rows[0]["nm_ativacao_iphone"].ToString(),
                    link_ativacao_windows = dt_produto_cliente.Rows[0]["nm_ativacao_windows"].ToString(),
                    link_ativacao_mac = dt_produto_cliente.Rows[0]["nm_ativacao_mac"].ToString(),
                    nome_produto = dt_produto_cliente.Rows[0]["nm_produto_kl"].ToString(),
                    urn_produto = dt_produto_cliente.Rows[0]["nm_urn"].ToString()
                });

                return new Ativacao_Retorno() { cod_retorno = 0, msg_retorno = "", produtos = produtos };

            }

            // looping do produto recebido
            var dt_produto_ativacao = seleciona_produto_cliente(client.id_cliente, client.id_cliente_certificado);

            List<object> comandos = new List<object>();
            int count = 1;
            List<Controle_Envio> controle = new List<Controle_Envio>();
            var produto_ativado = new List<Produto_Ativacao_Retorno>();
            // ativa o produto, sem os links

            var dt_produto = dt_produto_ativacao.Select("nm_urn = '" + ativacao.ProductID + "'");

            if (dt_produto == null || dt_produto.Length == 0)
            {
                return new Ativacao_Retorno() { cod_retorno = -1, msg_retorno = "Produto " + ativacao.ProductID + " não encontrado." };
            }

            string subscription_id = "cad-" + client.id_cliente.ToString() + "-" + id_cliente_usuario + "-" + dt_produto[0]["id_produto_kl"].ToString();
            string TransactionId = dt_usuario.Rows[0]["nm_transaction_id"].ToString() + DateTime.Now.ToString("yyyyMMddHHmmssffffff");

            string endTimeParam = "indefinite";

            if (!String.IsNullOrEmpty(ativacao.EndDate))
            {
                endTimeParam = ativacao.EndDate;
            }

            // Se nao achou, cadastra na hora 
            var ativacao_prod = new KL_Conexao().KL_retorna_ativacao(dt_produto[0]["qtd_licencas"].ToString(),
                                                                dt_produto[0]["cd_produto_kl"].ToString(),
                                                                DateTime.Now,
                                                                endTimeParam,
                                                                count.ToString(),
                                                                false,
                                                                subscription_id);

            controle.Add(new Controle_Envio()
            {
                comando = comando_kl.ativar,
                UnitId = count,
                SubscribeId = subscription_id,
                id_produto_kl = dt_produto[0]["id_produto_kl"].ToString(),
                id_cliente_usuario = id_cliente_usuario,
                urn_produto = ativacao.ProductID
            });

            comandos.Add((object)ativacao_prod);
            count++;

            var link_android = new KL_Conexao().KL_retorna_link(subscription_id,
                                     count.ToString(),
                                    "pt",
                                    PlatformEnum.Android);

            controle.Add(new Controle_Envio() { comando = comando_kl.link_android, UnitId = count, SubscribeId = subscription_id, id_cliente_usuario = id_cliente_usuario, urn_produto = ativacao.ProductID, nm_produto = dt_produto[0]["nm_produto_kl"].ToString() });
            comandos.Add((object)link_android);
            count++;

            var link_windows = new KL_Conexao().KL_retorna_link(subscription_id,
                                     count.ToString(),
                                    "pt",
                                    PlatformEnum.Windows);

            controle.Add(new Controle_Envio() { comando = comando_kl.link_windows, UnitId = count, SubscribeId = subscription_id, id_cliente_usuario = id_cliente_usuario, urn_produto = ativacao.ProductID, nm_produto = dt_produto[0]["nm_produto_kl"].ToString() });
            comandos.Add((object)link_windows);
            count++;

            var link_ios = new KL_Conexao().KL_retorna_link(subscription_id,
                                    count.ToString(),
                                   "pt",
                                   PlatformEnum.iOS);

            controle.Add(new Controle_Envio() { comando = comando_kl.link_iphone, UnitId = count, SubscribeId = subscription_id, id_cliente_usuario = id_cliente_usuario, urn_produto = ativacao.ProductID, nm_produto = dt_produto[0]["nm_produto_kl"].ToString() });
            comandos.Add((object)link_ios);
            count++;

            var link_mac = new KL_Conexao().KL_retorna_link(subscription_id,
                                    count.ToString(),
                                   "pt",
                                   PlatformEnum.macOS);

            controle.Add(new Controle_Envio() { comando = comando_kl.link_mac, UnitId = count, SubscribeId = subscription_id, id_cliente_usuario = id_cliente_usuario, urn_produto = ativacao.ProductID, nm_produto = dt_produto[0]["nm_produto_kl"].ToString() });
            comandos.Add((object)link_mac);
            count++;

            produto_ativado.Add(new Produto_Ativacao_Retorno()
            {
                ativado = false,
                nome_produto = dt_produto[0]["nm_produto_kl"].ToString(),
                urn_produto = ativacao.ProductID
            });

            string xmlRequest, xmlContainer;

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = new KL_Conexao().Comando_KL(TransactionId, client.nm_usuario_certificado, client.nm_senha_certificado, client.nm_thumbprint, comandos.ToArray(), out xmlContainer, out xmlRequest);

            log_inserir(client.id_cliente.ToString() + "- RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.license_ativation);
            log_inserir(client.id_cliente.ToString() + "- RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.license_ativation);
            log_inserir(client.id_cliente.ToString() + "- RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.license_ativation);

            try
            {
                foreach (object obj in container.Items)
                {
                    if (obj.GetType() == typeof(SubscriptionResponseItemCollection))
                    {
                        SubscriptionResponseItemCollection itens = new SubscriptionResponseItemCollection();
                        itens = (SubscriptionResponseItemCollection)obj;
                        string retorno = "";
                        // faz um looping nas solicitações
                        foreach (var item in itens.Items)
                        {
                            if (item.GetType() == typeof(SubscriptionResponseItemCollectionActivate))
                            {
                                var itemDetalhe = (SubscriptionResponseItemCollectionActivate)item;
                                // cancelou corretamente a licença
                                var licenca = new Controle_Envio();

                                licenca = controle.Where(x => x.UnitId.ToString() == itemDetalhe.UnitId).FirstOrDefault();

                                string id_cliente_licenca = String.Empty;

                                id_cliente_licenca = ClienteLicencaInserir("0",
                                licenca.id_cliente_usuario,
                                licenca.id_produto_kl,
                                itemDetalhe.ActivationCode,
                                licenca.SubscribeId,
                                "",
                                "",
                                "",
                                "",
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                "",
                                2);

                                var produto = new Produto_Ativacao_Retorno();

                                produto = produto_ativado.Where(x => x.urn_produto == licenca.urn_produto).FirstOrDefault();

                                produto.ativado = true;
                                produto.chave_ativacao = itemDetalhe.ActivationCode;
                                produto.cd_produto = id_cliente_licenca;

                                log_inserir(client.id_cliente.ToString() + " - Licença ativada (novo usuario) - " + id_cliente_licenca, (int)Lista_Erro.usar_add);
                            }

                            if (item.GetType() == typeof(SubscriptionResponseItemCollectionGetDownloadLinks))
                            {
                                var itemDetalhe = (SubscriptionResponseItemCollectionGetDownloadLinks)item;
                                // cancelou corretamente a licença
                                var licenca = controle.Where(x => x.UnitId.ToString() == itemDetalhe.UnitId).FirstOrDefault();

                                var produto = produto_ativado.Where(x => x.urn_produto == licenca.urn_produto).FirstOrDefault();

                                switch (licenca.comando)
                                {
                                    case comando_kl.link_android:
                                        produto.link_ativacao_android = itemDetalhe.DownloadLinks[0].Url.Replace("market://", "https://play.google.com/store/apps/");
                                        break;

                                    case comando_kl.link_iphone:
                                        produto.link_ativacao_iphone = itemDetalhe.DownloadLinks[0].Url;
                                        break;

                                    case comando_kl.link_mac:
                                        produto.link_ativacao_mac = itemDetalhe.DownloadLinks[0].Url;
                                        break;

                                    case comando_kl.link_windows:
                                        produto.link_ativacao_windows = itemDetalhe.DownloadLinks[0].Url;
                                        break;

                                }
                            }
                        }
                    }
                    else if (obj.GetType() == typeof(SubscriptionResponseErrorCollection))
                    {
                        SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                        itemErro = (SubscriptionResponseErrorCollection)obj;

                        foreach (var erro in itemErro.Items)
                        {
                            var licenca = controle.Where(x => x.UnitId.ToString() == erro.UnitId).FirstOrDefault();
                            log_inserir(client.id_cliente.ToString() + " - ERRO " + licenca.comando.ToString() + " - " + erro.ErrorCode + "-" + erro.ErrorMessage, (int)Lista_Erro.usar_add);
                        }

                        //return new UserAdd_Retorno() { cod_retorno = -2, msg_retorno = "Não foi possivel ativar todas as licenças." };
                    }
                    else if (obj.GetType() == typeof(TransactionErrorType))
                    {
                        TransactionErrorType itemErro = new TransactionErrorType();

                        itemErro = (TransactionErrorType)obj;
                        log_inserir(client.id_cliente.ToString() + " - ERRO ao ativar Licença - " + controle[0].id_cliente_usuario.ToString() + " - " + itemErro.ErrorCode + "-" + itemErro.ErrorMessage, (int)Lista_Erro.usar_add);

                        return new Ativacao_Retorno() { cod_retorno = -3, msg_retorno = "Ocorreu um erro na solicitação de ativacao." };
                    }
                }
            }
            catch (Exception ex)
            {
                log_inserir(client.id_cliente.ToString() + " - ERRO ao cadastrar usuario - " + controle[0].id_cliente_usuario.ToString() + " - " + ex.Message, (int)Lista_Erro.usar_add);
                return new Ativacao_Retorno() { cod_retorno = -4, msg_retorno = "Ocorreu um erro ao cadastrar o usuario." };
            }

            // Atualiza os links
            foreach (var produto in produto_ativado)
            {
                string id_cliente_licenca = ClienteLicencaInserir(produto.cd_produto,
                                   "",
                                   "",
                                   "",
                                   "",
                                   produto.link_ativacao_android == null ? "" : produto.link_ativacao_android,
                                   produto.link_ativacao_iphone == null ? "" : produto.link_ativacao_iphone,
                                   produto.link_ativacao_windows == null ? "" : produto.link_ativacao_windows,
                                   produto.link_ativacao_mac == null ? "" : produto.link_ativacao_mac,
                                   "",
                                   "",
                                   2);
            }

            return new Ativacao_Retorno { cod_retorno = 0, msg_retorno = "", produtos = produto_ativado };
        }

        public void userAddLote(UserAdd usuario, ClientInfo client, DataTable dt_usuario)
        {

        }


        public LoginRetorno login(Login login)
        {
            using (var client = new HttpClient())
            {
                LoginInterno loginInterno = new LoginInterno()
                {
                    sys = "MK0",
                    cd_servico = 9999,
                    token = "e9be9025025af4e7a0df70e6d2d3cd69",
                    password = "34091b705f83484"
                };

                WSMKUserSenhaSAC userSac = GetUserSAC(loginInterno, login.username, login.password, client);

                if (userSac.CodigoPessoa <= 0)
                {
                    return new LoginRetorno() { cod_retorno = 0 };
                }

                WSMKContratosPorCliente contratosPorCliente = GetContratosPorCliente(loginInterno, userSac, client);

                if (contratosPorCliente.ContratosAtivos != null && contratosPorCliente.ContratosAtivos.Count > 0)
                {
                    return new LoginRetorno() { cod_retorno = 1 };
                }

                return new LoginRetorno() { cod_retorno = 2 };
            }
        }

        public MKWSAutenticacao GetTokenMK(LoginInterno loginInterno, HttpClient client)
        {
            var responseAutenticacao =
                client.GetAsync($@"{@ConfigurationManager.AppSettings["MK_WSAutenticacao"]}{loginInterno.sys}&token={loginInterno.token}&password={loginInterno.password}&cd_servico={loginInterno.cd_servico}").Result;

            if (responseAutenticacao.Content != null)
            {
                var responseContent = responseAutenticacao.Content.ReadAsStringAsync().Result;

                try
                {
                    var response_autenticacao_itailers = Newtonsoft.Json.JsonConvert.DeserializeObject<MKWSAutenticacao>(responseContent);
                    return response_autenticacao_itailers;
                }
                catch (Exception ex)
                {
                }
            }

            return new MKWSAutenticacao();
        }

        public WSMKUserSenhaSAC GetUserSAC(LoginInterno loginInterno, string username, string password, HttpClient client)
        {
            MKWSAutenticacao autenticacao = GetTokenMK(loginInterno, client);

            var responseSac =
                client.GetAsync($@"{@ConfigurationManager.AppSettings["MK_WSMKUserSenhaSAC"]}{loginInterno.sys}&token={autenticacao.token}&user_sac={username}&pass_sac={password}").Result;

            if (responseSac.Content != null)
            {
                var responseContent = responseSac.Content.ReadAsStringAsync().Result;

                try
                {
                    var response_SAC = Newtonsoft.Json.JsonConvert.DeserializeObject<WSMKUserSenhaSAC>(responseContent);
                    return response_SAC;
                }
                catch (Exception ex)
                {
                }
            }

            return new WSMKUserSenhaSAC();
        }

        public WSMKContratosPorCliente GetContratosPorCliente(LoginInterno loginInterno, WSMKUserSenhaSAC userSac, HttpClient client)
        {
            MKWSAutenticacao autenticacao = GetTokenMK(loginInterno, client);

            var responseContratos =
                client.GetAsync($@"{@ConfigurationManager.AppSettings["MK_WSMKContratosPorCliente"]}{loginInterno.sys}&token={autenticacao.token}&cd_cliente={userSac.CodigoPessoa}").Result;

            if (responseContratos.Content != null)
            {
                var responseContent = responseContratos.Content.ReadAsStringAsync().Result;

                try
                {
                    var response_contratos_cliente = Newtonsoft.Json.JsonConvert.DeserializeObject<WSMKContratosPorCliente>(responseContent);
                    return response_contratos_cliente;
                }
                catch (Exception ex)
                {
                }
            }

            return new WSMKContratosPorCliente();
        }



        #region atualizacao BD
        public void ClienteInserir(string nm_cliente)
        {
            DataBase db = new DataBase();
            db.procedure = "p_insere_cliente";

            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@nm_cliente", nm_cliente));

            db.parametros = par;
            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_API);
        }

        public void ClienteTokenInserir(string nm_token, DateTime dt_validade_token, int id_status)
        {
            DataBase db = new DataBase();
            db.procedure = "p_insere_cliente_token";

            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@nm_token", nm_token));
            par.Add(db.retorna_parametros("@dt_validade_token", dt_validade_token.ToString()));
            par.Add(db.retorna_parametros("@id_status", id_status.ToString()));

            db.parametros = par;
            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_API);
        }

        public void ClienteProdutoKlInserir(int id_cliente, int id_produto)
        {
            DataBase db = new DataBase();
            db.procedure = "p_insere_cliente_produto_kl";

            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@id_cliente", id_cliente.ToString()));
            par.Add(db.retorna_parametros("@id_produto", id_produto.ToString()));

            db.parametros = par;
            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable ClienteUsuarioInserir(int id_cliente, string nm_user_id, string nm_email, DateTime dt_start, string dt_end, string nm_user_document, string nm_user_plan)
        {
            if (dt_end == null)
            {
                dt_end = "";
            }

            DataBase db = new DataBase();
            db.procedure = "p_insere_cliente_usuario";

            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@id_cliente", id_cliente.ToString()));
            par.Add(db.retorna_parametros("@nm_user_id", nm_user_id));
            //par.Add(db.retorna_parametros("@nm_transaction_id", nm_transaction_id));
            par.Add(db.retorna_parametros("@nm_email", nm_email));
            par.Add(db.retorna_parametros("@dt_start", dt_start.ToString()));
            par.Add(db.retorna_parametros("@dt_end", dt_end));
            par.Add(db.retorna_parametros("@nm_user_document", nm_user_document));
            par.Add(db.retorna_parametros("@nm_user_plan", nm_user_plan));

            db.parametros = par;
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public string ClienteLicencaInserir(string id_cliente_licenca, string id_cliente_usuario, string id_produto, string cd_ativacao_kl, string nm_subscriber_id, string nm_ativacao_android, 
                                            string nm_ativacao_iphone, string nm_ativacao_windows, string nm_ativacao_mac, string dt_ativacao, string dt_expiracao, int id_status)
        {
            DataBase db = new DataBase();
            db.procedure = "p_insere_cliente_licenca";

            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@id_cliente_licenca", id_cliente_licenca.ToString()));
            par.Add(db.retorna_parametros("@id_cliente_usuario", id_cliente_usuario.ToString()));
            par.Add(db.retorna_parametros("@id_produto", id_produto.ToString()));
            par.Add(db.retorna_parametros("@cd_ativacao_kl", cd_ativacao_kl.ToString()));
            par.Add(db.retorna_parametros("@nm_subscriber_id", nm_subscriber_id.ToString()));
            par.Add(db.retorna_parametros("@nm_ativacao_android", nm_ativacao_android.ToString()));
            par.Add(db.retorna_parametros("@nm_ativacao_iphone", nm_ativacao_iphone.ToString()));
            par.Add(db.retorna_parametros("@nm_ativacao_windows", nm_ativacao_windows.ToString()));
            par.Add(db.retorna_parametros("@nm_ativacao_mac", nm_ativacao_mac.ToString()));
            par.Add(db.retorna_parametros("@dt_ativacao", dt_ativacao.ToString()));
            par.Add(db.retorna_parametros("@dt_expiracao", dt_expiracao.ToString()));
            par.Add(db.retorna_parametros("@id_status", id_status.ToString()));

            log_inserir("TESTE: " + nm_ativacao_android.ToString() + "," +  nm_ativacao_iphone.ToString() + "," + nm_ativacao_windows.ToString() + "," + nm_ativacao_mac.ToString(), 9999);

            db.parametros = par;
            return Generico.Exec_retorno_string(db, DAL.Constantes_DAL.Conexao_API);
        }

        public void ClienteDeletar(string id_cliente_usuario, int delete = 0)
        {
            DataBase db = new DataBase();
            db.procedure = "p_delete_cliente_usuario";

            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@id_cliente_usuario", id_cliente_usuario.ToString()));
            par.Add(db.retorna_parametros("@delete", delete.ToString()));

            db.parametros = par;
            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable seleciona_token(string cd_validacao)
        {
            DataBase db = new DataBase();
            db.procedure = "p_consulta_codigo_fornecedor";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@cd_validacao", cd_validacao));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable seleciona_licenca_produto(int id_cliente, string nm_user_id, string nm_urn)
        {
            DataBase db = new DataBase();
            db.procedure = "p_consulta_cliente_licenca";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@id_cliente", id_cliente.ToString()));
            par.Add(db.retorna_parametros("@nm_user_id", nm_user_id));
            par.Add(db.retorna_parametros("@nm_urn", nm_urn));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable seleciona_produto_cliente(int id_cliente, int id_cliente_certificado)
        {
            DataBase db = new DataBase();
            db.procedure = "p_consulta_cliente_produto";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@id_cliente", id_cliente.ToString()));
            par.Add(db.retorna_parametros("@id_cliente_certificado", id_cliente_certificado.ToString()));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable seleciona_cliente_usuario(int id_cliente, string nm_user_id)
        {
            DataBase db = new DataBase();
            db.procedure = "p_consulta_usuario";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@id_cliente", id_cliente.ToString()));
            par.Add(db.retorna_parametros("@nm_user_id", nm_user_id.ToString()));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public void cancela_licenca_produto(string id_cliente_licenca)
        {
            DataBase db = new DataBase();
            db.procedure = "p_cancelamento_cliente_licenca";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@id_cliente_licenca", id_cliente_licenca.ToString()));

            db.parametros = par;

            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_API);

            return;
        }

        #endregion

        #region token
        public ClientInfo ValidaToken(string token)
        {
            DataBase db = new DataBase();
            db.procedure = "p_retorna_certificado";

            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@nm_token", token));

            db.parametros = par;
            var dt = Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);

            if (dt.Rows.Count == 0)
            {
                return new ClientInfo() { valido = false };
            }
            else
            {
                return new ClientInfo()
                {
                    valido = true,
                    id_cliente = int.Parse(dt.Rows[0]["id_cliente"].ToString()),
                    id_cliente_certificado = int.Parse(dt.Rows[0]["id_cliente_certificado"].ToString()),
                    nm_senha_certificado = dt.Rows[0]["nm_senha_certificado"].ToString(),
                    nm_thumbprint = dt.Rows[0]["nm_thumbprint"].ToString(),
                    nm_usuario_certificado = dt.Rows[0]["nm_usuario_certificado"].ToString()
                };
            }
        }


        #endregion

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

            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_API);
        }

        #endregion
    }
}