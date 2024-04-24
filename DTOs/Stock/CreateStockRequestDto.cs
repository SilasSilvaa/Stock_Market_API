using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol length limited to 10 characters.")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        public string Image { get; set; } = string.Empty;
        [Required]
        [MaxLength(40, ErrorMessage = "Company Name length limited to 40 characters.")]
        public string CompanyName { get; set;} = string.Empty;
        [Required]
        [Range(1, 10000000000)]
        public decimal Price { get; set; }
        [Required]
        [Range(-100, 100)]
        public decimal Changes { get; set; }
        [Required]
        [Range(-100, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [Range(1, 50000000000)]
        public long MarketCap { get; set; }
        [Required]
        public string Currency { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        [MaxLength(20, ErrorMessage = "Industry length limited to 20 characters.")]
        public string Industry { get; set; } = string.Empty;
    }
}