using App.ApplicationCore.Common;
using App.ApplicationCore.Domain.Dtos.UserDtos;
using App.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Interfaces
{
    public interface IUserService 
    {
        Task<IEnumerable<ReadUserDto>> GetAllUsersAsync(QueryOptions queryOptions);
        Task<ReadUserDto> CreateUserAsync(CreateUserDto userDto);
        Task<ReadUserDto> CreateAdminAsync(CreateUserDto userDto);
        Task<ReadUserDto> GetUserByIdAsync(Guid userId);
        Task<ReadUserDto> GetUserByEmailAsync(string email);
        //Task<ReadUserDto> UpdateUserAsync(Guid userId, UpdateUserDto userDto);
        Task<bool> DeleteUserByIdAsync(Guid userId);
    }
}
