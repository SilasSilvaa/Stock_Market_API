using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Stocks")]
    public class StockDB
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string CompanyName { get; set;} = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Changes { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; }
        public long MarketCap { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public List<StockPortifolio> Portifolio { get; set; } = new List<StockPortifolio>();

    }
}