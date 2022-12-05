using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using BLL;

namespace KL_Vendas.Admin
{
    public partial class FabricanteEditar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    lblIdFabricante.Text = Request.QueryString["id"].ToString();
                    CarregaFabricante(int.Parse(Request.QueryString["id"].ToString()));
                }
            }
        }

        protected void CarregaFabricante(int id_fabricante)
        {
            DataTable Dt = new DataTable();

            Dt = new Fabricante_BLL().fabricante_selecionar(new Objetos.Fabricante() { id_fabricante = id_fabricante });

            if (Dt.Rows.Count > 0)
            {
                txtCEPFabricante.Text = Dt.Rows[0]["cep"].ToString();
                txtCidadeFabricante.Text = Dt.Rows[0]["cidade"].ToString();
                txtCNPJFabricante.Text = Dt.Rows[0]["cnpj"].ToString();
                txtEnderecoFabricante.Text = Dt.Rows[0]["endereco"].ToString();
                txtEstadoFabricante.Text = Dt.Rows[0]["estado"].ToString();
                txtNomeFabricante.Text = Dt.Rows[0]["nm_fabricante"].ToString();
                cbAtivoFabricante.Checked = Dt.Rows[0]["dv_ativo"].ToString() == "1";

                txtCEPFabricante.Enabled = true;
                txtCidadeFabricante.Enabled = true;
                txtCNPJFabricante.Enabled = true;
                txtEnderecoFabricante.Enabled = true;
                txtEstadoFabricante.Enabled = true;
                txtNomeFabricante.Enabled = true;
                cbAtivoFabricante.Enabled = true;

                btnFabricanteIncluir.Enabled = true;
            }

        }

        protected void btnFabricanteIncluir_Click(object sender, EventArgs e)
        {
            Objetos.Fabricante fabricante = new Objetos.Fabricante();

            fabricante.id_fabricante = int.Parse(lblIdFabricante.Text);
            fabricante.cep = txtCEPFabricante.Text;
            fabricante.cidade = txtCidadeFabricante.Text;
            fabricante.cnpj = txtCNPJFabricante.Text;
            fabricante.dt_criacao = DateTime.Now;
            fabricante.id_usuario_criacao = 1;
            fabricante.dt_alteracao = DateTime.Now;
            fabricante.id_usuario_alteracao = 1;
            fabricante.nm_fabricante = txtNomeFabricante.Text;
            fabricante.endereco = txtEnderecoFabricante.Text;
            fabricante.estado = txtEstadoFabricante.Text;
            fabricante.dv_ativo = cbAtivoFabricante.Checked;

            new Fabricante_BLL().fabricante_inserir(fabricante);

            Response.Redirect("Fabricante.aspx");
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}