using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Account;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stockportifolio")]
    [ApiController]
    public class StockPortifolioController(
        UserManager<AppUser> userManager, 
        IStockRepository repository, 
        IUserPortfolioRepository portfolioRepo,
        IFinantialModPreparingService service
        ) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IStockRepository _repository = repository;
        private readonly IUserPortfolioRepository _portfolioRepo = portfolioRepo;
        private readonly IFinantialModPreparingService _service = service;
    
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortifolio([FromQuery] QueryObject query)
        {
            var appUser = await _userManager.FindByEmailAsync(query.Email);
            if(appUser != null)
            {
                var userPortifolio = await _portfolioRepo.GetUserPortfolioQueryableAsync(query);
                return Ok(userPortifolio);
            }
            
            return BadRequest("User not found");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddUserPortifolio([FromQuery] [Required]string email, [Required]string symbol)
        {
            var appUser = await _userManager.FindByEmailAsync(email);
            var stock = await _repository.GetBySymbol(symbol);

            if (stock == null)
            {
                stock = await _service.FindByStockBySymbolAsync(symbol);
                if (stock == null)
                {
                    return BadRequest("Stock does not exists");
                }
                else
                {
                    await _repository.CreateAsync(stock);
                }
            }
            if(appUser == null) return BadRequest("Error when searching for a user by email");
            var userPortifolio = await _portfolioRepo.GetUserPortfolioAsync(appUser);

            if(userPortifolio.Any(s => s.Symbol.Equals(symbol, StringComparison.CurrentCultureIgnoreCase))) 
            {
                return BadRequest("Cannot add same stock to portifolio");
            }

            var stockPortifolio = new StockPortifolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id
            };  

            await _portfolioRepo.CreateAsync(stockPortifolio);

            if(stockPortifolio == null)
            {
                return StatusCode(500, "Error to create");
            } else 
            {
                return Created();
            }

        }
        [HttpGet("id")]
        [Authorize]
        public async Task<IActionResult> GetStockByIdAsync([FromQuery] [Required]int id, [Required] string email)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if(user == null) return BadRequest("Erro to find user");

                var stockDb = await _portfolioRepo.GetStockDetailById(user.Id, id);
                if(stockDb == null) return BadRequest("Erro to find stock");
                
                var stockDetail = await _service.GetStockDetailById(stockDb.Symbol);

                return Ok(stockDetail);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
 
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteStockPortifolio([FromQuery] [Required]string email, int id)
        {
            var appUser = await _userManager.FindByEmailAsync(email);
            
            if(appUser == null) return BadRequest("Error when searching for a user by email");
            var userPortifolio = await _portfolioRepo.GetUserPortfolioAsync(appUser);

            if(userPortifolio == null)
            {
                return BadRequest("User portifolio not found");
            }   

            var stock =  userPortifolio.Where(s => s.Id == id).ToList();
            var currentStock = stock[0];

            if(stock.Count == 1)
            {
                await _portfolioRepo.DeleteAsync(appUser, currentStock.Id);
            } 
            else
            {
                return BadRequest("Stock is not in you stock portifolio");
            }

            return Ok();
        }
    }
}