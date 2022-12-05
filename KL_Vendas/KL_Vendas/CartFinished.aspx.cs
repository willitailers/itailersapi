using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAL;
using Objetos;
using BLL;

namespace KL_Vendas
{
    public partial class CartFinished : System.Web.UI.Page
    {
        protected string nm_tela = "Pedido | ";
        public string nm_produto { set; get; }
        public string link_produto { set; get; }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string dv_envio_email = "0", id_inscricao = "", id_carrinho = "", nome_cliente = "", email_cliente = "", id_compra = "";
                Decimal vl_compra = 0;
                string qtd_produto = "";

                if (Session["Pedido"] == null)
                {
                    if (Request.QueryString["sbi"] == null)
                    {
                        Response.Redirect(Generico.paginaInicial, false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    }
                    else
                    {
                        id_inscricao = Request.QueryString["sbi"].ToString();
                    }
                }
                else
                {
                    Carrinho carrinho = new Carrinho();
                    carrinho = (Carrinho)Session["Pedido"];

                    id_carrinho = carrinho.id_carrinho.ToString();

                    if (Request.QueryString["sbi"] != null)
                    {
                        id_inscricao = Request.QueryString["sbi"].ToString();
                    }

                }

                DataTable dt = new DataTable();
                dt = new Carrinho_DAL().pedido_selecionar("0", id_carrinho, id_inscricao);

                if (dt.Rows.Count > 0)
                {
                    //id_cliente = dt.Rows[0]["id_cliente"].ToString();
                    id_carrinho = id_carrinho != "0" ? id_carrinho : dt.Rows[0]["id_carrinho"].ToString();
                    id_inscricao = id_inscricao != "" ? id_inscricao : dt.Rows[0]["id_inscricao"].ToString();
                    //cd_ativacao = dt.Rows[0]["cd_ativacao"].ToString();
                    lblPedidoID.Text = dt.Rows[0]["id_compra"].ToString();
                    lblPedidoData.Text = DateTime.Parse(dt.Rows[0]["dt_compra"].ToString()).ToString("dd/MM/yyyy HH:mm");
                    txtNmEmail.Text = dt.Rows[0]["nm_email"].ToString();
                    nm_email.Text = dt.Rows[0]["nm_email"].ToString();
                    email_cliente = dt.Rows[0]["nm_email"].ToString();
                    id_compra = dt.Rows[0]["id_compra"].ToString();
                    vl_compra = Decimal.Parse(dt.Rows[0]["vl_compra"].ToString());
                    lblProduto.Text = dt.Rows[0]["nm_produto"].ToString() + (dt.Rows[0]["nm_produto"].ToString() == "1" ? " + " + dt.Rows[0]["nm_produto_sub_vinculado"].ToString() : "");
                    link_produto = dt.Rows[0]["link_downaload"].ToString();
                    nm_link.HRef = link_produto;
                    nm_cliente.Text = dt.Rows[0]["nm_cliente"].ToString();
                    nome_cliente = dt.Rows[0]["nm_cliente"].ToString();
                    lblProdutoSub.Text = dt.Rows[0]["nm_produto_sub"].ToString();
                    qtd_produto = dt.Rows[0]["qtd_produto"].ToString();
                    //cd_produto = dt.Rows[0]["cd_produto"].ToString();
                    dv_envio_email = dt.Rows[0]["dv_envio_email"].ToString();
                }
                else
                {
                    Session["Pedido"] = null;
                    Response.Redirect(Generico.paginaInicial, false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                if (dv_envio_email == "0")
                {
                    DAL.Email email = new Email();

                    string body = email.pedido_recebido(nome_cliente, lblPedidoID.Text, lblPedidoData.Text, true);
                    if (!string.IsNullOrEmpty(body))
                    {
                        email.EnviaEmail(email_cliente, "", body, new Comum().email_titulo_pedido_recebido);
                    }

                    body = email.pedido_analise(nome_cliente, lblPedidoID.Text, lblPedidoData.Text, true);
                    if (!string.IsNullOrEmpty(body))
                    {
                        email.EnviaEmail(email_cliente, "", body, new Comum().email_titulo_pagamento_analise);
                    }

                    new Carrinho_DAL().compra_atualiza_envio_email(id_compra);
                }

            }
            catch (Exception ex)
            {
                Generico.log_inserir(1, nm_tela + " - Page_Load | " + ex.Message, 0);
                Session["Pedido"] = null;
                Response.Redirect(Generico.paginaErro, false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}