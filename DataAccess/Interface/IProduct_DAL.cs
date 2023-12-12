using System.Data;

namespace DataAccess.Interface
{
    public interface IProduct_DAL
    {
        Task<DataTable> FindByWord(int PageSize, int PageNumber);
        Task<DataTable> FindImgNamePriceProducts(int PageSize, int PageNumber);
        Task<DataTable> FindCategorys();

    }
}
