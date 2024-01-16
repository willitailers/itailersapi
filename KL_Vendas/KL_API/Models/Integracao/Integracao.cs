using DAL;
using Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace KL_API.Models.Integracao
{
    public class EmailsEnviar
    {
        public string id_cliente { get; set; }
        public string nome { get; set; }
        public string nome_cliente { get; set; }
        public string email { get; set; }
        public string conteudo { get; set; }
    }

    public class UsuariosAtivar
    {
        public int id { get; set; }
        public string id_subscriber { get; set; }
        public int id_cliente { get; set; }
        public int id_usuario { get; set; }
        public int id_produto_relacionado { get; set; }
        public string chave_ativacao { get; set; }
        public int id_produto_kl { get; set;}
        public string cd_produto_kl { get; set; }
        public string qtd_licencas { get; set; }
        public int id_cliente_it { get; set; }
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

        public DataTable RetornaIntegracaoUsuariosNaoAtivados()
        {
            DataBase db = new DataBase();

            db.procedure = "p_integracao_consulta_usuarios_nao_ativados";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable RetornaIntegracaoLogin(string email, string cpf_cnpj)
        {
            DataBase db = new DataBase();

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@email", email),
                db.retorna_parametros("@cpf_cnpj", cpf_cnpj),
            };

            db.parametros = par;

            db.procedure = "p_integracao_login";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable RetornaIntegracaoProdutos(string id_usuario)
        {
            DataBase db = new DataBase();

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_usuario", id_usuario)
            };

            db.parametros = par;

            db.procedure = "p_integracao_produtos";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable RetornaIntegracaoRelacaoProdutos(string id_cliente)
        {
            DataBase db = new DataBase();

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente", id_cliente)
            };

            db.parametros = par;

            db.procedure = "p_integracao_consulta_relacao_produtos";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable AtualizaIntegracaoAtivacaoChave(string id_cliente, string id_subscriber, string chave_ativacao, string id_produto_relacionado)
        {
            DataBase db = new DataBase();

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente", id_cliente),
                db.retorna_parametros("@id_subscriber", id_subscriber),
                db.retorna_parametros("@chave_ativacao", chave_ativacao),
                db.retorna_parametros("@id_produto_relacionado", id_produto_relacionado),
            };

            db.parametros = par;

            db.procedure = "p_integracao_atualiza_ativacao_chave";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable RetornaIntegracaoAtivacaoUsuarios(string id_usuario)
        {
            DataBase db = new DataBase();

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_usuario", id_usuario)
            };

            db.parametros = par;

            db.procedure = "p_integracao_consulta_ativacoes_usuario";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable AtualizarIntegracaoEmailsEnviados(string id_usuario)
        {
            DataBase db = new DataBase();

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_usuario", id_usuario)
            };

            db.parametros = par;

            db.procedure = "p_integracao_consulta_atualiza_emails_enviados";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable AtualizaIntegracaoUsuarios(string id_cliente, string email, string cpf_cnpj, string nome, string cel, bool ativo)
        {
            DataBase db = new DataBase();

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente", id_cliente),
                db.retorna_parametros("@email", email),
                db.retorna_parametros("@cpf_cnpj", cpf_cnpj),
                db.retorna_parametros("@nome", nome),
                db.retorna_parametros("@cel", cel),
                db.retorna_parametros("@ativo", ativo.ToString())
            };

            db.parametros = par;

            db.procedure = "p_integracao_atualiza_usuario";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable AtualizaIntegracaoAtivacao(string id_subscriber, string id_cliente, string id_usuario, string id_produto_relacionado, bool ativo)
        {
            DataBase db = new DataBase();

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_subscriber", id_subscriber),
                db.retorna_parametros("@id_cliente", id_cliente),
                db.retorna_parametros("@id_usuario", id_usuario),
                db.retorna_parametros("@id_produto_relacionado", id_produto_relacionado),
                db.retorna_parametros("@ativo", ativo.ToString())
            };

            db.parametros = par;

            db.procedure = "p_integracao_atualiza_ativacao";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable InsereIntegracaoLog(string nm_log)
        {
            DataBase db = new DataBase();

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@nm_log", nm_log)
            };

            db.parametros = par;

            db.procedure = "p_integracao_insere_log";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public DataTable RelatorioAtivacoes()
        {
            DataBase db = new DataBase();

            db.procedure = "p_integracao_relatorio_ativacoes";
            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);
        }

        public ClientInfo RetornaClientInfo(string id_cliente)
        {
            DataBase db = new DataBase();
            db.procedure = "p_integracao_retorna_cliente_info";

            List<parametros> par = new List<parametros>
            {
                db.retorna_parametros("@id_cliente", id_cliente)
            };

            db.parametros = par;
            var dt = Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_API);

            if (dt.Rows.Count == 0)
            {
                return new ClientInfo() { valido = false };
            }
            else
            {
                return new ClientInfo()
                {
                    valido = true,
                    id_cliente = int.Parse(dt.Rows[0]["id_cliente"].ToString()),
                    id_cliente_certificado = int.Parse(dt.Rows[0]["id_cliente_certificado"].ToString()),
                    nm_senha_certificado = dt.Rows[0]["nm_senha_certificado"].ToString(),
                    nm_thumbprint = dt.Rows[0]["nm_thumbprint"].ToString(),
                    nm_usuario_certificado = dt.Rows[0]["nm_usuario_certificado"].ToString()
                };
            }
        }

        public string GenerateUniqueId(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                // Pegar os primeiros 30 caracteres do hash
                return hashString.Substring(0, Math.Min(hashString.Length, 30));
            }
        }
    }
}