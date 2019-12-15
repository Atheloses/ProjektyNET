using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopAppVIS.DTOApp
{
    public class SkupinaApp
    {
        public int ID { get; set; }
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.DisplayName("Název")]
        public string Nazev { get; set; }
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.DisplayName("Popis")]
        public string Popis { get; set; }
        [System.ComponentModel.DisplayName("Datum vytvoření")]
        public DateTime DatumVytvoreni { get; set; }

        public static SkupinaApp GetAppFromDTO(Skupina p_Skupina)
        {
            return new SkupinaApp()
            {
                ID = p_Skupina.IDSkupina,
                Nazev = p_Skupina.Nazev,
                Popis = p_Skupina.Popis,
                DatumVytvoreni = p_Skupina.DatumVytvoreni
            };
        }

        public static IEnumerable<SkupinaApp> GetAppFromDTO(IEnumerable<Skupina> p_Skupina)
        {
            var output = new List<SkupinaApp>();
            foreach (var item in p_Skupina)
            {
                output.Add(GetAppFromDTO(item));
            }
            return output;
        }

        //public static List<SkupinaApp> GetAppFromDTO(List<DTO.UzivatelSkupina> p_Skupina)
        //{
        //    var output = new HashSet<SkupinaApp>();
        //    foreach (var item in p_Skupina)
        //    {
        //        output.Add(GetAppFromDTO(item.SkupinaFK));
        //    }
        //    return output.ToList();
        //}
    }
}
