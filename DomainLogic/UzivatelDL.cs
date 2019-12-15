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
    public class UzivatelDL : BaseDL
    {
        public UzivatelDL() : base()
        {

        }

        /// <summary>
        /// Uloží uživatel ukol
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        public async Task<bool> UlozUzivatel(Uzivatel p_Uzivatel)
        {
            return await new UzivatelDA(connection).Update(p_Uzivatel);
        }

        public async Task<bool> VytvorUzivatel(Uzivatel p_Uzivatel)
        {
            return await new UzivatelDA(connection).InsertProc(p_Uzivatel);
        }

        public async Task<Uzivatel> ZiskejUzivatelNormPrezdivka(string p_NormPrezdivka)
        {
            return await new UzivatelDA(connection).SelectNormPrezdivka(p_NormPrezdivka);
        }

        /// <summary>
        /// Vrátí všechny záznamy z tabulky uživatel ukol
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        public async Task<List<Uzivatel>> ZiskejUzivatele()
        {
            return await new UzivatelDA(connection).SelectAll();
        }

        /// <summary>
        /// Vrátí uživatel ukol vazbu s ID
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        public async Task<Uzivatel> ZiskejUzivatel(int p_Id)
        {
            return await new UzivatelDA(connection).SelectId(p_Id);
        }

        public async Task<bool> SmazUzivatel(int p_Id)
        {
            return await new UzivatelDA(connection).DropId(p_Id);
        }
    }
}
