using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.Models
{
    public class FutureDate:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            //string format = "dd mm yyyy";
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
            var isValid = DateTime.TryParseExact(Convert.ToString(value), "dd MMM yyyy",culture, DateTimeStyles.None, out dateTime);
            return (isValid && dateTime > DateTime.Now);
        }
    }
}