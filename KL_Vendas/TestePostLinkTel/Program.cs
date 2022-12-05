using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace TestePostLinkTel
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var encode = Encoding.UTF8;
                Console.OutputEncoding = encode;

                Conteudo cont = new Conteudo()
                {
                    username = "teste@gmail.com",
                    password = "123123",
                    firstname = "Bruno",
                    lastname = "teste",
                    gender = "M",
                    nationality = "Brasil",
                    doc_cpf = "81969301007",
                    origin = "KasperskyCombo1",
                    date_of_birthday = "1995-05-05",
                    term_end = 1550545200
                };

                string tx2 = JsonConvert.SerializeObject(cont);

                Task t = new Task(Conecta);
                t.Start();

                /*

                string urlServidor = "https://api.linktelwifi.com.br/users";

                string postData = tx2;
                HttpWebRequest http = WebRequest.Create(urlServidor) as HttpWebRequest;
                http.Headers["Authorization"] = "Basic", Convert.ToBase64String(Encoding.Default.GetBytes("kaspersky:762&9@to"));  //Autenticação
                //http.Headers["Authorization"] = "Basic Auth,user:kaspersky,password:762&9@to"; //Autenticação
                http.Method = "POST";
                http.ContentType = "application/x-www-form-urlencoded";
                http.ContentLength = postData.Length;
                //http.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
                http.Accept = "application/json";

                StreamWriter requestWriter = new StreamWriter(http.GetRequestStream());
                requestWriter.Write(postData);
                requestWriter.Close();

                StreamReader responseReader = new StreamReader(http.GetResponse().GetResponseStream());
                string respostaData = responseReader.ReadToEnd();
                Console.WriteLine(respostaData);
                responseReader.Close();
                http.GetResponse().Close();

                
                */

                Console.ReadKey();


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async void Conecta()
        {
            Conteudo cont = new Conteudo()
            {
                username = "teste4@gmail.com",
                password = "123123",
                firstname = "Bruno",
                lastname = "teste",
                gender = "M",
                nationality = "Brasil",
                doc_cpf = "22576328342",
                origin = "KasperskyCombo1",
                date_of_birthday = "1995-05-05",
                term_end = 1550545200
            };

            string clientId = "kaspersky";
            string clientSecret = "762&9@to";
            string credentials = String.Format("{0}:{1}", clientId, clientSecret);

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://api232.linktelwifi.com.br/users");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

            var jsonContent = JsonConvert.SerializeObject(cont);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new
            MediaTypeHeaderValue("application/json");
            //contentString.Headers.Add("Session-Token", session_token);


            HttpResponseMessage response = client.PostAsync("https://api232.linktelwifi.com.br/users", contentString).Result;

            Console.WriteLine(response.ToString());

            Console.WriteLine(response.Content.ToString());

            HttpStatusCode status = response.StatusCode;

            Console.WriteLine(status);

            if (status == HttpStatusCode.Created )
            {
                Console.WriteLine("Cadastro realizado com sucesso");
            }
            else
            {
                Console.WriteLine("Status do cadastro: " + status);
            }
        }

        //private static async Task<string> GetToken(string body)
        //{
        //    //string clientId = "kaspersky";
        //    //string clientSecret = "762&9@to";
        //    //string credentials = String.Format("{0}:{1}", clientId, clientSecret);

        //    //using (var client = new HttpClient())
        //    //{
        //    //    //Define Headers
        //    //    client.DefaultRequestHeaders.Accept.Clear();
        //    //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

        //    //    //Prepare Request Body
        //    //    string requestData = body;
        //    //    //requestData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

        //    //    FormUrlEncodedContent requestBody = new FormUrlEncodedContent(;

        //    //    //Request Token
        //    //    var request = await client.PostAsync("https://accounts.spotify.com/api/token", requestBody);
        //    //    var response = await request.Content.ReadAsStringAsync();
        //    //    return JsonConvert.DeserializeObject<AccessToken>(response).ToString();
        //    //}
        //}
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
        public int term_end { get; set; }
    }
    
}
