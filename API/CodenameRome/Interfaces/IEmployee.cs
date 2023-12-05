namespace CodenameRome.Interfaces
{
    public interface IEmployee : IAuthenticatable
    {
        public string? Id { get; set; }
        public string ClientId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salary { get; set; }
        public string Role { get; set; }
    }
}
