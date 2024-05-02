using App.ApplicationCore.Domain.Entities;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;


namespace App.ApplicationCore.Domain.Dtos.UserDtos
{
    [AutoMap(typeof(User))]
    public class CreateUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        [Ignore]
        public string Password { get; set; }

        public string Addresse { get; set; }
       
        public string Phone { get; set; }


        public string CIN { get; set; }
        
       
    }
}
