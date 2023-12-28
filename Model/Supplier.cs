using System.Data;
using Model.Interface;

namespace Model
{
    public class Supplier : IKeySupplier
    {
        private int _id;
        private string _name;
        private string _phone;
        private string _gmail;
        private string _address;
#pragma warning disable CS8618 
        public Supplier()
        {

        }
        public Supplier(int id)
        {
            _id = id;
        }
#pragma warning restore CS8618 
        public Supplier(DataRow row)
        {
            _id = Convert.ToInt32(row["id"]);
#pragma warning disable CS8601 // Possible null reference assignment.
            _name = row["name"].ToString();
            _phone = row["phone"].ToString();
            _gmail = row["gmail"].ToString();
            _address = row["address"].ToString();
#pragma warning restore CS8601 // Possible null reference assignment.
        }
        public Supplier(int id, string name, string phone, string gmail, string address)
        {
            this._id = id;
            this._name = name;
            this._phone = phone;
            this._gmail = gmail;
            this._address = address;
        }
        public int id { get => _id; }
        public string name { get => _name; set => _name = value; }
        public string phone { get => _phone; set => _phone = value; }
        public string gmail { get => _gmail; set => _gmail = value; }
        public string address { get => _address; set => _address = value; }
    }

}