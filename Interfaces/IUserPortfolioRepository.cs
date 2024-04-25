using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IUserPortfolioRepository
    {
        Task<List<StockDB>> GetUserPortfolioAsync(AppUser user);
        Task<List<StockDB>> GetUserPortfolioWtihQueryAsync(AppUser user, QueryObject query);
        Task<StockPortfolio> CreateAsync(StockPortfolio stockPortfolio);
        Task<StockPortfolio> DeleteAsync(AppUser appUser, int id);
        Task<StockDB?> GetStockById(string userId, int id);
    }
}