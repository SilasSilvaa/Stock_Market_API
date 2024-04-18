using api.Models;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.DTOs.Stock;
using api.Interfaces;
using api.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController(IStockRepository repository) : ControllerBase
    {
        // private readonly AplicationDBContext _context = context;

        private readonly IStockRepository _repository = repository;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stocks= await _repository.GetAllAsync(query);
            var stocksDto = stocks.Select(s => s.ToStockDTO()).ToList();

            return Ok(stocksDto);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try{
            Stock stock = await _repository.GetByIdAsync(id);            
            return Ok(stock.ToStockDTO());

            }catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockModel = stockDto.ToStockFromCreateDTO();
            await _repository.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id }, stockModel.ToStockDTO());
        }    

        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStock)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _repository.UpdateAsync(id, updateStock);

            if(stockModel == null)  
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
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _repository.DeleteAsync(id);

            if(stock == null)
            {
                return NotFound();
            } 

            return NoContent();
        }
    }
}