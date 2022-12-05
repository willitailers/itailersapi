using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objetos;
using DAL;
using System.Data;


namespace BLL
{
    public class Produto_BLL
    {
        public DataTable produto_selecionar(Produto produto)
        {
            DataTable Dt = new DataTable();
            Produto_DAL dal = new Produto_DAL();

            //produto.id_produto = 0;

            Dt = dal.produto_selecionar(produto);

            return Dt;
        }

        public void produto_inserir(Produto produto)
        {
            Produto_DAL dal = new Produto_DAL();
            dal.produto_incluir(produto);

            return;
        }

        public DataTable produto_sub_selecionar(Produto_Sub produto)
        {
            DataTable Dt = new DataTable();
            Produto_DAL dal = new Produto_DAL();

            //produto.id_produto = 0;

            Dt = dal.produto_sub_selecionar(produto);

            return Dt;
        }

        public string produto_sub_especial_inserir(Produto_Sub produto)
        {
            DataTable Dt = new DataTable();
            Produto_DAL dal = new Produto_DAL();

            //produto.id_produto = 0;

            return dal.produto_sub_especial_inserir(produto);

        }

        public DataTable produto_sub_especial_consulta(Produto_Sub produto)
        {
            DataTable Dt = new DataTable();
            Produto_DAL dal = new Produto_DAL();

            //produto.id_produto = 0;

            Dt = dal.produto_sub_especial_consulta(produto);

            return Dt;
        }

        public DataTable produto_sub_especial_token(int id_parceiro_token)
        {
            DataTable Dt = new DataTable();
            Produto_DAL dal = new Produto_DAL();

            //produto.id_produto = 0;

            Dt = dal.produto_sub_especial_token(id_parceiro_token);

            return Dt;
        }

        public DataTable produto_sub_todos()
        {
            DataTable Dt = new DataTable();
            Produto_DAL dal = new Produto_DAL();

            //produto.id_produto = 0;

            Dt = dal.produto_sub_todos();

            return Dt;
        }

        public void produto_sub_inserir(Produto_Sub produto)
        {
            Produto_DAL dal = new Produto_DAL();
            dal.produto_sub_incluir(produto);

            return;
        }
    }
}
