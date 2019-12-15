using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Komentar
    {
        public int IDKomentar { get; set; }
        public string Koment { get; set; }
        public DateTime CasPridani { get; set; }
        public int IDUzivatel { get; set; }
        public int IDUkol { get; set; }
    }
}

