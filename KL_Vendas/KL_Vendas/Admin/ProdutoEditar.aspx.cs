using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace KL_Vendas.Admin
{
    public partial class ProdutoEditar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (Request.QueryString["id"] != null)
                {
                    lblIdProduto.Text = Request.QueryString["id"].ToString();

                    CarregaFabricante();
                    CarregaProduto(int.Parse(Request.QueryString["id"].ToString()), 0, "");
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

        protected void CarregaProduto(int id_produto, int id_fabricante, string nm_produto)
        {
            DataTable Dt = new DataTable();

            Dt = new Produto_BLL().produto_selecionar(new Objetos.Produto() { id_produto = id_produto, id_fabricante = id_fabricante, nm_produto = nm_produto });

            if (Dt.Rows.Count > 0)
            {
                txtProdutoLink.Text = Dt.Rows[0]["link_downaload"].ToString();
                txtProdutoNome.Text = Dt.Rows[0]["nm_produto"].ToString();
                ddlprodutoFabricante.SelectedIndex = new Comum().nivel_indice(int.Parse(Dt.Rows[0]["id_fabricante"].ToString()), ddlprodutoFabricante);
                txtProdutoDescrCompleta.Text = Dt.Rows[0]["nm_produto_descr_completa"].ToString();
                txtProdutoDescrCompleta2.Text = Dt.Rows[0]["nm_produto_descr_completa2"].ToString();
                txtProdutoDescrCurta.Text = Dt.Rows[0]["nm_produto_descr_curta"].ToString();
                cbProduto.Checked = Dt.Rows[0]["dv_ativo"].ToString() == "1";
                txtCdProduto.Text = Dt.Rows[0]["cd_produto"].ToString();

                txtProdutoLink.Enabled = true;
                txtProdutoNome.Enabled = true;
                ddlprodutoFabricante.Enabled = true;
                txtProdutoDescrCompleta.Enabled = true;
                txtProdutoDescrCompleta2.Enabled = true;
                txtProdutoDescrCurta.Enabled = true;
                txtCdProduto.Enabled = true;
                cbProduto.Enabled = true;

                btnProdutoIncluir.Enabled = true;
            }
            
        }

        protected void btnProdutoIncluir_Click(object sender, EventArgs e)
        {
            Objetos.Produto prod = new Objetos.Produto();

            prod.id_produto = int.Parse(lblIdProduto.Text);
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

            Response.Redirect("Produto.aspx");
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}