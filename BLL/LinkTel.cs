using DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LinkTel
    {
        public bool Acesso_Wifi(string _username, string _password, string _firstname, string _lastname, string _gender, string _nationality, string _doc_cpf, string _origin, string _date_of_birthday, Int64 _term_end)
        {
            bool retorno = true;

            Conteudo cont = new Conteudo()
            {
                username = _username,
                password = _password,
                firstname = _firstname,
                lastname = _lastname,
                gender = _gender,
                nationality = _nationality,
                doc_cpf = _doc_cpf,
                origin = _origin,
                date_of_birthday = _date_of_birthday,
                term_end = _term_end
            };

            string clientId = System.Configuration.ConfigurationManager.AppSettings["usuario_linktel"].ToString();
            string clientSecret = System.Configuration.ConfigurationManager.AppSettings["senha_linktel"].ToString().Replace("*", "&");
            string credentials = String.Format("{0}:{1}", clientId, clientSecret);

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["url_linktel"].ToString());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

            var jsonContent = JsonConvert.SerializeObject(cont);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new
            MediaTypeHeaderValue("application/json");
            //contentString.Headers.Add("Session-Token", session_token);


            HttpResponseMessage response = client.PostAsync(System.Configuration.ConfigurationManager.AppSettings["url_linktel"].ToString(), contentString).Result;

            var result = response.Content.ReadAsStringAsync().Result;

            retornoLinkTel ret = Newtonsoft.Json.JsonConvert.DeserializeObject<retornoLinkTel>(result);

            HttpStatusCode status = response.StatusCode;

            if (ret.status)
            {
                retorno = true;
            }
            else
            {
                // grava no banco que ao foi gerado
                if (ret.code == 203)
                {
                    // envia atualização de informação
                    if (Renovacao_Wifi(_username, _term_end))
                    {
                        if (Renovacao_Wifi_senha(_username, _password))
                        {
                            retorno = true;
                        }
                    }

                }
                
            }

            return retorno;
        }

        public bool Renovacao_Wifi(string _username, Int64 _term_end)
        {
            bool retorno = true;

            Conteudo cont = new Conteudo()
            {
                term_end = _term_end
            };

            string clientId = System.Configuration.ConfigurationManager.AppSettings["usuario_linktel"].ToString();
            string clientSecret = System.Configuration.ConfigurationManager.AppSettings["senha_linktel"].ToString().Replace("*", "&");
            string credentials = String.Format("{0}:{1}", clientId, clientSecret);

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["url_linktel_update"].ToString() + "/" + _username);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

            var jsonContent = JsonConvert.SerializeObject(cont);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new
            MediaTypeHeaderValue("application/json");
            //contentString.Headers.Add("Session-Token", session_token);

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), System.Configuration.ConfigurationManager.AppSettings["url_linktel_update"].ToString() + "/" + _username);
            request.Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.SendAsync(request).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            retornoLinkTel ret = Newtonsoft.Json.JsonConvert.DeserializeObject<retornoLinkTel>(result);
            HttpStatusCode status = response.StatusCode;

            if (ret.code == 200)
            {
                retorno = true;
            }
            else
            {
                Generico.log_inserir(100, "Renovação de e-mail não gerada: " + _username + " - Erro : " + (string.IsNullOrEmpty(ret.message) ? "msg de erro não enviada " : ret.message) + " - Status: " + ret.code.ToString(), 2);
            }

            return retorno;
        }

        public bool Renovacao_Wifi_senha(string _username, string pass)
        {
            bool retorno = true;

            Conteudo cont = new Conteudo()
            {
                password = pass
            };

            string clientId = System.Configuration.ConfigurationManager.AppSettings["usuario_linktel"].ToString();
            string clientSecret = System.Configuration.ConfigurationManager.AppSettings["senha_linktel"].ToString().Replace("*", "&");
            string credentials = String.Format("{0}:{1}", clientId, clientSecret);

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["url_linktel_update"].ToString() + "/" + _username);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

            var jsonContent = JsonConvert.SerializeObject(cont);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new
            MediaTypeHeaderValue("application/json");
            //contentString.Headers.Add("Session-Token", session_token);

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), System.Configuration.ConfigurationManager.AppSettings["url_linktel_update"].ToString() + "/" + _username);
            request.Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.SendAsync(request).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            retornoLinkTel ret = Newtonsoft.Json.JsonConvert.DeserializeObject<retornoLinkTel>(result);
            HttpStatusCode status = response.StatusCode;

            if (ret.code == 200)
            {
                retorno = true;
            }
            else
            {
                Generico.log_inserir(100, "Senha de e-mail não gerada: " + _username + " - Erro : " + (string.IsNullOrEmpty(ret.message) ? "msg de erro não enviada " : ret.message) + " - Status: " + ret.code.ToString(), 2);

            }

            return retorno;
        }
    }

    
    class retornoLinkTel
    {
        public bool status { set; get; }

        public int code { set; get; }

        public string message { set; get; }

        public string error { set; get; }



    }

    class AccessToken
    {
        public string user { get; set; }
        public string password { get; set; }
    }

    public class Conteudo
    {
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string gender { get; set; }
        public string nationality { get; set; }
        public string doc_cpf { get; set; }
        public string origin { get; set; }
        public string date_of_birthday { get; set; }
        public Int64 term_end { get; set; }
    }
}
