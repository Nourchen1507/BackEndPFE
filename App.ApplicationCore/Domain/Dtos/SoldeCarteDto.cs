using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Dtos
{
    public  class SoldeCarteDto
    {

        [Key]
        public string CIN {  get; set; }
        public string SoldeCarte {  get; set; }

    }
}
