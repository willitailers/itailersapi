using KL_API.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using System.Data;
using System;
using KL_API.Models.Guardiao;
using KL_API.Models.GuardiaoDAL;

namespace KL_API.Controllers.Guardiao
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class RetornaUsuariosGuardiaoController : ApiController
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage GetUsuariosGuardiao()
        {
            var guardiao_cliente = new GuardiaoClientes();
            var guardiao = new GuardiaoDAL();

            if (Request.Headers.Contains("token_acesso"))
            {
                string token = Request.Headers.GetValues("token_acesso").First();
                guardiao_cliente = guardiao.ValidaToken(token);

                if (guardiao_cliente.valido is false)
                {
                    return Request.CreateResponse<string>(HttpStatusCode.NotAcceptable, "Token Inválido" );
                }
            }
            else
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotAcceptable, "Token Inválido" );
            }

            DataTable dt_guardiao_licencas = guardiao.GetUsuariosPorCliente(guardiao_cliente.guardiao_id_cliente);

            List<GuardiaoLicencaResponse> guardiaoLicencasResponse = new List<GuardiaoLicencaResponse>();
            
            if (dt_guardiao_licencas.Rows.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Nenhum registro encontrado para o cliente");

            foreach (DataRow row in dt_guardiao_licencas.Rows)
            {
                GuardiaoLicencaResponse guardiaoLicencaResponse = new GuardiaoLicencaResponse()
                {
                    nome = row["nome"].ToString(),
                    email = row["email"].ToString(),
                    status = row["status"].ToString(),
                    data_criacao = Convert.ToDateTime(row["guardiao_data_criacao"]),
                    data_atualizacao = Convert.ToDateTime(row["guardiao_data_atualizacao"])
                };

                guardiaoLicencasResponse.Add(guardiaoLicencaResponse);
            }

            return Request.CreateResponse(HttpStatusCode.OK, guardiaoLicencasResponse);
        }

        public class GuardiaoLicencaResponse
        {
            public string nome { get; set; }
            public string email { get; set; }
            public string status { get; set; }
            public DateTime data_criacao { get; set; }
            public DateTime data_atualizacao { get; set; }
        }
    }
}
