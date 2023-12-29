using DataAccess.Interface;
using System.Data;
using Model.Interface;
using Model;

namespace DataAccess
{
    public class Account_DAL : ICRUD, IAccount_DAL
    {
        public async Task Delete(IKey obj)
        {
            IKeyAccount? account = obj as IKeyAccount;
            if (account == null) { throw new InvalidCastException("dynamic obj không phải là 1 account"); }
            await DataProvider.Instance.ExecuteNonQueryAsync($"delete Account where Username = '{account?.username}'");
        }

        public Task<DataTable> FindAll(int PageSize, int PageNumber)
        {
            return DataProvider.Instance.ExecuteQueryAsync($"Exec [GetPagedAccounts] @PageSize = {PageSize} , @PageNumber = {PageNumber}");
        }
        public Task<DataTable> FindOne(IKey obj)
        {
            IKeyAccount? account = obj as IKeyAccount;
            if (account == null) { throw new InvalidCastException("dynamic obj không phải là 1 account"); }
            return DataProvider.Instance.ExecuteQueryAsync($"select * from Account where Username = '{account?.username}'");
        }

        public Task<DataTable> GetCartForCustomer(string idCustomer)
        {
            string query = $"select BillingDetails.* , Product.* from BillingDetails " +
            "inner join Bill on BillingDetails.idBill = Bill.id " +
            "inner join Product on Product.name = BillingDetails.nameProduct and Product.color = BillingDetails.colorProduct and Product.size = BillingDetails.sizeProduct " +
            $"where customer = '{idCustomer}' and status = N'UnPaid'";
            return DataProvider.Instance.ExecuteQueryAsync(query);
        }

        public async Task Save(dynamic obj)
        {
            Account? account = obj as Account;
            if (account == null) { throw new InvalidCastException("dynamic obj không phải là 1 account"); }
            string proc = $"UpsertAccount @username='{account.username}' , @password='{account.password}' , @permissions='{account.permissions}' , @customer ='{account.customer.id}'";
            await DataProvider.Instance.ExecuteNonQueryAsync(proc);
        }
    }
}
