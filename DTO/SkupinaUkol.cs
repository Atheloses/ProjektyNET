using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class SkupinaUkol
    {
        public int IDSkupinaUkol { get; set; }
        public DateTime CasPripojeni { get; set; }
        public DateTime? CasSplneni { get; set; }
        public DateTime Termin { get; set; }
        public string Popis { get; set; }
        public int IDUkol { get; set; }
        public int IDSkupina { get; set; }
    }
}

