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
    public partial class ReenvioLicenca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt = new Pagamentos_BLL().pagamento_consulta_id(txtIdCompra.Text);
            if (dt.Rows.Count > 0)
            {
                string cd_ativacao = new KL_Conexao().ativar_compra2(dt.Rows[0]["id_carrinho"].ToString());

                lblMsg.Text = "codigo: " + cd_ativacao;

                if (cd_ativacao != "")
                {
                    string email = new Email().ativar_licenca(dt.Rows[0]["nm_cliente"].ToString(),
                                                        dt.Rows[0]["link_downaload"].ToString(),
                                                        cd_ativacao, true);

                    new Email().EnviaEmail(dt.Rows[0]["nm_email"].ToString(), "falecom@antiviruslinktel.com.br", email, new Comum().email_titulo_ativar_licenca);

                    txtIdCompra.Text = "";
                }
            }

        }
    }
}