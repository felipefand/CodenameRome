using CodenameRome.Contracts.Clients;
using CodenameRome.Models;

namespace CodenameRome.Application.Interfaces
{
    public interface IClientApplication
    {
        Task<List<Client>> GetAllClients();
        Task<Client?> GetClientsById(string id);
        Task<Client> ChangeClientStatus(string id, bool status);
        Task<ClientResponse> CreateClient(CreateClientDto createClient);
        Task<Client> UpdateClient(string id, ClientDto client);
        Task<DeleteClientResponse> DeleteClient(string id);
        Task<List<Employee>> DeleteClientEmployees(string clientId);
        Task<List<Product>> DeleteClientProducts(string clientId);
    }
}
