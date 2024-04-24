using api.Models;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.Interfaces;
using api.Helpers;
using Microsoft.AspNetCore.Authorization;
using api.Data;
using Microsoft.AspNetCore.Identity;
using api.DTOs.Stock;
using api.Service;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockDBController(AplicationDBContext context, IStockDBRepository repository, UserManager<AppUser> manager) : ControllerBase
    {
        // private readonly IFinantialModPreparingService _service = service;
        private readonly IStockDBRepository _repository = repository;
        private readonly AplicationDBContext _context = context;
        private readonly UserManager<AppUser> _manager = manager;


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            List<StockDB> stocks = await _repository.GetAllAsync(query);

            return Ok(stocks);
            
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            StockDB? stock = await _repository.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }
        
        [HttpGet("symbol")]
        [Authorize]
        public async Task<IActionResult> GetBySymbol([FromQuery] SearchType typeSearch, string symbol)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            StockDB? stock = await _repository.GetBySymbolAsync("asda");

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stock)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = stock.CreateStockDTO();
            await _repository.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDTO());
        }
        
        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockModel = await _repository.UpdateAsync(id, updateDto);

            if (stockModel == null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _repository.DeleteAsync(id);

            if (stockModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    
    }
}