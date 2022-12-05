using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using Objetos;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KL_Vendas
{
    public partial class Voce : System.Web.UI.Page
    {
        public string Token { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["Token"] = GetParam("nr_token");
                Token = GetParam("nr_token");
                CarregarProdutos();
                CarregarAvaliacao();
            }
        }

        private void CarregarAvaliacao()
        {
            Avaliacao aval = new Avaliacao() { tp_acao = 1, id_avaliacao = 0, nm_avaliacao = "", nm_titulo = "", nm_pagina = "Casa" };

            DataTable dt = new DataTable();
            dt = new Avaliacao_BLL().avaliacao_selecionar(aval);

            DataTable dt_new = new DataTable();
            dt_new.Columns.Add("classe");
            dt_new.Columns.Add("nm_titulo");
            dt_new.Columns.Add("nm_avaliacao");

            int i = 1;

            foreach (DataRow dr in dt.Rows)
            {
                if (i == 1)
                {
                    dt_new.Rows.Add("active", dr["nm_titulo"].ToString(), dr["nm_avaliacao"].ToString());
                }
                else
                    dt_new.Rows.Add("", dr["nm_titulo"].ToString(), dr["nm_avaliacao"].ToString());

                i++;
            }

            rptAvaliacao.DataSource = dt_new;
            rptAvaliacao.DataBind();

        }

        private void CarregarProdutos()
        {
            int count = 0;
            int id_tipo_produto = GetParam("product_type", 1);

            HomeSecurity_BLL business = new HomeSecurity_BLL();

            List<Objetos.Produto> produtos = business.carregar_produtos(id_tipo_produto.ToString(), "0", Token);

            //EqualizaProdutosSubs(ref produtos);

            foreach (var produto in produtos)
            {
                controls.HomeProduct product_control = (controls.HomeProduct)LoadControl("~/controls/HomeProduct.ascx");
                product_control.ID = string.Format("ctrl_product{0}", count.ToString());
                product_control.ProdutoId = produto.id_produto.ToString();
                product_control.NomeProduto = produto.nm_produto;
                product_control.CaminhoImagem = produto.nm_caminho_img;
                product_control.DescricaoProduto = produto.nm_produto_descr_completa;
                product_control.SubsProduto = produto.Produto_Subs;
                product_control.link_pagina = produto.link_pagina;

                divProducts.Controls.Add(product_control);

                count++;
            }
        }

        private void EqualizaProdutosSubs(ref List<Objetos.Produto> produtos)
        {
            int countBigger = 0;

            foreach (var produto in produtos)
            {
                if (produto.Produto_Subs.Count > countBigger)
                    countBigger = produto.Produto_Subs.Count;
            }

            foreach (var produto in produtos)
            {
                if (produto.Produto_Subs.Count != countBigger)
                {
                    for (int i = 0; i < countBigger; i++)
                    {
                        produto.Produto_Subs.Add(new Objetos.Produto_Sub());

                        if (produto.Produto_Subs.Count == countBigger)
                            break;
                    }
                }
            }
        }

        private int GetParam(string name, int default_value)
        {
            try
            {
                return string.IsNullOrEmpty(Request[name]) ? 1 : Convert.ToInt32(Request[name]);
            }
            catch (Exception)
            {
                return default_value;
            }
        }

        private string GetParam(string name)
        {
            try
            {
                return string.IsNullOrEmpty(Request[name]) ? "-" : Request[name];
            }
            catch (Exception)
            {
                return "-";
            }

        }
    }
}