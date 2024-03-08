using LittleLemonAPI.Dto;
using LittleLemonAPI.Interfaces;
using LittleLemonAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace LittleLemonAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly UserManager<Staff> _staffManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<Staff> _signInManager;
        public AccountController(UserManager<Staff> userManager, ITokenService tokenService, SignInManager<Staff> signInManager) 
        { 
            _staffManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _staffManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
            if (user == null) 
            { 
                return Unauthorized("Invalid Username"); 
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) 
            { 
                return Unauthorized("Incorrect username or pasword");
            }

            return Ok(_tokenService.CreateToken(user));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var staffUser = new Staff
                {
                    UserName = registerDto.Username,
                };

                var createdUser = await _staffManager.CreateAsync(staffUser, registerDto.Password);

                if (createdUser.Succeeded) 
                {
                    var roleResult = await _staffManager.AddToRoleAsync(staffUser, "Staff");
                    if (roleResult.Succeeded)
                    {
                        return Ok(_tokenService.CreateToken(staffUser));
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
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
