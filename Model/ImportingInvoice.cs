using System.Data;
using Model.Interface;

namespace Model
{
    public class ImportingInvoice : IKeyImportingInvoice
    {
        private int _id;
        private string? _note;
        private DateTime? _date;
        private Supplier? _supplier;
        private double? _discount;
        private string? _status;

        public ImportingInvoice(int id, string note, DateTime date, Supplier supplier, double discount, string status)
        {
            this._id = id;
            this._note = note;
            this._date = date;
            this._supplier = supplier;
            this._discount = discount;
            this._status = status;
        }

        public ImportingInvoice(DataRow row)
        {
            _id = Convert.ToInt32(row["id"]);
            _note = row["note"].ToString();
            _date = Convert.ToDateTime(row["date"]);
            _supplier = new Supplier(Convert.ToInt32(row["supplier"]));
            _discount = Convert.ToDouble(row["discount"]);
            _status = row["status"].ToString();
        }

        public int id { get => _id; set => _id = value; }
        public string? note { get => _note; set => _note = value; }
        public DateTime? date { get => _date; set => _date = value; }
        public Supplier? supplier { get => _supplier; set => _supplier = value; }
        public double? discount { get => _discount; set => _discount = value; }
        public string? status { get => _status; set => _status = value; }
    }


}