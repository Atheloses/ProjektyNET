using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOApp
{
    public class UzivatelUkolApp
    {
        [Required]
        public int IDUzivatel { get; set; }
        public int IDUkol { get; set; }
        public DateTime Termin { get; set; }
        public int IDUzivatelUkol { get; set; }
        public bool Oznacen { get; set; }
        public string Spravce { get; set; }
        public DateTime CasPripojeni { get; set; }
        public DateTime? CasSplneni { get; set; }
        public string Popis { get; set; }
        public bool Splnen
        {
            get { return CasSplneni == null ? false : true; }
            set { CasSplneni = DateTime.Now; }
        }

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

        public static IEnumerable<UzivatelUkolApp> GetAppFromDTO(IEnumerable<UzivatelUkol> p_UzivatelUkol)
        {
            var output = new List<UzivatelUkolApp>();
            foreach (var item in p_UzivatelUkol)
            {
                output.Add(GetAppFromDTO(item));
            }
            return output;
        }

        public static UzivatelUkol GetDTOFromApp(UzivatelUkolApp p_UzivatelUkolApp)
        {
            return new UzivatelUkol()
            {
                Spravce = p_UzivatelUkolApp.Spravce,
                CasPripojeni = p_UzivatelUkolApp.CasPripojeni,
                CasSplneni = p_UzivatelUkolApp.CasSplneni,
                Termin = p_UzivatelUkolApp.Termin,
                IDUzivatelUkol = p_UzivatelUkolApp.IDUzivatelUkol,
                Popis = p_UzivatelUkolApp.Popis,
                IDUzivatel = p_UzivatelUkolApp.IDUzivatel,
                IDUkol = p_UzivatelUkolApp.IDUkol
            };
        }
    }
}
