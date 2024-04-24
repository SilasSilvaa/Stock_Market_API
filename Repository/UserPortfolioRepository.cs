using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.DTOs.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class UserPortfolioRepository(AplicationDBContext context, IFinantialModPreparingService service) : IUserPortfolioRepository
    {
        private readonly AplicationDBContext _context = context;
        private readonly IFinantialModPreparingService _service = service;

        public async Task<List<StockDB>> GetUserPortfolioAsync(AppUser user)
        {
            var userPortifolio = await _context.StockPortifolios.Where(x => x.AppUserId == user.Id)
            .Select(s => new StockDB 
            {
                Id = s.Stock.Id,
                Image = s.Stock.Image,
                Symbol = s.Stock.Symbol,
                CompanyName = s.Stock.CompanyName,
                Price = s.Stock.Price,
                Changes = s.Stock.Changes,
                Currency = s.Stock.Currency,
                Description = s.Stock.Description,
                Industry = s.Stock.Industry,
                LastDiv = s.Stock.LastDiv,
                MarketCap = s.Stock.MarketCap,
            }).ToListAsync();

            return  userPortifolio;
        }

        public async Task<List<StockDB>> GetUserPortfolioWtihQueryAsync(AppUser user, QueryObject query)
        {
            var userPortifolio = _context.StockPortifolios.Where(x => x.AppUserId == user.Id)
            .Select(s => new StockDB 
            {
                Id = s.Stock.Id,
                Image = s.Stock.Image,
                Symbol = s.Stock.Symbol,
                CompanyName = s.Stock.CompanyName,
                Price = s.Stock.Price,
                Changes = s.Stock.Changes,
                Currency = s.Stock.Currency,
                Description = s.Stock.Description,
                Industry = s.Stock.Industry,
                LastDiv = s.Stock.LastDiv,
                MarketCap = s.Stock.MarketCap,
            }).AsQueryable();

                if(query.OrderByName) userPortifolio = userPortifolio.OrderBy(s => s.CompanyName);
                if(query.OrderByPrice) userPortifolio = userPortifolio.OrderBy(s => s.Price);
                if(query.OrderBySymbol) userPortifolio = userPortifolio.OrderBy(s => s.Symbol);

            var skipNumber = (query.PageNumber -1 ) * query.PageSize;
            return await userPortifolio.Skip(skipNumber).Take(query.PageSize).ToListAsync();;
        }


        public async Task<StockPortifolio> CreateAsync(StockPortifolio stockPortifolio)
        {
            await _context.StockPortifolios.AddAsync(stockPortifolio);
            await _context.SaveChangesAsync();

            return stockPortifolio;
        }

        public async Task<StockPortifolio> DeleteAsync(AppUser appUser, int id)
        {
            var userPortifolio = await _context.StockPortifolios.FirstOrDefaultAsync(
            x => x.AppUserId  == appUser.Id && x.StockId == id
            ) ?? throw new Exception("Stock not found Cannot be delete stock");

            _context.StockPortifolios.Remove(userPortifolio);
            await _context.SaveChangesAsync();

            return userPortifolio;
        }
        
        public async Task<StockDB?> GetStockById( string userId, int id)
        {
            var result = await _context.StockPortifolios.Where(x => x.AppUserId == userId && x.Stock.Id == id)
            .Select(x => x.Stock)
            .FirstOrDefaultAsync();

            if(result != null) return result;
            return null;
        }

    }
}