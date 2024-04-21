using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        public bool OrderBySymbol { get; set; } = false;
        public bool OrderByName { get; set; } = false;
        public bool OrderByPrice { get; set; } = false;
        public bool OrderByStockExchange { get; set; } = false;
        public bool OrderByExchangeShortName { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

    }
}