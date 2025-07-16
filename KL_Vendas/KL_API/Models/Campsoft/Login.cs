using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace KL_API.Models.Campsoft
{
    public class Login
    {
        public class LoginValidation
        {
            public string isp_identifier { get; set; }
            public string login { get; set; }
            public string password { get; set; }
        }
    }

    public class ValidationResponse
    {
        public class Success
        {
            public class Email
            {
                public string data { get; set; }
            }

            public class Phone
            {
                public string ddi { get; set; }
                public string ddd { get; set; }
                public string number { get; set; }
                public string mobile { get; set; }
            }

            public class Isp
            {
                public string name { get; set; }
                public string created_at { get; set; }
                public string updated_at { get; set; }
                public int isp_identifier { get; set; }
            }

            public class Product
            {
                public string code { get; set; }
                public string name { get; set; }
                public string queue { get; set; }
                public string status { get; set; }
                public List<object> queue_send { get; set; } = new List<object>();
                public object internal_id { get; set; }
                public string working_until { get; set; }
            }

            public class UsuarioResposta
            {
                public string userkey { get; set; }
                public string status { get; set; }
                public string name { get; set; }
                public string username_type { get; set; }
                public Email email { get; set; }
                public Phone phone { get; set; }
                public object gender { get; set; }
                public object birth { get; set; }
                public Isp isp { get; set; }
                public List<Product> products { get; set; } = new List<Product>();
                public string created_at { get; set; }
                public string updated_at { get; set; }
            }
        }

        public class ErroResposta
        {
            public string status { get; set; }
            public string error_id { get; set; }
            public string origem { get; set; }
            public string message { get; set; }
        }
    }

    public class LoginResponse
    {
        public bool access { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public string user_id { get; set; }
    }

    public class RequestLoginCampSoft
    {
        public string isp_identifier { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }

    public class RequestProdutosCampSoft
    {
        public string user_id { get; set; }
    }
}