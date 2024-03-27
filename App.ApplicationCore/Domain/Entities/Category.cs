using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Entities
{
    public  class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Url]
        [Required]
        public string Image {  get; set; }
        public List<Product> Products { get; set; }
    }
}
    