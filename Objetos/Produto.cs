using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objetos
{
    public class Produto
    {
        public Produto()
        {
            this.Produto_Subs = new List<Produto_Sub>();
        }

        public int id_produto { set; get; }

        public string nm_produto { set; get; }

        public int id_fabricante { set; get; }

        public string nm_produto_descr_curta { set; get; }

        public string nm_produto_descr_completa { set; get; }

        public string nm_produto_descr_completa2 { set; get; }

        public string link_downaload { set; get; }

        public string cd_produto { set; get; }

        public bool dv_ativo { set; get; }

        public DateTime dt_criacao { set; get; }

        public int id_usuario_criacao { set; get; }

        public DateTime dt_alteracao { set; get; }

        public int id_usuario_alteracao { set; get; }

        public string nm_caminho_img { get; set; }

        public string link_pagina { get; set; }

        public List<Produto_Sub> Produto_Subs { get; set; }
    }

    public class Produto_Sub
    {
        public int id_produto_sub { set; get; }

        public int id_produto { set; get; }

        public string nm_produto_sub { set; get; }

        public int id_tp_produto { set; get; }

        public string nm_produto_sub_descr_curta { set; get; }

        public string nm_produto_sub_descr_completa { set; get; }

        public string nm_produto_sub_descr_completa2 { set; get; }

        public decimal vl_produto_sub { set; get; }

        public bool dv_ativo { set; get; }

        public DateTime dt_criacao { set; get; }

        public int id_usuario_criacao { set; get; }

        public DateTime dt_alteracao { set; get; }

        public int id_usuario_alteracao { set; get; }

        public string cd_produto_sub_publico { get; set; }

        public decimal vl_produto_sub_desc { set; get; }

        public string vl_produto_sub_desc_str { set; get; }

        public bool dv_desconto { set; get; }

        public decimal vl_desconto { set; get; }

        public string vl_desconto_str { set; get; }

        public string checado { get; set; }

        public int id_parceiro_token { set; get; }

        public int nr_trial { set; get; }

        public int dv_vinculo { set; get; }

        public int id_produto_sub_vinculo { set; get; }

        public decimal vl_titular { set; get; }

        public decimal vl_vinculado { set; get; }

        public Objetos.Parceiro parceiro { set; get; }
    }
}
