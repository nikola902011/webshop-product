using System.ComponentModel.DataAnnotations;

namespace ProductsControl.DTO
{
    public class GetProductDto
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string? Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required, MaxLength(200)]
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int SellerId { get; set; }
    }
}
