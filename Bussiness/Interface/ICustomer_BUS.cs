using Model;
namespace Bussiness.Interface
{
    public interface ICustomer_BUS
    {
        Task<Customer> FindOneByAccount(Account account);
    }
}
