using DAL;
using KL_API.Models.Guardiao;
using KL_API.Models.Kiddlepass;
using Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KL_API.Models.Kiddlepass
{
    public class KiddlepassDAL
    {
        public DataTable InsertUser(KiddlepassUsuario kiddlepassUsuario)
        {
            DataBase db = new DataBase();
            db.procedure = "p_kiddlepass_insere_usuario";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("userid", kiddlepassUsuario.userid),
                db.retorna_parametros("@id_cliente", kiddlepassUsuario.id_cliente.ToString()),
                db.retorna_parametros("@product_type", kiddlepassUsuario.product_type),
                db.retorna_parametros("@status", kiddlepassUsuario.status),
                db.retorna_parametros("@email", kiddlepassUsuario.email),
                db.retorna_parametros("@name", kiddlepassUsuario.name),
            };

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable UpdateUser(KiddlepassUsuario kiddlepassUsuario)
        {
            DataBase db = new DataBase();
            db.procedure = "p_kiddlepass_atualiza_usuario";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("userid", kiddlepassUsuario.userid),
                db.retorna_parametros("@id_cliente", kiddlepassUsuario.id_cliente.ToString()),
                db.retorna_parametros("@status", kiddlepassUsuario.status),
            };

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public KiddlepassUsuario GetUsuario(KiddlepassUsuario kiddlepassUsuario)
        {
            DataBase db = new DataBase();
            db.procedure = "p_kiddlepass_get_usuario";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("id_cliente", kiddlepassUsuario.id_cliente.ToString()),
                db.retorna_parametros("userid", kiddlepassUsuario.userid),
            };

            db.parametros = par;

            DataTable dt = Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);

            if (dt.Rows.Count > 0)
            {
                kiddlepassUsuario.userid = dt.Rows[0]["userid"].ToString();
                kiddlepassUsuario.email = dt.Rows[0]["email"].ToString();
                kiddlepassUsuario.status = dt.Rows[0]["status"].ToString();
                kiddlepassUsuario.name = dt.Rows[0]["name"].ToString();

                return kiddlepassUsuario;
            }
            else
                return null;
        }

        public List<UserKiddlepassResponse> GetAllUsuario(KiddlepassUsuario kiddlepassUsuario)
        {
            DataBase db = new DataBase();
            db.procedure = "p_kiddlepass_get_all_usuarios";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("id_cliente", kiddlepassUsuario.id_cliente.ToString()),
            };

            db.parametros = par;

            DataTable dt = Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);

            var kiddlepass_usuarios = new List<UserKiddlepassResponse>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var userKiddlepassResponse = new UserKiddlepassResponse
                    {
                        email = row["email"].ToString(),
                        name = row["name"].ToString(),
                        status = row["status"].ToString(),
                        userid = row["userid"].ToString(),
                    };

                    kiddlepass_usuarios.Add(userKiddlepassResponse);
                }

                return kiddlepass_usuarios;
            }
            else
                return null;
        }

        #region token
        public KiddlepassCliente ValidaToken(string token)
        {
            DataBase db = new DataBase();
            db.procedure = "p_kiddlepass_get_cliente_por_token";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@kiddlepass_token_acesso", token)
            };

            db.parametros = par;
            var dt = Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);

            if (dt.Rows.Count == 0)
                return new KiddlepassCliente() { valido = false };
            else
            {
                DateTime? dataCriacao = dt.Rows[0]["kiddlepass_data_criacao"] == DBNull.Value
                ? (DateTime?)null
                : Convert.ToDateTime(dt.Rows[0]["kiddlepass_data_criacao"]);

                return new KiddlepassCliente()
                {
                    valido = true,
                    kiddlepass_id_cliente = int.Parse(dt.Rows[0]["kiddlepass_id_cliente"].ToString()),
                    kiddlepass_nome = dt.Rows[0]["kiddlepass_nome"].ToString(),
                    kiddlepass_token_acesso = dt.Rows[0]["kiddlepass_token_acesso"].ToString(),
                    kiddlepass_data_criacao = dataCriacao.Value
                };
            }
        }

        #endregion

        #region Log
        public void log_inserir(string log, string json)
        {
            DataBase db = new DataBase();
            db.procedure = "p_kiddlepass_insere_log";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@kiddlepass_log", log),
                db.retorna_parametros("@kiddlepass_json", json)
            };
            db.parametros = par;

            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_API);
        }

        #endregion
    }
}