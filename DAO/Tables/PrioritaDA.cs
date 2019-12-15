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
    public class PrioritaDA : CRUD<Priorita>
    {
        private static string _SQL_UPDATE = "UPDATE Priorita SET Nazev=:Nazev,Popis=:Popis where ID=:ID";
        private static string _SQL_INSERT = "INSERT INTO Priorita (Nazev,Popis) VALUES" +
            "(:Nazev,:Popis)";
        private string _SQL_SELECT = "SELECT " + _SQL_COLUMNS + " FROM Priorita pr WHERE pr.id=:ID";
        private static string _SQL_COLUMNS = "pr.id pr_id, pr.nazev pr_nazev, pr.popis pr_popis";
        private static string _SQL_DROP = "DELETE FROM Priorita WHERE id=:ID";
        private string _SQL_SELECT_ALL = "SELECT " + _SQL_COLUMNS + " FROM Priorita pr";
        private string _SQL_SEQ_VALUE = "select priorita_seq.CURRVAL as value from dual";

        public override string SQL_SEQ_VALUE => _SQL_SEQ_VALUE;
        public override string SQL_SELECT_ALL => _SQL_SELECT_ALL;
        public override string SQL_UPDATE => _SQL_UPDATE;
        public override string SQL_INSERT => _SQL_INSERT;
        public override string SQL_SELECT => _SQL_SELECT;
        public override string SQL_DROP => _SQL_DROP;
        public override string SQL_COLUMNS => _SQL_COLUMNS;

        public PrioritaDA(OracleConnection p_Connection)
        {
            Connection = p_Connection;
        }

        public override List<Priorita> GetDTOList(DataTable p_DataTable)
        {
            var output = new List<Priorita>();
            foreach (DataRow row in p_DataTable.Rows)
                output.Add(GetDTO(row));

            return output;
        }

        public override void AddParameters(Dictionary<string,object> p_Parameters, Priorita p_Priorita, bool p_UseID = true)
        {
            p_Parameters.Add(":Nazev", p_Priorita.Nazev);
            p_Parameters.Add(":Popis", p_Priorita.Popis);
            if (p_UseID)
                p_Parameters.Add(":ID", p_Priorita.IDPriorita);
        }

        public override void AddParametersID(Dictionary<string, object> p_Parameters, int p_ID)
        {
            p_Parameters.Add(":ID", p_ID);
        }

        public override int GetSeqValue(DataTable p_DataTable)
        {
            return Convert.ToInt32(((DataRow)p_DataTable.Rows[0])["value"]);
        }

        public override Priorita GetDTO(DataRow p_DataRow)
        {
            return new Priorita
            {
                IDPriorita = Convert.ToInt32(p_DataRow["pr_id"]),
                Nazev = Convert.ToString(p_DataRow["pr_nazev"]),
                Popis = Convert.ToString(p_DataRow["pr_popis"])
            };
        }
    }
}
