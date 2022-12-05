using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KL_Vendas
{
    public partial class Produto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaFabricante();
                CarregaProduto(0,0,"");
            }
        }

        protected void CarregaFabricante()
        {
            DataTable Dt = new DataTable();

            Dt = new Fabricante_BLL().fabricante_selecionar(new Objetos.Fabricante());

            ddlprodutoFabricante.Items.Clear();
            ddlprodutoFabricante.DataSource = Dt;
            ddlprodutoFabricante.DataBind();

        }


        protected void CarregaProduto(int id_produto, int id_fabricante, string nm_produto, bool limpadados = false)
        {
            DataTable Dt = new DataTable();

            Dt = new Produto_BLL().produto_selecionar(new Objetos.Produto() { id_produto = id_produto, id_fabricante = id_fabricante, nm_produto = nm_produto });

            grdProduto.DataSource = Dt;
            grdProduto.DataBind();

            if (limpadados)
            {
                txtProdutoLink.Text = "";
                txtProdutoNome.Text = "";
                ddlprodutoFabricante.SelectedIndex = 0;
                txtProdutoDescrCompleta.Text = "";
                txtProdutoDescrCompleta2.Text = "";
                txtProdutoDescrCurta.Text = "";
            }
        }

        protected void btnProdutoIncluir_Click(object sender, EventArgs e)
        {
            Objetos.Produto prod = new Objetos.Produto();

            prod.id_produto = 0;
            prod.id_fabricante = int.Parse(ddlprodutoFabricante.SelectedItem.Value);
            prod.link_downaload = txtProdutoLink.Text;
            prod.dv_ativo = cbProduto.Checked;
            prod.nm_produto = txtProdutoNome.Text;
            prod.nm_produto_descr_completa = txtProdutoDescrCompleta.Text;
            prod.nm_produto_descr_completa2 = txtProdutoDescrCompleta2.Text;
            prod.nm_produto_descr_curta = txtProdutoDescrCurta.Text;
            prod.dt_criacao = DateTime.Now;
            prod.id_usuario_criacao = 1;
            prod.dt_alteracao = DateTime.Now;
            prod.id_usuario_alteracao = 1;
            prod.cd_produto = txtCdProduto.Text;

            new Produto_BLL().produto_inserir(prod);

            CarregaProduto(0, int.Parse(ddlprodutoFabricante.SelectedItem.Value), "", true);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CarregaProduto(0, int.Parse(ddlprodutoFabricante.SelectedItem.Value), string.IsNullOrEmpty(txtProdutoNome.Text) ? "" : txtProdutoNome.Text, false);
        }
    }
}