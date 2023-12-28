using System.Data;
using System.Reflection;

namespace Bussiness.Middleware
{
    internal class Convert<T>
    {
        public static T DataRowToModel(DataRow row)
        {
            T instance = (T)Activator.CreateInstance(typeof(T), row);
            return instance;
        }
        public static T[] DatatableToModel(DataTable dataTable)
        {
            T[] result = new T[dataTable.Rows.Count];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = DataRowToModel(dataTable.Rows[i]);
            }
            return result;
        }
    }
}
