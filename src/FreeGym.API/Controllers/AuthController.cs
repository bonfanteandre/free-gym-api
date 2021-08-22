using FreeGym.API.Dtos;
using FreeGym.Data.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeGym.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenService _tokenService;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    .ToList();

                return BadRequest(errors);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new List<string>() { "Usuário ou senha inválidos" });
            }

            var user = await _userManager.FindByEmailAsync(loginDto.UserName);
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var token = _tokenService.GenerateToken(user, claims, roles);

            return Ok(new
            {
                Token = token,
                ExpiresIn = TimeSpan.FromHours(_tokenService.TokenExpiresIn).TotalSeconds
            });
        }
    }
}
