using BLL;
using BLL.KL_API;
using DAL;
using KL_API.Controllers.Provisionado;
using KL_API.Models.Integracao.cliente;
using Objetos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace KL_API.Models
{
    public enum Lista_Erro
    {
        license_ativation = 2000,
        license_cancel = 2001,
        usar_add = 2002,
        user_delete = 2003,
        login = 2004,
        get_info = 2005
    }

    public enum Tipo_autenticacao
    {
        mk = 1
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
        public string MonthsToExpire { set; get; }
    }

    public class Activation
    {
        public string UserID { set; get; }
        public string Email { set; get; }
        public List<Produto_UserAdd> Products { get; set; }
    }

    public class ReActivation
    {
        public string UserID { set; get; }
    }

    public class User
    {
        public string UserID { set; get; }
    }

    public class Produto_UserAdd
    {
        public string ProductID { set; get; }
        public int Qtd { get; set; }
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

    public class UserDeleteLote
    {
        public string[] UsersID { set; get; }
    }

    public class IntegracaoUserDeleteLote
    {
        public string[] SubscriberId { set; get; }
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
        public string id_cliente { get; set; }
        public string nm_cliente { get; set; }
        public int id_produto { get; set; }
        public string cd_produto { set; get; }
        public string nome_produto { set; get; }
        public string urn_produto { set; get; }
        public string chave_ativacao { set; get; }
        public bool ativado { set; get; }
        public string link_ativacao_android { set; get; }
        public string link_ativacao_windows { set; get; }
        public string link_ativacao_iphone { set; get; }
        public string link_ativacao_mac { set; get; }
        public DateTime? data_ativacao { get; set; }
        public string imagem_produto { get; set; }
        public string descricao { get; set; }
        public int index { get; set; }
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
        public int id_cliente { get; set; }
        public int id_cliente_usuario { get; set; }
        public int cod_retorno { get; set; }
        public string msg_retorno { set; get; }
        public bool autenticado { get; set; }
        public int quantidade { get; set; }
    }

    public class AtivacaoRetorno
    {
        public int id_cliente { get; set; }
        public int id_cliente_usuario { get; set; }
        public int cod_retorno { get; set; }
        public string msg_retorno { set; get; }
        public int id_produto { get; set; }

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

    public class WSMKConexoesPorCliente
    {
        public int CodigoPessoa { get; set; }
        public List<Conexoes> Conexoes { get; set; }
    }

    public class Conexoes
    {
        public string bloqueada { get; set; }
        public int? codconexao { get; set; }
        public int? conexao { get; set; }
        public int? contrato { get; set; }
        public string motivo_bloqueio { get; set; }
        public string username { get; set; }

        public bool acesso
        {
            get
            {
                return bloqueada == "Não" ? true : false;
            }
        }

        public int codigo_contrato
        {
            get
            {
                return contrato == null ? 0 : contrato.Value;
            }
        }
    }

    public class WSMKUserSenhaSAC
    {
        public string AcessoSAC { get; set; }
        public int CodigoPessoa { get; set; }
        public string Nome { get; set; }
        public string status { get; set; }
    }

    public class WSMKPlanosAcesso
    {
        public int CodigoPlano { get; set; }
        public string NomePlano { get; set; }
        public string Status { get; set; }
        public List<Contratos> contratos { get; set; }
    }

    public class Contratos
    {
        public int codcontrato { get; set; }
        public int codigopessoa { get; set; }
        public string loginsac { get; set; }
        public string senhasac { get; set; }
    }

    public class VendorTheme
    {
        public int id_vendor_theme { get; set; }
        public int id_cliente { get; set; }
        public string vendorDomainName { get; set; }
        public string primaryColor { get; set; }
        public string secondaryColor { get; set; }
        public string logoImage { get; set; }
        public string vendorTitleImage { get; set; }
        public string bannerImage { get; set; }
        public bool isDarkTheme { get; set; }
        public string kl_token { get; set; }
        public string bannerImageMobile { get; set; }
    }

    public class Ativacao
    {
        public int id_cliente { get; set; }
        public int id_cliente_usuario { get; set; }
        public int id_produto { get; set; }
    }

    public class GetProdutosObj
    {
        public int id_cliente_usuario { get; set; }
    }

    public class GetProdutosRetorno
    {
        public int id_cliente_usuario { get; set; }
        public int cod_retorno { get; set; }
        public string msg_retorno { get; set; }
        public List<Produto_Ativacao_Retorno> produtos { set; get; }
    }

    public class GetUsersInfoReturn
    {
        public string nm_cliente { get; set; }
        public string nm_user_id { get; set; }
        public string nm_email { get; set; }
        public DateTime dt_start { get; set; }
        public string nm_user_plan { get; set; }
        public string cd_ativacao_kl { get; set; }
        public string dt_ativacao { get; set; }
        public string dt_cancelamento { get; set; }
    }

    public class GetUsersReturn
    {
        public string users_id { get; set; }
        public string email { get; set; }
        public string cd_activation { get; set; }
        public string dt_ativacao { get; set; }
        public string dt_hard_cancel { get; set; }
        public string dt_soft_cancel { get; set; }
        public string product { get; set; }
    }

    public class ProdutosCliente
    {
        public string NomeProduto { get; set; }
        public string Urn { get; set; }
        public string QuantidadeLicencas { get; set; }
    }

    public class Ativacao_Controle
    {
        LoginInterno loginInterno = new LoginInterno()
        {
            sys = "MK0",
            cd_servico = 9999,
            token = "e9be9025025af4e7a0df70e6d2d3cd69",
            password = "34091b705f83484"
        };

        public DateTime PegaHoraBrasilia() => TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

        private string RetornaSubscriberProvisionado(ClientInfo clientInfo, string user, out string id_cliente_usuario_str)
        {
            var cliente_usuario = seleciona_cliente_usuario(clientInfo.id_cliente, user);
            id_cliente_usuario_str = string.Empty;

            if (cliente_usuario is null || cliente_usuario.Rows.Count == 0)
                return null;

            id_cliente_usuario_str = cliente_usuario.Rows[0]["id_cliente_usuario"].ToString();

            int.TryParse(id_cliente_usuario_str, out int id_cliente_usuario);

            if (id_cliente_usuario == 0)
                return null;

            var provisionamento = Retorna_provisionamento(Convert.ToInt32(id_cliente_usuario), 0);

            if (provisionamento is null || provisionamento.Rows.Count == 0)
            {
                return null;
            }

            string subscriber_id = provisionamento.Rows[0]["subscriber_id"].ToString();

            if (String.IsNullOrEmpty(subscriber_id))
            {
                return null;
            }

            return subscriber_id;
        }

        public Controllers.Provisionado.Response GerenciaLicenca(Controllers.Provisionado.Request users, ClientInfo clientInfo)
        {
            string TransactionId = DateTime.Now.ToString("yyyyMMddHHmmssffffff");

            List<object> comandos = new List<object>();
            List<Controle_Envio> controle = new List<Controle_Envio>();
            UserValidation userValidation = new UserValidation();
            Controllers.Provisionado.Response response = new Controllers.Provisionado.Response();

            int count = 1;

            foreach (var user in users.users_resume)
            {
                string subscriber_id = RetornaSubscriberProvisionado(clientInfo, user, out string id_cliente_usuario_str);

                if (String.IsNullOrEmpty(subscriber_id))
                {
                    string msg_validation_resume = "Este usuário ainda não possui ativação.";
                    UserValidation users_resume_validation = response.users_resume.Where(w => w.user_login == user).FirstOrDefault();

                    if (users_resume_validation is null)
                    {
                        users_resume_validation = new UserValidation();
                        users_resume_validation.user_login = user;
                        users_resume_validation.mensagens.Add(msg_validation_resume);
                        response.users_resume.Add(users_resume_validation);
                    }
                    else
                    {
                        users_resume_validation.mensagens.Add(msg_validation_resume);
                    }

                    continue;
                }

                var resume = new KL_Conexao().KL_retorna_renew(subscriber_id, count.ToString());

                comandos.Add((object)resume);

                controle.Add(
                    new Controle_Envio()
                    {
                        comando = comando_kl.renovar,
                        id_cliente_usuario = id_cliente_usuario_str,
                        UnitId = count,
                        SubscribeId = subscriber_id,
                        id_cliente_licenca = user
                    });

                count++;
            }

            foreach (var user in users.users_stop)
            {
                string subscriber_id = RetornaSubscriberProvisionado(clientInfo, user, out string id_cliente_usuario_str);

                if (String.IsNullOrEmpty(subscriber_id))
                {
                    string msg_validation_stop = "Este usuário ainda não possui ativação.";
                    UserValidation users_stop_validation = response.users_stop.Where(w => w.user_login == user).FirstOrDefault();

                    if (users_stop_validation is null)
                    {
                        users_stop_validation = new UserValidation();
                        users_stop_validation.user_login = user;
                        users_stop_validation.mensagens.Add(msg_validation_stop);
                        response.users_stop.Add(users_stop_validation);
                    }
                    else
                    {
                        users_stop_validation.mensagens.Add(msg_validation_stop);
                    }

                    continue;
                }

                var cancelamento_soft = new KL_Conexao().KL_retorna_cancelamento_soft(subscriber_id,
                    DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z"),
                    count.ToString());

                comandos.Add((object)cancelamento_soft);

                controle.Add(
                    new Controle_Envio()
                    {
                        comando = comando_kl.cancelar_soft,
                        id_cliente_usuario = id_cliente_usuario_str,
                        UnitId = count,
                        SubscribeId = subscriber_id,
                        id_cliente_licenca = user
                    });

                count++;
            }

            foreach (var user in users.users_cancel)
            {
                string subscriber_id = RetornaSubscriberProvisionado(clientInfo, user, out string id_cliente_usuario_str);

                if (String.IsNullOrEmpty(subscriber_id))
                {
                    string msg_validation_cancel = "Este usuário ainda não possui ativação.";
                    UserValidation users_cancel_validation = response.users_cancel.Where(w => w.user_login == user).FirstOrDefault();

                    if (users_cancel_validation is null)
                    {
                        users_cancel_validation = new UserValidation();
                        users_cancel_validation.user_login = user;
                        users_cancel_validation.mensagens.Add(msg_validation_cancel);
                        response.users_cancel.Add(users_cancel_validation);
                    }
                    else
                    {
                        users_cancel_validation.mensagens.Add(msg_validation_cancel);
                    }

                    continue;
                }

                var cancelamento_hard = new KL_Conexao().KL_retorna_cancelamento_hard(subscriber_id,
                    DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z"),
                    count.ToString());

                comandos.Add((object)cancelamento_hard);
                controle.Add(
                   new Controle_Envio()
                   {
                       comando = comando_kl.cancelar_hard,
                       id_cliente_usuario = id_cliente_usuario_str,
                       UnitId = count,
                       SubscribeId = subscriber_id,
                       id_cliente_licenca = user
                   });

                count++;
            }

            if (comandos.Count == 0)
            {
                return response;
            }

            string xmlRequest, xmlContainer;

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = new KL_Conexao().Comando_KL(TransactionId, clientInfo.nm_usuario_certificado, clientInfo.nm_senha_certificado, clientInfo.nm_thumbprint, comandos.ToArray(), out xmlContainer, out xmlRequest);

            log_inserir(0 + "- RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.license_cancel);
            log_inserir(0 + "- RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.license_cancel);
            log_inserir(0 + "- RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.license_cancel);

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

                                var licenca = controle.Where(x => x.UnitId.ToString() == itemDetalhe.UnitId).FirstOrDefault();

                                if (licenca.comando == comando_kl.cancelar_hard)
                                {
                                    Deletar_provisionamento(licenca.id_cliente_usuario, clientInfo.id_cliente.ToString());
                                }

                                log_inserir(clientInfo.id_cliente.ToString() + " - GerenciaLicenca - " + licenca.id_cliente_licenca, (int)Lista_Erro.user_delete);
                            }
                        }
                    }
                    else if (obj.GetType() == typeof(SubscriptionResponseErrorCollection))
                    {
                        SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                        itemErro = (SubscriptionResponseErrorCollection)obj;

                        foreach (var erro in itemErro.Items)
                        {
                            if (erro.ErrorCode == "202")
                            {
                                var control = controle.Where(w => w.UnitId.ToString() == erro.UnitId).FirstOrDefault();
                                if (control.comando == comando_kl.renovar)
                                {
                                    string msg_validation_resume = "Não é possível renovar a licença deste usuário, porque ela nao está pausada.";
                                    UserValidation users_resume_validation = response.users_resume.Where(w => w.user_login == control.id_cliente_licenca).FirstOrDefault();

                                    if (users_resume_validation is null)
                                    {
                                        users_resume_validation = new UserValidation();
                                        users_resume_validation.user_login = control.id_cliente_licenca;
                                        users_resume_validation.mensagens.Add(msg_validation_resume);
                                        response.users_resume.Add(users_resume_validation);
                                    }
                                    else
                                    {
                                        users_resume_validation.mensagens.Add(msg_validation_resume);
                                    }
                                }
                            }

                            var licenca = controle.Where(x => x.UnitId.ToString() == erro.UnitId).FirstOrDefault();
                            log_inserir(clientInfo.id_cliente.ToString() + " - GerenciaLicenca - " + licenca.id_cliente_licenca + " - " + erro.ErrorCode + "-" + erro.ErrorMessage, (int)Lista_Erro.user_delete);
                        }
                    }
                    else if (obj.GetType() == typeof(TransactionErrorType))
                    {
                        TransactionErrorType itemErro = new TransactionErrorType();

                        itemErro = (TransactionErrorType)obj;
                        log_inserir(clientInfo.id_cliente.ToString() + " - GerenciaLicenca - " + controle[0].id_cliente_usuario.ToString() + " - " + itemErro.ErrorCode + "-" + itemErro.ErrorMessage, (int)Lista_Erro.user_delete);
                    }
                }
            }
            catch (Exception ex)
            {
                log_inserir(clientInfo.id_cliente.ToString() + " - GerenciaLicenca - " + controle[0].id_cliente_usuario.ToString() + " - " + ex.Message, (int)Lista_Erro.user_delete);
            }


            return response;
        }


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
            string TransactionId = PegaHoraBrasilia().ToString("yyyyMMddHHmmssffffff");
            // cancela os produtos em lote
            foreach (DataRow dr in dt_produto_cliente.Rows)
            {
                var cancelamento_licenca = new KL_Conexao().KL_retorna_cancelamento_hard(dr["nm_subscriber_id"].ToString(),
                                        DateTime.Parse(PegaHoraBrasilia().ToString("yyyy-MM-dd") + "T" + PegaHoraBrasilia().ToString("HH:mm:ss.ffffff") + "Z"),
                                        count.ToString());

                controle.Add(new Controle_Envio() { comando = comando_kl.cancelar_hard, UnitId = count, SubscribeId = dr["nm_subscriber_id"].ToString(), id_cliente_licenca = dr["id_cliente_licenca"].ToString(), id_cliente_usuario = dr["id_cliente_usuario"].ToString() });
                comandos.Add((object)cancelamento_licenca);
                count++;
            }

            string xmlRequest, xmlContainer;

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = new KL_Conexao().Comando_KL(TransactionId, client.nm_usuario_certificado, client.nm_senha_certificado, client.nm_thumbprint, comandos.ToArray(), out xmlContainer, out xmlRequest);

            log_inserir(client.id_cliente.ToString() + "-HARD CANCEL RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.user_delete);
            log_inserir(client.id_cliente.ToString() + "-HARD CANCEL RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.user_delete);
            log_inserir(client.id_cliente.ToString() + "-HARD CANCEL RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.user_delete);

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

                                log_inserir(client.id_cliente.ToString() + "HARD CANCEL - Licença cancelar - " + licenca.id_cliente_licenca, (int)Lista_Erro.user_delete);
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
                            log_inserir(client.id_cliente.ToString() + "HARD CANCEL - ERRO ao cancelar Licença - " + licenca.id_cliente_licenca + " - " + erro.ErrorCode + "-" + erro.ErrorMessage, (int)Lista_Erro.user_delete);
                        }

                        return new UserDelete_Retorno() { cod_retorno = -2, msg_retorno = "Não foi possivel cancelar todas as licenças." };
                    }
                    else if (obj.GetType() == typeof(TransactionErrorType))
                    {
                        TransactionErrorType itemErro = new TransactionErrorType();

                        itemErro = (TransactionErrorType)obj;
                        log_inserir(client.id_cliente.ToString() + "HARD CANCEL - ERRO ao cancelar Licença - " + controle[0].id_cliente_usuario.ToString() + " - " + itemErro.ErrorCode + "-" + itemErro.ErrorMessage, (int)Lista_Erro.license_cancel);

                        return new UserDelete_Retorno() { cod_retorno = -3, msg_retorno = "Ocorreu um erro na solicitação de cancelamento." };

                    }

                }
            }
            catch (Exception ex)
            {
                log_inserir(client.id_cliente.ToString() + "HARD CANCEL - ERRO ao cancelar Licença - " + controle[0].id_cliente_usuario.ToString() + " - " + ex.Message, (int)Lista_Erro.user_delete);
                return new UserDelete_Retorno() { cod_retorno = -4, msg_retorno = "Ocorreu um erro na solicitação de cancelamento." };
            }

            // Deleta usuario 
            ClienteDeletar(dt_produto_cliente.Rows[0]["id_cliente_licenca"].ToString());
            log_inserir(client.id_cliente.ToString() + "HARD CANCEL - USUARIO EXCLUIDO - " + controle[0].id_cliente_usuario.ToString(), (int)Lista_Erro.user_delete);

            return new UserDelete_Retorno() { cod_retorno = 0, msg_retorno = "HARD CANCEL - Licença(s) cancelada(s)." };

        }
        public UserDelete_Retorno IntegracaoDeleteUserLote(IntegracaoUserDeleteLote usuarioLote, ClientInfo client)
        {
            Models.Integracao.Integracao integracao = new Models.Integracao.Integracao();
            List<object> comandos = new List<object>();
            List<Controle_Envio> controle = new List<Controle_Envio>();
            int count = 1;
            foreach (var subscriberId in usuarioLote.SubscriberId)
            {
                var cancelamento_licenca = new KL_Conexao().KL_retorna_cancelamento_hard(subscriberId,
                                        DateTime.Parse(PegaHoraBrasilia().ToString("yyyy-MM-dd") + "T" + PegaHoraBrasilia().ToString("HH:mm:ss.ffffff") + "Z"),
                                        count.ToString());

                controle.Add(new Controle_Envio() { comando = comando_kl.cancelar_hard, UnitId = count, SubscribeId = subscriberId });
                comandos.Add((object)cancelamento_licenca);
                count++;
            }

            string TransactionId = PegaHoraBrasilia().ToString("yyyyMMddHHmmssffffff");

            string xmlRequest, xmlContainer;

            decimal d = comandos.Count / 150;
            int loop = (int)Math.Ceiling(d);
            int take = 150;
            int skip = 0;
            for (int i = 0; i < loop; i++)
            {
                comandos = comandos.Skip(skip).Take(take).ToList();
                skip += 150;

                SubscriptionResponseContainer container = new SubscriptionResponseContainer();
                container = new KL_Conexao().Comando_KL(TransactionId, client.nm_usuario_certificado, client.nm_senha_certificado, client.nm_thumbprint, comandos.ToArray(), out xmlContainer, out xmlRequest);

                integracao.InsereIntegracaoLog(client.id_cliente.ToString() + "-HARD CANCEL RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer));
                integracao.InsereIntegracaoLog(client.id_cliente.ToString() + "-HARD CANCEL RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest));
                integracao.InsereIntegracaoLog(client.id_cliente.ToString() + "-HARD CANCEL RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container));

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

                                    integracao.InsereIntegracaoLog(client.id_cliente.ToString() + "HARD CANCEL - Licença cancelar SubscribeId- " + licenca.SubscribeId);
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
                                integracao.InsereIntegracaoLog(client.id_cliente.ToString() + "HARD CANCEL - ERRO ao cancelar SubscribeId - " + licenca.SubscribeId + " - " + erro.ErrorCode + "-" + erro.ErrorMessage);
                            }

                            return new UserDelete_Retorno() { cod_retorno = -2, msg_retorno = "Não foi possivel cancelar todas as licenças." };
                        }
                        else if (obj.GetType() == typeof(TransactionErrorType))
                        {
                            TransactionErrorType itemErro = new TransactionErrorType();

                            itemErro = (TransactionErrorType)obj;
                            integracao.InsereIntegracaoLog(client.id_cliente.ToString() + "HARD CANCEL - ERRO ao cancelar SubscribeId - " + controle[0].SubscribeId.ToString() + " - " + itemErro.ErrorCode + "-" + itemErro.ErrorMessage);

                            return new UserDelete_Retorno() { cod_retorno = -3, msg_retorno = "Ocorreu um erro na solicitação de cancelamento." };

                        }

                    }
                }
                catch (Exception ex)
                {
                    integracao.InsereIntegracaoLog(client.id_cliente.ToString() + "HARD CANCEL - ERRO ao cancelar Licença - " + controle[0].id_cliente_usuario.ToString() + " - " + ex.Message);
                    return new UserDelete_Retorno() { cod_retorno = -4, msg_retorno = "Ocorreu um erro na solicitação de cancelamento." };
                }
            }

            

            return new UserDelete_Retorno() { cod_retorno = 0, msg_retorno = "HARD CANCEL - Licença(s) cancelada(s)." };
        }

        public UserDelete_Retorno DeleteUserLote(UserDeleteLote usuarioLote, ClientInfo client)
        {
            List<object> comandos = new List<object>();
            List<Controle_Envio> controle = new List<Controle_Envio>();
            int count = 1;
            foreach (var usuario in usuarioLote.UsersID)
            {
                // consulta de cliente existe e/ou o produto que ele te esta ativado
                var dt_produto_cliente = seleciona_licenca_produto(client.id_cliente, usuario, "all");

                if (dt_produto_cliente.Rows.Count == 0)
                {
                    return new UserDelete_Retorno() { cod_retorno = -1, msg_retorno = "Usuario não encontrado ou sem licenca ativa." };
                }
                
                // cancela os produtos em lote
                foreach (DataRow dr in dt_produto_cliente.Rows)
                {
                    var cancelamento_licenca = new KL_Conexao().KL_retorna_cancelamento_hard(dr["nm_subscriber_id"].ToString(),
                                            DateTime.Parse(PegaHoraBrasilia().ToString("yyyy-MM-dd") + "T" + PegaHoraBrasilia().ToString("HH:mm:ss.ffffff") + "Z"),
                                            count.ToString());

                    controle.Add(new Controle_Envio() { comando = comando_kl.cancelar_hard, UnitId = count, SubscribeId = dr["nm_subscriber_id"].ToString(), id_cliente_licenca = dr["id_cliente_licenca"].ToString(), id_cliente_usuario = dr["id_cliente_usuario"].ToString() });
                    comandos.Add((object)cancelamento_licenca);
                    count++;
                }
            }

            string TransactionId = PegaHoraBrasilia().ToString("yyyyMMddHHmmssffffff");

            string xmlRequest, xmlContainer;

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = new KL_Conexao().Comando_KL(TransactionId, client.nm_usuario_certificado, client.nm_senha_certificado, client.nm_thumbprint, comandos.ToArray(), out xmlContainer, out xmlRequest);

            log_inserir(client.id_cliente.ToString() + "-HARD CANCEL RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.user_delete);
            log_inserir(client.id_cliente.ToString() + "-HARD CANCEL RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.user_delete);
            log_inserir(client.id_cliente.ToString() + "-HARD CANCEL RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.user_delete);

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

                                log_inserir(client.id_cliente.ToString() + "HARD CANCEL - Licença cancelar - " + licenca.id_cliente_licenca, (int)Lista_Erro.user_delete);
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
                            log_inserir(client.id_cliente.ToString() + "HARD CANCEL - ERRO ao cancelar Licença - " + licenca.id_cliente_licenca + " - " + erro.ErrorCode + "-" + erro.ErrorMessage, (int)Lista_Erro.user_delete);
                        }

                        return new UserDelete_Retorno() { cod_retorno = -2, msg_retorno = "Não foi possivel cancelar todas as licenças." };
                    }
                    else if (obj.GetType() == typeof(TransactionErrorType))
                    {
                        TransactionErrorType itemErro = new TransactionErrorType();

                        itemErro = (TransactionErrorType)obj;
                        log_inserir(client.id_cliente.ToString() + "HARD CANCEL - ERRO ao cancelar Licença - " + controle[0].id_cliente_usuario.ToString() + " - " + itemErro.ErrorCode + "-" + itemErro.ErrorMessage, (int)Lista_Erro.license_cancel);

                        return new UserDelete_Retorno() { cod_retorno = -3, msg_retorno = "Ocorreu um erro na solicitação de cancelamento." };

                    }

                }
            }
            catch (Exception ex)
            {
                log_inserir(client.id_cliente.ToString() + "HARD CANCEL - ERRO ao cancelar Licença - " + controle[0].id_cliente_usuario.ToString() + " - " + ex.Message, (int)Lista_Erro.user_delete);
                return new UserDelete_Retorno() { cod_retorno = -4, msg_retorno = "Ocorreu um erro na solicitação de cancelamento." };
            }

            return new UserDelete_Retorno() { cod_retorno = 0, msg_retorno = "HARD CANCEL - Licença(s) cancelada(s)." };
        }

        public UserDelete_Retorno DeleteUserSoft(UserDelete usuario, ClientInfo client)
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
            string TransactionId = PegaHoraBrasilia().ToString("yyyyMMddHHmmssffffff");
            // cancela os produtos em lote
            foreach (DataRow dr in dt_produto_cliente.Rows)
            {
                var cancelamento_licenca = new KL_Conexao().KL_retorna_cancelamento_soft(dr["nm_subscriber_id"].ToString(),
                                        DateTime.Parse(PegaHoraBrasilia().ToString("yyyy-MM-dd") + "T" + PegaHoraBrasilia().ToString("HH:mm:ss.ffffff") + "Z"),
                                        count.ToString());

                controle.Add(new Controle_Envio() { comando = comando_kl.cancelar_soft, UnitId = count, SubscribeId = dr["nm_subscriber_id"].ToString(), id_cliente_licenca = dr["id_cliente_licenca"].ToString(), id_cliente_usuario = dr["id_cliente_usuario"].ToString() });
                comandos.Add((object)cancelamento_licenca);
                count++;
            }

            string xmlRequest, xmlContainer;

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = new KL_Conexao().Comando_KL(TransactionId, client.nm_usuario_certificado, client.nm_senha_certificado, client.nm_thumbprint, comandos.ToArray(), out xmlContainer, out xmlRequest);

            log_inserir(client.id_cliente.ToString() + "SOFT CANCEL - RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.user_delete);
            log_inserir(client.id_cliente.ToString() + "SOFT CANCEL - RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.user_delete);
            log_inserir(client.id_cliente.ToString() + "SOFT CANCEL - RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.user_delete);

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

                                soft_cancel_licenca_produto(licenca.id_cliente_licenca);

                                log_inserir(client.id_cliente.ToString() + "SOFT CANCEL  - Licença cancelar soft- " + licenca.id_cliente_licenca, (int)Lista_Erro.user_delete);
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
                            log_inserir(client.id_cliente.ToString() + "SOFT CANCEL  - ERRO ao cancelar Licença soft- " + licenca.id_cliente_licenca + " - " + erro.ErrorCode + "-" + erro.ErrorMessage, (int)Lista_Erro.user_delete);
                        }

                        return new UserDelete_Retorno() { cod_retorno = -2, msg_retorno = "Não foi possivel cancelar(SOFT) todas as licenças." };
                    }
                    else if (obj.GetType() == typeof(TransactionErrorType))
                    {
                        TransactionErrorType itemErro = new TransactionErrorType();

                        itemErro = (TransactionErrorType)obj;
                        log_inserir(client.id_cliente.ToString() + "SOFT CANCEL  - ERRO ao cancelar Licença soft- " + controle[0].id_cliente_usuario.ToString() + " - " + itemErro.ErrorCode + "-" + itemErro.ErrorMessage, (int)Lista_Erro.license_cancel);

                        return new UserDelete_Retorno() { cod_retorno = -3, msg_retorno = "Ocorreu um erro na solicitação de soft cancel." };

                    }

                }
            }
            catch (Exception ex)
            {
                log_inserir(client.id_cliente.ToString() + "SOFT CANCEL  - ERRO ao cancelar Licença soft- " + controle[0].id_cliente_usuario.ToString() + " - " + ex.Message, (int)Lista_Erro.user_delete);
                return new UserDelete_Retorno() { cod_retorno = -4, msg_retorno = "Ocorreu um erro na solicitação de soft cancel." };
            }

            // Deleta usuario 
            ClienteDeletar(dt_produto_cliente.Rows[0]["id_cliente_licenca"].ToString());
            log_inserir(client.id_cliente.ToString() + "SOFT CANCEL  - USUARIO PAUSADO - " + controle[0].id_cliente_usuario.ToString(), (int)Lista_Erro.user_delete);

            return new UserDelete_Retorno() { cod_retorno = 0, msg_retorno = "SOFT CANCEL - Licença(s) pausada(s)" };
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
            string TransactionId = DateTime.Now.ToString("yyyyMMddHHmmssffffff");

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
                DateTime startTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");

                if (!String.IsNullOrEmpty(usuario.EndDate))
                {
                    endTimeParam = usuario.EndDate;
                }

                if (!String.IsNullOrEmpty(usuario.MonthsToExpire))
                {
                    int.TryParse(usuario.MonthsToExpire, out int monthsToExpire);
                    endTimeParam = startTime.AddMonths(monthsToExpire).ToLongDateString();
                }

                var ativacao = new KL_Conexao().KL_retorna_ativacao(dt_produto[0]["qtd_licencas"].ToString(),
                                                                    dt_produto[0]["cd_produto_kl"].ToString(),
                                                                    startTime,
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

        public UserAdd_Retorno LicenseReActivation(string id_cliente_usuario, ClientInfo client)
        {
            List<Produto_Ativacao_Retorno> produtos = new List<Produto_Ativacao_Retorno>();

            var dt_licencas_soft_cancel = retorna_licencas_usuario_soft_cancel(client.id_cliente, id_cliente_usuario);

            List<object> comandos = new List<object>();
            List<Controle_Envio> controle = new List<Controle_Envio>();
            var produto_ativado = new List<Produto_Ativacao_Retorno>();
            int count_comandos = 1;
            foreach (DataRow row in dt_licencas_soft_cancel.Rows)
            {
                string subscription_id = row["nm_subscriber_id"].ToString();
                var ativacao = new KL_Conexao().KL_retorna_renew(subscription_id, count_comandos.ToString());

                controle.Add(new Controle_Envio()
                {
                    comando = comando_kl.renovar,
                    UnitId = count_comandos,
                    SubscribeId = subscription_id,
                    id_cliente_licenca = row["id_cliente_licenca"].ToString()
                });

                comandos.Add((object)ativacao);
                count_comandos++;
            }

            string xmlRequest, xmlContainer;

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();

            string TransactionId = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
            container = new KL_Conexao().Comando_KL(TransactionId, client.nm_usuario_certificado, client.nm_senha_certificado, client.nm_thumbprint,
                comandos.ToArray(), out xmlContainer, out xmlRequest);

            log_inserir(client.id_cliente.ToString() + "-REATIVACAO RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.usar_add);
            log_inserir(client.id_cliente.ToString() + "-REATIVACAO RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.usar_add);
            log_inserir(client.id_cliente.ToString() + "-REATIVACAO RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.usar_add);

            try
            {
                foreach (object obj in container.Items)
                {
                    if (obj.GetType() == typeof(SubscriptionResponseItemCollection))
                    {
                        SubscriptionResponseItemCollection itens = new SubscriptionResponseItemCollection();
                        itens = (SubscriptionResponseItemCollection)obj;

                        foreach (var item in itens.Items)
                        {
                            if (item.GetType() == typeof(BaseResponseItemType))
                            {
                                var itemDetalhe = (BaseResponseItemType)item;
                                var licenca = controle.Where(x => x.UnitId.ToString() == itemDetalhe.UnitId).FirstOrDefault();

                                reativacao_licenca_produto(licenca.id_cliente_licenca);

                                log_inserir(client.id_cliente.ToString() + "- RE-ATIVOU - " + licenca.id_cliente_licenca, (int)Lista_Erro.user_delete);
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
                            log_inserir(client.id_cliente.ToString() + "REATIVACAO - ERRO " + licenca.comando.ToString() + " - " + erro.ErrorCode + "-" + erro.ErrorMessage, (int)Lista_Erro.usar_add);
                        }
                    }
                    else if (obj.GetType() == typeof(TransactionErrorType))
                    {
                        TransactionErrorType itemErro = new TransactionErrorType();

                        itemErro = (TransactionErrorType)obj;
                        log_inserir(client.id_cliente.ToString() + "REATIVACAO - ERRO ao ativar Licença - " + controle[0].id_cliente_usuario.ToString() + " - " + itemErro.ErrorCode + "-" + itemErro.ErrorMessage, (int)Lista_Erro.usar_add);
                        ClienteDeletar(id_cliente_usuario, 1);
                        return new UserAdd_Retorno() { cod_retorno = -3, msg_retorno = "Ocorreu um erro na solicitação de ativacao." };
                    }
                }
            }
            catch (Exception ex)
            {
                log_inserir(client.id_cliente.ToString() + " -REATIVACAO ERRO ao ativar - " + controle[0].id_cliente_usuario.ToString() + " - " + ex.Message, (int)Lista_Erro.usar_add);
                return new UserAdd_Retorno() { cod_retorno = -4, msg_retorno = "Ocorreu um erro ao cadastrar o usuario." };
            }

            log_inserir(client.id_cliente.ToString() + "- RE-ATIVOU " + Newtonsoft.Json.JsonConvert.SerializeObject(produto_ativado), (int)Lista_Erro.usar_add);

            return new UserAdd_Retorno { cod_retorno = 0, msg_retorno = "REACTIVATION - Usuário Reativado!", produtos = produto_ativado };
        }

        public UserAdd_Retorno LicenseActivation(Activation activation, ClientInfo client, DataTable dt_usuario)
        {
            List<Produto_Ativacao_Retorno> produtos = new List<Produto_Ativacao_Retorno>();

            string id_cliente_usuario = dt_usuario.Rows[0]["id_cliente_usuario"].ToString();

            if (id_cliente_usuario == "-1")
            {
                return new UserAdd_Retorno() { cod_retorno = -1, msg_retorno = "Usuario já cadastrado." };
            }

            var dt_produtos = seleciona_produto_cliente(client.id_cliente, client.id_cliente_certificado);

            List<object> comandos = new List<object>();
            List<Controle_Envio> controle = new List<Controle_Envio>();
            var produto_ativado = new List<Produto_Ativacao_Retorno>();
            int count_comandos = 1;
            foreach (var product in activation.Products)
            {
                for (int i = 0; i < product.Qtd; i++)
                {
                    var dt_produto = dt_produtos.Select("nm_urn = '" + product.ProductID + "'");
                    if (dt_produto == null || dt_produto.Length == 0)
                    {
                        // se nao achou, deleta o usuaro
                        ClienteDeletar(id_cliente_usuario, 1);
                        return new UserAdd_Retorno() { cod_retorno = -1, msg_retorno = "Produto " + product.ProductID + " não encontrado." };
                    }

                    string endTimeParam = "indefinite";
                    DateTime startTime = DateTime.Parse(PegaHoraBrasilia().ToString("yyyy-MM-dd") + "T" + PegaHoraBrasilia().ToString("HH:mm:ss.ffffff") + "Z");

                    string subscription_id = Guid.NewGuid().ToString("N");

                    var ativacao = new KL_Conexao().KL_retorna_ativacao(dt_produto[0]["qtd_licencas"].ToString(),
                                                                    dt_produto[0]["cd_produto_kl"].ToString(),
                                                                    startTime,
                                                                    endTimeParam,
                                                                    count_comandos.ToString(),
                                                                    false,
                                                                    subscription_id);
                    controle.Add(new Controle_Envio()
                    {
                        comando = comando_kl.ativar,
                        UnitId = count_comandos,
                        SubscribeId = subscription_id,
                        id_produto_kl = dt_produto[0]["id_produto_kl"].ToString(),
                        id_cliente_usuario = id_cliente_usuario,
                        urn_produto = product.ProductID
                    });

                    comandos.Add((object)ativacao);
                    count_comandos++;
                }
            }

            string xmlRequest, xmlContainer;

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();

            string TransactionId = PegaHoraBrasilia().ToString("yyyyMMddHHmmssffffff");
            container = new KL_Conexao().Comando_KL(TransactionId, client.nm_usuario_certificado, client.nm_senha_certificado, client.nm_thumbprint,
                comandos.ToArray(), out xmlContainer, out xmlRequest);

            log_inserir(client.id_cliente.ToString() + "-ATIVACAO RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.usar_add);
            log_inserir(client.id_cliente.ToString() + "-ATIVACAO RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.usar_add);
            log_inserir(client.id_cliente.ToString() + "-ATIVACAO RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.usar_add);

            try
            {
                foreach (object obj in container.Items)
                {
                    if (obj.GetType() == typeof(SubscriptionResponseItemCollection))
                    {
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
                                    PegaHoraBrasilia().ToString("yyyy-MM-dd HH:mm:ss"),
                                    "",
                                    2);

                                log_inserir(client.id_cliente.ToString() + "ATIVACAO - Licença ativada (novo usuario) - " + id_cliente_licenca, (int)Lista_Erro.usar_add);
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
                            log_inserir(client.id_cliente.ToString() + "ATIVACAO - ERRO " + licenca.comando.ToString() + " - " + erro.ErrorCode + "-" + erro.ErrorMessage, (int)Lista_Erro.usar_add);
                        }
                    }
                    else if (obj.GetType() == typeof(TransactionErrorType))
                    {
                        TransactionErrorType itemErro = new TransactionErrorType();

                        itemErro = (TransactionErrorType)obj;
                        log_inserir(client.id_cliente.ToString() + "ATIVACAO - ERRO ao ativar Licença - " + controle[0].id_cliente_usuario.ToString() + " - " + itemErro.ErrorCode + "-" + itemErro.ErrorMessage, (int)Lista_Erro.usar_add);
                        ClienteDeletar(id_cliente_usuario, 1);
                        return new UserAdd_Retorno() { cod_retorno = -3, msg_retorno = "Ocorreu um erro na solicitação de ativacao." };
                    }
                }
            }
            catch (Exception ex)
            {
                log_inserir(client.id_cliente.ToString() + "ATIVACAO - ERRO ao ativar - " + controle[0].id_cliente_usuario.ToString() + " - " + ex.Message, (int)Lista_Erro.usar_add);
                return new UserAdd_Retorno() { cod_retorno = -4, msg_retorno = "Ocorreu um erro ao cadastrar o usuario." };
            }

            log_inserir(client.id_cliente.ToString() + "-ATIVACAO ATIVOU " + Newtonsoft.Json.JsonConvert.SerializeObject(produto_ativado), (int)Lista_Erro.usar_add);

            return new UserAdd_Retorno { cod_retorno = 0, msg_retorno = "ACTIVATION - Usuário Ativado!", produtos = produto_ativado };
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
            string TransactionId = DateTime.Now.ToString("HHmmssfff");
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

                                log_inserir(client.id_cliente.ToString() + " - Licença cancelar - " + licenca.id_cliente_licenca, (int)Lista_Erro.license_cancel);
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
                            log_inserir(client.id_cliente.ToString() + " - ERRO ao cancelar Licença - " + licenca.id_cliente_licenca + " - " + erro.ErrorCode + "-" + erro.ErrorMessage, (int)Lista_Erro.license_cancel);
                        }

                        return new LicenseCancel_Retorno() { cod_retorno = -2, msg_retorno = "Não foi possivel cancelar todas as licenças." };
                    }
                    else if (obj.GetType() == typeof(TransactionErrorType))
                    {
                        TransactionErrorType itemErro = new TransactionErrorType();

                        itemErro = (TransactionErrorType)obj;
                        log_inserir(client.id_cliente.ToString() + " - ERRO ao cancelar Licença - " + controle[0].id_cliente_usuario.ToString() + " - " + itemErro.ErrorCode + "-" + itemErro.ErrorMessage, (int)Lista_Erro.license_cancel);

                        return new LicenseCancel_Retorno() { cod_retorno = -3, msg_retorno = "Ocorreu um erro na solicitação de cancelamento." };

                    }

                }
            }
            catch (Exception ex)
            { 
                log_inserir(client.id_cliente.ToString() + " - ERRO ao cancelar Licença - " + controle[0].id_cliente_usuario.ToString() + " - " + ex.Message, (int)Lista_Erro.license_cancel);
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

            if (dt_usuario.Rows.Count == 0 && ativacao.IgnoreRegister)
            {
                UserAdd user = new UserAdd()
                {
                   UserID = ativacao.UserID,
                   StartDate = ativacao.StartDate,
                   UserDocument = "",
                   UserPlan = "",
                   Email = "",
                   EndDate = ativacao.EndDate
                };

                dt_usuario = addUser(user, client);
            }

            string id_cliente_usuario = dt_usuario.Rows[0]["id_cliente_usuario"].ToString();

            // Se existir, verifica se ja nao foi ativado esse produto
            var dt_produto_cliente = seleciona_licenca_produto(client.id_cliente, ativacao.UserID, ativacao.ProductID);

            // O produto ja esta ativado, retorna os dados do produto
            if (dt_produto_cliente.Rows.Count > 0)
            {
                var produtos = new List<Produto_Ativacao_Retorno>
                {
                    new Produto_Ativacao_Retorno()
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
                    }
                };

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
            string TransactionId = DateTime.Now.ToString("yyyyMMddHHmmssffffff");

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

        public void Provisionar(ClientInfo client, int id_produto, int qtd_ativacoes)
        {
            string id_cliente = client.id_cliente.ToString();
            var dt_produto_cliente = seleciona_produto_cliente(client.id_cliente, client.id_cliente_certificado);

            DataRow dt_produto_cliente_row = dt_produto_cliente.Select("id_produto_kl = " + id_produto)[0];

            string urn = dt_produto_cliente_row["nm_urn"].ToString();
            string produto_kl = dt_produto_cliente_row["nm_produto_kl"].ToString();
            string id_produto_kl = dt_produto_cliente_row["id_produto_kl"].ToString();
            string cd_produto_kl = dt_produto_cliente_row["cd_produto_kl"].ToString();
            string qtd_licencas = dt_produto_cliente_row["qtd_licencas"].ToString();

            List<object> comandos = new List<object>();
            List<Controle_Envio> controle = new List<Controle_Envio>();
            var produto_ativado = new List<Produto_Ativacao_Retorno>();

            string TransactionId = DateTime.Now.ToString("yyyyMMddHHmmssffffff");

            int count = 1;
            List<string> subscriberIDsLista = new List<string>();
            for (int i = 0; i < qtd_ativacoes; i++)
            {
                string subscription_id = Guid.NewGuid().ToString("N");
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

                log_inserir_provisionamento(client.id_cliente.ToString() + "- RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.license_ativation);
                log_inserir_provisionamento(client.id_cliente.ToString() + "- RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.license_ativation);
                log_inserir_provisionamento(client.id_cliente.ToString() + "- RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.license_ativation);

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

                                    id_provisionamento = InserirProvisionamento(controleEnvio.id_cliente_licenca, controleEnvio.id_cliente_usuario, controleEnvio.SubscribeId,
                                    controleEnvio.id_produto_kl, itemDetalhe.ActivationCode, "", "", "", "", true, DateTime.Now);
                                }
                                catch (Exception ex)
                                {
                                    log_inserir_provisionamento("Erro Provisionamento InserirProvisionamento - " + ex.Message, (int)Lista_Erro.license_ativation);
                                }
                            }

                            if (item.GetType() == typeof(SubscriptionResponseItemCollectionGetDownloadLinks))
                            {
                                try
                                {
                                    var itemDetalhe = (SubscriptionResponseItemCollectionGetDownloadLinks)item;
                                    var licenca = controle.Where(x => x.UnitId.ToString() == itemDetalhe.UnitId).FirstOrDefault();

                                    if (itemDetalhe.DownloadLinks != null && itemDetalhe.DownloadLinks.Count() > 0)
                                    {
                                        switch (licenca.comando)
                                        {
                                            case comando_kl.link_android:
                                                string link_ativacao_android = itemDetalhe.DownloadLinks[0].Url.Replace("market://", "https://play.google.com/store/apps/");
                                                AtualizarLinksProvisionamento(itemDetalhe.SubscriberId, linkAndroid: link_ativacao_android);
                                                break;

                                            case comando_kl.link_iphone:
                                                string link_ativacao_iphone = itemDetalhe.DownloadLinks[0].Url;
                                                AtualizarLinksProvisionamento(itemDetalhe.SubscriberId, linkIOS: link_ativacao_iphone);
                                                break;

                                            case comando_kl.link_mac:
                                                string link_ativacao_mac = itemDetalhe.DownloadLinks[0].Url;
                                                AtualizarLinksProvisionamento(itemDetalhe.SubscriberId, linkMac: link_ativacao_mac);
                                                break;

                                            case comando_kl.link_windows:
                                                string link_ativacao_windows = itemDetalhe.DownloadLinks[0].Url;
                                                AtualizarLinksProvisionamento(itemDetalhe.SubscriberId, linkWindows: link_ativacao_windows);
                                                break;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    log_inserir_provisionamento("Erro Provisionamento AtualizarLinksProvisionamento - " + ex.Message + " - " + ex.InnerException, (int)Lista_Erro.license_ativation);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log_inserir_provisionamento(client.id_cliente.ToString() + " - Erro Provisionamento - " + controle[0].id_cliente_usuario.ToString() + " - " + ex.Message, (int)Lista_Erro.license_ativation);
            }
        }

        public void ProvisionarTodas(ClientInfo client, DataTable provision, int id_produto)
        {
            string id_cliente = client.id_cliente.ToString();
            var dt_produto_cliente = seleciona_produto_cliente(client.id_cliente, client.id_cliente_certificado);

            DataRow dt_produto_cliente_row = dt_produto_cliente.Select("id_produto_kl = " + id_produto)[0];

            string urn = dt_produto_cliente_row["nm_urn"].ToString();
            string produto_kl = dt_produto_cliente_row["nm_produto_kl"].ToString();
            string id_produto_kl = dt_produto_cliente_row["id_produto_kl"].ToString();
            string cd_produto_kl = dt_produto_cliente_row["cd_produto_kl"].ToString();
            string qtd_licencas = dt_produto_cliente_row["qtd_licencas"].ToString();

            List<object> comandos = new List<object>();
            List<Controle_Envio> controle = new List<Controle_Envio>();
            var produto_ativado = new List<Produto_Ativacao_Retorno>();

            string TransactionId = DateTime.Now.ToString("yyyyMMddHHmmssffffff");

            int count = 1;
            List<string> subscriberIDsLista = new List<string>();
            for (int i = 0; i < 30; i++)
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

                log_inserir_provisionamento(client.id_cliente.ToString() + "- RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.license_ativation);
                log_inserir_provisionamento(client.id_cliente.ToString() + "- RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.license_ativation);
                log_inserir_provisionamento(client.id_cliente.ToString() + "- RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.license_ativation);

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

                                    id_provisionamento = InserirProvisionamento(controleEnvio.id_cliente_licenca, controleEnvio.id_cliente_usuario, controleEnvio.SubscribeId,
                                    controleEnvio.id_produto_kl, itemDetalhe.ActivationCode, "", "", "", "", true, DateTime.Now);
                                }
                                catch (Exception ex)
                                {
                                    log_inserir_provisionamento("Erro Provisionamento InserirProvisionamento - " + ex.Message, (int)Lista_Erro.license_ativation);
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
                                            AtualizarLinksProvisionamento(itemDetalhe.SubscriberId, linkAndroid: link_ativacao_android);
                                            break;

                                        case comando_kl.link_iphone:
                                            string link_ativacao_iphone = itemDetalhe.DownloadLinks[0].Url;
                                            AtualizarLinksProvisionamento(itemDetalhe.SubscriberId, linkIOS: link_ativacao_iphone);
                                            break;

                                        case comando_kl.link_mac:
                                            string link_ativacao_mac = itemDetalhe.DownloadLinks[0].Url;
                                            AtualizarLinksProvisionamento(itemDetalhe.SubscriberId, linkMac: link_ativacao_mac);
                                            break;

                                        case comando_kl.link_windows:
                                            string link_ativacao_windows = itemDetalhe.DownloadLinks[0].Url;
                                            AtualizarLinksProvisionamento(itemDetalhe.SubscriberId, linkWindows: link_ativacao_windows);
                                            break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    log_inserir_provisionamento("Erro Provisionamento AtualizarLinksProvisionamento - " + ex.Message, (int)Lista_Erro.license_ativation);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log_inserir(client.id_cliente.ToString() + " - Erro Provisionamento - " + controle[0].id_cliente_usuario.ToString() + " - " + ex.Message, (int)Lista_Erro.license_ativation);
                throw;
            }
        }

        public AtivacaoRetorno AtivarLicenca(Ativacao ativacao, ClientInfo clientInfo)
        {
            AtivacaoRetorno ativacaoRetorno = new AtivacaoRetorno();
            ativacaoRetorno.produtos = new List<Produto_Ativacao_Retorno>();

            DataTable produto_cliente = seleciona_produto_cliente(clientInfo.id_cliente, clientInfo.id_cliente_certificado);

            string cd_produto, urn_produto, nome_produto, descricao, imagem_produto;
            int id_produto;

            if (produto_cliente.Select("id_produto_kl = " + ativacao.id_produto).Count() > 0)
            {
                id_produto = (int)produto_cliente.Select("id_produto_kl = " + ativacao.id_produto)[0]["id_produto_kl"];
                cd_produto = produto_cliente.Select("id_produto_kl = " + ativacao.id_produto)[0]["cd_produto_kl"].ToString();
                urn_produto = produto_cliente.Select("id_produto_kl = " + ativacao.id_produto)[0]["nm_urn"].ToString();
                nome_produto = produto_cliente.Select("id_produto_kl = " + ativacao.id_produto)[0]["nm_produto_kl"].ToString();
                descricao = produto_cliente.Rows[0]["descricao"].ToString();
                imagem_produto = produto_cliente.Rows[0]["imagem_produto"].ToString();
            }
            else
            {
                ativacaoRetorno.cod_retorno = -1;
                ativacaoRetorno.msg_retorno = string.Format("id_produto ({0}) não encontrado", ativacao.id_produto);
                return ativacaoRetorno;
            }

            var ativarPorProvisionamento = AtivarPorProvisionamento(ativacao.id_cliente, ativacao.id_cliente_usuario, id_produto);

            if (ativarPorProvisionamento.Rows.Count == 0)
            {
                ativacaoRetorno.cod_retorno = -1;
                ativacaoRetorno.id_cliente = ativacao.id_cliente;
                ativacaoRetorno.id_cliente_usuario = ativacao.id_cliente_usuario;
                ativacaoRetorno.msg_retorno = "Não foi possivel ativar este produto, pois não possui provisionamento. Entre em contato com o suporte.";
                ativacaoRetorno.id_produto = id_produto;

                return ativacaoRetorno;
            }

            ativacaoRetorno.cod_retorno = 0;
            ativacaoRetorno.id_cliente = ativacao.id_cliente;
            ativacaoRetorno.id_cliente_usuario = ativacao.id_cliente_usuario;
            ativacaoRetorno.msg_retorno = "Produto ativado com sucesso.";

            if (ativarPorProvisionamento.Rows.Count > 0)
            {
                foreach (DataRow provisionamento in ativarPorProvisionamento.Rows)
                {
                    Produto_Ativacao_Retorno produto_ativacao_retorno = new Produto_Ativacao_Retorno()
                    {
                        ativado = true,
                        id_produto = id_produto,
                        cd_produto = cd_produto,
                        urn_produto = urn_produto,
                        nome_produto = nome_produto,
                        chave_ativacao = provisionamento["chaveAtivacao"].ToString(),
                        link_ativacao_android = provisionamento["linkAndroid"].ToString(),
                        link_ativacao_iphone = provisionamento["linkIOS"].ToString(),
                        link_ativacao_mac = provisionamento["linkMac"].ToString(),
                        link_ativacao_windows = provisionamento["linkWindows"].ToString(),
                        data_ativacao = Convert.ToDateTime(provisionamento["data_atualizacao"]),
                        descricao = descricao,
                        imagem_produto = imagem_produto
                    };

                    ativacaoRetorno.produtos.Add(produto_ativacao_retorno);
                }
            }

            return ativacaoRetorno;
        }

        public LoginRetorno login(Login login, ClientInfo clientInfo)
        {
            using (var client = new HttpClient())
            {
                LoginRetorno loginRetorno = Autenticacao(Tipo_autenticacao.mk, login, client);

                if (!loginRetorno.autenticado)
                {
                    return loginRetorno;
                }

                var cliente_usuario = seleciona_cliente_usuario(clientInfo.id_cliente, login.username);

                if (cliente_usuario.Rows.Count == 0)
                {
                    var cliente_usuario_inserir = ClienteUsuarioInserir(clientInfo.id_cliente, login.username, "", DateTime.Now, "", "", "");

                    var produto_cliente = seleciona_produto_cliente(clientInfo.id_cliente, clientInfo.id_cliente_certificado);

                    loginRetorno.id_cliente = clientInfo.id_cliente;
                    loginRetorno.id_cliente_usuario = Convert.ToInt32(cliente_usuario_inserir.Rows[0]["id_cliente_usuario"].ToString());
                }
                else
                {
                    int id_cliente_usuario = Convert.ToInt32(cliente_usuario.Rows[0]["id_cliente_usuario"].ToString());
                    loginRetorno.id_cliente = clientInfo.id_cliente;
                    loginRetorno.id_cliente_usuario = id_cliente_usuario;
                }

                return loginRetorno;
            }
        }

        public LoginRetorno Autenticacao(Tipo_autenticacao tipo_Autenticacao, Login login, HttpClient client)
        {
            try
            {
                switch (tipo_Autenticacao)
                {
                    case Tipo_autenticacao.mk:
                        WSMKUserSenhaSAC userSac = GetUserSAC(login.username, login.password, client);

                        if (userSac.CodigoPessoa <= 0)
                        {
                            return new LoginRetorno() { cod_retorno = -1, msg_retorno = "Não encontrado usuário no SAC.", autenticado = false };
                        }

                        WSMKContratosPorCliente contratosPorCliente = GetContratosPorCliente(userSac, client);

                        List<string> planos_acesso = new List<string>()
                        {
                            "SEA ULTRA",
                            "SEA HIPER",
                            "SEA SUPER",
                            "KASPERKSY"
                        };

                        List<ContratosAtivo> contratosAtivos = new List<ContratosAtivo>();
                        foreach (var contrato in contratosPorCliente.ContratosAtivos)
                        {
                            foreach (var plano in planos_acesso)
                            {
                                if (contrato.plano_acesso.Contains(plano))
                                {
                                    contratosAtivos.Add(contrato);
                                }
                            }
                        }

                        if (contratosAtivos == null || contratosAtivos.Count == 0)
                        {
                            return new LoginRetorno()
                            {
                                cod_retorno = -2,
                                msg_retorno = "Este usuário não possui contratos que habilitam o produto Kaspersky. " +
                                    "Para saber mais entre em contato com a nossa central de relacionamento através do número: 91 2992-0230.",
                                autenticado = false
                            };
                        }

                        WSMKConexoesPorCliente wSMKConexoesPorCliente = GetConexoesPorCliente(userSac, client);

                        if (wSMKConexoesPorCliente.Conexoes.Count == 0)
                        {
                            return new LoginRetorno() { cod_retorno = -1, msg_retorno = "Nenhuma conexão foi encontrado para este usuário", autenticado = false };
                        }

                        if (wSMKConexoesPorCliente.Conexoes.Count > 0)
                        {
                            List<Conexoes> conexoes = wSMKConexoesPorCliente.Conexoes.Where(w => contratosAtivos.Select(s => s.codcontrato).Contains(w.codigo_contrato)).ToList();

                            if (conexoes.Count == 0)
                            {
                                return new LoginRetorno() { cod_retorno = -2, msg_retorno = "Este usuário não possui conexões para os contratos que habilitam o produto Kaspersky. " +
                                    "Para saber mais entre em contato com a nossa central de relacionamento através do número: 91 2992-0230.", autenticado = false };
                            }

                            if (conexoes.Where(w => w.acesso).Count() == 0)
                            {
                                return new LoginRetorno()
                                {
                                    cod_retorno = -2,
                                    msg_retorno = "Prezado cliente, informamos que no momento sua conexão encontra-se suspensa. " +
                                            "Para saber mais entre em contato com a nossa central de relacionamento através do número: 91 2992-0230.",
                                    autenticado = false
                                };
                            }

                            return new LoginRetorno() { cod_retorno = 0, msg_retorno = "", autenticado = true };
                        }

                        break;
                }

                return new LoginRetorno() { cod_retorno = -1, msg_retorno = "Erro ao logar usuário.", autenticado = false };
                
            }
            catch (Exception ex)
            {
                log_inserir_provisionamento("Autenticacao: " + ex.Message, 0);
                return new LoginRetorno() { cod_retorno = -1, msg_retorno = "Erro ao executar o processo de autenticação ex:" + ex.Message, autenticado = false };
            }
        }

        public MKWSAutenticacao GetTokenMK(HttpClient client)
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

        public WSMKUserSenhaSAC GetUserSAC(string username, string password, HttpClient client)
        {
            MKWSAutenticacao autenticacao = GetTokenMK(client);

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

        public WSMKContratosPorCliente GetContratosPorCliente(WSMKUserSenhaSAC userSac, HttpClient client)
        {
            MKWSAutenticacao autenticacao = GetTokenMK(client);

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

        public WSMKConexoesPorCliente GetConexoesPorCliente(WSMKUserSenhaSAC userSac, HttpClient client)
        {
            MKWSAutenticacao autenticacao = GetTokenMK(client);

            var responseConexoes =
                client.GetAsync($@"{@ConfigurationManager.AppSettings["MK_WSMKConexoesPorCliente"]}{loginInterno.sys}&token={autenticacao.token}&cd_cliente={userSac.CodigoPessoa}").Result;

            if (responseConexoes.Content != null)
            {
                var responseContent = responseConexoes.Content.ReadAsStringAsync().Result;

                try
                {
                    var response_conexoes_cliente = Newtonsoft.Json.JsonConvert.DeserializeObject<WSMKConexoesPorCliente>(responseContent);
                    return response_conexoes_cliente;
                }
                catch (Exception ex)
                {
                }
            }

            return new WSMKConexoesPorCliente();
        }

        public List<WSMKPlanosAcesso> GetPlanosAcesso()
        {
            List<WSMKPlanosAcesso> wSMKPlanosAcessos = new List<WSMKPlanosAcesso>();
            MKWSAutenticacao autenticacao = new MKWSAutenticacao();
            List<int> codigos_plano = new List<int> { 135, 136, 137, 452 };

            foreach (var codigo_plano in codigos_plano)
            {
                WSMKPlanosAcesso planoKaspersky = new WSMKPlanosAcesso();
                planoKaspersky.contratos = new List<Contratos>();

                using (var client = new HttpClient())
                {
                    #region Get Planos Ativos
                    autenticacao = GetTokenMK(client);
                    var responsePlanosAcessoAtivos =
                        client.GetAsync($@"{@ConfigurationManager.AppSettings["MK_WSMKPlanosAcesso"]}{loginInterno.sys}&token={autenticacao.token}&codigo_plano={codigo_plano}&cancelado=N&suspenso=N&aguarda_ativacao=N").Result;

                    if (responsePlanosAcessoAtivos.Content != null)
                    {
                        var responseContent = responsePlanosAcessoAtivos.Content.ReadAsStringAsync().Result;

                        try
                        {
                            WSMKPlanosAcesso response_planos_ativos = Newtonsoft.Json.JsonConvert.DeserializeObject<WSMKPlanosAcesso>(responseContent);
                            planoKaspersky.CodigoPlano = response_planos_ativos.CodigoPlano;
                            planoKaspersky.NomePlano = response_planos_ativos.NomePlano;
                            planoKaspersky.Status = response_planos_ativos.Status;

                            planoKaspersky.contratos.AddRange(response_planos_ativos.contratos);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    #endregion

                    #region Get Planos Cancelados
                    autenticacao = GetTokenMK(client);
                    var responsePlanosAcessoCancelados =
                        client.GetAsync($@"{@ConfigurationManager.AppSettings["MK_WSMKPlanosAcesso"]}{loginInterno.sys}&token={autenticacao.token}&codigo_plano={codigo_plano}&cancelado=S&suspenso=N&aguarda_ativacao=N").Result;

                    if (responsePlanosAcessoCancelados.Content != null)
                    {
                        var responseContent = responsePlanosAcessoCancelados.Content.ReadAsStringAsync().Result;

                        try
                        {
                            WSMKPlanosAcesso response_planos_cancelados = Newtonsoft.Json.JsonConvert.DeserializeObject<WSMKPlanosAcesso>(responseContent);
                            planoKaspersky.CodigoPlano = response_planos_cancelados.CodigoPlano;
                            planoKaspersky.NomePlano = response_planos_cancelados.NomePlano;
                            planoKaspersky.Status = response_planos_cancelados.Status;
                            planoKaspersky.contratos.AddRange(response_planos_cancelados.contratos);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    #endregion

                    #region Get Planos Suspensos
                    autenticacao = GetTokenMK(client);
                    var responsePlanosAcessoSuspensos =
                        client.GetAsync($@"{@ConfigurationManager.AppSettings["MK_WSMKPlanosAcesso"]}{loginInterno.sys}&token={autenticacao.token}&codigo_plano={codigo_plano}&cancelado=N&suspenso=S&aguarda_ativacao=N").Result;

                    if (responsePlanosAcessoSuspensos.Content != null)
                    {
                        var responseContent = responsePlanosAcessoSuspensos.Content.ReadAsStringAsync().Result;

                        try
                        {
                            WSMKPlanosAcesso response_planos_suspensos = Newtonsoft.Json.JsonConvert.DeserializeObject<WSMKPlanosAcesso>(responseContent);
                            planoKaspersky.CodigoPlano = response_planos_suspensos.CodigoPlano;
                            planoKaspersky.NomePlano = response_planos_suspensos.NomePlano;
                            planoKaspersky.Status = response_planos_suspensos.Status;
                            planoKaspersky.contratos.AddRange(response_planos_suspensos.contratos);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    #endregion
                }

                wSMKPlanosAcessos.Add(planoKaspersky);
            }

            return wSMKPlanosAcessos;
        }

        #region atualizacao BD
        public void ClienteInserir(string nm_cliente)
        {
            DataBase db = new DataBase();
            db.procedure = "p_insere_cliente";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@nm_cliente", nm_cliente)
            };

            db.parametros = par;
            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_API);
        }

        public void ClienteTokenInserir(string nm_token, DateTime dt_validade_token, int id_status)
        {
            DataBase db = new DataBase();
            db.procedure = "p_insere_cliente_token";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@nm_token", nm_token),
                db.retorna_parametros("@dt_validade_token", dt_validade_token.ToString()),
                db.retorna_parametros("@id_status", id_status.ToString())
            };

            db.parametros = par;
            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_API);
        }

        public void ClienteProdutoKlInserir(int id_cliente, int id_produto)
        {
            DataBase db = new DataBase();
            db.procedure = "p_insere_cliente_produto_kl";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente", id_cliente.ToString()),
                db.retorna_parametros("@id_produto", id_produto.ToString())
            };

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

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente", id_cliente.ToString()),
                db.retorna_parametros("@nm_user_id", nm_user_id),
                //par.Add(db.retorna_parametros("@nm_transaction_id", nm_transaction_id));
                db.retorna_parametros("@nm_email", nm_email),
                db.retorna_parametros("@dt_start", dt_start.ToString()),
                db.retorna_parametros("@dt_end", dt_end),
                db.retorna_parametros("@nm_user_document", nm_user_document == null ? "": nm_user_document),
                db.retorna_parametros("@nm_user_plan", nm_user_plan == null ? "": nm_user_plan)
            };

            db.parametros = par;
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public string ClienteLicencaInserir(string id_cliente_licenca, string id_cliente_usuario, string id_produto, string cd_ativacao_kl, string nm_subscriber_id, string nm_ativacao_android, 
                                            string nm_ativacao_iphone, string nm_ativacao_windows, string nm_ativacao_mac, string dt_ativacao, string dt_expiracao, int id_status)
        {
            DataBase db = new DataBase();
            db.procedure = "p_insere_cliente_licenca";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente_licenca", id_cliente_licenca.ToString()),
                db.retorna_parametros("@id_cliente_usuario", id_cliente_usuario.ToString()),
                db.retorna_parametros("@id_produto", id_produto.ToString()),
                db.retorna_parametros("@cd_ativacao_kl", cd_ativacao_kl.ToString()),
                db.retorna_parametros("@nm_subscriber_id", nm_subscriber_id.ToString()),
                db.retorna_parametros("@nm_ativacao_android", nm_ativacao_android.ToString()),
                db.retorna_parametros("@nm_ativacao_iphone", nm_ativacao_iphone.ToString()),
                db.retorna_parametros("@nm_ativacao_windows", nm_ativacao_windows.ToString()),
                db.retorna_parametros("@nm_ativacao_mac", nm_ativacao_mac.ToString()),
                db.retorna_parametros("@dt_ativacao", dt_ativacao.ToString()),
                db.retorna_parametros("@dt_expiracao", dt_expiracao.ToString()),
                db.retorna_parametros("@id_status", id_status.ToString())
            };

            db.parametros = par;
            return Generico.Exec_retorno_string(db, DAL.Constantes_DAL.Conexao_API);
        }

        public string InserirProvisionamento(string id_cliente, string id_cliente_usuario, string subscriber_id, string id_produto, string chaveAtivacao, string linkIOS, string linkMac, string linkAndroid,
                                             string linkWindows, bool status, DateTime data_criacao)
        {
            DataBase db = new DataBase();
            db.procedure = "p_insere_provisionamento";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_provisionamento", "0"),
                db.retorna_parametros("@id_cliente", id_cliente.ToString()),
                db.retorna_parametros("@id_cliente_usuario", id_cliente_usuario.ToString()),
                db.retorna_parametros("@subscriber_id", subscriber_id.ToString()),
                db.retorna_parametros("@id_produto", id_produto.ToString()),
                db.retorna_parametros("@chaveAtivacao", chaveAtivacao.ToString()),
                db.retorna_parametros("@linkIOS", linkIOS.ToString()),
                db.retorna_parametros("@linkMac", linkMac.ToString()),
                db.retorna_parametros("@linkAndroid", linkAndroid.ToString()),
                db.retorna_parametros("@linkWindows", linkWindows.ToString()),
                db.retorna_parametros("@status", status.ToString()),
                db.retorna_parametros("@data_criacao", data_criacao.ToString("yyyy-MM-dd HH:mm:ss"))
            };

            db.parametros = par;
            return Generico.Exec_retorno_string(db, DAL.Constantes_DAL.Conexao_API);
        }

        public void AtualizarLinksProvisionamento(string subscriber_id, string linkIOS = "", string linkMac = "", string linkAndroid = "", string linkWindows = "")
        {
            try
            {
                DataBase db = new DataBase();
                db.procedure = "p_atualiza_links_provisionamento";

                List<parametros> par = new List<parametros>
                {
                    db.retorna_parametros("@subscriber_id", subscriber_id),
                    db.retorna_parametros("@linkIOS", linkIOS),
                    db.retorna_parametros("@linkMac", linkMac),
                    db.retorna_parametros("@linkAndroid", linkAndroid),
                    db.retorna_parametros("@linkWindows", linkWindows)
                };

                db.parametros = par;
                Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_API);
            }
            catch (Exception ex)
            {
                log_inserir_provisionamento(string.Format($"Erro Inside AtualizarLinksProvisionamento - {ex.Message} - " +
                    $"subscriber_id: {subscriber_id}, IOS:{linkIOS}, MAC: {linkMac}, Android:{linkAndroid}, windows:{linkWindows}"), (int)Lista_Erro.license_ativation);
            }
        }

        public void ClienteDeletar(string id_cliente_usuario, int delete = 0)
        {
            DataBase db = new DataBase();
            db.procedure = "p_delete_cliente_usuario";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente_usuario", id_cliente_usuario.ToString()),
                db.retorna_parametros("@delete", delete.ToString())
            };

            db.parametros = par;
            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable seleciona_licenca_produto(int id_cliente, string nm_user_id, string nm_urn)
        {
            DataBase db = new DataBase();
            db.procedure = "p_consulta_cliente_licenca";
            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente", id_cliente.ToString()),
                db.retorna_parametros("@nm_user_id", nm_user_id),
                db.retorna_parametros("@nm_urn", nm_urn)
            };

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable seleciona_produto_cliente(int id_cliente, int id_cliente_certificado)
        {
            DataBase db = new DataBase();
            db.procedure = "p_consulta_cliente_produto";
            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente", id_cliente.ToString()),
                db.retorna_parametros("@id_cliente_certificado", id_cliente_certificado.ToString())
            };

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable seleciona_cliente_usuario(int id_cliente, string nm_user_id)
        {
            DataBase db = new DataBase();
            db.procedure = "p_consulta_usuario";
            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente", id_cliente.ToString()),
                db.retorna_parametros("@nm_user_id", nm_user_id.ToString())
            };

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public void cancela_licenca_produto(string id_cliente_licenca)
        {
            DataBase db = new DataBase();
            db.procedure = "p_cancelamento_cliente_licenca";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente_licenca", id_cliente_licenca.ToString()),
                db.retorna_parametros("@dt_cancelamento", PegaHoraBrasilia().ToString("yyyy-MM-dd HH:mm:ss")),
            };

            db.parametros = par;

            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_API);

            return;
        }

        public void IntegracaoDeletaUser(string id_subscriber)
        {
            DataBase db = new DataBase();
            db.procedure = "p_integracao_deleta_ativacao_por_subscriber_id";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_subscriber", id_subscriber),
            };

            db.parametros = par;

            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_API);

            return;
        }

        public void soft_cancel_licenca_produto(string id_cliente_licenca)
        {
            DataBase db = new DataBase();
            db.procedure = "p_soft_cancel_cliente_licenca";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente_licenca", id_cliente_licenca.ToString()),
                db.retorna_parametros("@dt_cancelamento", PegaHoraBrasilia().ToString("yyyy-MM-dd HH:mm:ss")),
            };

            db.parametros = par;

            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_API);

            return;
        }

        public void reativacao_licenca_produto(string id_cliente_licenca)
        {
            DataBase db = new DataBase();
            db.procedure = "p_reativacao_cliente_licenca";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente_licenca", id_cliente_licenca.ToString())
            };

            db.parametros = par;

            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_API);

            return;
        }

        public DataTable retorna_licencas_usuario_soft_cancel(int id_cliente, string id_cliente_usuario)
        {
            DataBase db = new DataBase();
            db.procedure = "p_get_licencas_user_soft_cancel";
            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente", id_cliente.ToString()),
                db.retorna_parametros("@id_cliente_usuario", id_cliente_usuario.ToString())
            };

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable Deletar_provisionamento(string id_cliente_usuario, string id_cliente)
        {
            DataBase db = new DataBase();
            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente_usuario", id_cliente_usuario.ToString()),
                db.retorna_parametros("@id_cliente", id_cliente.ToString())
            };

            db.parametros = par;

            db.procedure = "p_deletar_provisionamento";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable Retorna_provisionamento(int id_cliente_usuario, int id_produto)
        {
            DataBase db = new DataBase();
            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente_usuario", id_cliente_usuario.ToString()),
                db.retorna_parametros("@id_produto", id_produto.ToString())
            };

            db.parametros = par;

            db.procedure = "p_retorna_provisionamento";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable Retorna_provisionamento_por_id_cliente(int id_cliente, int id_produto)
        {
            DataBase db = new DataBase();
            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente", id_cliente.ToString()),
                db.retorna_parametros("@id_produto", id_produto.ToString())
            };

            db.parametros = par;

            db.procedure = "p_retorna_provisionamento_por_id_cliente";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable ConsultaVendorTheme(string vendor_domain_name)
        {
            DataBase db = new DataBase();
            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@vendorDomainName", vendor_domain_name.ToString())
            };

            db.parametros = par;

            db.procedure = "p_consulta_vendor_theme";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable InserirVendorTheme(VendorTheme vendorTheme)
        {
            DataBase db = new DataBase();

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente", vendorTheme.id_cliente.ToString()),
                db.retorna_parametros("@vendorDomainName", vendorTheme.vendorDomainName),
                db.retorna_parametros("@primaryColor", vendorTheme.primaryColor),
                db.retorna_parametros("@secondaryColor", vendorTheme.secondaryColor),
                db.retorna_parametros("@logoImage", vendorTheme.logoImage),
                db.retorna_parametros("@vendorTitleImage", vendorTheme.vendorTitleImage),
                db.retorna_parametros("@bannerImage", vendorTheme.bannerImage),
                db.retorna_parametros("@isDarkTheme", vendorTheme.isDarkTheme.ToString()),
                db.retorna_parametros("@bannerImageMobile", vendorTheme.bannerImageMobile.ToString())
            };

            db.parametros = par;

            db.procedure = "p_inserir_vendor_theme";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable DeleteVendorTheme(VendorTheme vendorTheme)
        {
            DataBase db = new DataBase();

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_vendor_theme", vendorTheme.id_vendor_theme.ToString())
            };

            db.parametros = par;

            db.procedure = "p_delete_vendor_theme";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable AtivarPorProvisionamento(int id_cliente, int id_cliente_usuario, int id_produto)
        {
            DataBase db = new DataBase();

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente", id_cliente.ToString()),
                db.retorna_parametros("@id_cliente_usuario", id_cliente_usuario.ToString()),
                db.retorna_parametros("@data_atualizacao", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                db.retorna_parametros("@id_produto", id_produto.ToString())
            };

            db.parametros = par;

            db.procedure = "p_ativar_por_provisionamento";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public GetProdutosRetorno GetProdutos(GetProdutosObj getProdutos, ClientInfo clientInfo)
        {
            GetProdutosRetorno getProdutosRetorno = new GetProdutosRetorno();
            getProdutosRetorno.id_cliente_usuario = getProdutos.id_cliente_usuario;
            getProdutosRetorno.produtos = new List<Produto_Ativacao_Retorno>();

            if (getProdutos.id_cliente_usuario == 0)
            {
                getProdutosRetorno.cod_retorno = -1;
                getProdutosRetorno.msg_retorno = "id_cliente_usuario não encontrado";
                return getProdutosRetorno;
            }

            var produto_cliente = seleciona_produto_cliente(clientInfo.id_cliente, clientInfo.id_cliente_certificado);

            List<Produto_Ativacao_Retorno> produtoAtivacaoLista = new List<Produto_Ativacao_Retorno>();
            foreach (DataRow prod_cliente in produto_cliente.Rows)
            {
                Produto_Ativacao_Retorno produto_ativacao_retorno = new Produto_Ativacao_Retorno()
                {
                    ativado = false,
                    cd_produto = prod_cliente["cd_produto_kl"].ToString(),
                    urn_produto = prod_cliente["nm_urn"].ToString(),
                    nome_produto = prod_cliente["nm_produto_kl"].ToString(),
                    id_produto = Convert.ToInt32(prod_cliente["id_produto_kl"].ToString()),
                    descricao = prod_cliente["descricao"].ToString(),
                    imagem_produto = prod_cliente["imagem_produto"].ToString()
                };

                var provisionamentoLista = Retorna_provisionamento(getProdutos.id_cliente_usuario, Convert.ToInt32(prod_cliente["id_produto_kl"]));

                if (provisionamentoLista.Rows.Count > 0)
                {
                    produto_ativacao_retorno.ativado = true;
                    produto_ativacao_retorno.chave_ativacao = provisionamentoLista.Rows[0]["chaveAtivacao"].ToString();
                    produto_ativacao_retorno.link_ativacao_android = provisionamentoLista.Rows[0]["linkAndroid"].ToString();
                    produto_ativacao_retorno.link_ativacao_iphone = provisionamentoLista.Rows[0]["linkIOS"].ToString();
                    produto_ativacao_retorno.link_ativacao_mac = provisionamentoLista.Rows[0]["linkMac"].ToString();
                    produto_ativacao_retorno.link_ativacao_windows = provisionamentoLista.Rows[0]["linkWindows"].ToString();
                    produto_ativacao_retorno.data_ativacao = Convert.ToDateTime(provisionamentoLista.Rows[0]["data_atualizacao"]);
                }

                getProdutosRetorno.produtos.Add(produto_ativacao_retorno);
            }

            return getProdutosRetorno;
        }

        public List<GetUsersInfoReturn> GetUsersInfoReturn(string id_cliente)
        {
            List<GetUsersInfoReturn> listReturn = new List<GetUsersInfoReturn>();

            DataBase db = new DataBase();
            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente", id_cliente.ToString())
            };

            db.parametros = par;

            db.procedure = "p_get_users_info";
            DataTable usersInfoTable = Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);

            foreach (DataRow row in usersInfoTable.Rows)
            {
                GetUsersInfoReturn getUsersInfoReturn = new GetUsersInfoReturn()
                {
                    cd_ativacao_kl = row["cd_ativacao_kl"].ToString(),
                    dt_ativacao = row["dt_ativacao"].ToString(),
                    dt_cancelamento = row["dt_cancelamento"].ToString(),
                    dt_start = Convert.ToDateTime(row["dt_start"]),
                    nm_cliente = row["nm_cliente"].ToString(),
                    nm_email = row["nm_email"].ToString(),
                    nm_user_id = row["nm_user_id"].ToString(),
                    nm_user_plan = row["nm_user_plan"].ToString()
                };

                listReturn.Add(getUsersInfoReturn);
            }

            return listReturn;
        }

        public List<GetUsersReturn> GetUsersReturn(string id_cliente, string userID)
        {
            List<GetUsersReturn> listReturn = new List<GetUsersReturn>();

            DataBase db = new DataBase();
            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente", id_cliente),
                db.retorna_parametros("@userID", userID)
            };

            db.parametros = par;

            db.procedure = "p_get_users";
            DataTable usersInfoTable = Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);

            foreach (DataRow row in usersInfoTable.Rows)
            {
                GetUsersReturn getUsersReturn = new GetUsersReturn()
                {
                    cd_activation = row["cd_ativacao_kl"].ToString(),
                    dt_ativacao = row["dt_ativacao"].ToString(),
                    dt_hard_cancel = row["dt_cancelamento"].ToString(),
                    dt_soft_cancel = row["dt_soft_cancel"].ToString(),
                    email = row["nm_email"].ToString(),
                    users_id = row["nm_user_id"].ToString(),
                    product = row["nm_produto_kl"].ToString()
                };

                listReturn.Add(getUsersReturn);
            }

            return listReturn;
        }

        public string GetInfoKaspersky(string subscriber_id, string usernameCertificado, string passwordCertificado, 
            string thumbprint)
        {
            // cancela cliente Neo
            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = new KL_Conexao().GetInfoKaspersky(DateTime.Now.ToString("yyyyMMddHHmmss"), subscriber_id, 
                usernameCertificado, passwordCertificado, thumbprint, out string xmlContainer, out string xmlRequest);

            log_inserir("GetInfoContainer subscriber_id= " + subscriber_id + "- " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.get_info);
            log_inserir("GetInfoRequest subscriber_id= " + subscriber_id + "- " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.get_info);
            log_inserir("GetInfoResponse subscriber_id= " + subscriber_id + "- " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.get_info);

            return "OK";
        }

        public DataTable ConsultaProdutosCliente(int id_cliente)
        {
            DataBase db = new DataBase();
            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente", id_cliente.ToString())
            };

            db.parametros = par;

            db.procedure = "p_consulta_produtos_cliente";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        #endregion

        #region token
        public ClientInfo ValidaToken(string token)
        {
            DataBase db = new DataBase();
            db.procedure = "p_retorna_certificado";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@nm_token", token)
            };

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
            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@nm_log", nm_log),
                db.retorna_parametros("@id_tipo_log", id_tipo_log.ToString())
            };
            db.parametros = par;

            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_API);
        }

        public void log_inserir_provisionamento(string nm_log, int id_tipo_log)
        {
            DataBase db = new DataBase();
            db.procedure = "p_insere_log_provisionamento";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@nm_log", nm_log),
                db.retorna_parametros("@id_tipo_log", id_tipo_log.ToString())
            };
            db.parametros = par;

            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_API);
        }

        #endregion
    }
}