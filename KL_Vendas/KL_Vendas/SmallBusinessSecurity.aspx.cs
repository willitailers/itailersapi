using BLL;
using KL_Vendas.controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KL_Vendas
{
    public partial class SmallBusinessSecurity : System.Web.UI.Page
    {
        public string EmailRepresentante { get; set; }
        public string Token { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["Token"] = GetParam("nr_token");
                CarregarProdutos();

                this.EmailRepresentante = "roldig@gmail.com";
            }
            this.Token = GetParam("nr_token");
        }

        private void CarregarProdutos()
        {
            int count = 0;
            int id_tipo_produto = 3; //Kaspersky Small Office Security

            HomeSecurity_BLL business = new HomeSecurity_BLL();

            List<Objetos.Produto> produtos = business.carregar_produtos(id_tipo_produto.ToString(), "0", Token);

            foreach (var produto in produtos)
            {
                foreach(var sub_produto in produto.Produto_Subs)
                {
                    controls.BusinessProduct product_control = (controls.BusinessProduct)LoadControl("~/controls/BusinessProduct.ascx");
                    product_control.ID = string.Format("ctrl_product{0}", count.ToString());
                    product_control.SubProdutoId = sub_produto.id_produto_sub.ToString();
                    product_control.CdSubProduto = sub_produto.cd_produto_sub_publico;
                    product_control.NomeProduto = produto.nm_produto;

                    product_control.DescricaoProduto = sub_produto.nm_produto_sub_descr_completa.Split('|');
                    product_control.ValorProduto = sub_produto.vl_produto_sub;
                    product_control.ValorDesconto = sub_produto.vl_produto_sub_desc;

                    divProducts.Controls.Add(product_control);

                    count++;
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