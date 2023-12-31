using System.Data;
using Model;

namespace DataAccess.Interface
{
    public interface IBill_DAL
    {
        Task<DataTable> GetBillingDetails(int idbill);
    }
}
