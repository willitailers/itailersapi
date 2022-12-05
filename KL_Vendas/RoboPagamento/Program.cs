using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BLL;
using PagarMe;
using DAL;

namespace RoboPagamento
{
    class Program
    {
        static DateTime dt_ultima = DateTime.Now.Date;

        static void Main(string[] args)
        {
            bool log = System.Configuration.ConfigurationManager.AppSettings["log"] == "1";
            bool ambiente_web = System.Configuration.ConfigurationManager.AppSettings["ambiente_web"] == "1";
            string passo = "";
            while (true)
            {
                try
                {
                    VerificaPago(ambiente_web, log);

                    VerificaCancelados(ambiente_web, log);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Erro no processo: " + passo + " - erro: " + ex.Message);
                }
                finally
                {
                    Console.WriteLine("Sleep " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    Thread.Sleep(1000 * 60 * 5);
                }
            }

        }

        static void VerificaPago(bool ambiente_web, bool log)
        {
            string passo = "";
            try
            {
                Console.WriteLine("Inicio do Looping Pagamentos" + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                // consulta lista de pagamentos pendentes
                DataTable dt = new DataTable();
                string cd_ativacao = "";

                passo = "Consulta pagamentos pendentes";
                dt = new Pagamentos_BLL().pagamento_consulta(DateTime.Now.AddMonths(-12), DateTime.Now.AddDays(1), 1, "");

                Console.WriteLine("Numeros de pagamentos pendentes: " + dt.Rows.Count.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    cd_ativacao = "";
                    SubscriptionStatus status;
                    // consulta pagamento

                    passo = "Consulta assinatura " + dr["subscribe_id"].ToString();
                    status = new PagarMe_BLL().consulta_pagamento_assinatura(dr["subscribe_id"].ToString());



                    // Aprova os pagamentos
                    if (status == SubscriptionStatus.Paid || status == SubscriptionStatus.Ended)
                    {
                        passo = "Aceitar pagamento " + dr["id_compra"].ToString();
                        new Carrinho_DAL().compra_pagamento_aceito(dr["id_compra"].ToString(), "0", "2");


                        // envia email com o cd ativação
                        if (dr["cd_ativacao"].ToString() == "-")
                        {
                            passo = "Ativar produto " + dr["id_compra"].ToString();
                            cd_ativacao = new KL_Conexao().ativar_compra(dr["id_carrinho"].ToString());
                        }
                        else
                            cd_ativacao = dr["cd_ativacao"].ToString();

                        passo = "Email pagamento confirmado " + dr["id_compra"].ToString();
                        string email = new Email().pagamento_aprovado(dr["nm_cliente"].ToString(),
                                                                    dr["nm_caminho_img"].ToString(),
                                                                    dr["nm_produto"].ToString(),
                                                                    dr["qtd_licencas"].ToString(),
                                                                    cd_ativacao,
                                                                    dr["nome"].ToString(),
                                                                    dr["nm_email"].ToString(),
                                                                    ambiente_web);

                        new Email().EnviaEmail(dr["nm_email"].ToString(), "", email, new Comum().email_titulo_pagamento_aprovado);

                        if (dr["dv_wifi"].ToString() == "1")
                        {
                            // gera senha
                            Random random = new Random();
                            int randomNumber = random.Next(111111, 999999);

                            // cadastra e-mail
                            LinkTel lkt = new LinkTel();

                            var diffInSeconds = (DateTime.Now.Date.AddMonths(1) - DateTime.Now.Date).TotalSeconds;

                            bool acesso_lktt = lkt.Acesso_Wifi(dr["nm_email"].ToString(),
                                          randomNumber.ToString(),
                                    dr["nm_cliente"].ToString().Substring(0, dr["nm_cliente"].ToString().IndexOf(" ")),
                                    dr["nm_cliente"].ToString().Substring(dr["nm_cliente"].ToString().IndexOf(" ") + 1),
                                    dr["v_sexo"].ToString(),
                                    "Brasil",
                                    dr["nr_cpf"].ToString(),
                                    dr["nm_produto_sub_descr_completa2"].ToString(), 
                                    DateTime.Parse(dr["dt_nascimento"].ToString()).ToString("yyyy-MM-dd"),
                                    Int64.Parse(diffInSeconds.ToString().Replace(",", "."))
                                );

                            if (acesso_lktt)
                            {
                                Generico.log_inserir(90, "Senha de e-mail gerada: " + dr["nm_email"].ToString() + " - " + randomNumber.ToString(), 2);

                                new Compra_BLL().wifi_confirma(dr["id_compra"].ToString());

                                // envia e-mail
                                email = new Email().wifi_senha(dr["nm_cliente"].ToString(),
                                                                            dr["nm_email"].ToString(), randomNumber.ToString(),
                                                                            false);

                                new Email().EnviaEmail(dr["nm_email"].ToString(), "", email, new Comum().email_titulo_wifi_acesso);
                            }
                            else
                            {
                                Generico.log_inserir(90, "WIFI ACESSO NÃO GERADO!!! " + dr["nm_email"].ToString() + " - " + randomNumber.ToString(), 2);
                            }

                        }

                        if (cd_ativacao != "" && int.Parse(dr["nr_trial"].ToString()) <= 0)
                        {
                            passo = "Email envio de licença " + dr["id_compra"].ToString();
                            email = new Email().ativar_licenca(dr["nm_cliente"].ToString(),
                                                                dr["link_downaload"].ToString(),
                                                                cd_ativacao,
                                                                ambiente_web);


                            new Email().EnviaEmail(dr["nm_email"].ToString(), "", email, new Comum().email_titulo_ativar_licenca);
                            if (log)
                            {
                                Generico.log_inserir(50, " Envio de e-mail " + passo + " - " + cd_ativacao, 2);
                            }
                        }
                        else
                        {
                            Generico.log_inserir(20, "cd_ativacao vazio - Compra nr " + dr["id_compra"].ToString(), 2);
                            Console.WriteLine("Codigo de ativação não retornou valor " + dr["id_compra"].ToString());
                        }

                    }
                    // trial
                    else if (status == SubscriptionStatus.Trialing)
                    {
                        // envia email com o cd ativação
                        passo = "Trialing Verifica ativacao " + dr["id_compra"].ToString();
                        if (dr["cd_ativacao"].ToString() == "-")
                        {
                            passo = "Ativar produto " + dr["id_compra"].ToString();
                            cd_ativacao = new KL_Conexao().ativar_compra(dr["id_carrinho"].ToString());

                            if (cd_ativacao != "")
                            {
                                passo = "Email envio de licença " + dr["id_compra"].ToString();
                                string email = new Email().ativar_licenca(dr["nm_cliente"].ToString(),
                                                                    dr["link_downaload"].ToString(),
                                                                    cd_ativacao,
                                                                    ambiente_web);


                                new Email().EnviaEmail(dr["nm_email"].ToString(), "", email, new Comum().email_titulo_ativar_licenca);
                                if (log)
                                {
                                    Generico.log_inserir(50, " Envio de e-mail " + passo + " - " + cd_ativacao, 2);
                                }
                            }
                            else
                            {
                                Generico.log_inserir(20, "cd_ativacao vazio - Compra nr " + dr["id_compra"].ToString(), 2);
                                Console.WriteLine("Codigo de ativação não retornou valor " + dr["id_compra"].ToString());
                            }
                        }

                        passo = " Trialing Verifica senha de wifi " + dr["id_compra"].ToString();
                        if (dr["dv_wifi"].ToString() == "1" && dr["dv_envio_wifi"].ToString() == "0")
                        {
                            // gera senha
                            Random random = new Random();
                            int randomNumber = random.Next(111111, 999999);

                            // cadastra e-mail
                            LinkTel lkt = new LinkTel();

                            var diffInSeconds = (DateTime.Now.Date.AddMonths(1) - DateTime.Now.Date).TotalSeconds;

                            bool acesso_lktt = lkt.Acesso_Wifi(dr["nm_email"].ToString(),
                                           randomNumber.ToString(),
                                     dr["nm_cliente"].ToString().Substring(0, dr["nm_cliente"].ToString().IndexOf(" ")),
                                     dr["nm_cliente"].ToString().Substring(dr["nm_cliente"].ToString().IndexOf(" ") + 1),
                                     dr["v_sexo"].ToString(),
                                     "Brasil",
                                     dr["nr_cpf"].ToString(),
                                     dr["nm_produto_sub_descr_completa2"].ToString(), 
                                     DateTime.Parse(dr["dt_nascimento"].ToString()).ToString("yyyy-MM-dd"),
                                     Int64.Parse(diffInSeconds.ToString().Replace(",", "."))
                                 );

                            if (acesso_lktt)
                            {
                                Generico.log_inserir(90, "Senha de e-mail gerada: " + dr["nm_email"].ToString() + " - " + randomNumber.ToString(), 2);


                                new Compra_BLL().wifi_confirma(dr["id_compra"].ToString());
                                // envia e-mail
                                string email = new Email().wifi_senha(dr["nm_cliente"].ToString(),
                                                                            dr["nm_email"].ToString(), randomNumber.ToString(),
                                                                            false);



                                new Email().EnviaEmail(dr["nm_email"].ToString(), "", email, new Comum().email_titulo_wifi_acesso);
                            }
                            else
                            {
                                Generico.log_inserir(90, "WIFI ACESSO NÃO GERADO!!! " + dr["nm_email"].ToString() + " - " + randomNumber.ToString(), 2);
                            }
                        }
                    }
                    // recusa os pagamentos
                    else if (status == SubscriptionStatus.Canceled || status == SubscriptionStatus.Canceled || status == SubscriptionStatus.Unpaid)
                    {
                        passo = "Recusar pagamento " + dr["id_compra"].ToString();
                        new Carrinho_DAL().compra_pagamento_aceito(dr["id_compra"].ToString(), "1", "2");

                        // envia email com a informação de recusa
                        string email = new Email().pedido_recusado(dr["nm_cliente"].ToString(), dr["id_compra"].ToString(), dr["dt_compra"].ToString(), ambiente_web);
                        new Email().EnviaEmail(dr["nm_cliente"].ToString(), "", email, new Comum().email_titulo_pedido_recusado);

                        if (log)
                        {
                            Generico.log_inserir(50, " Envio de e-mail " + passo, 2);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro no processo: " + passo + " - erro: " + ex.Message);
            }
            finally
            {
                
            }
        }

        static void VerificaCancelados(bool ambiente_web, bool log)
        {
            string passo = "";
            try
            {

                if (dt_ultima == DateTime.Now.Date)
                    dt_ultima = DateTime.Now.Date.AddDays(1);
                else
                    return;

                Console.WriteLine("Inicio do Looping Cancelados" + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                // consulta lista de pagamentos pendentes
                DataTable dt = new DataTable();

                passo = "Consulta pagamentos ativos";
                dt = new Pagamentos_BLL().pagamento_consulta(DateTime.Now.AddMonths(-12), DateTime.Now.AddDays(1), 2, "");

                Console.WriteLine("Numeros de pagamentos ativos: " + dt.Rows.Count.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    SubscriptionStatus status;
                    // consulta pagamento

                    passo = "Consulta assinatura " + dr["subscribe_id"].ToString();
                    status = new PagarMe_BLL().consulta_pagamento_assinatura(dr["subscribe_id"].ToString());

                    if (status == SubscriptionStatus.Canceled || status == SubscriptionStatus.Canceled || status == SubscriptionStatus.Unpaid)
                    {
                        
                        passo = "Recusar pagamento " + dr["id_compra"].ToString();

                        Console.WriteLine(passo);

                        new Carrinho_DAL().assinatura_cancelar(dr["id_compra"].ToString(), "2");

                        new KL_Conexao().Cancelamento(dr["id_compra"].ToString(), dr["id_inscricao"].ToString());

                        // envia email com a informação de recusa
                        string email = new Email().assinatura_cancelada(dr["nm_cliente"].ToString(), ambiente_web);
                        new Email().EnviaEmail(dr["nm_cliente"].ToString(), "ewerton.k@itailers.com.br", email, new Comum().email_titulo_assinatura_cancelada);

                        if (log)
                        {
                            Generico.log_inserir(50, " Envio de e-mail " + passo, 2);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro no processo: " + passo + " - erro: " + ex.Message);
            }
            finally
            {

            }
        }
    }
}
