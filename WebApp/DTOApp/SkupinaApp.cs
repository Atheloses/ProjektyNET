using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.DTOApp
{
    public class SkupinaApp
    {
        public int IDSkupina { get; set; }
        public string Nazev { get; set; }
        public DateTime DatumVytvoreni { get; set; }
        public string Popis { get; set; }
        public string Efektivita { get; set; }

        public static SkupinaApp GetAppFromDTO(Skupina p_Skupina)
        {
            return new SkupinaApp()
            {
                IDSkupina = p_Skupina.IDSkupina,
                Nazev = p_Skupina.Nazev,
                DatumVytvoreni = p_Skupina.DatumVytvoreni,
                Popis = p_Skupina.Popis,
                Efektivita = p_Skupina.Efektivita
            };
        }

        public static Skupina GetDTOFromApp(SkupinaApp p_SkupinaApp)
        {
            return new Skupina()
            {
                IDSkupina = p_SkupinaApp.IDSkupina,
                Nazev = p_SkupinaApp.Nazev,
                DatumVytvoreni = p_SkupinaApp.DatumVytvoreni,
                Popis = p_SkupinaApp.Popis,
                Efektivita = p_SkupinaApp.Efektivita
            };
        }

        public static List<SkupinaApp> GetAppFromDTO(List<Skupina> p_Skupina)
        {
            var output = new HashSet<SkupinaApp>();
            foreach (var item in p_Skupina)
            {
                output.Add(GetAppFromDTO(item));
            }
            return output.ToList();
        }

        public static List<SkupinaApp> GetDTOFromApp(List<Skupina> p_Skupina)
        {
            var output = new HashSet<SkupinaApp>();
            foreach (var item in p_Skupina)
            {
                output.Add(GetAppFromDTO(item));
            }
            return output.ToList();
        }
    }

}
