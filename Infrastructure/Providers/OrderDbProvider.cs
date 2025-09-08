using Microsoft.EntityFrameworkCore;
using ProductsControl.Infrastructure.IProviders;
using ProductsControl.Models.Enums;
using ProductsControl.Models;
using System;

namespace ProductsControl.Infrastructure.Providers
{
    public class OrderDbProvider : IOrderDbProvider
    {
        private readonly ProductDbContext dbContext;

        public OrderDbProvider(ProductDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddOrderAsync(Order newOrder)
        {
            await dbContext.Orders.AddAsync(newOrder);
            await dbContext.SaveChangesAsync();
        }

        public async Task CancelOrderAsync(int orderId)
        {
            Order? order = await dbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Order?> FindOrderByIdAsync(int orderId)
        {
            Order? order = await dbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            order.OrderItems = await GetOrderItemsOnOrderAsync(orderId);

            return order;
        }

        public async Task<List<OrderItem>> GetOrderItemsOnOrderAsync(int orderId)
        {
            return await dbContext.OrderItems.Where(x => x.OrderId == orderId).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Order>> GetOldOrdersForBuyerAsync(int buyerId)
        {
            List<Order>? orders = await dbContext.Orders
                .Include(x => x.OrderItems)
                .Where(x => x.BuyerId == buyerId &&
                            x.OrderStatus == EOrderStatus.NOT_CANCELED &&
                            x.DeliveryTime <= DateTime.Now)
                .ToListAsync();

            return orders;
        }

        public async Task<List<Order>> GetNewOrdersForBuyerAsync(int buyerId)
        {
            List<Order>? orders = await dbContext.Orders
                .Include(x => x.OrderItems)
                .Where(x => x.BuyerId == buyerId &&
                            x.OrderStatus == EOrderStatus.NOT_CANCELED &&
                            x.DeliveryTime > DateTime.Now)
                .ToListAsync();

            return orders;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            List<Order> orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .ToListAsync();

            return orders;
        }

        public async Task<List<Order>> GetOldOrdersForSellerAsync(int sellerId)
        {
            List<Order> orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.OrderStatus == EOrderStatus.NOT_CANCELED &&
                            o.DeliveryTime <= DateTime.Now &&
                            o.OrderItems.Any(oi => dbContext.Products.Any(p => p.Id == oi.ProductId && p.SellerId == sellerId)))
                .ToListAsync();

            foreach (var order in orders)
            {
                order.OrderItems = order.OrderItems.Where(oi => dbContext.Products.Any(p => p.Id == oi.ProductId && p.SellerId == sellerId)).ToList();
            }

            return orders;
        }


        public async Task<List<Order>> GetNewOrdersForSellerAsync(int sellerId)
        {
            List<Order> orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.OrderStatus == EOrderStatus.NOT_CANCELED &&
                            o.DeliveryTime > DateTime.Now &&
                            o.OrderItems.Any(oi => dbContext.Products.Any(p => p.Id == oi.ProductId && p.SellerId == sellerId)))
                .ToListAsync();

            foreach (var order in orders)
            {
                order.OrderItems = order.OrderItems.Where(oi => dbContext.Products.Any(p => p.Id == oi.ProductId && p.SellerId == sellerId)).ToList();
            }

            return orders;
        }
    }
}
