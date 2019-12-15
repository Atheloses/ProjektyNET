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
    public class UkolDA : CRUD<Ukol>
    {
        private static string _SQL_UPDATE = "UPDATE Ukol SET CasVytvoreni=:CasVytvoreni,Nazev=:Nazev,Popis=:Popis," +
            "CasSplneni=:CasSplneni,Termin=:Termin,Priorita_ID=:Priorita_ID,Mandays=:Mandays where ID=:ID";
        private static string _SQL_INSERT = "INSERT INTO Ukol (CasVytvoreni,Nazev,Popis,CasSplneni,Termin,Priorita_ID,Mandays) VALUES" +
            "(:CasVytvoreni,:Nazev,:Popis,:CasSplneni,:Termin,:Priorita_ID,:Mandays)";
        private string _SQL_SELECT = "SELECT " + _SQL_COLUMNS + " FROM Ukol uk WHERE uk.id=:ID";
        private static string _SQL_COLUMNS = "uk.id uk_id,uk.casvytvoreni uk_casvytvoreni,uk.nazev uk_nazev,uk.popis uk_popis," +
            "uk.cassplneni uk_cassplneni,uk.termin uk_termin,uk.priorita_id uk_priorita_id,uk.mandays uk_mandays";
        private static string _SQL_DROP = "DELETE FROM Ukol WHERE id=:ID";
        private string _SQL_SELECT_ALL = "SELECT " + _SQL_COLUMNS + " FROM Ukol uk";
        private string _SQL_SEQ_VALUE = "select ukol_seq.CURRVAL as value from dual";

        public override string SQL_SEQ_VALUE => _SQL_SEQ_VALUE;
        public override string SQL_SELECT_ALL => _SQL_SELECT_ALL;
        public override string SQL_UPDATE => _SQL_UPDATE;
        public override string SQL_INSERT => _SQL_INSERT;
        public override string SQL_SELECT => _SQL_SELECT;
        public override string SQL_DROP => _SQL_DROP;
        public override string SQL_COLUMNS => _SQL_COLUMNS;

        public UkolDA(OracleConnection p_Connection)
        {
            Connection = p_Connection;
        }

        [Obsolete("DropId is deprecated, please use SplneniUkolu instead")]
        public new async Task<bool> DropId(int p_ID)
        {
            return await base.DropId(p_ID);
        }

        public async Task<bool> SplneniUkolu(int p_IDUkol)
        {
            return await SpecialOperations.SplneniUkolu(p_IDUkol, Connection);
        }

        public virtual async Task<IEnumerable<Ukol>> SelectId(IEnumerable<int> p_IDs)
        {
            IEnumerable<Ukol> output = null;

            using (var command = new OracleCommand("", Connection))
            {
                command.CommandText = SQL_SELECT;
                if (command.CommandText.Contains("WHERE"))
                    command.CommandText = command.CommandText.Remove(command.CommandText.IndexOf("WHERE"));
                command.CommandText += "where uk.id in (" + string.Join(',', p_IDs) + ")";

                using var reader = await command.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                output = GetDTOList(dataTable);
            }

            return output;
        }

        public override List<Ukol> GetDTOList(DataTable p_DataTable)
        {
            var output = new List<Ukol>();
            foreach (DataRow row in p_DataTable.Rows)
                output.Add(GetDTO(row));

            return output;
        }

        public override void AddParameters(Dictionary<string, object> p_Parameters, Ukol p_Ukol, bool p_UseID = true)
        {
            p_Parameters.Add(":CasVytvoreni", p_Ukol.CasVytvoreni);
            p_Parameters.Add(":Nazev", p_Ukol.Nazev);
            p_Parameters.Add(":Popis", p_Ukol.Popis);
            p_Parameters.Add(":CasSplneni", p_Ukol.CasSplneni);
            p_Parameters.Add(":Termin", p_Ukol.Termin);
            p_Parameters.Add(":Priorita_ID", p_Ukol.IDPriorita);
            p_Parameters.Add(":Mandays", p_Ukol.Mandays);
            if (p_UseID)
                p_Parameters.Add(":ID", p_Ukol.IDUkol);
        }

        public override void AddParametersID(Dictionary<string, object> p_Parameters, int p_ID)
        {
            p_Parameters.Add(":ID", p_ID);
        }

        public override int GetSeqValue(DataTable p_DataTable)
        {
            return Convert.ToInt32(((DataRow)p_DataTable.Rows[0])["value"]);
        }

        public override Ukol GetDTO(DataRow p_DataRow)
        {
            return new Ukol
            {
                IDUkol = Convert.ToInt32(p_DataRow["uk_id"]),
                CasVytvoreni = Convert.ToDateTime(p_DataRow["uk_casvytvoreni"]),
                Nazev = Convert.ToString(p_DataRow["uk_nazev"]),
                Popis = Convert.ToString(p_DataRow["uk_popis"]),
                CasSplneni = p_DataRow["uk_cassplneni"].GetValue<DateTime?>(),
                Termin = Convert.ToDateTime(p_DataRow["uk_termin"]),
                IDPriorita = Convert.ToInt32(p_DataRow["uk_priorita_id"]),
                Mandays = Convert.ToInt32(p_DataRow["uk_mandays"])
            };
        }
    }
}
