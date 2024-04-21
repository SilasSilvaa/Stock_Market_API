using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.DTOs.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IUserPortfolioRepository
    {
        Task<List<StockDto>> GetUserPortfolioAsync(AppUser user);
        Task<List<Stock>> GetUserPortfolioQueryableAsync(QueryObject query);
        Task<StockPortifolio> CreateAsync(StockPortifolio stockPortifolio);
        Task<StockPortifolio> DeleteAsync(AppUser appUser, int id);
        Task<Stock?> GetStockDetailById(string userId, int id);
    }
}