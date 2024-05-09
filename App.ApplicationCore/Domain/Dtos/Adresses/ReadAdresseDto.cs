﻿using App.ApplicationCore.Domain.Dtos.Category;
using App.ApplicationCore.Domain.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Dtos.Adresses
{
    public class ReadAdresseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string ResidenceName { get; set; }
        public int Floor { get; set; }
        public string HouseNumber { get; set; }
        public string AccessCode { get; set; }
    }
}