using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task <Stock?> GetStockDetailById(string userId, int id);
        Task<Stock> GetByIdAsync(int id);
        Task<Stock?> GetBySymbol(string symbol);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> DeleteAsync(string userId, int id);
    }
}