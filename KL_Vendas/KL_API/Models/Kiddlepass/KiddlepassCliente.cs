using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KL_API.Models.Kiddlepass
{
    public class KiddlepassCliente
    {
        public int kiddlepass_id_cliente { get; set; }
        public string kiddlepass_nome { get; set; }
        public string kiddlepass_token_acesso { get; set; }
        public DateTime kiddlepass_data_criacao { get; set; }
        public bool valido { get; set; }
    }
}