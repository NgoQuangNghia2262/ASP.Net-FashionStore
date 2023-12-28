using Model.Interface;

namespace Model
{
    public class Customer : IKeyCustomer
    {
        private string _id;
        private string _phone;
        private string _name;
        private string _gmail;
        private Account _account;
#pragma warning disable CS8618 
        public Customer() { }
        public Customer(string id)
        {
            this._id = id;
        }
#pragma warning restore CS8618 

        public Customer(string id, string phone, string name, string gmail, Account account)
        {
            this._id = id;
            this._phone = phone;
            this._name = name;
            this._gmail = gmail;
            this._account = account;
        }

        public string id { get => _id; }
        public string phone { get => _phone; set => _phone = value; }
        public string name { get => _name; set => _name = value; }
        public string gmail { get => _gmail; set => _gmail = value; }
        public Account account { get => _account; set => _account = value; }
    }
}
