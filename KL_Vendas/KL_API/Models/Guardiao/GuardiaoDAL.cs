using DAL;
using KL_API.Models.Guardiao;
using Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KL_API.Models.GuardiaoDAL
{
    public class GuardiaoDAL
    {
        public DataTable InsereLicenca(int guardiao_id_usuario, string guardiao_status)
        {
            DataBase db = new DataBase();
            db.procedure = "p_guardiao_insere_licenca";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("guardiao_id_usuario", guardiao_id_usuario.ToString()),
                db.retorna_parametros("guardiao_status", guardiao_status),
            };

            db.parametros = par;
            
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public GuardiaoUsuario InsereUsuario(GuardiaoUsuario guardiaoUsuario)
        {
            DataBase db = new DataBase();
            db.procedure = "p_guardiao_insere_usuario";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("guardiao_id_cliente", guardiaoUsuario.guardiao_id_cliente.ToString()),
                db.retorna_parametros("guardiao_nome", guardiaoUsuario.guardiao_nome),
                db.retorna_parametros("guardiao_email", guardiaoUsuario.guardiao_email),
                db.retorna_parametros("guardiao_status", guardiaoUsuario.guardiao_status),
            };

            db.parametros = par;

            DataTable dt = Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);

            if (dt.Rows.Count > 0)
            {
                guardiaoUsuario.guardiao_id_usuario = Convert.ToInt32(dt.Rows[0][0]);
                return guardiaoUsuario;
            }
            else
                return null;
        }

        public GuardiaoUsuario AtualizaUsuario(GuardiaoUsuario guardiaoUsuario)
        {
            DataBase db = new DataBase();
            db.procedure = "p_guardiao_atualiza_usuario";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("guardiao_id_cliente", guardiaoUsuario.guardiao_id_cliente.ToString()),
                db.retorna_parametros("guardiao_email", guardiaoUsuario.guardiao_email),
                db.retorna_parametros("guardiao_status", guardiaoUsuario.guardiao_status),
            };

            db.parametros = par;

            DataTable dt = Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);

            if (dt.Rows.Count > 0)
            {
                return guardiaoUsuario;
            }
            else
                return null;
        }

        public GuardiaoUsuario GetUsuario(GuardiaoUsuario guardiaoUsuario)
        {
            DataBase db = new DataBase();
            db.procedure = "p_guardiao_get_usuario";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("guardiao_id_cliente", guardiaoUsuario.guardiao_id_cliente.ToString()),
                db.retorna_parametros("guardiao_email", guardiaoUsuario.guardiao_email),
            };

            db.parametros = par;

            DataTable dt = Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);

            if (dt.Rows.Count > 0)
            {
                guardiaoUsuario.guardiao_id_usuario = (int)dt.Rows[0]["guardiao_id_usuario"];
                guardiaoUsuario.guardiao_nome = dt.Rows[0]["guardiao_nome"].ToString();
                guardiaoUsuario.guardiao_email = dt.Rows[0]["guardiao_email"].ToString();
                guardiaoUsuario.guardiao_status = dt.Rows[0]["guardiao_status"].ToString();
                guardiaoUsuario.guardiao_data_atualizacao = Convert.ToDateTime(dt.Rows[0]["guardiao_data_atualizacao"]);
                guardiaoUsuario.guardiao_data_criacao = Convert.ToDateTime(dt.Rows[0]["guardiao_data_criacao"]);

                return guardiaoUsuario;
            }
            else
                return null;
        }

        public DataTable GetUsuariosPorCliente(int guardiao_id_cliente)
        {
            DataBase db = new DataBase();
            db.procedure = "p_guardiao_get_usuarios_por_cliente";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("guardiao_id_cliente", guardiao_id_cliente.ToString()),
            };

            db.parametros = par;

            DataTable dt = Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);

            return dt;
        }

        #region token
        public GuardiaoClientes ValidaToken(string token)
        {
            DataBase db = new DataBase();
            db.procedure = "p_guardiao_get_cliente_por_token";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@guardiao_token_acesso", token)
            };

            db.parametros = par;
            var dt = Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);

            if (dt.Rows.Count == 0)
                return new GuardiaoClientes() { valido = false };
            else
            {
                DateTime? dataCriacao = dt.Rows[0]["guardiao_data_criacao"] == DBNull.Value
                ? (DateTime?)null
                : Convert.ToDateTime(dt.Rows[0]["guardiao_data_criacao"]);

                return new GuardiaoClientes()
                {
                    valido = true,
                    guardiao_id_cliente = int.Parse(dt.Rows[0]["guardiao_id_cliente"].ToString()),
                    guardiao_nome = dt.Rows[0]["guardiao_nome"].ToString(),
                    guardiao_token_acesso = dt.Rows[0]["guardiao_token_acesso"].ToString(),
                    guardiao_data_criacao = dataCriacao.Value
                };
            }
        }

        #endregion

        #region Log
        public void log_inserir(string log, string json)
        {
            DataBase db = new DataBase();
            db.procedure = "p_guardiao_insere_log";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@guardiao_log", log),
                db.retorna_parametros("@guardiao_json", json)
            };
            db.parametros = par;

            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_API);
        }

        #endregion
    }
}