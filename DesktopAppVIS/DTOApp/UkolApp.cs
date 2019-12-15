using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopAppVIS.DTOApp
{
    public class UkolApp
    {
        public int IDUkol { get; set; }
        public DateTime CasVytvoreni { get; set; }
        public string Nazev { get; set; }
        public string Popis { get; set; }
        public DateTime? CasSplneni { get; set; }
        public DateTime Termin { get; set; }
        public int IDPriorita { get; set; }
        public int Mandays { get; set; }

        public static UkolApp GetAppFromDTO(Ukol p_Ukol)
        {
            return new UkolApp()
            {
                IDUkol = p_Ukol.IDUkol,
                CasVytvoreni = p_Ukol.CasVytvoreni,
                Nazev = p_Ukol.Nazev,
                Popis = p_Ukol.Popis,
                CasSplneni = p_Ukol.CasSplneni,
                Termin = p_Ukol.Termin,
                IDPriorita = p_Ukol.IDPriorita,
                Mandays = p_Ukol.Mandays
            };
        }

        public static List<UkolApp> GetAppFromDTO(List<Ukol> p_Ukol)
        {
            var output = new List<UkolApp>();
            foreach (var item in p_Ukol)
            {
                output.Add(GetAppFromDTO(item));
            }
            return output;
        }

        public static Ukol GetDTOFromApp(UkolApp p_UkolApp)
        {
            return new Ukol()
            {
                IDUkol = p_UkolApp.IDUkol,
                CasVytvoreni = p_UkolApp.CasVytvoreni,
                Nazev = p_UkolApp.Nazev,
                Popis = p_UkolApp.Popis,
                CasSplneni = p_UkolApp.CasSplneni,
                Termin = p_UkolApp.Termin,
                IDPriorita = p_UkolApp.IDPriorita,
                Mandays = p_UkolApp.Mandays
            };
        }
    }
}
