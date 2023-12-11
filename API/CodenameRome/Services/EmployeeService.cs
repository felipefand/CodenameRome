using CodenameRome.Contracts.Employees;
using CodenameRome.Database;
using CodenameRome.Models;
using CodenameRome.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CodenameRome.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMongoCollection<Employee> _employeeCollection;

        public EmployeeService(IOptions<DBSettings> DBSettings)
        {
            var mongoClient = new MongoClient(
                DBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DBSettings.Value.DBName);

            _employeeCollection = mongoDatabase.GetCollection<Employee>(
                DBSettings.Value.EmployeeCollectionName);
        }
        public async Task<List<Employee>> GetByClientId(string clientId) =>
            await _employeeCollection.Find(x => x.ClientId == clientId).ToListAsync();
        public async Task<Employee?> GetById(string id) =>
            await _employeeCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Employee?> GetByUsername(string username) =>
            await _employeeCollection.Find(x => x.Username == username).FirstOrDefaultAsync();

        public async Task Create(Employee employee) =>
            await _employeeCollection.InsertOneAsync(employee);

        public async Task Update(string id, EmployeeDto employee)
        {
            //FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("Id", id);
            //UpdateDefinition<Employee> update = Builders<Employee>.Update.Set(e => e.Id, id);

            //var employeeProperties = typeof(EmployeeDto).GetProperties();
            //foreach (var property in employeeProperties)
            //{
            //    var value = property.GetValue(employee);
            //    if (value != null)
            //        update = update.Set(property.Name, value);
            //}

            //await _employeeCollection.UpdateOneAsync(filter, update);

            var updatedEmployee = await _employeeCollection.Find(e => e.Id == id).FirstOrDefaultAsync();

            var employeeProperties = typeof(EmployeeDto).GetProperties();
            foreach (var property in employeeProperties)
            {
                var value = property.GetValue(employee);
                if (value != null)
                {
                    var propertyToUpdate = typeof(Employee).GetProperty(property.Name);
                    propertyToUpdate!.SetValue(updatedEmployee, value);
                }
            }

            await _employeeCollection.ReplaceOneAsync(e => e.Id == id, updatedEmployee);
        }
        public async Task ChangePassword(string id, string newPassword)
        {
            //FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("Id", id);
            //UpdateDefinition<Employee> update = Builders<Employee>.Update.Set(e => e.PasswordHash, newPassword);

            //await _employeeCollection.UpdateOneAsync(filter, update);

            var employee = await _employeeCollection.Find(e => e.Id == id).FirstOrDefaultAsync();
            employee.Password = newPassword;
            await _employeeCollection.ReplaceOneAsync(e => e.Id == id, employee);
        }

        public async Task Remove(string id) =>
            await _employeeCollection.DeleteOneAsync(x => x.Id == id);
    }
}
