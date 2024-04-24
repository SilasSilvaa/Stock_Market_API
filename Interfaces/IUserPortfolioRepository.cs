using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IUserPortfolioRepository
    {
        Task<List<StockDB>> GetUserPortfolioAsync(AppUser user);
        Task<List<StockDB>> GetUserPortfolioWtihQueryAsync(AppUser user, QueryObject query);
        Task<StockPortifolio> CreateAsync(StockPortifolio stockPortifolio);
        Task<StockPortifolio> DeleteAsync(AppUser appUser, int id);
        Task<StockDB?> GetStockById(string userId, int id);
    }
}