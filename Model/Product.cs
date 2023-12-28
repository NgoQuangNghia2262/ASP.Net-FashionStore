using System.Data;
using Model.Interface;


namespace Model
{
    public class Product : IKeyProduct
    {

        private string _name;
        private string _size;
        private string _color;
        private double? _price;
        private string? _img;
        private double? _discount;
        private string? _describe;
        private double? _inventory;
        private string? _category;

        public string name { get => _name; set => _name = value; }
        public string size { get => _size; set => _size = value; }
        public string color { get => _color; set => _color = value; }
        public double? price { get => _price; set => _price = value; }
        public string? img { get => _img; set => _img = value; }
        public string? category { get => _category; set => _category = value; }
        public double? discount
        {
            get
            {
                if (_discount == null) { return 0; }
                return _discount;
            }
            set => _discount = value;
        }
        public string? describe { get => _describe; set => _describe = value; }
        public double? inventory
        {
            get
            {
                if (_inventory == null) { return 0; }
                return _inventory;
            }
            set => _inventory = value;
        }

        public Product()
        {
            _name = string.Empty;
            _size = string.Empty;
            _color = string.Empty;
        }
        public Product(string name, string size, string color)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(size) || string.IsNullOrEmpty(color))
            {
                throw new ArgumentNullException("Khóa chính không được null");
            }
            _name = name;
            _size = size;
            _color = color;
        }
        public Product(DataRow row)
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            _name = row["name"].ToString();
            _size = row["size"].ToString();
            _color = row["color"].ToString();
#pragma warning restore CS8601 // Possible  reference assignment.

            _price = row["price"] is DBNull ? null : (double?)Convert.ToDouble(row["price"]);
            _img = row["img"] is DBNull ? null : row["img"].ToString();
            _discount = row["discount"] is DBNull ? null : (double?)Convert.ToDouble(row["discount"]);
            _describe = row["describe"] is DBNull ? null : row["describe"].ToString();
            _inventory = row["inventory"] is DBNull ? null : (double?)Convert.ToDouble(row["inventory"]);
            _category = row["category"] is DBNull ? null : row["category"].ToString();
        }
        public Product(double price, string name, string size, string color, string img, double discount, string describe, double inventory, string category)
        {
            this.price = price;
            this._name = name;
            this._size = size;
            this._color = color;
            this.img = img;
            this.discount = discount;
            this.describe = describe;
            this.inventory = inventory;
            this.category = category;
        }
    }
}
