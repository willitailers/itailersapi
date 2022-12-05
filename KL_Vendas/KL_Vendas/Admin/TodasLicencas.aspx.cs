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
    public partial class TodasLicencas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                

                DateTime dt_one = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                txtDt_Inicial.Text = dt_one.ToString("dd/MM/yyyy");
                txtDt_Final.Text = DateTime.Now.ToString("dd/MM/yyyy");

                carrega_produto();
                carrega_produto_sub();
                CarregaGrid();
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
                    grdPagamentos.Columns[8].Visible = true;
                    grdPagamentos.Columns[7].Visible = true;
                }
                else
                {
                    grdPagamentos.Columns[8].Visible = false;
                    grdPagamentos.Columns[7].Visible = false;
                }
            }
            catch(Exception ex)
            {

            }
         }

        protected void CarregaGrid()
        {
            try
            {
                divErro.Visible = false;
                DataTable dt = new DataTable();

                dt = new Compra_BLL().licencas_consulta(DateTime.Parse(Generico.formata_data(txtDt_Inicial.Text)), 
                                                    DateTime.Parse(Generico.formata_data(txtDt_Final.Text)).AddDays(1).AddMinutes(-1), 
                                                    int.Parse(ddlProduto.SelectedItem.Value),
                                                    int.Parse(ddlSubProduto.SelectedItem.Value),
                                                    txtEmail_cliente.Text,
                                                    ddlStatus.SelectedItem.Value);

                grdPagamentos.DataSource = dt;
                grdPagamentos.DataBind();

                if (ddlStatus.SelectedItem.Value == "1")
                {
                    grdPagamentos.Columns[8].Visible = false;
                    grdPagamentos.Columns[7].Visible = false;
                }
                else
                {
                    grdPagamentos.Columns[8].Visible = true;
                    grdPagamentos.Columns[7].Visible = true;
                }
            }
            catch
            {
                divErro.Visible = true;
            }

        }

        public void carrega_produto()
        {
            ddlProduto.Items.Clear();
            ddlProduto.DataSource = new Produto_BLL().produto_selecionar(new Objetos.Produto() { id_produto = 0, id_fabricante = 0, nm_produto = "" });
            ddlProduto.DataBind();
            ddlProduto.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlProduto.SelectedIndex = 0;
        }

        public void carrega_produto_sub()
        {
            DataTable Dt = new DataTable();

            Dt = new Produto_BLL().produto_sub_selecionar(new Objetos.Produto_Sub() { id_produto_sub = 0, id_produto = int.Parse(ddlProduto.SelectedItem.Value) });

            ddlSubProduto.Items.Clear();
            ddlSubProduto.DataSource = Dt;
            ddlSubProduto.DataBind();
            ddlSubProduto.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void ddlProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            carrega_produto_sub();
        }

        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            CarregaGrid();
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

                int index = Convert.ToInt32(e.CommandArgument);

                // Get the last name of the selected author from the appropriate
                // cell in the GridView control.
                GridViewRow selectedRow = grdPagamentos.Rows[index];
                TableCell Area = selectedRow.Cells[0];

                Generico.log_inserir(19, "Tela de licença: Comando enviado " + e.CommandName + " - " + Area.Text, usuario.id_usuario);

                mostra_erro(false, false, "");

                if (e.CommandName == "Recusa")
                {
                    // Convert the row index stored in the CommandArgument
                    // property to an Integer.
                   
                    string id = Area.Text;

                    DataTable dt = new DataTable();

                    dt = new Pagamentos_BLL().pagamento_consulta_id(id);

                    if (dt.Rows.Count > 0)
                    {
                        new Carrinho_DAL().assinatura_cancelar(dt.Rows[0]["id_compra"].ToString(), usuario.id_usuario.ToString());

                        //new KL_Conexao().Cancelamento(dt.Rows[0]["id_compra"].ToString(), dt.Rows[0]["id_inscricao"].ToString());

                        //// cancelar pagamento na pagar.me
                        //new PagarMe_BLL().assinatura_cancelar(dt.Rows[0]["subscribe_id"].ToString());

                        // envia email com o cd ativação
                        string email = new Email().assinatura_cancelada(dt.Rows[0]["nm_cliente"].ToString(), true);

                        new Email().EnviaEmail(dt.Rows[0]["nm_email"].ToString(), "", email, new Comum().email_titulo_pedido_recusado);

                        mostra_erro(false, true, "Assinatura Cancelada nr: " + dt.Rows[0]["id_compra"].ToString() + " - Cliente: " + dt.Rows[0]["nm_cliente"].ToString() + " - " + dt.Rows[0]["nm_email"].ToString());
                    }
                    else
                    {
                        mostra_erro(true, false, "Assinatura não identificada");
                    }

                }

                if (e.CommandName == "Reenviar")
                {
                    // Convert the row index stored in the CommandArgument
                    // property to an Integer.
                    //int index = Convert.ToInt32(e.CommandArgument);

                    //// Get the last name of the selected author from the appropriate
                    //// cell in the GridView control.
                    //GridViewRow selectedRow = grdPagamentos.Rows[index];
                    //TableCell Area = selectedRow.Cells[0];
                    string id = Area.Text;

                    DataTable dt = new DataTable();

                    dt = new Pagamentos_BLL().pagamento_consulta_id(id);

                    if (dt.Rows.Count > 0)
                    {
                        DAL.Email email = new Email();
                        string mensagem = email.ativar_licenca(dt.Rows[0]["nm_cliente"].ToString(), dt.Rows[0]["link_downaload"].ToString(), dt.Rows[0]["cd_ativacao"].ToString(), true);

                        string ret = email.EnviaEmail(dt.Rows[0]["nm_email"].ToString(), "", mensagem, new Comum().email_titulo_ativar_licenca);

                        if (string.IsNullOrEmpty(ret))
                            mostra_erro(false, true, "E-mail de ativação reenviado para: " + dt.Rows[0]["id_compra"].ToString() + " - Cliente: " + dt.Rows[0]["nm_email"].ToString());
                        else
                            mostra_erro(true, false, "E-mail não enviado. " + ret);


                    }
                    else
                    {
                        mostra_erro(true, false, "Assinatura não identificada");
                    }

                }


                CarregaGrid();
            }
            catch (Exception ex)
            {
                mostra_erro(true, false, "Erro: " + ex.Message);
            }
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