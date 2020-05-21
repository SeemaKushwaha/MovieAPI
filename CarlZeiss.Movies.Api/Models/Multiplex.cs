using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarlZeiss.Movies.Api.Models
{
    public class Multiplex
    {
        public int Id { get; set; }
        public string MultiplexName { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
    }
}
