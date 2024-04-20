using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryApiObject
    {
        public bool OrderBySymbol { get; set; } = false;
        public bool OrderByName { get; set; } = false;
        public bool OrderByStockExchange { get; set; } = false; 
        public bool OrderByExchangeShortName { get; set; } = false; 
        [Required]
        public int PageNumber { get; set; } = 1;
        [Required]
        public int PageSize { get; set; } = 20;
    }
}