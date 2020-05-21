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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarlZeiss.Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> GetBookings()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userBookingFromRepo = await _repo.GetBookings(userId);
            var userBookingToReturn = _mapper.Map<UserBookingReturnDto>(userBookingFromRepo);

            return Ok(userBookingToReturn);
        }

        [HttpGet(Name = "GetBookingById")]
        public async Task<IActionResult> GetBookings(int id)
        {
            var userBookingFromRepo = await _repo.GetBooking(id);
            var userBookingToReturn = _mapper.Map<UserBookingReturnDto>(userBookingFromRepo);

            return Ok(userBookingToReturn);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> BookMovie(UserBookingDto bookingDetails)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState.AddModelError();
            //}
            if(await _repo.GetShow(bookingDetails.ShowId) == null || await _repo.GetMultiplexe(bookingDetails.MultiplexId) == null)
            {
                return BadRequest("Show not found");
            }

            var SeatList = _mapper.Map<IEnumerable<SeatDto>>(await _repo.GetSeats(bookingDetails.MultiplexId, bookingDetails.ShowId));
            
            if(bookingDetails.Seat.Intersect(SeatList).Count() == bookingDetails.Seat.Count())
            {
                return BadRequest("Seats not available");
            }

            var bookingToRepo = _mapper.Map<Booking>(bookingDetails);
            _repo.Add(bookingToRepo);

            var bookedSeatsToRepo = _mapper.Map<BookedSeat>(bookingDetails.Seat);
            _repo.Add(bookingToRepo);

            if(await _repo.SaveAll())
            {
                return CreatedAtRoute("GetBookingById", new { controller = "Bookings", userId = bookingToRepo.Id }, bookingToRepo);
            }

            throw new Exception("Booking cannot be completed");
        }

    }
}