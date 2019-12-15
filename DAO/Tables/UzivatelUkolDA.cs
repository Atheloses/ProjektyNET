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
    public class UzivatelUkolDA : CRUD<UzivatelUkol>
    {
        private static string _SQL_UPDATE = "UPDATE UzivatelUkol SET Spravce=:Spravce,CasPripojeni=:CasPripojeni," +
            "CasSplneni=:CasSplneni,Termin=:Termin,Uzivatel_ID=:Uzivatel_ID,Ukol_ID=:Ukol_ID,Popis=:Popis where ID=:ID";
        private static string _SQL_INSERT = "INSERT INTO UzivatelUkol (Spravce,CasPripojeni,CasSplneni,Termin,Uzivatel_ID,Ukol_ID,Popis) VALUES" +
            "(:Spravce,:CasPripojeni,:CasSplneni,:Termin,:Uzivatel_ID,:Ukol_ID,:Popis)";
        private string _SQL_SELECT = "SELECT " + _SQL_COLUMNS + " FROM UzivatelUkol uzuk WHERE uzuk.id=:ID";
        private static string _SQL_COLUMNS = "uzuk.id uzuk_id,uzuk.spravce uzuk_spravce,uzuk.caspripojeni uzuk_caspripojeni,uzuk.termin uzuk_termin," +
            "uzuk.uzivatel_id uzuk_uzivatel_id,uzuk.ukol_id uzuk_ukol_id,uzuk.cassplneni uzuk_cassplneni,uzuk.popis uzuk_popis";
        private static string _SQL_DROP = "DELETE FROM UzivatelUkol WHERE id=:ID";

        public override string SQL_UPDATE => _SQL_UPDATE;
        public override string SQL_INSERT => _SQL_INSERT;
        public override string SQL_SELECT => _SQL_SELECT;
        public override string SQL_DROP => _SQL_DROP;
        public override string SQL_COLUMNS => _SQL_COLUMNS;

        public UzivatelUkolDA(OracleConnection p_Connection)
        {
            Connection = p_Connection;
        }

        [Obsolete("DropId is deprecated, please use SplneniPodukolu instead")]
        public new async Task<bool> DropId(int p_ID)
        {
            return await base.DropId(p_ID);
        }

        public async Task<bool> SplnitId(int p_IDUzivatelUkol)
        {
            string text = "call SplnitPodukolUzivatele(:IDUzivatelUkol)";
            OracleCommand command = new OracleCommand(text, Connection);
            command.Parameters.Add(":IDUzivatelUkol", p_IDUzivatelUkol);
            await command.ExecuteNonQueryAsync();
            return true;
        }

        public override List<UzivatelUkol> GetDTOList(DbDataReader p_Reader)
        {
            var output = new List<UzivatelUkol>();
            while (p_Reader.Read())
            {
                output.Add(GetDTO(p_Reader));
            }
            return output;
        }

        protected override void AddParameters(OracleCommand p_Command, UzivatelUkol p_UzivatelUkol, bool p_UseID = true)
        {
            p_Command.Parameters.Add(":Spravce", p_UzivatelUkol.Spravce);
            p_Command.Parameters.Add(":CasPripojeni", p_UzivatelUkol.CasPripojeni);
            p_Command.Parameters.Add(":CasSplneni", p_UzivatelUkol.CasSplneni);
            p_Command.Parameters.Add(":Termin", p_UzivatelUkol.Termin);
            p_Command.Parameters.Add(":Uzivatel_ID", p_UzivatelUkol.IDUzivatel);
            p_Command.Parameters.Add(":Ukol_ID", p_UzivatelUkol.IDUkol);
            p_Command.Parameters.Add(":Popis", p_UzivatelUkol.Popis);
            if (p_UseID)
                p_Command.Parameters.Add(":ID", p_UzivatelUkol.IDUzivatelUkol);
        }

        public override UzivatelUkol GetDTO(DbDataReader p_Reader)
        {
            return new UzivatelUkol
            {
                IDUzivatelUkol = Convert.ToInt32(p_Reader["uzuk_id"]),
                Spravce = Convert.ToString(p_Reader["uzuk_spravce"]),
                CasPripojeni = Convert.ToDateTime(p_Reader["uzuk_caspripojeni"]),
                CasSplneni = p_Reader["uzuk_cassplneni"].GetValue<DateTime?>(),
                Termin = Convert.ToDateTime(p_Reader["uzuk_termin"]),
                IDUzivatel = Convert.ToInt32(p_Reader["uzuk_uzivatel_id"]),
                IDUkol = Convert.ToInt32(p_Reader["uzuk_ukol_id"]),
                Popis = Convert.ToString(p_Reader["uzuk_popis"])
            };
        }
    }
}
