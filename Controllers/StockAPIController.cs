using api.DTOs.Stock;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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