using CodenameRome.Models;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using CodenameRome.Application.Auth;

namespace CodenameRome.Auth
{
    public class TokenGenerator : ITokenGenerator
    {
        public readonly TokenSettings _tokenSettings;
        public TokenGenerator(IOptions<TokenSettings> tokenSettings) =>
            _tokenSettings = tokenSettings.Value;

        public string GenerateToken(Employee employee)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, employee.ClientId), // MAKE SURE THIS IS OK
                new Claim(ClaimTypes.Role, employee.Role.ToString()!),
                new Claim(ClaimTypes.NameIdentifier, employee.Id!)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret!));
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
