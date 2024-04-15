using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository(AplicationDBContext context) : IStockRepository
    {
        private readonly AplicationDBContext _context = context;

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stock.ToListAsync();
        }

        public async Task<Stock> GetByIdAsync(int id)
        {
            var stock = await _context.Stock.FindAsync(id) ?? throw new Exception("Stock Not Found");
            return stock;
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stock.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id );
            
            if(stock == null)
            {
                return null;
            }

            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStock)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if(stock == null)
            {
                return null;
            }

            stock.Symbol = updateStock.Symbol;
            stock.CompanyName = updateStock.CompanyName;
            stock.Industry = updateStock.Industry;
            stock.LastDiv = updateStock.LastDiv;
            stock.Purchase = updateStock.Purchase;
            stock.MarketCap = updateStock.MarketCap;

            await _context.SaveChangesAsync();

            return stock;
        }
    }
}