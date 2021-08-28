using System;
using System.Collections.Generic;
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
            int expirationInMinutes = Convert.ToInt32(this.Configuration["auth:expirationtime"]);
            IDictionary<string, object> claims = this.GenerateClaims(access: true, refresh: false);
            return GenerateToken(user, expirationInMinutes, claims);
        }

        public string CreateRefreshToken(User user)
        {
            int expirationInMinutes = Convert.ToInt32(this.Configuration["auth:refreshexpirationtime"]);
            IDictionary<string, object> claims = this.GenerateClaims(access: false, refresh: true);
            return GenerateToken(user, expirationInMinutes, claims);
        }

        private IDictionary<string, object> GenerateClaims(bool access, bool refresh)
        {
            var claims = new Dictionary<string, object>();
            
            if (access)
            {
                claims.Add("canAccess", "true");
            }

            if (refresh)
            {
                claims.Add("canRefresh", "true");
            }
            
            return claims;
        }

        private string GenerateToken(User user, int expirationTimeInMinutes, IDictionary<string, object> claims)
        {
            string issuer = this.Configuration["auth:issuer"];
            string key = this.Configuration["auth:key"];
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] bytesKey = Encoding.ASCII.GetBytes(key);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(bytesKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddMinutes(expirationTimeInMinutes),
                IssuedAt = DateTime.UtcNow,
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
                Subject = CreateIdentityClaim(user),
                Claims = claims
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private ClaimsIdentity CreateIdentityClaim(User user)
            => new ClaimsIdentity(new Claim[] 
        {
            new Claim(ClaimTypes.NameIdentifier, user.Guid.ToString()),                
        });
    }
}