using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KL_Vendas
{
    public partial class Close : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["user"] = null;

            Response.Redirect("Login.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}