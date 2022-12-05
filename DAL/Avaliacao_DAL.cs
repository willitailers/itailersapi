using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Objetos;

namespace DAL
{
    public class Avaliacao_DAL
    {
        public DataTable avaliacao_selecionar(Avaliacao avaliacao)
        {
            DataBase db = new DataBase();
            db.procedure = "p_avaliacao";

            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@tp_acao", avaliacao.tp_acao.ToString()));
            par.Add(db.retorna_parametros("@id_avalicao", avaliacao.id_avaliacao.ToString()));
            par.Add(db.retorna_parametros("@nm_pagina", avaliacao.nm_pagina));
            par.Add(db.retorna_parametros("@nm_titulo", avaliacao.nm_titulo));
            par.Add(db.retorna_parametros("@nm_avaliacao", avaliacao.nm_avaliacao));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable melhor_produto_selecionar(Avaliacao avaliacao)
        {
            DataBase db = new DataBase();
            db.procedure = "p_melhor_produto";

            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@tp_acao", avaliacao.tp_acao.ToString()));
            par.Add(db.retorna_parametros("@id_melhor_produto", avaliacao.id_melhor_produto.ToString()));
            par.Add(db.retorna_parametros("@nm_pagina", avaliacao.nm_pagina));
            par.Add(db.retorna_parametros("@nm_titulo_melhor_produto", avaliacao.nm_titulo_melhor_produto));
            par.Add(db.retorna_parametros("@nm_melhor_produto", avaliacao.nm_melhor_produto));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }
    }
}
