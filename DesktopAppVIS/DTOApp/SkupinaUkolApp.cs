using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopAppVIS.DTOApp
{
    public class SkupinaUkolApp
    {
        public int IDUkol { get; set; }
        [System.ComponentModel.DisplayName("Označit")]
        public bool OznacenUkol { get; set; }
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.DisplayName("Název úkolu")]
        public string NazevUkolu { get; set; }
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.DisplayName("Termín")]
        public DateTime TerminPodukolu { get; set; }
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.DisplayName("Priorita")]
        public string PrioritaUkolu { get; set; }
        public int IDPodukol { get; set; }
        [System.ComponentModel.DisplayName("Označit")]
        public bool OznacenPodukol { get; set; }
        [System.ComponentModel.Browsable(false)]
        public bool SplnenUkol { get; set; }
        [System.ComponentModel.Browsable(false)]
        public bool SplnenPodukol { get; set; }

        public static SkupinaUkolApp GetAppFromDTO(SkupinaUkol p_SkupinaUkol)
        {
            return new SkupinaUkolApp()
            {
                IDUkol = p_SkupinaUkol.IDUkol,
                TerminPodukolu = p_SkupinaUkol.Termin,
                IDPodukol = p_SkupinaUkol.IDSkupinaUkol,
                SplnenPodukol = p_SkupinaUkol.CasSplneni == null ? false : true,
            };
        }

        public static List<SkupinaUkolApp> GetAppFromDTO(List<SkupinaUkol> p_SkupinaUkol)
        {
            var output = new List<SkupinaUkolApp>();
            foreach (var item in p_SkupinaUkol)
            {
                output.Add(GetAppFromDTO(item));
            }
            return output;
        }
    }
}
