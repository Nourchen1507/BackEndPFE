using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Entities
{
    public  class Facture
    {
        public Guid Id { get; set; }
        public Decimal Montant { get; set; }
        public DateTime DateFacture { get; set; }
        public decimal SoldeAvant { get; set; }
        public decimal SoldeApres { get; set; }
        public Guid OrderId { get; set; } // Renommé de IdCommande à OrderId
        public string PayRef { get; set; }
        public string PayGateway { get; set; }
        public string PayStatus { get; set; }

        // Propriété de navigation vers la commande
        [ForeignKey("OrderId")]
        public Order Order { get; set; } // Renommé de Commande à Order
    }
}
 
