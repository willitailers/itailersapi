using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objetos
{
    public class Fabricante
    {
        public int id_fabricante { set; get; }

        public string nm_fabricante { set; get; }

        public string cnpj { set; get; }

        public string endereco { set; get; }

        public string cep { set; get; }

        public string cidade { set; get; }

        public string estado { set; get; }

        public bool dv_ativo { set; get; }

        public int id_usuario_criacao { set; get; }

        public DateTime dt_criacao { set; get; }

        public int id_usuario_alteracao { set; get; }

        public DateTime dt_alteracao { set; get; }
    }

    public class Fabricante_Info
    {
        public int id_fabricante_info { set; get; }

        public int id_fabricante { set; get; }

        public string nm_titulo_info { set; get; }

        public string nm_informacao { set; get; }
        
    }

    public class Fabricante_Contato
    {
        public int id_fabricante_contato { set; get; }

        public int id_fabricante { set; get; }

        public string nm_contato { set; get; }

        public string nm_telefone { set; get; }

        public string nm_ramal { set; get; }

        public string nm_observacao { set; get; }

        public bool dv_principal { set; get; }
    }
}
