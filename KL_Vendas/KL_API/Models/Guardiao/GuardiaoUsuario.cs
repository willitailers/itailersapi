using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KL_API.Models.Guardiao
{
    public class GuardiaoUsuario
    {
        public int guardiao_id_usuario { get; set; }
        public int guardiao_id_cliente { get; set; }
        public string guardiao_nome { get; set; }
        public string guardiao_email { get; set; }
        public string guardiao_status { get; set; }
        public DateTime guardiao_data_atualizacao { get; set; }
        public DateTime guardiao_data_criacao { get; set; }
    }
}