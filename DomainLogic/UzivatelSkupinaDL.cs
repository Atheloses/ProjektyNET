using DAO.Tables;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLogic
{
    public class UzivatelSkupinaDL : BaseDL
    {

        public UzivatelSkupinaDL() : base()
        {

        }

        /// <summary>
        /// Uloží uživatel ukol
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        public async Task<bool> UlozUzivatelSkupina(UzivatelSkupina p_UzivatelSkupina)
        {
            return await new UzivatelSkupinaDA(connection).Update(p_UzivatelSkupina);
        }

        public async Task<int> VytvorUzivatelSkupina(UzivatelSkupina p_UzivatelSkupina)
        {
            return await new UzivatelSkupinaDA(connection).Insert(p_UzivatelSkupina);
        }

        /// <summary>
        /// Vrátí všechny záznamy z tabulky uživatel ukol
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UzivatelSkupina>> ZiskejUzivatelSkupiny()
        {
            return await new UzivatelSkupinaDA(connection).SelectAll();
        }

        /// <summary>
        /// Vrátí uživatel ukol vazbu s ID
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        public async Task<UzivatelSkupina> ZiskejUzivatelSkupina(int p_Id)
        {
            return await new UzivatelSkupinaDA(connection).SelectId(p_Id);
        }

        /// <summary>
        /// Vrátí seznam ID všech uživatelů pod touto skupinou
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<int>> ZiskejUzivateleSkupiny(int p_Id)
        {
            var all = await new UzivatelSkupinaDA(connection).SelectAll();
            return all.Where(x => x.IDSkupina == p_Id).Select(x => x.IDUzivatel);
        }

        /// <summary>
        /// Vrátí seznam podúkolů uživatele s ID
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UzivatelSkupina>> ZiskejUzivateloveSkupiny(int p_Id)
        {
            var podukoly = await new UzivatelSkupinaDA(connection).SelectAll();
            return podukoly.Where(i => i.IDUzivatel == p_Id);
        }
    }
}
