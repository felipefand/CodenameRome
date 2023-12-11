namespace CodenameRome.Contracts.Login
{
    public class LoginResponse
    {
        public string EmployeeId { get; set; } = String.Empty;
        public string EmployeeName { get; set; } = String.Empty;
        public string JwtToken { get; set; } = String.Empty;
    }
}
