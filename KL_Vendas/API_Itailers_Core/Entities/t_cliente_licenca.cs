namespace API_Itailers_Core.Entities
{
    public class t_cliente_licenca
    {
        public int id_cliente_licenca { get; set; }
        public int id_cliente_usuario { get; set; }
        public int id_produto { get; set; }
        public string cd_ativacao_kl { get; set; }
        public string nm_subscriber_id { get; set; }
        public string nm_ativacao_android { get; set; }
        public string nm_ativacao_iphone { get; set; }
        public string nm_ativacao_windows { get; set; }
        public string nm_ativacao_mac { get; set; }
        public DateTime dt_ativacao { get; set; }
        public DateTime dt_expiracao { get; set; }
        public DateTime dt_cancelamento { get; set; }
        public int id_status { get; set; }
    }
}
