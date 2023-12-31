using Microsoft.AspNetCore.Http;
using Model;
namespace Bussiness.Interface
{
    public interface ICustomer_BUS
    {
        Task<Customer> FindOneByAccount(Account account);
        void PlacingAnOrder(HttpContext context, string note);
        Task<BillingDetail[]> GetCartForCustomer(HttpContext context);
        void RemoveProductsFromCart(HttpContext context, BillingDetail detail);
        void Purchase(HttpContext context, BillingDetail detail);


    }
}
