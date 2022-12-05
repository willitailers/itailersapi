using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KL_Vendas.controls
{
    public partial class HomeProduct : System.Web.UI.UserControl
    {
        private const string C_IMG_DEFAULT = @"img/BR_KAV_2019.png";
        private const int C_LENGTH_MAX_SUB_NOME = 20;

        public string ProdutoId { get; set; }

        public string NomeProduto { get; set; }

        public string PrimeiroNomeProduto { get; set; }

        public string RestanteNomeProduto { get; set; }

        public string CaminhoImagem { get; set; }

        public string DescricaoProduto { get; set; }

        public string VlDesconto { get; set; }

        public string Token { get; set; }

        public string link_pagina { get; set; }

        public List<Objetos.Produto_Sub> SubsProduto { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (string.IsNullOrEmpty(this.CaminhoImagem))
                    this.CaminhoImagem = C_IMG_DEFAULT;

                Session["Token"] = GetParam("nr_token");
                Token = GetParam("nr_token");

                PreencheNomeProduto();

                repeaterChecks.DataSource = SubsProduto;
                repeaterChecks.DataBind();
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

        public string ValidarEval(object eval)
        {
            if (eval == null)
                return "visibility: hidden";

            if (eval.ToString() == string.Empty)
                return "visibility: hidden";

            return "";
        }

        public string ValidarDesconto(object vl_desconto)
        {
            if (vl_desconto == null)
                return "";

            if (decimal.Parse(vl_desconto.ToString()) > 0)
                return "active";

            return "";
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