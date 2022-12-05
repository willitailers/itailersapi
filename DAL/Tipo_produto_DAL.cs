using Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Tipo_produto_DAL
    {
        public DataTable tipo_produto_selecionar(Tipo_produto tp)
        {
            DataBase db = new DataBase();
            db.procedure = "p_tp_produto";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_tp_produto", tp.id_tp_produto.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable tipo_pproduto_incluir(Tipo_produto tp)
        {
            DataBase db = new DataBase();
            db.procedure = "p_tp_produto_incluir";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@nm_tp_produto", tp.nm_tp_produto.ToString()));
            par.Add(db.retorna_parametros("@dv_assinatura", tp.dv_assinatura.ToString()));
            par.Add(db.retorna_parametros("@dv_venda", tp.dv_venda.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }
    }
}
