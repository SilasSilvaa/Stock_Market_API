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
     public static StockDto ToStockDTO(this Stock stockModel)
     {
        return new StockDto
        {
            Id = stockModel.Id,
            Image = stockModel.Image ?? "",
            Symbol = stockModel.Symbol,
            Name = stockModel.Name,
            Price = stockModel.Price ?? 0,
            ExchangeShortName = stockModel.ExchangeShortName ?? "",
            StockExchange = stockModel.StockExchange ?? "",
        };
     }
     public static Stock FromFMPToStock(this FMPStock stockModel)
     {
        return new Stock
        {
            Image = stockModel.image,
            Symbol = stockModel.symbol,
            Name = stockModel.companyName,
            Price = (decimal?)stockModel.price ?? 0,
            ExchangeShortName = stockModel.exchangeShortName,
            StockExchange = stockModel.exchange,
        };
     }
     public static Stock CreateRequestNewStock(this CreateStockRequestDto stockModel)
     {
        return new Stock
        {
            Image = stockModel.Image,
            Symbol = stockModel.Symbol,
            Name = stockModel.Name,
            Price = (decimal)stockModel.Price,
            ExchangeShortName = stockModel.ExchangeShortName,
            StockExchange = stockModel.StockExchange,
            UserId = stockModel.UserId,
        };
     }
     public static StockDto ToPortfolioDTO(this Stock stockModel)
     {
        return new StockDto
        {
            Id = stockModel.Id,
            Image = stockModel.Image ?? "",
            Symbol = stockModel.Symbol,
            Name = stockModel.Name,
            Price = stockModel.Price ?? 0,
            ExchangeShortName = stockModel.ExchangeShortName ?? "",
            StockExchange = stockModel.StockExchange ?? "",
        };
     }

     public static StockDetail FromFMPToStockDetail(this FMPStock stock)
     {
        return new StockDetail 
        {
            Image = stock.image,
            Ceo = stock.ceo,
            Changes = (decimal)stock.changes,
            CompanyName = stock.companyName,
            Description = stock.description,
            Industry = stock.industry,
            MarketCap = stock.mktCap,
            Price = (decimal?)stock.price ?? 0,
            Symbol = stock.symbol,
        };
     } 
    }
}