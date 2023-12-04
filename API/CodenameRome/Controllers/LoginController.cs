using Microsoft.AspNetCore.Mvc;
using CodenameRome.Auth;
using CodenameRome.Dtos;
using CodenameRome.Services;
using CodenameRome.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CodenameRome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly Hasher _hasher;
        private readonly TokenGenerator _tokenGenerator;
        private readonly LoginService _loginService;
        public LoginController(LoginService loginService, TokenGenerator tokenGenerator)
        {
            _loginService = loginService;
            _tokenGenerator = tokenGenerator;
            _hasher = new Hasher();
        }

        [HttpPost("register")]
        [Authorize(Roles = "sysadmin")]
        public async Task<IActionResult> Register(UserDto user, string clientId)
        {
            var newUser = new User();
            newUser.Username = user.Username;
            newUser.PasswordHash = _hasher.passwordTextToHash(user.Password);
            newUser.Role = user.Role;
            newUser.ClientId = clientId;
            await _loginService.CreateAsync(newUser);

            return Created(String.Empty, newUser);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginRequest)
        {
            var user = await _loginService.GetAsync(loginRequest.Username);

            if (user == null) return BadRequest("User not found.");

            var isPasswordCorrect = _hasher.verifyPassword(loginRequest.Password, user.PasswordHash);
            if (!isPasswordCorrect) return BadRequest("Wrong password.");

            var token = _tokenGenerator.GenerateToken(user);
            return Ok(token);
        }
    }
}
