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

        public override string SQL_UPDATE => _SQL_UPDATE;
        public override string SQL_INSERT => _SQL_INSERT;
        public override string SQL_SELECT => _SQL_SELECT;
        public override string SQL_DROP => _SQL_DROP;
        public override string SQL_COLUMNS => _SQL_COLUMNS;

        public StatistikyUkoluGlobalDA(OracleConnection p_Connection)
        {
            Connection = p_Connection;
        }

        public override List<StatistikyUkoluGlobal> GetDTOList(DbDataReader p_Reader)
        {
            var output = new List<StatistikyUkoluGlobal>();
            while (p_Reader.Read())
            {
                output.Add(GetDTO(p_Reader));
            }
            return output;
        }

        protected override void AddParameters(OracleCommand p_Command, StatistikyUkoluGlobal p_StatistikyUkoluGlobal, bool p_UseID = true)
        {
            p_Command.Parameters.Add(":Kod", p_StatistikyUkoluGlobal.Kod);
            p_Command.Parameters.Add(":Popis", p_StatistikyUkoluGlobal.Popis);
            p_Command.Parameters.Add(":Hodnota", p_StatistikyUkoluGlobal.Hodnota);
            if (p_UseID)
                p_Command.Parameters.Add(":ID", p_StatistikyUkoluGlobal.IDStatistikyUkoluGlobal);
        }

        public override StatistikyUkoluGlobal GetDTO(DbDataReader p_Reader)
        {
            return new StatistikyUkoluGlobal
            {
                IDStatistikyUkoluGlobal = Convert.ToInt32(p_Reader["st_id"]),
                Kod = Convert.ToString(p_Reader["st_kod"]),
                Popis = Convert.ToString(p_Reader["st_popis"]),
                Hodnota = Convert.ToInt32(p_Reader["st_hodnota"])
            };
        }
    }
}
