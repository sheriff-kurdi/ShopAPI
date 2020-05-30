using Microsoft.IdentityModel.Tokens;
using shop.InfraStructure.Data;
using shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace shop.Services.Auth
{
    public class TokenAuth : ITokenAuth
    {
        private readonly AppDbContext context;

        public TokenAuth(AppDbContext context)
        {
            this.context = context;
        }
        public string GenerateToken()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:44323",
                audience: "http://localhost:4200",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;

        }

    }
}
