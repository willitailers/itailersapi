using System;
using System.Collections.Generic;
using System.Data;
using BLL;
using System.Web;
using System.Web.UI.WebControls;
using DAL;
using System.Globalization;

namespace KL_Vendas.Admin
{
    public partial class SubProdutoEditar : System.Web.UI.Page
    {
        protected string tela = "SubProdutoEditar";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (Request.QueryString["id"] != null)
                {
                    lblIdProdutoSub.Text = Request.QueryString["id"].ToString();
                    CarregaProdutoSub(int.Parse(Request.QueryString["id"].ToString()));
                }
        }

        public void carrega_parceiros()
        {
            DataTable dt = new DataTable();
            dt = new Parceiro_BLL().parceiro_selecionar("0");

            ddlParceiro.DataSource = dt;
            ddlParceiro.DataBind();
        }

        //public void carrega_lista_link()
        //{
        //    ddlUrl.Items.Add(new ListItem("HOME", new Constantes_DAL().home));
        //    ddlUrl.Items.Add(new ListItem("CASA", new Constantes_DAL().home_voce));
        //    ddlUrl.Items.Add(new ListItem("EMPRESA", new Constantes_DAL().home_empresa));
        //    ddlUrl.Items.Add(new ListItem("KAV", new Constantes_DAL().home_kav));
        //    ddlUrl.Items.Add(new ListItem("KIS", new Constantes_DAL().home_kts));
        //    ddlUrl.Items.Add(new ListItem("KTS", new Constantes_DAL().home_kis));
        //    ddlUrl.Items.Add(new ListItem("KISA", new Constantes_DAL().home_kisa));
        //    ddlUrl.Items.Add(new ListItem("KSK", new Constantes_DAL().home_ksk));

        //    ddlUrl.SelectedIndex = 0;
        //}

        protected void CarregaProdutoSub(int id_produto_sub)
        {
            DataTable Dt = new DataTable();

            Dt = new Produto_BLL().produto_sub_selecionar(new Objetos.Produto_Sub() { id_produto_sub = id_produto_sub, id_produto = 0 });

            if (Dt.Rows.Count > 0)
            {
                ddlProduto.Items.Clear();
                ddlProduto.DataSource = new Produto_BLL().produto_selecionar(new Objetos.Produto() { id_produto = 0, id_fabricante = 0, nm_produto = "" });
                ddlProduto.DataBind();

                ddlTipoProduto.Items.Clear();
                ddlTipoProduto.DataSource = new Tipo_produto_BLL().tipo_pagamento_selecionar(new Objetos.Tipo_produto());
                ddlTipoProduto.DataBind();

                ddlTipoProduto.SelectedIndex = new Comum().nivel_indice(int.Parse(Dt.Rows[0]["id_tp_produto"].ToString()), ddlTipoProduto); 
                ddlProduto.SelectedIndex = new Comum().nivel_indice(int.Parse(Dt.Rows[0]["id_produto"].ToString()), ddlProduto); ;
                txtProdutoSub.Text = Dt.Rows[0]["nm_produto_sub"].ToString();
                txtProdutoSubDescrCompleta.Text = Dt.Rows[0]["nm_produto_sub_descr_completa"].ToString();
                txtProdutoSubDescrCompleta2.Text = Dt.Rows[0]["nm_produto_sub_descr_completa2"].ToString();
                txtProdutoSUbDescrCurta.Text = Dt.Rows[0]["nm_produto_sub_descr_curta"].ToString();

                txtValorSUb.Text = Dt.Rows[0]["vl_produto_sub"].ToString();
                txtValorDeProdutoSub.Text = Dt.Rows[0]["vl_produto_sub_desc"].ToString();
                txtValorDesconto.Text = Dt.Rows[0]["vl_desconto"].ToString();
                lblCdp.Text = Dt.Rows[0]["cd_produto_sub_publico"].ToString();  
                cbProdutoSub.Checked = Dt.Rows[0]["dv_ativo"].ToString() == "1";
                chkDesconto.Checked = Dt.Rows[0]["dv_desconto"].ToString() == "1";

                btnProdutoSubIncluir.Enabled = true;
                btnCondicao.Enabled = true;

                ddlTipoProduto.Enabled = true; 
                ddlProduto.Enabled = true;
                txtProdutoSub.Enabled = true;
                txtProdutoSubDescrCompleta.Enabled = true;
                txtProdutoSubDescrCompleta2.Enabled = true;
                txtProdutoSUbDescrCurta.Enabled = true;
                txtValorSUb.Enabled = true;
                cbProdutoSub.Enabled = true;
            }

        }

