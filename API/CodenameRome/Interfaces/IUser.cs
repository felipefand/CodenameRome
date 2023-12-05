namespace CodenameRome.Interfaces
{
    public interface IUser : IAuthenticatable
    {
        public string? Id { get; set; }
        public string ClientId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
