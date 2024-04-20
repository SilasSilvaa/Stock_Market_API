using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Stock
{
    public class ListStockDto
    {
        public string Symbol { get; set; } = string.Empty;
        public string Name { get; set;} = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string StockExchange { get; set; } = string.Empty;
        public string ExchangeShortName { get; set; } = string.Empty;
    }
}