using System.Data;
using Model.Interface;

namespace Model
{
    public class BillingDetails : IKeyBillingDetails
    {
        private int _id;
        private Product? _product;
        private Bill? _bill;
        private int? _quantity;

        public BillingDetails()
        {

        }
        public BillingDetails(int id)
        {
            this._id = id;
        }
        public BillingDetails(int id, Product product, Bill bill, int quantity)
        {
            this._id = id;
            this._product = product;
            this._bill = bill;
            this._quantity = quantity;
        }
        public BillingDetails(DataRow row)
        {
            _id = Convert.ToInt32(row["id"]);
            _quantity = Convert.ToInt32(row["quantity"]);
            _product = new Product(row["nameProduct"].ToString(), row["sizeProduct"].ToString(), row["colorProduct"].ToString());
            _bill = new Bill(Convert.ToInt32(row["idbill"]));
        }
        public int id { get => _id; }
        public Product? product { get => _product; set => _product = value; }
        public Bill? bill { get => _bill; set => _bill = value; }
        public int? quantity { get => _quantity; set => _quantity = value; }
    }

}