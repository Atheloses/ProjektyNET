using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTOApp;
using DomainLogic;

namespace WebApp.Models
{
    public class UkolService
    {
        public async Task<IEnumerable<UkolApp>> ZiskejUkoly()
        {
            IEnumerable<UkolApp> output;

            using var ukolDL = new UkolDL();
            output = UkolApp.GetAppFromDTO(await ukolDL.ZiskejUkoly());

            return output;
        }

        public async Task<IEnumerable<PrioritaApp>> ZiskejPriority()
        {
            IEnumerable<PrioritaApp> output;

            using var prioritaDL = new PrioritaDL();
            output = PrioritaApp.GetAppFromDTO(await prioritaDL.ZiskejPriority());

            return output;
        }

        public async Task<Dictionary<UkolApp, IEnumerable<UzivatelUkolApp>>> ZiskejUkolyUzivatele(int p_IDUkol)
        {
            Dictionary<UkolApp, IEnumerable<UzivatelUkolApp>> output = new Dictionary<UkolApp, IEnumerable<UzivatelUkolApp>>();
            try
            {
                using var UkolDL = new UkolDL();
                foreach (var row in await UkolDL.ZiskejUkolyUzivatele(p_IDUkol))
                    output.Add(UkolApp.GetAppFromDTO(row.Key),UzivatelUkolApp.GetAppFromDTO(row.Value));
            }
            catch (Exception ex) { throw ex; }

            return output;
        }

        public async Task<bool> SplnitUkol(int p_IDUkol)
        {
            bool output;
            try
            {
                using var UkolDL = new UkolDL();
                output = await UkolDL.SplnitUkol(p_IDUkol);
            }
            catch (Exception ex) { throw ex; }

            return output;
        }

        public async Task<UkolApp> ZiskejUkol(int p_Id)
        {
            UkolApp output = new UkolApp();

            try
            {
                using var UkolDL = new UkolDL();
                output = UkolApp.GetAppFromDTO(await UkolDL.ZiskejUkol(p_Id));
            }
            catch (Exception ex) { throw ex; }

            return output;
        }

        public async Task<bool> UlozUkol(UkolApp p_Podukol)
        {
            try
            {
                using var UkolDL = new UkolDL();
                await UkolDL.UlozUkol(UkolApp.GetDTOFromApp(p_Podukol));
            }
            catch (Exception ex) { throw ex; }

            return true;
        }

        public async Task<bool> VytvorUkol(UkolApp p_Podukol, IEnumerable<int> p_IDUzivatele, IEnumerable<int> p_IDSkupiny)
        {
            try
            {
                using var UkolDL = new UkolDL();
                await UkolDL.VytvorUkol(UkolApp.GetDTOFromApp(p_Podukol), p_IDUzivatele, p_IDSkupiny);
            }
            catch (Exception ex) { throw ex; }

            return true;
        }
    }
}
