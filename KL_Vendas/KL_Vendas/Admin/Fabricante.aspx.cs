using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Objetos;

namespace KL_Vendas
{
    public partial class Fabricante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaFabricante(0);
        }

        protected void CarregaFabricante( int id_fabricante)
        {
            DataTable Dt = new DataTable();

            Dt = new Fabricante_BLL().fabricante_selecionar(new Objetos.Fabricante() { id_fabricante = id_fabricante });

            grdFabricante.DataSource = Dt;
            grdFabricante.DataBind();

            //ddlFabricanteInfo.Items.Clear();
            //ddlFabricanteInfo.DataSource = Dt;
            //ddlFabricanteInfo.DataBind();

            //ddlfabricanteContato.Items.Clear();
            //ddlfabricanteContato.DataSource = Dt;
            //ddlfabricanteContato.DataBind();

            //ddlprodutoFabricante.Items.Clear();
            //ddlprodutoFabricante.DataSource = Dt;
            //ddlprodutoFabricante.DataBind();

            txtCEPFabricante.Text = "";
            txtCidadeFabricante.Text = "";
            txtCNPJFabricante.Text = "";
            txtEnderecoFabricante.Text = "";
            txtEstadoFabricante.Text = "";
            txtNomeFabricante.Text = "";

        }

        protected void btnFabricanteIncluir_Click(object sender, EventArgs e)
        {
            Objetos.Fabricante fabricante = new Objetos.Fabricante();

            fabricante.id_fabricante = 0;
            fabricante.cep = txtCEPFabricante.Text;
            fabricante.cidade = txtCidadeFabricante.Text;
            fabricante.cnpj = txtCNPJFabricante.Text;
            //fabricante.dt_alteracao = "";
            fabricante.dt_criacao = DateTime.Now;
            fabricante.id_usuario_criacao = 1;
            fabricante.dt_alteracao = DateTime.Now;
            fabricante.id_usuario_alteracao = 1;
            fabricante.nm_fabricante = txtNomeFabricante.Text;
            fabricante.endereco = txtEnderecoFabricante.Text;
            fabricante.estado = txtEstadoFabricante.Text;
            fabricante.dv_ativo = cbAtivoFabricante.Checked;

            new Fabricante_BLL().fabricante_inserir(fabricante);

            CarregaFabricante(0);

        }
    }
}