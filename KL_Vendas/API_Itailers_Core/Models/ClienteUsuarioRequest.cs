namespace API_Itailers_Core.Models
{
    public class ClienteUsuarioRequest
    {
        public int id_cliente { get; set; }
        public string nm_user_id { get; set; }
        public string nm_transaction_id { get; set; }
        public string nm_email { get; set; }
        public DateTime dt_start { get; set; }
        public DateTime dt_end { get; set; }
        public string nm_user_document { get; set; }
        public string nm_user_plan { get; set; }
        public int id_status { get; set; }
    }
}
