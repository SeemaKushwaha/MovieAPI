using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarlZeiss.Movies.Api.Dtos
{
    public class ShowDetailsDto
    {
        [Required]
        public string MovieName { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public int MultiplexId { get; set; }
        [Required]
        public DateTime ShowDate { get; set; }
    }
}
