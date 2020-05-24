using CarlZeiss.Movies.Api.Helpers;
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

        public int UserId { get; set; }

        [ListValidation]
        public List<SeatDto> Seat { get; set; }

        [Required(ErrorMessage ="Show id is required")]
        [IntValidation]
        public int ShowId { get; set; }

        [Required(ErrorMessage = "Multiplex id is required")]
        [IntValidation]
        public int MultiplexId { get; set; }

        [Required(ErrorMessage = "Date of booking is required")]
        [DataType(DataType.DateTime, ErrorMessage = "Date field is invalid")]
        public DateTime BookingDate { get; set; }
    }
}
