using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KL_Vendas
{
    public partial class Index : System.Web.UI.Page
    {
        public string Token { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.Token = GetParam("nr_token");
                RegistrarAcesso();
            }
        }

        private void RegistrarAcesso()
        {
            if (!string.IsNullOrEmpty(this.Token))
            {
                Usuario_BLL bll = new Usuario_BLL();

                bll.RegistrarAcesso(this.Token);
            }
        }

        private string GetParam(string name)
        {
            try
            {
                return string.IsNullOrEmpty(Request[name]) ? "-" : Request[name];
            }
            catch (Exception)
            {
                return "-";
            }

        }

        private int GetParam(string name, int default_value)
        {
            try
            {
                return Convert.ToInt32(Request[name]);
            }
            catch (Exception)
            {
                return default_value;
            }
        }
    }
}