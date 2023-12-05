using CodenameRome.Database;
using CodenameRome.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CodenameRome.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _customerCollection;

        public CustomerService(IOptions<DBSettings> DBSettings, DatabaseFilters databaseFilters)
        {
            var mongoClient = new MongoClient(
                DBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DBSettings.Value.DBName);

            _customerCollection = mongoDatabase.GetCollection<Customer>(
                DBSettings.Value.CustomerCollectionName);
        }

        public async Task<List<Customer>> GetAsync(string clientId) =>
            await _customerCollection.Find(x => x.CustomerFrom.Contains(clientId)).ToListAsync();
        public async Task<Customer?> GetByPhoneNumberAsync(int phoneNumber) =>
            await _customerCollection.Find(x => x.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
    }
}
