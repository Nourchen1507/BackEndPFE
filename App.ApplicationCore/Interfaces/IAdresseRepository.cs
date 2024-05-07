using App.ApplicationCore.Common;
using App.ApplicationCore.Domain.Dtos.Adresses;
using App.ApplicationCore.Domain.Dtos.Category;
using App.ApplicationCore.Domain.Dtos.UserDtos;
using App.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Interfaces
{
    public interface IAdresseRepository : IGenericRepository<Adresse>
    {


        Task<IEnumerable<User>> GetAllUsersInAdresseAsync(Guid adresseId);
        Task<Adresse> GetAdresseByResidenceNameAsync(string ResidenceName);
    }
}
