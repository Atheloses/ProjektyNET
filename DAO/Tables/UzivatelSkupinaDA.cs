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
    public class UzivatelSkupinaDA : CRUD<UzivatelSkupina>
    {
        private static string _SQL_UPDATE = "UPDATE UzivatelSkupina SET Spravce=:Spravce,CasPripojeni=:CasPripojeni," +
            "Uzivatel_ID=:Uzivatel_ID,Skupina_ID=:Skupina_ID,CasOdpojeni=:CasOdpojeni where ID=:ID";
        private static string _SQL_INSERT = "INSERT INTO UzivatelSkupina (Spravce,CasPripojeni,Uzivatel_ID,Skupina_ID,CasOdpojeni) VALUES" +
            "(:Spravce,:CasPripojeni,:Uzivatel_ID,:Skupina_ID,:CasOdpojeni)";
        private string _SQL_SELECT = "SELECT " + _SQL_COLUMNS + " FROM UzivatelSkupina uzsk WHERE uzsk.id=:ID";
        private static string _SQL_COLUMNS = "uzsk.id uzsk_id,uzsk.spravce uzsk_spravce,uzsk.caspripojeni uzsk_caspripojeni," +
            "uzsk.uzivatel_id uzsk_uzivatel_id,uzsk.skupina_id uzsk_skupina_id,uzsk.casodpojeni uzsk_casodpojeni";
        private static string _SQL_DROP = "DELETE FROM UzivatelSkupina WHERE id=:ID";

        public override string SQL_UPDATE => _SQL_UPDATE;
        public override string SQL_INSERT => _SQL_INSERT;
        public override string SQL_SELECT => _SQL_SELECT;
        public override string SQL_DROP => _SQL_DROP;
        public override string SQL_COLUMNS => _SQL_COLUMNS;

        public UzivatelSkupinaDA(OracleConnection p_Connection)
        {
            Connection = p_Connection;
        }

        [Obsolete("DropId is deprecated, please use VyhozeniZeSkupiny instead")]
        public new async Task<bool> DropId(int p_ID)
        {
            return await base.DropId(p_ID);
        }

        public async void VyhozeniZeSkupiny(UzivatelSkupina p_UzivatelSkupina)
        {
            p_UzivatelSkupina.CasOdpojeni = DateTime.Now;
            await Update(p_UzivatelSkupina);
        }

        public override List<UzivatelSkupina> GetDTOList(DbDataReader p_Reader)
        {
            var output = new List<UzivatelSkupina>();
            while (p_Reader.Read())
            {
                output.Add(GetDTO(p_Reader));
            }
            return output;
        }

        protected override void AddParameters(OracleCommand p_Command, UzivatelSkupina p_UzivatelSkupina, bool p_UseID = true)
        {
            p_Command.Parameters.Add(":Spravce", p_UzivatelSkupina.Spravce);
            p_Command.Parameters.Add(":CasPripojeni", p_UzivatelSkupina.CasPripojeni);
            p_Command.Parameters.Add(":Uzivatel_ID", p_UzivatelSkupina.IDUzivatel);
            p_Command.Parameters.Add(":Skupina_ID", p_UzivatelSkupina.IDSkupina);
            p_Command.Parameters.Add(":CasOdpojeni", p_UzivatelSkupina.CasOdpojeni);
            if (p_UseID)
                p_Command.Parameters.Add(":ID", p_UzivatelSkupina.IDUzivatelSkupina);
        }

        public override UzivatelSkupina GetDTO(DbDataReader p_Reader)
        {
            return new UzivatelSkupina
            {
                IDUzivatelSkupina = Convert.ToInt32(p_Reader["uzsk_id"]),
                Spravce = Convert.ToString(p_Reader["uzsk_spravce"]),
                CasPripojeni = Convert.ToDateTime(p_Reader["uzsk_caspripojeni"]),
                IDUzivatel = Convert.ToInt32(p_Reader["uzsk_uzivatel_id"]),
                IDSkupina = Convert.ToInt32(p_Reader["uzsk_skupina_id"]),
                CasOdpojeni = p_Reader["uzsk_casodpojeni"].GetValue<DateTime?>()
            };
        }
    }
}
