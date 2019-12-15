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
    public class KomentarDA : CRUD<Komentar>
    {
        private static string _SQL_UPDATE = "UPDATE Komentar SET Komentar=:Komentar,CasPridani=:CasPridani," +
            "Uzivatel_ID=:Uzivatel_ID,Ukol_ID=:Ukol_ID where ID=:ID";
        private static string _SQL_INSERT = "INSERT INTO Komentar (Komentar,CasPridani,Uzivatel_ID,Ukol_ID) VALUES" +
            "(:Komentar,:CasPridani,:Uzivatel_ID,:Ukol_ID)";
        private string _SQL_SELECT = "SELECT " + _SQL_COLUMNS + " FROM Komentar ko WHERE ko.id=:ID";
        private static string _SQL_COLUMNS = "ko.id ko_id,ko.komentar ko_komentar,ko.caspridani ko_caspridani," +
            "ko.uzivatel_id ko_uzivatel_id,ko.ukol_id ko_ukol_id";
        private static string _SQL_DROP = "DELETE FROM Komentar WHERE id=:ID";

        public override string SQL_UPDATE => _SQL_UPDATE;
        public override string SQL_INSERT => _SQL_INSERT;
        public override string SQL_SELECT => _SQL_SELECT;
        public override string SQL_DROP => _SQL_DROP;
        public override string SQL_COLUMNS => _SQL_COLUMNS;

        public KomentarDA(OracleConnection p_Connection)
        {
            Connection = p_Connection;
        }

        public override List<Komentar> GetDTOList(DbDataReader p_Reader)
        {
            var output = new List<Komentar>();
            while (p_Reader.Read())
            {
                output.Add(GetDTO(p_Reader));
            }
            return output;
        }

        protected override void AddParameters(OracleCommand p_Command, Komentar p_Komentar, bool p_UseID = true)
        {
            p_Command.Parameters.Add(":Komentar", p_Komentar.Koment);
            p_Command.Parameters.Add(":CasPridani", p_Komentar.CasPridani);
            p_Command.Parameters.Add(":Uzivatel_ID", p_Komentar.IDUzivatel);
            p_Command.Parameters.Add(":Ukol_ID", p_Komentar.IDUkol);
            if (p_UseID)
                p_Command.Parameters.Add(":ID", p_Komentar.IDKomentar);
        }

        public override Komentar GetDTO(DbDataReader p_Reader)
        {
            return new Komentar
            {
                IDKomentar = Convert.ToInt32(p_Reader["ko_id"]),
                Koment = Convert.ToString(p_Reader["ko_komentar"]),
                CasPridani = Convert.ToDateTime(p_Reader["ko_caspridani"]),
                IDUzivatel = Convert.ToInt32(p_Reader["ko_uzivatel_id"]),
                IDUkol = Convert.ToInt32(p_Reader["ko_ukol_id"])
            };
        }
    }
}
