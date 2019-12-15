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
        private string _SQL_SELECT_ALL = "SELECT " + _SQL_COLUMNS + " FROM UzivatelUkol uzuk";
        private string _SQL_SEQ_VALUE = "select uzivatel_ukol_seq.CURRVAL as value from dual";

        public override string SQL_SEQ_VALUE => _SQL_SEQ_VALUE;
        public override string SQL_SELECT_ALL => _SQL_SELECT_ALL;
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

        public override List<UzivatelUkol> GetDTOList(DataTable p_DataTable)
        {
            var output = new List<UzivatelUkol>();
            foreach (DataRow row in p_DataTable.Rows)
                output.Add(GetDTO(row));

            return output;
        }

        public override void AddParameters(Dictionary<string, object> p_Parameters, UzivatelUkol p_UzivatelUkol, bool p_UseID = true)
        {
            p_Parameters.Add(":Spravce", p_UzivatelUkol.Spravce);
            p_Parameters.Add(":CasPripojeni", p_UzivatelUkol.CasPripojeni);
            p_Parameters.Add(":CasSplneni", p_UzivatelUkol.CasSplneni);
            p_Parameters.Add(":Termin", p_UzivatelUkol.Termin);
            p_Parameters.Add(":Uzivatel_ID", p_UzivatelUkol.IDUzivatel);
            p_Parameters.Add(":Ukol_ID", p_UzivatelUkol.IDUkol);
            p_Parameters.Add(":Popis", p_UzivatelUkol.Popis);
            if (p_UseID)
                p_Parameters.Add(":ID", p_UzivatelUkol.IDUzivatelUkol);
        }

        public override void AddParametersID(Dictionary<string, object> p_Parameters, int p_ID)
        {
            p_Parameters.Add(":ID", p_ID);
        }

        public override int GetSeqValue(DataTable p_DataTable)
        {
            return Convert.ToInt32(((DataRow)p_DataTable.Rows[0])["value"]);
        }

        public override UzivatelUkol GetDTO(DataRow p_DataRow)
        {
            return new UzivatelUkol
            {
                IDUzivatelUkol = Convert.ToInt32(p_DataRow["uzuk_id"]),
                Spravce = Convert.ToString(p_DataRow["uzuk_spravce"]),
                CasPripojeni = Convert.ToDateTime(p_DataRow["uzuk_caspripojeni"]),
                CasSplneni = p_DataRow["uzuk_cassplneni"].GetValue<DateTime?>(),
                Termin = Convert.ToDateTime(p_DataRow["uzuk_termin"]),
                IDUzivatel = Convert.ToInt32(p_DataRow["uzuk_uzivatel_id"]),
                IDUkol = Convert.ToInt32(p_DataRow["uzuk_ukol_id"]),
                Popis = Convert.ToString(p_DataRow["uzuk_popis"])
            };
        }
    }
}
