using CarlZeiss.Movies.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarlZeiss.Movies.Api.Dtos
{
    public class UserBookingDto
    {
        public UserBookingDto()
        {
            BookingDate = DateTime.Now;
        }

        [Required]
        public int UserId { get; set; }

        [StringLength(5, MinimumLength = 1, ErrorMessage = "Maximum 5 seats can be booked")]
        public List<SeatDto> Seat { get; set; }

        [Required]
        public int ShowId { get; set; }

        [Required]
        public int MultiplexId { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }
    }
}
