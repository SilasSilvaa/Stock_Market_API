using api.Data;
using api.DTOs.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockDBRepository( AplicationDBContext context ) : IStockDBRepository
    {
        private readonly AplicationDBContext _context = context;

        public async Task<List<StockDB>> GetAllAsync(QueryObject query)
        {
            try
            {
                var stocks = _context.Stock.AsQueryable();
                
                if(query.OrderByName) stocks = stocks.OrderBy(s => s.CompanyName);
                if(query.OrderByPrice) stocks = stocks.OrderBy(s => s.Price);
                if(query.OrderBySymbol) stocks = stocks.OrderBy(s => s.Symbol);
                
                var skipNumber = (query.PageNumber -1 ) * query.PageSize;
                return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<StockDB?> GetByIdAsync(int id)
        {
            return await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<StockDB> CreateAsync(StockDB stock)
        {
            await _context.Stock.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<StockDB?> UpdateAsync(int id, UpdateStockRequestDto updateStock)
        {
             var existingStock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if (existingStock == null)
            {
                return null;
            }
            
            existingStock.Symbol = updateStock.Symbol;
            existingStock.CompanyName = updateStock.CompanyName;
            existingStock.Price = updateStock.Price;
            existingStock.LastDiv = updateStock.LastDiv;
            existingStock.Industry = updateStock.Industry;
            existingStock.MarketCap = updateStock.MarketCap;
            existingStock.Changes = updateStock.Changes;
            existingStock.Currency = updateStock.Currency;
            existingStock.Description = updateStock.Description;
            existingStock.Image = updateStock.Image;

            await _context.SaveChangesAsync();

            return existingStock;
        }

        public async Task<StockDB> DeleteAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel == null)
            {
                return null;
            }

            _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<StockDB?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stock.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }
    }
}