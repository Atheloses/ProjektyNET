using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data;
using System.Data.Common;

namespace DAO.Tables
{
    public class SkupinaDA : CRUD<Skupina>
    {
        private static string _SQL_UPDATE = "UPDATE Skupina SET Nazev=:Nazev,DatumVytvoreni=:DatumVytvoreni,Popis=:Popis,Efektivita=:Efektivita where ID=:ID";
        private static string _SQL_INSERT = "INSERT INTO Skupina (Nazev,DatumVytvoreni,Popis,Efektivita) VALUES" +
            "(:Nazev,:DatumVytvoreni,:Popis,:Efektivita)";
        private string _SQL_SELECT = "SELECT " + _SQL_COLUMNS + " FROM Skupina sk WHERE sk.id=:ID";
        private static string _SQL_COLUMNS = "sk.id sk_id, sk.nazev sk_nazev, sk.datumvytvoreni sk_datumvytvoreni," +
            "sk.popis sk_popis, sk.efektivita sk_efektivita";
        private static string _SQL_DROP = "DELETE FROM Skupina WHERE id=:ID";
        private string _SQL_SELECT_ALL = "SELECT " + _SQL_COLUMNS + " FROM Skupina sk";
        private string _SQL_SEQ_VALUE = "select skupina_seq.CURRVAL as value from dual";

        public override string SQL_SEQ_VALUE => _SQL_SEQ_VALUE;
        public override string SQL_SELECT_ALL => _SQL_SELECT_ALL;
        public override string SQL_UPDATE => _SQL_UPDATE;
        public override string SQL_INSERT => _SQL_INSERT;
        public override string SQL_SELECT => _SQL_SELECT;
        public override string SQL_DROP => _SQL_DROP;
        public override string SQL_COLUMNS => _SQL_COLUMNS;

        public SkupinaDA(OracleConnection p_Connection)
        {
            Connection = p_Connection;
        }

        public override List<Skupina> GetDTOList(DataTable p_DataTable)
        {
            var output = new List<Skupina>();
            foreach (DataRow row in p_DataTable.Rows)
                output.Add(GetDTO(row));

            return output;
        }

        public override void AddParameters(Dictionary<string, object> p_Parameters, Skupina p_Skupina, bool p_UseID = true)
        {
            p_Parameters.Add(":Nazev", p_Skupina.Nazev);
            p_Parameters.Add(":DatumVytvoreni", p_Skupina.DatumVytvoreni);
            p_Parameters.Add(":Popis", p_Skupina.Popis);
            p_Parameters.Add(":Efektivita", p_Skupina.Efektivita);
            if (p_UseID)
                p_Parameters.Add(":ID", p_Skupina.IDSkupina);
        }

        public override void AddParametersID(Dictionary<string, object> p_Parameters, int p_ID)
        {
            p_Parameters.Add(":ID", p_ID);
        }

        public override int GetSeqValue(DataTable p_DataTable)
        {
            return Convert.ToInt32(((DataRow)p_DataTable.Rows[0])["value"]);
        }

        public override Skupina GetDTO(DataRow p_DataRow)
        {
            return new Skupina
            {
                IDSkupina = Convert.ToInt32(p_DataRow["sk_id"]),
                Nazev = Convert.ToString(p_DataRow["sk_nazev"]),
                DatumVytvoreni = Convert.ToDateTime(p_DataRow["sk_datumvytvoreni"]),
                Popis = Convert.ToString(p_DataRow["sk_popis"]),
                Efektivita = Convert.ToString(p_DataRow["sk_efektivita"])
            };
        }
    }
}
