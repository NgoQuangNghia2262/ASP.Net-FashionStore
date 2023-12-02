using Bussiness.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Bussiness.Middleware
{
    internal class ActionJWT
    {
        private readonly static string JWT_SECRET = "NghiaxNghiax";
        private readonly static RSAParameters RSAParameters = new RSAParameters();
        public static string createJWT(Account obj)
        {
            if (obj == null) { throw new ArgumentNullException("Tham số Account = null"); }
            byte[] hash;
            using (var algorithm = SHA256.Create())
            {
                hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(JWT_SECRET));
            }
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(hash);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,obj.username),
                new Claim(ClaimTypes.Role,obj.permissions ?? "")
            };
            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
        public static Account VerifyJwtToken(string jwtToken)
        {
            if (jwtToken == null) { throw new NotAuthenticated("User is not authenticated"); }
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken? jsonToken = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;
            string username = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
            string? role = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            Account account = new Account
            {
                username = username,
                permissions = role
            };
            return account;
        }
    }
}
