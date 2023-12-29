using System.Data;
using Model.Interface;

namespace Model
{
    public class Customer : IKeyCustomer
    {
        private string _id;
        private string? _phone;
        private string? _name;
        private string? _gmail;
        private string? _address;


#pragma warning disable CS8618 
        public Customer() { }
        public Customer(string id)
        {
            this._id = id;
        }
#pragma warning restore CS8618 

        public Customer(string id, string? phone, string? name, string? gmail, string? address)
        {
            this._id = id;
            this._phone = phone;
            this._name = name;
            this._gmail = gmail;
            this._address = address;
        }
        public Customer(DataRow row)
        {
            if (row == null)
            {
                return;
            }

            _id = row.Table.Columns.Contains("id") ? row["id"].ToString() : string.Empty;
            _phone = row.Table.Columns.Contains("phone") ? row["phone"]?.ToString() : null;
            _name = row.Table.Columns.Contains("name") ? row["name"]?.ToString() : null;
            _gmail = row.Table.Columns.Contains("gmail") ? row["gmail"]?.ToString() : null;
            _gmail = row.Table.Columns.Contains("address") ? row["address"]?.ToString() : null;
        }
        public string id { get => _id; set => _id = value; }
        public string? phone { get => _phone; set => _phone = value; }
        public string? name { get => _name; set => _name = value; }
        public string? gmail { get => _gmail; set => _gmail = value; }
        public string? address { get => _address; set => _address = value; }
    }
}
