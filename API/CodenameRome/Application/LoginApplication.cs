using CodenameRome.Application.Auth;
using CodenameRome.Application.Interfaces;
using CodenameRome.Contracts.Login;
using CodenameRome.Services.Interfaces;

namespace CodenameRome.Application
{
    public class LoginApplication : ILoginApplication
    {
        private readonly IEmployeeService _employeeService;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly string ERRORMESSAGE = "Wrong username or password.";

        public LoginApplication(IEmployeeService employeeService, ITokenGenerator tokenGenerator)
        {
            _employeeService = employeeService;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<LoginResponse> AttemptLogin(LoginRequest loginRequest)
        {
            var employee = await _employeeService.GetByUsername(loginRequest.Username);

            if (employee == null)
                throw new Exception(ERRORMESSAGE);

            var isPasswordCorrect = employee.VerifyPassword(loginRequest.Password);

            if (!isPasswordCorrect)
                throw new Exception(ERRORMESSAGE);

            var response = new LoginResponse
            {
                JwtToken = _tokenGenerator.GenerateToken(employee),
                EmployeeName = employee.Name,
                EmployeeId = employee.Id!
            };

            return response;
        }
    }
}
