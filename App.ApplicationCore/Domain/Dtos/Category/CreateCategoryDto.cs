using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Dtos.Category
{
    public class CreateCategoryDto
    {
        public required string Name { get; set; }
        public required string Image { get; set; }
    }
}
