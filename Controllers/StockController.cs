using api.Models;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.DTOs.Stock;
using api.Interfaces;
using api.Helpers;
using Microsoft.AspNetCore.Authorization;
using api.Service;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController(AplicationDBContext context, IStockRepository repository, IFinantialModPreparingService service, UserManager<AppUser> manager) : ControllerBase
    {
        private readonly IFinantialModPreparingService _service = service;
        private readonly IStockRepository _repository = repository;
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
            var stocks= await _repository.GetAllAsync(query);
            var stocksDto = stocks.Select(s => s.ToStockDTO()).ToList();

            return Ok(stocksDto);
            
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] [Required]int id, [Required]string email)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _manager.FindByEmailAsync(email);
                if(user == null) return BadRequest("Erro to find user");

                var stockDb = await _repository.GetStockDetailById(user.Id, id);
                if(stockDb == null) return BadRequest("Erro to find stock");

                Stock stock = await _repository.GetByIdAsync(id);            
                return Ok(stock.ToStockDTO());

            }catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromQuery] string symbol, [Required]string userEmail)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _manager.FindByEmailAsync(userEmail);
            if(user == null ) return BadRequest("Error when searching for a user by email");

            var stockDb = await _service.FindByStockBySymbolAsync(symbol);
            var checkStock = await _context.Stock.FirstOrDefaultAsync(x => x.Symbol == symbol && x.UserId == user.Id);

            if(checkStock != null)
            {
                return BadRequest("Error saving stock to history, this stock already exists.");
            }

            var stock = new CreateStockRequestDto {
                Image = stockDb.Image ?? "",
                Name = stockDb.Name,
                ExchangeShortName = stockDb.ExchangeShortName ?? "",
                Price = stockDb.Price ?? 0,
                StockExchange = stockDb.ExchangeShortName ?? "",
                Symbol = stockDb.Symbol,
                UserId = user.Id ,
            };
        
            await _repository.CreateAsync(stock.CreateRequestNewStock());

            return Created();
        }    


        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] [Required]int id, [Required] string email)
        {

            var appUser = await _manager.FindByEmailAsync(email);
            
            if(appUser == null) return BadRequest("Error when searching for a user by email");
            var stock = await _repository.DeleteAsync(appUser.Id, id);

            if(stock == null)
            {
                return NotFound();
            } 

            return NoContent();
        }
    
    }
}