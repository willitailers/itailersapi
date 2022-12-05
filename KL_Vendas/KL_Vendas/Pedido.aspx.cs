using Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DAL;

namespace KL_Vendas
{
    public partial class Pedido : System.Web.UI.Page
    {
        protected string nm_tela = "Pedido | ";
        public string nm_produto { set; get; }
        public string link_produto { set; get; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string id_carrinho = "0", id_inscricao = "", id_cliente = "0", cd_ativacao = "-", nm_email = "", id_compra = "", nm_cliente = "", nm_produto_sub = "", cd_produto = "", dv_envio_email = "0";
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
                    id_cliente = dt.Rows[0]["id_cliente"].ToString();
                    id_carrinho = id_carrinho != "0" ? id_carrinho : dt.Rows[0]["id_carrinho"].ToString();
                    id_inscricao = id_inscricao != "" ? id_inscricao : dt.Rows[0]["id_inscricao"].ToString();
                    cd_ativacao = dt.Rows[0]["cd_ativacao"].ToString();
                    lblPedidoID.Text = dt.Rows[0]["id_compra"].ToString();
                    lblPedidoData.Text = DateTime.Parse(dt.Rows[0]["dt_compra"].ToString()).ToString("dd/MM/yyyy HH:mm");
                    nm_email = dt.Rows[0]["nm_email"].ToString();
                    id_compra = dt.Rows[0]["id_compra"].ToString();
                    vl_compra = Decimal.Parse(dt.Rows[0]["vl_compra"].ToString());
                    nm_produto = dt.Rows[0]["nm_produto"].ToString();
                    link_produto = dt.Rows[0]["link_downaload"].ToString();
                    nm_cliente = dt.Rows[0]["nm_cliente"].ToString();
                    nm_produto_sub = dt.Rows[0]["nm_produto_sub"].ToString();
                    qtd_produto = dt.Rows[0]["qtd_produto"].ToString();
                    cd_produto = dt.Rows[0]["cd_produto"].ToString();
                    dv_envio_email = dt.Rows[0]["dv_envio_email"].ToString();
                }
                else
                {
                    Session["Pedido"] = null;
                    Response.Redirect(Generico.paginaInicial, false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                StringBuilder str = new StringBuilder();
                foreach(DataRow dr in dt.Rows)
                {
                    str.AppendLine("<div class=\"dr_productName\">");
                    str.AppendLine("<div class=\"tyProdImage col-xs-3 col-sm-2 col-md-1\">");
                    str.AppendLine("<img class=\"dr_productThumbnail\" src=\"" + dr["nm_caminho_img"].ToString() + "\" alt=\"" + dr["nm_produto"].ToString() + "\"/>");
                    str.AppendLine("</div>");
                    str.AppendLine("<div class=\"tyProdInfo col-xs-9 col-sm-10 col-md-11\">");
                    str.AppendLine("<span class=\"dr_TYPProdName\">" + dr["nm_produto"].ToString() + "</span>");
                    str.AppendLine("<br/>");
                    str.AppendLine("<span class=\"dr_platform\"></span>");
                    str.AppendLine("<br/>");
                    str.AppendLine("<span class=\"dr_autoRenew arText\">" + dr["nm_produto_sub_descr_curta"].ToString() + "</span>");
                    str.AppendLine("<br/>");
                    str.AppendLine("<span class=\"dr_deliveryDigital\">Transferência de arquivos (Download)</span>");
                    str.AppendLine("<br/>");
                    str.AppendLine("<span class=\"qty\">Quantidade: " + dr["qtd_produto"].ToString() + "</span>");
                    str.AppendLine("<span class=\"dr_License grey\">" + dr["nm_produto_sub"].ToString() + "</span>");
                    str.AppendLine("</div>");
                    str.AppendLine("</div>");

                }

                lblprodutos.Text = str.ToString();


                if (cd_ativacao == "-")
                {
                    msgCompra.Text = "Seu pedido foi realizado. Assim que a operadora de cartão aprovar o seu pagamento, iremos lhe enviar o código de ativação do seu produto no e-mail cadastrado: <strong>" + nm_email + "</strong>. Caso este e-mail não seja o correto, favor entrar com contato com falecom@antiviruslinktel.com.br.<br><br>";
                    msgCompra.Text += "Por Favor, anote o seu numero de pedido: <strong>" + id_compra + "</strong><br><br>";
                }
                else
                {
                    msgCompra.Text = "Seu pagamento foi aprovado. Abaixo esta o seu código de ativação. Nos tambem iremo envia-lo no e-mail cadastrado: " + nm_email + "<br><br>";
                    msgCompra.Text += "O codigo de ativação do seu produto é: <strong>" + cd_ativacao + "</strong><br><br>";
                }


                DataTable dt_cliente = new DataTable();
                dt_cliente = new Carrinho_DAL().cliente_selecionar(id_cliente);

                if (dt_cliente.Rows.Count > 0)
                {
                    lblNomeCliente.Text = dt_cliente.Rows[0]["nm_cliente"].ToString() + " " + dt_cliente.Rows[0]["nm_cliente_sobrenome"].ToString();
                    //lblEndereco.Text = dt_cliente.Rows[0]["endereco"].ToString();
                    //lblNumero.Text = dt_cliente.Rows[0]["endereco_nr"].ToString();
                    //lblBairro.Text = dt_cliente.Rows[0]["bairro"].ToString();
                    //lblCidade.Text = dt_cliente.Rows[0]["cidade"].ToString();
                    //lblEstado.Text = dt_cliente.Rows[0]["estado"].ToString();
                    //lblPais.Text = dt_cliente.Rows[0]["pais"].ToString();
                    lblEmail.Text = dt_cliente.Rows[0]["nm_email"].ToString();
                }

                StringBuilder strtag = new StringBuilder();

                //strtag.AppendLine("<script type=\"text/javascript\"> ");
                //strtag.AppendLine("var _st_account = 5423; ");
                //strtag.AppendLine("var _cv_data = { ");
                //strtag.AppendLine("'order_id': " + id_compra + ", ");
                //strtag.AppendLine("'valor': " + vl_compra + " ");
                //strtag.AppendLine("};");
                //strtag.AppendLine("(function () { ");
                //strtag.AppendLine("var ss = document.createElement('script'); ");
                //strtag.AppendLine("ss.type = 'text/javascript'; ");
                //strtag.AppendLine(" ss.async = true; ");
                //strtag.AppendLine("ss.src = '//app.shoptarget.com.br/js/tracking.js'; ");
                //strtag.AppendLine("var sc = document.getElementsByTagName('script')[0]; ");
                //strtag.AppendLine("sc.parentNode.insertBefore(ss, sc); ");
                //strtag.AppendLine("})(); </script>");

                strtag.AppendLine("<script> ");
                strtag.AppendLine("dataLayer.push({ ");
                strtag.AppendLine("'event': 'orderPlaced', ");
                strtag.AppendLine("'transactionId': '" + id_compra + "', ");
                strtag.AppendLine("'transactionAffiliation': 'LinkTel',");
                strtag.AppendLine("'transactionTotal': " + vl_compra + ", ");
                strtag.AppendLine("'transactionProducts': [{");
                strtag.AppendLine("'sku': '" + nm_produto + "', ");
                strtag.AppendLine("'name': '" + nm_produto + " - " + nm_produto_sub + "', "); 
                strtag.AppendLine("'category': '" + cd_produto + "', ");
                strtag.AppendLine(" 'price': " + vl_compra + ", ");
                strtag.AppendLine("'quantity': "+ qtd_produto +" ");
                strtag.AppendLine("}] ");
                strtag.AppendLine("}); ");
                strtag.AppendLine("</script>");

                lblTagConversao.Text = strtag.ToString();

                if (dv_envio_email == "0")
                {
                    DAL.Email email = new Email();

                    string body = email.pedido_recebido(nm_cliente, lblPedidoID.Text, lblPedidoData.Text, true);
                    if (!string.IsNullOrEmpty(body))
                    {
                        email.EnviaEmail(nm_email, "", body, new Comum().email_titulo_pedido_recebido);
                    }

                    body = email.pedido_analise(nm_cliente, lblPedidoID.Text, lblPedidoData.Text, true);
                    if (!string.IsNullOrEmpty(body))
                    {
                        email.EnviaEmail(nm_email, "", body, new Comum().email_titulo_pagamento_analise);
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