using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Ukol
    {
        public int IDUkol { get; set; }
        public DateTime CasVytvoreni { get; set; }
        public string Nazev { get; set; }
        public string Popis { get; set; }
        public DateTime? CasSplneni { get; set; }
        public DateTime Termin { get; set; }
        public int Mandays { get; set; }
        public int IDPriorita { get; set; }
    }
}
