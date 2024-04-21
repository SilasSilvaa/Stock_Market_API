using api.DTOs.Stock;
using api.Models;

namespace api.Interfaces
{
    public interface IFinantialModPreparingService
    {
        Task<Stock> FindByStockBySymbolAsync(string symbol);
        Task<IQueryable<Stock>> GetDataUpdated(IQueryable<Stock> stocksDb);
        Task<StockDetail?> GetStockDetailById(string symbol);
    }
}