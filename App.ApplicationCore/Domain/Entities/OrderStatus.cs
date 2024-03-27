using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Entities
{
    public enum OrderStatus
    {
        [Display(Name = "Pending")]
        Pending,
        [Display(Name = "Shipped")]
        Shipped,
        [Display(Name = "Cancelled")]
        Cancelled


    }

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var displayAttribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .OfType<DisplayAttribute>()
                .SingleOrDefault();

            return displayAttribute?.Name ?? value.ToString();
        }
    }
}
