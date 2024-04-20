using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api.DTOs;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

namespace api.Service
{
    public class FinantialModPreparingService(HttpClient httpClient, IConfiguration configuration) : IFinantialModPreparingService
    {
        private HttpClient _httpClient = httpClient;
        private IConfiguration _configuration = configuration;

        public async Task<Stock> FindByStockBySymbolAsync(string symbol)
        {
         try{
            var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_configuration["APIKey"]}");
        
            if(result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var contentDeserialize = JsonConvert.DeserializeObject<FMPStock[]>(content);
                
                if(contentDeserialize != null && contentDeserialize.Length > 0){

                var stock = contentDeserialize[0];

                if(stock != null)
                {
                    return stock.ToStockFromFMP();
                }
                    throw new JsonException("Error to deserialize json");             
                }

            }
            throw new HttpRequestException("Error to request data");
         
         }  catch (Exception) {
            throw new Exception();
         } 
        }

        public async Task<List<StockList>> GetAllStocks(QueryApiObject query)
        {
            try
            {
                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/stock/list?apikey={_configuration["APIKey"]}");
                
                if(result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var simpleStockList = JsonConvert.DeserializeObject<List<StockList>>(content) ?? throw new Exception("Error");

                    var skipNumber = (query.PageNumber -1 ) * query.PageSize;
                    List<StockList> stockList = simpleStockList.ToSimpleStockList().Skip(skipNumber).Take(query.PageSize).ToList();
                    
                    return stockList;                    
                }
                else
                {
                    throw new Exception("Erro to get data");
                }   

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

    }
}