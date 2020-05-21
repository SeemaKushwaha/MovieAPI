using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarlZeiss.Movies.Api.Models
{
    public class BookedSeat
    {
        
        public int BookingId { get; set; }
        public Booking Bookings { get; set; }
        public int SeatId { get; set; }
        public SeatMaster Seat { get; set; }
    }
}
