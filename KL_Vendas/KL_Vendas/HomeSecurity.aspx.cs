using BLL;
using KL_Vendas.controls;
using Objetos;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI;

namespace KL_Vendas
{
    public partial class HomeSecurity : System.Web.UI.Page
    {
        public string Token
        {
            get { return Session["Token"].ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["Token"] = GetParam("nr_token");
                CarregarProdutos();
            }
        }

        private void CarregarProdutos()
        {
            int count = 0;
            int id_tipo_produto = GetParam("product_type", 0);

            HomeSecurity_BLL business = new HomeSecurity_BLL();

            List<Objetos.Produto> produtos = business.carregar_produtos(id_tipo_produto.ToString(), "0", Token);

            EqualizaProdutosSubs(ref produtos);

            foreach (var produto in produtos)
            {
                controls.HomeProduct product_control = (controls.HomeProduct)LoadControl("~/controls/HomeProduct.ascx");
                product_control.ID = string.Format("ctrl_product{0}", count.ToString());
                product_control.ProdutoId = produto.id_produto.ToString();
                product_control.NomeProduto = produto.nm_produto;
                product_control.CaminhoImagem = produto.nm_caminho_img;
                product_control.DescricaoProduto = produto.nm_produto_descr_curta;
                product_control.SubsProduto = produto.Produto_Subs;

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
                        produto.Produto_Subs.Add(new Produto_Sub());

                        if (produto.Produto_Subs.Count == countBigger)
                            break;
                    }
                }
            }
        }

        private string GetParam(string name)
        {
            try
            {
                return Request[name];
            }
            catch (Exception)
            {
                return "-";
            }

        }

        private int GetParam(string name, int default_value)
        {
            try
            {
                return Convert.ToInt32(Request[name]);
            }
            catch (Exception)
            {
                return default_value;
            }
        }
    }
}