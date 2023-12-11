using Microsoft.AspNetCore.Mvc;
using CodenameRome.Contracts.Login;
using CodenameRome.Application.Interfaces;

namespace CodenameRome.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginApplication _loginApplication;
        public LoginController (ILoginApplication loginApplication)
        {
            _loginApplication = loginApplication;
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                var response = await _loginApplication.AttemptLogin(loginRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
