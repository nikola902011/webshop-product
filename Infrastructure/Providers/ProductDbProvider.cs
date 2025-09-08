using Microsoft.EntityFrameworkCore;
using ProductsControl.Infrastructure.IProviders;
using ProductsControl.Models;
using System;

namespace ProductsControl.Infrastructure.Providers
{
    public class ProductDbProvider : IProductDbProvider
    {
        private readonly ProductDbContext dbContext;

        public ProductDbProvider(ProductDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddProduct(Product newProduct)
        {
            await dbContext.Products.AddAsync(newProduct);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveProduct(Product product)
        {
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Product>> GetAllProductsAsync(int sellerId)
        {
            List<Product> products = await dbContext.Products
                .Where(p => p.SellerId == sellerId)
                .ToListAsync();

            return products;
        }
        public async Task<Product?> GetProductByNameAsync(string productName)
        {
            Product? product = await dbContext.Products.FirstOrDefaultAsync(x => x.Name == productName);

            return product;
        }
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            Product? product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            return product;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> products = await dbContext.Products.ToListAsync();

            return products;
        }
    }
}
