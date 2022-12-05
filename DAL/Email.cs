using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Threading.Tasks;
//using Amazon.SimpleEmail;
//using Amazon.SimpleEmail.Model;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace DAL
{
    public class Email
    {
        //public bool EnviaEmail(string EmailDestintario, string EmailDestinatarioCopia, string corpoHtml, string Assunto)
        //{
            //try
            //{
            //    var config = new AmazonSimpleEmailServiceConfig();
            //    //config.AuthenticationRegion = "eu-west-1";
            //    config.RegionEndpoint = Amazon.RegionEndpoint.USEast1;
            //    var client = new AmazonSimpleEmailServiceClient(config);
            //    SendEmailRequest request = new SendEmailRequest();

            //    request.Destination = new Destination();
            //    request.Destination.ToAddresses.Add(EmailDestintario.ToLower());
            //    if (!string.IsNullOrEmpty(EmailDestinatarioCopia))
            //        request.Destination.CcAddresses.Add(EmailDestinatarioCopia);

            //    //request.Destination.BccAddresses.Add("tom@myhub.com.br");

            //    request.Message = new Message();
            //    request.Message.Body = new Body();
            //    request.Message.Body.Html = new Amazon.SimpleEmail.Model.Content();
            //    request.Message.Body.Html.Data = corpoHtml;
            //    //request.Message.Body.Text = new Amazon.SimpleEmail.Model.Content();
            //    //request.Message.Body.Text.Data = "Hello World! I'm in Text.";
            //    request.Message.Subject = new Amazon.SimpleEmail.Model.Content();
            //    request.Message.Subject.Data = Assunto;

            //    request.Source = ConfigurationManager.AppSettings["EmailLog"].ToString(); // ? fala que é um identificador do e-mail no formato ascii
            //    request.ReturnPath = ConfigurationManager.AppSettings["EmailLog"].ToString(); // email que irá o log do e-mail enviado
            //    request.ReplyToAddresses.Add(ConfigurationManager.AppSettings["EmailRetorno"].ToString()); // email para qual ira uma resposta caso o recebedor queria responder

            //    var response = client.SendEmail(request);

            //    return true;
            //}
            //catch (Exception ex)
            //{

            //    return false;
            //}
        //}

        public string EnviaEmail(string EmailDestintario, string EmailDestinatarioCopia, string corpoHtml, string Assunto)
        {
            try
            {
                var fromAddress = new MailAddress(ConfigurationManager.AppSettings["EmailLog"].ToString(), ConfigurationManager.AppSettings["EmailTitulo"].ToString().Replace("+","&"));
                var toAddress = new MailAddress(EmailDestintario, EmailDestintario);
                string fromPassword = ConfigurationManager.AppSettings["cdr"].ToString();
                //const string subject = "test";
                //const string body = "Hey now!!";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = Assunto,
                    Body = corpoHtml,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(message);
                }

                return "";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
           

            var fromAddress = new MailAddress("seupedido@antiviruslinktel.com.br", "Tom Mysubvi");
            var toAddress = new MailAddress(recepientEmail, "To Name");
            const string fromPassword = "Linktel577sp@";
            //const string subject = "test";
            //const string body = "Hey now!!";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }

        public string pedido_recebido(string userName, string nr_pedido, string dt_pedido, bool ambiente_web)
        {
            try
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(ambiente_web ? HttpContext.Current.Server.MapPath("~/Email/Pedido_Recebido.html") : @"c:/Email/Pedido_Recebido.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{nome_cliente}", userName);
                body = body.Replace("{nr_pedido}", nr_pedido);
                body = body.Replace("{dt_pedido}", dt_pedido);
                return body;
            }
            catch
            {
                return "";
            }
        }

        public string pagamento_aprovado(string userName, string caminho_imagem, string nm_produto, string nr_licencas, string key, string nome_completo, string email_cliente, bool ambiente_web)
        {
            try
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(ambiente_web ? HttpContext.Current.Server.MapPath("~/Email/Pagamento_Aprovado.html") : @"c:/Email/Pagamento_Aprovado.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{nome_cliente}", userName);
                body = body.Replace("{caminho_imagem}", caminho_imagem);
                body = body.Replace("{nm_produto}", nm_produto);
                body = body.Replace("{nr_licencas}", nr_licencas);
                body = body.Replace("{key}", key);
                body = body.Replace("{nome_completo}", nome_completo);
                body = body.Replace("{email_cliente}", email_cliente);
                return body;
            }
            catch
            {
                return "";
            }
        }

        public string wifi_senha(string nome_cliente, string email, string senha, bool ambiente_web)
        {
            try
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(ambiente_web ? HttpContext.Current.Server.MapPath("~/Email/Wifi_Acesso.html") : @"c:/Email/Wifi_Acesso.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{nome_cliente}", nome_cliente);
                body = body.Replace("{email}", email);
                body = body.Replace("{senha}", senha);
                return body;
            }
            catch
            {
                return "";
            }
        }

        public string pedido_analise(string userName, string nr_pedido, string dt_pedido, bool ambiente_web)
        {
            try
            { 
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(ambiente_web ? HttpContext.Current.Server.MapPath("~/Email/Pedido_Analise.html") : @"c:/Email/Pedido_Analise.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{nome_cliente}", userName);
                body = body.Replace("{nr_pedido}", nr_pedido);
                body = body.Replace("{dt_pedido}", dt_pedido);
                return body;
            }
            catch
            {
                return "";
            }
        }

        public string pedido_cancelado(string userName, string nr_pedido, string dt_pedido, bool ambiente_web)
        {
            try
            { 
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(ambiente_web ? HttpContext.Current.Server.MapPath("~/Email/Assinatura_Cancelada.html") : @"c:/Email/Assinatura_Cancelada.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{nome_cliente}", userName);
                body = body.Replace("{nr_pedido}", nr_pedido);
                body = body.Replace("{dt_pedido}", dt_pedido);
                return body;
            }
            catch
            {
                return "";
            }
        }

        public string assinatura_cancelada(string userName, bool ambiente_web)
        {
            try
            { 
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(ambiente_web ? HttpContext.Current.Server.MapPath("~/Email/Assinatura_Cancelada.html") : @"c:/Email/Assinatura_Cancelada.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{nome_cliente}", userName);
                return body;
            }
            catch
            {
                return "";
            }
        }

        public string pedido_recusado(string userName, string nr_pedido, string dt_pedido, bool ambiente_web)
        {
            try
            { 
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(ambiente_web ? HttpContext.Current.Server.MapPath("~/Email/Pedido_Recusado.html") : @"c:/Email/Pedido_Recusado.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{nome_cliente}", userName);
                body = body.Replace("{nr_pedido}", nr_pedido);
                body = body.Replace("{dt_pedido}", dt_pedido);
                return body;
            }
            catch
            {
                return "";
            }
        }

        public string ativar_licenca(string userName, string url, string key, bool ambiente_web)
        {
            try
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(ambiente_web ? HttpContext.Current.Server.MapPath("~/Email/Ative_Licenca.html") : @"c:/Email/Ative_Licenca.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{nome_cliente}", userName);
                body = body.Replace("{URL}", url);
                body = body.Replace("{key}", key);
                return body;
            }
            catch
            {
                return "";
            }
        }

        public string pagamento_confirmado(string userName, string nm_fatura, string nr_cartao_final, string vl_compra, string vl_compra_final, bool ambiente_web)
        {
            try
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(ambiente_web ? HttpContext.Current.Server.MapPath("~/Email/Pagamento_Confirmado.html") : @"c:/Email/Pagamento_Confirmado.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{nome_cliente}", userName);
                body = body.Replace("{nm_fatura}", nm_fatura);
                body = body.Replace("{nr_cartao_final}", nr_cartao_final);
                body = body.Replace("{vl_compra}", vl_compra);
                body = body.Replace("{vl_compra_final}", vl_compra_final);
                return body;
            }
            catch
            {
                return "";
            }
        }
    }
}
