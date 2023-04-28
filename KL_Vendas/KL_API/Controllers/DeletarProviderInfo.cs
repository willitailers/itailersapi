using KL_API.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace KL_API.Controllers
{
    public class DeletarProviderInfoController : ApiController
    {
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage DeletarProviderInfo([FromBody] VendorTheme vendorTheme)
        {
            var vendorThemeDataTable = new Ativacao_Controle().DeleteVendorTheme(vendorTheme);

            List<VendorTheme> vendorThemes = new List<VendorTheme>();

            if (vendorThemeDataTable.Rows.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Registro deletado, tabela vazia");
            }

            foreach (System.Data.DataRow item in vendorThemeDataTable.Rows)
            {
                VendorTheme vendorThemeRetorno = new VendorTheme
                {
                    id_vendor_theme = Convert.ToInt32(item["id_vendor_theme"].ToString()),
                    id_cliente = Convert.ToInt32(item["id_cliente"].ToString()),
                    vendorDomainName = item["vendorDomainName"].ToString(),
                    primaryColor = item["primaryColor"].ToString(),
                    secondaryColor = item["secondaryColor"].ToString(),
                    logoImage = item["logoImage"].ToString(),
                    vendorTitleImage = item["vendorTitleImage"].ToString(),
                    bannerImage = item["bannerImage"].ToString(),
                    isDarkTheme = (Boolean)item["isDarkTheme"],
                    kl_token = item["kl_token"].ToString(),
                    bannerImageMobile = item["bannerImageMobile"].ToString()
                };

                vendorThemes.Add(vendorThemeRetorno);
            }


            return Request.CreateResponse<List<VendorTheme>>(HttpStatusCode.OK, vendorThemes);
        }
    }
}
