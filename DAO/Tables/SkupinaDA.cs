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

        public override string SQL_UPDATE => _SQL_UPDATE;
        public override string SQL_INSERT => _SQL_INSERT;
        public override string SQL_SELECT => _SQL_SELECT;
        public override string SQL_DROP => _SQL_DROP;
        public override string SQL_COLUMNS => _SQL_COLUMNS;

        public SkupinaDA(OracleConnection p_Connection)
        {
            Connection = p_Connection;
        }

        public override List<Skupina> GetDTOList(DbDataReader p_Reader)
        {
            List<Skupina> output = new List<Skupina>();
            while (p_Reader.Read())
            {
                output.Add(GetDTO(p_Reader));
            }
            return output;
        }

        protected override void AddParameters(OracleCommand p_Command, Skupina p_Skupina, bool p_UseID = true)
        {
            p_Command.Parameters.Add(":Nazev", p_Skupina.Nazev);
            p_Command.Parameters.Add(":DatumVytvoreni", p_Skupina.DatumVytvoreni);
            p_Command.Parameters.Add(":Popis", p_Skupina.Popis);
            p_Command.Parameters.Add(":Efektivita", p_Skupina.Efektivita);
            if (p_UseID)
                p_Command.Parameters.Add(":ID", p_Skupina.IDSkupina);
        }

        public override Skupina GetDTO(DbDataReader p_Reader)
        {
            return new Skupina
            {
                IDSkupina = Convert.ToInt32(p_Reader["sk_id"]),
                Nazev = Convert.ToString(p_Reader["sk_nazev"]),
                DatumVytvoreni = Convert.ToDateTime(p_Reader["sk_datumvytvoreni"]),
                Popis = Convert.ToString(p_Reader["sk_popis"]),
                Efektivita = Convert.ToString(p_Reader["sk_efektivita"])
            };
        }
    }
}
