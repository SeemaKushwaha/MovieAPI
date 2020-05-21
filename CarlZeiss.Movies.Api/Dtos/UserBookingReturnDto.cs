using CarlZeiss.Movies.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarlZeiss.Movies.Api.Dtos
{
    public class UserBookingReturnDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public List<SeatDto> SeatNo { get; set; }

        public Show Show { get; set; }

        public DateTime BookingDate { get; set; }
    }
}
