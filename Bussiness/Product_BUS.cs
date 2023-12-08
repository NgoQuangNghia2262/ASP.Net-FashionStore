using System.Data;
using Bussiness.Exceptions;
using DataAccess;
using DataAccess.Interface;
using Model;
using Model.Interface;

namespace Bussiness
{
    public class Product_BUS : Interface.IValidate, Interface.IProduct_BUS
    {
        private IProduct_DAL dal;
        private ICRUD ICrud;
        public Product_BUS()
        {
            dal = new Product_DAL();
            ICrud = new Product_DAL();
        }
        public Product_BUS(ICRUD ICrud, IProduct_DAL dal)
        {
            this.ICrud = ICrud;
            this.dal = dal;
        }
        public bool ValidateKeyModel(IKey obj)
        {
            IKeyProduct? product = obj as IKeyProduct;
            if (product == null) { throw new ArgumentNullException("Tham số null"); }
            bool isName = product.name.Length > 6;
            bool isColor = product.color.Length >= 1;
            bool isSize = product.size.Length >= 1;
            if (!isName || !isColor || !isSize)
            {
                throw new LengthPropertyException("Sản phẩm không hợp lệ");
            }
            return true;
        }
        public bool ValidateModelData(object obj)
        {
            Product? product = obj as Product;
            if (product == null) { throw new ArgumentNullException("Tham số null"); }
            bool isName = product.name.Length > 6;
            bool isColor = product.color.Length > 1;
            bool isSize = product.size.Length > 1;
            bool isPrice = product.price >= 0;
            if (!isName || !isPrice || !isColor || !isSize)
            {
                throw new LengthPropertyException("Sản phẩm không hợp lệ");
            }
            return true;
        }
        public async Task<ResponseResult<Product[]>> FindProductByWords(int PageSize, int PageNumber)
        {
            ResponseResult<Product[]> res = new ResponseResult<Product[]>();
            DataTable result = new DataTable();
            result = await dal.FindByWord(PageSize, PageNumber);
            if (result.Rows.Count == 0) { throw new DataNotFoundException("No results found."); }
            res.Data = Middleware.Convert<Product>.DatatableToModel(result);
            res.TotalRows = int.Parse(result.Rows[0]["TotalRows"]?.ToString() ?? "");
            return res;
        }
        public async Task<bool> ExistsModel(IKey obj)
        {
            DataTable dt = await ICrud.FindOne(obj);
            return dt.Rows.Count > 0;
        }
        public async Task<ResponseResult<Product[]>> FindImgNamePriceProducts(int PageSize, int PageNumber)
        {
            ResponseResult<Product[]> res = new ResponseResult<Product[]>();
            DataTable result = new DataTable();
            result = await dal.FindImgNamePriceProducts(PageSize, PageNumber);
            if (result.Rows.Count == 0) { throw new DataNotFoundException("No results found."); }
            int TotalRows = int.Parse(result.Rows[0]["TotalRows"]?.ToString() ?? "");
            res.Data = Middleware.Convert<Product>.DatatableToModel(result);
            res.TotalRows = TotalRows;
            return res;
        }
    }
}
