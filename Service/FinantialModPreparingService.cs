using api.Data;
using api.DTOs;
using api.DTOs.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Newtonsoft.Json;

namespace api.Service
{
    public class FinantialModPreparingService(HttpClient httpClient, IConfiguration configuration, AplicationDBContext context) : IFinantialModPreparingService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IConfiguration _configuration = configuration;
        private readonly AplicationDBContext _context = context;

        public async Task<Stock> FindByStockBySymbolAsync(string symbol)
        {
            var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_configuration["APIKey"]}");

            if(result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var task = JsonConvert.DeserializeObject<FMPStock[]>(content);
                
                if(task != null && task.Length > 0)
                {
                    var stock = task[0];
                    return stock.FromFMPToStock();
                }
                
                throw new JsonException("Error to deserialize json object");
            }
            else
            {
                throw new HttpRequestException("Error to request data");
            } 
        }

        public async Task<IQueryable<Stock>> GetDataUpdated(IQueryable<Stock> stocksDb)
        {
            try{
                foreach(Stock stockDb in stocksDb)
                {
                    var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{stockDb.Symbol}?apikey={_configuration["APIKey"]}");
                    
                    if(result.IsSuccessStatusCode)
                    {
                        var content = await result.Content.ReadAsStringAsync();
                        var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content) ?? throw new Exception("Error");
                        var currentStock = tasks[0];
                        var existingStock = stockDb.Symbol == currentStock.symbol;
                        
                        if(!existingStock)
                        {
                            stockDb.Name = currentStock.companyName;
                            stockDb.Price = (decimal?)currentStock.price ?? 0;
                            stockDb.StockExchange = currentStock.exchange;
                            stockDb.ExchangeShortName = currentStock.exchangeShortName;
                        }
                    }
                }
                
                await _context.SaveChangesAsync();                
                return stocksDb;              

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<StockDetail?> GetStockDetailById(string symbol)
        {
            var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_configuration["APIKey"]}");
            StockDetail stock = new StockDetail();
            
            if(result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);
                
                if(tasks != null && tasks.Length > 0)
                {
                    var currentStock = tasks[0];
                    
                    return currentStock.FromFMPToStockDetail();
                }
                    return null;
            }                
                throw new JsonException("Error to deserialize json object");
        }   
    }
}