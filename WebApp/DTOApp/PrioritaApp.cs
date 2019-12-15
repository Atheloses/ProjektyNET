using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.DTOApp
{
    public class PrioritaApp
    {
        public int IDPriorita { get; set; }
        [System.ComponentModel.DisplayName("Název")]
        public string Nazev { get; set; }
        [System.ComponentModel.DisplayName("Popis")]
        public string Popis { get; set; }

        public static PrioritaApp GetAppFromDTO(Priorita p_Priorita)
        {
            return new PrioritaApp
            {
                IDPriorita = p_Priorita.IDPriorita,
                Nazev = p_Priorita.Nazev,
                Popis = p_Priorita.Popis,
            };
        }

        public static List<PrioritaApp> GetAppFromDTO(List<Priorita> p_Priorita)
        {
            var output = new List<PrioritaApp>();
            foreach (var item in p_Priorita)
            {
                output.Add(GetAppFromDTO(item));
            }
            return output;
        }
    }
}
