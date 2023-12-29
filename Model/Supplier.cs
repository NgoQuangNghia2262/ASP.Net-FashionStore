using System.Data;
using Model.Interface;

namespace Model
{
    public class Supplier : IKeySupplier
    {
        private int _id;
        private string? _name;
        private string? _phone;
        private string? _gmail;
        public Supplier()
        {
            _id = -1;
        }
        public Supplier(int id)
        {
            _id = id;
        }
        public Supplier(DataRow row)
        {
            if (row == null)
            {
                return;
            }

            _id = row.Table.Columns.Contains("id") ? Convert.ToInt32(row["id"]) : 0;
            name = row.Table.Columns.Contains("name") ? row["name"]?.ToString() : string.Empty;
            phone = row.Table.Columns.Contains("phone") ? row["phone"]?.ToString() : string.Empty;
            gmail = row.Table.Columns.Contains("gmail") ? row["gmail"]?.ToString() : string.Empty;
        }

        public Supplier(int id, string name, string phone, string gmail)
        {
            this._id = id;
            this._name = name;
            this._phone = phone;
            this._gmail = gmail;
        }
        public int id { get => _id; set => _id = value; }
        public string? name { get => _name; set => _name = value; }
        public string? phone { get => _phone; set => _phone = value; }
        public string? gmail { get => _gmail; set => _gmail = value; }
    }

}