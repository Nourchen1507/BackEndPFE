using App.ApplicationCore.Domain.Dtos;
using App.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Interfaces
{
    public interface ITransactionRepository
    {
        Task<SoldeCarte> GetSolde(string cin);
        Task<SoldeCarte> UpdateSolde(string cin, Decimal montant);
        Task<List<SoldeCarte>> ListSolde();
        Task<List<UserSoldeDto>> ListUserWithSolde();

        Task<Facture> Recharge(decimal montant); // Déclaration de la méthode de recharge
        Task<User> GetUserByCIN(string cin);
        Task<TransactionDto> Achat(string cin, string orderId);
    }



}
