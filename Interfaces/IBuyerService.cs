using ProductsControl.DTO;

namespace ProductsControl.Interfaces
{
    public interface IBuyerService
    {
        Task AddOrderAsync(AddOrderDto addOrderDto, int buyerId);
        Task CancelOrderAsync(int orderId, int buyerId);
        Task<List<GetOrderDto>> GetOldOrdersAsync(int buyerId);
        Task<List<GetOrderDto>> GetNewOrdersAsync(int buyerId);
        Task<List<GetProductDto>> GetAllProductsAsync();
    }
}