        protected void btnProdutoSubIncluir_Click(object sender, EventArgs e)
        {
            Objetos.Produto_Sub prod = new Objetos.Produto_Sub();

            prod.id_produto_sub = int.Parse(lblIdProdutoSub.Text);
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
            prod.vl_produto_sub = decimal.Parse(txtValorSUb.Text.Replace(",", "").Replace(".", "")) / 100;
            prod.dv_desconto = chkDesconto.Checked;
            prod.vl_produto_sub_desc = decimal.Parse(txtValorDeProdutoSub.Text.Replace(",", "").Replace(".", "")) / 100;
            prod.vl_desconto = decimal.Parse(txtValorDesconto.Text.Replace(",", "").Replace(".", "")) / 100;

            new Produto_BLL().produto_sub_inserir(prod);

            Response.Redirect("SubProduto.aspx");
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        protected void btnCondicao_Click(object sender, EventArgs e)
        {
            carrega_parceiros();
            //carrega_lista_link();
            PnlCE.Visible = true;
            carrega_condicao_especial();
            carrega_token();
        }

        protected void carrega_condicao_especial()
        {
            Objetos.Produto_Sub prod = new Objetos.Produto_Sub();
            prod.id_produto_sub = int.Parse(lblIdProdutoSub.Text);
            prod.id_parceiro_token = 0;

            DataTable dt = new DataTable();
            dt = new Produto_BLL().produto_sub_especial_consulta(prod);

            grdCondicoes.DataSource = dt;
            grdCondicoes.DataBind();
        }

        protected void carrega_token()
        {
            DataTable dt = new DataTable();
            dt = new Produto_BLL().produto_sub_especial_token(0);

            ddlToken.DataSource = dt;
            ddlToken.DataBind();

            ddlToken.Items.Insert(0, new ListItem("Selecione um Token existente", "0"));


            ddlProdutoVinculado.DataSource = new Produto_BLL().produto_sub_todos();
            ddlProdutoVinculado.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Erro(false, "");
                // gravar token
                string token = new Parceiro_BLL().token_gerar();
                Objetos.Produto_Sub prod = new Objetos.Produto_Sub();

                Objetos.Parceiro parca = new Objetos.Parceiro()
                {
                    id_parceiro_token = int.Parse(ddlToken.SelectedItem.Value),
                    id_parceiro = int.Parse(ddlParceiro.SelectedItem.Value),
                    nr_token = token,
                    nm_link = "Preço Especial: " + txtNomeLink.Text,
                    url_amigavel = new Constantes_DAL().home + txtUrlAmigavel.Text,
                    url_acesso = new Constantes_DAL().home + @"/meu-carrinho?nr_token=" + token + "&cdp=" + lblCdp.Text
                };

                prod.parceiro = parca;

                prod.id_produto_sub = int.Parse(lblIdProdutoSub.Text);
                prod.vl_produto_sub = decimal.Parse(txtCEVl_Venda.Text.Replace(",", "."), CultureInfo.InvariantCulture);
                prod.nr_trial = int.Parse(txtTrial.Text);
                prod.dv_vinculo = chkVinculado.Checked ? 1 : 0;
                prod.id_produto_sub_vinculo = chkVinculado.Checked ? int.Parse(ddlProdutoVinculado.SelectedItem.Value) : 0;
                prod.vl_titular = string.IsNullOrEmpty(txtVlProdutoTitular.Text) ? 0 : decimal.Parse(txtVlProdutoTitular.Text.Replace(",", "."), CultureInfo.InvariantCulture);
                prod.vl_vinculado = string.IsNullOrEmpty(txtVlProdutoVinculado.Text) ? 0 : decimal.Parse(txtVlProdutoVinculado.Text.Replace(",", "."), CultureInfo.InvariantCulture);

                new Produto_BLL().produto_sub_especial_inserir(prod);

                // criar url

                Generico.gerar_url_amigavel("~/" + txtUrlAmigavel.Text, "~/Cart.aspx" + "?nr_token=" + token + "&cdp=" + lblCdp.Text);

                carrega_condicao_especial();

                txtCEVl_Venda.Text = "";
                txtTrial.Text = "";
                ddlParceiro.SelectedIndex = 0;
                txtNomeLink.Text = "";
                txtUrlAmigavel.Text = "";
                ddlProdutoVinculado.SelectedIndex = 0;
                txtVlProdutoTitular.Text = "";
                txtVlProdutoVinculado.Text = "";
            }
            catch(Exception ex)
            {
                Generico.log_inserir(5, tela + ".Button1_Click - " + ex.Message, 0);
                Erro(true, "Erro ao cadastrar Condição especial");
            }
            
            
        }

        protected void Erro(bool mostra, string texto)
        {
            divErro.Visible = mostra;
            lblErro.Text = texto;
        }

        protected void chkVinculado_CheckedChanged(object sender, EventArgs e)
        {
            ddlProdutoVinculado.Enabled = chkVinculado.Checked;
            txtVlProdutoTitular.Enabled = chkVinculado.Checked;
            txtVlProdutoVinculado.Enabled = chkVinculado.Checked;
        }
    }
}