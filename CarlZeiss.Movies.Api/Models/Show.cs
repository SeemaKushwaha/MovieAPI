using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarlZeiss.Movies.Api.Models
{
    public class Show
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int MultiplexId { get; set; }
        public Movie Movie { get; set; }
        public Multiplex Multiplex { get; set; }
        public DateTime ShowDate { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
