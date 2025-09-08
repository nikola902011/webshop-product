using System.ComponentModel.DataAnnotations;

namespace ProductsControl.DTO
{
    public class AddOrderItemDto
    {
        [Required, Range(0, int.MaxValue)]
        public int ProductId { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int Amount { get; set; }
    }
}
