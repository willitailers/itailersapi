using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objetos;
using System.Data;

namespace DAL
{
    public class Fabricante_DAL
    {
        public DataTable fabricante_selecionar(Fabricante fabricante)
        {
            DataBase db = new DataBase();
            db.procedure = "p_fabricante";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_fabricante", fabricante.id_fabricante.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable fabricante_incluir(Fabricante fabricante)
        {
            DataBase db = new DataBase();
            db.procedure = "p_fabricante_incluir";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_fabricante", fabricante.id_fabricante.ToString()));
            par.Add(db.retorna_parametros("@nm_fabricante", fabricante.nm_fabricante.ToString()));
            par.Add(db.retorna_parametros("@cnpj", fabricante.cnpj.ToString()));
            par.Add(db.retorna_parametros("@endereco", fabricante.endereco.ToString()));
            par.Add(db.retorna_parametros("@cep", fabricante.cep.ToString()));
            par.Add(db.retorna_parametros("@cidade", fabricante.cidade.ToString()));
            par.Add(db.retorna_parametros("@estado", fabricante.estado.ToString()));
            par.Add(db.retorna_parametros("@dt_criacao", fabricante.dt_criacao.ToString("yyyy/MM/hh HH:mm")));
            par.Add(db.retorna_parametros("@id_usuario_criacao", fabricante.id_usuario_criacao.ToString()));
            par.Add(db.retorna_parametros("@dt_alteracao", fabricante.dt_alteracao.ToString("yyyy/MM/hh HH:mm")));
            par.Add(db.retorna_parametros("@id_usuario_alteracao", fabricante.id_usuario_alteracao.ToString()));
            par.Add(db.retorna_parametros("@dv_ativo", fabricante.dv_ativo.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable fabricante_info_selecionar(Fabricante_Info fabricante)
        {
            DataBase db = new DataBase();
            db.procedure = "p_fabricante_info";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_fabricante_info", fabricante.id_fabricante_info.ToString()));
            par.Add(db.retorna_parametros("@id_fabricante", fabricante.id_fabricante.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable fabricante_info_incluir(Fabricante_Info fabricante)
        {
            DataBase db = new DataBase();
            db.procedure = "p_fabricante_info_incluir";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_fabricante", fabricante.id_fabricante.ToString()));
            par.Add(db.retorna_parametros("@nm_titulo_info", fabricante.nm_titulo_info.ToString()));
            par.Add(db.retorna_parametros("@nm_informacao", fabricante.nm_informacao.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable fabricante_contato_selecionar(Fabricante_Contato fabricante)
        {
            DataBase db = new DataBase();
            db.procedure = "p_fabricante_contato";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_fabricante_contato", fabricante.id_fabricante_contato.ToString()));
            par.Add(db.retorna_parametros("@id_fabricante", fabricante.id_fabricante.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable fabricante_contato_incluir(Fabricante_Contato fabricante)
        {
            DataBase db = new DataBase();
            db.procedure = "p_fabricante_contato_incluir";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_fabricante", fabricante.id_fabricante.ToString()));
            par.Add(db.retorna_parametros("@nm_contato", fabricante.nm_contato.ToString()));
            par.Add(db.retorna_parametros("@nm_telefone", fabricante.nm_telefone.ToString()));
            par.Add(db.retorna_parametros("@nm_ramal", fabricante.nm_ramal.ToString()));
            par.Add(db.retorna_parametros("@nm_observacao", fabricante.nm_observacao.ToString()));
            par.Add(db.retorna_parametros("@dv_principal", fabricante.dv_principal.ToString()));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }
    }
}
