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
    public class SkupinaUkolDA : CRUD<SkupinaUkol>
    {
        private static string _SQL_UPDATE = "UPDATE SkupinaUkol SET CasPripojeni=:CasPripojeni," +
            "CasSplneni=:CasSplneni,Termin=:Termin,Skupina_ID=:Skupina_ID,Ukol_ID=:Ukol_ID,Popis=:Popis where ID=:ID";
        private static string _SQL_INSERT = "INSERT INTO SkupinaUkol (CasPripojeni,CasSplneni,Termin,Skupina_ID,Ukol_ID,Popis) VALUES" +
            "(:CasPripojeni,:CasSplneni,:Termin,:Skupina_ID,:Ukol_ID,:Popis)";
        private string _SQL_SELECT = "SELECT " + _SQL_COLUMNS + " FROM SkupinaUkol skuk WHERE skuk.id=:ID";
        private static string _SQL_COLUMNS = "skuk.id skuk_id,skuk.caspripojeni skuk_caspripojeni,skuk.cassplneni skuk_cassplneni," +
            "skuk.termin skuk_termin,skuk.skupina_id skuk_skupina_id,skuk.ukol_id skuk_ukol_id,skuk.popis skuk_popis";
        private static string _SQL_DROP = "DELETE FROM SkupinaUkol WHERE id=:ID";

        public override string SQL_UPDATE => _SQL_UPDATE;
        public override string SQL_INSERT => _SQL_INSERT;
        public override string SQL_SELECT => _SQL_SELECT;
        public override string SQL_DROP => _SQL_DROP;
        public override string SQL_COLUMNS => _SQL_COLUMNS;

        public SkupinaUkolDA(OracleConnection p_Connection)
        {
            Connection = p_Connection;
        }

        [Obsolete("DropId is deprecated, please use SplneniPodukolu instead")]
        public new async Task<bool> DropId(int p_ID)
        {
            return await base.DropId(p_ID);
        }

        public async void SplneniPodukolu(SkupinaUkol p_SkupinaUkol)
        {
            p_SkupinaUkol.CasSplneni = DateTime.Now;
            await Update(p_SkupinaUkol);
        }

        public override List<SkupinaUkol> GetDTOList(DbDataReader p_Reader)
        {
            var output = new List<SkupinaUkol>();
            while (p_Reader.Read())
            {
                output.Add(GetDTO(p_Reader));
            }
            return output;
        }

        protected override void AddParameters(OracleCommand p_Command, SkupinaUkol p_SkupinaUkol, bool p_UseID = true)
        {
            p_Command.Parameters.Add(":CasPripojeni", p_SkupinaUkol.CasPripojeni);
            p_Command.Parameters.Add(":CasSplneni", p_SkupinaUkol.CasSplneni);
            p_Command.Parameters.Add(":Termin", p_SkupinaUkol.Termin);
            p_Command.Parameters.Add(":Skupina_ID", p_SkupinaUkol.IDSkupina);
            p_Command.Parameters.Add(":Ukol_ID", p_SkupinaUkol.IDUkol);
            p_Command.Parameters.Add(":Popis", p_SkupinaUkol.Popis);
            if (p_UseID)
                p_Command.Parameters.Add(":ID", p_SkupinaUkol.IDSkupinaUkol);
        }

        public override SkupinaUkol GetDTO(DbDataReader p_Reader)
        {
            return new SkupinaUkol
            {
                IDSkupinaUkol = Convert.ToInt32(p_Reader["skuk_id"]),
                CasPripojeni = Convert.ToDateTime(p_Reader["skuk_caspripojeni"]),
                CasSplneni = p_Reader["skuk_cassplneni"].GetValue<DateTime?>(),
                Termin = Convert.ToDateTime(p_Reader["skuk_termin"]),
                IDSkupina = Convert.ToInt32(p_Reader["skuk_skupina_id"]),
                IDUkol = Convert.ToInt32(p_Reader["skuk_ukol_id"]),
                Popis = Convert.ToString(p_Reader["skuk_popis"])
            };
        }
    }
}
