using api.Data;
using api.DTOs;
using api.DTOs.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Newtonsoft.Json;

namespace api.Service
{
    public class FinantialModPreparingService(HttpClient httpClient, IConfiguration configuration) : IFinantialModPreparingService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IConfiguration _configuration = configuration;
        public async Task<GetStockDto?> FindStockBySymbolAsync(string symbol)
        {
            var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_configuration["APIKey"]}");

            if(result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var task = JsonConvert.DeserializeObject<FMPStockDto[]>(content);
                
                if(task != null && task.Length > 0)
                {
                    var stock = task[0];
                    return stock.FromFMPToGetStock();
                }
                
                throw new Exception("Error when searching for an stock");
            }
            else
            {
                throw new HttpRequestException("Error to request data");
            } 
        }

    }
}