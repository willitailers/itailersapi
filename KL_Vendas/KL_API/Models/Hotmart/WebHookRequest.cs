using System.Collections.Generic;
using System;

namespace KL_API.Models.Hotmart
{
    public class WebHookRequest
    {
        public string Id { get; set; }
        public long Creation_Date { get; set; }
        public string Event { get; set; }
        public string Version { get; set; }
        public Data_Object Data { get; set; }

        public class Data_Object
        {
            public Product Product { get; set; }
            public List<Affiliate> Affiliates { get; set; }
            public Buyer Buyer { get; set; }
            public Producer Producer { get; set; }
            public List<Commission> Commissions { get; set; }
            public Purchase Purchase { get; set; }
            public Subscription Subscription { get; set; }
        }

        public class Product
        {
            public int Id { get; set; }
            public string Ucode { get; set; }
            public string Name { get; set; }
            public bool Has_Co_Production { get; set; }
            public DateTime Warranty_Date { get; set; }
            public string Support_Email { get; set; }
        }

        public class Affiliate
        {
            public string Affiliate_Code { get; set; }
            public string Name { get; set; }
        }

        public class Buyer
        {
            public string Email { get; set; }
            public string Name { get; set; }
            public string First_Name { get; set; }
            public string Last_Name { get; set; }
            public string Checkout_Phone { get; set; }
            public string Checkout_Phone_Code { get; set; }
            public string Document { get; set; }
            public string Document_Type { get; set; }
            public Address_Object Address { get; set; }
        }

        public class Address_Object
        {
            public string Zipcode { get; set; }
            public string Country { get; set; }
            public string Number { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Neighborhood { get; set; }
            public string Complement { get; set; }
            public string Country_Iso { get; set; }
        }

        public class Producer
        {
            public string Name { get; set; }
            public string Legal_Nature { get; set; }
            public string Document { get; set; }
        }

        public class Commission
        {
            public double Value { get; set; }
            public string Currency_Value { get; set; }
            public string Source { get; set; }
            public CurrencyConversion Currency_Conversion { get; set; }
        }

        public class CurrencyConversion
        {
            public double Converted_Value { get; set; }
            public string Converted_To_Currency { get; set; }
            public double Conversion_Rate { get; set; }
        }

        public class Purchase
        {
            public long Approved_Date { get; set; }
            public Price Full_Price { get; set; }
            public Price Original_Offer_Price { get; set; }
            public Price Price { get; set; }
            public Offer Offer { get; set; }
            public int Recurrence_Number { get; set; }
            public bool Subscription_Anticipation_Purchase { get; set; }
            public CheckoutCountry Checkout_Country { get; set; }
            public Origin Origin { get; set; }
            public OrderBump Order_Bump { get; set; }
            public string Order_Date { get; set; }
            public long Date_Next_Charge { get; set; }
            public string Status { get; set; }
            public string Transaction { get; set; }
            public Payment Payment { get; set; }
            public bool Is_Funnel { get; set; }
            public EventTickets Event_Tickets { get; set; }
            public string Business_Model { get; set; }
        }

        public class Price
        {
            public double Value { get; set; }
            public string Currency_Value { get; set; }
        }

        public class Offer
        {
            public string Code { get; set; }
            public string Coupon_Code { get; set; }
        }

        public class CheckoutCountry
        {
            public string Name { get; set; }
            public string Iso { get; set; }
        }

        public class Origin
        {
            public string Xcod { get; set; }
        }

        public class OrderBump
        {
            public bool Is_Order_Bump { get; set; }
            public string Parent_Purchase_Transaction { get; set; }
        }

        public class Payment
        {
            public string Billet_Barcode { get; set; }
            public string Billet_Url { get; set; }
            public int Installments_Number { get; set; }
            public string Pix_Code { get; set; }
            public long Pix_Expiration_Date { get; set; }
            public string Pix_Qrcode { get; set; }
            public string Refusal_Reason { get; set; }
            public string Type { get; set; }
        }

        public class EventTickets
        {
            public int Amount { get; set; }
        }

        public class Subscription
        {
            public string Status { get; set; }
            public Plan Plan { get; set; }
            public Subscriber Subscriber { get; set; }
        }

        public class Plan
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Subscriber
        {
            public string Code { get; set; }
        }

    }
}