using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.DTOApp
{
    public class UzivatelApp
    {
        public int IDUzivatel { get; set; }
        public string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(Jmeno) || !string.IsNullOrEmpty(Prijmeni))
                    return $"{Jmeno} {Prijmeni} ({Prezdivka})";
                else
                    return Prezdivka;
            }
        }
        public string Prezdivka { get; set; }
        public string NormPrezdivka { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public string Email { get; set; }
        public int PocetPodukolu { get; set; }
        public char Aktivni { get; set; }
        public DateTime DatumRegistrace { get; set; }
        public string HesloHash { get; set; }

        public static UzivatelApp GetAppFromDTO(DTO.Uzivatel p_Uzivatel)
        {
            if (p_Uzivatel == null)
                return null;

            return new UzivatelApp()
            {
                IDUzivatel = p_Uzivatel.IDUzivatel,
                Prezdivka = p_Uzivatel.Prezdivka,
                Jmeno = p_Uzivatel.Jmeno,
                Prijmeni = p_Uzivatel.Prijmeni,
                Email = p_Uzivatel.Email,
                PocetPodukolu = p_Uzivatel.PocetPodukolu,
                Aktivni = p_Uzivatel.Aktivni,
                DatumRegistrace = p_Uzivatel.DatumRegistrace,
                HesloHash = p_Uzivatel.HesloHash,
                NormPrezdivka = p_Uzivatel.NormPrezdivka,
            };
        }

        public static Uzivatel GetDTOFromApp(UzivatelApp p_UzivatelApp)
        {
            if (p_UzivatelApp == null)
                return null;

            return new Uzivatel()
            {
                IDUzivatel = p_UzivatelApp.IDUzivatel,
                Prezdivka = p_UzivatelApp.Prezdivka,
                Jmeno = p_UzivatelApp.Jmeno,
                Prijmeni = p_UzivatelApp.Prijmeni,
                Email = p_UzivatelApp.Email,
                PocetPodukolu = p_UzivatelApp.PocetPodukolu,
                Aktivni = p_UzivatelApp.Aktivni,
                DatumRegistrace = p_UzivatelApp.DatumRegistrace,
                HesloHash = p_UzivatelApp.HesloHash,
                NormPrezdivka = p_UzivatelApp.NormPrezdivka
            };
        }

        public static List<UzivatelApp> GetAppFromDTO(List<Uzivatel> p_Uzivatel)
        {
            var output = new List<UzivatelApp>();
            if (p_Uzivatel != null)
                foreach (var item in p_Uzivatel)
                {
                    output.Add(GetAppFromDTO(item));
                }
            return output;
        }
    }
}
