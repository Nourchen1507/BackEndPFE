using App.ApplicationCore.Domain.Entities;
using App.ApplicationCore.Interfaces;
using App.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories
{
    public class AddresseRepository : GenericRepository<Adresse>, IAdresseRepository
    {

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<Adresse> _addresse;
        private readonly DbSet<User> _user;
        public AddresseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {


            _applicationDbContext = dbContext;
            _addresse = _applicationDbContext.Set<Adresse>();
            _user = _applicationDbContext.Set<User>();
        }

        public async Task<Adresse> GetAdresseByResidenceNameAsync(string ResidenceName)
        {
            return await _addresse
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => c.ResidenceName.ToLower() == ResidenceName.ToLower());
        }

        public async Task<IEnumerable<User>> GetAllUsersInAdresseAsync(Guid adresseId)
        {
            var users = await _user
                .Where(u => u.AdresseId == adresseId)
                .ToListAsync();

            return users;
        }
    }

}
