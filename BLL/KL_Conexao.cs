using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.KL_API;
using DAL;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.ServiceModel;
using System.Xml.Linq;

namespace BLL
{

    public class Neo_Produto_Combo
    {
        public string _ProductId { set; get; }
        public string _SubscriberId { set; get; }
        public string qtd_licencas { set; get; }
        public string UnitId { set; get; }
        public string id_produto_kl { set; get; }
        public string id_produto { set; get; }
        public string nm_produto { set; get; }
    }
    public class KL_Conexao
    {
        public string ativar_compra(string id_carrinho)
        {
            string passo = "";
            try
            {
                passo = "seleciona pedido";
                // consulta dados da compra
                DataTable dt = new DataTable();
                dt = new Carrinho_DAL().pedido_selecionar("0", id_carrinho, "");

                if (dt.Rows.Count > 0)
                {
                    // ativa apenas quando retornar comprovacao de pagamento
                    passo = "Solicita Ativação KL";
                    // Solicita licença KL
                    string transaction_id = dt.Rows[0]["id_compra"].ToString(), cd_produto = dt.Rows[0]["cd_produto"].ToString(), subcribe_id = dt.Rows[0]["id_inscricao"].ToString(), qtd_licencas = dt.Rows[0]["qtd_licencas"].ToString();

                    KL_Conexao con = new KL_Conexao();
                    SubscriptionResponseContainer container = new SubscriptionResponseContainer();
                    container = con.Ativacao(transaction_id, cd_produto, subcribe_id, qtd_licencas);


                    bool ativado = false, erro = false;

                    passo = "analisa retorno KL";
                    foreach (object obj in container.Items)
                    {
                        try
                        {
                            SubscriptionResponseItemCollection item = new SubscriptionResponseItemCollection();
                            item = (SubscriptionResponseItemCollection)obj;

                            SubscriptionResponseItemCollectionActivate ativo = new SubscriptionResponseItemCollectionActivate();

                            ativo = (SubscriptionResponseItemCollectionActivate)item.Items[0];

                            passo = "Atualiza ativacao";
                            new Carrinho_DAL().compra_atualiza_licenca(transaction_id, ativo.ActivationCode, "");

                            ativado = true;

                            return ativo.ActivationCode;
                        }
                        catch
                        {
                            ativado = false;
                        }

                        if (!ativado)
                        {
                            try
                            {
                                SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                                itemErro = (SubscriptionResponseErrorCollection)obj;

                                Generico.log_inserir(2, " KLWS - ativacao - " + transaction_id + " subscricao " + subcribe_id + " " + itemErro.Items[0].ErrorMessage + " - code " + itemErro.Items[0].ErrorCode, 0);

                                

                                passo = "Cancelar pagamento ou solicitar nova ativação";
                                // prepara cancelamento do pagamento 
                            }
                            catch
                            {
                                passo = "retorno de erro KL - ativacao - " + transaction_id + " subscricao " + subcribe_id;
                                return "";
                            }
                        }
                    }
                }
                else
                {
                    passo = "pedido não encontrado. nr: " + id_carrinho;
                    return "";
                }

                return "";

            }
            catch (Exception ex)
            {
                Generico.log_inserir(1, " - ativar_compra | passo: " + passo + " - " + ex.Message, 0);
                return "";
            }
        }

        public string ativar_compra2(string id_carrinho)
        {
            string passo = "";
            try
            {
                passo = "seleciona pedido";
                // consulta dados da compra
                DataTable dt = new DataTable();
                dt = new Carrinho_DAL().pedido_selecionar("0", id_carrinho, "");

                if (dt.Rows.Count > 0)
                {
                    // ativa apenas quando retornar comprovacao de pagamento
                    passo = "Solicita Ativação KL";
                    // Solicita licença KL
                    string transaction_id = dt.Rows[0]["id_compra"].ToString(), cd_produto = dt.Rows[0]["cd_produto"].ToString(), subcribe_id = dt.Rows[0]["id_inscricao"].ToString(), qtd_licencas = dt.Rows[0]["qtd_licencas"].ToString();

                    KL_Conexao con = new KL_Conexao();
                    SubscriptionResponseContainer container = new SubscriptionResponseContainer();
                    container = con.Ativacao("00999" + transaction_id, cd_produto, subcribe_id, qtd_licencas);


                    bool ativado = false, erro = false;

                    passo = "analisa retorno KL";
                    foreach (object obj in container.Items)
                    {
                        try
                        {
                            SubscriptionResponseItemCollection item = new SubscriptionResponseItemCollection();
                            item = (SubscriptionResponseItemCollection)obj;

                            SubscriptionResponseItemCollectionActivate ativo = new SubscriptionResponseItemCollectionActivate();

                            ativo = (SubscriptionResponseItemCollectionActivate)item.Items[0];

                            passo = "Atualiza ativacao";
                            new Carrinho_DAL().compra_atualiza_licenca(transaction_id, ativo.ActivationCode, "");

                            ativado = true;

                            return ativo.ActivationCode;
                        }
                        catch
                        {
                            ativado = false;
                        }

                        if (!ativado)
                        {
                            try
                            {
                                SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                                itemErro = (SubscriptionResponseErrorCollection)obj;

                                Generico.log_inserir(2, " KLWS - ativacao - " + transaction_id + " subscricao " + subcribe_id + " " + itemErro.Items[0].ErrorMessage, 0);

                                passo = "Cancelar pagamento ou solicitar nova ativação";
                                // prepara cancelamento do pagamento 
                            }
                            catch
                            {
                                passo = "retorno de erro KL - ativacao - " + transaction_id + " subscricao " + subcribe_id;
                                return "";
                            }
                        }
                    }
                }
                else
                {
                    passo = "pedido não encontrado. nr: " + id_carrinho;
                    return "";
                }

                return "";

            }
            catch (Exception ex)
            {
                Generico.log_inserir(1, " - ativar_compra | passo: " + passo + " - " + ex.Message, 0);
                return "";
            }
        }



