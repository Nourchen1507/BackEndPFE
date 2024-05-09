using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Interfaces
{
    public interface IStripeService
    {

        Task<string> GetPaymentStatusAsync(string sessionId);

    }
}
