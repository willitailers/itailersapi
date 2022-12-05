using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.KL_API;
using DAL;
using Objetos;

namespace KL_Vendas
{
    public partial class TesteEnvio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                KL_Conexao con = new KL_Conexao();
                SubscriptionResponseContainer container = new SubscriptionResponseContainer();
                container = con.Ativacao(txtTransactionId.Text, ddlProdutos.SelectedItem.Text, txtSubcribe.Text, "1");

                bool deuErro = false;

                foreach (object obj in container.Items)
                {
                    try
                    {
                        SubscriptionResponseItemCollection item = new SubscriptionResponseItemCollection();
                        item = (SubscriptionResponseItemCollection)obj;

                        SubscriptionResponseItemCollectionActivate ativo = new SubscriptionResponseItemCollectionActivate();

                        ativo = (SubscriptionResponseItemCollectionActivate)item.Items[0];

                        //ativo.ActivationCode
                        lblEmail.Text = ativo.ActivationCode;
                    }
                    catch (Exception ex)
                    {
                        deuErro = true;
                        lblEmail.Text = "Erro no primeiro try " + ex.Message;
                    }

                    if (deuErro)
                    {
                        try
                        {
                            SubscriptionResponseErrorCollection erro = new SubscriptionResponseErrorCollection();

                            erro = (SubscriptionResponseErrorCollection)obj;

                            lblEmail.Text = "erro no retorno do serviço " + erro.Items[0].ErrorMessage;

                        }
                        catch (Exception ex)
                        {
                            lblEmail.Text = "Erro no segundo try " + ex.Message;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                lblEmail.Text = "Erro geral " + ex.Message + " - " + ex.InnerException + " "  + HttpContext.Current.Server.MapPath("~/Email/Itailers.pfx");
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //DataTable dt_pagamento = new DataTable();
            //dt_pagamento = new Carrinho_DAL().compra_selecionar("132", "0");

            DAL.Email email = new Email();

            string mensagem = email.pedido_cancelado("Bruno", "132", "2019-03-21", true);

            //email.SendHtmlFormattedEmail("brunofgabriel@gmail.com", "Pedido Recebido", mensagem);
            email.EnviaEmail("brunofgabriel@gmail.com", "", mensagem, "PEDIDO CANCELADO");

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string passo = "", id_compra = "0", nm_plano = "", id_inscricao = "";
            int subscribe_id = 0;
            try
            {
                passo = "seleciona pedido";
                // consulta dados da compra
                DataTable dt = new DataTable();
                dt = new Carrinho_DAL().pedido_selecionar("0", lblCartdID.Text, "");

                DataTable dt_pagamento = new DataTable();
                dt_pagamento = new Carrinho_DAL().compra_selecionar("0", lblCartdID.Text);

                Cartao card = new Cartao();
                Cliente cli = new Cliente();

                if (dt.Rows.Count > 0)
                {

                    if (dt.Rows[0]["dv_enviado"].ToString() == "1")
                    {
                        Response.Redirect("Pedido.aspx?sid=" + dt.Rows[0]["id_inscricao"].ToString(), false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    }

                    passo = "Enviar pagamento";
                    // envia pagamento Pagar.me
                    id_inscricao = dt.Rows[0]["id_inscricao"].ToString();
                    nm_plano = dt.Rows[0]["nm_produto"].ToString();
                    cli.document_number = dt.Rows[0]["nr_cpf"].ToString();
                    cli.email = dt.Rows[0]["nm_email"].ToString();
                    cli.name = dt.Rows[0]["nm_cliente"].ToString() + " " + dt.Rows[0]["nm_cliente_sobrenome"].ToString();
                    cli.street = dt.Rows[0]["endereco"].ToString();
                    cli.street_number = dt.Rows[0]["endereco_nr"].ToString();
                    cli.zipcode = dt.Rows[0]["cep"].ToString();
                    cli.ddd = "11";
                    cli.phone = "24765820";
                    cli.Neighborhood = dt.Rows[0]["cidade"].ToString();


                }
                else
                {
                    passo = "pedido não encontrado. nr: " + lblCartdID.Text;
                    throw new Exception("Pedido não encontrado");
                }

                if (dt_pagamento.Rows.Count > 0)
                {
                    passo = "Enviar pagamento";
                    // envia pagamento Pagar.me
                    id_compra = dt_pagamento.Rows[0]["id_compra"].ToString();
                    card.card_cvv = dt_pagamento.Rows[0]["cvv"].ToString();
                    card.card_expiration_date = dt_pagamento.Rows[0]["dt_cartao"].ToString().Replace("/","");
                    card.card_holder_name = dt_pagamento.Rows[0]["nm_cartao"].ToString();
                    card.card_number = dt_pagamento.Rows[0]["nr_cartao"].ToString();
                    card.vl_compra = dt_pagamento.Rows[0]["vl_compra"].ToString().Replace(",", "").Replace(".", "");
                }
                else
                {
                    passo = "pedido não encontrado. nr: " + lblCartdID.Text;
                    throw new Exception("Pedido não encontrado");
                }

                AssinaturaRetorno retorno = new AssinaturaRetorno();

                //retorno = new PagarMe_BLL().gerar_pagamento_assinatura(cli, card, Int64.Parse(id_compra), nm_plano, 0);

                if (retorno.valido)
                {
                    Response.Redirect("Pedido.aspx?sid=" + dt.Rows[0]["id_inscricao"].ToString(), false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
                else
                {
                    Session["Pedido"] = null;
                    Response.Redirect(Generico.paginaErro, false);
                    Context.ApplicationInstance.CompleteRequest();
                }

            }
            catch (Exception ex)
            {
                //Generico.log_inserir(1, nm_tela + " - btnEnviar_Click | passo: " + passo + " - " + ex.Message, 0);
                //Session["Pedido"] = null;
                //Response.Redirect(Generico.paginaErro, false);
                //Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            try
            {
                DAL.Email email = new Email();
                string mensagem = "";

                switch (DropDownList1.SelectedItem.Value)
                {
                    case "1":
                        mensagem = email.pedido_recebido("Bruno", "132", "2019-03-21", true);
                        break;
                    case "2":
                        mensagem = email.pedido_cancelado("Bruno", "132", "2019-03-21", true);
                        break;
                    case "3":
                        mensagem = email.assinatura_cancelada("Bruno", true);
                        break;
                    case "4":
                        mensagem = email.pedido_analise("Bruno", "132", "2019-03-21", true);
                        break;
                    case "5":
                        mensagem = email.pedido_recusado("Bruno", "132", "2019-03-21", true);
                        break;
                    case "6":
                        mensagem = email.ativar_licenca("Bruno", "link", "1235456", true);
                        break;
                    case "7":
                        mensagem = email.pagamento_confirmado("Bruno", "LinkTel Antivirus", "4567", "6,66", "6,66", true);
                        break;
                }
                // string mensagem = email.pedido_cancelado("Bruno", "132", "2019-03-21");

                //email.SendHtmlFormattedEmail("brunofgabriel@gmail.com", "Pedido Recebido", mensagem);
                string ret = email.EnviaEmail(txtEmail.Text, "", mensagem, DropDownList1.SelectedItem.Text);

                if (string.IsNullOrEmpty(ret))
                    lblEmail.Text = "Email: " + DropDownList1.SelectedItem.Text + " enviado para " + txtEmail.Text + " as " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                else
                    lblEmail.Text = "** ERRO *** " + ret;
            }
            catch(Exception ex)
            {
                lblEmail.Text = "Erro: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " | " + ex.Message;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            // Create a request using a URL that can receive a post. 
            WebRequest request = WebRequest.Create("http://www.contoso.com/PostAccepter.aspx ");
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            string postData = "This is a test that posts this string to a Web server.";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
        }
    }
}