using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using PagarMe;
using Objetos;
using DAL;

namespace BLL
{
    public class PagarMe_BLL
    {
        protected string api = System.Configuration.ConfigurationManager.AppSettings["chave_pgm"];
        protected string encrypt = System.Configuration.ConfigurationManager.AppSettings["crpit_pgm"];
        protected string url_pgm = System.Configuration.ConfigurationManager.AppSettings["url_pgm"];

        #region pagamento

        //public AssinaturaRetorno gerar_pagamento_assinatura(Cliente cli, Cartao cartao, Int64 id_compra, string nm_plano, int nr_trial)
        //{
        //    AssinaturaRetorno erro = new AssinaturaRetorno();

        //    try
        //    {
        //        PagarMeService.DefaultEncryptionKey = encrypt;
        //        PagarMeService.DefaultApiEndpoint = url_pgm;
        //        PagarMeService.DefaultApiKey = api;


        //        string card_hash = "", card_id = ""; int id_plano = 0, subscribe_id = 0;
        //        Card card = new Card();
        //        Plan plan = new Plan();
        //        // 1 passo gerar card hash
        //        card_hash = obter_card_hash(cartao);

        //        cartao.card_hash = card_hash;

        //        // 2 gerar card id -- não irei gerar
        //        //card = obter_card_id(card_hash, cartao);

        //        //cartao.card_id = card.Id;

        //        // 3 grar plano
        //        plan = criar_plano(nm_plano, int.Parse(cartao.vl_compra), nr_trial);

        //        // 4 gerar assinatura
        //        subscribe_id = enviar_cobranca_assinatura(card_hash, cartao, cli, plan, card);

        //        new Carrinho_DAL().compra_atualiza_assinatura(id_compra.ToString(), card_hash, card_id, id_plano.ToString(), subscribe_id);

        //        erro.valido = true;
        //        erro.msgErro = "";
        //        erro.subscribe_id = subscribe_id;

        //        return erro;
        //    }
        //    catch(PagarMeException error)
        //    {
        //        erro.valido = false;
        //        erro.msgErro = error.Error.Errors[0].Message;
        //        erro.erro_pagto = true;

        //        return erro;

        //    }
        //    catch(Exception ex)
        //    {
        //        erro.valido = false;
        //        erro.msgErro = ex.Message;
        //        erro.erro_pagto = false;

        //        return erro;
        //    }

        //}

        //public SubscriptionStatus consulta_pagamento_assinatura(string subscribe_id)
        //{
        //    try
        //    {
        //        PagarMeService.DefaultEncryptionKey = encrypt;
        //        PagarMeService.DefaultApiEndpoint = url_pgm;
        //        PagarMeService.DefaultApiKey = api;

        //        Subscription subscription = PagarMeService.GetDefaultService().Subscriptions.Find(subscribe_id);

        //        return subscription.Status;
                
        //    }
        //    catch(Exception ex)
        //    {
        //        Generico.log_inserir(5, "erro co consultar status da assinatura: " + subscribe_id + " - " + ex.Message, 0);
        //        return SubscriptionStatus.None;
        //    }
        //}




        //public string obter_card_hash(Cartao _card)
        //{
        //    //PagarMeService.DefaultEncryptionKey = "ek_test_imdFM4Sx17xgGcbrmOaib0A2uyaJrW"; // encrypt;
        //    //PagarMeService.DefaultApiEndpoint = "https://api.pagar.me/1"; // url_pgm;
        //    //PagarMeService.DefaultApiKey = "ak_test_Cvr3oEe24nll6MLHsvNsWKkOQHSl0Z"; // api;

        //    CardHash card = new CardHash();

        //        card.CardNumber = _card.card_number;
        //        card.CardHolderName = _card.card_holder_name;
        //        card.CardExpirationDate = _card.card_expiration_date;
        //        card.CardCvv = _card.card_cvv;

        //        string cardhash = card.Generate();


        //        return cardhash;

        //}

        //public Card obter_card_id(string card_hash, Cartao _card)
        //{
        //    //PagarMeService.DefaultEncryptionKey = "ek_test_imdFM4Sx17xgGcbrmOaib0A2uyaJrW"; // encrypt;
        //    //PagarMeService.DefaultApiEndpoint = "https://api.pagar.me/1"; // url_pgm;
        //    //PagarMeService.DefaultApiKey = "ak_test_Cvr3oEe24nll6MLHsvNsWKkOQHSl0Z"; // api;

        //    Card card = new Card();

