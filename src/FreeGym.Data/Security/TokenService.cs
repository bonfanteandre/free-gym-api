using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FreeGym.Data.Security
{
    public class TokenService
    {
        private readonly JwtOptions _jwtOptions;
        public int TokenExpiresIn => _jwtOptions.Expiration;

        public TokenService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(IdentityUser user, IList<Claim> claims, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaims(claims);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Expires = DateTime.UtcNow.AddHours(_jwtOptions.Expiration),
                Subject = claimsIdentity,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
