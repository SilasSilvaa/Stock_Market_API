using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Stock
{
    public class CreateStockRequestDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public string Image {get; set; } = string.Empty;
        public string Name { get; set;} = string.Empty;
        public decimal Price { get; set; }
        public string StockExchange { get; set; } = string.Empty;
        public string ExchangeShortName { get; set; } = string.Empty;
    }
}