        //    card.Number = _card.card_number;
        //    card.HolderName = _card.card_holder_name;
        //    card.ExpirationDate = _card.card_expiration_date;
        //    card.Cvv = _card.card_cvv;
        //    card.CardHash = card_hash;

        //    card.Save();


        //    return card;

        //}

        //public Plan criar_plano(string nm_plano, int valor_centavos, int nr_trial)
        //{
        //    //PagarMeService.DefaultEncryptionKey = "ek_test_imdFM4Sx17xgGcbrmOaib0A2uyaJrW"; // encrypt;
        //    //PagarMeService.DefaultApiEndpoint = "https://api.pagar.me/1"; // url_pgm;
        //    //PagarMeService.DefaultApiKey = "ak_test_Cvr3oEe24nll6MLHsvNsWKkOQHSl0Z"; // api;

        //    Plan plan = new Plan
        //    {
        //        Name = nm_plano,
        //        Amount = valor_centavos,
        //        Days = 30,
        //        TrialDays = nr_trial
        //    };

        //    plan.Save();

        //    return plan;
        //}

        //public void enviar_cobranca_cartao()
        //{
        //    PagarMeService.DefaultEncryptionKey = encrypt;
        //    PagarMeService.DefaultApiEndpoint = url_pgm;
        //    PagarMeService.DefaultApiKey = api;

        //    Transaction transaction = new Transaction();

        //    transaction.Amount = 1000;
        //    transaction.CardHash = "CARD_HASH_GERADO";
        //    transaction.Customer = new Customer()
        //    {
        //        Name = "John Appleseed",
        //        DocumentNumber = "92545278157",
        //        Email = "jappleseed@apple.com",
        //        Address = new Address()
        //        {
        //            Street = "Rua Dr. Geraldo Campos Moreira",
        //            Neighborhood = "Cidade Monções",
        //            Zipcode = "04571020",
        //            StreetNumber = "240"
        //        },
        //        Phone = new Phone()
        //        {
        //            Ddd = "11",
        //            Number = "15510101"
        //        }
        //    };

        //    transaction.Save();
        //}

        //public int enviar_cobranca_assinatura(string card_hash, Cartao cartao, Cliente cli, Plan plan, Card card)
        //{
        //    //PagarMeService.DefaultEncryptionKey = "ek_test_imdFM4Sx17xgGcbrmOaib0A2uyaJrW"; // encrypt;
        //    //PagarMeService.DefaultApiEndpoint = "https://api.pagar.me/1"; // url_pgm;
        //    //PagarMeService.DefaultApiKey = "ak_test_Cvr3oEe24nll6MLHsvNsWKkOQHSl0Z"; // api;


        //    Subscription subscription = new Subscription();
        //    subscription.PaymentMethod = PaymentMethod.CreditCard;
        //    subscription.CardHash = card_hash;
        //    subscription.Plan = plan;

        //    subscription.Customer = new Customer()
        //    {
        //        Name = cli.name,
        //        DocumentNumber = cli.document_number,
        //        Email = cli.email,
        //        //Address = new Address()
        //        //{
        //        //    Street = cli.street,
        //        //    Neighborhood = cli.Neighborhood,
        //        //    Zipcode = cli.zipcode,
        //        //    StreetNumber = cli.street_number
        //        //},
        //        Phone = new Phone()
        //        {
        //            Ddd = cli.ddd,
        //            Number = cli.phone
        //        }
                
        //    };

        //    subscription.Save();


        //    /*
        //    Subscription subscription = new Subscription
        //    {
        //        PaymentMethod = PaymentMethod.CreditCard,
        //        CardHash = card_hash,
        //        CardNumber = cartao.card_number,
        //        CardHolderName = cartao.card_holder_name,
        //        CardExpirationDate = cartao.card_expiration_date.Replace("/", ""),
        //        CardCvv = cartao.card_cvv
        //    };

        //    subscription.Plan = new Plan { Id = plan.Id };
        //    //subscription.Card = card;
        //    //subscription.Plan = plan;

        //    Customer customer = new Customer
        //    {
        //        Email = cli.email,
        //        Name = cli.name,
        //        DocumentNumber = cli.document_number
        //    };

        //    subscription.Customer = customer;

        //    subscription.Save();
        //    */

        //    return int.Parse(subscription.Id); 
        //}

        #endregion

        #region cancelamento

        public void assinatura_cancelar(string id_subscricao)
        {
            //PagarMeService.DefaultApiKey = api;
            //var subscription = PagarMeService.GetDefaultService().Subscriptions.Find(id_subscricao);
            //subscription.Cancel();
        }

        #endregion
    }


}
