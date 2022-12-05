using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Objetos;
using BLL;


namespace KL_Vendas
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Objetos.Login login = new Objetos.Login();
                Objetos.Usuario usuario = new Objetos.Usuario();
                login.user = email.Value;
                login.pass = password.Value;

                usuario = new BLL.Usuario_BLL().getlogin(login);

                usuario.menu = new BLL.Usuario_BLL().monta_menu(usuario.id_usuario);

                if (usuario.dv_logado)
                {
                    Session["user"] = usuario;
                    Response.Redirect("Home.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    divLogin.Visible = true;
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}