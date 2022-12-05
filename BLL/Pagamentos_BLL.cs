using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Pagamentos_BLL
    {
        public DataTable pagamento_consulta(DateTime dt_inicial, DateTime dt_final, int id_tp_consulta, string nm_email)
        {
            DataTable Dt = new DataTable();
            Pagamentos_DAL dal = new Pagamentos_DAL();

            Dt = dal.pagamento_consulta(dt_inicial, dt_final, id_tp_consulta, nm_email);

            return Dt;
        }

        public DataTable pagamento_consulta_id(string id_compra)
        {
            DataTable Dt = new DataTable();
            Pagamentos_DAL dal = new Pagamentos_DAL();

            Dt = dal.pagamento_consulta_id(id_compra);

            return Dt;
        }

        public DataTable relatorio_venda_token(DateTime dt_inicial, DateTime dt_final, int cd_status)
        {
            return new Pagamentos_DAL().relatorio_venda_token(dt_inicial, dt_final, cd_status);
        }
    }
}
