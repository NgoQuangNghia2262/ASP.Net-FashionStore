using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Middleware
{
    internal class Convert<T>
    {
        public static T DataRowToModel(DataRow row)
        {
            T result = Activator.CreateInstance<T>(); // Thực hiện tạo instance của Model T bằng phương thức khởi tạo không tham số vậy nên nếu không có sẽ gây ra lỗi [Lưu ý (1)]
            foreach (DataColumn column in row.Table.Columns) // Lấy ra các trường cửa Datarow (Datarow là 1 hàng dữu liệu trong DB)
            {
                // Lấy ra property của đối tượng T dựa vào các columName (Tên của các trường trong DB)
                PropertyInfo prop = result.GetType().GetProperty(column.ColumnName); 
                object value = row[column.ColumnName];
                // Lấy được trường dữ liệu thì ta set giá trị cho trường đó của T dựa vào dữ liệu từ Datarow
                prop.SetValue(result, value, null);
                // Phương này cần lấy property dựa vào các trường trong DB vậy nên khi thiết kế DB ta cần tạo property trong Model khớp với DB [Lưu ý (2)]
            }
            return result;
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
