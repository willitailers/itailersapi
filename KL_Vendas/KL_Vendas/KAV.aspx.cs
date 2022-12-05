using BLL;
using System;
using System.Collections.Generic;
using Objetos;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KL_Vendas
{
    public partial class KAV : System.Web.UI.Page
    {
        public string Token { get; set; }

        private const string C_IMG_DEFAULT = @"img/BR_KAV_2019.png";
        private const int C_LENGTH_MAX_SUB_NOME = 20;

        public string ProdutoId { get; set; }

        public string NomeProduto { get; set; }

        public string PrimeiroNomeProduto { get; set; }

        public string RestanteNomeProduto { get; set; }

        public string CaminhoImagem { get; set; }

        public string DescricaoProduto { get; set; }

        public string VlDesconto { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["Token"] = GetParam("nr_token");
                Token = GetParam("nr_token");
                CarregarProdutos();
                CarregarAvaliacao();
            }

            this.Token = GetParam("nr_token");
        }

        public string ValidarDesconto(object vl_desconto)
        {
            if (vl_desconto == null)
                return "";

            if (decimal.Parse(vl_desconto.ToString()) > 0)
                return "active";

            return "";
        }

        private void CarregarProdutos()
        {
            int id_tipo_produto = int.Parse(System.Configuration.ConfigurationManager.AppSettings["KAV"].ToString());

            HomeSecurity_BLL business = new HomeSecurity_BLL();

            List<Objetos.Produto> produtos = business.carregar_produtos("1", id_tipo_produto.ToString(), Token);

            //EqualizaProdutosSubs(ref produtos);

            foreach (var produto in produtos)
            {
                ProdutoId = produto.id_produto.ToString();
                NomeProduto = produto.nm_produto;
                CaminhoImagem = produto.nm_caminho_img;
                DescricaoProduto = produto.nm_produto_descr_completa;

            }

            repeaterChecks.DataSource = produtos[0].Produto_Subs;
            repeaterChecks.DataBind();

        }

        private void CarregarAvaliacao()
        {
            Avaliacao aval = new Avaliacao() { tp_acao = 1, id_avaliacao = 0, nm_avaliacao = "", nm_titulo = "", nm_pagina = "KAV", id_melhor_produto = 0, nm_melhor_produto = "", nm_titulo_melhor_produto = "" };

            DataTable dt = new DataTable();
            dt = new Avaliacao_BLL().avaliacao_selecionar(aval);

            DataTable dt_new = new DataTable();
            dt_new.Columns.Add("classe");
            dt_new.Columns.Add("nm_titulo");
            dt_new.Columns.Add("nm_avaliacao");

            int i = 1;

            foreach (DataRow dr in dt.Rows)
            {
                if (i == 1)
                {
                    dt_new.Rows.Add("active", dr["nm_titulo"].ToString(), dr["nm_avaliacao"].ToString());
                }
                else
                    dt_new.Rows.Add("", dr["nm_titulo"].ToString(), dr["nm_avaliacao"].ToString());

                i++;
            }

            rptAvaliacao.DataSource = dt_new;
            rptAvaliacao.DataBind();

            rptMelhor.DataSource = new Avaliacao_BLL().melhor_produto_selecionar(aval);
            rptMelhor.DataBind();

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