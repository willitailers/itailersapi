using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace API_Licencas.Models
{
    public class Produtos
    {
        public int id_produto_kl { get; set; }
        public string nm_urn { get; set; }
        public string nm_produto_kl { get; set; }
        public string descricao { get; set; }
        public string qtd_licencas { get; set; }
        public string id_combo { get; set; }
        public string cd_produto_kl { get; set; }
    }

    public class ProdutoAtivado
    {
        public int id_produto_kl { get; set; }
        public string nm_subrscribe_kl { get; set; }
        public string activation_code { get; set; }
        public string dt_ativacao { get; set; }
    }

    public static class DataTableExtensions
    {
        public static List<T> ConvertToList<T>(this DataTable dataTable) where T : new()
        {
            List<T> list = new List<T>();
            var properties = typeof(T).GetProperties();

            foreach (DataRow row in dataTable.Rows)
            {
                T obj = new T();
                foreach (var prop in properties)
                {
                    if (dataTable.Columns.Contains(prop.Name) && row[prop.Name] != DBNull.Value)
                    {
                        prop.SetValue(obj, Convert.ChangeType(row[prop.Name], prop.PropertyType));
                    }
                }
                list.Add(obj);
            }

            return list;
        }
    }

    public class Helper
    {
        public string GetHash(string data, int length)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(data);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                string hashString = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
                return hashString.Substring(0, length);
            }
        }
    }
}