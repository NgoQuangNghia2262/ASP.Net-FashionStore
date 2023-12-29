using System.Data;
using Model.Interface;

namespace Model
{
    public class Bill : IKeyBill
    {
        private int _id;
        private DateTime _date = DateTime.MinValue;
        private string? _status;
        private double? _discount = 0;
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
            if (row == null)
            {
                return;
            }
            _id = row.Table.Columns.Contains("id") ? Convert.ToInt32(row["id"]) : 0;
            if (row.Table.Columns.Contains("date"))
            {
                if (!string.IsNullOrEmpty(row["date"].ToString()))
                {
                    _date = Convert.ToDateTime(row["date"]);
                }
            }
            _status = row.Table.Columns.Contains("status") ? row["status"].ToString() : string.Empty;
            _discount = row.Table.Columns.Contains("discount") && !string.IsNullOrEmpty(row["discount"].ToString()) ? Convert.ToDouble(row["discount"]) : 0.0;
            if (row.Table.Columns.Contains("customer"))
            {
                _customer = new Customer(row["customer"].ToString() ?? "");
            }
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
                return _id;
            }
            set => _id = value;
        }
        public DateTime date { get => _date; set => _date = value; }
        public string? status { get => _status; set => _status = value; }
        public double? discount
        {
            get
            {
                if (_discount == null)
                {
                    return 0;
                }
                return _discount;
            }
            set => _discount = value;
        }
        public Customer? customer { get => _customer; set => _customer = value; }
    }
}