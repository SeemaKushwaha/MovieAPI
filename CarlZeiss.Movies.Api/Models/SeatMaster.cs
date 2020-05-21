using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarlZeiss.Movies.Api.Models
{
    public class SeatMaster
    {
        public int Id { get; set; }
        public int SeatNo { get; set; }
        public int MultiplexId { get; set; }
        public Multiplex multiplex { get; set; }
    }
}
