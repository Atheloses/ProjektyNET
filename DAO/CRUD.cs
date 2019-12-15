using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
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


        public virtual async Task<T> SelectId(int p_ID)
        {
            T output = default(T);
            using var command = new OracleCommand(SQL_SELECT, Connection);
            command.Parameters.Add(":ID", p_ID);

            SaveCommand(command);
            using (var reader = await command.ExecuteReaderAsync())
            {
                if (!reader.HasRows)
                    throw new Exception(DateTime.Now + ": Neexistuje zaznam s id '" + p_ID + "'");
                output = GetDTOList(reader)[0];
            }
            await new OracleCommand("commit", Connection).ExecuteNonQueryAsync();
            return output;
        }

        public async Task<bool> Update(T p_Object)
        {
            using var command = new OracleCommand(SQL_UPDATE, Connection);
            AddParameters(command, p_Object);

            SaveCommand(command);
            await command.ExecuteNonQueryAsync();
            await new OracleCommand("commit", Connection).ExecuteNonQueryAsync();
            return true;
        }

        public async Task<int> Insert(T p_Object)
        {
            int output=-1;
            using var command = new OracleCommand(SQL_INSERT, Connection);
            AddParameters(command, p_Object, false);

            SaveCommand(command);
            await command.ExecuteNonQueryAsync();
            await new OracleCommand("commit", Connection).ExecuteNonQueryAsync();

            using var command2 = new OracleCommand("select skupina_seq.CURRVAL as value from dual", Connection);
            SaveCommand(command2);
            using (var reader = await command2.ExecuteReaderAsync())
            {
                if (reader.Read())
                    output = Convert.ToInt32(reader["value"]);
            }
            await new OracleCommand("commit", Connection).ExecuteNonQueryAsync();

            return output;
        }

        public virtual async Task<bool> DropId(int p_ID)
        {
            using var command = new OracleCommand(SQL_DROP, Connection);
            command.Parameters.Add(":ID", p_ID);

            SaveCommand(command);
            await command.ExecuteNonQueryAsync();
            await new OracleCommand("commit", Connection).ExecuteNonQueryAsync();
            return true;
        }

        public virtual async Task<List<T>> SelectAll()
        {
            using var command = new OracleCommand(SQL_SELECT, Connection);
            if (command.CommandText.Contains("WHERE"))
                command.CommandText = command.CommandText.Remove(command.CommandText.IndexOf("WHERE"));

            SaveCommand(command);
            using var reader = await command.ExecuteReaderAsync();
            await new OracleCommand("commit", Connection).ExecuteNonQueryAsync();
            return GetDTOList(reader);
        }

        private void SaveCommand(OracleCommand p_Command)
        {
            var output = p_Command.CommandText;

            if (p_Command.Parameters != null && p_Command.Parameters.Count > 0)
                for (int i = 0; i < p_Command.Parameters.Count; i++)
                {
                    if(p_Command.Parameters[i].Value == null)
                        output = output.Replace(p_Command.Parameters[i].ParameterName, "''");
                    else
                    output = output.Replace(p_Command.Parameters[i].ParameterName, "'" + p_Command.Parameters[i].Value.ToString() + "'");
                }

            Trace.WriteLine(output);
        }

        public abstract List<T> GetDTOList(DbDataReader p_Reader);
        public abstract T GetDTO(DbDataReader p_Reader);
        protected abstract void AddParameters(OracleCommand p_Command, T p_Object, bool p_UseID = true);
    }
}
