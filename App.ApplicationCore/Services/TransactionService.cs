using App.ApplicationCore.Domain.Dtos;
using App.ApplicationCore.Domain.Entities;
using App.ApplicationCore.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<TransactionDto> Achat(string cin, string orderId)
        {
            return await _transactionRepository.Achat(cin, orderId);
        }

        public async Task<SoldeCarte> GetSolde(string cin)
        {
            return await _transactionRepository.GetSolde(cin);
        }

        public async Task<List<SoldeCarte>> ListSolde()
        {
            return await _transactionRepository.ListSolde();
        }

        public async Task<List<UserSoldeDto>> ListUserWithSolde()
        {
            return await _transactionRepository.ListUserWithSolde();
        }

        public async Task<Facture> Recharge( decimal montant)
        {
           return await _transactionRepository.Recharge( montant);
        }

        public async Task<SoldeCarte> UpdateSolde(string cin, decimal montant)
        {
            return await _transactionRepository.UpdateSolde(cin, montant);
        }
    }
}

