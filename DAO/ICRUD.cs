using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    interface ICRUD<T>
    {
        string SQL_INSERT { get; }
        string SQL_SELECT { get; }
        string SQL_UPDATE { get; }
        string SQL_DROP { get; }

        OracleConnection Connection { get; set; }
        Task<int> Insert(T p_Uzivatel);
        Task<List<T>> SelectAll();
        Task<T> SelectId(int p_ID);
        Task<bool> Update(T p_Uzivatel);
        Task<bool> DropId(int p_ID);
    }
}
