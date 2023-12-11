namespace CodenameRome.Contracts.Clients
{
    public class CreateClientDto
    {
        public string BusinessName { get; set; } = String.Empty;
        public string BusinessAddress { get; set; } = String.Empty;
        public string BusinessPhoneNumber { get; set; } = String.Empty;
        public string OwnerName { get; set; } = String.Empty;
        public string OwnerAddress { get; set; } = String.Empty;
        public string OwnerPhoneNumber { get; set; } = String.Empty;
        public string OwnerEmail { get; set; } = String.Empty;
        public string OwnerUsername { get; set; } = String.Empty;
        public string OwnerPassword { get; set; } = String.Empty;
    }
}
