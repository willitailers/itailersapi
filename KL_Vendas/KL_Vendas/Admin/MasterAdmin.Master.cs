using System;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Objetos;
using BLL;

namespace KL_Vendas
{
    public partial class MasterAdmin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                Usuario user = new Usuario();
                user = (Usuario)Session["user"];

                Usuario_BLL bll = new Usuario_BLL();

                DataTable dt = new DataTable();
                dt = bll.monta_menu(user.id_usuario);

                bool primeiro = true, anterior_principal = false, anterior_sub = false;
                StringBuilder str = new StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {

                    if (dr["id_menu_pai"].ToString() == "0")
                    {
                        if (anterior_sub) // encera o sub
                        {
                            str.AppendLine("</ul>");
                        }

                        if (!primeiro)
                        {
                            if (anterior_principal)
                            {
                                str.AppendLine("</a>");
                            }

                            str.AppendLine("</li>");
                        }

                        if (dr["dv_filho"].ToString() == "0")
                        {
                            str.AppendFormat(primeiro ? "<li class=\"active\">" : "<li class=\"parent\">");
                            str.AppendLine("<a href=\"" + dr["nm_tela"].ToString() + "\" class=\"\">");
                            str.AppendLine("<em class=\"fa " + dr["nm_icone"].ToString() + "\">&nbsp;</em> " + dr["nm_menu"].ToString());
                        }
                        else
                        {
                            str.AppendFormat(primeiro ? "<li class=\"active\">" : "<li class=\"parent\">");
                            str.AppendLine("<a data-toggle=\"collapse\" href=\"#sub-item-" + dr["id_menu"].ToString() + "\">");
                            str.AppendLine("<em class=\"fa " + dr["nm_icone"].ToString() + "\">&nbsp;</em> " + dr["nm_menu"].ToString() + " <span data-toggle=\"collapse\" href=\"#sub-item-" + dr["id_menu"].ToString() + "\" class=\"icon pull-right\"><em class=\"fa fa-plus\"></em></span></a>");
                        }

                        


                        anterior_principal = true;
                        anterior_sub = false;
                    }
                    else
                    {
                        anterior_sub = true;

                        if (anterior_principal)
                        {
                            str.AppendLine("<ul class=\"children collapse\" id=\"sub-item-" + dr["id_menu_pai"].ToString() + "\">");
                            str.AppendLine("<li><a href=\"" + dr["nm_tela"].ToString() + "\">");
                            str.AppendLine("<span class=\"fa " + dr["nm_icone"].ToString() + "\">&nbsp;</span> " + dr["nm_menu"].ToString() );
                            str.AppendLine("</a></li>");
                        }
                        else
                        {
                            str.AppendLine("<li><a href=\"" + dr["nm_tela"].ToString() + "\">");
                            str.AppendLine("<span class=\"fa " + dr["nm_icone"].ToString() + "\">&nbsp;</span> " + dr["nm_menu"].ToString());
                            str.AppendLine("</a></li>");
                        }

                        anterior_principal = false;
                    }

                    primeiro = false;
                }

                if (anterior_sub) // encera o sub
                {
                    str.AppendLine("</ul>");
                }

                str.AppendLine("</a></li>");

                lblMenu.Text = str.ToString();

                lblNome.Text = user.nm_usuario_primeiro;
            }
        }
    }
}