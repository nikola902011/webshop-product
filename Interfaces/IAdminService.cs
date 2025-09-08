using ProductsControl.DTO;

namespace ProductsControl.Interfaces
{
    public interface IAdminService
    {
        Task<List<GetOrderDto>> GetAllOrdersAsync();
    }
}
