using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KL_Vendas.controls
{
    public partial class BusinessProduct : System.Web.UI.UserControl
    {
        private const int C_LENGTH_MAX_SUB_NOME = 25;

        public string SubProdutoId { get; set; }

        public string NomeProduto { get; set; }

        public string PrimeiroNomeProduto { get; set; }

        public string RestanteNomeProduto { get; set; }

        public string[] DescricaoProduto { get; set; }

        public decimal ValorProduto { get; set; }

        public decimal ValorDesconto { get; set; }

        public string CdSubProduto { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                PreencheNomeProduto();

                repeaterLicencas.DataSource = DescricaoProduto;
                repeaterLicencas.DataBind();
            }
        }

        private void PreencheNomeProduto()
        {
            if (!string.IsNullOrEmpty(this.NomeProduto))
            {
                string[] partes = NomeProduto.Split(' ');

                if (partes.Length > 0)
                {
                    this.PrimeiroNomeProduto = partes[0];
                    this.RestanteNomeProduto = string.Empty;

                    for (int i = 1; i < partes.Length; i++)
                    {
                        if (i == partes.Length)
                            this.RestanteNomeProduto += partes[i];
                        else
                            this.RestanteNomeProduto += string.Format("{0} ", partes[i]);
                    }

                    if (this.RestanteNomeProduto.Length > C_LENGTH_MAX_SUB_NOME)
                        this.RestanteNomeProduto = this.RestanteNomeProduto.Substring(0, C_LENGTH_MAX_SUB_NOME - 1);
                }
            }
        }
    }
}