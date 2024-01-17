using KL_API.Models.Integracao;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Web.Http.Description;
using System.Linq;

namespace KL_API.Controllers.api
{
    public class Request
    {
        public string id_cliente { get; set; }
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public class ApiEmailsEnviarController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Request obj)
        {
            Models.Integracao.Integracao integracao = new Models.Integracao.Integracao();

            var emails_enviar = integracao.RetornaEmailsEnviarAPI(obj.id_cliente);

            List<EmailsEnviar> emailsEnviar = new List<EmailsEnviar>();

            var retornarIntegracaoTemplate = integracao.RetornaIntegracaoTemplate();
            DataRow[] template_data = retornarIntegracaoTemplate.Select("id_cliente = 15");
            string conteudo = template_data[0]["conteudo"].ToString();

            foreach (DataRow row in emails_enviar.Rows)
            {
                var email = row["nm_email"].ToString();
                var id_cliente_usuario = row["id_cliente_usuario"].ToString();
                var nm_user_id = row["nm_user_id"].ToString();
                var ativacao_usuarios = integracao.RetornaApiAtivacaoUsuarios(id_cliente_usuario);

                string ativacoes_html = string.Empty;
                foreach (DataRow ativacao in ativacao_usuarios.Rows)
                {
                    ativacoes_html += $"<p>{ativacao["cd_ativacao_kl"]}</p>";
                }

                conteudo = conteudo.Replace("[[chave_ativacao]]", ativacoes_html);

                string[] emails = email.Split(';');
                string[] uniqueEmails = emails.Distinct().ToArray();
                string resultString = string.Join(";", uniqueEmails);

                email = resultString.Replace(";", ","); //n8n diferencia os emails por virgula.

                EmailsEnviar cliente = new EmailsEnviar()
                {
                    conteudo = conteudo,
                    email = email,
                    id_cliente = obj.id_cliente
                };

                emailsEnviar.Add(cliente);

                integracao.AtualizaApiEmailsEnviados(id_cliente_usuario);
            }

            return Request.CreateResponse(HttpStatusCode.OK, emailsEnviar);
        }
    }
}