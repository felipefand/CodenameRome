using CodenameRome.Contracts.Clients;
using CodenameRome.Models;

namespace CodenameRome.Application.Interfaces
{
    public interface IClientApplication
    {
        Task<List<Client>> GetAllClients();
        Task<Client?> GetClientById(string id);
        Task<ClientResponse> CreateClient(CreateClientDto createClient);
        Task<Client> UpdateClient(string id, ClientDto client);
        Task<Client> DeleteClient(string id);
    }
}
