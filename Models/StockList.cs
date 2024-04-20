
namespace api.Models
{
 
    public class StockList
    {
        public string Symbol { get; set; } = string.Empty;
        public string Name { get; set;} = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string StockExchange { get; set; } = string.Empty;
        public string ExchangeShortName { get; set; } = string.Empty;
    }
}