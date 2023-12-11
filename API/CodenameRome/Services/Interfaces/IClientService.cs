using CodenameRome.Contracts.Clients;
using CodenameRome.Models;

namespace CodenameRome.Services.Interfaces
{
    public interface IClientService
    {
        Task<List<Client>> GetAll();
        Task<Client?> GetById(string id);
        Task Create(Client client);
        Task Replace(string id, Client updatedClient);
        Task Update(string id, ClientDto client);
        Task Remove(string id);
    }
}
