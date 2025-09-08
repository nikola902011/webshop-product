using ProductsControl.DTO;
using ProductsControl.Interfaces;
using ProductsControl.Models.Enums;
using ProductsControl.Models;
using ProductsControl.Infrastructure.IProviders;
using AutoMapper;

namespace ProductsControl.Services
{
    public class BuyerService : IBuyerService
    {
        private readonly IOrderDbProvider _orderDbProvider;
        private readonly IProductDbProvider _productDbProvider;
        private readonly IMapper _mapper;
        private readonly double deliveryPrice = 1;

        public BuyerService(IOrderDbProvider orderDbProvider, IProductDbProvider productDbProvider, IMapper mapper)
        {
            _orderDbProvider = orderDbProvider;
            _productDbProvider = productDbProvider;
            _mapper = mapper;
        }

        public async Task AddOrderAsync(AddOrderDto addOrderDto, int buyerId)
        {
            if (addOrderDto.OrderItems.Count == 0)
            {
                throw new Exception("This order has no items.");
            }

            /*
            User? buyer = await _userDbProvider.FindUserByIdAsync(buyerId);
            if (buyer == null)
            {
                throw new Exception("User doesn't exist.");
            }
            if (buyer.UserKind != Models.Enums.EUserKind.BUYER)
            {
                throw new Exception("Only buyers can create order.");
            }
            */

            List<OrderItem> orderItems = _mapper.Map<List<OrderItem>>(addOrderDto.OrderItems);
            Order order = _mapper.Map<Order>(addOrderDto);
            order.OrderItems = orderItems;
            order.OrderPrice = 0;
            List<int> sellers = new List<int>();

            foreach (OrderItem item in orderItems)
            {
                Product? product = await _productDbProvider.GetProductByIdAsync(item.ProductId);
                if (product == null)
                {
                    throw new Exception("Product doesn't exist.");
                }

                if (product.Amount < item.Amount)
                {
                    throw new Exception("There are not enough products available.");
                }

                item.Name = product.Name;
                item.Price = product.Price;

                product.Amount -= item.Amount;

                await _productDbProvider.UpdateProduct(product);

                order.OrderPrice += item.Amount * item.Price;
                if (!sellers.Contains(product.SellerId))
                {
                    sellers.Add(product.SellerId);
                    order.OrderPrice += deliveryPrice;
                }
            }

            order.BuyerId = buyerId;

            order.OrderTime = DateTime.Now;
            Random random = new Random();
            order.DeliveryTime = DateTime.Now.AddHours(1).AddDays(2).AddMinutes(random.Next(0, 360));

            order.OrderStatus = EOrderStatus.NOT_CANCELED;

            await _orderDbProvider.AddOrderAsync(order);
        }

        public async Task CancelOrderAsync(int orderId, int buyerId)
        {
            Order order = await _orderDbProvider.FindOrderByIdAsync(orderId);

            if (order == null)
            {
                throw new Exception("That order doesn't exist.");
            }
            if (order.OrderItems == null)
            {
                throw new Exception("That order doesn't have any item.");
            }
            if (order.OrderStatus == EOrderStatus.CANCELED)
            {
                throw new Exception("That order has already been canceled.");
            }
            else if (order.DeliveryTime < DateTime.Now)
            {
                throw new Exception("That order has already been shipped.");
            }
            if (order.OrderTime.AddHours(1) < DateTime.Now)
            {
                throw new Exception("You can cancel one hour after ordering.");
            }

            foreach (OrderItem item in order.OrderItems)
            {
                Product? product = await _productDbProvider.GetProductByIdAsync(item.ProductId);
                if (product == null)
                {
                    throw new Exception("Product doesn't exist.");
                }

                product.Amount += item.Amount;

                await _productDbProvider.UpdateProduct(product);
            }

            order.OrderStatus = EOrderStatus.CANCELED;

            await _orderDbProvider.SaveChangesAsync();
        }

        public async Task<List<GetOrderDto>> GetOldOrdersAsync(int buyerId)
        {
            List<Order> orders = await _orderDbProvider.GetOldOrdersForBuyerAsync(buyerId);

            return _mapper.Map<List<GetOrderDto>>(orders);
        }

        public async Task<List<GetOrderDto>> GetNewOrdersAsync(int buyerId)
        {
            List<Order> orders = await _orderDbProvider.GetNewOrdersForBuyerAsync(buyerId);

            return _mapper.Map<List<GetOrderDto>>(orders);
        }

        public async Task<List<GetProductDto>> GetAllProductsAsync()
        {
            List<Product> products = await _productDbProvider.GetAllProductsAsync();

            return _mapper.Map<List<GetProductDto>>(products);
        }
    }
}
