using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objetos;
using DAL;
using System.Data;

namespace BLL
{
    public class Banco_BLL
    {
        public DataTable banco_selecionar(Banco banco)
        {
            DataTable Dt = new DataTable();
            Banco_DAL dal = new Banco_DAL();

            banco.id_banco = 0;

            Dt = dal.banco_selecionar(banco);

            return Dt;
        }

        public void banco_inserir(Banco banco)
        {
            Banco_DAL dal = new Banco_DAL();
            dal.banco_incluir(banco);

            return;
        }
    }
}
