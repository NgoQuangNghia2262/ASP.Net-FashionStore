using System.Data;
using DataAccess.Interface;
using Model;
using Model.Interface;


namespace DataAccess
{
    public class Product_DAL : ICRUD, IProduct_DAL
    {
        public void Delete(IKey obj)
        {
            IKeyProduct? keyProduct = obj as IKeyProduct;
            string query = $"DELETE FROM product " +
            $"WHERE name = N'{keyProduct?.name}' AND color = N'{keyProduct?.color}' AND size = N'{keyProduct?.size}'";
            _ = DataProvider.Instance.ExecuteNonQueryAsync(query);
        }
        public Task<DataTable> FindAll(int PageSize, int PageNumber)
        {
            string query = $"Exec GetPagedProducts @PageSize = {PageSize} , @PageNumber = {PageNumber}";
            return DataProvider.Instance.ExecuteQueryAsync(query);
        }
        public Task<DataTable> FindByWord(int PageSize, int PageNumber)
        {
            string query = $"";
            return DataProvider.Instance.ExecuteQueryAsync(query);
        }

        public Task<DataTable> FindCategorys()
        {
            string query = $"select category from Product group by category";
            return DataProvider.Instance.ExecuteQueryAsync(query);
        }

        public Task<DataTable> FindImgNamePriceProducts(int PageSize, int PageNumber)
        {
            string query = $"Exec [GetPagedProductsGroupByImgNamePrice] @PageSize = {PageSize} , @PageNumber = {PageNumber}";
            return DataProvider.Instance.ExecuteQueryAsync(query);
        }

        public Task<DataTable> FindOne(IKey name)
        {
            IKeyProduct? keyProduct = name as IKeyProduct;
            string query = $"SELECT * FROM product " +
                   $"WHERE name = N'{keyProduct?.name}' AND color = N'{keyProduct?.color}' AND size = N'{keyProduct?.size}'";

            return DataProvider.Instance.ExecuteQueryAsync(query);
        }
        public void Save(dynamic obj)
        {
            Product? product = obj as Product;
            if (product == null) { throw new InvalidCastException("dynamic obj không phải là 1 Product"); }
            string query = $@"EXEC UpsertProduct 
            @img = '{product.img}',
            @name = N'{product.name}',
            @category = N'{product.category}',
            @color = N'{product.color}',
            @size = '{product.size}',
            @price = {product.price},
            @discount = {product.discount},
            @describe = N'{product.describe}',
            @inventory = {product.inventory}";
            _ = DataProvider.Instance.ExecuteNonQueryAsync(query);
        }
    }
}
