using System.Collections.Generic;

namespace KL_API.Models.Integracao.cliente_contrato
{
    public class cliente_contrato
    {
        public string page { get; set; }
        public string total { get; set; }
        public List<registros> registros { get; set; }
    }

    public class registros
    {
        public string id { get; set; }
        public string status { get; set; }
        public string status_internet { get; set; }
        public string id_cliente { get; set; }
    }
}