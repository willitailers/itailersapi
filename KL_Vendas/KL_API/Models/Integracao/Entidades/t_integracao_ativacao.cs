using System;

namespace KL_API.Models.Integracao.Entidades
{
    public class t_integracao_ativacao
    {
        public int id { get; set; }
        public int id_cliente { get; set; }
        public int id_usuario { get; set; }
        public int id_produto_kl { get; set; }
        public string chave_ativacao { get; set; }
        public bool ativo { get; set; }
        public DateTime data_atualizacao { get; set; }
    }
}