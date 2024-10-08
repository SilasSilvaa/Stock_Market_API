
namespace api.DTOs.Stock
{
    public class GetStockDto
    {
        public string Symbol { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string CompanyName { get; set;} = string.Empty;
        public decimal Price { get; set; }
        public decimal Changes { get; set; }
        public decimal LastDiv { get; set; }
        public long MarketCap { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
    }
}