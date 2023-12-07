using Model;
namespace Bussiness.Interface
{
    public interface IProduct_BUS
    {
        Product[] FindProductByWords(string word);
        Product[] FindImgNamePriceProducts(int PageSize, int PageNumber, out int TotalRows);

    }
}
