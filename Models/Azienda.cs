using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace back_end_s6_l01_02_03_04.Models
{
    public class Azienda
    {
        public int AziendaID { get; set; }
        public string PartitaIVA { get; set; }
        public string RagioneSociale { get; set; }
        public string Indirizzo { get; set; }
        public string Email { get; set; }
    }
}