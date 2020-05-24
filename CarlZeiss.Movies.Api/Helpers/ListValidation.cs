using CarlZeiss.Movies.Api.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarlZeiss.Movies.Api.Helpers
{
    public class ListValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(Object value, ValidationContext validationContext)
        {
            var list = value as IEnumerable<SeatDto>;

            if (list == null)
                return new ValidationResult("Atleast one seat must be selected");

            if (list.Count() > 5)
            {
                return new ValidationResult("Maximum 5 seats can be booked");
            }

            if(list.GroupBy(x => x.SeatId).Any(g => g.Count() > 1))
            {
                return new ValidationResult("Duplicate seat selected , Booking cannot be processed");
            }

            return ValidationResult.Success;
        }
    }
}
