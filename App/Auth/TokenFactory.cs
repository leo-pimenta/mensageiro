using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace App.Auth
{
    public class TokenFactory : ITokenFactory
    {
        private readonly IConfiguration Configuration;

        public TokenFactory(IConfiguration configuration) => this.Configuration = configuration;

        public string Create(User user)
        {
            string issuer = this.Configuration["auth:issuer"];
            string key = this.Configuration["auth:key"];
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] bytesKey = Encoding.ASCII.GetBytes(key);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(bytesKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddMinutes(30),
                IssuedAt = DateTime.UtcNow,
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}