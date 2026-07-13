using E_Commerce.Application.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Services
{
    internal class TokenService : ITokenService
    {
        private readonly JWTSettings _jWTSettings;
        public TokenService(IOptions<JWTSettings> jwtoptions)
        {
            _jWTSettings=jwtoptions.Value;
        }

        public string CreateToken(string userId, string email, string userName, IReadOnlyList<string> roles)
        {
            var claims = new List<Claim>()
           {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Email,email),
            new Claim(ClaimTypes.Name,userName),
           };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var secKey=_jWTSettings.SecretKey;
            if (string.IsNullOrEmpty(secKey))
                throw new InvalidOperationException("JWT SecretKey Is Missing");
            if (secKey.Length < 32)
                throw new InvalidOperationException("JWT SecretKey Is To Short");
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secKey));
            var cerdentials= new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                issuer: _jWTSettings.Issuer,
                audience: _jWTSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jWTSettings.ExpirationMinutes),
                signingCredentials: cerdentials
                );
           return new JwtSecurityTokenHandler().WriteToken(token);

         
        }
    }

    public class JWTSettings
    {
        public string SecretKey { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; }= default!;
        public int ExpirationMinutes { get; set; }
    }
}
