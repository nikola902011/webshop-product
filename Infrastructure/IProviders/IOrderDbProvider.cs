using ProductsControl.Models;

namespace ProductsControl.Infrastructure.IProviders
{
    public interface IOrderDbProvider
    {
        Task AddOrderAsync(Order newOrder);
        Task CancelOrderAsync(int orderId);
        Task<Order?> FindOrderByIdAsync(int orderId);
        Task SaveChangesAsync();
        Task<List<OrderItem>> GetOrderItemsOnOrderAsync(int orderId);
        Task<List<Order>> GetOldOrdersForBuyerAsync(int buyerId);
        Task<List<Order>> GetNewOrdersForBuyerAsync(int buyerId);
        Task<List<Order>> GetOldOrdersForSellerAsync(int sellerId);
        Task<List<Order>> GetNewOrdersForSellerAsync(int sellerId);
        Task<List<Order>> GetAllOrdersAsync();
    }
}
