using ProductsControl.DTO;

namespace ProductsControl.Interfaces
{
    public interface ISellerService
    {
        Task<bool> AddProductAsync(AddProductDto addProductDto, int sellerId);
        Task<bool> RemoveProductAsync(int id, int sellerId);
        Task<GetProductDto> GetProductAsync(int id, int sellerId);
        Task UpdateProductAsync(UpdateProductDto updateProductDto, int sellerId);
        Task<List<GetOrderDto>> GetOldOrdersAsync(int sellerId);
        Task<List<GetOrderDto>> GetNewOrdersAsync(int sellerId);
        Task<List<GetProductDto>> GetProductsAsync(int sellerId);
    }
}
