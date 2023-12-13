using System.Collections.Generic;

namespace KL_API.Models.Integracao
{
    public class view_vd_contratos_produtos_gen
    {
        public string page { get; set; }
        public string total { get; set; }
        public List<Row> rows { get; set; }
    }

    public class Row
    {
        public string id { get; set; }
        public List<string> cell { get; set; }
    }

    
}