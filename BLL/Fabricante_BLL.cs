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
    public class Fabricante_BLL
    {
        public DataTable fabricante_selecionar(Fabricante fabricante)
        {
            DataTable Dt = new DataTable();
            Fabricante_DAL dal = new Fabricante_DAL();

            //fabricante.id_fabricante = 0;

            Dt = dal.fabricante_selecionar(fabricante);

            return Dt;
        }

        public void fabricante_inserir(Fabricante fabricante)
        {
            Fabricante_DAL dal = new Fabricante_DAL();
            dal.fabricante_incluir(fabricante);

            return;
        }

        public DataTable fabricante_info_selecionar(Fabricante_Info fabricante)
        {
            DataTable Dt = new DataTable();
            Fabricante_DAL dal = new Fabricante_DAL();

            fabricante.id_fabricante_info = 0;
            fabricante.id_fabricante = 0;

            Dt = dal.fabricante_info_selecionar(fabricante);

            return Dt;
        }

        public void fabricante_info_inserir(Fabricante_Info fabricante)
        {
            Fabricante_DAL dal = new Fabricante_DAL();
            dal.fabricante_info_incluir(fabricante);

            return;
        }

        public DataTable fabricante_contato_selecionar(Fabricante_Contato fabricante)
        {
            DataTable Dt = new DataTable();
            Fabricante_DAL dal = new Fabricante_DAL();

            fabricante.id_fabricante_contato = 0;
            fabricante.id_fabricante = 0;

            Dt = dal.fabricante_contato_selecionar(fabricante);

            return Dt;
        }

        public void fabricante_contato_inserir(Fabricante_Contato fabricante)
        {
            Fabricante_DAL dal = new Fabricante_DAL();
            dal.fabricante_contato_incluir(fabricante);

            return;
        }
    }
}
