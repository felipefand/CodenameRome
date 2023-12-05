namespace CodenameRome.Interfaces
{
    public interface IAuthenticatable
    {
        string Username { get; set; }
        string PasswordHash { get; set; }
        string ClientId { get; set; }
        string Role { get; set; }
    }
}
