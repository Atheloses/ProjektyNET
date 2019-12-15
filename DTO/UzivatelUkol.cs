using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class UzivatelUkol
    {
        public int IDUzivatelUkol { get; set; }
        public string Spravce { get; set; }
        public DateTime CasPripojeni { get; set; }
        public DateTime? CasSplneni { get; set; }
        public DateTime Termin { get; set; }
        public string Popis { get; set; }
        public int IDUzivatel { get; set; }
        public int IDUkol { get; set; }
    }
}
