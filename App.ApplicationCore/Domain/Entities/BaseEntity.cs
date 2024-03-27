using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Entities
{
    public class BaseEntity : TimeStamp
    {
        public Guid Id { get; set; }
    }
}
