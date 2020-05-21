using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarlZeiss.Movies.Api.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ShowId { get; set; }
        public Show Show { get; set; }
        public IEnumerable<BookedSeat> Seats { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
