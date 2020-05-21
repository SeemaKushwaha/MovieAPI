using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarlZeiss.Movies.Api.Dtos
{
    public class UserRegisterDto
    {

        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You Must Specify Password Between 4 and 8 characters")]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
