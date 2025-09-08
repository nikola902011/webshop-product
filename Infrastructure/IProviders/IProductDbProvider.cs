using ProductsControl.Models;

namespace ProductsControl.Infrastructure.IProviders
{
    public interface IProductDbProvider
    {
        Task<bool> AddProduct(Product newProduct);
        Task<bool> RemoveProduct(Product product);
        Task<List<Product>> GetAllProductsAsync(int sellerId);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByNameAsync(string productName);
        Task<Product?> GetProductByIdAsync(int id);
        Task<bool> UpdateProduct(Product product);
    }
}
