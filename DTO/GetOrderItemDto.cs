using System.ComponentModel.DataAnnotations;

namespace ProductsControl.DTO
{
    public class GetOrderItemDto
    {
        public string? Name { get; set; }
        public double? Price { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int Amount { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int ProductId { get; set; }
    }
}
