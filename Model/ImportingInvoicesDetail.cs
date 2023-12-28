using System.Data;

namespace Model
{
    public class ImportingInvoicesDetail
    {
        private int _id;
        private Product? _product;
        private Bill? _bill;
        private int? _quantity;

        public ImportingInvoicesDetail()
        {

        }
        public ImportingInvoicesDetail(int id)
        {
            this._id = id;
        }
        public ImportingInvoicesDetail(int id, Product product, Bill bill, int quantity)
        {
            this._id = id;
            this._product = product;
            this._bill = bill;
            this._quantity = quantity;
        }
        public ImportingInvoicesDetail(DataRow row)
        {
            _id = Convert.ToInt32(row["id"]);
            _quantity = Convert.ToInt32(row["quantity"]);
#pragma warning disable CS8604 // Possible null reference argument.
            _product = new Product(row["nameProduct"].ToString(), row["sizeProduct"].ToString(), row["colorProduct"].ToString());
#pragma warning restore CS8604 // Possible null reference argument.
            _bill = new Bill(Convert.ToInt32(row["idbill"]));
        }
        public int id { get => _id; set => _id = value; }
        public Product? product { get => _product; set => _product = value; }
        public Bill? bill { get => _bill; set => _bill = value; }
        public int? quantity { get => _quantity; set => _quantity = value; }
    }
}