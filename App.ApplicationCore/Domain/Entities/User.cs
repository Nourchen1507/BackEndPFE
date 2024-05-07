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
        public string Localisation { get; set; }

        [Required]
        public Guid AdresseId { get; set; }

        public string Phone { get; set; }

        [Required]
        public string CIN { get; set; }

        public string PasswordHash { get; set; }

        public UserRole Role { get; set; }


    }
}