        public SubscriptionResponseContainer Ativacao(string _TransactionId, string _ProductId, string _SubscriberId, string qtd_licencas)
        {
            SubscriptionRequestContainer requestContainer = new SubscriptionRequestContainer();
            requestContainer.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            requestContainer.TransactionId = _TransactionId; // "010000002";

            SubscriptionRequestRequest request = new SubscriptionRequestRequest();
            if (System.Configuration.ConfigurationManager.AppSettings["ambiente_Producao"].ToString() == "1")
                request.AccessInfo = new AccessInfo() { UserName = "linktel_br", Password = "3G2%4H14i9xgrd@" };
            else
                request.AccessInfo = new AccessInfo() { UserName = "itailers_test", Password = "Twu1QUbtn8Rx" };

            SubscriptionRequestActivateItem activateItem = new SubscriptionRequestActivateItem();

            activateItem.ActivationType = ActivationTypeEnum.Standard;
            activateItem.LicenseCount = qtd_licencas;
            activateItem.ProductId = _ProductId; // "KISMD";
            activateItem.StartTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");  //DateTime.Now;
            activateItem.EndTime = "indefinite"; //DateTime.Now.ad.ToString("yyyy -MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z";  //DateTime.Now.AddYears(1).ToString("yyyy-MM-dd HH:mm");
            activateItem.UnitId = "1";
            activateItem.SubscriberId = _SubscriberId; // "C8147E02-8A91-43B2-AD48-5D04DAFD38C2";

            object[] obj = new object[1];

            obj[0] = activateItem;

            requestContainer.SubscriptionRequest = obj;
            X509Certificate2 cert;

            if (System.Configuration.ConfigurationManager.AppSettings["ambiente_web"].ToString() == "1")
                cert = new X509Certificate2(HttpContext.Current.Server.MapPath("~/Email/Itailers.pfx"), "+P47c-!3&v7!", X509KeyStorageFlags.PersistKeySet);
            else
                cert = new X509Certificate2(@"C:/certificado/Itailers.pfx", "+P47c-!3&v7!", X509KeyStorageFlags.PersistKeySet);

            KasperskySubscriptionServicePortTypeClient client = new KasperskySubscriptionServicePortTypeClient();
            client.ClientCredentials.ClientCertificate.Certificate = cert;
            client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByIssuerName, "Kaspersky Subscription Service CA G3");
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;


            //client.ClientCredentials.HttpDigest.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Anonymous;
            //client.ClientCredentials.HttpDigest.ClientCredential.UserName = "linktel_br";
            //client.ClientCredentials.HttpDigest.ClientCredential.Password = "3G2%4H14i9xgrd@";
            
            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = client.SubscriptionRequest(request.AccessInfo, requestContainer);

            return container;
        }

