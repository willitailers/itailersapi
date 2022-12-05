using DAL;
using Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class HomeSecurity_BLL
    {
        public List<Produto> carregar_produtos(string id_tipo_produto, string id_produto, string nr_token)
        {
            List<Produto> produtos = new List<Produto>();
            bool primeiro = true;

            DataTable dt = new DataTable();
            HomeSecurity_DAL dal = new HomeSecurity_DAL();

            dt = dal.carregar_produtos(id_tipo_produto, id_produto);

            foreach (DataRow row in dt.Rows)
            {

                Produto produto = new Produto()
                {
                    id_produto = Convert.ToInt32(row["id_produto"]),
                    id_fabricante = Convert.ToInt32(row["id_fabricante"]),
                    nm_produto = row["nm_produto"].ToString(),
                    nm_produto_descr_curta = row["nm_produto_descr_curta"].ToString(),
                    nm_caminho_img = row["nm_caminho_img"].ToString(),
                    nm_produto_descr_completa = row["nm_produto_descr_completa"].ToString(),
                    link_pagina = row["link_pagina"].ToString()
                };


                DataTable dt_sub = new DataTable();

                dt_sub = dal.carregar_sub_produtos(produto.id_produto.ToString(), "0", nr_token);
                primeiro = true;

                foreach (DataRow row_sub in dt_sub.Rows)
                {
                    Produto_Sub sub_produto = new Produto_Sub()
                    {
                        id_produto_sub = Convert.ToInt32(row_sub["id_produto_sub"]),
                        id_produto = produto.id_produto,
                        nm_produto_sub = row_sub["nm_produto_sub"].ToString(),
                        vl_produto_sub = Convert.ToDecimal(row_sub["vl_produto_sub"]),
                        cd_produto_sub_publico = row_sub["cd_produto_sub_publico"].ToString(),
                        nm_produto_sub_descr_completa = row_sub["nm_produto_sub_descr_completa"].ToString(),
                        vl_produto_sub_desc = Convert.ToDecimal(row_sub["vl_produto_sub_desc"]),
                        vl_desconto = Convert.ToDecimal(row_sub["vl_desconto"]),
                        dv_desconto = (row_sub["dv_desconto"].ToString() == "1"),
                        vl_desconto_str = row_sub["vl_desconto"].ToString(),
                        vl_produto_sub_desc_str = row_sub["vl_produto_sub_desc"].ToString(),
                        checado = primeiro ? "checked" : "",
                        nr_trial = int.Parse(row_sub["nr_trial"].ToString())
                    };

                    produto.Produto_Subs.Add(sub_produto);

                    primeiro = false;
                }

                produtos.Add(produto);
            }

            return produtos;
        }

        public DataTable carregar_sub_produtos(string id_produto, string id_produto_sub)
        {
            DataTable dt_sub = new DataTable();
            HomeSecurity_DAL dal = new HomeSecurity_DAL();
            dt_sub = dal.carregar_sub_produtos(id_produto, id_produto_sub, "");

            return dt_sub;
        }
    }
}
