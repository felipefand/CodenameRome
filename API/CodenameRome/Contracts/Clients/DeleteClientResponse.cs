using CodenameRome.Models;

namespace CodenameRome.Contracts.Clients
{
    public class DeleteClientResponse
    {
        public Client? Client { get; set; }
        public List<Employee>? Employees { get; set; }
        public List<Product>? Products { get; set; }
    }
}
