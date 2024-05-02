using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.ApplicationCore.Domain.Dtos.UserDtos;
using App.ApplicationCore.Interfaces;
using App.ApplicationCore.Domain.Entities;
using App.ApplicationCore.Domain.Dtos;
using App.Infrastructure.Persistance;

namespace App.Infrastructure.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<User> _users;
        private readonly DbSet<SoldeCarte> _soldecarte;
        private readonly DbSet<Facture> _facture;
        private readonly DbSet<Transaction> _transaction;
        private readonly IUserRepository _userRepository;

        public TransactionRepository(ApplicationDbContext applicationDbContext, IUserRepository userRepository) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _soldecarte = _applicationDbContext.Set<SoldeCarte>();
            _transaction = _applicationDbContext.Set<Transaction>();
            _users = _applicationDbContext.Set<User>();
            _userRepository = userRepository;
        }

        public async Task<SoldeCarte> GetSolde(string cin)
        {
            var data = await _soldecarte.FirstOrDefaultAsync(x => x.CIN == cin);
            return data;
        }

        public async Task<SoldeCarte> UpdateSolde(string cin, Decimal montant)
        {
            try
            {
                var compte = await _soldecarte.FirstOrDefaultAsync(x => x.CIN == cin);

                if (compte == null)
                {
                    return null;
                }

                Decimal soldeavant = compte.SoldeDisponible;
                Decimal soldeapres = soldeavant + montant;
                compte.SoldeDisponible = soldeapres;

                await _applicationDbContext.SaveChangesAsync();

                return compte;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur de mise à jour du solde : " + ex.Message);
                throw;
            }
        }

        public async Task<List<SoldeCarte>> ListSolde()
        {
            var res = await _soldecarte.ToListAsync();
            return res;
        }

        public async Task<List<UserSoldeDto>> ListUserWithSolde()
        {
            var soldeCarteList = await ListSolde();
            if (soldeCarteList == null || soldeCarteList.Count == 0)
                return null;

            var usersWithSolde = new List<UserSoldeDto>();

            foreach (var soldeCarte in soldeCarteList)
            {
                var user = await _userRepository.GetUserByCIN(soldeCarte.CIN);

                if (user != null)
                {
                    var userSoldeInfo = new UserSoldeDto
                    {
                        CIN = user.CIN,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Phone = user.Phone,
                        Addresse = user.Addresse,
                        SoldeApres = soldeCarte.SoldeDisponible
                    };

                    usersWithSolde.Add(userSoldeInfo);
                }
            }

            return usersWithSolde;
        }

        public async Task<TransactionDto> Achat(string cin, string OrderId)
        {
            var commandeinfo = await _facture.FirstOrDefaultAsync(x => x.OrderId.ToString() == OrderId);

            if (commandeinfo == null) return null;

            var montantcmd = commandeinfo.Montant;

            var compte = await _soldecarte.FirstOrDefaultAsync(x => x.CIN == cin);

            if (compte == null)
            {
                return null;
            }

            Decimal soldeavant = compte.SoldeDisponible;
            Decimal soldeapres = soldeavant - montantcmd;
            compte.SoldeDisponible = soldeapres;

            TransactionDto tr = new TransactionDto
            {
                Montant = montantcmd,
                SoldeAvant = soldeavant,
                SoldeApres = soldeapres,
                CIN = compte.CIN,
                DateTransaction = DateTime.Now
            };

            await _applicationDbContext.SaveChangesAsync();

            return tr;
        }

        public Task<User> GetUserByCIN(string cin)
        {
            throw new NotImplementedException();
        }


        public async Task<Facture> Recharge( decimal montant)
        {
            try
            {
                var compte = await _facture.FirstOrDefaultAsync(x => x.Montant == montant);

                if (compte == null)
                {
                    return null;
                }

                // Mise à jour du solde du compte
                compte.SoldeApres += montant;

                // Sauvegarde des modifications dans la base de données
                await _applicationDbContext.SaveChangesAsync();

                return compte;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur de recharge du solde : " + ex.Message);
                throw;
            }
        }

       
    }
}
