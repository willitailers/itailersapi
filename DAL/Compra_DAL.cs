using Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Compra_DAL
    {
        public DataTable licencas_consulta(DateTime dt_inicial, DateTime dt_final, int id_produto, int id_produto_sub, string nm_email, string ic_recusado)
        {
            DataBase db = new DataBase();
            db.procedure = "p_licenca_consulta";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@nm_email", nm_email.ToString()));
            par.Add(db.retorna_parametros("@dt_incial", dt_inicial.ToString("yyyyMMdd")));
            par.Add(db.retorna_parametros("@dt_final", dt_final.ToString("yyyyMMdd HH:mm")));
            par.Add(db.retorna_parametros("@id_produto", id_produto.ToString()));
            par.Add(db.retorna_parametros("@id_produto_sub", id_produto_sub.ToString()));
            par.Add(db.retorna_parametros("@ic_recusado", ic_recusado.ToString()));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public void wifi_confirma(string id_compra)
        {
            DataBase db = new DataBase();
            db.procedure = "p_compra_confirma_wifi";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_compra", id_compra));
            db.parametros = par;

            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_AV);
        }
    }
}
