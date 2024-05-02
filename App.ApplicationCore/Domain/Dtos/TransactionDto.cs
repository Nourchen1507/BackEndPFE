using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Dtos
{
    public class TransactionDto
    {


        public Decimal Montant { get; set; }

        [Key]
        public string CIN { get; set; }

       public Decimal SoldeAvant { get; set; }
       public Decimal SoldeApres { get; set; }

        public string NumCommande { get; set; }
        public DateTime DateTransaction { get; set; }

    }
}
