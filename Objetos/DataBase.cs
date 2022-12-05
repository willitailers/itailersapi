using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Objetos
{
    public class DataBase
    {

        public string procedure
        { set; get; }

        public List<parametros> parametros
        { set; get; }

        public parametros retorna_parametros(string nm_parametro, string vl_parametro)
        {
            parametros p = new parametros();

           p.nm_parametro = nm_parametro;
            p.vl_parametro = vl_parametro;
            return p;

        }
    }

    public class parametros
    {
        public string nm_parametro
        { set; get; }

        public string vl_parametro
        { set; get; }

        public DbType tp_parametro
        { set; get; }

        public ParameterDirection in_out
        { get; set; }
    }
}
