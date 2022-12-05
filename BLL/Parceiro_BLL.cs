using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Objetos;

namespace BLL
{
    public class Parceiro_BLL
    {
        public DataTable parceiro_token_selecionar(Parceiro parca)
        {
            DataTable Dt = new DataTable();
            Parceiro_DAL dal = new Parceiro_DAL();

            Dt = dal.parceiro_token_selecionar(parca);

            return Dt;
        }

        public DataTable parceiro_selecionar(string id_parceiro)
        {
            DataTable Dt = new DataTable();
            Parceiro_DAL dal = new Parceiro_DAL();

            Dt = dal.parceiro_selecionar(id_parceiro);

            return Dt;
        }

        public string parceiro_token_incluir(Parceiro parca)
        {
            
            Parceiro_DAL dal = new Parceiro_DAL();

            return dal.parceiro_token_incluir(parca);
        }

        public string token_gerar()
        {

            Parceiro_DAL dal = new Parceiro_DAL();

            return dal.token_gerar();
        }
    }
}
