using System.Data;
using Model;

namespace DataAccess.Interface
{
    public interface IAccount_DAL
    {
        Task<DataTable> GetCartForCustomer(string idCustomer);
    }
}
