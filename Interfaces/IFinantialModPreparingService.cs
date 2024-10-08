using api.DTOs.Stock;

namespace api.Interfaces
{
    public interface IFinantialModPreparingService
    {
        Task<GetStockDto?> FindStockBySymbolAsync(string symbol);
    }
}