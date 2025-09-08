using ProductsControl.Models.Enums;

namespace ProductsControl.DTO
{
    public class GetOrderDto
    {
        public int Id { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? Comment { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public double? OrderPrice { get; set; }
        public EOrderStatus OrderStatus { get; set; }
        public List<GetOrderItemDto>? OrderItems { get; set; }
    }
}
