using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using PagarMe;

namespace Objetos
{
    public class Carrinho
    {
        public Int64 id_carrinho { set; get; }

        public string nr_token { set; get; }

        public string id_cliente { set; get; }

        public string cd_carrinho { set; get; }

        public List<CarrinhoItem> carrinhoItems { set; get; }
    }

    public class CarrinhoItem
    {
        public int id_produto_sub { set; get; }

        public int id_produto { set; get; }

        public string nm_produto { set; get; }

        public string nm_produto_sub { set; get; }

        public string nm_produto_sub_descr_curta { set; get; }

        public string vl_produto { set; get; }

        public string nm_caminho_img { set; get; }

        public int quantidade_produto { set; get; }
        public int nr_trial { set; get; }
    }

    public class AssinaturaRetorno
    {
        public int subscribe_id { set; get; }

        public bool valido { set; get; }

        public bool erro_pagto { set; get; }

        public string msgErro { set; get; }
    }
}
