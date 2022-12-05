using BLL;
using Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KL_Vendas
{
    public partial class ContatoFabricante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaFabricante();
                CarregaFabricanteContato();
            }

        }

        protected void btnfabricanteContatoIncluir_Click(object sender, EventArgs e)
        {
            Fabricante_Contato fabricante = new Fabricante_Contato();

            fabricante.id_fabricante = int.Parse(ddlfabricanteContato.SelectedItem.Value);
            fabricante.nm_contato = txtFabricanteContatoNome.Text;
            fabricante.nm_observacao = txtFabricanteContatoObs.Text;
            fabricante.nm_ramal = txtFabricanteContatoRamal.Text;
            fabricante.nm_telefone = txtFabricanteContatoTelefone.Text;

            new Fabricante_BLL().fabricante_contato_inserir(fabricante);

            CarregaFabricanteContato();
        }

        protected void CarregaFabricanteContato()
        {
            DataTable Dt = new DataTable();

            Dt = new Fabricante_BLL().fabricante_contato_selecionar(new Objetos.Fabricante_Contato());

            grdFabricandoContato.DataSource = Dt;
            grdFabricandoContato.DataBind();


            txtFabricanteContatoNome.Text = "";
            txtFabricanteContatoObs.Text = "";
            ddlfabricanteContato.SelectedIndex = 0;
            txtFabricanteContatoRamal.Text = "";
            txtFabricanteContatoTelefone.Text = "";
        }

        protected void CarregaFabricante()
        {
            DataTable Dt = new DataTable();

            Dt = new Fabricante_BLL().fabricante_selecionar(new Objetos.Fabricante());

            ddlfabricanteContato.Items.Clear();
            ddlfabricanteContato.DataSource = Dt;
            ddlfabricanteContato.DataBind();

        }
    }
}