using CodenameRome.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using CodenameRome.Utils;
using CodenameRome.Database;

namespace CodenameRome.Services
{
    public class LoginService
    {
        private readonly IMongoCollection<User> _userCollection;
        private readonly DatabaseFilters _dbFilters;

        public LoginService(
            IOptions<DBSettings> DBSettings)
        {
            var mongoClient = new MongoClient(
                DBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DBSettings.Value.DBName);

            _userCollection = mongoDatabase.GetCollection<User>(
                DBSettings.Value.UserCollectionName);

            _dbFilters = new DatabaseFilters();
        }
        public async Task CreateAsync(User newUser) =>
            await _userCollection.InsertOneAsync(newUser);

        public async Task<User?> GetAsync(string username) =>
            await _userCollection.Find(x => x.Username == username).FirstOrDefaultAsync();
    }
}
