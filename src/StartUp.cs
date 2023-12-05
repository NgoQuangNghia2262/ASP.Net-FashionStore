using Microsoft.AspNetCore.Hosting;
namespace src
{
    public class StartUp
    {
        public void ConfigureServices(IServiceCollection service)
        {

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Run(async (HttpContext context) =>
            {
            });
        }
    }
}