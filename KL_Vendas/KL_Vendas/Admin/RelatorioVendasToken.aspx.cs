using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KL_Vendas.Admin
{
    public partial class RelatorioVendasToken : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime dt_one = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                txtDt_Inicial.Text = dt_one.ToString("dd/MM/yyyy");
                txtDt_Final.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            try
            {
                CarregaPagamentos();
            }
            catch(Exception ex)
            {
                Generico.log_inserir(-1, "Relatorio Token " + ex.Message, 0);
                divErro.Visible = true;
            }
        }

        protected void CarregaPagamentos()
        {
            try
            {
                divErro.Visible = false;
                DataTable dt = new DataTable();

                dt = new Pagamentos_BLL().relatorio_venda_token(DateTime.Parse(Generico.formata_data(txtDt_Inicial.Text)), DateTime.Parse(Generico.formata_data(txtDt_Final.Text)).AddDays(1).AddMinutes(-1), int.Parse(ddlStatus.SelectedItem.Value));

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