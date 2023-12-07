using System.Data;

namespace DataAccess.Interface
{
    public interface IProduct_DAL
    {
        DataTable FindByWord(string word);
        DataTable FindImgNamePriceProducts(int PageSize, int PageNumber);

    }
}
