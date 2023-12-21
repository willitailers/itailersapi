using DAL;
using Objetos;
using System.Collections.Generic;
using System.Data;

namespace KL_API.Models.Integracao
{
    public class EmailsEnviar
    {
        public string id_cliente { get; set; }
        public string nome { get; set; }
        public string nome_cliente { get; set; }
        public string conteudo { get; set; }
        public string email { get; set; }
    }

    public class Integracao
    {
        public DataTable RetornaIntegracaoClientes()
        {
            DataBase db = new DataBase();

            db.procedure = "p_integracao_consulta_clientes";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable RetornaIntegracaoTemplate()
        {
            DataBase db = new DataBase();

            db.procedure = "p_integracao_consulta_template";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable RetornaIntegracaoEmailsEnviar()
        {
            DataBase db = new DataBase();

            db.procedure = "p_integracao_consulta_emails_enviar";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable RetornaIntegracaoUsuarios()
        {
            DataBase db = new DataBase();

            db.procedure = "p_integracao_consulta_usuario";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable AtualizaIntegracaoUsuarios(string id_cliente, string email, string cpf, string nome, string cel, bool ativo)
        {
            DataBase db = new DataBase();

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente", id_cliente),
                db.retorna_parametros("@email", email),
                db.retorna_parametros("@cpf", cpf),
                db.retorna_parametros("@nome", nome),
                db.retorna_parametros("@cel", cel),
                db.retorna_parametros("@ativo", ativo.ToString())
            };

            db.parametros = par;

            db.procedure = "p_integracao_atualiza_usuario";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }
    }
}