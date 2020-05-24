using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    [Authorize(Roles = Role.User)]
    public class BookingsController : ControllerBase
    {
        private readonly IMovieBookingRepository _repo;
        private readonly IMapper _mapper;
        public BookingsController(IMovieBookingRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        // GET api/bookings/
        [HttpGet(Name = "GetBooking")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetBookings()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userBookingFromRepo = await _repo.GetBookings(userId);
            if(userBookingFromRepo == null)
            {
                return NoContent();
            }

            var userBookingToReturn = _mapper.Map<List<UserBookingReturnDto>>(userBookingFromRepo);
            return Ok(userBookingToReturn);
        }

        [HttpGet("{id}", Name = "GetBookingById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetBookings(int id)
        {
            var userBookingFromRepo = await _repo.GetBooking(id);
            if(userBookingFromRepo == null)
            {
                return NoContent();
            }
            var userBookingToReturn = _mapper.Map<UserBookingReturnDto>(userBookingFromRepo);

            return Ok(userBookingToReturn);
        }

        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> BookMovie(UserBookingDto bookingDetails)
        {
            bookingDetails.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            if(await _repo.GetShow(bookingDetails.ShowId) == null || await _repo.GetMultiplexe(bookingDetails.MultiplexId) == null)
            {
                return NotFound("Selected Show not found");
            }

            var availableSeats = _mapper.Map<IEnumerable<SeatDto>>(await _repo.GetSeats(bookingDetails.MultiplexId, bookingDetails.ShowId));
            
            if(!bookingDetails.Seat.All(x => availableSeats.Any(y => x.SeatId == y.SeatId)))
            {
                return NotFound("Selected seats are not available");
            }

            var bookingToRepo = _mapper.Map<Booking>(bookingDetails);
            _repo.Add(bookingToRepo);

            if (await _repo.SaveAll())
            {
                var bookedSeatsToRepo = _mapper.Map<List<BookedSeat>>(bookingDetails.Seat);
                foreach (var bookedSeatToRepo in bookedSeatsToRepo)
                {
                    bookedSeatToRepo.BookingId = bookingToRepo.Id;
                    _repo.Add(bookedSeatToRepo);
                }

                var bookingReturnDto = _mapper.Map<UserBookingReturnDto>(bookingToRepo);

                if (await _repo.SaveAll())
                {
                    return CreatedAtRoute("GetBookingById", new { controller = "Bookings", id = bookingToRepo.Id }, bookingReturnDto);
                }
            }

            throw new Exception("Booking cannot be completed");
        }

    }
}