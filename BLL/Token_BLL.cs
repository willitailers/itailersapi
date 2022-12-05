using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Token_BLL
    {
        public bool ValidaToken(string token)
        {
            bool retorno = false;

            DataTable dt = new DataTable();

            dt = new Token_DAL().seleciona_token();

            foreach (DataRow dr in dt.Rows)
            {
                if (token == dr["cd_token"].ToString() && dr["dv_ativo"].ToString() == "1")
                    retorno = true;
            }


            return retorno;
        }
    }
}
