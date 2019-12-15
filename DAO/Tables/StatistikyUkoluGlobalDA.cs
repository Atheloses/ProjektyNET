using DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Tables
{
    public class StatistikyUkoluGlobalDA : CRUD<StatistikyUkoluGlobal>
    {
        private static string _SQL_UPDATE = "UPDATE StatistikyUkoluGlobal SET Kod=:Kod,Popis=:Popis,Hodnota=:Hodnota where ID=:ID";
        private static string _SQL_INSERT = "INSERT INTO StatistikyUkoluGlobal (Kod,Popis,Hodnota) VALUES" +
            "(:Kod,:Popis,:Hodnota)";
        private string _SQL_SELECT = "SELECT " + _SQL_COLUMNS + " FROM StatistikyUkoluGlobal st WHERE st.id=:ID";
        private static string _SQL_COLUMNS = "st.id st_id, st.kod st_kod, st.popis st_popis, " +
            "st.hodnota st_hodnota";
        private static string _SQL_DROP = "DELETE FROM StatistikyUkoluGlobal WHERE id=:ID";
        private string _SQL_SELECT_ALL = "SELECT " + _SQL_COLUMNS + " FROM StatistikyUkoluGlobal st";
        private string _SQL_SEQ_VALUE = "select statistiky_ukolu_global_seq.CURRVAL as value from dual";

        public override string SQL_SEQ_VALUE => _SQL_SEQ_VALUE;
        public override string SQL_SELECT_ALL => _SQL_SELECT_ALL;
        public override string SQL_UPDATE => _SQL_UPDATE;
        public override string SQL_INSERT => _SQL_INSERT;
        public override string SQL_SELECT => _SQL_SELECT;
        public override string SQL_DROP => _SQL_DROP;
        public override string SQL_COLUMNS => _SQL_COLUMNS;

        public StatistikyUkoluGlobalDA(OracleConnection p_Connection)
        {
            Connection = p_Connection;
        }

        public override List<StatistikyUkoluGlobal> GetDTOList(DataTable p_DataTable)
        {
            var output = new List<StatistikyUkoluGlobal>();
            foreach (DataRow row in p_DataTable.Rows)
                output.Add(GetDTO(row));

            return output;
        }

        public override void AddParameters(Dictionary<string, object> p_Parameters, StatistikyUkoluGlobal p_StatistikyUkoluGlobal, bool p_UseID = true)
        {
            p_Parameters.Add(":Kod", p_StatistikyUkoluGlobal.Kod);
            p_Parameters.Add(":Popis", p_StatistikyUkoluGlobal.Popis);
            p_Parameters.Add(":Hodnota", p_StatistikyUkoluGlobal.Hodnota);
            if (p_UseID)
                p_Parameters.Add(":ID", p_StatistikyUkoluGlobal.IDStatistikyUkoluGlobal);
        }

        public override void AddParametersID(Dictionary<string, object> p_Parameters, int p_ID)
        {
            p_Parameters.Add(":ID", p_ID);
        }

        public override int GetSeqValue(DataTable p_DataTable)
        {
            return Convert.ToInt32(((DataRow)p_DataTable.Rows[0])["value"]);
        }

        public override StatistikyUkoluGlobal GetDTO(DataRow p_DataRow)
        {
            return new StatistikyUkoluGlobal
            {
                IDStatistikyUkoluGlobal = Convert.ToInt32(p_DataRow["st_id"]),
                Kod = Convert.ToString(p_DataRow["st_kod"]),
                Popis = Convert.ToString(p_DataRow["st_popis"]),
                Hodnota = Convert.ToInt32(p_DataRow["st_hodnota"])
            };
        }
    }
}
