using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Interface;
using Model;

namespace DataAccess
{
    public class Account_DAL : ICRUD , IAccount_DAL
    {
        public void Delete(IKey obj)
        {
            IKeyAccount? account = obj as IKeyAccount;
            if (account == null) { throw new InvalidCastException("dynamic obj không phải là 1 account"); }
            DataProvider.Instance.ExecuteNonQuery($"delete Account where Username = '{account?.username}'");
        }

        public DataTable FindAll()
        {
            return DataProvider.Instance.ExecuteQuery("select * from Account");
        }
        public DataTable FindOne(IKey obj)
        {
            IKeyAccount? account = obj as IKeyAccount;
            if (account == null) { throw new InvalidCastException("dynamic obj không phải là 1 account"); }
            return DataProvider.Instance.ExecuteQuery($"select * from Account where Username = '{account?.username}'");
        }

        public void Login(Account account)
        {
            return;
        }

        public void Save(dynamic obj)
        {
            Account? account = obj as Account;
            if ( account == null ) { throw new InvalidCastException("dynamic obj không phải là 1 account"); }
            DataProvider.Instance.ExecuteNonQuery($"UpsertAccount @username={account.username} , @password={account.password} , @permissions={account.permissions}");
        }
    }
}
