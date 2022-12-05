using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Objetos;

namespace DAL
{
    public class Parceiro_DAL
    {
        public DataTable parceiro_token_selecionar(Parceiro parca)
        {
            DataBase db = new DataBase();
            db.procedure = "p_parceiro_token";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_parceiro_token", parca.id_parceiro_token.ToString()));
            par.Add(db.retorna_parametros("@id_parceiro", parca.id_parceiro.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable parceiro_selecionar(string id_parceiro)
        {
            DataBase db = new DataBase();
            db.procedure = "p_parceiro";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
            
            par.Add(db.retorna_parametros("@id_parceiro", id_parceiro.ToString()));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public string parceiro_token_incluir(Parceiro parca)
        {
            DataBase db = new DataBase();
            db.procedure = "p_parceiro_token_incluir";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_parceiro", parca.id_parceiro.ToString()));
            par.Add(db.retorna_parametros("@nm_link", parca.nm_link.ToString()));
            par.Add(db.retorna_parametros("@url_amigavel", parca.url_amigavel.ToString()));
            par.Add(db.retorna_parametros("@url_acesso", parca.url_acesso.ToString()));
            par.Add(db.retorna_parametros("@nr_token", parca.nr_token.ToString()));
            db.parametros = par;

            return Generico.Exec_retorno_string(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public string token_gerar()
        {
            DataBase db = new DataBase();
            db.procedure = "p_token_gerar";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
          
            db.parametros = par;

            return Generico.Exec_retorno_string(db, DAL.Constantes_DAL.Conexao_AV);
        }
    }
}
