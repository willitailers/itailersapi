using System.Collections.Generic;

namespace KL_API.Controllers.CampSoft.Models
{
    public class Produtos
    {
        public string nm_produto { get; set; }
        public string chave_ativacao { get; set; }
        public string imagem_produto { get; set; }
        public string descricao { get; set; }
        public string dt_ativacao { get; set; }
    }

    public class Produto_Retorno
    {
        public string message { get; set; }
        public List<Produtos> produtos { get; set; } = new List<Produtos>();
    }
}