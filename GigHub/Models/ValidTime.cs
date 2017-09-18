using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.Models
{
    public class ValidTime : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime time;
            const string format = "HH:mm";
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
            var isValid = DateTime.TryParseExact(value.ToString(), format, culture, DateTimeStyles.None, out time);
            return (isValid);
        }
    }
}
























