using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Stock
{
    public class StockDetail
    {
        public string Image { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set;} = string.Empty;
        public decimal Price { get; set; }
        public decimal Changes { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Ceo { get; set; } = string.Empty;
    }
}