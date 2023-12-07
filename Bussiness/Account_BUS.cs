using Bussiness.Exceptions;
using Bussiness.Interface;
using Bussiness.Middleware;
using DataAccess;
using DataAccess.Interface;
using Microsoft.AspNetCore.Http;
using Model;
using Model.Interface;
using System;
using System.Data;
using System.Security.Principal;


namespace Bussiness
{
    public class Account_BUS : IValidate, IAuthentication, IAccount_BUS
    {
        private readonly static string[] categorysUser = { "admin", "customer", "employe" };
        private IAccount_DAL account_DAL = new Account_DAL();
        private DataAccess.Interface.ICRUD ICrud = new Account_DAL();
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
        public bool ExistsModel(IKey obj)
        {
            DataTable dt = ICrud.FindOne(obj);
            return dt.Rows.Count > 0;
        }
        public void Login(HttpContext context, Account account)
        {
            account_DAL.Login(account);
            string nameCookie = "AccessToken";
            string value = ActionJWT.createJWT(account);
            ActionCookie.AddCookie(context, nameCookie, value);
        }
        public bool AdminAuth(HttpContext context)
        {
            string token = ActionCookie.GetCookieName(context, "AccessToken");
            Account account = ActionJWT.VerifyJwtToken(token);
            return account.permissions == "admin";
        }

        public void Logout(HttpResponse res)
        {
            ActionCookie.DeleteCookie(res, "AccessToken");
        }
        public void Regist(Account account)
        {
            DataTable dt = ICrud.FindOne(account);
            if (dt.Rows.Count > 0)
            {
                throw new DuplicateDataException("Account đã tồn tại");
            }
            ValidateKeyModel(account);
            account.password = BCrypt.Net.BCrypt.HashPassword(account.password);
            ICrud.Save(account);

        }
        public void ChangePassword(Account account, string? newPass)
        {
            DataTable dt = ICrud.FindOne(account);
            if (dt.Rows.Count == 0)
            {
                throw new DataNotFoundException("not found");
            }
            bool verified = BCrypt.Net.BCrypt.Verify(account.password, dt?.Rows[0]["password"]?.ToString()?.Trim());
            if (!verified) { throw new InvalidCredentialsException("Wrong Pass"); }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(newPass);
            account.password = passwordHash;
            ICrud.Save(account);
        }
    }
}
