using CarlZeiss.Movies.Api.Models;
using System;

namespace CarlZeiss.Movies.Api.Dtos
{
    public class ShowDetailsReturnDto
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public Multiplex Multiplex { get; set; }
        public DateTime ShowDate { get; set; }
    }
}
