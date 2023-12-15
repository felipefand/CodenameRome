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
        private readonly IProductService _productService;
        private readonly string NOTFOUND = "Client not found.";
        private readonly string EMAILINUSE = "Email is already registered.";

        public ClientApplication(IClientService clientService, IEmployeeService employeeService, IProductService productService)
        {
            _clientService = clientService;
            _employeeService = employeeService;
            _productService = productService;
        }

        public async Task<List<Client>> GetAllClients()
        {
            var clientList = await _clientService.GetAll();
            return clientList;
        }

        public async Task<Client?> GetClientsById(string id)
        {
            var client = await _clientService.GetById(id);

            if (client == null)
                throw new Exception(NOTFOUND);

            return client;
        }

        public async Task<Client> ChangeClientStatus(string id, bool status)
        {
            var client = await _clientService.GetById(id);
            if (client == null)
                throw new Exception(NOTFOUND);

            client.IsLive = status;
            await _clientService.Replace(id, client);

            return client;
        }

        public async Task<ClientResponse> CreateClient(CreateClientDto createClient)
        {
            var employeeWithEmail = await _employeeService.GetByEmail(createClient.OwnerEmail);
            if (employeeWithEmail != null)
                throw new Exception(EMAILINUSE);

            createClient.Validate();

            var client = new Client
            {
                Name = createClient.BusinessName,
                Address = createClient.BusinessAddress,
                PhoneNumber = createClient.BusinessPhoneNumber,
                OwnerName = createClient.OwnerName,
                OwnerEmail = createClient.BusinessEmail,
                OwnerId = ""
            };

            await _clientService.Create(client);

            var employee = new Employee
            {
                Name = createClient.OwnerName,
                Address = createClient.OwnerAddress,
                PhoneNumber = createClient.OwnerPhoneNumber,
                Email = createClient.OwnerEmail,
                Password = createClient.OwnerPassword,
                Role = EmployeeRole.Owner,
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

            client.Validate();

            await _clientService.Update(id, client);
            var updatedClient = await _clientService.GetById(id);

            return updatedClient!;
        }

        public async Task<DeleteClientResponse> DeleteClient(string id)
        {
            var client = await _clientService.GetById(id);

            if (client == null)
                throw new Exception(NOTFOUND);

            await _clientService.Remove(id);

            var response = new DeleteClientResponse();
            response.Client = client;
            response.Employees = await DeleteClientEmployees(id);
            response.Products = await DeleteClientProducts(id);

            return response;
        }

        public async Task<List<Employee>> DeleteClientEmployees(string clientId)
        {
            var employeeList = await _employeeService.GetByClientId(clientId);

            foreach (var employee in employeeList)
            {
                await _employeeService.Remove(employee.Id!);
            }

            return employeeList;
        }

        public async Task<List<Product>> DeleteClientProducts(string clientId)
        {
            var productList = await _productService.GetByClientId(clientId);

            foreach(var product in productList)
            {
                await _productService.Remove(product.Id!);
            }

            return productList;
        }
    }
}
