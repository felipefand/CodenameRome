using CodenameRome.Dtos;
using CodenameRome.Models;

namespace CodenameRome.Application.Interfaces
{
    public interface IProductApplication
    {
        Task<List<Product>> GetProductsByClientId(string clientId);
        Task<Product?> GetProductById(string id, string clientId);
        Task<Product> CreateProduct(ProductDto product, string clientId);
        Task<Product> UpdateProduct(ProductDto product, string clientId, string id);
        Task<Product> DeleteProduct(string clientId, string id);
    }
}
