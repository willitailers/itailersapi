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
    public class Tipo_pagamento_BLL
    {
        public DataTable tipo_pagamento_selecionar(Tipo_pagamento tp)
        {
            DataTable Dt = new DataTable();
            Tipo_pagamento_DAL dal = new Tipo_pagamento_DAL();

            tp.id_tp_pagamento = 0;

            Dt = dal.tipo_pagamento_selecionar(tp);

            return Dt;
        }

        public void tipo_pagamento_inserir(Tipo_pagamento tp)
        {
            Tipo_pagamento_DAL dal = new Tipo_pagamento_DAL();
            dal.tipo_pagamento_incluir(tp);

            return;
        }
    }
}
