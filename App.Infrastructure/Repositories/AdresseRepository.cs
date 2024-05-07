
using App.ApplicationCore.Domain.Entities;
using App.ApplicationCore.Interfaces;
using App.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;


namespace App.Infrastructure.Repositories
{
    public class AdresseRepository : GenericRepository<Adresse>, IAdresseRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<Adresse> _addresse;
        private readonly DbSet<User> _user;
        private readonly AdresseRepository _adresseRepository;
        private readonly UserRepository _userRepository;

        public AdresseRepository(ApplicationDbContext applicationDbContext, AdresseRepository adresseRepository,UserRepository userRepository) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _addresse = _applicationDbContext.Set<Adresse>();
            _user = _applicationDbContext.Set<User>();
            _adresseRepository = adresseRepository;
            _userRepository = userRepository;
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