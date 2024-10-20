using api.Interfaces;
using api.Models;
using api.Service;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            try
            {

                if (!ModelState.IsValid) return BadRequest(ModelState);

                if (register.Email != null)
                {
                    var existingUserByEmail = await _userManager.FindByEmailAsync(register.Email);
                    if (existingUserByEmail != null)
                    {
                        return BadRequest("Email already exists.");
                    }
                }

                var appUser = new AppUser
                {
                    UserName = register.UserName,
                    Email = register.Email,
                };

                if (register.Password == null) return BadRequest("Error password cannot be null.");
                var createdUser = await _userManager.CreateAsync(appUser, register.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new User
                            {
                                UserName = register.UserName,
                                Email = register.Email,
                                Token = _token.CreateToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (login.Email == null) return BadRequest("Error email cannot be null.");
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == login.Email.ToLower());

            if (user == null) return Unauthorized("Invalid E-mail");

            if (login.Password == null) return BadRequest("Error password cannot be null.");
            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!result.Succeeded) return Unauthorized("E-mail not found or your password is wrong");

            return Ok(
                new User
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _token.CreateToken(user)
                }
            );

        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok("User logged out successfully.");
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost("getrole")]
        [Authorize]
        public async Task<IActionResult> GetRoleByUser([FromBody] UserEmail user)
        {
            var appUser = await _userManager.FindByEmailAsync(user.Email);
            if (appUser == null)
            {
                return BadRequest("User not found");
            }
            var role = await _userManager.GetRolesAsync(appUser);

            return Ok(role);
        }
    }
}