using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.DTOs.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Controllers
{
    [Route("api/stockbysymbol")]
    [ApiController]
    public class StockAPIController(IFinantialModPreparingService service) : ControllerBase
    {

    private readonly IFinantialModPreparingService _service = service;

     [HttpGet("{symbol}")]
     [Authorize]
     public async Task<IActionResult> GetStockBySymbol([FromRoute] string symbol)
     {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        };

        try
        {
            GetStockDto? stock = await _service.FindStockBySymbolAsync(symbol);
            if(stock  == null) 
            {
                return BadRequest("Stock not found");
            }
            return Ok(stock);
        }
        catch (Exception)
        {
            throw;
        }

     }
    }
}