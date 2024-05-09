using App.ApplicationCore.Domain.Dtos;
using App.ApplicationCore.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Services
{
    public class FactureService : IFactureService
    {

        private readonly IStripeService _stripeService;
        private readonly IMapper _mapper;

        public FactureService(IStripeService stripeService,IMapper mapper)
        {
            _stripeService = stripeService;
            _mapper = mapper;
        }

        public async Task<string> GetPaymentStatusAsync(string sessionId)
        {
            // Utilisation de _stripeService pour obtenir le statut de paiement
            return await _stripeService.GetPaymentStatusAsync(sessionId);




        }
        public async Task<string> CreatePaymentSessionAsync(PaymentRequestDto paymentRequest)
        {
            
            await Task.Delay(100);

            // Retourne un ID de session de paiement simulé
            return "session_id_simulated";
        }
    }


    }

