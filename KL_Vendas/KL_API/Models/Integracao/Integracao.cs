using DAL;
using Objetos;
using System.Collections.Generic;
using System.Data;

namespace KL_API.Models.Integracao
{
    public class EmailsEnviar 
    {
        public List<string> emails { get; set; }
        public string template { get; set; }
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
    }
}