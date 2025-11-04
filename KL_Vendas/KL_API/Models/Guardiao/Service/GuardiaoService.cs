using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KL_API.Models.Guardiao.Service
{
    public sealed class GuardiaoApiClient : IDisposable
    {
        private const string EndpointUrl = "https://hook.us2.make.com/1jt5vbf6bp6q5udsmtqht5bprk8k0ty6";
        private const string ApiKeyHeaderName = "x-make-apikey";
        private const string ApiKeyHeaderValue = "x&yy&92]z{a+iqA/H]/BQS0^+2iHX&CL";

        private readonly HttpClient _http;

        public GuardiaoApiClient()
        {
            _http = new HttpClient();

            // Header obrigatório do Make
            if (!_http.DefaultRequestHeaders.Contains(ApiKeyHeaderName))
                _http.DefaultRequestHeaders.Add(ApiKeyHeaderName, ApiKeyHeaderValue);

            // Opcional: aceitar JSON
            if (!_http.DefaultRequestHeaders.Accept.Contains(new MediaTypeWithQualityHeaderValue("application/json")))
                _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Replica o cURL do Make: POST com parâmetros na query string e corpo vazio.
        /// </summary>
        public async Task<HttpResponseMessage> SendActionAsync(string email, string action, string name)
        {
            // Monta URL com query string (escapando valores)
            var url = string.Concat(
                EndpointUrl,
                "?email=", Uri.EscapeDataString(email ?? string.Empty),
                "&name=", Uri.EscapeDataString(name ?? string.Empty),
                "&action=", Uri.EscapeDataString(action ?? string.Empty)
            );

            var req = new HttpRequestMessage(HttpMethod.Post, url);

            // Corpo vazio (cURL usa --data '')
            req.Content = new StringContent(string.Empty, Encoding.UTF8, "application/x-www-form-urlencoded");

            return await _http.SendAsync(req);
        }

        public void Dispose()
        {
            _http.Dispose();
        }
    }
}
