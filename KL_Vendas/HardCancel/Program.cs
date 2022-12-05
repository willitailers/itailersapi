using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BLL;

namespace HardCancel
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
                try
                {
                    Cancelar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " - Erro na execução: " + ex.Message);
                    Thread.Sleep(1000 * 60 * 30);
                }
                finally
                {
                    Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " - Fim da execução");
                    Thread.Sleep(1000 * 60 * 60 * 24);
                }
                
        }

        static void Cancelar()
        {
            new Arquivo_BLL().Cancelamento_licencas();
        }
    }
}
