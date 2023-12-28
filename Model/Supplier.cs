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
        private Account? _account;
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
            _id = Convert.ToInt32(row["id"]);
            name = row["name"].ToString();
            phone = row["phone"].ToString();
            gmail = row["gmail"].ToString();
        }
        public Supplier(int id, string name, string phone, string gmail, string address)
        {
            this._id = id;
            this._name = name;
            this._phone = phone;
            this._gmail = gmail;
        }
        public int id { get => _id; set => _id = value; }
        public string? name { get => _name; set => _name = value; }
        public string? phone { get => _phone; set => _phone = value; }
        public Account? account { get => _account; set => _account = value; }
        public string? gmail { get => _gmail; set => _gmail = value; }
    }

}