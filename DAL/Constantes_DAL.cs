using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Constantes_DAL
    {

        //string home = System.Configuration.ConfigurationManager.AppSettings["url_config"].ToString();

        public static string Conexao_AV = ConfigurationManager.ConnectionStrings["ConnVenda"].ConnectionString;

        public static string Conexao_UOL = ConfigurationManager.ConnectionStrings["ConnUOL"].ConnectionString;

        public static string Conexao_API = ConfigurationManager.ConnectionStrings["ConnAPI"].ConnectionString;


        // prod

        public string home = System.Configuration.ConfigurationManager.AppSettings["home"].ToString();

        public string home_voce = System.Configuration.ConfigurationManager.AppSettings["home_voce"].ToString();

        public string home_kav = System.Configuration.ConfigurationManager.AppSettings["home_kav"].ToString();

        public string home_kis = System.Configuration.ConfigurationManager.AppSettings["home_kis"].ToString();

        public string home_kts = System.Configuration.ConfigurationManager.AppSettings["home_kts"].ToString();

        public string home_kisa = System.Configuration.ConfigurationManager.AppSettings["home_kisa"].ToString();

        public string home_ksk = System.Configuration.ConfigurationManager.AppSettings["home_ksk"].ToString();

        public string home_empresa = System.Configuration.ConfigurationManager.AppSettings["home_empresa"].ToString();
        

        // dev
        /*
        public const string home = "http://localhost:59056/";

        public const string home_page = "http://localhost:59056/";

        public const string home_pc = "http://localhost:59056/HomeSecurity.aspx?product_type=1";

        public const string home_mac = "http://localhost:59056/HomeSecurity.aspx?product_type=1";

        public const string home_mobile = "http://localhost:59056/HomeSecurity.aspx?product_type=2";

        public const string home_psmall_business = "http://localhost:59056/BusinessSecurity.aspx";
        */

    }
}
