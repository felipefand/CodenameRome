using CodenameRome.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using CodenameRome.Database;

namespace CodenameRome.Services
{
    public class LoginService
    {
        private readonly IMongoCollection<User> _userCollection;
        private readonly IMongoCollection<Employee> _employeeCollection;

        public LoginService(IOptions<DBSettings> DBSettings)
        {
            var mongoClient = new MongoClient(
                DBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DBSettings.Value.DBName);

            _userCollection = mongoDatabase.GetCollection<User>(
                DBSettings.Value.UserCollectionName);

            _employeeCollection = mongoDatabase.GetCollection<Employee>(
                DBSettings.Value.EmployeeCollectionName);
        }
        public async Task CreateAsync(User newUser) =>
            await _userCollection.InsertOneAsync(newUser);

        public async Task<User?> GetUserAsync(string username) =>
            await _userCollection.Find(x => x.Username == username).FirstOrDefaultAsync();

        public async Task<Employee?> GetEmployeeAsync(string username) =>
            await _employeeCollection.Find(x => x.Username == username).FirstOrDefaultAsync();
    }
}
