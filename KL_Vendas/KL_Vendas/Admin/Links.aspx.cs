using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using DAL;

namespace KL_Vendas
{
    public partial class Links : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carrega_parceiros();
                carrega_lista_link();
                carrega_parceiros_token();
            }
        }

        public void carrega_parceiros()
        {
            DataTable dt = new DataTable();
            dt = new Parceiro_BLL().parceiro_selecionar("0");

            ddlParceiro.DataSource = dt;
            ddlParceiro.DataBind();
        }

        public void carrega_lista_link()
        {
            ddlUrl.Items.Add(new ListItem("HOME", new Constantes_DAL().home));
            ddlUrl.Items.Add(new ListItem("CASA", new Constantes_DAL().home_voce));
            ddlUrl.Items.Add(new ListItem("EMPRESA", new Constantes_DAL().home_empresa));
            ddlUrl.Items.Add(new ListItem("KAV", new Constantes_DAL().home_kav));
            ddlUrl.Items.Add(new ListItem("KIS", new Constantes_DAL().home_kts));
            ddlUrl.Items.Add(new ListItem("KTS", new Constantes_DAL().home_kis));
            ddlUrl.Items.Add(new ListItem("KISA", new Constantes_DAL().home_kisa));
            ddlUrl.Items.Add(new ListItem("KSK", new Constantes_DAL().home_ksk));

            ddlUrl.SelectedIndex = 0;
        }

        public void carrega_parceiros_token()
        {
            DataTable dt = new DataTable();
            dt = new Parceiro_BLL().parceiro_token_selecionar(new Objetos.Parceiro() { id_parceiro = 0, id_parceiro_token = 0 });

            grdProduto.DataSource = dt;
            grdProduto.DataBind();
        }

        protected void btnProdutoIncluir_Click(object sender, EventArgs e)
        {
            // gravar token
            string token = new Parceiro_BLL().token_gerar();
            Objetos.Usuario usuario = new Objetos.Usuario();
            usuario = (Objetos.Usuario)Session["user"]; 

            Objetos.Parceiro parca = new Objetos.Parceiro() {
                id_parceiro = int.Parse(ddlParceiro.SelectedItem.Value),
                nr_token = token,
                nm_link = txtNomeLink.Text,
                url_amigavel = new Constantes_DAL().home + txtUrlAmigavel.Text,
                url_acesso = ddlUrl.SelectedItem.Value + "?nr_token=" + token
            };

            new Parceiro_BLL().parceiro_token_incluir(parca);

            // criar url

            Generico.gerar_url_amigavel("~/" + txtUrlAmigavel.Text, (ddlUrl.SelectedItem.Text == "HOME" ? "~/Index.aspx" : ddlUrl.SelectedItem.Value) + "?nr_token=" + token);

            Session["user"] = usuario;
            carrega_parceiros_token();
        }
    }
}