using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objetos
{
    public class Parceiro
    {
        public int id_parceiro_token { set; get; }

        public int id_parceiro { set; get; }
        public int id_parceiro_dados_pagto { set; get; }

        public string nr_token { set; get; }
        public string nm_link { set; get; }
        public string url_amigavel { set; get; }
        public string url_acesso { set; get; }
    }
}
