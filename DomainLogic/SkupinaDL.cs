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
    public class SkupinaDL : BaseDL
    {

        public SkupinaDL() : base()
        {

        }

        /// <summary>
        /// Uloží skupinu
        /// </summary>
        /// <param name="p_Skupina"></param>
        /// <returns></returns>
        public async Task<bool> UlozSkupinu(Skupina p_Skupina)
        {
            return await new SkupinaDA(connection).Update(p_Skupina);
        }

        /// <summary>
        /// Vytvoří skupinu
        /// </summary>
        /// <param name="p_Skupina"></param>
        /// <returns></returns>
        public async Task<bool> VytvorSkupinu(Skupina p_Skupina, IEnumerable<int> p_IDUzivatele)
        {
            p_Skupina.Efektivita = "0";
            p_Skupina.DatumVytvoreni = DateTime.Now;
            var IDSkupina = await new SkupinaDA(connection).Insert(p_Skupina);

            var uzivatelSkupinaDA = new UzivatelSkupinaDA(connection);
            foreach (var IDUzivatel in p_IDUzivatele)
                await uzivatelSkupinaDA.Insert(new UzivatelSkupina() { CasPripojeni = p_Skupina.DatumVytvoreni, IDSkupina=IDSkupina, IDUzivatel=IDUzivatel});

            return true;
        }

        /// <summary>
        /// Vrátí všechny záznamy z tabulky skupina
        /// </summary>
        /// <returns></returns>
        public async Task<List<Skupina>> ZiskejSkupiny()
        {
            return await new SkupinaDA(connection).SelectAll();
        }

        /// <summary>
        /// Vrátí skupinu s ID
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        public async Task<Skupina> ZiskejSkupinu(int p_Id)
        {
            return await new SkupinaDA(connection).SelectId(p_Id);
        }

        /// <summary>
        /// Smaže skupinu a všechny její vazby
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        public async Task<bool> SmazatSkupinu(int p_Id)
        {
            var uzivatelSkupinaDA = new UzivatelSkupinaDA(connection);
            var vazby = await uzivatelSkupinaDA.SelectAll();
            foreach (var vazba in vazby)
                await uzivatelSkupinaDA.DropId(vazba.IDUzivatelSkupina);

            return await new SkupinaDA(connection).DropId(p_Id);
        }

        public async Task<IEnumerable<Skupina>> ZiskejSkupinyUzivatele(int p_IDUzivatel)
        {
            var uzivatelSkupiny = (await new UzivatelSkupinaDA(connection).SelectAll()).Where(i => i.IDUzivatel == p_IDUzivatel);
            IEnumerable<Skupina> output = new List<Skupina>();
            var skupinaDL = new SkupinaDL();
            if (uzivatelSkupiny.Count() > 0)
            {
                var skupiny = await skupinaDL.ZiskejSkupiny();
                output = skupiny.Where(x => uzivatelSkupiny.Any(y => y.IDSkupina == x.IDSkupina));
            }

            return output;
        }
    }
}
