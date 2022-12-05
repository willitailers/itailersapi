using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Compra_BLL
    {
        public DataTable licencas_consulta(DateTime dt_inicial, DateTime dt_final, int id_produto, int id_produto_sub, string nm_email, string ic_recusado)
        {
            DataTable Dt = new DataTable();
            Compra_DAL dal = new Compra_DAL();

            Dt = dal.licencas_consulta(dt_inicial, dt_final, id_produto, id_produto_sub, nm_email, ic_recusado);

            return Dt;
        }

        public void wifi_confirma(string id_compra)
        {
            new Compra_DAL().wifi_confirma(id_compra);
        }
    }
}
