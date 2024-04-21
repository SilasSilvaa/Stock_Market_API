using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Stocks")]
    public class Stock
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
        public string Name { get; set;} = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Price { get; set; }
        public string? StockExchange { get; set; } = string.Empty;
        public string? ExchangeShortName { get; set; } = string.Empty;        
        public List<StockPortifolio> Portifolio { get; set; } = new List<StockPortifolio>();

    }
}