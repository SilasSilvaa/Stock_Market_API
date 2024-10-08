using System.ComponentModel.DataAnnotations;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stockportfolio")]
    [ApiController]
    public class StockPortifolioController(
        UserManager<AppUser> userManager, 
        IStockDBRepository repository, 
        IUserPortfolioRepository portfolioRepo,
        IFinantialModPreparingService service
        ) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IStockDBRepository _repository = repository;
        private readonly IUserPortfolioRepository _portfolioRepo = portfolioRepo;
        private readonly IFinantialModPreparingService _service = service;
    
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortifolio([FromQuery] QueryObject query, [Required]string email)
        {
            var appUser = await _userManager.FindByEmailAsync(email);
            if(appUser != null)
            {
                var userPortifolio = await _portfolioRepo.GetUserPortfolioWtihQueryAsync(appUser, query);
                return Ok(userPortifolio);
            }
            
            return BadRequest("User not found");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio([FromBody] AddPortfolio portfolio)
        {
            
            var appUser = await _userManager.FindByEmailAsync(portfolio.Email);
            if(appUser == null) return BadRequest("User not found.");

            var stock = await _repository.GetBySymbolAsync(portfolio.Symbol);
            
            if (stock == null)
            {   
                var findStock = await _service.FindStockBySymbolAsync(portfolio.Symbol);
                if(findStock == null ) 
                {
                    return BadRequest("Stock does not exists");
                }

                stock = findStock.FromGetStockToStockDB();
                await _repository.CreateAsync(stock);
            }
            

            if (stock == null) return BadRequest("Stock not found");

            var userPortfolio = await _portfolioRepo.GetUserPortfolioAsync(appUser);

            if (userPortfolio.Any(e => e.Symbol.ToLower() == portfolio.Symbol.ToLower())) return BadRequest("Cannot add same stock to portfolio");

            var portfolioModel = new StockPortfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id
            };

            await _portfolioRepo.CreateAsync(portfolioModel);

            if (portfolioModel == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }
       
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetStockByIdAsync([FromRoute] int id, string email)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if(user == null) return BadRequest("Erro to find user");

                var stock = await _portfolioRepo.GetStockById(user.Id, id);
                if(stock == null) return BadRequest("Erro to find stock");
                
                return Ok(stock);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
 
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteStockPortifolio([FromBody] DeletePortfolio portfolio)
        {
            var appUser = await _userManager.FindByEmailAsync(portfolio.Email);
            
            if(appUser == null) return BadRequest("Error when searching for a user by email");
            var userPortifolio = await _portfolioRepo.GetUserPortfolioAsync(appUser);

            if(userPortifolio == null)
            {
                return BadRequest("User portifolio not found");
            }   

            var stock =  userPortifolio.Where(s => s.Id == portfolio.Id).ToList();
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