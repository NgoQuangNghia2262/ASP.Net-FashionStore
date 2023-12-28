using System.Data;
using Model.Interface;

namespace Model
{
    public class Bill : IKeyBill
    {
        private int _id;
        private DateTime _date;
        private string? _status;
        private double? _discount;
        private Customer? _customer;
        public Bill()
        {

        }
        public Bill(int id)
        {
            this._id = id;
        }

        public Bill(int id, DateTime date, string status, double discount, string customer)
        {
            this._id = id;
            this.date = date;
            this.status = status;
            this.discount = discount;
            this.customer = new Customer(customer);
        }
        public Bill(DataRow row)
        {
            _id = Convert.ToInt32(row["id"]);
            _date = Convert.ToDateTime(row["date"]);
            _status = row["status"].ToString();
            _discount = Convert.ToDouble(row["discount"]);
            _customer = new Customer(row["customer"].ToString() ?? "");
        }
        public int id
        {
            get
            {
                if (_id <= 0)
                {
                    Random random = new Random();
                    return random.Next(1, 999999999);
                }
                return 0;
            }
            set => _id = value;
        }
        public DateTime date { get => _date; set => _date = value; }
        public string? status { get => _status; set => _status = value; }
        public double? discount { get => _discount; set => _discount = value; }
        public Customer? customer { get => _customer; set => _customer = value; }
    }
}