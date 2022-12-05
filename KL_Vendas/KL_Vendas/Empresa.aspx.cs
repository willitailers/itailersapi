using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KL_Vendas
{
    public partial class Business : System.Web.UI.Page
    {
        public string Token { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["Token"] = GetParam("nr_token");
                this.Token = GetParam("nr_token");

                CarregarProdutos();
                CarregarProdutosDetalhe();
            }
            
        }

        protected void ddlProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarProdutosDetalhe();
        }

        protected void btn_comprar_Click(object sender, EventArgs e)
        {
            Response.Redirect("meu-carrinho?nr_token=" + (string.IsNullOrEmpty(this.Token) ? "-" : this.Token) + "&cdp=" + cpd.Text);

        }

        private void CarregarProdutos()
        {
            int id_tipo_produto = 3; //Kaspersky Small Office Security

            HomeSecurity_BLL business = new HomeSecurity_BLL();
            List<Objetos.Produto> produtos = new List<Objetos.Produto>();
            produtos = business.carregar_produtos(id_tipo_produto.ToString(), "0", Token);

            ddl_produtos.DataSource = produtos[0].Produto_Subs;
            ddl_produtos.DataBind();
        }

        private void CarregarProdutosDetalhe()
        {
            string id_produto_sub = ddl_produtos.SelectedItem.Value;
            HomeSecurity_BLL business = new HomeSecurity_BLL();
            DataTable dt_sub = new DataTable();

            dt_sub = business.carregar_sub_produtos("0", id_produto_sub);

            if (dt_sub.Rows.Count > 0)
            {
                lbl_dtl_produto.Text = "<span>" + dt_sub.Rows[0]["nm_produto_sub_descr_completa"].ToString().Replace("|", "</span><span>") + "</span>";

                lbl_valor_produto.Text = dt_sub.Rows[0]["vl_produto_sub"].ToString().Replace(".", ",");
                lbl_desconto.Text = dt_sub.Rows[0]["vl_desconto"].ToString();
                lbl_valor_desconto.Text = dt_sub.Rows[0]["vl_produto_sub_desc"].ToString();
                DivDesconto.Visible = dt_sub.Rows[0]["dv_desconto"].ToString() == "1";
                cpd.Text = dt_sub.Rows[0]["cd_produto_sub_publico"].ToString();
                DivEconomize.Visible = Decimal.Parse(dt_sub.Rows[0]["vl_produto_sub_desc"].ToString()) > 0;

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