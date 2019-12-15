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
            return await SpecialOperations.SplneniUkolu(p_IDUkol, Connection, new AllDA(Connection));
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

                using (var reader = await command.ExecuteReaderAsync())
                    output = GetDTOList(reader);
            }

            return output;
        }

        public override List<Ukol> GetDTOList(DbDataReader p_Reader)
        {
            var output = new List<Ukol>();
            while (p_Reader.Read())
            {
                output.Add(GetDTO(p_Reader));
            }
            return output;
        }

        protected override void AddParameters(OracleCommand p_Command, Ukol p_Ukol, bool p_UseID = true)
        {
            p_Command.Parameters.Add(":CasVytvoreni", p_Ukol.CasVytvoreni);
            p_Command.Parameters.Add(":Nazev", p_Ukol.Nazev);
            p_Command.Parameters.Add(":Popis", p_Ukol.Popis);
            p_Command.Parameters.Add(":CasSplneni", p_Ukol.CasSplneni);
            p_Command.Parameters.Add(":Termin", p_Ukol.Termin);
            p_Command.Parameters.Add(":Priorita_ID", p_Ukol.IDPriorita);
            p_Command.Parameters.Add(":Mandays", p_Ukol.Mandays);
            if (p_UseID)
                p_Command.Parameters.Add(":ID", p_Ukol.IDUkol);
        }

        public override Ukol GetDTO(DbDataReader p_Reader)
        {
            return new Ukol
            {
                IDUkol = Convert.ToInt32(p_Reader["uk_id"]),
                CasVytvoreni = Convert.ToDateTime(p_Reader["uk_casvytvoreni"]),
                Nazev = Convert.ToString(p_Reader["uk_nazev"]),
                Popis = Convert.ToString(p_Reader["uk_popis"]),
                CasSplneni = p_Reader["uk_cassplneni"].GetValue<DateTime?>(),
                Termin = Convert.ToDateTime(p_Reader["uk_termin"]),
                IDPriorita = Convert.ToInt32(p_Reader["uk_priorita_id"]),
                Mandays = Convert.ToInt32(p_Reader["uk_mandays"])
            };
        }
    }
}
