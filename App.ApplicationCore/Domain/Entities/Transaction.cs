using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Entities
{
    public class Transaction : BaseEntity

    {

        [Key]
        public Guid Id { get; set; }

        public Decimal Montant { get; set; }
        public string CIN { get; set; }

        public Decimal SoldeAvant { get; set; }
        public Decimal SoldeApres { get; set; }
        public string NumCommande { get; set; }
        public DateTime DateTransaction { get; set; }


    }
}
