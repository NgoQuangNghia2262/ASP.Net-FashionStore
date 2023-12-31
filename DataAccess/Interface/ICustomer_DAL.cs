using System.Data;
using Model;

namespace DataAccess.Interface
{
    public interface ICustomer_DAL
    {
        Task<DataTable> FindOneByAccount(Account account);
        Task PlacingAnOrder(string idCustomer, string note);
        Task<DataTable> GetCartForCustomer(string idCustomer);
        Task RemoveProductsFromCart(BillingDetail detail, string idCustomer);
        Task Purchase(BillingDetail detail, string khachhang);


    }
}
