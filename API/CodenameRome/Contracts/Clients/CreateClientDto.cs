using System.ComponentModel.DataAnnotations;

namespace CodenameRome.Contracts.Clients
{
    public class CreateClientDto
    {
        [Required]
        public string BusinessName { get; set; } = String.Empty;
        [Required]
        public string BusinessAddress { get; set; } = String.Empty;
        [Required]
        public string BusinessPhoneNumber { get; set; } = String.Empty;
        [Required]
        public string BusinessEmail { get; set; } = String.Empty;
        [Required]
        public string OwnerName { get; set; } = String.Empty;
        [Required]
        public string OwnerAddress { get; set; } = String.Empty;
        [Required]
        public string OwnerPhoneNumber { get; set; } = String.Empty;
        [Required]
        public string OwnerEmail { get; set; } = String.Empty;
        [Required]
        public string OwnerPassword { get; set; } = String.Empty;

        public void Validate()
        {
            //TO ADD
        }
    }
}
