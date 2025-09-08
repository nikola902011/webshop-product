using System.ComponentModel.DataAnnotations;

namespace ProductsControl.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string? Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required, MaxLength(200)]
        public string? Description { get; set; }
        [Required]
        public byte[]? Image { get; set; }

        [Required]
        public int SellerId { get; set; }
    }
}
