using DAO.Tables;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainLogic
{
    public class PrioritaDL : BaseDL
    {
        public PrioritaDL() : base()
        {

        }

        /// <summary>
        /// Vrátí všechny záznamy z tabulky uživatel ukol
        /// </summary>
        /// <param name="p_Id"></param>
        /// <returns></returns>
        public async Task<List<Priorita>> ZiskejPriority()
        {
            return await new PrioritaDA(connection).SelectAll();
        }
    }
}
