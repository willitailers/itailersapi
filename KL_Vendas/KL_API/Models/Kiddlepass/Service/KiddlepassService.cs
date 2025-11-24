using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KL_API.Models.Kiddlepass.Service
{
    public sealed class KiddlepassApiClient : IDisposable
    {
        // Homologacao
        //private readonly string _bearerToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2OGVmZDFjZGNlN2E1MDYzNTE0MGRmMmYiLCJlbnYiOiJob21vbG9nIn0.e9S5QWXZapDJg01ibQ2JgREMRzC0uN7jYquAOImnAKo";
        
        // Producao
        private readonly string _bearerToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2OGVmZDFjZGNlN2E1MDYzNTE0MGRmMmYifQ.RwOqCYmeLkfk3-l6DfHtTMBVVO5oasNWONlQ4hUtvMo";

        private readonly HttpClient _http;
        private readonly string _endpointUrl = "https://kiddlepass-homolog-ws-8f793e3cfebd.herokuapp.com/ws";

        public KiddlepassApiClient()
        {
            _http = new HttpClient();
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
            _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> PostInsertUserAsync (
            string id,
            string associatedCompanyExternalId,
            string productType,
            string email,
            string name,
            CancellationToken ct = default)
        {
            var payload = new
            {
                id = id,
                associated_company_external_id = associatedCompanyExternalId,
                productType = productType,
                communication = new
                {
                    email = email,
                    name = name
                }
            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync(_endpointUrl, content, ct);
            return response;
        }

        public async Task<HttpResponseMessage> PutUpdateUserAsync(
            string id,
            string action,
            string associatedCompanyExternalId,
            string productType,
            CancellationToken ct = default)
        {
            var payload = new
            {
                id = id,
                action = action,
                associated_company_external_id = associatedCompanyExternalId,
                productType = productType,
            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PutAsync(_endpointUrl, content, ct);
            return response;
        }

        public void Dispose()
        {
            _http?.Dispose();
        }
    }
}
