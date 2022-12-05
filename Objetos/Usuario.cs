using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Objetos
{
    public class Usuario
    {
        public string nm_email { set; get; }

        public int id_usuario { set; get; }

        public string nm_usuario_primeiro { set; get; }

        public string nm_usuario_segundo { set; get; }

        public string nm_senha { set; get; }

        public int dv_ativo { set; get; }

        public bool dv_logado { set; get; }

        public int id_usuario_nivel { set; get; }

        public DataTable menu { set; get; }

    }

    public class Login
    {
        public string user { set; get; }

        public string pass { set; get; }
    }

    public class Cartao
    {
        public string card_hash { set; get; }

        public string card_id { set; get; }

        public string card_number { set; get; }

        public string card_cvv { set; get; }

        public string card_holder_name { set; get; }

        public string card_expiration_date { set; get; }

        public string vl_compra { set; get; }
    }

    public class Cliente
    {
        public string email { set; get; }

        public string name { set; get; }

        public string document_number { set; get; }

        public string street { set; get; }

        public string street_number { set; get; }

        public string zipcode { set; get; }

        public string ddd { set; get; }

        public string phone { set; get; }

        public string Neighborhood { set; get; }
        
    }
}
