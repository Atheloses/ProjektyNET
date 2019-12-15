using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public static class Extensions
    {
        public static T GetValue<T>(this object value)
        {
            return value == DBNull.Value ? default(T) : (T)value;
        }
    }
}
