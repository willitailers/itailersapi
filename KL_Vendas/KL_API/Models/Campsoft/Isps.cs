using System.Collections.Generic;

namespace KL_API.Controllers.CampSoft.Models
{
    public class Isps
    {
        public List<Datum> data { get; set; } = new List<Datum>();
    }

    public class Datum
    {
        public int isp_identifier { get; set; }
        public string status { get; set; }
        public string name { get; set; }
        public int active_licenses { get; set; }
        public Logos logos { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class Logos
    {
        public string dark { get; set; }
        public string normal { get; set; }
    }
}