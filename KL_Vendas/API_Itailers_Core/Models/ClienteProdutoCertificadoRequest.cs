namespace API_Itailers_Core.Models
{
    public class ClienteProdutoCertificadoRequest
    {
        public int id_cliente { get; set; }
        public int id_produto_kl { get; set; }
        public int id_cliente_certificado { get; set; }
        public int dv_ativa_cadastro { get; set; }
    }
}
