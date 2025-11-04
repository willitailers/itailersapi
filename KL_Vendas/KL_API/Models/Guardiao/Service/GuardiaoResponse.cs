using Newtonsoft.Json;

namespace KL_API.Models.Guardiao.Service
{
    /// <summary>
    /// Representa o retorno da API Guardiao
    /// </summary>
    public class GuardiaoResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}