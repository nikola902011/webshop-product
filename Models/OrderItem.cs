using System.ComponentModel.DataAnnotations;

namespace ProductsControl.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public double Price { get; set; }

        [Required]
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
