using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KL_API.Models.Guardiao
{
    public class GuardiaoClientes
    {
        public int guardiao_id_cliente { get; set; }
        public string guardiao_nome { get; set; }
        public string guardiao_token_acesso { get; set; }
        public DateTime guardiao_data_criacao { get; set; }
        public bool valido { get; set; }
    }
}