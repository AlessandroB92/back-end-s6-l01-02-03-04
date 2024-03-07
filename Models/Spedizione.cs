using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace back_end_s6_l01_02_03_04.Models
{
    public class Spedizione
    {
        public int SpedizioneID { get; set; }
        public virtual ClientiPrivati ClienteID { get; set; } // Chiave esterna per ClientePrivato
        public virtual Aziende AziendaID { get; set; } // Chiave esterna per Azienda
        public string NumeroSpedizione { get; set; }
        public DateTime DataSpedizione { get; set; }
        public decimal Peso { get; set; }
        public string CittaDestinataria { get; set; }
        public string IndirizzoDestinatario { get; set; }
        public string NominativoDestinatario { get; set; }
        public decimal CostoSpedizione { get; set; }
        public DateTime? DataConsegnaPrevista { get; set; }
        public string StatoSpedizione { get; set; }
    }
}