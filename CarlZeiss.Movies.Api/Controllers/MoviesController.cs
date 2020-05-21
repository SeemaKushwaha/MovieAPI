using System.Threading.Tasks;
using AutoMapper;
using CarlZeiss.Movies.Api.Dtos;
using CarlZeiss.Movies.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CarlZeiss.Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> GetShows(int multiplxId)
        {
            var moviesFromRepo = await _repo.GetShows(multiplxId);
            var showDetailsReturnDto = _mapper.Map<ShowDetailsReturnDto>(moviesFromRepo);

            return Ok(showDetailsReturnDto);
        }

       

    }
}