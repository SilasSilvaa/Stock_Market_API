using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IFinantialModPreparingService
    {
        Task<Stock> FindByStockBySymbolAsync(string symbol);
        Task<List<StockList>> GetAllStocks(QueryApiObject query);
    }
}