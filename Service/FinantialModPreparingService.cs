using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api.DTOs;
using api.Interfaces;
using api.Mappers;
using api.Models;
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
                var content = await result.Content.ReadAsStringAsync();
        
            Console.WriteLine("Resultado ----> " + JsonConvert.DeserializeObject<FMPStock[]>(content));
            if(result.IsSuccessStatusCode)
            {
                // var content = await result.Content.ReadAsStringAsync();
                var contentDeserialize = JsonConvert.DeserializeObject<FMPStock[]>(content);
                var stock = contentDeserialize[0];

                if(stock != null)
                {
                    return stock.ToStockFromFMP();
                }
                return null;             
            }   
            return null;
         
         }  catch (Exception e) {
            Console.WriteLine(e);
            return null;
         } 
        }
    }
}