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

        public override string SQL_UPDATE => _SQL_UPDATE;
        public override string SQL_INSERT => _SQL_INSERT;
        public override string SQL_SELECT => _SQL_SELECT;
        public override string SQL_DROP => _SQL_DROP;
        public override string SQL_COLUMNS => _SQL_COLUMNS;

        public PrioritaDA(OracleConnection p_Connection)
        {
            Connection = p_Connection;
        }

        public override List<Priorita> GetDTOList(DbDataReader p_Reader)
        {
            List<Priorita> output = new List<Priorita>();
            while (p_Reader.Read())
            {
                output.Add(GetDTO(p_Reader));
            }
            return output;
        }

        protected override void AddParameters(OracleCommand p_Command, Priorita p_Priorita, bool p_UseID = true)
        {
            p_Command.Parameters.Add(":Nazev", p_Priorita.Nazev);
            p_Command.Parameters.Add(":Popis", p_Priorita.Popis);
            if (p_UseID)
                p_Command.Parameters.Add(":ID", p_Priorita.IDPriorita);
        }

        public override Priorita GetDTO(DbDataReader p_Reader)
        {
            return new Priorita
            {
                IDPriorita = Convert.ToInt32(p_Reader["pr_id"]),
                Nazev = Convert.ToString(p_Reader["pr_nazev"]),
                Popis = Convert.ToString(p_Reader["pr_popis"])
            };
        }
    }
}
