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
    public partial class SubProduto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaProdutoSub();
            }
        }

        protected void btnProdutoSubIncluir_Click(object sender, EventArgs e)
        {
            Objetos.Produto_Sub prod = new Produto_Sub();

            prod.dt_alteracao = DateTime.Now;
            prod.dt_criacao = DateTime.Now;
            prod.dv_ativo = cbProdutoSub.Checked;
            prod.id_produto = int.Parse(ddlProduto.SelectedItem.Value);
            prod.id_tp_produto = int.Parse(ddlTipoProduto.SelectedItem.Value);
            prod.id_usuario_alteracao = 1;
            prod.id_usuario_criacao = 1;
            prod.nm_produto_sub = txtProdutoSub.Text;
            prod.nm_produto_sub_descr_completa = txtProdutoSubDescrCompleta.Text;
            prod.nm_produto_sub_descr_completa2 = txtProdutoSubDescrCompleta2.Text;
            prod.nm_produto_sub_descr_curta = txtProdutoSUbDescrCurta.Text;
            prod.vl_produto_sub = string.IsNullOrEmpty(txtValorSUb.Text) ? 0 : (decimal.Parse(txtValorSUb.Text.Replace(",","").Replace(".","")) / 100);
            prod.vl_produto_sub = string.IsNullOrEmpty(txtValorSUb.Text) ? 0 : (decimal.Parse(txtValorSUb.Text.Replace(",", "").Replace(".", "")) / 100);
            prod.dv_desconto = chkDesconto.Checked;
            prod.vl_produto_sub_desc = string.IsNullOrEmpty(txtValorDeProdutoSub.Text) ? 0 : (decimal.Parse(txtValorDeProdutoSub.Text.Replace(",", "").Replace(".", "")) / 100);
            prod.vl_desconto = string.IsNullOrEmpty(txtValorDesconto.Text) ? 0 : (decimal.Parse(txtValorDesconto.Text.Replace(",", "").Replace(".", "")) / 100);

            new Produto_BLL().produto_sub_inserir(prod);

            CarregaProdutoSub();

            return;

        }

        protected void CarregaProdutoSub()
        {
            DataTable Dt = new DataTable();

            Dt = new Produto_BLL().produto_sub_selecionar(new Objetos.Produto_Sub());

            grdProdutoSub.DataSource = Dt;
            grdProdutoSub.DataBind();

            ddlProduto.Items.Clear();
            ddlProduto.DataSource = new Produto_BLL().produto_selecionar(new Objetos.Produto() { id_produto = 0, id_fabricante = 0, nm_produto = "" });
            ddlProduto.DataBind();

            ddlTipoProduto.Items.Clear();
            ddlTipoProduto.DataSource = new Tipo_produto_BLL().tipo_pagamento_selecionar(new Objetos.Tipo_produto());
            ddlTipoProduto.DataBind();

            ddlTipoProduto.SelectedIndex = 0;
            ddlProduto.SelectedIndex = 0;
            txtProdutoSub.Text = "";
            txtProdutoSubDescrCompleta.Text = "";
            txtProdutoSubDescrCompleta2.Text = "";
            txtProdutoSUbDescrCurta.Text = "";
            txtValorSUb.Text = "";

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DataTable Dt = new DataTable();
            Dt = new Produto_BLL().produto_sub_selecionar(new Objetos.Produto_Sub() { id_produto = int.Parse(ddlProduto.SelectedItem.Value) });

            grdProdutoSub.DataSource = Dt;
            grdProdutoSub.DataBind();
        }
    }
}