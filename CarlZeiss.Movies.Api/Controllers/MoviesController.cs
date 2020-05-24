using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarlZeiss.Movies.Api.Dtos;
using CarlZeiss.Movies.Api.Repository;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarlZeiss.Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Role.User + "," + Role.Admin)]

    public class MoviesController : ControllerBase
    {
        private readonly IMovieBookingRepository _repo;
        private readonly IMapper _mapper;

        public MoviesController(IMovieBookingRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("{multiplxId}", Name = "Getshow")]
        [EnableQuery]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetShows(int multiplxId)
        {
            var moviesFromRepo = await _repo.GetShows(multiplxId);
            if(moviesFromRepo == null)
            {
                return NotFound();
            }
            var showDetailsReturnDto = _mapper.Map<IEnumerable<ShowDetailsReturnDto>>(moviesFromRepo);

            return Ok(showDetailsReturnDto);
        }
    }
}