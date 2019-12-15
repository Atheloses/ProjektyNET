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
    public class UzivatelUkolDL : BaseDL
    {

        public UzivatelUkolDL() : base()
        {

        }

        /// <summary>
        /// Uloží uživatel ukol
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        public async Task<bool> UlozUzivatelUkol(UzivatelUkol p_UzivatelUkol)
        {
            return await new UzivatelUkolDA(connection).Update(p_UzivatelUkol);
        }

        public async Task<int> VytvorUzivatelUkol(UzivatelUkol p_UzivatelUkol)
        {
            return await new UzivatelUkolDA(connection).Insert(p_UzivatelUkol);
        }

        /// <summary>
        /// Vrátí všechny záznamy z tabulky uživatel ukol
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UzivatelUkol>> ZiskejUzivatelUkoly()
        {
            return await new UzivatelUkolDA(connection).SelectAll();
        }

        /// <summary>
        /// Vrátí uživatel ukol vazbu s ID
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        public async Task<UzivatelUkol> ZiskejUzivatelUkol(int p_Id)
        {
            return await new UzivatelUkolDA(connection).SelectId(p_Id);
        }

        public async Task<bool> SplnitUzivatelUkol(int p_IDUzivatelUkol)
        {
            return await new UzivatelUkolDA(connection).SplnitId(p_IDUzivatelUkol);
        }

        /// <summary>
        /// Vrátí seznam podúkolů uživatele s ID
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UzivatelUkol>> ZiskejUzivatelUkoly(int p_Id)
        {
            var podukoly = await new UzivatelUkolDA(connection).SelectAll();
            return podukoly.Where(i => i.IDUzivatel == p_Id);
        }
    }
}
