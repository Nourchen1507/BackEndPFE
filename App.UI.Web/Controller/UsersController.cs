using App.ApplicationCore.Common;
using App.ApplicationCore.Domain.Dtos.UserDtos;
using App.ApplicationCore.Domain.Entities;
using App.ApplicationCore.Interfaces;

using App.Infrastructure.Persistance;
using App.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace App.UI.Web.Controller
{


    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;
        public UsersController(IUserService userService, IMapper mapper , ApplicationDbContext applicationDbContext, IUserRepository userRepository)
        {
            _userService = userService;
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ReadUserDto>>> GetAllUsersAsync()
        {
            var users = await _applicationDbContext.User.ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<ReadUserDto>> CreateUserAsync([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userService.CreateUserAsync(createUserDto);
            return Ok(user);
        }

        [HttpGet("{id:Guid}")]
        //[Authorize]
        public async Task<ActionResult<ReadUserDto>> GetUserByIdAsync(Guid id)
        {
            var user =
                await _applicationDbContext.User
                .FirstOrDefaultAsync(x => x.Id == id);

            if (
                
                user == null)
            {

                return NotFound();
            }
            return Ok(user);
        }


        //[Authorize(Roles = "Admin")]
        [HttpPost("Admin/")]
        public async Task<ActionResult<ReadUserDto>> CreateAdminAsync([FromBody] CreateUserDto userDto)
        {
            var adminUser = await _userService.CreateAdminAsync(userDto);
            return Ok(adminUser);
        }

    
        // [Authorize]
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<bool>> DeleteUserAsync(Guid id, [FromBody] string email)
        {
            try
            {
                // Vérification de l'email
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null || user.Email != email)
                {
                    return NotFound();
                }

                var result = await _userService.DeleteUserByIdAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return true;
            }
            catch (Exception ex)
            {
                // Log l'exception pour le débogage
                Console.Error.WriteLine(ex.Message);
                return StatusCode(500); // Erreur interne du serveur
            }
        }

        [HttpPut("{id:Guid}")]
        //[Authorize]
        public async Task<ActionResult<ReadUserDto>> UpdateUserAsync(Guid id, [FromBody] UpdateUserDto userDto)
        {
            var requestUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (requestUserIdClaim == null || !Guid.TryParse(requestUserIdClaim.Value, out var UpdateRequestingUserId))
            {
                return Forbid();
            }

            if (UpdateRequestingUserId == id)
            {
                var user = await _userService.UpdateUserAsync(id, userDto);
                if (user == null)
                {
                    return BadRequest();
                }
                var readUserDto = _mapper.Map<ReadUserDto>(user);
                return Ok(readUserDto);
            }
            return Forbid();
        }

    }
}
