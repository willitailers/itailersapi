using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Objetos;
using DAL;

namespace BLL
{
    public class Carrinho_BLL
    {
        public DataTable token_selecionar(string nr_token)
        {
            return new DAL.Carrinho_DAL().token_selecionar(nr_token);
        }

        public DataTable produto_selecionar(string cd_produto_sub_publico, string nr_token = "")
        {
            return new DAL.Carrinho_DAL().produto_selecionar(cd_produto_sub_publico, nr_token);
        }

        public Carrinho carrinho_selecionar(string cd_carrinho, string id_carrinho)
        {
            return new DAL.Carrinho_DAL().carrinho_selecionar(cd_carrinho, id_carrinho);
        }
    }
}
