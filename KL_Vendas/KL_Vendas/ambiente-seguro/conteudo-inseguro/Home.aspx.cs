using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KL_Vendas.ambiente_seguro.conteudo_improprio
{
    public partial class Home : System.Web.UI.Page
    {
        public string Token { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Token = GetParam("nr_token");

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
    }
}