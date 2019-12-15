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
        private string _SQL_SELECT_ALL = "SELECT " + _SQL_COLUMNS + " FROM UzivatelSkupina uzsk";
        private string _SQL_SEQ_VALUE = "select uzivatel_skupina_seq.CURRVAL as value from dual";

        public override string SQL_SEQ_VALUE => _SQL_SEQ_VALUE;
        public override string SQL_SELECT_ALL => _SQL_SELECT_ALL;
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

        public override List<UzivatelSkupina> GetDTOList(DataTable p_DataTable)
        {
            var output = new List<UzivatelSkupina>();
            foreach (DataRow row in p_DataTable.Rows)
                output.Add(GetDTO(row));

            return output;
        }

        public override void AddParameters(Dictionary<string, object> p_Parameters, UzivatelSkupina p_UzivatelSkupina, bool p_UseID = true)
        {
            p_Parameters.Add(":Spravce", p_UzivatelSkupina.Spravce);
            p_Parameters.Add(":CasPripojeni", p_UzivatelSkupina.CasPripojeni);
            p_Parameters.Add(":Uzivatel_ID", p_UzivatelSkupina.IDUzivatel);
            p_Parameters.Add(":Skupina_ID", p_UzivatelSkupina.IDSkupina);
            p_Parameters.Add(":CasOdpojeni", p_UzivatelSkupina.CasOdpojeni);
            if (p_UseID)
                p_Parameters.Add(":ID", p_UzivatelSkupina.IDUzivatelSkupina);
        }

        public override void AddParametersID(Dictionary<string, object> p_Parameters, int p_ID)
        {
            p_Parameters.Add(":ID", p_ID);
        }

        public override int GetSeqValue(DataTable p_DataTable)
        {
            return Convert.ToInt32(((DataRow)p_DataTable.Rows[0])["value"]);
        }

        public override UzivatelSkupina GetDTO(DataRow p_DataRow)
        {
            return new UzivatelSkupina
            {
                IDUzivatelSkupina = Convert.ToInt32(p_DataRow["uzsk_id"]),
                Spravce = Convert.ToString(p_DataRow["uzsk_spravce"]),
                CasPripojeni = Convert.ToDateTime(p_DataRow["uzsk_caspripojeni"]),
                IDUzivatel = Convert.ToInt32(p_DataRow["uzsk_uzivatel_id"]),
                IDSkupina = Convert.ToInt32(p_DataRow["uzsk_skupina_id"]),
                CasOdpojeni = p_DataRow["uzsk_casodpojeni"].GetValue<DateTime?>()
            };
        }
    }
}
