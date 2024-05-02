using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Entities
{
    public class ProductBrand 
    {

        [Key]
        public int ProductBrandId { get; set; }
        public string Name { get; set; }
    }
}
