using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BLL
{
    public class Comum
    {
        public string email_titulo_pedido_recebido = "Recebemos seu pedido!";
        public string email_titulo_pagamento_analise = "Estamos analisando seu pedido";
        public string email_titulo_pagamento_aprovado = "Pronto! Confirmamos o pagamento";
        public string email_titulo_ativar_licenca = "Ative sua licença do Antivirus Linktel & Kaspersky";
        public string email_titulo_pagamento_cancelado = "Seu pagamento foi cancelado";
        public string email_titulo_assinatura_cancelada = "Sua assinatura foi cancelada";
        public string email_titulo_pedido_recusado = "Seu pedido foi recusado";
        public string email_titulo_wifi_acesso = "Acesso Wi-Fi Linktel";

        public int nivel_indice(int id, DropDownList ddl)
        {
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                if (id == int.Parse(ddl.Items[i].Value))
                {
                    return i;
                }
            }

            return 0;
        }

        public static bool EditaTela(DataTable dt, string URL)
        {
            bool edita = false;
            foreach(DataRow dr in dt.Rows)
            {
                if (URL.Contains(dr["nm_tela"].ToString()) && dr["dv_edita"].ToString() == "1")
                    edita = true;
            }

            return edita;
        }

    }
}
