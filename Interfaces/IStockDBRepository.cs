using api.DTOs.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IStockDBRepository
    {
        Task<List<StockDB>> GetAllAsync(QueryObject query);
        Task<StockDB?> GetByIdAsync(int id);
        Task<StockDB?> GetBySymbolAsync(string symbol);
        Task<StockDB> CreateAsync(StockDB stock);
        Task<StockDB?> UpdateAsync(int id, UpdateStockRequestDto updateStock);
        Task<StockDB> DeleteAsync(int id);
    }
}