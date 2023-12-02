using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Middleware
{
    internal class ActionCookie
    {
        public static void AddCookie(HttpContext context, string cookieName, string cookieValue)
        {
            context.Response.Cookies.Append(cookieName, cookieValue, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // Set thành true để triển khai với HTTPS
                Expires = DateTime.Now.AddDays(1),
                Path = "/",
                SameSite = SameSiteMode.Strict
            });
        }
        public static string GetCookieName(HttpContext context , string cookieName) {
            string myCookieValue = context.Request.Cookies[cookieName];
            return myCookieValue;
        }
        public static void DeleteCookie(HttpResponse response, string cookieName)
        {
            response.Cookies.Delete(cookieName);
        }
    }
}
