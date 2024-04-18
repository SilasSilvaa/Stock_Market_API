using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class UserPortifolioReposiroty(AplicationDBContext context) : IUserPortifolioRepository
    {
        private readonly AplicationDBContext _context = context;
        public async Task<List<Stock>> GetUserPortifolio(AppUser user)
        {
            return await _context.StockPortifolios.Where(x => x.AppUserId == user.Id)
            .Select(s => new Stock 
            {
                Id = s.Stock.Id,
                Symbol = s.Stock.Symbol,
                CompanyName = s.Stock.CompanyName,
                Purchase = s.Stock.Purchase,
                LastDiv = s.Stock.LastDiv,
                Industry = s.Stock.Industry,
                MarketCap = s.Stock.MarketCap
            }).ToListAsync();
        }
    }
}