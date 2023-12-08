using DataAccess.Interface;
using System.Data;
using Model.Interface;
using Model;

namespace DataAccess
{
    public class Account_DAL : ICRUD, IAccount_DAL
    {
        public void Delete(IKey obj)
        {
            IKeyAccount? account = obj as IKeyAccount;
            if (account == null) { throw new InvalidCastException("dynamic obj không phải là 1 account"); }
            _ = DataProvider.Instance.ExecuteNonQueryAsync($"delete Account where Username = '{account?.username}'");
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

        public void Login(Account account)
        {

            return;
        }

        public void Save(dynamic obj)
        {
            Account? account = obj as Account;
            if (account == null) { throw new InvalidCastException("dynamic obj không phải là 1 account"); }
            _ = DataProvider.Instance.ExecuteNonQueryAsync($"UpsertAccount @username='{account.username}' , @password='{account.password}' , @permissions='{account.permissions}'");
        }
    }
}
