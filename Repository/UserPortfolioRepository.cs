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

        public async Task<List<Stock>> GetUserPortfolioQueryableAsync(QueryObject query)
        {
            ArgumentNullException.ThrowIfNull(query);
                    
            var userPortifolio = _context.StockPortifolios.Where(x => x.AppUser != null && x.AppUser.Email == query.Email)
            .Select(s => new Stock 
            {
                Id = s.Stock.Id,
                Image = s.Stock.Image,
                Symbol = s.Stock.Symbol,
                Name = s.Stock.Name,
                ExchangeShortName = s.Stock.ExchangeShortName,
                Price = s.Stock.Price,
                StockExchange = s.Stock.StockExchange
            }).AsQueryable();
            await _service.GetDataUpdated(userPortifolio);
            
            if(query.OrderByName) userPortifolio = userPortifolio.OrderBy(x => x.Name);
            if(query.OrderByPrice) userPortifolio = userPortifolio.OrderBy(s => s.Price);
            if(query.OrderByExchangeShortName) userPortifolio = userPortifolio.OrderBy(s => s.ExchangeShortName);
            if(query.OrderByStockExchange) userPortifolio = userPortifolio.OrderBy(s => s.StockExchange);
            if(query.OrderBySymbol) userPortifolio = userPortifolio.OrderBy(s => s.Symbol);

            var skipNumber = (query.PageNumber -1 ) * query.PageSize;
            return await userPortifolio.Skip(skipNumber).Take(query.PageSize).ToListAsync();
            
        
        }

        public async Task<List<StockDto>> GetUserPortfolioAsync(AppUser user)
        {
            var userPortifolio = await _context.StockPortifolios.Where(x => x.AppUserId == user.Id)
            .Select(s => new Stock 
            {
                Id = s.Stock.Id,
                Image = s.Stock.Image,
                Symbol = s.Stock.Symbol,
                Name = s.Stock.Name,
                ExchangeShortName = s.Stock.ExchangeShortName,
                Price = s.Stock.Price,
                StockExchange = s.Stock.StockExchange
            }.ToPortfolioDTO()).ToListAsync();
            
            return userPortifolio;
        }

        public async Task<Stock?> GetStockDetailById( string userId, int id)
        {
            var result = await _context.StockPortifolios.Where(x => x.AppUserId == userId && x.Stock.Id == id)
            .Select(x => x.Stock)
            .FirstOrDefaultAsync();
            
            if(result != null) return result;

            return null;
        }
    }
}