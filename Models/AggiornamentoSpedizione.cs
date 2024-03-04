using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace back_end_s6_l01_02_03_04.Models
{
    public class AggiornamentoSpedizione
    {
        public int AggiornamentoID { get; set; }
        public int SpedizioneID { get; set; } // Chiave esterna per Spedizione
        public string StatoSpedizione { get; set; }
        public string LuogoAttuale { get; set; }
        public string Descrizione { get; set; }
        public DateTime DataOraAggiornamento { get; set; }
    }
}