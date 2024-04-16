using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Account;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [Route("api/account")]
    [ApiController] 
    public class AccountController(UserManager<AppUser> userManager) : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager = userManager;
    
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            try{

                if(!ModelState.IsValid)
                    return BadRequest(ModelState);    
                
                var appUser = new AppUser
                {
                    UserName = register.UserName,
                    Email = register.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, register.Password);

                if(createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if(roleResult.Succeeded)
                    {
                        return Ok("User Created");
                    }else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }else 
                {
                    return StatusCode(500, createdUser.Errors);
                }

            }catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }


    }
}