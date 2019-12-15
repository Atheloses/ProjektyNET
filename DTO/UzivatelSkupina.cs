using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class UzivatelSkupina
    {
        public int IDUzivatelSkupina { get; set; }
        public string Spravce { get; set; }
        public DateTime CasPripojeni { get; set; }
        public DateTime? CasOdpojeni { get; set; }
        public int IDUzivatel { get; set; }
        public int IDSkupina { get; set; }
    }
}
