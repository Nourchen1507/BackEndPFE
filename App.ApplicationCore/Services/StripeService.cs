using App.ApplicationCore.Interfaces;
using AutoMapper;
using Stripe;
using Stripe.Checkout;

namespace App.ApplicationCore.Services
{
    public class StripeService : IStripeService
    {

        private readonly IMapper _mapper;

        public StripeService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<string> GetPaymentStatusAsync(string sessionId)
        {
            var stripeOptions = new RequestOptions
            {
                ApiKey = "sk_test_51PBcNmJ8pIPE274zvQ8PFERK7bAxxtxQtkJx6AhEXnyQLLodFqscTYefYxMla7tKmqUE7CbcDT4XObveLrmiOYpb008Vmpgva8"
            };

            var sessionService = new SessionService();
            var session = await sessionService.GetAsync(sessionId, null, stripeOptions);

            return session.PaymentStatus;
        }
    }
}

