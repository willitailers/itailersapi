using BLL;
using BLL.KL_API;
using DAL;
using Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KL_UOL.Models
{
    public class Ativacao_Envio
    {
        public string api_token { set; get; }

        public string cd_validacao { set; get; }
    }

    public class Ativacao_KL
    {
        public string api_token { set; get; }
        public string cd_validacao { set; get; }
        public string id_transacao { set; get; }
        public string id_produto { set; get; }
        public string id_subscribe { set; get; }
        public string qtd_licenca { set; get; }
        public DateTime data_ini { set; get; }
        public string data_fim { set; get; }
        public bool ignore_user { set; get; }
    }

    public class Ativacao_Retorno
    {
        public int cod_retorno { set; get; }

        public int dv_ativado { set; get; }

        public string msg_retorno { set; get; }

        public string link_ativacao { set; get; }

        public string chave_ativacao { set; get; }
    }

    public class Ativacao_Controle
    {        
        public Ativacao_Retorno ValidaCodigo(string cd_ativacao)
        {
            var retorno = new Ativacao_Retorno { cod_retorno = 0, msg_retorno = "", link_ativacao = "" };
            int numero = 0;

            // codigo vazio
            if (string.IsNullOrEmpty(cd_ativacao))
            {
                return new Ativacao_Retorno() { cod_retorno = -3, msg_retorno = "Codigo vazio", link_ativacao = "" };
            }

            //se nao for numerico
            if (!int.TryParse(cd_ativacao, out numero))
            {
                return new Ativacao_Retorno() { cod_retorno = -4, msg_retorno = "Codigo incorreto", link_ativacao = "" };
            }

            // se nao tiver 16 digitos
            if(cd_ativacao.Length != 16)
            {
                return new Ativacao_Retorno() { cod_retorno = -4, msg_retorno = "Codigo incorreto", link_ativacao = "" };
            }


            return retorno;
        }
        
        #region token
        public bool ValidaToken(string token)
        {
            bool retorno = false;

            DataTable dt = new DataTable();

            dt = seleciona_token();

            foreach (DataRow dr in dt.Rows)
            {
                if (token == dr["cd_token"].ToString() && dr["dv_ativo"].ToString() == "1")
                    retorno = true;
            }

            return retorno;
        }

        public DataTable seleciona_token()
        {
            DataBase db = new DataBase();
            db.procedure = "p_consulta_token";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }


        #endregion

        #region Ativar

        public int AtivarCodigo(string id_lote_kl_item, string dt_expiracao_kl, string dt_encerramento, string nm_link_url)
        {
            DataBase db = new DataBase();
            db.procedure = "p_ativar_codigo";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@id_lote_kl_item", id_lote_kl_item));
            par.Add(db.retorna_parametros("@dt_expiracao_kl", dt_expiracao_kl));
            par.Add(db.retorna_parametros("@dt_encerramento", dt_encerramento));
            par.Add(db.retorna_parametros("@nm_link_url", nm_link_url));

            db.parametros = par;

            return int.Parse(Generico.Exec_retorno_string(db, DAL.Constantes_DAL.Conexao_AV));
        }

        #endregion

        #region codigo

        public Ativacao_Retorno ativar(Ativacao_Envio ativacao)
        {
            log_inserir("Solicitacao de ativacao - token " + (string.IsNullOrEmpty(ativacao.api_token) ? "vazio" : ativacao.api_token) + " - codigo " + (string.IsNullOrEmpty(ativacao.cd_validacao) ? "vazio" : ativacao.cd_validacao), "10");
            // validação
            if (!ValidaToken(ativacao.api_token))
            {
                log_inserir("Solicitacao de ativacao - token invalido " + (string.IsNullOrEmpty(ativacao.api_token) ? "vazio" : ativacao.api_token), "12");
                return new Ativacao_Retorno() { cod_retorno = -2, msg_retorno = "Token invalido" };
            }


            if (string.IsNullOrEmpty(ativacao.cd_validacao))
            {
                log_inserir("Solicitacao de ativacao - codigo " + (string.IsNullOrEmpty(ativacao.cd_validacao) ? "vazio" : ativacao.cd_validacao), "12");
                return new Ativacao_Retorno() { cod_retorno = -3, msg_retorno = "Codigo vazio", link_ativacao = "" };
            }
                

            Int64 cd_validacao_ = 0;

            if(!Int64.TryParse(ativacao.cd_validacao, out cd_validacao_) || ativacao.cd_validacao.Length != 8)
            {
                log_inserir("Solicitacao de ativacao - Codigo incorreto " + (string.IsNullOrEmpty(ativacao.cd_validacao) ? "vazio" : ativacao.cd_validacao), "12");
                return new Ativacao_Retorno() { cod_retorno = -4, msg_retorno = "Codigo incorreto", link_ativacao = "" };
            }

            int dv_ativado = 0;

            var dt = seleciona_token(ativacao.cd_validacao);

            if (dt.Rows.Count <= 0)
            {
                log_inserir("Solicitacao de ativacao - Codigo não cadastrado " + (string.IsNullOrEmpty(ativacao.cd_validacao) ? "vazio" : ativacao.cd_validacao), "12");
                return new Ativacao_Retorno() { cod_retorno = -5, msg_retorno = "Codigo não cadastrado", link_ativacao = "" };
            }

            if (!string.IsNullOrEmpty(dt.Rows[0]["dt_limite"].ToString()) && DateTime.Parse(dt.Rows[0]["dt_limite"].ToString()).Date < DateTime.Now.Date)
            {
                log_inserir("Solicitacao de ativacao - Periodo de ativação expirado " + (string.IsNullOrEmpty(ativacao.cd_validacao) ? "vazio" : ativacao.cd_validacao) + " - " + DateTime.Parse(dt.Rows[0]["dt_limite"].ToString()).Date.ToString("dd/MM/yyyy"), "12");
                return new Ativacao_Retorno() { cod_retorno = -7, msg_retorno = "Periodo de ativação expirado", link_ativacao = "" };
            }

            if (int.Parse(dt.Rows[0]["dv_ativo"].ToString()) == 0)
            {
                log_inserir("Solicitacao de ativacao - Codigo desativado " + (string.IsNullOrEmpty(ativacao.cd_validacao) ? "vazio" : ativacao.cd_validacao), "12");
                return new Ativacao_Retorno() { cod_retorno = -8, msg_retorno = "Codigo desativado", link_ativacao = "" };
            }

            dv_ativado = int.Parse(dt.Rows[0]["dv_ativado"].ToString());

            // registra ativação da licença 

            KL_Conexao con = new KL_Conexao();
            bool ativado = true;
            if (dv_ativado == 0)
            {
                // ativação

                SubscriptionResponseContainer container = new SubscriptionResponseContainer();
                container = con.AtivacaoUOL(dt.Rows[0]["cd_validacao"].ToString(), dt.Rows[0]["cd_produto"].ToString(), dt.Rows[0]["id_subscribe"].ToString(), "1", DateTime.Now.Date.AddDays(210));


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

                            log_inserir("Codigo Ativado pela KL " + dt.Rows[0]["cd_validacao"].ToString() + " - " + dt.Rows[0]["id_subscribe"].ToString(), "12");
                        }
                        else if (obj.GetType() == typeof(SubscriptionResponseErrorCollection))
                        {
                            SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                            itemErro = (SubscriptionResponseErrorCollection)obj;


                            foreach (var erro in itemErro.Items)
                            {
                                log_inserir("Solicitacao de ativacao[2] - erro ativacao " + erro.ErrorMessage, "12");
                            }

                            return new Ativacao_Retorno() { cod_retorno = -6, msg_retorno = "Não foi possivel ativar a licença, erro interno", link_ativacao = "" };
                        }
                        else if (obj.GetType() == typeof(TransactionErrorType))
                        {
                            TransactionErrorType itemErro = new TransactionErrorType();

                            itemErro = (TransactionErrorType)obj;

                            log_inserir("Solicitacao de ativacao[3] - erro ativacao " + itemErro.ErrorMessage, "12");

                            return new Ativacao_Retorno() { cod_retorno = -6, msg_retorno = "Não foi possivel ativar a licença, erro interno", link_ativacao = "" };

                        }



                    }
                }
                catch (Exception ex)
                {
                    log_inserir("Solicitacao de ativacao[1] - erro ativacao " + ex.Message, "12");
                    ativado = false;
                }
            }
            // link

            SubscriptionResponseContainer containerlink = new SubscriptionResponseContainer();
            containerlink = con.DownloadLink(PlatformEnum.Android, dt.Rows[0]["id_subscribe"].ToString(), dt.Rows[0]["cd_validacao"].ToString() + DateTime.Now.ToString("yyyyMMddHHmm"));
            string link = "";

            foreach (object obj in containerlink.Items)
            {
                try
                {
                    SubscriptionResponseItemCollection item = new SubscriptionResponseItemCollection();
                    item = (SubscriptionResponseItemCollection)obj;

                    SubscriptionResponseItemCollectionGetDownloadLinks ativo = new SubscriptionResponseItemCollectionGetDownloadLinks();

                    ativo = (SubscriptionResponseItemCollectionGetDownloadLinks)item.Items[0];
                    link = ativo.DownloadLinks[0].Url.Replace("market://", "https://play.google.com/store/apps/");

                }
                catch (Exception ex)
                {
                    log_inserir("Solicitacao de ativacao - erro ativacao " + ex.Message, "12");
                    ativado = false;
                }

                if (!ativado)
                {
                    try
                    {
                        SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                        itemErro = (SubscriptionResponseErrorCollection)obj;

                        foreach (var erro in itemErro.Items)
                        {
                            log_inserir("Solicitacao de ativacao - erro link " + erro.ErrorMessage, "12");
                        }


                    }
                    catch (Exception ex)
                    {
                        log_inserir("Solicitacao de ativacao - erro link " + ex.Message, "12");
                        ativado = false;
                    }

                    return new Ativacao_Retorno() { cod_retorno = -6, msg_retorno = "Não foi possivel ativar a licença, erro interno", link_ativacao = "" };
                }
            }

            if (AtivarCodigo(dt.Rows[0]["id_lote_kl_item"].ToString(), DateTime.Now.Date.AddDays(180).ToString("yyyy-MM-dd"), DateTime.Now.Date.AddDays(210).ToString("yyyy-MM-dd"), @link) == 0)
                return new Ativacao_Retorno() { cod_retorno = 0, msg_retorno = "", link_ativacao = @link, dv_ativado = dv_ativado, chave_ativacao = dt.Rows[0]["cd_ativacao_kl"].ToString() };
            else
                return new Ativacao_Retorno() { cod_retorno = -6, msg_retorno = "Não foi possivel ativar a licença, erro interno", link_ativacao = "" };

        }

        public Ativacao_Retorno ativarKL(Ativacao_KL ativacao)
        {
            log_inserir("Solicitacao de ativacao - token " + (string.IsNullOrEmpty(ativacao.api_token) ? "vazio" : ativacao.api_token) + " - codigo " + (string.IsNullOrEmpty(ativacao.cd_validacao) ? "vazio" : ativacao.cd_validacao), "10");
            // validação
            if (!ValidaToken(ativacao.api_token))
            {
                log_inserir("Solicitacao de ativacao - token invalido " + (string.IsNullOrEmpty(ativacao.api_token) ? "vazio" : ativacao.api_token), "12");
                return new Ativacao_Retorno() { cod_retorno = -2, msg_retorno = "Token invalido" };
            }


            if (string.IsNullOrEmpty(ativacao.cd_validacao))
            {
                log_inserir("Solicitacao de ativacao - codigo " + (string.IsNullOrEmpty(ativacao.cd_validacao) ? "vazio" : ativacao.cd_validacao), "12");
                return new Ativacao_Retorno() { cod_retorno = -3, msg_retorno = "Codigo vazio", link_ativacao = "" };
            }


            Int64 cd_validacao_ = 0;

            if (!Int64.TryParse(ativacao.cd_validacao, out cd_validacao_) || ativacao.cd_validacao.Length != 8)
            {
                log_inserir("Solicitacao de ativacao - Codigo incorreto " + (string.IsNullOrEmpty(ativacao.cd_validacao) ? "vazio" : ativacao.cd_validacao), "12");
                return new Ativacao_Retorno() { cod_retorno = -4, msg_retorno = "Codigo incorreto", link_ativacao = "" };
            }

            int dv_ativado = 0;

            var dt = seleciona_token(ativacao.cd_validacao);

            if (dt.Rows.Count <= 0)
            {
                log_inserir("Solicitacao de ativacao - Codigo não cadastrado " + (string.IsNullOrEmpty(ativacao.cd_validacao) ? "vazio" : ativacao.cd_validacao), "12");
                return new Ativacao_Retorno() { cod_retorno = -5, msg_retorno = "Codigo não cadastrado", link_ativacao = "" };
            }

            if (!string.IsNullOrEmpty(dt.Rows[0]["dt_limite"].ToString()) && DateTime.Parse(dt.Rows[0]["dt_limite"].ToString()).Date < DateTime.Now.Date)
            {
                log_inserir("Solicitacao de ativacao - Periodo de ativação expirado " + (string.IsNullOrEmpty(ativacao.cd_validacao) ? "vazio" : ativacao.cd_validacao) + " - " + DateTime.Parse(dt.Rows[0]["dt_limite"].ToString()).Date.ToString("dd/MM/yyyy"), "12");
                return new Ativacao_Retorno() { cod_retorno = -7, msg_retorno = "Periodo de ativação expirado", link_ativacao = "" };
            }

            if (int.Parse(dt.Rows[0]["dv_ativo"].ToString()) == 0)
            {
                log_inserir("Solicitacao de ativacao - Codigo desativado " + (string.IsNullOrEmpty(ativacao.cd_validacao) ? "vazio" : ativacao.cd_validacao), "12");
                return new Ativacao_Retorno() { cod_retorno = -8, msg_retorno = "Codigo desativado", link_ativacao = "" };
            }

            dv_ativado = int.Parse(dt.Rows[0]["dv_ativado"].ToString());

            // registra ativação da licença 

            KL_Conexao con = new KL_Conexao();
            bool ativado = true;
            if (dv_ativado == 0)
            {
                // ativação

                SubscriptionResponseContainer container = new SubscriptionResponseContainer();
                container = con.AtivacaoLicense(ativacao.cd_validacao, ativacao.id_produto, ativacao.id_subscribe, ativacao.qtd_licenca, DateTime.Now.Date.AddDays(210), ativacao.ignore_user);


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

                            log_inserir("Codigo Ativado pela KL " + dt.Rows[0]["cd_validacao"].ToString() + " - " + dt.Rows[0]["id_subscribe"].ToString(), "12");
                        }
                        else if (obj.GetType() == typeof(SubscriptionResponseErrorCollection))
                        {
                            SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                            itemErro = (SubscriptionResponseErrorCollection)obj;


                            foreach (var erro in itemErro.Items)
                            {
                                log_inserir("Solicitacao de ativacao[2] - erro ativacao " + erro.ErrorMessage, "12");
                            }

                            return new Ativacao_Retorno() { cod_retorno = -6, msg_retorno = "Não foi possivel ativar a licença, erro interno", link_ativacao = "" };
                        }
                        else if (obj.GetType() == typeof(TransactionErrorType))
                        {
                            TransactionErrorType itemErro = new TransactionErrorType();

                            itemErro = (TransactionErrorType)obj;

                            log_inserir("Solicitacao de ativacao[3] - erro ativacao " + itemErro.ErrorMessage, "12");

                            return new Ativacao_Retorno() { cod_retorno = -6, msg_retorno = "Não foi possivel ativar a licença, erro interno", link_ativacao = "" };

                        }



                    }
                }
                catch (Exception ex)
                {
                    log_inserir("Solicitacao de ativacao[1] - erro ativacao " + ex.Message, "12");
                    ativado = false;
                }
            }
            // link

            SubscriptionResponseContainer containerlink = new SubscriptionResponseContainer();
            containerlink = con.DownloadLink(PlatformEnum.Android, dt.Rows[0]["id_subscribe"].ToString(), dt.Rows[0]["cd_validacao"].ToString() + DateTime.Now.ToString("yyyyMMddHHmm"));
            string link = "";

            foreach (object obj in containerlink.Items)
            {
                try
                {
                    SubscriptionResponseItemCollection item = new SubscriptionResponseItemCollection();
                    item = (SubscriptionResponseItemCollection)obj;

                    SubscriptionResponseItemCollectionGetDownloadLinks ativo = new SubscriptionResponseItemCollectionGetDownloadLinks();

                    ativo = (SubscriptionResponseItemCollectionGetDownloadLinks)item.Items[0];
                    link = ativo.DownloadLinks[0].Url.Replace("market://", "https://play.google.com/store/apps/");

                }
                catch (Exception ex)
                {
                    log_inserir("Solicitacao de ativacao - erro ativacao " + ex.Message, "12");
                    ativado = false;
                }

                if (!ativado)
                {
                    try
                    {
                        SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                        itemErro = (SubscriptionResponseErrorCollection)obj;

                        foreach (var erro in itemErro.Items)
                        {
                            log_inserir("Solicitacao de ativacao - erro link " + erro.ErrorMessage, "12");
                        }


                    }
                    catch (Exception ex)
                    {
                        log_inserir("Solicitacao de ativacao - erro link " + ex.Message, "12");
                        ativado = false;
                    }

                    return new Ativacao_Retorno() { cod_retorno = -6, msg_retorno = "Não foi possivel ativar a licença, erro interno", link_ativacao = "" };
                }
            }

            if (AtivarCodigo(dt.Rows[0]["id_lote_kl_item"].ToString(), DateTime.Now.Date.AddDays(180).ToString("yyyy-MM-dd"), DateTime.Now.Date.AddDays(210).ToString("yyyy-MM-dd"), @link) == 0)
                return new Ativacao_Retorno() { cod_retorno = 0, msg_retorno = "", link_ativacao = @link, dv_ativado = dv_ativado, chave_ativacao = dt.Rows[0]["cd_ativacao_kl"].ToString() };
            else
                return new Ativacao_Retorno() { cod_retorno = -6, msg_retorno = "Não foi possivel ativar a licença, erro interno", link_ativacao = "" };

        }

        public DataTable seleciona_token(string cd_validacao)
        {
            DataBase db = new DataBase();
            db.procedure = "p_consulta_codigo_fornecedor";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
            par.Add(db.retorna_parametros("@cd_validacao", cd_validacao));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        #endregion

        #region Log
        public void log_inserir(string nm_log, string id_tipo_log)
        {
            DataBase db = new DataBase();
            db.procedure = "p_insere_log";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@nm_log", nm_log));
            par.Add(db.retorna_parametros("@id_tipo_log", id_tipo_log));
            db.parametros = par;

            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_UOL);
        }

        #endregion
    }
}