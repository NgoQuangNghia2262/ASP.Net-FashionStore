using Model;
namespace Bussiness.Interface
{
    public interface IProduct_BUS
    {
        Task<ResponseResult<Product[]>> FindProductByWords(int PageSize, int PageNumber);
        Task<ResponseResult<Product[]>> FindImgNamePriceProducts(int PageSize, int PageNumber);
        Task<Product[]> FindProductByName(string name);
    }
}
