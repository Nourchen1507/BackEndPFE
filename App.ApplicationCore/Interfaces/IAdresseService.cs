using App.ApplicationCore.Domain.Dtos.Adresses;
using App.ApplicationCore.Domain.Dtos.Category;
using App.ApplicationCore.Domain.Dtos.Product;
using App.ApplicationCore.Domain.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Interfaces
{
    public interface IAdresseService
    {
        Task<IEnumerable<ReadAdresseDto>> GetAllAddresseAsync();
       
        Task<ReadAdresseDto> CreateAddresseAsync(CreateAdresseDto addresseDto);
        Task<ReadAdresseDto> GetAdresseByIdAsync(Guid adresseId);

        Task<ReadAdresseDto> UpdateAddresseAsync(Guid adresseId, UpdateAdresseDto addresseDto);
        Task<bool> DeleteAddresseByIdAsync(Guid adresseId);

        Task<IEnumerable<ReadUserDto>> GetAllUsersInAddresseAsync(Guid adresseId);


    }
}
