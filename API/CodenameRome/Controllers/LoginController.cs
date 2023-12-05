using Microsoft.AspNetCore.Mvc;
using CodenameRome.Auth;
using CodenameRome.Dtos;
using CodenameRome.Services;
using CodenameRome.Models;
using Microsoft.AspNetCore.Authorization;
using CodenameRome.Interfaces;

namespace CodenameRome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Hasher _hasher;
        private readonly TokenGenerator _tokenGenerator;
        private readonly LoginService _loginService;
        public LoginController(LoginService loginService, TokenGenerator tokenGenerator, Hasher hasher)
        {
            _loginService = loginService;
            _tokenGenerator = tokenGenerator;
            _hasher = hasher;
        }

        [HttpPost("register")]
        [Authorize(Roles = "sysadmin")]
        public async Task<IActionResult> Register(UserDto user, string clientId)
        {
            var newUser = new User();
            newUser.Username = user.Username;
            newUser.PasswordHash = _hasher.passwordTextToHash(user.Password);
            newUser.ClientId = clientId;
            await _loginService.CreateAsync(newUser);

            return Created(String.Empty, newUser);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginDto loginRequest)
        {
            IAuthenticatable user = await _loginService.GetUserAsync(loginRequest.Username);

            if (user == null)
                user = await _loginService.GetEmployeeAsync(loginRequest.Username);

            if (user == null) return BadRequest("User not found.");

            var isPasswordCorrect = _hasher.verifyPassword(loginRequest.Password, user.PasswordHash);
            if (!isPasswordCorrect) return BadRequest("Wrong password.");

            var token = _tokenGenerator.GenerateToken(user);
            return Ok(token);
        }
    }
}
