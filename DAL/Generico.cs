using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Objetos;
using System.Web.Configuration;
using System.Configuration;

namespace DAL
{
    public class Generico
    {
        public const string paginaErro = "Error.aspx";
        public const string paginaInicial = "Index.aspx";
        public const string paginaProdutos = "HomeSecurity.aspx";
        public const string paginaProdutosSmall = "SmallBusinessSecurity.aspx";  

        public static string formata_data(string data)
        {
            DateTime data_formatada = new DateTime(int.Parse(data.Substring(6, 4)), int.Parse(data.Substring(3, 2)), int.Parse(data.Substring(0, 2)));

            return data_formatada.ToString("yyyy/MM/dd");
        }

        public static void Exec_sem_retorno(Objetos.DataBase Database, string conexao)
        {
            using (SqlConnection conn = new SqlConnection(conexao))
            {
                using (SqlCommand comando = new SqlCommand(Database.procedure, conn))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    if (Database.parametros != null)
                        foreach(Objetos.parametros par in Database.parametros)
                        {
                            if (par.vl_parametro == "")
                                comando.Parameters.AddWithValue(par.nm_parametro, DBNull.Value);
                            else
                                comando.Parameters.AddWithValue(par.nm_parametro, par.vl_parametro);
                        }

                    conn.Open();
                    comando.ExecuteNonQuery();
                    conn.Close();
                }
            }

        }

        public static void gerar_url_amigavel(string url_amigavel, string url_link)
        {
            // Cria uma variável do tipo UrlMapping
            UrlMapping urlMap = null;

            // Abre o Web.config
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");

            // Recupera a seção urlMappings, do web.config
            UrlMappingsSection urlMapSection = (UrlMappingsSection)config.GetSection("system.web/urlMappings");
            // Adiciona a URL Amigável a seção, que é salva no Web.Config
            //urlMap = new UrlMapping("~/Produto", "~/DetalheProduto.aspx?IdProduto=12345");
            urlMap = new UrlMapping(url_amigavel, url_link);

            urlMapSection.UrlMappings.Add(urlMap);

            // Grava no web.config
            config.Save();

            //Response.Redirect("Produto");
            return;
        }

        public static int Exec_linhas_afetadas(Objetos.DataBase Database, string conexao)
        {
            int nr_linhas = 0;
            using (SqlConnection conn = new SqlConnection(conexao))
            {
                using (SqlCommand comando = new SqlCommand(Database.procedure, conn))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    if (Database.parametros != null)
                        foreach (Objetos.parametros par in Database.parametros)
                        {
                            if (par.vl_parametro == "")
                                comando.Parameters.AddWithValue(par.nm_parametro, DBNull.Value);
                            else
                                comando.Parameters.AddWithValue(par.nm_parametro, par.vl_parametro);
                        }

                    conn.Open();
                    nr_linhas = int.Parse(comando.ExecuteScalar().ToString());
                    conn.Close();
                }
            }

            return nr_linhas;
        }

        public static string Exec_retorno_string(Objetos.DataBase Database, string conexao)
        {
            string nr_linhas = "";
            using (SqlConnection conn = new SqlConnection(conexao))
            {
                using (SqlCommand comando = new SqlCommand(Database.procedure, conn))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    if (Database.parametros != null)
                        foreach (Objetos.parametros par in Database.parametros)
                        {
                            if (par.vl_parametro == "")
                                comando.Parameters.AddWithValue(par.nm_parametro, DBNull.Value);
                            else
                                comando.Parameters.AddWithValue(par.nm_parametro, par.vl_parametro);
                        }

                    conn.Open();
                    nr_linhas = comando.ExecuteScalar().ToString();
                    conn.Close();
                }
            }

            return nr_linhas;
        }

        public static DataTable Exec_tabela(Objetos.DataBase Database, string conexao)
        {
            DataTable Dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(conexao))
            {
                using (SqlCommand comando = new SqlCommand(Database.procedure, conn))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    if (Database.parametros != null)
                        foreach (Objetos.parametros par in Database.parametros)
                        {
                            if (par.vl_parametro == "")
                                comando.Parameters.AddWithValue(par.nm_parametro, DBNull.Value);
                            else
                                comando.Parameters.AddWithValue(par.nm_parametro, par.vl_parametro);
                        }

                    SqlDataAdapter Dp = new SqlDataAdapter(comando);
                    Dp.Fill(Dt);
                }
            }

            return Dt;
        }

        public static void log_inserir(int id_tp_log, string erro, int id_usuario )
        {
            DataBase db = new DataBase();
            db.procedure = "p_log_inserir";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_tp_log", id_tp_log.ToString()));
            par.Add(db.retorna_parametros("@erro", erro));
            par.Add(db.retorna_parametros("@id_usuario_log", id_usuario.ToString()));

            db.parametros = par;

            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_AV);
        }
    }
}
