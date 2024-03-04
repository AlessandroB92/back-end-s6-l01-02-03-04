using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace back_end_s6_l01_02_03_04.Models
{
    public class ClientePrivato
    {
        public int ClientePrivatoID { get; set; }
        public string CodiceFiscale { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Indirizzo { get; set; }
        public string Email { get; set; }
    }
}