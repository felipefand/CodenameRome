using CodenameRome.Models;

namespace CodenameRome.Application.Auth
{
    public interface ITokenGenerator
    {
        string GenerateToken(Employee employee);
    }
}
