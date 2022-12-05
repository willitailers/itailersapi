using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objetos;
using DAL;
using System.Data;

namespace DAL
{
    public class Pagamentos_DAL
    {
        public DataTable pagamento_consulta(DateTime dt_inicial, DateTime dt_final, int id_tp_consulta, string nm_email)
        {
            DataBase db = new DataBase();
            db.procedure = "p_pagamento_consulta";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@nm_email", nm_email.ToString()));
            par.Add(db.retorna_parametros("@id_tp_consulta", id_tp_consulta.ToString()));
            par.Add(db.retorna_parametros("@dt_incial", dt_inicial.ToString("yyyyMMdd")));
            par.Add(db.retorna_parametros("@dt_final", dt_final.ToString("yyyyMMdd HH:mm")));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable pagamento_consulta_id(string id_compra)
        {
            DataBase db = new DataBase();
            db.procedure = "p_pagamento_consulta_id";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_compra", id_compra.ToString()));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable relatorio_venda_token(DateTime dt_inicial, DateTime dt_final, int cd_status)
        {
            DataBase db = new DataBase();
            db.procedure = "p_relatorio_venda_token";
            
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@dt_inicial", dt_inicial.ToString("yyyy-MM-dd HH:mm")));
            par.Add(db.retorna_parametros("@dt_final", dt_final.ToString("yyyy-MM-dd HH:mm")));
            par.Add(db.retorna_parametros("@cd_status", cd_status.ToString()));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }
    }
}
