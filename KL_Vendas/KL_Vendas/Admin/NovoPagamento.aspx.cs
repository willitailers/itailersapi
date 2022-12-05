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
    public partial class NovoPagamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime dt_one = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                txtDt_Inicial.Text = dt_one.ToString("dd/MM/yyyy");
                txtDt_Final.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CarregaPagamentos();
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

                dt = new Pagamentos_BLL().pagamento_consulta(DateTime.Parse(Generico.formata_data(txtDt_Inicial.Text)), DateTime.Parse(Generico.formata_data(txtDt_Final.Text)).AddDays(1).AddMinutes(-1), 2, txtEmail_cliente.Text);

                grdPagamentos.DataSource = dt;
                grdPagamentos.DataBind();
            }
            catch
            {
                divErro.Visible = true;
            }

        }
    }
}