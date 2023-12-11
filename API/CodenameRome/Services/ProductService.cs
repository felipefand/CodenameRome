using CodenameRome.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using CodenameRome.Database;
using CodenameRome.Dtos;
using CodenameRome.Services.Interfaces;

namespace CodenameRome.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductService(IOptions<DBSettings> DBSettings)
        {
            var mongoClient = new MongoClient(
                DBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DBSettings.Value.DBName);

            _productCollection = mongoDatabase.GetCollection<Product>(
                DBSettings.Value.ProductCollectionName);
        }

        public async Task<List<Product>> GetByClientId(string clientId) =>
            await _productCollection.Find(x => x.ClientId == clientId).ToListAsync();

        public async Task<Product?> GetById(string id) =>
            await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task Create(Product newProduct) =>
            await _productCollection.InsertOneAsync(newProduct);

        public async Task Update(string id, ProductDto product)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq("Id", id);
            UpdateDefinition<Product> update = Builders<Product>.Update.Set(c => c.Id, id);

            var clientProperties = typeof(ProductDto).GetProperties();
            foreach (var property in clientProperties)
            {
                var value = property.GetValue(product);
                if (value != null)
                    update = update.Set(property.Name, value);
            }

            await _productCollection.UpdateOneAsync(filter, update);
        }

        public async Task Remove(string id) =>
            await _productCollection.DeleteOneAsync(x => x.Id == id);
    }
}
