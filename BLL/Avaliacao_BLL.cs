using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Objetos;
using DAL;

namespace BLL
{
    public class Avaliacao_BLL
    {
        public DataTable avaliacao_selecionar(Avaliacao avaliacao)
        {
            return new Avaliacao_DAL().avaliacao_selecionar(avaliacao);
        }

        public DataTable melhor_produto_selecionar(Avaliacao avaliacao)
        {
            return new Avaliacao_DAL().melhor_produto_selecionar(avaliacao);
        }
    }
}
