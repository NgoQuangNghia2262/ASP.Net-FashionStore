using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
namespace src
{
    public class StartUp
    {
        public void ConfigureServices(IServiceCollection service)
        {
            service.AddControllers();
            service.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie = new CookieBuilder
        {
            HttpOnly = true,
            Path = "/",
            SameSite = SameSiteMode.Strict,

        };
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}