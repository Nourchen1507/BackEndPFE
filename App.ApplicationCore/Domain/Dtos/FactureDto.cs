using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Dtos
{
    public  class FactureDto
    {


        public Guid Id { get; set; }
        public Decimal Montant { get; set; }
        public DateTime DateFacture { get; set; }
        public decimal SoldeAvant { get; set; }
        public decimal SoldeApres { get; set; }
        public Guid OrderId { get; set; }


        public string PayRef { get; set; }
        public string PayGateway { get; set; }
        public string PayStatus { get; set; }
    }
}
