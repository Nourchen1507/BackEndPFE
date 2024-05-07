using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Entities
{
    public class Product : BaseEntity 
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public Decimal Price { get; set; }

        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual Category Category { get; set; }

        public int Inventory {  get; set; }

        public string ImageUrl { get; set; }
       
    }
}
