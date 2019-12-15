using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopAppVIS.DTOApp
{
    public class UzivatelApp
    {
        public int ID { get; set; }
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DisplayName("Uživatel")]
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
        [System.ComponentModel.Browsable(false)]
        public string Jmeno { get; set; }
        [System.ComponentModel.Browsable(false)]
        public string Prijmeni { get; set; }
        [System.ComponentModel.Browsable(false)]
        public string Email { get; set; }
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.DisplayName("Počet podúkolů")]
        public int PocetPodukolu { get; set; }
        [System.ComponentModel.DisplayName("Označit")]
        public bool Oznacen { get; set; }
        [System.ComponentModel.Browsable(false)]
        public char Aktivni { get; set; }

        public static UzivatelApp GetAppFromDTO(DTO.Uzivatel p_Uzivatel)
        {
            return new UzivatelApp()
            {
                ID = p_Uzivatel.IDUzivatel,
                Prezdivka = p_Uzivatel.Prezdivka,
                Jmeno = p_Uzivatel.Jmeno,
                Prijmeni = p_Uzivatel.Prijmeni,
                Email = p_Uzivatel.Email,
                PocetPodukolu = p_Uzivatel.PocetPodukolu,
                Aktivni = p_Uzivatel.Aktivni
            };
        }

        public static DTO.Uzivatel GetDTOFromApp(UzivatelApp p_UzivatelApp)
        {
            return new DTO.Uzivatel()
            {
                IDUzivatel = p_UzivatelApp.ID,
                Prezdivka = p_UzivatelApp.Prezdivka,
                Jmeno = p_UzivatelApp.Jmeno,
                Prijmeni = p_UzivatelApp.Prijmeni,
                Email = p_UzivatelApp.Email,
                PocetPodukolu = p_UzivatelApp.PocetPodukolu,
                Aktivni = p_UzivatelApp.Aktivni
            };
        }

        public static List<UzivatelApp> GetAppFromDTO(List<DTO.Uzivatel> p_Uzivatel)
        {
            var output = new List<UzivatelApp>();
            foreach (var item in p_Uzivatel)
            {
                output.Add(GetAppFromDTO(item));
            }
            return output;
        }
    }
}
