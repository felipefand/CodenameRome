using CodenameRome.Auth;
using CodenameRome.Contracts.Login;
using CodenameRome.Services;

namespace CodenameRome.Application.Interfaces
{
    public interface ILoginApplication
    { 
        Task<LoginResponse> AttemptLogin(LoginRequest loginRequest);
    }
}
