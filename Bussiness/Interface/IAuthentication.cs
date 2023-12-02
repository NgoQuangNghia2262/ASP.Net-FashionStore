using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Interface
{
    public interface IAuthentication
    {
        void Login(HttpContext context , Account account);
        bool AdminAuth(HttpContext context);
        void Logout(HttpResponse res);
    }
}
