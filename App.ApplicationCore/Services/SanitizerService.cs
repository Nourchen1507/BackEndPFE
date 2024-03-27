using App.ApplicationCore.Interfaces;
using Ganss.Xss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Services
{
    public class SanitizerService : ISanitizerService
    {
        private readonly IHtmlSanitizer _htmlSanitizer;

        public SanitizerService()
        {
            _htmlSanitizer = new HtmlSanitizer();
        }
        public string SanititzeHtml(string input)
        {
            return _htmlSanitizer.Sanitize(input);
        }

        public T SanitizeDto<T>(T inputDto)
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string) && property.Name.ToLower() != "password" && property.Name.ToLower() != "imageurl")
                {
                    var value = property.GetValue(inputDto) as string;
                    if (value != null)
                    {
                        var sanitizedValue = SanititzeHtml(value);
                        property.SetValue(inputDto, sanitizedValue);
                    }
                }
            }
            return inputDto;
        }
    }
}
