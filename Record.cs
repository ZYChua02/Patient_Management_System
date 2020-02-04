using System;
using System.Collections.Generic;
using System.Text;

namespace PRG2_T08_Team2
{
    class Record
    {
            public string End_of_month { get; set; }
            public string Preliminary { get; set; }
            public string Eur_sgd { get; set; }
            public string Gbp_sgd { get; set; }
            public string Usd_sgd { get; set; }
            public string Aud_sgd { get; set; }
            public string Cad_sgd { get; set; }
            public string Cny_sgd_100 { get; set; }
            public string Hkd_sgd_100 { get; set; }
            public string Inr_sgd_100 { get; set; }
            public string Idr_sgd_100 { get; set; }
            public string Jpy_sgd_100 { get; set; }
            public string Krw_sgd_100 { get; set; }
            public string Myr_sgd_100 { get; set; }
            public string Twd_sgd_100 { get; set; }
            public string Nzd_sgd { get; set; }
            public string Php_sgd_100 { get; set; }
            public string Qar_sgd_100 { get; set; }
            public string Sar_sgd_100 { get; set; }
            public string Chf_sgd { get; set; }
            public string Thb_sgd_100 { get; set; }
            public string Aed_sgd_100 { get; set; }
            public string Vnd_sgd_100 { get; set; }
            public string Timestamp { get; set; }

        public Record(string eem, string prelim, string eur, string gbp, 
            string usd, string aud, string cad, string cny, string hkd, 
            string inr, string idr, string jpy, string krw, string myr, 
            string twd, string nzd, string php, string qar, string sar, 
            string chf, string thb, string aed, string vnd, string ts)
        {
            End_of_month = eem;
            Preliminary = prelim;
            Eur_sgd = eur;
            Gbp_sgd = gbp;
            Usd_sgd = usd;
            Aud_sgd = aud;
            Cad_sgd = cad;
            Cny_sgd_100 = cny;
            Hkd_sgd_100 = hkd;
            Inr_sgd_100 = inr;
            Idr_sgd_100 = idr;
            Jpy_sgd_100 = jpy;
            Krw_sgd_100 = krw;
            Myr_sgd_100 = myr;
            Twd_sgd_100 = twd;
            Nzd_sgd = nzd;
            Php_sgd_100 = php;
            Qar_sgd_100 = qar;
            Sar_sgd_100 = sar;
            Chf_sgd = chf;
            Thb_sgd_100 = thb;
            Aed_sgd_100 = aed;
            Vnd_sgd_100 = vnd;
            Timestamp = ts;
        }
    }
}
