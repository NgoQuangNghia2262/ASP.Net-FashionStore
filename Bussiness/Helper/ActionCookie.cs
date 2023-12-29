using Bussiness.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Bussiness.Helper
{
    public class ActionCookie
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

        public static string GetCookieName(HttpContext context, string cookieName)
        {
            string myCookieValue = context.Request.Cookies[cookieName];
            if (string.IsNullOrEmpty(myCookieValue))
            {
                throw new NotAuthenticated("User is not authenticated");
            }
            return myCookieValue;
        }
        public static void DeleteCookie(HttpResponse response, string cookieName)
        {
            response.Cookies.Delete(cookieName);
        }
    }
}
