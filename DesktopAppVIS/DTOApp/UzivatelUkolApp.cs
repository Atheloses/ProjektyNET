using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopAppVIS.DTOApp
{
    public class UzivatelUkolApp
    {
        public int IDUzivatelUkol { get; set; }
        public int IDUzivatel { get; set; }
        public int IDUkol { get; set; }
        public DateTime Termin { get; set; }
        public bool Oznacen { get; set; }
        public string Spravce { get; set; }
        public DateTime CasPripojeni { get; set; }
        public DateTime? CasSplneni { get; set; }
        public string Popis { get; set; }

        public static UzivatelUkolApp GetAppFromDTO(UzivatelUkol p_UzivatelUkol)
        {
            return new UzivatelUkolApp()
            {
                IDUkol = p_UzivatelUkol.IDUkol,
                Termin = p_UzivatelUkol.Termin,
                IDUzivatelUkol = p_UzivatelUkol.IDUzivatelUkol,
                IDUzivatel = p_UzivatelUkol.IDUzivatel,
                Spravce = p_UzivatelUkol.Spravce,
                CasPripojeni = p_UzivatelUkol.CasPripojeni,
                CasSplneni = p_UzivatelUkol.CasSplneni,
                Popis = p_UzivatelUkol.Popis
            };
        }

        public static List<UzivatelUkolApp> GetAppFromDTO(IEnumerable<UzivatelUkol> p_UzivatelUkol)
        {
            var output = new List<UzivatelUkolApp>();
            foreach (var item in p_UzivatelUkol)
            {
                output.Add(GetAppFromDTO(item));
            }
            return output;
        }
    }
}
