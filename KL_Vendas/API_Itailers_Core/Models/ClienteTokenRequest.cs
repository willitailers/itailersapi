namespace API_Itailers_Core.Models
{
    public class ClienteTokenRequest
    {
        public int id_cliente { get; set; }
        public string nm_token { get; set; }
        public DateTime dt_validade_token { get; set; }
        public int id_status { get; set; }
    }
}
