using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopAppVIS.DTOApp
{
    public class DetailUkolApp
    {
        public int IDUkol { get; set; }
        public int IDUzivatelUkol { get; set; }
        public int IDUzivatel { get; set; }
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.DisplayName("Název úkolu")]
        public string NazevUkolu { get; set; }
        [System.ComponentModel.DisplayName("Popis úkolu")]
        public string PopisUkolu { get; set; }
        [System.ComponentModel.DisplayName("Splněn úkol")]
        public bool OznacenUkol { get; set; }
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.DisplayName("Termín úkolu")]
        public DateTime TerminUkolu { get; set; }
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DisplayName("Priorita úkolu")]
        public string PrioritaUkolu { get; set; }
        [System.ComponentModel.DisplayName("Popis podúkolu")]
        public string PopisPodukolu { get; set; }
        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.DisplayName("Termín podúkolu")]
        public DateTime TerminPodukolu { get; set; }
        [System.ComponentModel.DisplayName("Splněn podúkol")]
        public bool OznacenPodukol { get; set; }

        public static DetailUkolApp GetAppFromDTO(Ukol p_Ukol,UzivatelUkol p_UzivatelUkol)
        {
            return new DetailUkolApp()
            {
                IDUkol = p_Ukol.IDUkol,
                IDUzivatel = p_UzivatelUkol.IDUzivatel,
                IDUzivatelUkol = p_UzivatelUkol.IDUzivatelUkol,
                PopisUkolu = p_Ukol.Popis,
                NazevUkolu = p_Ukol.Nazev,
                TerminUkolu = p_Ukol.Termin,
                OznacenUkol = p_Ukol.CasSplneni == null ? false : true,
                PrioritaUkolu = p_Ukol.IDPriorita.ToString(),
                TerminPodukolu = p_UzivatelUkol.Termin,
                PopisPodukolu = p_UzivatelUkol.Popis,
                OznacenPodukol = p_UzivatelUkol.CasSplneni == null ? false : true
            };
        }
    }
}
