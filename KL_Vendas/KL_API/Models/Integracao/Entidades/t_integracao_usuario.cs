using System;

namespace KL_API.Models.Integracao.Entidades
{
    public class t_integracao_usuario
    {
        public int id { get; set; }
        public int id_cliente { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string cel { get; set; }
        public bool ativo { get; set; }
        public DateTime data_atualizacao { get; set; }
    }
}