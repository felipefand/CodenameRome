namespace CodenameRome.Contracts.Employees
{
    public class ChangePassRequest
    {
        public string OldPassword { get; set; } = String.Empty;
        public string NewPassword { get; set; } = String.Empty;
    }
}