        public SubscriptionResponseContainer AtivacaoUOL(string _TransactionId, string _ProductId, string _SubscriberId, string qtd_licencas, DateTime dateTime)
        {
            SubscriptionRequestContainer requestContainer = new SubscriptionRequestContainer();
            requestContainer.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            requestContainer.TransactionId = _TransactionId; // "010000002";

            SubscriptionRequestRequest request = new SubscriptionRequestRequest();
            if (System.Configuration.ConfigurationManager.AppSettings["ambiente_Producao"].ToString() == "1")
                request.AccessInfo = new AccessInfo() { UserName = "uol_edtech_30d", Password = "@Itailers2021#" };
            else
                request.AccessInfo = new AccessInfo() { UserName = "uol_edtech_30d", Password = "@Itailers2021#" };

            SubscriptionRequestActivateItem activateItem = new SubscriptionRequestActivateItem();

            activateItem.ActivationType = ActivationTypeEnum.Standard;
            activateItem.LicenseCount = qtd_licencas;
            activateItem.ProductId = _ProductId; // "KISMD";
            activateItem.StartTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");  //DateTime.Now;
            activateItem.EndTime = dateTime.ToString("yyyy -MM-dd") + "T" + dateTime.ToString("HH:mm:ss.ffffff") + "Z";  
            activateItem.UnitId = "1";
            activateItem.SubscriberId = _SubscriberId; // "C8147E02-8A91-43B2-AD48-5D04DAFD38C2";

            object[] obj = new object[1];

            obj[0] = activateItem;

            requestContainer.SubscriptionRequest = obj;
            X509Certificate2 cert;
            KasperskySubscriptionServicePortTypeClient client = new KasperskySubscriptionServicePortTypeClient();

            if (System.Configuration.ConfigurationManager.AppSettings["ambiente_Producao"].ToString() == "1")
            {
                X509Store myX509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                myX509Store.Open(OpenFlags.ReadOnly);
                X509Certificate2 myCertificate = myX509Store.Certificates.OfType<X509Certificate2>().FirstOrDefault(certi => certi.Thumbprint.ToLower() == "240d0eb8d41bde38e10c4ac8370d800cf00b9070");
                client.ClientCredentials.ClientCertificate.Certificate = myCertificate;
                //client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, "b14be2f3364e4ff886ef5c07c1074b1898653fa7");
            }

            else
            {
                cert = new X509Certificate2(@"F:/certificado/kss_cert_CN=uol_edtech, O=Itailers_20210308.pfx", "@Itailers2021#", X509KeyStorageFlags.PersistKeySet);
                client.ClientCredentials.ClientCertificate.Certificate = cert;
            }


            ((BasicHttpBinding)client.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;


            //client.ClientCredentials.HttpDigest.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Anonymous;
            //client.ClientCredentials.HttpDigest.ClientCredential.UserName = "linktel_br";
            //client.ClientCredentials.HttpDigest.ClientCredential.Password = "3G2%4H14i9xgrd@";

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = client.SubscriptionRequest(request.AccessInfo, requestContainer);

            return container;
        }

        public SubscriptionResponseContainer AtivacaoLicense(string _TransactionId, string _ProductId, string _SubscriberId, string qtd_licencas, DateTime dateTime, bool ignore_User)
        {
            SubscriptionRequestContainer requestContainer = new SubscriptionRequestContainer();
            requestContainer.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            requestContainer.TransactionId = _TransactionId; // "010000002";

            SubscriptionRequestRequest request = new SubscriptionRequestRequest();
            if (System.Configuration.ConfigurationManager.AppSettings["ambiente_Producao"].ToString() == "1")
                request.AccessInfo = new AccessInfo() { UserName = "uol_edtech_30d", Password = "@Itailers2021#" };
            else
                request.AccessInfo = new AccessInfo() { UserName = "uol_edtech_30d", Password = "@Itailers2021#" };

            SubscriptionRequestActivateItem activateItem = new SubscriptionRequestActivateItem();

            activateItem.ActivationType = ActivationTypeEnum.Standard;
            activateItem.LicenseCount = qtd_licencas;
            activateItem.ProductId = _ProductId; // "KISMD";
            activateItem.StartTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");  //DateTime.Now;
            activateItem.EndTime = dateTime.ToString("yyyy -MM-dd") + "T" + dateTime.ToString("HH:mm:ss.ffffff") + "Z";
            activateItem.UnitId = "1";
            activateItem.UserIgnore = ignore_User;
            activateItem.SubscriberId = _SubscriberId; // "C8147E02-8A91-43B2-AD48-5D04DAFD38C2";

            object[] obj = new object[1];

            obj[0] = activateItem;

            requestContainer.SubscriptionRequest = obj;
            X509Certificate2 cert;
            KasperskySubscriptionServicePortTypeClient client = new KasperskySubscriptionServicePortTypeClient();

            if (System.Configuration.ConfigurationManager.AppSettings["ambiente_Producao"].ToString() == "1")
            {
                X509Store myX509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                myX509Store.Open(OpenFlags.ReadOnly);
                X509Certificate2 myCertificate = myX509Store.Certificates.OfType<X509Certificate2>().FirstOrDefault(certi => certi.Thumbprint.ToLower() == "240d0eb8d41bde38e10c4ac8370d800cf00b9070");
                client.ClientCredentials.ClientCertificate.Certificate = myCertificate;
                //client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, "b14be2f3364e4ff886ef5c07c1074b1898653fa7");
            }

            else
            {
                cert = new X509Certificate2(@"F:/certificado/kss_cert_CN=uol_edtech, O=Itailers_20210308.pfx", "@Itailers2021#", X509KeyStorageFlags.PersistKeySet);
                client.ClientCredentials.ClientCertificate.Certificate = cert;
            }


            ((BasicHttpBinding)client.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;


            //client.ClientCredentials.HttpDigest.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Anonymous;
            //client.ClientCredentials.HttpDigest.ClientCredential.UserName = "linktel_br";
            //client.ClientCredentials.HttpDigest.ClientCredential.Password = "3G2%4H14i9xgrd@";

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = client.SubscriptionRequest(request.AccessInfo, requestContainer);

            return container;
        }

        public SubscriptionRequestActivateItem KL_retorna_ativacao(string LicenseCount, string ProductId, DateTime StartTime, string EndTime, string UnitId, bool UserIgnore, string SubscriberId)
        {
            SubscriptionRequestActivateItem activateItem = new SubscriptionRequestActivateItem();

            activateItem.ActivationType = ActivationTypeEnum.Standard;
            activateItem.LicenseCount = LicenseCount;
            activateItem.ProductId = ProductId; 
            activateItem.StartTime = StartTime;  
            activateItem.EndTime = EndTime;
            activateItem.UnitId = UnitId;
            activateItem.UserIgnore = UserIgnore;
            activateItem.SubscriberId = SubscriberId;

            return activateItem;
        }

        [Obsolete]
        public SubscriptionRequestResumeItem KL_retorna_resume(string SubscriberId, string UnitId)
        {
            SubscriptionRequestResumeItem resumeItem = new SubscriptionRequestResumeItem();

            resumeItem.UnitId = UnitId;
            resumeItem.SubscriberId = SubscriberId;

            return resumeItem;
        }

        public SubscriptionRequestRenewItem KL_retorna_renew(string SubscriberId, string UnitId)
        {
            SubscriptionRequestRenewItem renewItem = new SubscriptionRequestRenewItem();

            renewItem.UnitId = UnitId;
            renewItem.SubscriberId = SubscriberId;
            renewItem.EndTime = "indefinite";

            return renewItem;
        }

        public SubscriptionRequestGetDownloadLinksItem KL_retorna_link(string SubscriberId, string UnitId, string Language, PlatformEnum Platform)
        {
            SubscriptionRequestGetDownloadLinksItem Item = new SubscriptionRequestGetDownloadLinksItem();

            Item.UnitId = UnitId;
            Item.SubscriberId = SubscriberId; // "C8147E02-8A91-43B2-AD48-5D04DAFD38C2";
            Item.Language = Language;
            Item.Platform = Platform;
            Item.Region = "BR";

            return Item;
        }

        public SubscriptionRequestHardCancelItem KL_retorna_cancelamento_hard(string SubscriberId, DateTime EndTime, string UnitId)
        {
            SubscriptionRequestHardCancelItem cancelItem = new SubscriptionRequestHardCancelItem();

            cancelItem.EndTime = EndTime;
            cancelItem.SubscriberId = SubscriberId;
            cancelItem.UnitId = UnitId;

            return cancelItem;
        }

        public SubscriptionRequestSoftCancelItem KL_retorna_cancelamento_soft(string SubscriberId, DateTime EndTime, string UnitId)
        {
            SubscriptionRequestSoftCancelItem cancelItem = new SubscriptionRequestSoftCancelItem();

            cancelItem.EndTime = EndTime;
            cancelItem.SubscriberId = SubscriberId;
            cancelItem.UnitId = UnitId;

            return cancelItem;
        }

        public SubscriptionResponseContainer Comando_KL(string _TransactionId, string UserName, string Password, string Thumbprint, object[] Itens, out string xmlContainer, out string xmlrequest)
        {
            SubscriptionRequestContainer requestContainer = new SubscriptionRequestContainer();
            requestContainer.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            requestContainer.TransactionId = _TransactionId; // "010000002";

            SubscriptionRequestRequest request = new SubscriptionRequestRequest();
            request.AccessInfo = new AccessInfo() { UserName = UserName, Password = Password };

            requestContainer.SubscriptionRequest = Itens;
            X509Certificate2 cert;
            KasperskySubscriptionServicePortTypeClient client = new KasperskySubscriptionServicePortTypeClient();

            if (System.Configuration.ConfigurationManager.AppSettings["ambiente_Producao"].ToString() == "1")
            {
                X509Store myX509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                myX509Store.Open(OpenFlags.ReadOnly);
                X509Certificate2 myCertificate = myX509Store.Certificates.OfType<X509Certificate2>().FirstOrDefault(certi => certi.Thumbprint.ToLower() == Thumbprint.ToLower());
                client.ClientCredentials.ClientCertificate.Certificate = myCertificate;
            }

            else
            {
                cert = new X509Certificate2(@"F:/certificado/kss_cert_CN=uol_edtech, O=Itailers_20210308.pfx", "@Itailers2021#", X509KeyStorageFlags.PersistKeySet);
                client.ClientCredentials.ClientCertificate.Certificate = cert;
            }


            ((BasicHttpBinding)client.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

            XmlSerializer xsSubmit = new XmlSerializer(typeof(SubscriptionRequestContainer));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, requestContainer);
                    xmlContainer = sww.ToString(); // Your XML
                }
            }

            XmlSerializer xsSubmitrequest = new XmlSerializer(typeof(SubscriptionRequestRequest));

            using (var swwrequest = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(swwrequest))
                {
                    xsSubmitrequest.Serialize(writer, request);
                    xmlrequest = swwrequest.ToString(); // Your XML
                }
            }

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = client.SubscriptionRequest(request.AccessInfo, requestContainer);

