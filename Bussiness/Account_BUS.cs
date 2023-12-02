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
    public class Account_BUS : IValidate , IAuthentication
    {
        private readonly static string[] categorysUser = { "admin", "customer", "employe" };
        private IAccount_DAL account_DAL = new Account_DAL();
        public Account_BUS() { }
        public Account_BUS(IAccount_DAL account_DAL)
        {
            this.account_DAL = account_DAL;
        }

        public bool ValidateModelData(object obj)
        {
            Account? account = obj as Account;
            if (account == null) { throw new ArgumentNullException("Tham số null"); }
            bool isUsername = account.username.Length > 2 && account.username.Length < 15;
            bool isPassword = account?.password?.Length > 6;
            bool isCategory = categorysUser.Contains(account?.permissions?.ToLower());
            if (!isUsername || !isPassword || !isCategory) { throw new InvalidAccountException("Account không hợp lệ."); }
           
            return true;
        }
        public bool ValidateKeyModel(IKey obj)
        {
            IKeyAccount? keyAccount = obj as IKeyAccount;
            if (keyAccount == null) { throw new ArgumentNullException("Tham số null"); }
            bool isUsername = keyAccount.username.Length > 2 && keyAccount.username.Length < 15;
            if (!isUsername) { throw new InvalidAccountException("Username không hợp lệ."); }
            return true;
        }

        public void Login(HttpContext context, Account account)
        {
            try
            {
                account_DAL.Login(account);
                string nameCookie = "AccessToken";
                string value = ActionJWT.createJWT(account);
                ActionCookie.AddCookie(context, nameCookie, value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool AdminAuth(HttpContext context)
        {
            string token = ActionCookie.GetCookieName(context , "AccessToken");
            Account account = ActionJWT.VerifyJwtToken(token);
            return token != null;
        }

        public void Logout(HttpResponse res)
        {
            ActionCookie.DeleteCookie(res, "AccessToken");
        }
    }
}
