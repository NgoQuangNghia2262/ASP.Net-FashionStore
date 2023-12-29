using System.Data;
using Model.Interface;

namespace Model
{
    public class BillingDetail : IKeyBillingDetail
    {
        private int _id;
        private Product? _product;
        private Bill? _bill;
        private int? _quantity;
        private double? _price;

        public BillingDetail()
        {

        }
        public BillingDetail(int id)
        {
            this._id = id;
        }
        public BillingDetail(int id, Product product, Bill bill, int quantity)
        {
            this._id = id;
            this._product = product;
            this._bill = bill;
            this._quantity = quantity;
        }
        public BillingDetail(DataRow row)
        {
            if (row == null)
            {
                return;
            }

            _id = row.Table.Columns.Contains("id") && !string.IsNullOrEmpty(row["id"].ToString()) ? Convert.ToInt32(row["id"]) : 0;
            _quantity = row.Table.Columns.Contains("quantity") && !string.IsNullOrEmpty(row["quantity"].ToString()) ? Convert.ToInt32(row["quantity"]) : 0;

            if (row.Table.Columns.Contains("nameProduct") &&
                row.Table.Columns.Contains("sizeProduct") &&
                row.Table.Columns.Contains("colorProduct"))
            {
                _product = new Product(row["nameProduct"].ToString(), row["sizeProduct"].ToString(), row["colorProduct"].ToString());
            }

            if (row.Table.Columns.Contains("idBill"))
            {
                _bill = new Bill(Convert.ToInt32(row["idBill"]));
            }
            _price = row.Table.Columns.Contains("price") && !string.IsNullOrEmpty(row["price"].ToString()) ? Convert.ToInt32(row["price"]) : 0;

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
        public Product? product { get => _product; set => _product = value; }
        public Bill? bill { get => _bill; set => _bill = value; }
        public int? quantity { get => _quantity; set => _quantity = value; }
        public double? price { get => _price; set => _price = value; }
    }

}