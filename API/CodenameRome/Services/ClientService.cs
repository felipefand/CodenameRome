using CodenameRome.Contracts.Clients;
using CodenameRome.Database;
using CodenameRome.Models;
using CodenameRome.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CodenameRome.Services
{
    public class ClientService : IClientService
    {
        private readonly IMongoCollection<Client> _clientCollection;

        public ClientService(IOptions<DBSettings> dbSettings)
        {
            var mongoClient = new MongoClient(
                dbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                dbSettings.Value.DBName);

            _clientCollection = mongoDatabase.GetCollection<Client>(
                dbSettings.Value.ClientCollectionName);
        }

        public async Task<List<Client>> GetAll() =>
            await _clientCollection.Find(_ => true).ToListAsync();

        public async Task<Client?> GetById(string id) =>
            await _clientCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task Create(Client client) =>
            await _clientCollection.InsertOneAsync(client);

        public async Task Replace(string id, Client updatedClient) =>
            await _clientCollection.ReplaceOneAsync(x => x.Id == id, updatedClient);

        public async Task Update(string id, ClientDto client)
        {
            FilterDefinition<Client> filter = Builders<Client>.Filter.Eq("Id", id);
            UpdateDefinition<Client> update = Builders<Client>.Update.Set(c => c.Id, id);

            var clientProperties = typeof(ClientDto).GetProperties();
            foreach (var property in clientProperties)
            {
                var value = property.GetValue(client);
                if (value != null)
                    update = update.Set(property.Name, value);
            }

            await _clientCollection.UpdateOneAsync(filter, update);
        }

        public async Task Remove(string id) =>
            await _clientCollection.DeleteOneAsync(x => x.Id == id);
    }
}
