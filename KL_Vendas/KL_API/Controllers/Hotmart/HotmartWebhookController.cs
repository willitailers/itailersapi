using System.Collections.Specialized;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace KL_API.Controllers.Hotmart
{
    public class HotmartWebhookController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> ReceberWebhook([FromBody] HotmartWebhookRequest hotmartWebhookRequest)
        {
            // Lê os dados do corpo da requisição como formulário
            NameValueCollection form = HttpContext.Current.Request.Form;

            // Exemplo de leitura de campos específicos
            string transaction = form["transaction"];
            string eventType = form["event"];
            string emailComprador = form["buyer_email"];
            string nomeComprador = form["name"];

            // TODO: Salvar no banco, logar, disparar outro processo, etc.

            // Retornar sucesso (Hotmart espera status 200 OK)
            return Request.CreateResponse(HttpStatusCode.OK, "Recebido com sucesso!");
        }

        public class HotmartWebhookRequest
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("creation_date")]
            public long CreationDate { get; set; }

            [JsonPropertyName("event")]
            public string Event { get; set; }

            [JsonPropertyName("version")]
            public string Version { get; set; }

            [JsonPropertyName("data")]
            public HotmartData Data { get; set; }
        }

        public class HotmartData
        {
            [JsonPropertyName("product")]
            public HotmartProduct Product { get; set; }

            [JsonPropertyName("affiliates")]
            public List<HotmartAffiliate> Affiliates { get; set; }

            [JsonPropertyName("buyer")]
            public HotmartBuyer Buyer { get; set; }

            [JsonPropertyName("producer")]
            public HotmartProducer Producer { get; set; }

            [JsonPropertyName("commissions")]
            public List<HotmartCommission> Commissions { get; set; }

            [JsonPropertyName("purchase")]
            public HotmartPurchase Purchase { get; set; }

            [JsonPropertyName("subscription")]
            public HotmartSubscription Subscription { get; set; }
        }

        public class HotmartProduct
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("ucode")]
            public string Ucode { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("warranty_date")]
            public DateTime WarrantyDate { get; set; }

            [JsonPropertyName("support_email")]
            public string SupportEmail { get; set; }

            [JsonPropertyName("has_co_production")]
            public bool HasCoProduction { get; set; }

            [JsonPropertyName("is_physical_product")]
            public bool IsPhysicalProduct { get; set; }

            [JsonPropertyName("content")]
            public HotmartContent Content { get; set; }
        }

        public class HotmartContent
        {
            [JsonPropertyName("has_physical_products")]
            public bool HasPhysicalProducts { get; set; }

            [JsonPropertyName("products")]
            public List<HotmartContentProduct> Products { get; set; }
        }

        public class HotmartContentProduct
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("ucode")]
            public string Ucode { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("is_physical_product")]
            public bool IsPhysicalProduct { get; set; }
        }

        public class HotmartAffiliate
        {
            [JsonPropertyName("affiliate_code")]
            public string AffiliateCode { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }
        }

        public class HotmartBuyer
        {
            [JsonPropertyName("email")]
            public string Email { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("first_name")]
            public string FirstName { get; set; }

            [JsonPropertyName("last_name")]
            public string LastName { get; set; }

            [JsonPropertyName("checkout_phone_code")]
            public string CheckoutPhoneCode { get; set; }

            [JsonPropertyName("checkout_phone")]
            public string CheckoutPhone { get; set; }

            [JsonPropertyName("address")]
            public HotmartAddress Address { get; set; }

            [JsonPropertyName("document")]
            public string Document { get; set; }

            [JsonPropertyName("document_type")]
            public string DocumentType { get; set; }
        }

        public class HotmartAddress
        {
            [JsonPropertyName("city")]
            public string City { get; set; }

            [JsonPropertyName("country")]
            public string Country { get; set; }

            [JsonPropertyName("country_iso")]
            public string CountryIso { get; set; }

            [JsonPropertyName("state")]
            public string State { get; set; }

            [JsonPropertyName("neighborhood")]
            public string Neighborhood { get; set; }

            [JsonPropertyName("zipcode")]
            public string Zipcode { get; set; }

            [JsonPropertyName("address")]
            public string Address { get; set; }

            [JsonPropertyName("number")]
            public string Number { get; set; }

            [JsonPropertyName("complement")]
            public string Complement { get; set; }
        }

        public class HotmartProducer
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("document")]
            public string Document { get; set; }

            [JsonPropertyName("legal_nature")]
            public string LegalNature { get; set; }
        }

        public class HotmartCommission
        {
            [JsonPropertyName("value")]
            public decimal Value { get; set; }

            [JsonPropertyName("source")]
            public string Source { get; set; }

            [JsonPropertyName("currency_value")]
            public string CurrencyValue { get; set; }
        }

        public class HotmartPurchase
        {
            [JsonPropertyName("approved_date")]
            public long ApprovedDate { get; set; }

            [JsonPropertyName("full_price")]
            public HotmartPrice FullPrice { get; set; }

            [JsonPropertyName("price")]
            public HotmartPrice Price { get; set; }

            [JsonPropertyName("checkout_country")]
            public HotmartCheckoutCountry CheckoutCountry { get; set; }

            [JsonPropertyName("order_bump")]
            public HotmartOrderBump OrderBump { get; set; }

            [JsonPropertyName("event_tickets")]
            public HotmartEventTickets EventTickets { get; set; }

            [JsonPropertyName("original_offer_price")]
            public HotmartPrice OriginalOfferPrice { get; set; }

            [JsonPropertyName("order_date")]
            public long OrderDate { get; set; }

            [JsonPropertyName("status")]
            public string Status { get; set; }

            [JsonPropertyName("transaction")]
            public string Transaction { get; set; }

            [JsonPropertyName("payment")]
            public HotmartPayment Payment { get; set; }

            [JsonPropertyName("offer")]
            public HotmartOffer Offer { get; set; }

            [JsonPropertyName("sckPaymentLink")]
            public string SckPaymentLink { get; set; }

            [JsonPropertyName("is_funnel")]
            public bool IsFunnel { get; set; }

            [JsonPropertyName("business_model")]
            public string BusinessModel { get; set; }
        }

        public class HotmartPrice
        {
            [JsonPropertyName("value")]
            public decimal Value { get; set; }

            [JsonPropertyName("currency_value")]
            public string CurrencyValue { get; set; }
        }

        public class HotmartCheckoutCountry
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("iso")]
            public string Iso { get; set; }
        }

        public class HotmartOrderBump
        {
            [JsonPropertyName("is_order_bump")]
            public bool IsOrderBump { get; set; }

            [JsonPropertyName("parent_purchase_transaction")]
            public string ParentPurchaseTransaction { get; set; }
        }

        public class HotmartEventTickets
        {
            [JsonPropertyName("amount")]
            public long Amount { get; set; }
        }

        public class HotmartPayment
        {
            [JsonPropertyName("installments_number")]
            public int InstallmentsNumber { get; set; }

            [JsonPropertyName("type")]
            public string Type { get; set; }
        }

        public class HotmartOffer
        {
            [JsonPropertyName("code")]
            public string Code { get; set; }

            [JsonPropertyName("coupon_code")]
            public string CouponCode { get; set; }
        }

        public class HotmartSubscription
        {
            [JsonPropertyName("status")]
            public string Status { get; set; }

            [JsonPropertyName("plan")]
            public HotmartPlan Plan { get; set; }

            [JsonPropertyName("subscriber")]
            public HotmartSubscriber Subscriber { get; set; }
        }

        public class HotmartPlan
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }
        }

        public class HotmartSubscriber
        {
            [JsonPropertyName("code")]
            public string Code { get; set; }
        }



    }
}