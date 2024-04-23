using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.DTOs.Account;
using api.DTOs.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
    
    public static StockDB CreateStockDTO(this CreateStockRequestDto stockModel)
    {
        return new StockDB 
        {
            Symbol = stockModel.Symbol,
            Image =  stockModel.Image,
            CompanyName =  stockModel.CompanyName,
            Price =  stockModel.Price,
            Changes =  stockModel.Changes,
            LastDiv =  stockModel.LastDiv,
            MarketCap =  stockModel.MarketCap,
            Currency = stockModel.Currency,
            Description = stockModel.Description,
            Industry = stockModel.Industry
        };
    }
    public static StockDto ToStockDTO(this StockDB stockModel)
    {
        return new StockDto 
        {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            Image =  stockModel.Image,
            CompanyName =  stockModel.CompanyName,
            Price =  stockModel.Price,
            Changes =  stockModel.Changes,
            LastDiv =  stockModel.LastDiv,
            MarketCap =  stockModel.MarketCap,
            Currency = stockModel.Currency,
            Description = stockModel.Description,
            Industry = stockModel.Industry
        };
    }
    public static StockDto UpdateStockRequestDTO(this StockDB stockModel)
    {
        return new StockDto 
        {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            Image =  stockModel.Image,
            CompanyName =  stockModel.CompanyName,
            Price =  stockModel.Price,
            Changes =  stockModel.Changes,
            LastDiv =  stockModel.LastDiv,
            MarketCap =  stockModel.MarketCap,
            Currency = stockModel.Currency,
            Description = stockModel.Description,
            Industry = stockModel.Industry
        };
    }
    public static GetStockDto FromFMPToGetStock(this FMPStockDto stockModel)
    {
        return new GetStockDto
        {
            Symbol = stockModel.symbol,
            Image =  stockModel.image,
            CompanyName =  stockModel.companyName,
            Price =  (decimal?)stockModel.price ?? 0,
            Changes =  (decimal?)stockModel.changes ?? 0,
            LastDiv =  (decimal?)stockModel.lastDiv ?? 0,
            MarketCap =  stockModel.mktCap,
            Currency = stockModel.currency,
            Description = stockModel.description,
            Industry = stockModel.industry
        };
    }
    public static StockDB FromGetStockToStockDB(this GetStockDto stockModel)
    {
        return new StockDB
        {
            Symbol = stockModel.Symbol,
            Image =  stockModel.Image,
            CompanyName =  stockModel.CompanyName,
            Price =  (decimal?)stockModel.Price ?? 0,
            Changes =  (decimal?)stockModel.Changes ?? 0,
            LastDiv =  (decimal?)stockModel.LastDiv ?? 0,
            MarketCap =  stockModel.MarketCap,
            Currency = stockModel.Currency,
            Description = stockModel.Description,
            Industry = stockModel.Industry
        };
    }
    }
}