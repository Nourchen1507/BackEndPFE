using App.ApplicationCore.Domain.Entities;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;


namespace App.ApplicationCore.Domain.Dtos.UserDtos
{
    [AutoMap(typeof(User))]
    public class CreateUserDto
    {
     
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        [Ignore]
        public string Password { get; set; }

        public string Localisation { get; set; }

        public string ResidenceName { get; set; }
        public Guid AdresseId { get; set; }
        public string Phone { get; set; }


        public string CIN { get; set; }
        
       
    }
}
