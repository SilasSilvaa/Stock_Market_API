using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository(AplicationDBContext context, IFinantialModPreparingService service, UserManager<AppUser> manager) : IStockRepository
    {
        private readonly AplicationDBContext _context = context;
        private readonly  UserManager<AppUser> _manager = manager;
        private readonly IFinantialModPreparingService _service = service;

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            try
            {
                var stocks = _context.Stock.AsQueryable();
                var user = await _manager.FindByEmailAsync(query.Email) ?? throw new HttpRequestException("Error when searching for a user by email");
                await _service.GetDataUpdated(stocks);

                stocks = stocks.Where(x => x.UserId == user.Id);

                if(query.OrderByName) stocks = stocks.OrderBy(s => s.Name);
                if(query.OrderByPrice) stocks = stocks.OrderBy(s => s.Price);
                if(query.OrderByExchangeShortName) stocks = stocks.OrderBy(s => s.ExchangeShortName);
                if(query.OrderByStockExchange) stocks = stocks.OrderBy(s => s.StockExchange);
                if(query.OrderBySymbol) stocks = stocks.OrderBy(s => s.Symbol);
                
                var skipNumber = (query.PageNumber -1 ) * query.PageSize;
                return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<Stock> GetByIdAsync(int id)
        {
            return await _context.Stock.FindAsync(id) ?? throw new Exception("Stock Not Found");
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stock.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(string userId, int id)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id );
            
            if(stock == null)
            {
                return null;
            }

            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> GetBySymbol(string symbol)
        {
            if(symbol == null)
            {
                return null;
            }
            return await _context.Stock.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }

        public async Task<Stock?> GetStockDetailById(string userId, int id)
        {
            var result = await _context.Stock.Where(x => x.UserId == userId && x.Id == id)
            .FirstOrDefaultAsync();
            
            if(result != null) return result;

            return null;
        }
    }
}