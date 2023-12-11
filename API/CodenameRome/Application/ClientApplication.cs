using CodenameRome.Application.Interfaces;
using CodenameRome.Contracts.Clients;
using CodenameRome.Models;
using CodenameRome.Services.Interfaces;

namespace CodenameRome.Application
{
    public class ClientApplication : IClientApplication
    {
        private readonly IClientService _clientService;
        private readonly IEmployeeService _employeeService;
        private readonly string NOTFOUND = "Client not found.";
        private readonly string DEFAULTACCESSLEVEL = "10";

        public ClientApplication(IClientService clientService, IEmployeeService employeeService)
        {
            _clientService = clientService;
            _employeeService = employeeService;
        }

        public async Task<List<Client>> GetAllClients()
        {
            var clientList = await _clientService.GetAll();
            return clientList;
        }

        public async Task<Client?> GetClientById(string id)
        {
            var client = await _clientService.GetById(id);

            if (client == null)
                throw new Exception(NOTFOUND);

            return client;
        }

        public async Task<ClientResponse> CreateClient(CreateClientDto createClient)
        {
            var client = new Client
            {
                Name = createClient.BusinessName,
                Address = createClient.BusinessAddress,
                PhoneNumber = createClient.BusinessPhoneNumber,
                OwnerName = createClient.OwnerName,
                OwnerEmail = createClient.OwnerEmail,
                OwnerId = ""
            };

            await _clientService.Create(client);

            var employee = new Employee
            {
                Name = createClient.OwnerName,
                Address = createClient.OwnerAddress,
                PhoneNumber = createClient.OwnerPhoneNumber,
                Username = createClient.OwnerUsername,
                Password = createClient.OwnerPassword,
                AccessLevel = DEFAULTACCESSLEVEL,
                ClientId = client.Id!
            };

            await _employeeService.Create(employee);

            client.OwnerId = employee.Id!;
            await _clientService.Replace(client.Id!, client);

            var response = new ClientResponse
            {
                Client = client,
                Employee = employee
            };

            return response;
        }

        public async Task<Client> UpdateClient(string id, ClientDto client)
        {
            var oldClient = await _clientService.GetById(id);

            if (oldClient == null)
                throw new Exception(NOTFOUND);

            await _clientService.Update(id, client);
            var updatedClient = await _clientService.GetById(id);

            return updatedClient!;
        }

        public async Task<Client> DeleteClient(string id)
        {
            var client = await _clientService.GetById(id);

            if (client == null)
                throw new Exception(NOTFOUND);

            await _clientService.Remove(id);

            return client;
        }
    }
}
