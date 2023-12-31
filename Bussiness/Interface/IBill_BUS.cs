using Model;
namespace Bussiness.Interface
{
    public interface IBill_BUS
    {
        Task<BillingDetail[]> GetBillingDetails(int idbill);
    }
}
