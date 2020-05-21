using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarlZeiss.Movies.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CarlZeiss.Movies.Api.Repository
{
    public class MovieBookingRepository : IMovieBookingRepository
    {
        
        private readonly DataContext _context;
        public MovieBookingRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        //public async Task<IEnumerable<Booking>> GetBookings(int userId)
        //{
        //    return await _context.Bookings.Include(u => u.User).Where(u => u.UserId == userId).ToListAsync();
        //}

        public async Task<IEnumerable<City>> GetCities()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<IEnumerable<Multiplex>> GetMultiplexes(int cityId)
        {
            return await _context.Multiplexes.Where(c => c.CityId == cityId).ToListAsync();
        }

        public async Task<IEnumerable<Show>> GetShows(int multiplexId)
        {
            return await _context.Shows
                                 .Include(m => m.Movie)
                                 .Include(m => m.Multiplex)
                                 .Where(m => m.MultiplexId == multiplexId && m.ShowDate.Date >= DateTime.Today.Date)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookings(int userId)
        {
            return await _context.Bookings
                                 .Include(u => u.Show).ThenInclude(m => m.Movie)
                                 .Include(u => u.Show).ThenInclude(m => m.Multiplex)
                                 .Where(u => u.UserId.Equals(userId))
                                 .ToListAsync();
        }

        public async Task<IEnumerable<SeatMaster>> GetSeats(int multiplexId, int showId)
        {
            var bookingList = await _context.Bookings.Where(s => s.ShowId.Equals(showId)).Select(s => s.Id).ToListAsync();
            var bookedSeats = await _context.BookedSeats.Where(s => bookingList.Contains(s.BookingId)).Select(s => s.SeatId).ToListAsync();
            return await _context.MasterSeats.Where(x => !bookedSeats.Contains(x.Id)).ToListAsync();


        }

        public async Task<Multiplex> GetMultiplexe(int multiplexId)
        {
            return await _context.Multiplexes.FirstOrDefaultAsync(x => x.Id.Equals(multiplexId));
        }

        public async Task<Show> GetShow(int showId)
        {
            return await _context.Shows.FirstOrDefaultAsync(x => x.Id.Equals(showId));
        }

        public async Task<Booking> GetBooking(int bookingId)
        {
            return await _context.Bookings.FirstOrDefaultAsync(x => x.Id.Equals(bookingId));
        }

        public async Task<SeatMaster> GetSeat(int seatId)
        {
            return await _context.MasterSeats.FirstOrDefaultAsync(x => x.Id.Equals(seatId));
        }
    }
}
