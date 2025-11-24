using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KL_API.Models.Kiddlepass
{
    public class KiddlepassLog
    {
        public int kiddlepass_id { get; set; }
        public string kiddlepass_log { get; set; }
        public string kiddlepass_data_criacao { get; set; }
        public string kiddlepass_json { get; set; }
    }
}