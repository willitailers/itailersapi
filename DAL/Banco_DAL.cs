using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objetos;
using System.Data;

namespace DAL
{
    public class Banco_DAL
    {
        public DataTable banco_selecionar(Banco banco)
        {
            DataBase db = new DataBase();
            db.procedure = "p_banco";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_banco", banco.id_banco.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable banco_incluir(Banco banco)
        {
            DataBase db = new DataBase();
            db.procedure = "p_banco_incluir";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_banco", banco.id_banco.ToString()));
            par.Add(db.retorna_parametros("@nm_banco", banco.nm_banco.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }
    }
}
