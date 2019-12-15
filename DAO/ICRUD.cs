using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    interface ICRUD<T>
    {
        string SQL_UPDATE { get; }
        string SQL_INSERT { get; }
        string SQL_SELECT { get; }
        string SQL_SELECT_ALL { get; }
        string SQL_DROP { get; }
        string SQL_COLUMNS { get; }
        string SQL_SEQ_VALUE { get; }

        OracleConnection Connection { get; set; }
        Task<int> Insert(T p_Uzivatel);
        Task<List<T>> SelectAll();
        Task<T> SelectId(int p_ID);
        Task<bool> Update(T p_Uzivatel);
        Task<bool> DropId(int p_ID);
        List<T> GetDTOList(DataTable p_DataTable);
        int GetSeqValue(DataTable p_DataTable);
        T GetDTO(DataRow p_DataRow);
        void AddParameters(Dictionary<string, object> p_Parameters, T p_Object, bool p_UseID = true);
        void AddParametersID(Dictionary<string, object> p_Parameters, int p_ID);
    }
}
