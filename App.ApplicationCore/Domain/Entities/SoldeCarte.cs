using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Entities
{
    public  class SoldeCarte
    {

        [Key]
        public string CIN {  get; set; }
        public Decimal SoldeDisponible {  get; set; }


    }
}
