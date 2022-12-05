using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objetos;
using DAL;

namespace BLL
{
    public class Tipo_produto_BLL
    {
        public DataTable tipo_pagamento_selecionar(Tipo_produto tp)
        {
            DataTable Dt = new DataTable();
            Tipo_produto_DAL dal = new Tipo_produto_DAL();

            tp.id_tp_produto = 0;

            Dt = dal.tipo_produto_selecionar(tp);

            return Dt;
        }

        public void tipo_pagamento_inserir(Tipo_produto tp)
        {
            Tipo_produto_DAL dal = new Tipo_produto_DAL();
            dal.tipo_pproduto_incluir(tp);

            return;
        }
    }
}
