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
            if (row == null)
            {
                return;
            }

            _id = row.Table.Columns.Contains("id") ? Convert.ToInt32(row["id"]) : 0;
            _quantity = row.Table.Columns.Contains("quantity") ? Convert.ToInt32(row["quantity"]) : 0;

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
        }
        public int id { get => _id; set => _id = value; }
        public Product? product { get => _product; set => _product = value; }
        public Bill? bill { get => _bill; set => _bill = value; }
        public int? quantity { get => _quantity; set => _quantity = value; }
    }
}