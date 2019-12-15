using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DomainLogic
{
    public class BaseDL : IDisposable
    {
        protected OracleConnection connection;
        private static string connetionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=dbsys.cs.vsb.cz)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=oracle.dbsys.cs.vsb.cz)));User id=pus0065;Password=x;";

        public BaseDL()
        {
            OpenConnection();
        }

        private void OpenConnection()
        {
            connection = new OracleConnection(connetionString);
            //OracleConnection.ClearAllPools();
            Task open;
            do
            {
                open = connection.OpenAsync();
            }
            while (!open.IsCompletedSuccessfully);
        }

        public void Dispose()
        {
            //if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }
}
