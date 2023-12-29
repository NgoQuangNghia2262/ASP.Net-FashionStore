using Bussiness.Exceptions;
using Bussiness.Interface;
using Bussiness.Helper;
using DataAccess;
using DataAccess.Interface;
using Microsoft.AspNetCore.Http;
using Model;
using Model.Interface;
using System.Data;


namespace Bussiness
{
    public class Account_BUS : IValidate, IAuthentication, IAccount_BUS
    {
        private readonly static string[] categorysUser = { "admin", "customer", "employe" };
        private IAccount_DAL account_DAL = new Account_DAL();
        private ICRUD ICrud = new Account_DAL();
        public bool ValidateModelData(object obj)
        {
            Account? account = obj as Account;
            if (account == null) { throw new ArgumentNullException("Tham số null"); }
            bool isUsername = account.username.Length > 2 && account.username.Length < 15;
            bool isPassword = account?.password?.Length > 6;
            bool isCategory = categorysUser.Contains(account?.permissions?.ToLower()) ? true : throw new Exception("Quyền của accoun tkhoong hợp lệ");
            if (!isUsername || !isPassword) { throw new LengthPropertyException("Account không hợp lệ."); }
            return true;
        }
        public bool ValidateKeyModel(IKey obj)
        {
            IKeyAccount? keyAccount = obj as IKeyAccount;
            if (keyAccount == null) { throw new ArgumentNullException("Tham số null"); }
            bool isUsername = keyAccount.username.Length > 2 && keyAccount.username.Length < 15;
            if (!isUsername) { throw new LengthPropertyException("Username không hợp lệ."); }
            return true;
        }
        public async Task<bool> ExistsModel(IKey obj)
        {
            DataTable dt = await ICrud.FindOne(obj);
            return dt.Rows.Count > 0;
        }
        public async Task LoginAsync(HttpContext context, Account account)
        {
            DataTable dt = await ICrud.FindOne(account);
            if (dt.Rows.Count == 0) { throw new InvalidCredentialsException("Tài khoản hoặc mật khẩu không đúng"); }
            bool verified = BCrypt.Net.BCrypt.Verify(account.password, dt?.Rows[0]["password"]?.ToString()?.Trim());
            if (!verified) { throw new InvalidCredentialsException("Tài khoản hoặc mật khẩu không đúng"); }
            account = Convert<Account>.DataRowToModel(dt.Rows[0]);
            string nameCookie = "accessToken";
            string value = ActionJWT.createJWT(account);
            ActionCookie.AddCookie(context, nameCookie, value);
        }
        public bool AdminAuth(HttpContext context)
        {
            string token = ActionCookie.GetCookieName(context, "accessToken");
            Account account = ActionJWT.VerifyJwtToken(token);
            return account.permissions == "admin";
        }

        public void Logout(HttpResponse res)
        {
            ActionCookie.DeleteCookie(res, "accessToken");
        }
        public async Task Regist(Account account, HttpContext context)
        {
            DataTable dt = await ICrud.FindOne(account);
            if (dt.Rows.Count > 0)
            {
                throw new DuplicateDataException("Account đã tồn tại");
            }
            string idCustomer = string.IsNullOrEmpty(context.Request.Cookies["newCustomer"]) ? StringUtility.GenerateRandomString(64) : context.Request.Cookies["newCustomer"];
            account.customer = new Customer(idCustomer);
            await Bussiness<Customer>.Save(account.customer);
            ValidateKeyModel(account);
            account.password = BCrypt.Net.BCrypt.HashPassword(account.password);
            await ICrud.Save(account);

        }
        public async void ChangePassword(Account account, string? newPass)
        {
            DataTable dt = await ICrud.FindOne(account);
            if (dt.Rows.Count == 0)
            {
                throw new DataNotFoundException("not found");
            }
            bool verified = BCrypt.Net.BCrypt.Verify(account.password, dt?.Rows[0]["password"]?.ToString()?.Trim());
            if (!verified) { throw new InvalidCredentialsException("Wrong Pass"); }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(newPass);
            account.password = passwordHash;
            await ICrud.Save(account);
        }
    }
}
