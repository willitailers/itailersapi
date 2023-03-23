using KL_API.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KL_API.Controllers
{
    public class GetProviderInfoController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetProviderInfo(string provedor)
        {
            //var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<VendorTheme>(value.ToString());
            //VendorTheme vendorDomain = (VendorTheme)obj;

            var vendorThemeDataTable = new Ativacao_Controle().ConsultaVendorTheme(provedor);

            if (vendorThemeDataTable.Rows.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Informações do provedor não encontradas.");
            }

            VendorTheme vendorTheme = new VendorTheme
            {
                id_vendor_theme = Convert.ToInt32(vendorThemeDataTable.Rows[0]["id_vendor_theme"].ToString()),
                id_cliente = Convert.ToInt32(vendorThemeDataTable.Rows[0]["id_cliente"].ToString()),
                vendorDomainName = vendorThemeDataTable.Rows[0]["vendorDomainName"].ToString(),
                primaryColor = vendorThemeDataTable.Rows[0]["primaryColor"].ToString(),
                secondaryColor = vendorThemeDataTable.Rows[0]["secondaryColor"].ToString(),
                logoImage = vendorThemeDataTable.Rows[0]["logoImage"].ToString(),
                vendorTitleImage = vendorThemeDataTable.Rows[0]["vendorTitleImage"].ToString(),
                bannerImage = vendorThemeDataTable.Rows[0]["bannerImage"].ToString(),
                isDarkTheme = (Boolean)vendorThemeDataTable.Rows[0]["isDarkTheme"],
                kl_token = vendorThemeDataTable.Rows[0]["kl_token"].ToString()
            };

            return Request.CreateResponse<VendorTheme>(HttpStatusCode.OK, vendorTheme);
        }
    }
}
