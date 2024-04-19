using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
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
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            Industry = stockModel.Industry,
            LastDiv = stockModel.LastDiv,
            MarketCap = stockModel.MarketCap,
            Purchase = stockModel.Purchase,
        };
     }
     public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto)
     {
        return new Stock
        {
            Symbol = stockDto.Symbol,
            CompanyName = stockDto.CompanyName,
            Industry = stockDto.Industry,
            LastDiv = stockDto.LastDiv,
            MarketCap = stockDto.MarketCap,
            Purchase = stockDto.Purchase,
        };
     }
    public static Stock ToStockFromFMP(this FMPStock fMPStock)
     {
        return new Stock
        {
            Symbol = fMPStock.symbol,
            CompanyName = fMPStock.companyName,
            Industry = fMPStock.industry,
            LastDiv =  (decimal)fMPStock.lastDiv,
            MarketCap = fMPStock.mktCap,
            Purchase = (decimal) fMPStock.price,
        };
     }

    }
}