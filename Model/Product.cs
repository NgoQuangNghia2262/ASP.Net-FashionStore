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
        public double? price { get => _price; set => _price = value; }
        public string name { get => _name; set => _name = value; }
        public string size { get => _size; set => _size = value; }
        public string color { get => _color; set => _color = value; }
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
