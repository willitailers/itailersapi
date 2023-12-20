using DAL;
using Objetos;
using System.Collections.Generic;
using System.Data;

namespace KL_API.Models.Integracao.cliente
{
    public class cliente
    {
        public string page { get; set; }
        public string total { get; set; }
        public List<registros> registros { get; set; }
    }

    public class registros
    {
        public string id { get; set; }
        public string ativo { get; set; }
        public string email { get; set; }
        public string cnpj_cpf { get; set; }
        public string telefone_celular { get; set; }
    }
}