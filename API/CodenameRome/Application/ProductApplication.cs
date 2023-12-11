using CodenameRome.Application.Interfaces;
using CodenameRome.Dtos;
using CodenameRome.Models;
using CodenameRome.Services.Interfaces;

namespace CodenameRome.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductService _productService;
        private readonly string NOTFOUND = "Product not found.";

        public ProductApplication(IProductService productService) =>
            _productService = productService;

        public async Task<List<Product>> GetProductsByClientId(string clientId)
        {
            var productList = await _productService.GetByClientId(clientId);
            return productList;
        }

        public async Task<Product?> GetProductById(string id, string clientId)
        {
            var product = await _productService.GetById(id);

            if (product == null || product.ClientId != clientId)
                throw new Exception(NOTFOUND);

            return product;
        }

        public async Task<Product> CreateProduct(ProductDto product, string clientId)
        {
            product.ValidateProduct();

            var newProduct = new Product
            {
                ClientId = clientId,
                Category = product.Category,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };

            await _productService.Create(newProduct);
            return newProduct;
        }

        public async Task<Product> UpdateProduct(ProductDto product, string clientId, string id)
        {
            var oldProduct = await _productService.GetById(id);

            if (oldProduct == null || oldProduct.ClientId != clientId)
                throw new ArgumentNullException(NOTFOUND);

            product.ValidateProduct();
            await _productService.Update(id, product);
            var updatedProduct = await _productService.GetById(id);

            return updatedProduct!;
        }

        public async Task<Product> DeleteProduct(string clientId, string id)
        {
            var product = await _productService.GetById(id);

            if (product == null || product.ClientId != clientId)
                throw new Exception(NOTFOUND);

            await _productService.Remove(id);
            return product;
        }
    }
}
