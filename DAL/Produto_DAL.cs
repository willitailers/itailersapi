using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objetos;
using System.Data;

namespace DAL
{
    public class Produto_DAL
    {
        public DataTable produto_selecionar(Produto produto)
        {
            DataBase db = new DataBase();
            db.procedure = "p_produto";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_produto", produto.id_produto.ToString()));
            par.Add(db.retorna_parametros("@id_fabricante", produto.id_fabricante.ToString()));
            par.Add(db.retorna_parametros("@nm_produto", produto.nm_produto.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable produto_incluir(Produto produto)
        {
            DataBase db = new DataBase();
            db.procedure = "p_produto_incluir";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_produto", produto.id_produto.ToString()));
            par.Add(db.retorna_parametros("@id_fabricante", produto.id_fabricante.ToString()));
            par.Add(db.retorna_parametros("@nm_produto", produto.nm_produto.ToString()));
            par.Add(db.retorna_parametros("@nm_produto_descr_curta", produto.nm_produto_descr_curta.ToString()));
            par.Add(db.retorna_parametros("@nm_produto_descr_completa", produto.nm_produto_descr_completa.ToString()));
            par.Add(db.retorna_parametros("@nm_produto_descr_completa2", produto.nm_produto_descr_completa2.ToString()));
            par.Add(db.retorna_parametros("@link_downaload", produto.link_downaload.ToString()));
            par.Add(db.retorna_parametros("@dt_criacao", produto.dt_criacao.ToString("yyyy/MM/hh HH:mm")));
            par.Add(db.retorna_parametros("@id_usuario_criacao", produto.id_usuario_criacao.ToString()));
            par.Add(db.retorna_parametros("@dt_alteracao", produto.dt_alteracao.ToString("yyyy/MM/hh HH:mm")));
            par.Add(db.retorna_parametros("@id_usuario_alteracao", produto.id_usuario_alteracao.ToString()));
            par.Add(db.retorna_parametros("@dv_ativo", produto.dv_ativo.ToString()));
            par.Add(db.retorna_parametros("@cd_produto", produto.cd_produto.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable produto_sub_selecionar(Produto_Sub produto)
        {
            DataBase db = new DataBase();
            db.procedure = "p_produto_sub";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_produto", produto.id_produto.ToString()));
            par.Add(db.retorna_parametros("@id_produto_sub", produto.id_produto_sub.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable produto_sub_especial_consulta(Produto_Sub produto)
        {
            DataBase db = new DataBase();
            db.procedure = "p_produto_sub_especial_consulta";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_produto_sub", produto.id_produto_sub.ToString()));
            par.Add(db.retorna_parametros("@id_parceiro_token", produto.id_parceiro_token.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }
        
        public DataTable produto_sub_especial_token(int id_parceiro_token)
        {
            DataBase db = new DataBase();
            db.procedure = "p_produto_sub_especial_token";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_parceiro_token", id_parceiro_token.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable produto_sub_todos()
        {
            DataBase db = new DataBase();
            db.procedure = "p_seleciona_produto_sub_todos";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public string produto_sub_especial_inserir(Produto_Sub produto)
        {
            DataBase db = new DataBase();
            db.procedure = "p_produto_sub_especial_inserir";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_produto_sub", produto.id_produto_sub.ToString()));
            par.Add(db.retorna_parametros("@vl_produto_sub_especial", produto.vl_produto_sub.ToString().Replace(",",".")));
            par.Add(db.retorna_parametros("@nr_trial", produto.nr_trial.ToString()));
            par.Add(db.retorna_parametros("@id_parceiro", produto.parceiro.id_parceiro.ToString()));
            par.Add(db.retorna_parametros("@nm_link", produto.parceiro.nm_link.ToString()));
            par.Add(db.retorna_parametros("@url_amigavel", produto.parceiro.url_amigavel.ToString()));
            par.Add(db.retorna_parametros("@url_acesso", produto.parceiro.url_acesso.ToString()));
            par.Add(db.retorna_parametros("@nr_token", produto.parceiro.nr_token.ToString()));
            par.Add(db.retorna_parametros("@id_parceiro_token", produto.parceiro.id_parceiro_token.ToString()));
            par.Add(db.retorna_parametros("@ic_produto_vinculado", produto.dv_vinculo.ToString()));
            par.Add(db.retorna_parametros("@id_produto_sub_vinculado", produto.id_produto_sub_vinculo.ToString()));
            par.Add(db.retorna_parametros("@vl_produto_sub_titular", produto.vl_titular.ToString().Replace(",", ".")));
            par.Add(db.retorna_parametros("@vl_produto_sub_vinculado", produto.vl_vinculado.ToString().Replace(",", ".")));
            db.parametros = par;

            return Generico.Exec_retorno_string(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable produto_sub_incluir(Produto_Sub produto)
        {
            DataBase db = new DataBase();
            db.procedure = "p_produto_sub_incluir";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_produto_sub", produto.id_produto_sub.ToString()));
            par.Add(db.retorna_parametros("@id_produto", produto.id_produto.ToString()));
            par.Add(db.retorna_parametros("@nm_produto_sub", produto.nm_produto_sub.ToString()));
            par.Add(db.retorna_parametros("@nm_produto_sub_descr_curta", produto.nm_produto_sub_descr_curta.ToString()));
            par.Add(db.retorna_parametros("@nm_produto_sub_descr_completa", produto.nm_produto_sub_descr_completa.ToString()));
            par.Add(db.retorna_parametros("@nm_produto_sub_descr_completa2", produto.nm_produto_sub_descr_completa2.ToString()));
            par.Add(db.retorna_parametros("@vl_produto_sub", produto.vl_produto_sub.ToString().Replace(",",".")));
            par.Add(db.retorna_parametros("@id_tp_produto", produto.id_tp_produto.ToString()));
            par.Add(db.retorna_parametros("@dt_criacao", produto.dt_criacao.ToString("yyyy/MM/hh HH:mm")));
            par.Add(db.retorna_parametros("@id_usuario_criacao", produto.id_usuario_criacao.ToString()));
            par.Add(db.retorna_parametros("@dt_alteracao", produto.dt_alteracao.ToString("yyyy/MM/hh HH:mm")));
            par.Add(db.retorna_parametros("@id_usuario_alteracao", produto.id_usuario_alteracao.ToString()));
            par.Add(db.retorna_parametros("@dv_ativo", produto.dv_ativo.ToString()));
            par.Add(db.retorna_parametros("@vl_desconto", produto.vl_desconto.ToString().Replace(",", ".")));
            par.Add(db.retorna_parametros("@vl_produto_sub_desc", produto.vl_produto_sub_desc.ToString().Replace(",", ".")));
            par.Add(db.retorna_parametros("@dv_desconto", produto.dv_desconto.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }
    }
}
