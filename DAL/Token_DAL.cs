using Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Token_DAL
    {
        public DataTable seleciona_token()
        {
            DataBase db = new DataBase();
            db.procedure = "p_consulta_token";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
          
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }
    }
}
