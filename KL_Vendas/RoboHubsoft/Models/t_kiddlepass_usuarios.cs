namespace RoboHubsoft.Models
{
    public class t_kiddlepass_usuarios
    {
        public int id { get; set; }
        public string userid { get; set; }
        public int id_cliente { get; set; }
        public string product_type { get; set; }
        public string status { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public DateTime data_criacao { get; set; }
        public DateTime data_atualizacao { get; set; }
    }
}