            return container;
        }

        public SubscriptionResponseContainer AtivacaoNeo(string _TransactionId, string _ProductId, string _SubscriberId, string qtd_licencas, DateTime dateTime, out string xmlContainer, out string xmlRequest)
        {
            SubscriptionRequestContainer requestContainer = new SubscriptionRequestContainer();
            requestContainer.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            requestContainer.TransactionId = _TransactionId; // "010000002";

            SubscriptionRequestRequest request = new SubscriptionRequestRequest();
            request.AccessInfo = new AccessInfo() { UserName = "freenet-br", Password = "@Itailers2021#" };

            SubscriptionRequestActivateItem activateItem = new SubscriptionRequestActivateItem();

            activateItem.ActivationType = ActivationTypeEnum.Standard;
            activateItem.LicenseCount = qtd_licencas;
            activateItem.ProductId = _ProductId; // "KISMD";
            activateItem.StartTime = DateTime.Parse(DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd") + "T" + DateTime.Now.AddMinutes(5).ToString("HH:mm:ss.ffffff") + "Z");  //DateTime.Now;
            activateItem.EndTime = "indefinite"; //dateTime.ToString("yyyy -MM-dd") + "T" + dateTime.ToString("HH:mm:ss.ffffff") + "Z";
            activateItem.UnitId = "1";
            activateItem.SubscriberId = _SubscriberId; // "C8147E02-8A91-43B2-AD48-5D04DAFD38C2";

            object[] obj = new object[1];

            obj[0] = activateItem;

            requestContainer.SubscriptionRequest = obj;
            X509Certificate2 cert;
            KasperskySubscriptionServicePortTypeClient client = new KasperskySubscriptionServicePortTypeClient();

            if (System.Configuration.ConfigurationManager.AppSettings["ambiente_Producao"].ToString() == "1")
            {
                X509Store myX509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                myX509Store.Open(OpenFlags.ReadOnly);
                X509Certificate2 myCertificate = myX509Store.Certificates.OfType<X509Certificate2>().FirstOrDefault(certi => certi.Thumbprint.ToLower() == "bfb8799d126d3be12f759dcc68d6243578d80b28");
                client.ClientCredentials.ClientCertificate.Certificate = myCertificate;
                //client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, "b14be2f3364e4ff886ef5c07c1074b1898653fa7");
            }

            else
            {
                cert = new X509Certificate2(@"kss_cert_CN=freenet-itailers-br_20211111.pfx", "@Itailers2021#", X509KeyStorageFlags.PersistKeySet);
                client.ClientCredentials.ClientCertificate.Certificate = cert;
            }


            ((BasicHttpBinding)client.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;


            //client.ClientCredentials.HttpDigest.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Anonymous;
            //client.ClientCredentials.HttpDigest.ClientCredential.UserName = "linktel_br";
            //client.ClientCredentials.HttpDigest.ClientCredential.Password = "3G2%4H14i9xgrd@";
          

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = client.SubscriptionRequest(request.AccessInfo, requestContainer);

            XmlSerializer xsSubmit = new XmlSerializer(typeof(SubscriptionRequestContainer));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, requestContainer);
                    xmlContainer = sww.ToString(); // Your XML
                }
            }

            XmlSerializer xsSubmitrequest = new XmlSerializer(typeof(SubscriptionRequestRequest));

            using (var swwrequest = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(swwrequest))
                {
                    xsSubmitrequest.Serialize(writer, request);
                    xmlRequest = swwrequest.ToString(); // Your XML
                }
            }

            return container;
        }

        public SubscriptionResponseContainer AtivacaoNeoLote(string _TransactionId, List<Neo_Produto_Combo> produtos, DateTime dateTime, out string xmlContainer, out string xmlRequest)
        {
            SubscriptionRequestContainer requestContainer = new SubscriptionRequestContainer();
            requestContainer.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            requestContainer.TransactionId = _TransactionId; // "010000002";

            SubscriptionRequestRequest request = new SubscriptionRequestRequest();
            request.AccessInfo = new AccessInfo() { UserName = "freenet-br", Password = "@Itailers2021#" };

            List<object> incricoes = new List<object>();

            foreach (var produto in produtos)
            {
                SubscriptionRequestActivateItem activateItem = new SubscriptionRequestActivateItem();

                activateItem.ActivationType = ActivationTypeEnum.Standard;
                activateItem.LicenseCount = produto.qtd_licencas;
                activateItem.ProductId = produto._ProductId; // "KISMD";
                activateItem.StartTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");  //DateTime.Now;
                activateItem.EndTime = "indefinite"; //dateTime.ToString("yyyy -MM-dd") + "T" + dateTime.ToString("HH:mm:ss.ffffff") + "Z";
                activateItem.UnitId = produto.UnitId;
                activateItem.SubscriberId = produto._SubscriberId; // "C8147E02-8A91-43B2-AD48-5D04DAFD38C2";

                incricoes.Add(activateItem);
            }

            requestContainer.SubscriptionRequest = incricoes.ToArray();

            X509Certificate2 cert;
            KasperskySubscriptionServicePortTypeClient client = new KasperskySubscriptionServicePortTypeClient();

            if (System.Configuration.ConfigurationManager.AppSettings["ambiente_Producao"].ToString() == "1")
            {
                X509Store myX509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                myX509Store.Open(OpenFlags.ReadOnly);
                X509Certificate2 myCertificate = myX509Store.Certificates.OfType<X509Certificate2>().FirstOrDefault(certi => certi.Thumbprint.ToLower() == "bfb8799d126d3be12f759dcc68d6243578d80b28");
                client.ClientCredentials.ClientCertificate.Certificate = myCertificate;
                //client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, "b14be2f3364e4ff886ef5c07c1074b1898653fa7");
            }

            else
            {
                cert = new X509Certificate2(@"kss_cert_CN=freenet-itailers-br_20211111.pfx", "@Itailers2021#", X509KeyStorageFlags.PersistKeySet);
                client.ClientCredentials.ClientCertificate.Certificate = cert;
            }


            ((BasicHttpBinding)client.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;


            //client.ClientCredentials.HttpDigest.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Anonymous;
            //client.ClientCredentials.HttpDigest.ClientCredential.UserName = "linktel_br";
            //client.ClientCredentials.HttpDigest.ClientCredential.Password = "3G2%4H14i9xgrd@";

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = client.SubscriptionRequest(request.AccessInfo, requestContainer);

            XmlSerializer xsSubmit = new XmlSerializer(typeof(SubscriptionRequestContainer));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, requestContainer);
                    xmlContainer = sww.ToString(); // Your XML
                }
            }

            XmlSerializer xsSubmitrequest = new XmlSerializer(typeof(SubscriptionRequestRequest));

            using (var swwrequest = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(swwrequest))
                {
                    xsSubmitrequest.Serialize(writer, request);
                    xmlRequest = swwrequest.ToString(); // Your XML
                }
            }

            return container;
        }

        public SubscriptionResponseContainer DownloadLinkNeo(PlatformEnum platform, string _SubscriberId, string _TransactionId, out string xmlContainer, out string xmlRequest)
        {
            SubscriptionRequestContainer requestContainer = new SubscriptionRequestContainer();
            requestContainer.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            requestContainer.TransactionId = _TransactionId; // "010000002";

            SubscriptionRequestRequest request = new SubscriptionRequestRequest();
            request.AccessInfo = new AccessInfo() { UserName = "freenet-br", Password = "@Itailers2021#" };

            SubscriptionRequestGetDownloadLinksItem Item = new SubscriptionRequestGetDownloadLinksItem();

            Item.UnitId = "1";
            Item.SubscriberId = _SubscriberId; // "C8147E02-8A91-43B2-AD48-5D04DAFD38C2";
            Item.Language = "pt";
            Item.Platform = platform;

            object[] obj = new object[1];

            obj[0] = Item;

            requestContainer.SubscriptionRequest = obj;
            X509Certificate2 cert;
            KasperskySubscriptionServicePortTypeClient client = new KasperskySubscriptionServicePortTypeClient();

            if (System.Configuration.ConfigurationManager.AppSettings["ambiente_Producao"].ToString() == "1")
            {
                X509Store myX509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                myX509Store.Open(OpenFlags.ReadOnly);
                X509Certificate2 myCertificate = myX509Store.Certificates.OfType<X509Certificate2>().FirstOrDefault(certi => certi.Thumbprint.ToLower() == "bfb8799d126d3be12f759dcc68d6243578d80b28");
                client.ClientCredentials.ClientCertificate.Certificate = myCertificate;
                //client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, "b14be2f3364e4ff886ef5c07c1074b1898653fa7");
            }

            else
            {
                cert = new X509Certificate2(@"F:/certificado/kss_cert_CN=freenet-itailers-br_20211111.pfx", "@Itailers2021", X509KeyStorageFlags.PersistKeySet);
                client.ClientCredentials.ClientCertificate.Certificate = cert;
            }
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = client.SubscriptionRequest(request.AccessInfo, requestContainer);

            XmlSerializer xsSubmit = new XmlSerializer(typeof(SubscriptionRequestContainer));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, requestContainer);
                    xmlContainer = sww.ToString(); // Your XML
                }
            }

            XmlSerializer xsSubmitrequest = new XmlSerializer(typeof(SubscriptionRequestRequest));

            using (var swwrequest = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(swwrequest))
                {
                    xsSubmitrequest.Serialize(writer, request);
                    xmlRequest = swwrequest.ToString(); // Your XML
                }
            }

            return container;
        }

        public SubscriptionResponseContainer DownloadLinkNeoLote(string _TransactionId, object[] obj, out string xmlContainer, out string xmlRequest)
        {
            SubscriptionRequestContainer requestContainer = new SubscriptionRequestContainer();
            requestContainer.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            requestContainer.TransactionId = _TransactionId; // "010000002";

            SubscriptionRequestRequest request = new SubscriptionRequestRequest();
            request.AccessInfo = new AccessInfo() { UserName = "freenet-br", Password = "@Itailers2021#" };

            requestContainer.SubscriptionRequest = obj;
            X509Certificate2 cert;
            KasperskySubscriptionServicePortTypeClient client = new KasperskySubscriptionServicePortTypeClient();

            if (System.Configuration.ConfigurationManager.AppSettings["ambiente_Producao"].ToString() == "1")
            {
                X509Store myX509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                myX509Store.Open(OpenFlags.ReadOnly);
                X509Certificate2 myCertificate = myX509Store.Certificates.OfType<X509Certificate2>().FirstOrDefault(certi => certi.Thumbprint.ToLower() == "bfb8799d126d3be12f759dcc68d6243578d80b28");
                client.ClientCredentials.ClientCertificate.Certificate = myCertificate;
                //client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, "b14be2f3364e4ff886ef5c07c1074b1898653fa7");
            }

            else
            {
                cert = new X509Certificate2(@"F:/certificado/kss_cert_CN=freenet-itailers-br_20211111.pfx", "@Itailers2021", X509KeyStorageFlags.PersistKeySet);
                client.ClientCredentials.ClientCertificate.Certificate = cert;
            }
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = client.SubscriptionRequest(request.AccessInfo, requestContainer);

            XmlSerializer xsSubmit = new XmlSerializer(typeof(SubscriptionRequestContainer));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, requestContainer);
                    xmlContainer = sww.ToString(); // Your XML
                }
            }

            XmlSerializer xsSubmitrequest = new XmlSerializer(typeof(SubscriptionRequestRequest));

            using (var swwrequest = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(swwrequest))
                {
                    xsSubmitrequest.Serialize(writer, request);
                    xmlRequest = swwrequest.ToString(); // Your XML
                }
            }

            return container;
        }

        public void Renovacao(string _TransactionId, string _ProductId, string _SubscriberId)
        {
            SubscriptionRequestContainer requestContainer = new SubscriptionRequestContainer();
            requestContainer.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            requestContainer.TransactionId = _TransactionId; // "010000002";

            SubscriptionRequestRequest request = new SubscriptionRequestRequest();
            request.AccessInfo = new AccessInfo() { UserName = "freenet-br", Password = "@Itailers2021#" };

            SubscriptionRequestRenewItem Item = new SubscriptionRequestRenewItem();

            Item.EndTime = "indefinite"; //DateTime.Now.ad.ToString("yyyy -MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z";  //DateTime.Now.AddYears(1).ToString("yyyy-MM-dd HH:mm");
            Item.UnitId = "1";
            Item.SubscriberId = _SubscriberId; // "C8147E02-8A91-43B2-AD48-5D04DAFD38C2";

            object[] obj = new object[1];

            obj[0] = Item;

            requestContainer.SubscriptionRequest = obj;

            KasperskySubscriptionServicePortTypeClient client = new KasperskySubscriptionServicePortTypeClient();

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = client.SubscriptionRequest(request.AccessInfo, requestContainer);
        }

        public SubscriptionResponseContainer Cancelamento(string _TransactionId, string _SubscriberId)
        {

            SubscriptionRequestContainer requestContainer = new SubscriptionRequestContainer();
            requestContainer.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            requestContainer.TransactionId = _TransactionId; // "010000002";

            SubscriptionRequestRequest request = new SubscriptionRequestRequest();
            request.AccessInfo = new AccessInfo() { UserName = "linktel_br", Password = "3G2%4H14i9xgrd@" };

            SubscriptionRequestHardCancelItem activateItem = new SubscriptionRequestHardCancelItem();

            activateItem.EndTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z"); 
            activateItem.SubscriberId = _SubscriberId;
            activateItem.UnitId = "1"; // "KISMD";

            object[] obj = new object[1];

            obj[0] = activateItem;

            requestContainer.SubscriptionRequest = obj;
            X509Certificate2 cert;

            if (System.Configuration.ConfigurationManager.AppSettings["ambiente_web"].ToString() == "1")
                cert = new X509Certificate2(HttpContext.Current.Server.MapPath("~/Email/Itailers.pfx"), "+P47c-!3&v7!", X509KeyStorageFlags.PersistKeySet);
            else
                cert = new X509Certificate2(@"C:/certificado/Itailers.pfx", "+P47c-!3&v7!", X509KeyStorageFlags.PersistKeySet);

            KasperskySubscriptionServicePortTypeClient client = new KasperskySubscriptionServicePortTypeClient();
            client.ClientCredentials.ClientCertificate.Certificate = cert;
            client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByIssuerName, "Kaspersky Subscription Service CA G3");
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

            //client.ClientCredentials.HttpDigest.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Anonymous;
            //client.ClientCredentials.HttpDigest.ClientCredential.UserName = "linktel_br";
            //client.ClientCredentials.HttpDigest.ClientCredential.Password = "3G2%4H14i9xgrd@";

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = client.SubscriptionRequest(request.AccessInfo, requestContainer);

            return container;

            /*SubscriptionRequestContainer requestContainer = new SubscriptionRequestContainer();
            requestContainer.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            requestContainer.TransactionId = _TransactionId; // "010000002";

            SubscriptionRequestRequest request = new SubscriptionRequestRequest();
            request.AccessInfo = new AccessInfo() { UserName = "linktel_br", Password = "3G2%4H14i9xgrd@" };

            SubscriptionRequestSoftCancelItem Item = new SubscriptionRequestSoftCancelItem();

            Item.EndTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z"); //DateTime.Now.ad.ToString("yyyy -MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z";  //DateTime.Now.AddYears(1).ToString("yyyy-MM-dd HH:mm");
            Item.UnitId = "1";
            Item.SubscriberId = _SubscriberId; // "C8147E02-8A91-43B2-AD48-5D04DAFD38C2";

            object[] obj = new object[1];

            obj[0] = Item;

            requestContainer.SubscriptionRequest = obj;

            KasperskySubscriptionServicePortTypeClient client = new KasperskySubscriptionServicePortTypeClient();

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = client.SubscriptionRequest(request.AccessInfo, requestContainer);

            bool sucesso = true;
            foreach (object ret in container.Items)
            {
                try
                {
                    SubscriptionResponseItemCollection item = new SubscriptionResponseItemCollection();
                    item = (SubscriptionResponseItemCollection)ret;

                    SubscriptionRequestSoftCancelItem ativo = new SubscriptionRequestSoftCancelItem();

                    ativo = (SubscriptionRequestSoftCancelItem)item.Items[0];

                    Generico.log_inserir(7, "KL cancelamento de assinatura - " + _TransactionId + " subscricao " + _SubscriberId, 0);

                    return "";
                }
                catch
                {
                    sucesso = false;
                }

                if (!sucesso)
                {
                    try
                    {
                        SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                        itemErro = (SubscriptionResponseErrorCollection)ret;

                        Generico.log_inserir(7, "Erro no KL cancelamento de assinatura - " + _TransactionId + " subscricao " + _SubscriberId + " " + itemErro.Items[0].ErrorMessage, 0);

                        return "NOK";
                    }
                    catch
                    {
                        return "NOK ERRO";
                    }
                }
            }

            return "NULL";*/
        }

        public SubscriptionResponseContainer CancelamentoUOL(string _TransactionId, string _SubscriberId)
        {

            SubscriptionRequestContainer requestContainer = new SubscriptionRequestContainer();
            requestContainer.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            requestContainer.TransactionId = _TransactionId; // "010000002";

            SubscriptionRequestRequest request = new SubscriptionRequestRequest();
            request.AccessInfo = new AccessInfo() { UserName = "uol_edtech_30d", Password = "@Itailers2021#" };

            SubscriptionRequestHardCancelItem activateItem = new SubscriptionRequestHardCancelItem();

            activateItem.EndTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            activateItem.SubscriberId = _SubscriberId;
            activateItem.UnitId = "1"; // "KISMD";

            object[] obj = new object[1];

            obj[0] = activateItem;

            requestContainer.SubscriptionRequest = obj;
            X509Certificate2 cert;
            KasperskySubscriptionServicePortTypeClient client = new KasperskySubscriptionServicePortTypeClient();

            if (System.Configuration.ConfigurationManager.AppSettings["ambiente_Producao"].ToString() == "1")
            {
                X509Store myX509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                myX509Store.Open(OpenFlags.ReadOnly);
                X509Certificate2 myCertificate = myX509Store.Certificates.OfType<X509Certificate2>().FirstOrDefault(certi => certi.Thumbprint.ToLower() == "b14be2f3364e4ff886ef5c07c1074b1898653fa7");
                client.ClientCredentials.ClientCertificate.Certificate = myCertificate;
                //client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, "b14be2f3364e4ff886ef5c07c1074b1898653fa7");
            }

            else
            {
                cert = new X509Certificate2(@"C:/certificado/kss_cert_CN=uol_edtech, O=Itailers_20210308.pfx", "@Itailers2021#", X509KeyStorageFlags.PersistKeySet);
                client.ClientCredentials.ClientCertificate.Certificate = cert;
            }

            ((BasicHttpBinding)client.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = client.SubscriptionRequest(request.AccessInfo, requestContainer);

            return container;

            /*SubscriptionRequestContainer requestContainer = new SubscriptionRequestContainer();
            requestContainer.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            requestContainer.TransactionId = _TransactionId; // "010000002";

            SubscriptionRequestRequest request = new SubscriptionRequestRequest();
            request.AccessInfo = new AccessInfo() { UserName = "linktel_br", Password = "3G2%4H14i9xgrd@" };

            SubscriptionRequestSoftCancelItem Item = new SubscriptionRequestSoftCancelItem();

            Item.EndTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z"); //DateTime.Now.ad.ToString("yyyy -MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z";  //DateTime.Now.AddYears(1).ToString("yyyy-MM-dd HH:mm");
            Item.UnitId = "1";
            Item.SubscriberId = _SubscriberId; // "C8147E02-8A91-43B2-AD48-5D04DAFD38C2";

            object[] obj = new object[1];

            obj[0] = Item;

            requestContainer.SubscriptionRequest = obj;

            KasperskySubscriptionServicePortTypeClient client = new KasperskySubscriptionServicePortTypeClient();

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = client.SubscriptionRequest(request.AccessInfo, requestContainer);

            bool sucesso = true;
            foreach (object ret in container.Items)
            {
                try
                {
                    SubscriptionResponseItemCollection item = new SubscriptionResponseItemCollection();
                    item = (SubscriptionResponseItemCollection)ret;

                    SubscriptionRequestSoftCancelItem ativo = new SubscriptionRequestSoftCancelItem();

                    ativo = (SubscriptionRequestSoftCancelItem)item.Items[0];

                    Generico.log_inserir(7, "KL cancelamento de assinatura - " + _TransactionId + " subscricao " + _SubscriberId, 0);

                    return "";
                }
                catch
                {
                    sucesso = false;
                }

                if (!sucesso)
                {
                    try
                    {
                        SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                        itemErro = (SubscriptionResponseErrorCollection)ret;

                        Generico.log_inserir(7, "Erro no KL cancelamento de assinatura - " + _TransactionId + " subscricao " + _SubscriberId + " " + itemErro.Items[0].ErrorMessage, 0);

                        return "NOK";
                    }
                    catch
                    {
                        return "NOK ERRO";
                    }
                }
            }

            return "NULL";*/
        }

        public SubscriptionResponseContainer GetInfoNeo(string _TransactionId, string[] _SubscriberId, out string xmlContainer, out string xmlRequest)
        {

            SubscriptionRequestContainer requestContainer = new SubscriptionRequestContainer();
            requestContainer.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            requestContainer.TransactionId = _TransactionId; // "010000002";

            SubscriptionRequestRequest request = new SubscriptionRequestRequest();
            request.AccessInfo = new AccessInfo() { UserName = "freenet-br", Password = "@Itailers2021#" };

            List<object> lista = new List<object>();
            int cont = 1; 

            foreach (var item in _SubscriberId)
            {
                SubscriptionRequestGetInfoItem activateItem = new SubscriptionRequestGetInfoItem();

                activateItem.SubscriberId = item;
                activateItem.UnitId = cont.ToString(); // "KISMD";
                activateItem.InfoSection = SubscriptionInfoType.SubscriptionSubscriptionUsage;
                cont++;

                lista.Add(activateItem);
            }
           

            requestContainer.SubscriptionRequest = lista.ToArray();
            X509Certificate2 cert;
            KasperskySubscriptionServicePortTypeClient client = new KasperskySubscriptionServicePortTypeClient();

            if (System.Configuration.ConfigurationManager.AppSettings["ambiente_Producao"].ToString() == "1")
            {
                X509Store myX509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                myX509Store.Open(OpenFlags.ReadOnly);
                X509Certificate2 myCertificate = myX509Store.Certificates.OfType<X509Certificate2>().FirstOrDefault(certi => certi.Thumbprint.ToLower() == "bfb8799d126d3be12f759dcc68d6243578d80b28");
                client.ClientCredentials.ClientCertificate.Certificate = myCertificate;
                //client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, "b14be2f3364e4ff886ef5c07c1074b1898653fa7");
            }

            else
            {
                cert = new X509Certificate2(@"F:/certificado/kss_cert_CN=freenet-itailers-br_20211111.pfx", "@Itailers2021", X509KeyStorageFlags.PersistKeySet);
                client.ClientCredentials.ClientCertificate.Certificate = cert;
            }

            ((BasicHttpBinding)client.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = client.SubscriptionRequest(request.AccessInfo, requestContainer);

            XmlSerializer xsSubmit = new XmlSerializer(typeof(SubscriptionRequestContainer));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, requestContainer);
                    xmlContainer = sww.ToString(); // Your XML
                }
            }

            XmlSerializer xsSubmitrequest = new XmlSerializer(typeof(SubscriptionRequestRequest));

            using (var swwrequest = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(swwrequest))
                {
                    xsSubmitrequest.Serialize(writer, request);
                    xmlRequest = swwrequest.ToString(); // Your XML
                }
            }

            return container;

        }

        public SubscriptionResponseContainer CancelamentoNeo(string _TransactionId, string _SubscriberId, out string xmlContainer, out string xmlRequest)
        {
            SubscriptionRequestContainer requestContainer = new SubscriptionRequestContainer();
            requestContainer.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            requestContainer.TransactionId = _TransactionId; // "010000002";

            SubscriptionRequestRequest request = new SubscriptionRequestRequest();
            request.AccessInfo = new AccessInfo() { UserName = "freenet-br", Password = "@Itailers2021#" };

            SubscriptionRequestHardCancelItem activateItem = new SubscriptionRequestHardCancelItem();

            activateItem.EndTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            activateItem.SubscriberId = _SubscriberId;
            activateItem.UnitId = "1"; // "KISMD";

            object[] obj = new object[1];

            obj[0] = activateItem;

            requestContainer.SubscriptionRequest = obj;
            X509Certificate2 cert;
            KasperskySubscriptionServicePortTypeClient client = new KasperskySubscriptionServicePortTypeClient();

            if (System.Configuration.ConfigurationManager.AppSettings["ambiente_Producao"].ToString() == "1")
            {
                X509Store myX509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                myX509Store.Open(OpenFlags.ReadOnly);
                X509Certificate2 myCertificate = myX509Store.Certificates.OfType<X509Certificate2>().FirstOrDefault(certi => certi.Thumbprint.ToLower() == "bfb8799d126d3be12f759dcc68d6243578d80b28");
                client.ClientCredentials.ClientCertificate.Certificate = myCertificate;
                //client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, "b14be2f3364e4ff886ef5c07c1074b1898653fa7");
            }

            else
            {
                cert = new X509Certificate2(@"F:/certificado/kss_cert_CN=freenet-itailers-br_20211111.pfx", "@Itailers2021", X509KeyStorageFlags.PersistKeySet);
                client.ClientCredentials.ClientCertificate.Certificate = cert;
            }

            ((BasicHttpBinding)client.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = client.SubscriptionRequest(request.AccessInfo, requestContainer);

            XmlSerializer xsSubmit = new XmlSerializer(typeof(SubscriptionRequestContainer));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, requestContainer);
                    xmlContainer = sww.ToString(); // Your XML
                }
            }

            XmlSerializer xsSubmitrequest = new XmlSerializer(typeof(SubscriptionRequestRequest));

            using (var swwrequest = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(swwrequest))
                {
                    xsSubmitrequest.Serialize(writer, request);
                    xmlRequest = swwrequest.ToString(); // Your XML
                }
            }

            return container;

            /*SubscriptionRequestContainer requestContainer = new SubscriptionRequestContainer();
            requestContainer.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            requestContainer.TransactionId = _TransactionId; // "010000002";

            SubscriptionRequestRequest request = new SubscriptionRequestRequest();
            request.AccessInfo = new AccessInfo() { UserName = "linktel_br", Password = "3G2%4H14i9xgrd@" };

            SubscriptionRequestSoftCancelItem Item = new SubscriptionRequestSoftCancelItem();

            Item.EndTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z"); //DateTime.Now.ad.ToString("yyyy -MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z";  //DateTime.Now.AddYears(1).ToString("yyyy-MM-dd HH:mm");
            Item.UnitId = "1";
            Item.SubscriberId = _SubscriberId; // "C8147E02-8A91-43B2-AD48-5D04DAFD38C2";

            object[] obj = new object[1];

            obj[0] = Item;

            requestContainer.SubscriptionRequest = obj;

            KasperskySubscriptionServicePortTypeClient client = new KasperskySubscriptionServicePortTypeClient();

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = client.SubscriptionRequest(request.AccessInfo, requestContainer);

            bool sucesso = true;
            foreach (object ret in container.Items)
            {
                try
                {
                    SubscriptionResponseItemCollection item = new SubscriptionResponseItemCollection();
                    item = (SubscriptionResponseItemCollection)ret;

                    SubscriptionRequestSoftCancelItem ativo = new SubscriptionRequestSoftCancelItem();

                    ativo = (SubscriptionRequestSoftCancelItem)item.Items[0];

                    Generico.log_inserir(7, "KL cancelamento de assinatura - " + _TransactionId + " subscricao " + _SubscriberId, 0);

                    return "";
                }
                catch
                {
                    sucesso = false;
                }

                if (!sucesso)
                {
                    try
                    {
                        SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                        itemErro = (SubscriptionResponseErrorCollection)ret;

                        Generico.log_inserir(7, "Erro no KL cancelamento de assinatura - " + _TransactionId + " subscricao " + _SubscriberId + " " + itemErro.Items[0].ErrorMessage, 0);

                        return "NOK";
                    }
                    catch
                    {
                        return "NOK ERRO";
                    }
                }
            }

            return "NULL";*/
        }

        public SubscriptionResponseContainer CancelamentoNeoLote(string _TransactionId, string[] _SubscriberId, out string xmlContainer, out string xmlRequest)
        {
            SubscriptionRequestContainer requestContainer = new SubscriptionRequestContainer();
            requestContainer.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            requestContainer.TransactionId = _TransactionId; // "010000002";

            SubscriptionRequestRequest request = new SubscriptionRequestRequest();
            request.AccessInfo = new AccessInfo() { UserName = "freenet-br", Password = "@Itailers2021#" };

            List<object> lista = new List<object>();
            int cont = 1;

            foreach (var item in _SubscriberId)
            {
                SubscriptionRequestHardCancelItem activateItem = new SubscriptionRequestHardCancelItem();

                activateItem.EndTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
                activateItem.SubscriberId = item;
                activateItem.UnitId = cont.ToString(); // "KISMD";
                cont++;
                lista.Add(activateItem);
            }

            requestContainer.SubscriptionRequest = lista.ToArray();
            X509Certificate2 cert;
            KasperskySubscriptionServicePortTypeClient client = new KasperskySubscriptionServicePortTypeClient();

            if (System.Configuration.ConfigurationManager.AppSettings["ambiente_Producao"].ToString() == "1")
            {
                X509Store myX509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                myX509Store.Open(OpenFlags.ReadOnly);
                X509Certificate2 myCertificate = myX509Store.Certificates.OfType<X509Certificate2>().FirstOrDefault(certi => certi.Thumbprint.ToLower() == "bfb8799d126d3be12f759dcc68d6243578d80b28");
                client.ClientCredentials.ClientCertificate.Certificate = myCertificate;
                //client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, "b14be2f3364e4ff886ef5c07c1074b1898653fa7");
            }

            else
            {
                cert = new X509Certificate2(@"F:/certificado/kss_cert_CN=freenet-itailers-br_20211111.pfx", "@Itailers2021", X509KeyStorageFlags.PersistKeySet);
                client.ClientCredentials.ClientCertificate.Certificate = cert;
            }

            ((BasicHttpBinding)client.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = client.SubscriptionRequest(request.AccessInfo, requestContainer);

            XmlSerializer xsSubmit = new XmlSerializer(typeof(SubscriptionRequestContainer));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, requestContainer);
                    xmlContainer = sww.ToString(); // Your XML
                }
            }

            XmlSerializer xsSubmitrequest = new XmlSerializer(typeof(SubscriptionRequestRequest));

            using (var swwrequest = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(swwrequest))
                {
                    xsSubmitrequest.Serialize(writer, request);
                    xmlRequest = swwrequest.ToString(); // Your XML
                }
            }

            return container;
            
        }

        public SubscriptionResponseContainer DownloadLink(PlatformEnum platform, string _SubscriberId, string _TransactionId)
        {
            SubscriptionRequestContainer requestContainer = new SubscriptionRequestContainer();
            requestContainer.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.ffffff") + "Z");
            requestContainer.TransactionId = _TransactionId; // "010000002";

            SubscriptionRequestRequest request = new SubscriptionRequestRequest();
            if (System.Configuration.ConfigurationManager.AppSettings["ambiente_Producao"].ToString() == "1")
                request.AccessInfo = new AccessInfo() { UserName = "uol_edtech_30d", Password = "@Itailers2021#" };
            else
                request.AccessInfo = new AccessInfo() { UserName = "uol_edtech_30d", Password = "@Itailers2021#" };

            SubscriptionRequestGetDownloadLinksItem Item = new SubscriptionRequestGetDownloadLinksItem();

            Item.UnitId = "1";
            Item.SubscriberId = _SubscriberId; // "C8147E02-8A91-43B2-AD48-5D04DAFD38C2";
            Item.Language = "pt";
            Item.Platform = platform;

            object[] obj = new object[1];

            obj[0] = Item;

            requestContainer.SubscriptionRequest = obj;
            X509Certificate2 cert;
            KasperskySubscriptionServicePortTypeClient client = new KasperskySubscriptionServicePortTypeClient();

            if (System.Configuration.ConfigurationManager.AppSettings["ambiente_Producao"].ToString() == "1")
            {
                X509Store myX509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                myX509Store.Open(OpenFlags.ReadOnly);
                X509Certificate2 myCertificate = myX509Store.Certificates.OfType<X509Certificate2>().FirstOrDefault(certi => certi.Thumbprint.ToLower() == "240d0eb8d41bde38e10c4ac8370d800cf00b9070");
                client.ClientCredentials.ClientCertificate.Certificate = myCertificate;
                //client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, "b14be2f3364e4ff886ef5c07c1074b1898653fa7");
            }

            else
            {
                cert = new X509Certificate2(@"F:/certificado/kss_cert_CN=uol_edtech, O=Itailers_20210308.pfx", "@Itailers2021#", X509KeyStorageFlags.PersistKeySet);
                client.ClientCredentials.ClientCertificate.Certificate = cert;
            }
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            ((BasicHttpBinding)client.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

            SubscriptionResponseContainer container = new SubscriptionResponseContainer();
            container = client.SubscriptionRequest(request.AccessInfo, requestContainer);

            return container;
        }

        
    }
}
