namespace API_Itailers_Core.Entities
{
    public class t_cliente_token
    {
        public int id_cliente_token { get; set; }
        public int id_cliente { get; set; }
        public string nm_token { get; set; }
        public DateTime dt_validade_token { get; set; }
        public int id_status { get; set; }
    }
}
