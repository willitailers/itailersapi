using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KL_API.Models.Guardiao
{
    public class GuardiaoLog
    {
        public int guardiao_id { get; set; }
        public string guardiao_log { get; set; }
        public string guardiao_data_criacao { get; set; }
        public string guardiao_json { get; set; }
    }
}