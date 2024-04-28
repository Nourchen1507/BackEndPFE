using App.ApplicationCore.Domain.Dtos.UserDtos;
using App.ApplicationCore.Domain.Entities;
using App.ApplicationCore.Interfaces;
using App.UI.Web.Authentification;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace App.UI.Web.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
      
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> AuthenticateUserAsync([FromBody] UserCredentialsDto userCredentials)
        {
            return Ok(await _authService.AutheticateUser(userCredentials));
        }

        [HttpPost("token")]
        [Authorize]
        public async Task<ActionResult<string>> RefreshTokenAsync([FromBody] string token)
        {
            return Ok(await _authService.RefreshToken(token));
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<ReadUserDto>> GetUserProfileAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}

