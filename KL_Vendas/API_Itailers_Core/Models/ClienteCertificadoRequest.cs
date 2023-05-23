namespace API_Itailers_Core.Models
{
    public class ClienteCertificadoRequest
    {
        public int id_cliente_token { get; set; }
        public string nm_thumbprint { get; set; }
        public string nm_usuario_certificado { get; set; }
        public string nm_senha_certificado { get; set; }
    }
}