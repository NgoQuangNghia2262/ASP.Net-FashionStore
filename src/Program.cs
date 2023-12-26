using Bussiness;
using Bussiness.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IProduct_BUS, Product_BUS>();
builder.Services.AddCors();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
var app = builder.Build();

//app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:8000")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials();
});
app.MapControllers();

app.Run();
//namespace src
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            string passwordHash = BCrypt.Net.BCrypt.HashPassword("Pa$$w0rd");
//            Console.WriteLine(passwordHash);
//        }
//    }
//}
