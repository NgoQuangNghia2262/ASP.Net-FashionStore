using System.Data;
using Model;

namespace DataAccess.Interface
{
    public interface IBill_DAL
    {
        void Purchase(BillingDetail detail, string khachhang);
        Task<DataTable> GetBillingDetails(int idbill);
    }
}
