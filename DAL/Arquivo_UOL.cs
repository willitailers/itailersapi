using Objetos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Arquivo_UOL
    {
        //string conn = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        public int insere_lote_fornecedor(string nm_arquivo, string nr_item_lote_fornecedor)
        {
            try
            {
                DataBase db = new DataBase();
                db.procedure = "p_insere_lote_fornecedor";

                parametros p = new parametros();
                List<parametros> par = new List<parametros>();

                par.Add(db.retorna_parametros("@nm_arquivo", nm_arquivo));
                par.Add(db.retorna_parametros("@nr_item_lote_fornecedor", nr_item_lote_fornecedor));
                db.parametros = par;

                return Generico.Exec_linhas_afetadas(db, DAL.Constantes_DAL.Conexao_UOL);
            }
            catch(Exception ex)
            {
                log_inserir("Erro ao cadastrar lote fornecedor - " + ex.Message, "-1");
                return -10;
            }
            
        }

        public int insere_lote_fornecedor_item(string id_lote_fornecedor, string cd_validacao, string nr_ordem)
        {
            try
            {
                DataBase db = new DataBase();
                db.procedure = "p_insere_lote_fornecedor_item";

                parametros p = new parametros();
                List<parametros> par = new List<parametros>();

                par.Add(db.retorna_parametros("@id_lote_fornecedor", id_lote_fornecedor));
                par.Add(db.retorna_parametros("@cd_validacao", cd_validacao));
                par.Add(db.retorna_parametros("@nr_ordem", nr_ordem));
                db.parametros = par;

                return Generico.Exec_linhas_afetadas(db, DAL.Constantes_DAL.Conexao_UOL);
            }
            catch (Exception ex)
            {
                log_inserir("Erro ao cadastrar item de lote fornecedor - " + ex.Message, "-1");
                return -10;
            }
           
        }

        public int insere_lote_kl(string nr_item_lote_kl, string nm_arquivo, string id_lote_fornecedor)
        {
            try
            {
                DataBase db = new DataBase();
                db.procedure = "p_insere_lote_kl";

                parametros p = new parametros();
                List<parametros> par = new List<parametros>();

                par.Add(db.retorna_parametros("@nr_item_lote_kl", nr_item_lote_kl));
                par.Add(db.retorna_parametros("@nm_arquivo", nm_arquivo));
                par.Add(db.retorna_parametros("@id_lote_fornecedor", id_lote_fornecedor));
                db.parametros = par;

                return Generico.Exec_linhas_afetadas(db, DAL.Constantes_DAL.Conexao_UOL);
            }
            catch (Exception ex)
            {
                log_inserir("Erro ao cadastrar lote KL - " + ex.Message, "-1");
                return -1;
            }
            
        }

        public int insere_lote_kl_item(string id_lote_kl, string id_lote_fornecedor, string id_subscribe, string cd_produto,
                                        string qtd_licenca, string activation_type, string nm_linguagem, string id_plataforma, string nr_ordem, string cd_ativacao_kl)
        {
            try
            {
                DataBase db = new DataBase();
                db.procedure = "p_insere_lote_kl_item";

                parametros p = new parametros();
                List<parametros> par = new List<parametros>();

                par.Add(db.retorna_parametros("@id_lote_kl", id_lote_kl));
                par.Add(db.retorna_parametros("@id_lote_fornecedor", id_lote_fornecedor));
                par.Add(db.retorna_parametros("@id_subscribe", id_subscribe));
                par.Add(db.retorna_parametros("@cd_produto", cd_produto));
                par.Add(db.retorna_parametros("@qtd_licenca", qtd_licenca));
                par.Add(db.retorna_parametros("@activation_type", activation_type));
                par.Add(db.retorna_parametros("@nm_linguagem", nm_linguagem));
                par.Add(db.retorna_parametros("@id_plataforma", id_plataforma));
                par.Add(db.retorna_parametros("@cd_ativacao_kl", cd_ativacao_kl));
                par.Add(db.retorna_parametros("@nr_ordem", nr_ordem));
                db.parametros = par;

                return Generico.Exec_linhas_afetadas(db, DAL.Constantes_DAL.Conexao_UOL);
            }
            catch (Exception ex)
            {
                log_inserir("Erro ao cadastrar item lote KL - " + ex.Message, "-1");
                return -1;
            }
            
        }

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

        public void lote_deletar(string id_lote_fornecedor)
        {
            DataBase db = new DataBase();
            db.procedure = "p_deletar_lote";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_lote_fornecedor", id_lote_fornecedor));
            db.parametros = par;

            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_UOL);
        }

        public DataTable busca_licenca_cancelamento()
        {
            DataBase db = new DataBase();
            db.procedure = "p_licencas_vencimento";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_UOL);
        }

        public void Licenca_cancelada(string id_lote_kl_item)
        {
            DataBase db = new DataBase();
            db.procedure = "p_licenca_cancelada";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_lote_kl_item", id_lote_kl_item));
            db.parametros = par;

            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_UOL);
        }
    }
}
