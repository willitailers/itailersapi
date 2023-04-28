using KL_API.Models;
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
    public class GerenciaLicencaMKController : ApiController
    {
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage GerenciaLicencaMK()
        {
            //List<WSMKPlanosAcesso> wSMKPlanosAcesso = new Ativacao_Controle().GetPlanosAcesso();
            //return Request.CreateResponse<List<WSMKPlanosAcesso>>(HttpStatusCode.OK, wSMKPlanosAcesso);

            new Ativacao_Controle().CancelarMK();
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Cancelamento realizado com sucesso.");
        }
    }
}
