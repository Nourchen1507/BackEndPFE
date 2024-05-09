using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Dtos
{
    public class PaymentRequestDto
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public decimal MontantTTC { get; set; }
        public decimal MontantHT { get; set; }
        public decimal Montant { get; set; }
        public string Currency { get; set; }
    }
}
