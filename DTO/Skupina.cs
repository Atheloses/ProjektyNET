using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Skupina
    {
        public int IDSkupina { get; set; }
        public string Nazev { get; set; }
        public DateTime DatumVytvoreni { get; set; }
        public string Popis { get; set; }
        public string Efektivita { get; set; }
    }
}

