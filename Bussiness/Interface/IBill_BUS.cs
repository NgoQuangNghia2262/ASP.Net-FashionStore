using Model;
namespace Bussiness.Interface
{
    public interface IBill_BUS
    {
        void Purchase(BillingDetail detail, string khachhang);
        Task<BillingDetail[]> GetBillingDetails(int idbill);
    }
}
