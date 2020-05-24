using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarlZeiss.Movies.Api.Helpers
{
    public class IntValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            return Convert.ToInt32(value) > 0 ?
                ValidationResult.Success :
                new ValidationResult($"{validationContext.DisplayName} must be an integer greater than 0.");
        }
    }
}
