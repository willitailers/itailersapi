using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objetos;
using System.Data;

namespace DAL
{
    public class Usuario_DAL
    {
        public Usuario getlogin(Login login)
        {
            Usuario usuario = new Usuario();
            DataBase db = new DataBase();
            DataTable Dt = new DataTable();

            db.procedure = "p_usuario_login";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@nm_email", login.user));
            par.Add(db.retorna_parametros("@nm_senha", login.pass));

            db.parametros = par;

            Dt = Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);

            if (Dt.Rows.Count > 0)
            {
                usuario.nm_email = Dt.Rows[0]["nm_email"].ToString();
                usuario.nm_usuario_primeiro = Dt.Rows[0]["nm_usuario_primeiro"].ToString();
                usuario.nm_usuario_segundo = Dt.Rows[0]["nm_usuario_segundo"].ToString();
                usuario.dv_ativo = Dt.Rows[0]["dv_ativo"].ToString() == "True" ? 1 : 0;
                usuario.id_usuario_nivel = int.Parse(Dt.Rows[0]["id_usuario_nivel"].ToString());
                usuario.id_usuario = int.Parse(Dt.Rows[0]["id_usuario"].ToString());

                usuario.dv_logado = true;
            }
            else
            {
                usuario.nm_email = login.user;
                usuario.dv_logado = false;
            }


            return usuario;
        }

        public DataTable monta_menu(int id_usuario)
        {
            DataTable dt = new DataTable();

            DataBase db = new DataBase();
            db.procedure = "p_monta_menu";

            db.parametros = new List<parametros>();
            db.parametros.Add(db.retorna_parametros("@id_usuario", id_usuario.ToString()));
            dt = Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);

            return dt;
        }

        public void RegistrarAcesso(string token)
        {
            DataBase db = new DataBase();
            db.procedure = "p_registrar_acesso";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@nr_token", token));

            db.parametros = par;

            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public int usuario_inserir(Usuario user)
        {
            DataBase db = new DataBase();
            db.procedure = "p_usuario_inserir";

            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@nm_email", user.nm_email));
            par.Add(db.retorna_parametros("@nm_senha", user.nm_senha));
            par.Add(db.retorna_parametros("@nm_usuario_primeiro", user.nm_usuario_primeiro));
            par.Add(db.retorna_parametros("@nm_usuario_segundo", user.nm_usuario_segundo));
            par.Add(db.retorna_parametros("@id_usuario_nivel", user.id_usuario_nivel.ToString()));
            par.Add(db.retorna_parametros("@dv_ativo", "1"));

            db.parametros = par;

            return int.Parse(Generico.Exec_retorno_string(db, DAL.Constantes_DAL.Conexao_AV));
        }

        public DataTable usuario_consulta(Usuario user)
        {
            DataBase db = new DataBase();
            db.procedure = "p_usuario_consulta";

            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@nm_email", user.nm_email));
            par.Add(db.retorna_parametros("@id_usuario_nivel", user.id_usuario_nivel.ToString()));
            par.Add(db.retorna_parametros("@dv_ativo", user.dv_ativo.ToString()));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable usuario_nivel_consulta(int id_usuario_nivel)
        {
            DataBase db = new DataBase();
            db.procedure = "p_usuario_nivel_consulta";

            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_usuario_nivel", id_usuario_nivel.ToString()));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }
    }
}
