using System.ComponentModel.DataAnnotations;

namespace ProductsControl.DTO
{
    public class AddOrderDto
    {
        [Required, MaxLength(100)]
        public string? DeliveryAddress { get; set; }
        public string? Comment { get; set; }
        public List<AddOrderItemDto>? OrderItems { get; set; }
    }
}
