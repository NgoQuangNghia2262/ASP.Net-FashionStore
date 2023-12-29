using System.Data;
using Model;

namespace DataAccess.Interface
{
    public interface ICustomer_DAL
    {
        Task<DataTable> FindOneByAccount(Account account);
    }
}
