using DAO;
using DAO.Tables;
using DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DomainLogic
{
    public class UkolDL : BaseDL
    {

        public UkolDL() : base()
        {

        }

        /// <summary>
        /// Uloží uživatel ukol
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        public async Task<bool> UlozUkol(Ukol p_Ukol)
        {
            return await new UkolDA(connection).Update(p_Ukol);
        }

        public async Task<bool> VytvorUkol(Ukol p_Ukol, IEnumerable<int> p_IDUzivatele, IEnumerable<int> p_IDSkupiny)
        {
            return await SpecialOperations.VytvoreniUkolu(p_Ukol, string.Join(',', p_IDUzivatele), string.Join(',', p_IDUzivatele), connection);
        }

        /// <summary>
        /// Vrátí všechny záznamy z tabulky uživatel ukol
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        public async Task<List<Ukol>> ZiskejUkoly()
        {
            return await new UkolDA(connection).SelectAll();
        }

        public async Task<Dictionary<Ukol, IEnumerable<UzivatelUkol>>> ZiskejUkolyUzivatele(int p_IDUzivatel)
        {
            var uzivatelUkoly = (await new UzivatelUkolDA(connection).SelectAll()).Where(i => i.IDUzivatel == p_IDUzivatel);
            Dictionary<Ukol, IEnumerable<UzivatelUkol>> output = new Dictionary<Ukol, IEnumerable<UzivatelUkol>>();
            IEnumerable<int> IDUkolu = uzivatelUkoly.Select(i => i.IDUkol);
            if (IDUkolu.Count() > 0)
            {
                var Ukoly = await new UkolDA(connection).SelectId(IDUkolu);
                foreach (var uzivatelUkol in uzivatelUkoly)
                {
                    Ukol tempUkol = Ukoly.First(i => i.IDUkol == uzivatelUkol.IDUkol);

                    output.TryGetValue(tempUkol, out IEnumerable<UzivatelUkol> tempList);
                    if (tempList != null)
                        tempList.Append(uzivatelUkol);
                    else
                        output.Add(tempUkol, new List<UzivatelUkol>().Append(uzivatelUkol));
                }
            }

            return output;
        }

        /// <summary>
        /// Vrátí uživatel ukol vazbu s ID
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        public async Task<Ukol> ZiskejUkol(int p_Id)
        {
            return await new UkolDA(connection).SelectId(p_Id);
        }

        public async Task<bool> SplnitUkol(int p_IDUkol)
        {
            return await new UkolDA(connection).SplneniUkolu(p_IDUkol);
        }

        /// <summary>
        /// Vrátí seznam podúkolů uživatele s ID
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        //public async Task<IEnumerable<Ukol>> ZiskejPodukolyUzivatele(int p_Id)
        //{
        //    var podukoly = await new UkolDA(connection).SelectAll();
        //    return podukoly.Where(i => i.IDUzivatel == p_Id);
        //}
    }
}
