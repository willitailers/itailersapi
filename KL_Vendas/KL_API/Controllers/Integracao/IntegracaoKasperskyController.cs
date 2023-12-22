using BLL;
using BLL.KL_API;
using KL_API.Models;
using KL_API.Models.Integracao.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Util;

namespace KL_API.Controllers.Integracao
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class IntegracaoKasperskyController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post()
        {
            Models.Integracao.Integracao integracao = new Models.Integracao.Integracao();
            var usuarios_nao_ativados = integracao.RetornaIntegracaoUsuariosNaoAtivados();

            List<t_integracao_usuario> usuarios = new List<t_integracao_usuario>();

            foreach (DataRow row in usuarios_nao_ativados.Rows)
            {
                
                t_integracao_usuario t_integracao_usuario = new t_integracao_usuario()
                {
                     id_cliente = Convert.ToInt32(row["id_cliente"]),
                     id = Convert.ToInt32(row["id"])
                };

                usuarios.Add(t_integracao_usuario);
            }

            foreach (var id_cliente in usuarios.Select(s => s.id_cliente))
            {
                ClientInfo client = integracao.RetornaClientInfo(id_cliente.ToString());

                List<object> comandos = new List<object>();
                List<Controle_Envio> controle = new List<Controle_Envio>();
                string TransactionId = id_cliente + DateTime.Now.ToString("yyyyMMddHHmmssffffff");

                foreach (var id_usuario in usuarios.Where(w => w.id_cliente == id_cliente).Select(s => s.id))
                {

                }

                string xmlRequest, xmlContainer;

                SubscriptionResponseContainer container = new SubscriptionResponseContainer();
                container = new KL_Conexao().Comando_KL(TransactionId, client.nm_usuario_certificado, client.nm_senha_certificado,
                    client.nm_thumbprint, comandos.ToArray(), out xmlContainer, out xmlRequest);

                //log_inserir_provisionamento(client.id_cliente.ToString() + "- RETORNO Container " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlContainer), (int)Lista_Erro.license_ativation);
                //log_inserir_provisionamento(client.id_cliente.ToString() + "- RETORNO Request " + Newtonsoft.Json.JsonConvert.SerializeObject(xmlRequest), (int)Lista_Erro.license_ativation);
                //log_inserir_provisionamento(client.id_cliente.ToString() + "- RETORNO Response " + Newtonsoft.Json.JsonConvert.SerializeObject(container), (int)Lista_Erro.license_ativation);

                try
                {
                    foreach (object obj in container.Items)
                    {
                        if (obj.GetType() == typeof(SubscriptionResponseItemCollection))
                        {
                            SubscriptionResponseItemCollection itens = new SubscriptionResponseItemCollection();
                            itens = (SubscriptionResponseItemCollection)obj;

                            string id_provisionamento = string.Empty;

                            foreach (var item in itens.Items)
                            {
                                if (item.GetType() == typeof(SubscriptionResponseItemCollectionActivate))
                                {
                                    try
                                    {
                                        var controleEnvio = new Controle_Envio();

                                        var itemDetalhe = (SubscriptionResponseItemCollectionActivate)item;

                                        controleEnvio = controle.Where(x => x.UnitId.ToString() == itemDetalhe.UnitId).FirstOrDefault();

                                        id_provisionamento = InserirProvisionamento(controleEnvio.id_cliente_licenca, controleEnvio.id_cliente_usuario,
                                            controleEnvio.SubscribeId, controleEnvio.id_produto_kl, itemDetalhe.ActivationCode, "", "", "", "", true, DateTime.Now);
                                    }
                                    catch (Exception ex)
                                    {
                                        //log_inserir_provisionamento("Erro Provisionamento InserirProvisionamento - " + ex.Message, (int)Lista_Erro.license_ativation);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //log_inserir_provisionamento(client.id_cliente.ToString() + " - Erro Provisionamento - " + controle[0].id_cliente_usuario.ToString() + " - " + ex.Message, (int)Lista_Erro.license_ativation);
                }
            }

            //string uniqueID = integracao.GenerateUniqueId(row["email"].ToString());

            

            return Request.CreateResponse(HttpStatusCode.OK, "OK!");
        }
    }
}

