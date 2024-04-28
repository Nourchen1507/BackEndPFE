using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Entities
{
    public class User : BaseEntity 
    {

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Addresse { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]

        public required string PasswordHash { get; set; }

        public UserRole Role { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
