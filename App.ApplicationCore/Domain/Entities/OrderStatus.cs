using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Entities
{



    [JsonConverter(typeof(JsonStringEnumConverter))]
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
