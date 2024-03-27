using App.ApplicationCore.Common;
using App.ApplicationCore.Domain.Dtos.UserDtos;
using App.ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.UI.Web.Controller
{


    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ReadUserDto>>> GetAllUsersAsync([FromQuery] QueryOptions queryOptions)
        {
            var users = await _userService.GetAllUsersAsync(queryOptions);
            return Ok(users);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReadUserDto>> CreateUserAsync([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userService.CreateUserAsync(createUserDto);
            return Ok(user);
        }


    }
}
