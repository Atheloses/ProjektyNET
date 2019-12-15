using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTOApp;
using DomainLogic;

namespace WebApp.Models
{
    public class SkupinaService
    {
        public async Task<IEnumerable<SkupinaApp>> ZiskejSkupiny()
        {
            IEnumerable<SkupinaApp> output;

            using var skupinaDL = new SkupinaDL();
            output = SkupinaApp.GetAppFromDTO(await skupinaDL.ZiskejSkupiny());

            return output;
        }

        public async Task<SkupinaApp> ZiskejSkupinu(int p_Id)
        {
            SkupinaApp output = new SkupinaApp();

            using var skupinaDL = new SkupinaDL();
            output = SkupinaApp.GetAppFromDTO(await skupinaDL.ZiskejSkupinu(p_Id));

            return output;
        }

        public async Task<IEnumerable<int>> ZiskejUzivateleSkupiny(int p_Id)
        {
            return await new UzivatelSkupinaDL().ZiskejUzivateleSkupiny(p_Id);
        }

        public async Task<bool> UlozSkupinu(SkupinaApp p_SkupinaApp)
        {
            using var skupinaDL = new SkupinaDL();
            await skupinaDL.UlozSkupinu(SkupinaApp.GetDTOFromApp(p_SkupinaApp));

            return true;
        }

        public async Task<bool> SmazatSkupinu(int p_Id)
        {
            return await new SkupinaDL().SmazatSkupinu(p_Id);
        }

        public async Task<bool> VytvorSkupinu(SkupinaApp p_SkupinaApp, IEnumerable<int> p_IDUzivatele)
        {
            using var skupinaDL = new SkupinaDL();
            await skupinaDL.VytvorSkupinu(SkupinaApp.GetDTOFromApp(p_SkupinaApp), p_IDUzivatele);

            return true;
        }
    }
}
