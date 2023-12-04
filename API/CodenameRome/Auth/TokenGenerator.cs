using CodenameRome.Models;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace CodenameRome.Auth
{
    public class TokenGenerator
    {
        public readonly TokenSettings _tokenSettings;
        public TokenGenerator(IOptions<TokenSettings> tokenSettings) =>
            _tokenSettings = tokenSettings.Value;

        public string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.ClientId),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_tokenSettings.ExpirationTimeInMinutes),
                    issuer: _tokenSettings.Issuer,
                    signingCredentials: credentials,
                    audience: _tokenSettings.Audience
                    );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
