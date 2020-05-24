using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarlZeiss.Movies.Api.Dtos;
using CarlZeiss.Movies.Api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarlZeiss.Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Role.User + "," + Role.Admin)]

    public class ValuesController : ControllerBase
    {
        private readonly IMovieBookingRepository _repo;
        private readonly IMapper _mapper;

        public ValuesController(IMovieBookingRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        // GET api/city
        [HttpGet("city")]
        public async Task<IActionResult> GetCities()
        {
            var result = await _repo.GetCities();
            return Ok(result);
        }

        // GET api/city/5
        [HttpGet("multiplexes/{cityId}")]
        public async Task<IActionResult> GetMultiplexes(int cityId)
        {
            var result = await _repo.GetMultiplexes(cityId);
            return Ok(result);
        }

        [HttpGet("seats/{multiplxId}/{movieId}")]
        public async Task<IActionResult> GetSeats(int multiplxId, int movieId)
        {
            var seatsFromRepo = await _repo.GetSeats(multiplxId,movieId);
            var seatsToReturn = _mapper.Map<List<SeatDto>>(seatsFromRepo);
            return Ok(seatsToReturn);
        }
    }
}
