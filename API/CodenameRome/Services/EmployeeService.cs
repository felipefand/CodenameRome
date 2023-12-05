using CodenameRome.Database;
using CodenameRome.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CodenameRome.Services
{
    public class EmployeeService
    {
        private readonly IMongoCollection<Employee> _employeeCollection;

        public EmployeeService(IOptions<DBSettings> DBSettings, DatabaseFilters databaseFilters)
        {
            var mongoClient = new MongoClient(
                DBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DBSettings.Value.DBName);

            _employeeCollection = mongoDatabase.GetCollection<Employee>(
                DBSettings.Value.EmployeeCollectionName);
        }
        public async Task<List<Employee>> GetAsync(string clientId) =>
            await _employeeCollection.Find(x => x.ClientId == clientId).ToListAsync();
        public async Task<Employee?> GetByIdAsync(string id) =>
            await _employeeCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Employee employee) =>
            await _employeeCollection.InsertOneAsync(employee);

        public async Task UpdateAsync(string id, Employee updatedEmployee) =>
            await _employeeCollection.ReplaceOneAsync(x => x.Id == id, updatedEmployee);

        public async Task RemoveAsync(string id) =>
            await _employeeCollection.DeleteOneAsync(x => x.Id == id);
    }
}
