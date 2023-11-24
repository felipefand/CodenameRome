using CodenameRome.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using CodenameRome.Utils;

namespace CodenameRome.Services
{
    public class MenuService
    {
        private readonly IMongoCollection<MenuItem> _menuCollection;
        private readonly DatabaseFilters _dbFilters;

        public MenuService(
            IOptions<DBSettings> DBSettings)
        {
            var mongoClient = new MongoClient(
                DBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DBSettings.Value.DBName);

            _menuCollection = mongoDatabase.GetCollection<MenuItem>(
                DBSettings.Value.MenuCollectionName);

            _dbFilters = new DatabaseFilters();
        }

        public async Task<List<MenuItem>> GetAsync() =>
            await _menuCollection.Find(_ => true).ToListAsync();

        public async Task<MenuItem?> GetAsync(string id) =>
            await _menuCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<string>> GetCategoriesAsync()
        {
            var pipeline = _dbFilters.getCategoriesFilter();
            var categories = await _menuCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
            return categories.Select(x => x.GetValue("_id").AsString).ToList();
        }

        public async Task<List<MenuItem>> GetByCategoryAsync(string type) =>
            await _menuCollection.Find(x => x.Category == type).ToListAsync();

        public async Task CreateAsync(MenuItem newItem) =>
            await _menuCollection.InsertOneAsync(newItem);

        public async Task UpdateAsync(string id, MenuItem updatedItem) =>
            await _menuCollection.ReplaceOneAsync(x => x.Id == id, updatedItem);

        public async Task RemoveAsync(string id) =>
            await _menuCollection.DeleteOneAsync(x => x.Id == id);
    }
}
