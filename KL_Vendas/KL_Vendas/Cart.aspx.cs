using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using Objetos;
using BLL;
using DAL;
using System.Web.UI.WebControls;
using System.Globalization;

namespace KL_Vendas
{
    public partial class Cart : System.Web.UI.Page
    {
        protected string nm_tela = "Cart";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int i = 0; i <= 15; i++)
                {
                    int ano = DateTime.Now.Year;
                    ddlAnoCartao.Items.Add(new ListItem((ano + i).ToString(), (ano + i).ToString().Substring(2, 2)));
                }

                try
                {
                    string nr_token = "", cdp = "";

                    if (Request.QueryString["cdp"] == null)
                    {
                        Session["Pedido"] = null;

                        Response.Redirect(Generico.paginaInicial, false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        cdp = Request.QueryString["cdp"].ToString();
                        nr_token = Request.QueryString["nr_token"] == null ? "-" : Request.QueryString["nr_token"].ToString();
                        lblToken.Text = nr_token;
                        lblCdProduto.Text = cdp;
                        int trial = 0;

                        DataTable dt = new DataTable();

                        dt = new BLL.Carrinho_BLL().produto_selecionar(cdp, nr_token);

                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["ic_produto_vinculado"].ToString() == "0")
                            {
                                lblHeader.Text = "Meu Antivírus";
                                divEmailWifi.Visible = false;
                            }
                            else
                                lblHeader.Text = "Meu Combo";

                            trial = int.Parse(dt.Rows[0]["nr_trial"].ToString());
                            lblNmProduto.Text = dt.Rows[0]["nm_produto"].ToString();
                            lblNmProdutoSub.Text = dt.Rows[0]["nm_produto_sub"].ToString();
                            imgProduto.ImageUrl = dt.Rows[0]["nm_caminho_img"].ToString();
                            imgProduto.ToolTip = dt.Rows[0]["nm_produto"].ToString();
                            
                            //lblTotal.Text = dt.Rows[0]["vl_produto_sub"].ToString().Replace(".", ",");
                            //lblValorTotal.Text = dt.Rows[0]["vl_produto_sub"].ToString().Replace(".", ",");
                            
                            if (dt.Rows[0]["ic_produto_vinculado"].ToString() == "1")
                                lblNomeProdutoDetalhe.Text = dt.Rows[0]["nm_produto"].ToString() + " + " + dt.Rows[0]["nm_produto_sub_vinculado"].ToString();
                            else
                                lblNomeProdutoDetalhe.Text = dt.Rows[0]["nm_produto"].ToString();

                            lblIdProduto.Text = dt.Rows[0]["id_produto"].ToString();
                            lblIdProdutoSub.Text = dt.Rows[0]["id_produto_sub"].ToString();
                            lblProdutoSubDescCurta.Text = dt.Rows[0]["nm_produto_sub_descr_curta"].ToString();
                            //lblCdProduto.Text = dt.Rows[0]["id_tp_produto"].ToString();
                            lblValorProduto.Text = dt.Rows[0]["vl_produto_sub"].ToString().Replace(".", ",");

                            if (trial > 0)
                            {
                                aval_gratuita1.Visible = true;
                                lblNrTrial1.Text = trial.ToString();
                                lblValorGratuito.Text = "Avalie GRÁTIS por " + trial.ToString() + (trial == 1 ? " dia" : " dias");
                                lblValor.Text = "Depois R$ " + dt.Rows[0]["vl_produto_sub"].ToString().Replace(".", ",") + "/mês";
                                divAvisoTrial.Visible = true;
                                
                            }
                            else
                            {
                                aval_gratuita1.Visible = false;
                                lblValorGratuito.Text = "OFERTA! R$ " + dt.Rows[0]["vl_produto_sub"].ToString().Replace(".", ",") + "/mês";
                                lblValor.Text = "PREÇO EXCLUSIVO";
                                divAvisoTrial.Visible = false;
                            }
                            

                        }
                        else
                        {
                            nenhum_item();
                        }

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

        protected void nenhum_item()
        {
            Response.Redirect(Generico.paginaInicial, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            lblErro.Visible = false;
            string id_cliente = "0", id_compra = "", passo = "", nm_plano = "", id_inscricao = "";
            try
            {
                string dt_nascimento = formataData(txtDataNascimento.Text);
                DataTable dt = new DataTable();
                dt = new BLL.Carrinho_BLL().produto_selecionar(lblCdProduto.Text, lblToken.Text);

                if (dt.Rows.Count > 0)
                {
                    Carrinho carrinho = new Carrinho();
                    carrinho = new Carrinho_DAL().inserir_carrinho(lblToken.Text);

                    if (carrinho.id_carrinho > 0)
                    {
                        lblCartdID.Text = carrinho.id_carrinho.ToString();
                        new Carrinho_DAL().inserir_carrinho_item(carrinho.id_carrinho.ToString(), 
                            dt.Rows[0]["id_produto_sub"].ToString(), 
                            dt.Rows[0]["vl_produto_sub"].ToString().Replace(",", "."), 
                            "1", 
                            dt.Rows[0]["nr_trial"].ToString(), 
                            dt.Rows[0]["id_produto_sub_especial"].ToString(),
                            dt.Rows[0]["dv_wifi"].ToString());

                        CarrinhoItem itens = new CarrinhoItem()
                        {
                            id_produto = int.Parse(dt.Rows[0]["id_produto"].ToString()),
                            id_produto_sub = int.Parse(dt.Rows[0]["id_produto_sub"].ToString()),
                            nm_produto = dt.Rows[0]["nm_produto"].ToString(),
                            nm_produto_sub = dt.Rows[0]["nm_produto_sub"].ToString(),
                            quantidade_produto = 1,
                            vl_produto = dt.Rows[0]["vl_produto_sub"].ToString().Replace(".", ","),
                            nm_produto_sub_descr_curta = dt.Rows[0]["nm_produto_sub_descr_curta"].ToString(),
                            nm_caminho_img = dt.Rows[0]["nm_caminho_img"].ToString(),
                            nr_trial = int.Parse(dt.Rows[0]["nr_trial"].ToString())
                        };

                        carrinho.carrinhoItems = new List<CarrinhoItem>();
                        carrinho.carrinhoItems.Add(itens);

                        passo = "Inserir cliente";
                        // Inserir Cliente
                        id_cliente = new Carrinho_DAL().inserir_cliente("0",
                                                                        lblCartdID.Text,
                                                                        txtEmail.Text,
                                                                        txtNome.Text,
                                                                        "", // txtEndereco.Text,
                                                                        "", // txtCEP.Text,
                                                                        "", // txtCidade.Text,
                                                                        "", // ddlEstado.SelectedItem.Value,
                                                                        "",
                                                                        txtCPF.Text, //txtCPF.Text,
                                                                        "1", //(fisica.Checked ? "1" : "2"),
                                                                        "", //txtIncricaoEstadual.Text,
                                                                        "", // txtNumero.Text,
                                                                        "", // txtBairro.Text,
                                                                        "", // ddlPais.SelectedItem.Value,
                                                                        "", //txtCNPJ.Text,
                                                                        txtTelefone.Text,
                                                                        chkM.Checked || chkO.Checked ? "M" : "F",
                                                                        dt_nascimento);

                        DataTable dt_compra = new DataTable();
                        string novo = "0", id_carrinho = "";
                        dt_compra = new Carrinho_DAL().compra_inserir(carrinho.id_carrinho.ToString(),
                                                            valor_compra(carrinho).ToString().Replace(",", "."), "0",
                                                            carrinho.carrinhoItems[0].id_produto_sub.ToString(),
                                                            carrinho.carrinhoItems[0].id_produto.ToString(),
                                                            txtCPF.Text);

                        if (dt_compra.Rows.Count > 0)
                        {
                            id_compra = dt_compra.Rows[0]["retorno"].ToString();
                            novo = dt_compra.Rows[0]["novo"].ToString();
                            id_carrinho = dt_compra.Rows[0]["id_carrinho"].ToString();
                            id_inscricao = dt_compra.Rows[0]["id_inscricao"].ToString();
                        }
                        else
                        {
                            lblErro.Visible = true;
                            lblErro.Text = "Err o ao enviar o pagamento";
                            return;
                        }

                        if (novo == "0")
                        {
                            carrinho = null;
                            carrinho = new Carrinho_BLL().carrinho_selecionar("", id_carrinho);

                            Session["Pedido"] = carrinho;
                            Response.Redirect("CartFinished.aspx?sid=" + id_inscricao, false);
                            Context.ApplicationInstance.CompleteRequest();
                            return;
                        }

                        passo = "Inserir Dados Pagamento";
                        // inserir dados compra
                        new Carrinho_DAL().compra_dados_pagamento_inserir(id_compra, "1",
                                                                            txtNrCartao.Text,
                                                                            txtNomeCartao.Text,
                                                                            ddlMesCartao.SelectedItem.Value + "/" + ddlAnoCartao.SelectedItem.Value,
                                                                            "", "", "", "", ""
                                                                            , txtCVV.Text);

                        string id_subscricao = new Carrinho_DAL().compra_id_subscricao_seleciona(id_compra);

                        passo = "seleciona pedido";
                        // consulta dados da compra
                        DataTable dtp = new DataTable();
                        dtp = new Carrinho_DAL().pedido_selecionar("0", lblCartdID.Text, "");

                        DataTable dt_pagamento = new DataTable();
                        dt_pagamento = new Carrinho_DAL().compra_selecionar("0", lblCartdID.Text);

                        Cartao card = new Cartao();
                        Cliente cli = new Cliente();

                        if (dtp.Rows.Count > 0)
                        {

                            if (dtp.Rows[0]["dv_enviado"].ToString() == "1")
                            {
                                Response.Redirect("CartFinished.aspx?sid=" + dtp.Rows[0]["id_inscricao"].ToString(), false);
                                Context.ApplicationInstance.CompleteRequest();
                                return;
                            }

                            passo = "Enviar pagamento";
                            // envia pagamento Pagar.me
                            id_inscricao = dtp.Rows[0]["id_inscricao"].ToString();
                            nm_plano = dtp.Rows[0]["nm_produto"].ToString();
                            cli.document_number = dtp.Rows[0]["nr_cpf"].ToString();
                            cli.email = dtp.Rows[0]["nm_email"].ToString();
                            //cli.name = dtp.Rows[0]["id_tp_pessoa"].ToString() == "1" ? dtp.Rows[0]["nm_cliente"].ToString() + " " + dtp.Rows[0]["nm_cliente_sobrenome"].ToString() : dtp.Rows[0]["nm_razao_social"].ToString();
                            cli.name = dtp.Rows[0]["nm_cliente"].ToString();
                            cli.street = dtp.Rows[0]["endereco"].ToString();
                            cli.street_number = dtp.Rows[0]["endereco_nr"].ToString();
                            cli.zipcode = dtp.Rows[0]["cep"].ToString();
                            cli.ddd = dtp.Rows[0]["nr_telefone"].ToString().Replace("(", "").Replace(")", "").Substring(0, 2);
                            cli.phone = dtp.Rows[0]["nr_telefone"].ToString().Replace("(", "").Replace(")", "").Replace("-", "").Substring(2);
                            cli.Neighborhood = dtp.Rows[0]["cidade"].ToString();


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
                            card.card_expiration_date = dt_pagamento.Rows[0]["dt_cartao"].ToString().Replace("/", "");
                            card.card_holder_name = dt_pagamento.Rows[0]["nm_cartao"].ToString();
                            card.card_number = dt_pagamento.Rows[0]["nr_cartao"].ToString();
                            card.vl_compra = dt_pagamento.Rows[0]["vl_compra"].ToString().Replace(",", "").Replace(".", "");
                        }
                        else
                        {
                            passo = "pedido não encontrado. nr: " + lblCartdID.Text;
                            throw new Exception("Pedido não encontrado");
                        }

                        //if (System.Configuration.ConfigurationManager.AppSettings["ambiente_Producao"] == "0")
                        //{
                        //    Session["Pedido"] = carrinho;
                        //    Response.Redirect("CartFinished.aspx?sid=" + dtp.Rows[0]["id_inscricao"].ToString(), false);
                        //    Context.ApplicationInstance.CompleteRequest();
                        //    return;
                        //}

                        AssinaturaRetorno retorno = new AssinaturaRetorno();
                        //retorno.valido = true;

                        //retorno = new PagarMe_BLL().gerar_pagamento_assinatura(cli, card, Int64.Parse(id_compra), nm_plano, int.Parse(dtp.Rows[0]["nr_trial"].ToString()));

                        if (retorno.valido)
                        {
                            Session["Pedido"] = carrinho;
                            Response.Redirect("CartFinished.aspx?sid=" + dtp.Rows[0]["id_inscricao"].ToString(), false);
                            Context.ApplicationInstance.CompleteRequest();
                            return;
                        }
                        else
                        {
                            lblErro.Visible = true;
                            lblErro.Text = retorno.msgErro;
                        }

                        
                    }
                }
                else
                {
                    lblErro.Visible = true;
                    lblErro.Text = "Produto não disponível";
                }
            }
            catch (Exception ex)
            {
                Generico.log_inserir(1, nm_tela + " - btnContinuar_Click | " + ex.Message, 0);
                Session["Pedido"] = null;
                Response.Redirect(Generico.paginaInicial, false);
                Context.ApplicationInstance.CompleteRequest();
            }

        }

        protected string formataData(string data)
        {
            CultureInfo enUS = new CultureInfo("en-US");
            CultureInfo enbr = new CultureInfo("pt-BR");
            string datafinal = "";
            // formata dd/mm/aaaa ou com -
            DateTime dtnascimento = new DateTime();

            if (DateTime.TryParseExact(data.Replace("/","").Replace("-",""), "ddMMyyyy", enbr, DateTimeStyles.None, out dtnascimento))
            {
                datafinal = dtnascimento.ToString("yyyyMMdd");
            }

            // formatada aaaa/mm/dd ou com -
            if (DateTime.TryParseExact(data.Replace("/", "").Replace("-", ""), "yyyyMMdd", enUS, DateTimeStyles.None, out dtnascimento))
            {
                datafinal = dtnascimento.ToString("yyyyMMdd");
            }

            return datafinal;
        }

        protected decimal valor_compra(Carrinho carrinho)
        {
            decimal valor = 0;
            foreach (CarrinhoItem car in carrinho.carrinhoItems)
            {
                valor += decimal.Parse(car.vl_produto.Replace(",", "."), NumberStyles.Currency, CultureInfo.InvariantCulture);
            }

            return valor;

        }

        protected void btnContinuar_Click1(object sender, EventArgs e)
        {
            DateTime dt = new DateTime();
            lblErro.Visible = false;
            if (!DateTime.TryParseExact(txtDataNascimento.Text, "dd/MM/yyyy", new CultureInfo("pt-BR"),
                                 DateTimeStyles.None, out dt ))
            {
                lblErro.Text = "Preencha a data de nascimento corretamente ";
                lblErro.Visible = true;
                return;
            }

            if (!chkF.Checked && !chkM.Checked && !chkO.Checked)
            {
                lblErro.Text = "Escolha a opção de sexo ";
                lblErro.Visible = true;
                return;
            }


            if (!txtNome.Text.Contains(" "))
            {
                lblErro.Visible = true;
                lblErro.Text = "Favor informar o nome completo";

                return;
            }

            btnContinuar.Visible = false;
            btnEnviar.Visible = true;
            pnlDados.Visible = false;
            pnlPagto.Visible = true;
            btnVoltar.Visible = true;
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            btnContinuar.Visible = true;
            btnEnviar.Visible = false;
            pnlDados.Visible = true;
            pnlPagto.Visible = false;
            btnVoltar.Visible = false;
        }

        /*

        protected void imgexcluir_Click(object sender, ImageClickEventArgs e)
        {
            Session["Pedido"] = null; lblCartdID.Text = "";
            string path = HttpContext.Current.Request.Url.AbsolutePath, cdp = Request.QueryString["cdp"].ToString();

            path = path.Replace(cdp, "").Replace("&cdp=", "") + "?nr_token=" + lblToken.Text;

            Response.Redirect(path, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            try
            {
                string URL = Generico.paginaProdutos + "?nr_token=" + lblToken.Text + "&product_type=1";

                Response.Redirect(URL, false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch
            {
                Response.Redirect(Generico.paginaInicial, false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
        */
    }
}