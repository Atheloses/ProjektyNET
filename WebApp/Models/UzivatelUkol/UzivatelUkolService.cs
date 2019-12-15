using DAO.Tables;
using DomainLogic;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTOApp;

namespace WebApp.Models
{
    public class UzivatelUkolService
    {

        public async Task<IEnumerable<int>> ZiskejUzivateleUkolu(int p_Id)
        {
            IEnumerable<int> output;

            try
            {
                using var uzivatelUkolDL = new UzivatelUkolDL();
                var all = await uzivatelUkolDL.ZiskejUzivatelUkoly();
                output = all.Where(x => x.IDUkol == p_Id).Select(x => x.IDUzivatel);
            }
            catch (Exception ex) { throw ex; }

            return output;
        }

        public async Task<IEnumerable<UzivatelUkolApp>> ZiskejUzivatelUkoly()
        {
            IEnumerable<UzivatelUkolApp> output;

            try
            {
                using var uzivatelUkolDL = new UzivatelUkolDL();
                output = UzivatelUkolApp.GetAppFromDTO(await uzivatelUkolDL.ZiskejUzivatelUkoly());
            }
            catch (Exception ex) { throw ex; }

            return output;
        }

        public async Task<IEnumerable<UzivatelUkolApp>> ZiskejUzivatelUkolyProUzivatele(int p_IDUzivatel)
        {
            IEnumerable<UzivatelUkolApp> output;

            try
            {
                using var uzivatelUkolDL = new UzivatelUkolDL();
                output = UzivatelUkolApp.GetAppFromDTO(await uzivatelUkolDL.ZiskejUzivatelUkoly(p_IDUzivatel));
            }
            catch (Exception ex) { throw ex; }

            return output;
        }

        public async Task<bool> SplnitUzivatelUkol(int p_IDUzivatelUkol)
        {
            bool output;
            try
            {
                using var uzivatelUkolDL = new UzivatelUkolDL();
                output = await uzivatelUkolDL.SplnitUzivatelUkol(p_IDUzivatelUkol);
            }
            catch (Exception ex) { throw ex; }

            return output;
        }

        public async Task<UzivatelUkolApp> ZiskejUzivatelUkol(int p_Id)
        {
            UzivatelUkolApp output = new UzivatelUkolApp();

            try
            {
                using var uzivatelUkolDL = new UzivatelUkolDL();
                output = UzivatelUkolApp.GetAppFromDTO(await uzivatelUkolDL.ZiskejUzivatelUkol(p_Id));
            }
            catch (Exception ex){ throw ex; }

            return output;
        }

        public async Task<bool> UlozUzivatelUkol(UzivatelUkolApp p_Podukol)
        {
            try
            {
                using var uzivatelUkolDL = new UzivatelUkolDL();
                await uzivatelUkolDL.UlozUzivatelUkol(UzivatelUkolApp.GetDTOFromApp(p_Podukol));
            }
            catch (Exception ex) { throw ex; }

            return true;
        }

        public async Task<bool> VytvorUzivatelUkol(UzivatelUkolApp p_Podukol)
        {
            try
            {
                using var uzivatelUkolDL = new UzivatelUkolDL();
                await uzivatelUkolDL.VytvorUzivatelUkol(UzivatelUkolApp.GetDTOFromApp(p_Podukol));
            }
            catch (Exception ex) { throw ex; }

            return true;
        }
    }
}
