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
    public class StockPortifolioController(UserManager<AppUser> userManager, IStockRepository repository, IUserPortifolioRepository portifolioRepositoy) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IStockRepository _repository = repository;
        private readonly IUserPortifolioRepository _portifolioRepositoy = portifolioRepositoy;
    
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortifolio([FromBody] UserPortifolioDto userPortifolioDto)
        {
            var appUser = await _userManager.FindByEmailAsync(userPortifolioDto.Email);
            var userPortifolio = await _portifolioRepositoy.GetUserPortifolio(appUser);
            
            return Ok(userPortifolio);
        }
    }
}