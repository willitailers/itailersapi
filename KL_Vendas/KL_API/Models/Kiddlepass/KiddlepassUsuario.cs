using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KL_API.Models.Kiddlepass
{
    public class KiddlepassUsuario
    {
        public int id { get; set; }
        public string userid { get; set; }
        public int id_cliente { get; set; }
        public string product_type { get; set; }
        public string status { get; set; }
        public string email { get; set; }
        public string name { get; set; }
    }

    public class UserKiddlepassResponse
    {
        public string userid { get; set; }
        public string status { get; set; }
        public string email { get; set; }
        public string name { get; set; }
    }
}