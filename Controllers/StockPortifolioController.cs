using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Account;
using api.Interfaces;
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
        IUserPortifolioRepository portifolioRepositoy,
        IFinantialModPreparingService finantialMod
        ) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IStockRepository _repository = repository;
        private readonly IUserPortifolioRepository _portifolioRepositoy = portifolioRepositoy;
        private readonly IFinantialModPreparingService _finantialMod = finantialMod;
    
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortifolio([FromQuery] UserPortifolioDto userPortifolioDto)
        {
            var appUser = await _userManager.FindByEmailAsync(userPortifolioDto.Email);
            var userPortifolio = await _portifolioRepositoy.GetUserPortifolio(appUser);
            
            return Ok(userPortifolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddUserPortifolio([FromQuery] UserPortifolioDto userPortifolioDto, string symbol)
        {
            var appUser = await _userManager.FindByEmailAsync(userPortifolioDto.Email);
            var stock = await _repository.GetBySymbolAsync(symbol);

            if (stock == null)
            {
                stock = await _finantialMod.FindByStockBySymbolAsync(symbol);
                if (stock == null)
                {
                    return BadRequest("Stock does not exists");
                }
                else
                {
                    await _repository.CreateAsync(stock);
                }
            }
            
            var userPortifolio = await _portifolioRepositoy.GetUserPortifolio(appUser);
            if(userPortifolio.Any(s => s.Symbol.Equals(symbol, StringComparison.CurrentCultureIgnoreCase))) 
            {
                return BadRequest("Cannot add same stock to portifolio");
            }

            var stockPortifolio = new StockPortifolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id
            };  

            await _portifolioRepositoy.CreateAsync(stockPortifolio);

            if(stockPortifolio == null)
            {
                return StatusCode(500, "Error to create");
            } else 
            {
                return Created();
            }

        }
 
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteStockPortifolio([FromQuery] UserPortifolioDto userPortifolioDto, string symbol)
        {
            var appUser = await _userManager.FindByEmailAsync(userPortifolioDto.Email);
            var userPortifolio = await _portifolioRepositoy.GetUserPortifolio(appUser);

            if(userPortifolio == null)
            {
                return BadRequest("User portifolio not found");
            }   

            var stock =  userPortifolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

            if(stock.Count() == 1)
            {
                await _portifolioRepositoy.DeleteAsync(appUser, symbol);
            } 
            else
            {
                return BadRequest("Stock is not in you stock portifolio");
            }

            return Ok();
        }
    }
}