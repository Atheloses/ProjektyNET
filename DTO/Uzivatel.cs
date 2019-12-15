using System;

namespace DTO
{
    public class Uzivatel
    {
        public int IDUzivatel { get; set; }
        public string Prezdivka { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public string Email { get; set; }
        public DateTime DatumRegistrace { get; set; }
        public int PocetPodukolu { get; set; }
        public char Aktivni { get; set; }
        public string HesloHash { get; set; }
        public string NormPrezdivka { get; set; }
    }
}
