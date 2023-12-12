using Microsoft.AspNetCore.Http;
using Model;
using System;

namespace Bussiness.Interface
{
    public interface IAuthentication
    {
        void Login(HttpContext context, Account account);
        bool AdminAuth(HttpContext context);
        bool UserAuth(HttpContext context);

        void Logout(HttpResponse res);
    }
}
