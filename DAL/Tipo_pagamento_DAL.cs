using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objetos;

namespace DAL
{
    public class Tipo_pagamento_DAL
    {
        public DataTable tipo_pagamento_selecionar(Tipo_pagamento tp)
        {
            DataBase db = new DataBase();
            db.procedure = "p_tp_pagamento";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_tp_pagamento", tp.id_tp_pagamento.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public void tipo_pagamento_incluir(Tipo_pagamento tp)
        {
            DataBase db = new DataBase();
            db.procedure = "p_tp_pagamento_incluir";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@nm_tp_pagamento", tp.nm_tp_pagamento.ToString()));
            par.Add(db.retorna_parametros("@dv_ativo", tp.dv_ativo.ToString()));
            db.parametros = par;

            Generico.Exec_sem_retorno(db, DAL.Constantes_DAL.Conexao_AV);
        }
    }
}
