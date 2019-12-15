using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public abstract class CRUD<T> : ICRUD<T>
    {
        public abstract string SQL_UPDATE { get; }
        public abstract string SQL_INSERT { get; }
        public abstract string SQL_SELECT { get; }
        public abstract string SQL_DROP { get; }
        public abstract string SQL_COLUMNS { get; }

        private OracleConnection _Connection;
        public OracleConnection Connection { get => _Connection; set => _Connection = value; }

        //protected static string connetionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=dbsys.cs.vsb.cz)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=oracle.dbsys.cs.vsb.cz)));User id=pus0065;Password=iQpR3iw8r9;";

        public virtual async Task<T> SelectId(int p_ID)
        {
            var start = DateTime.Now;
            T output = default(T);
            var command = new OracleCommand("", Connection);
            command.CommandText = SQL_SELECT;
            command.Parameters.Add(":ID", p_ID);
            try
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (!reader.HasRows)
                        throw new Exception(DateTime.Now + ": Neexistuje zaznam s id '" + p_ID + "'");
                    output = GetDTOList(reader)[0];
#if DEBUG
                    //Console.WriteLine(DateTime.Now + ": SelectId trval '" +
                    //    (DateTime.Now - start) + "', nad tabulkou '" + typeof(T).Name + "', s ID: '" + p_ID + "'.");
#endif
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + ": SelectId nad tabulkou '" + typeof(T).Name + "' trval '" +
                    (DateTime.Now - start) + "' a vratil chybu: '" + ex.Message + "'.");
            }
            return output;
        }

        public async Task<bool> Update(T p_Object)
        {
            var start = DateTime.Now;
            var command = new OracleCommand(SQL_UPDATE, Connection);
            AddParameters(command, p_Object);
            try
            {
                await command.ExecuteNonQueryAsync();
#if DEBUG
                Console.WriteLine(DateTime.Now + ": Update trval '" +
                    (DateTime.Now - start) + "', nad tabulkou '" + typeof(T).Name + "'.");
#endif
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + ": Update nad tabulkou '" + typeof(T).Name + "' trval '" +
                    (DateTime.Now - start) + "' a vratil chybu: '" + ex.Message + "'.");
                return false;
            }
        }

        public async Task<int> Insert(T p_Object)
        {
            int output=-1;
            var start = DateTime.Now;
            var command = new OracleCommand(SQL_INSERT, Connection);
            AddParameters(command, p_Object, false);
            try
            {
                await command.ExecuteNonQueryAsync();
                command.CommandText = "select skupina_seq.CURRVAL as value from dual";
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                        output = Convert.ToInt32(reader["value"]);
                }
#if DEBUG
                Console.WriteLine(DateTime.Now + ": Insert trval '" +
                    (DateTime.Now - start) + "', nad tabulkou '" + typeof(T).Name + "'.");
#endif
                return output;
            }
            catch (Exception ex)
            {
                var chyba = command.CommandText;

                for (int i = 0; i < command.Parameters.Count; i++)
                {
                    chyba = chyba.Replace(command.Parameters[i].ParameterName, "'"+command.Parameters[i].Value.ToString()+"'");
                }

                Console.WriteLine(DateTime.Now + ": Insert nad tabulkou '" + typeof(T).Name + "' trval '" +
                    (DateTime.Now - start) + "' a vratil chybu: '" + ex.Message + "'.");
                Console.WriteLine(chyba);
                return output;
            }
        }

        public virtual async Task<bool> DropId(int p_ID)
        {
            DateTime start = DateTime.Now;
            try
            {
                OracleCommand command = new OracleCommand(SQL_DROP, Connection);
                command.Parameters.Add(":ID", p_ID);
                await command.ExecuteNonQueryAsync();
#if DEBUG
                Console.WriteLine(DateTime.Now + ": DropId trval '" +
                    (DateTime.Now - start) + "', nad tabulkou '" + typeof(T).Name + "', s ID: '" + p_ID + "'.");
#endif
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + ": DropId nad tabulkou '" + typeof(T).Name + "' trval '" +
                    (DateTime.Now - start) + "' a vratil chybu: '" + ex.Message + "'.");
                return false;
            }
        }

        public virtual async Task<List<T>> SelectAll()
        {
            DateTime start = DateTime.Now;
            var command = new OracleCommand("", Connection);
            command.CommandText = SQL_SELECT;
            if (command.CommandText.Contains("WHERE"))
                command.CommandText = command.CommandText.Remove(command.CommandText.IndexOf("WHERE"));
            List<T> output = null;
            try
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    output = GetDTOList(reader);
#if DEBUG
                    Console.WriteLine(DateTime.Now + ": SelectAll trval '" +
                        (DateTime.Now - start) + "' a vratil '" + output.Count + "' zaznamu, nad tabulkou '" + typeof(T).Name + "'.");
#endif
                }
            }
            catch (Exception ex)
            {
                output = new List<T>();
                Console.WriteLine(DateTime.Now + ": SelectAll nad tabulkou '" + typeof(T).Name + "' trval '" +
                    (DateTime.Now - start) + "' a vratil chybu: '" + ex.Message + "'.");
            }
            return output;
        }

        public abstract List<T> GetDTOList(DbDataReader p_Reader);
        public abstract T GetDTO(DbDataReader p_Reader);
        protected abstract void AddParameters(OracleCommand p_Command, T p_Object, bool p_UseID = true);
    }
}
