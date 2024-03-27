﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Entities
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public  enum UserRole
    {

        Customer,
        Admin
    }
}
