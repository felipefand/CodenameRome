using CodenameRome.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using CodenameRome.Database;

namespace CodenameRome.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly DatabaseFilters _dbFilters;

        public ProductService(IOptions<DBSettings> DBSettings, DatabaseFilters databaseFilters)
        {
            var mongoClient = new MongoClient(
                DBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DBSettings.Value.DBName);

            _productCollection = mongoDatabase.GetCollection<Product>(
                DBSettings.Value.ProductCollectionName);

            _dbFilters = databaseFilters;
        }

        public async Task<List<Product>> GetAsync(string clientId) =>
            await _productCollection.Find(x => x.ClientId == clientId).ToListAsync();

        public async Task<Product?> GetByIdAsync(string id) =>
            await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        //public async Task<List<string>> GetCategoriesAsync(string clientId)
        //{
        //    var pipeline = _dbFilters.getCategoriesFilter();
        //    var categories = await _productCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
        //    return categories.Select(x => x.GetValue("_id").AsString).ToList();
        //}

        //public async Task<List<Product>> GetByCategoryAsync(string type) =>
        //    await _productCollection.Find(x => x.Category == type).ToListAsync();

        public async Task CreateAsync(Product newProduct) =>
            await _productCollection.InsertOneAsync(newProduct);

        public async Task UpdateAsync(string id, Product updatedProduct) =>
            await _productCollection.ReplaceOneAsync(x => x.Id == id, updatedProduct);

        public async Task RemoveAsync(string id) =>
            await _productCollection.DeleteOneAsync(x => x.Id == id);
    }
}
