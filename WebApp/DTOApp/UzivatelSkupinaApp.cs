using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.DTOApp
{
    public class UzivatelSkupinaApp
    {
        public int IDUzivatel { get; set; }
        public int IDSkupina { get; set; }
        public int IDUzivatelSkupina { get; set; }
        public string Spravce { get; set; }
        public DateTime? CasOdpojeni { get; set; }
        public DateTime CasPripojeni { get; set; }

        public static UzivatelSkupina GetDTOFromApp(UzivatelSkupinaApp p_UzivatelSkupinaApp)
        {
            return new UzivatelSkupina()
            {
                IDUzivatel = p_UzivatelSkupinaApp.IDUzivatel,
                IDSkupina = p_UzivatelSkupinaApp.IDSkupina,
                IDUzivatelSkupina = p_UzivatelSkupinaApp.IDUzivatelSkupina,
                Spravce = p_UzivatelSkupinaApp.Spravce,
                CasOdpojeni = p_UzivatelSkupinaApp.CasOdpojeni,
                CasPripojeni = p_UzivatelSkupinaApp.CasPripojeni
            };
        }

        public static UzivatelSkupinaApp GetAppFromDTO(UzivatelSkupina p_UzivatelSkupina)
        {
            return new UzivatelSkupinaApp()
            {
                IDUzivatel = p_UzivatelSkupina.IDUzivatel,
                IDSkupina = p_UzivatelSkupina.IDSkupina,
                IDUzivatelSkupina = p_UzivatelSkupina.IDUzivatelSkupina,
                Spravce = p_UzivatelSkupina.Spravce,
                CasOdpojeni = p_UzivatelSkupina.CasOdpojeni,
                CasPripojeni = p_UzivatelSkupina.CasPripojeni
            };
        }

        public static List<UzivatelSkupinaApp> GetAppFromDTO(List<UzivatelSkupina> p_UzivatelSkupina)
        {
            var output = new List<UzivatelSkupinaApp>();
            foreach (var item in p_UzivatelSkupina)
            {
                output.Add(GetAppFromDTO(item));
            }
            return output;
        }

        public static List<UzivatelSkupina> GetDTOFromApp(List<UzivatelSkupinaApp> p_UzivatelSkupinaApp)
        {
            var output = new List<UzivatelSkupina>();
            foreach (var item in p_UzivatelSkupinaApp)
            {
                output.Add(GetDTOFromApp(item));
            }
            return output;
        }
    }
}
