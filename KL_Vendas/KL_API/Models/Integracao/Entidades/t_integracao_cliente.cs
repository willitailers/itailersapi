namespace KL_API.Models.Integracao.Entidades
{
    public class t_integracao_cliente
    {
        public int id { get; set; }
        public string cliente { get; set; }
        public string url { get; set; }
        public string id_produto { get; set; }
        public string usuario_itailers { get; set; }
        public string password_itailers { get; set; }
    }
}