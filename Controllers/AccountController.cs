using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{

    [Route("api/account")]
    [ApiController] 
    public class AccountController(UserManager<AppUser> userManager, ITokenService token, SignInManager<AppUser> signInManager) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly ITokenService _token = token;
        private readonly SignInManager<AppUser> _signInManager = signInManager;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            try{

                if(!ModelState.IsValid) return BadRequest(ModelState);    
                
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
                        return Ok(
                            new NewUserDto 
                            {
                                UserName = register.UserName,
                                Email = register.Email,
                                Token = _token.CreateToken(appUser)
                            }
                        );
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == login.Email.ToLower());

            if(user == null) return Unauthorized("Invalid E-mail");

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if(!result.Succeeded) return Unauthorized("E-mail not found or your password is wrong");

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _token.CreateToken(user)
                }
            );

        }
    }
}