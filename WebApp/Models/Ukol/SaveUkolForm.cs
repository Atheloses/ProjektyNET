using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTOApp;

namespace WebApp.Models
{
    public class SaveUkolForm
    {
        public UkolApp ukolApp { get; set; }
        public List<UzivatelApp> uzivateleApp { get; set; }
        public List<string> selectedUzivateleApp { get; set; }
        public IEnumerable<int> selectedUzivatele { get; set; }
        public IEnumerable<PrioritaApp> prioritaApp { get; set; }
        public string selectedPrioritaApp { get; set; }
    }
}
