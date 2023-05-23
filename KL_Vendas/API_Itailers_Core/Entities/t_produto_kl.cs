namespace API_Itailers_Core.Entities
{
    public class t_produto_kl
    {
        public int id_produto_kl { get; set; }
        public string nm_produto_kl { get; set; }
        public string cd_produto_kl { get; set; }
        public string nm_urn { get; set; }
        public int qtd_licencas { get; set;}
        public int id_combo { get; set; }
        public string imagem_produto { get; set; }
        public string descricao { get; set; }
    }
}
