using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarlZeiss.Movies.Api.Dtos;
using CarlZeiss.Movies.Api.Helpers;
using CarlZeiss.Movies.Api.Models;
using CarlZeiss.Movies.Api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarlZeiss.Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Role.Admin)]
    public class AdminController : ControllerBase
    {
        private readonly IMovieBookingRepository _repo;
        private readonly IMapper _mapper;
        public AdminController(IMovieBookingRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpPost("show")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddShow(ShowDetailsDto bookingDetails)
        {
            
            if (await _repo.GetMultiplexe(bookingDetails.MultiplexId) == null)
            {
                return NotFound("Multiplex not found");
            }

            var movieToRepo = _mapper.Map<Movie>(bookingDetails);
            _repo.Add(movieToRepo);

            if(await _repo.SaveAll())
            {
                var showToRepo = _mapper.Map<Show>(bookingDetails);
                showToRepo.MovieId = movieToRepo.Id;
                _repo.Add(showToRepo);

                var showToReturn = _mapper.Map<ShowDetailsReturnDto>(showToRepo);

                if (await _repo.SaveAll())
                {
                    return CreatedAtRoute("Getshow", new { controller = "Movies", multiplxId = showToRepo.MultiplexId }, showToRepo);
                }
            }
            throw new Exception("Error in saving Movie show details");
        }
    }
}