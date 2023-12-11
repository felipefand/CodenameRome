using CodenameRome.Dtos;
using CodenameRome.Models;

namespace CodenameRome.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetByClientId(string clientId);
        Task<Product?> GetById(string id);
        Task Create(Product newProduct);
        Task Update(string id, ProductDto product);
        Task Remove(string id);
    }
}
