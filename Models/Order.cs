using ProductsControl.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProductsControl.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string? DeliveryAddress { get; set; }
        [Required]
        public DateTime OrderTime { get; set; }
        [Required]
        public DateTime DeliveryTime { get; set; }
        public string? Comment { get; set; }
        [Required]
        public double? OrderPrice { get; set; }
        [Required]
        public EOrderStatus OrderStatus { get; set; }

        [Required]
        public int BuyerId { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}
