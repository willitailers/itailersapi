using KL_API.Models.Integracao;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Web.Http.Description;
using System.Linq;

namespace KL_API.Controllers.Integracao
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class IntegracaoEmailsController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post()
        {
            Models.Integracao.Integracao integracao = new Models.Integracao.Integracao();

            var emails_enviar = integracao.RetornaIntegracaoEmailsEnviar();

            List<EmailsEnviar> emailsEnviar = new List<EmailsEnviar>();
            foreach (DataRow row in emails_enviar.Rows)
            {
                var id_cliente = row["id_cliente"].ToString();
                var conteudo = row["conteudo"].ToString();
                var email = row["email"].ToString();
                var nome = row["nome"].ToString();
                var nome_cliente = row["nome_cliente"].ToString();
                var id_usuario = row["id_usuario"].ToString();

                conteudo = conteudo.Replace("[[nome_cliente]]", nome);
                conteudo = conteudo.Replace("[[nome_provedor]]", nome_cliente);
                conteudo = conteudo.Replace("[[nome_provedor.baixemeuapp.com.br]]", $"{nome_cliente.ToLower()}.baixemeuapp.com.br");

                var ativacao_usuarios = integracao.RetornaIntegracaoAtivacaoUsuarios(id_usuario);
                string ativacoes_html = string.Empty;
                bool enviar = true;
                foreach (DataRow ativacao in ativacao_usuarios.Rows)
                {
                    if (ativacao["chave_ativacao"].ToString().Trim() == "" || ativacao["chave_ativacao"].ToString().Trim() == "-")
                    {
                        enviar = false;
                    }

                    ativacoes_html += $"<p style=\"font-size:1rem;line-height:1.5rem;margin:16p2P3JJx 0;background-color:rgb(250,251,251);text-align:center;\">{ativacao["chave_ativacao"]}</p>";
                }

                if (enviar == false) continue;

                conteudo = conteudo.Replace("[[chave_ativacao]]", ativacoes_html);

                string[] emails = email.Split(';');
                string[] uniqueEmails = emails.Distinct().ToArray();
                string resultString = string.Join(";", uniqueEmails);

                email = resultString.Replace(";", ","); //n8n diferencia os emails por virgula.

                EmailsEnviar cliente = new EmailsEnviar()
                {
                    conteudo = conteudo,
                    email = email,
                    id_cliente = id_cliente,
                    nome = nome,
                    nome_cliente = nome_cliente
                };

                emailsEnviar.Add(cliente);

                integracao.AtualizarIntegracaoEmailsEnviados(id_usuario);
            }

            return Request.CreateResponse(HttpStatusCode.OK, emailsEnviar);
        }
    }
}