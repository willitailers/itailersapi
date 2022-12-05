using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DAL;

namespace KL_Vendas
{
    public partial class PagamentoPendente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime dt_one = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                txtDt_Inicial.Text = dt_one.ToString("dd/MM/yyyy");
                txtDt_Final.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CarregaPagamentos();
                habilitaBotoes();
            }
        }

        protected void habilitaBotoes()
        {
            try
            {
                Objetos.Usuario usuario = new Objetos.Usuario();
                usuario = (Objetos.Usuario)Session["user"];

                bool edita = Comum.EditaTela(usuario.menu, Request.Url.AbsoluteUri);

                if (edita)
                {
                    grdPagamentos.Columns[6].Visible = true;
                    grdPagamentos.Columns[7].Visible = true;
                }
                else
                {
                    grdPagamentos.Columns[6].Visible = false;
                    grdPagamentos.Columns[7].Visible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            CarregaPagamentos();
        }

        protected void CarregaPagamentos()
        {
            try
            {
                divErro.Visible = false;
                DataTable dt = new DataTable();

                dt = new Pagamentos_BLL().pagamento_consulta(DateTime.Parse(Generico.formata_data(txtDt_Inicial.Text)), DateTime.Parse(Generico.formata_data(txtDt_Final.Text)).AddDays(1).AddMinutes(-1), 1, txtEmail_cliente.Text);

                grdPagamentos.DataSource = dt;
                grdPagamentos.DataBind();
            }
            catch
            {
                divErro.Visible = true;
            }

        }

        protected void grdPagamentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Objetos.Usuario usuario = new Objetos.Usuario();

                if (Session["user"] == null)
                {
                    Response.Redirect("Login.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    return;
                }
                else
                {
                    usuario = (Objetos.Usuario)Session["user"];
                }

                string cd_ativacao = "";

                mostra_erro(false, false, "");

                if (e.CommandName == "Recusa")
                {
                    // Convert the row index stored in the CommandArgument
                    // property to an Integer.
                    int index = Convert.ToInt32(e.CommandArgument);

                    // Get the last name of the selected author from the appropriate
                    // cell in the GridView control.
                    GridViewRow selectedRow = grdPagamentos.Rows[index];
                    TableCell Area = selectedRow.Cells[0];
                    string id = Area.Text;

                    DataTable dt = new DataTable();

                    dt = new Pagamentos_BLL().pagamento_consulta_id(id);

                    if (dt.Rows.Count > 0)
                    {
                        new Carrinho_DAL().compra_pagamento_aceito(dt.Rows[0]["id_compra"].ToString(), "1", usuario.id_usuario.ToString());
                        //cd_ativacao = new KL_Conexao().ativar_compra(dt.Rows[0]["id_carrinho"].ToString());

                        // envia email com o cd ativação
                        string email = new Email().pedido_recusado(dt.Rows[0]["nm_cliente"].ToString(), dt.Rows[0]["id_compra"].ToString(), dt.Rows[0]["dt_compra"].ToString(), true);

                        new Email().EnviaEmail(dt.Rows[0]["nm_email"].ToString(), "", email, new Comum().email_titulo_pedido_recusado);

                        mostra_erro(false, true, "Pagamento recusado nr: " + dt.Rows[0]["id_compra"].ToString() + " - Cliente: " + dt.Rows[0]["nm_cliente"].ToString() + " - " + dt.Rows[0]["nm_email"].ToString());
                    }
                    else
                    {
                        mostra_erro(true, false, "Pagamento não identificado");
                    }

                }
                if (e.CommandName == "Aprova")
                {
                    // Convert the row index stored in the CommandArgument
                    // property to an Integer.
                    int index = Convert.ToInt32(e.CommandArgument);

                    // Get the last name of the selected author from the appropriate
                    // cell in the GridView control.
                    GridViewRow selectedRow = grdPagamentos.Rows[index];
                    TableCell Area = selectedRow.Cells[0];
                    string id = Area.Text;

                    DataTable dt = new DataTable();

                    dt = new Pagamentos_BLL().pagamento_consulta_id(id);

                    if (dt.Rows.Count > 0)
                    {
                        new Carrinho_DAL().compra_pagamento_aceito(dt.Rows[0]["id_compra"].ToString(), "0", usuario.id_usuario.ToString());

                        // atualiza cd ativação
                        if (dt.Rows[0]["cd_ativacao"].ToString() == "-")
                        {
                            cd_ativacao = new KL_Conexao().ativar_compra(dt.Rows[0]["id_carrinho"].ToString());
                        }
                        else
                            cd_ativacao = dt.Rows[0]["cd_ativacao"].ToString();



                        // cd_ativacao = new KL_Conexao().ativar_compra(dt.Rows[0]["id_carrinho"].ToString());
                        // envia email com o cd ativação
                        string email = new Email().pagamento_aprovado(dt.Rows[0]["nm_cliente"].ToString(),
                                                                        dt.Rows[0]["nm_caminho_img"].ToString(),
                                                                        dt.Rows[0]["nm_produto"].ToString(),
                                                                        dt.Rows[0]["qtd_licencas"].ToString(),
                                                                        cd_ativacao,
                                                                        dt.Rows[0]["nome"].ToString(),
                                                                        dt.Rows[0]["nm_email"].ToString(),
                                                                        true);

                        new Email().EnviaEmail(dt.Rows[0]["nm_email"].ToString(), "", email, new Comum().email_titulo_pagamento_aprovado);


                        if (dt.Rows[0]["dv_wifi"].ToString() == "1")
                        {
                            // gera senha
                            Random random = new Random();
                            int randomNumber = random.Next(111111, 999999);

                            // cadastra e-mail
                            LinkTel lkt = new LinkTel();
                            //DateTime dt_final = DateTime.Now.Date.AddMonths(1);
                            //var dateTimeOffset = new DateTimeOffset(dt_final);
                            //var unixDateTime = dateTimeOffset.ToUnixTimeSeconds();

                            var diffInSeconds = (DateTime.Now.Date.AddMonths(1) - DateTime.Now.Date).TotalSeconds;

                            bool acesso_lktt = lkt.Acesso_Wifi(dt.Rows[0]["nm_email"].ToString(),
                                          randomNumber.ToString(),
                                    dt.Rows[0]["nm_cliente"].ToString().Substring(0, dt.Rows[0]["nm_cliente"].ToString().IndexOf(" ")),
                                    dt.Rows[0]["nm_cliente"].ToString().Substring(dt.Rows[0]["nm_cliente"].ToString().IndexOf(" ") + 1),
                                    dt.Rows[0]["v_sexo"].ToString(),
                                    "Brasil",
                                    dt.Rows[0]["nr_cpf"].ToString(),
                                    "KasperskyComboBrasil", //dt.Rows[0]["nm_produto"].ToString(),
                                    DateTime.Parse(dt.Rows[0]["dt_nascimento"].ToString()).ToString("yyyy-MM-dd"),
                                    Int64.Parse(diffInSeconds.ToString().Replace(",","."))
                                );

                            if (acesso_lktt)
                            {
                                Generico.log_inserir(90, "Senha de e-mail gerada: " + dt.Rows[0]["nm_email"].ToString() + " - " + randomNumber.ToString(), 2);

                                // envia e-mail
                                email = new Email().wifi_senha(dt.Rows[0]["nm_cliente"].ToString(),
                                                                            dt.Rows[0]["nm_email"].ToString(), randomNumber.ToString(),
                                                                            true);

                                new Email().EnviaEmail(dt.Rows[0]["nm_email"].ToString(), "", email, new Comum().email_titulo_wifi_acesso);
                            }
                            else
                            {
                                Generico.log_inserir(90, "ACESSO NÃO GERADO!!! " + dt.Rows[0]["nm_email"].ToString() + " - " + randomNumber.ToString(), 2);
                            }
                            
                        }

                        mostra_erro(false, true, "Pagamento aprovado nr: " + dt.Rows[0]["id_compra"].ToString() + " - Cliente: " + dt.Rows[0]["nm_cliente"].ToString() + " - " + dt.Rows[0]["nm_email"].ToString());

                        if (cd_ativacao != "")
                        {
                            email = new Email().ativar_licenca( dt.Rows[0]["nm_cliente"].ToString(),
                                                                dt.Rows[0]["link_downaload"].ToString(),
                                                                cd_ativacao, true);

                            new Email().EnviaEmail(dt.Rows[0]["nm_email"].ToString(), "", email, new Comum().email_titulo_ativar_licenca);
                        }
                    }
                    else
                    {
                        mostra_erro(true, false, "Pagamento não identificado");
                    }

                }

                CarregaPagamentos();
            }
            catch (Exception ex)
            {
                mostra_erro(true, false, "Erro: " + ex.Message);
            }
        }

        private static double ConvertToTimestamp(DateTime value)
        {
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            //return the total seconds (which is a UNIX timestamp)
            return (double)span.TotalSeconds;
        }

        protected void mostra_erro(bool mostraErro, bool mostraSucesso, string mensagem)
        {
            divErro.Visible = mostraErro;
            lblErro.Text = mensagem;
            lblSucesso.Text = mensagem;
            div1.Visible = mostraSucesso;
        }
    }
}