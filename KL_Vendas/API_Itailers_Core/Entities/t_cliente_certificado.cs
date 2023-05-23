namespace API_Itailers_Core.Entities
{
    public class t_cliente_certificado
    {
        public int id_cliente_certificado { get; set; }
        public int id_cliente_token { get; set; }
        public string nm_thumbprint { get; set; }
        public string nm_usuario_certificado { get; set; }
        public string nm_senha_certificado { get; set; }
    }
}
