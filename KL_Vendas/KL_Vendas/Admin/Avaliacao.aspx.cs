using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Objetos;

namespace KL_Vendas.Admin
{
    public partial class Avalicacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carrega_avaliacoes();
            }
        }

        protected void carrega_avaliacoes()
        {
            DivErro.Visible = false;
            DataTable dt = new DataTable();
            Avaliacao aval = new Avaliacao() { tp_acao = 1, id_avaliacao = 0, nm_avaliacao = "", nm_titulo = "", nm_pagina = ddlPagina.SelectedItem.Value };

            dt = new Avaliacao_BLL().avaliacao_selecionar(aval);

            grdAvaliacao.DataSource = dt;
            grdAvaliacao.DataBind();

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            carrega_avaliacoes();
        }

        protected void btnProdutoSubIncluir_Click(object sender, EventArgs e)
        {
            if (ddlPagina.SelectedIndex <= 0)
            {
                DivErro.Visible = true;
                lblErro.Text = "Favor selecionar a tela";
                return;
            }
            else
                DivErro.Visible = false;
            
            DataTable dt = new DataTable();
            Avaliacao aval = new Avaliacao() { tp_acao = 2, id_avaliacao = 0, nm_avaliacao = txtAvaliacao.Text, nm_titulo = txtTitulo.Text, nm_pagina = ddlPagina.SelectedItem.Value };

            dt = new Avaliacao_BLL().avaliacao_selecionar(aval);

            txtTitulo.Text = "";
            txtAvaliacao.Text = "";

            carrega_avaliacoes();
        }

        protected void grdAvaliacao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Get the last name of the selected author from the appropriate
                // cell in the GridView control.
                GridViewRow selectedRow = grdAvaliacao.Rows[index];
                TableCell Area = selectedRow.Cells[0];
                string id = Area.Text;

                Avaliacao aval = new Avaliacao() { tp_acao = 3, id_avaliacao = int.Parse(id), nm_avaliacao = "", nm_titulo = "", nm_pagina = "" };

                new Avaliacao_BLL().avaliacao_selecionar(aval);

                carrega_avaliacoes();

            }
        }
    }
}