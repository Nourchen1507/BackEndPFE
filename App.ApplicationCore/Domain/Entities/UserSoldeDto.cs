using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Entities
{
    public class UserSoldeDto
    {


        public Decimal Montant { get; set; }
        public string CIN { get; set; }

        public Decimal SoldeAvant { get; set; }
        public Decimal SoldeApres { get; set; }

        public string NumCommande { get; set; }
        public DateTime DateTransaction { get; set; }

        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Addresse { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
