using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboHubsoft.Helpers
{
    public static class CpfHelper
    {
        public static string NormalizarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return null; // ou string.Empty, se preferir

            // Mantém só os dígitos
            var somenteDigitos = new string(cpf.Where(char.IsDigit).ToArray());

            if (somenteDigitos.Length != 11)
                return null; // ou lançar exceção, depende da sua regra

            // Aplica a máscara 000.000.000-00
            return Convert.ToUInt64(somenteDigitos).ToString(@"000\.000\.000\-00");
        }
    }
}
