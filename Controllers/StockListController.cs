using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    
    [Route("api/stocklist")]
    [ApiController]
    public class StockListController(IFinantialModPreparingService service) : ControllerBase
    {
        private readonly IFinantialModPreparingService _service = service;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllStocks([FromQuery] QueryApiObject query)
        {
            var stocks = await _service.GetAllStocks(query);

            return Ok(stocks);
            
        }
        
    }
}