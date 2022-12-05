using Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HomeSecurity_DAL
    {
        public DataTable carregar_produtos(string id_tipo_produto, string id_produto)
        {
            DataBase db = new DataBase();
            db.procedure = "p_security_produtos";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_tipo_produto", id_tipo_produto));
            par.Add(db.retorna_parametros("@id_produto", id_produto));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable carregar_sub_produtos(string id_produto, string id_produto_sub, string nr_token)
        {
            DataBase db = new DataBase();
            db.procedure = "p_security_sub_produtos";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_produto", id_produto.ToString()));
            par.Add(db.retorna_parametros("@id_produto_sub", id_produto_sub.ToString()));
            par.Add(db.retorna_parametros("@nr_token", nr_token.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }
    }
}
