using AutoMapper;
using ProductsControl.DTO;
using ProductsControl.Infrastructure.IProviders;
using ProductsControl.Interfaces;
using ProductsControl.Models;

namespace ProductsControl.Services
{
    public class AdminService : IAdminService
    {
        private readonly IOrderDbProvider _orderDbProvider;
        private readonly IMapper _mapper;

        public AdminService(IOrderDbProvider orderDbProvider, IMapper mapper)
        {
            _orderDbProvider = orderDbProvider;
            _mapper = mapper;
        }

        public async Task<List<GetOrderDto>> GetAllOrdersAsync()
        {
            List<Order>? orders = await _orderDbProvider.GetAllOrdersAsync();

            return _mapper.Map<List<GetOrderDto>>(orders);
        }
    }
}
