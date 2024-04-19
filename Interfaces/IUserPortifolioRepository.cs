using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IUserPortifolioRepository
    {
        Task<List<Stock>> GetUserPortifolio(AppUser user);
        Task<StockPortifolio> CreateAsync(StockPortifolio stockPortifolio);
        Task<StockPortifolio> DeleteAsync(AppUser appUser, string symbol);
    }
}