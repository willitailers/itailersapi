using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboHubsoft.Models
{
    public class t_hubsoft_filaprocessamento
    {
        public int id { get; set; }
        public int id_cliente { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
        public bool processando { get; set; }
        public DateTime dt_criacao { get; set; }
    }
}
