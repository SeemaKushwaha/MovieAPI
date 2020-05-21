using CarlZeiss.Movies.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarlZeiss.Movies.Api.Repository
{
    public interface IMovieBookingRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();

        Task<IEnumerable<City>> GetCities();
        
        Task<IEnumerable<Multiplex>> GetMultiplexes(int cityId);
        Task<Multiplex> GetMultiplexe(int multiplexId);

        Task<IEnumerable<Show>> GetShows(int multiplexId);
        Task<Show> GetShow(int showId);

        Task<IEnumerable<Booking>> GetBookings(int userId);
        Task<Booking> GetBooking(int bookingId);


        Task<IEnumerable<SeatMaster>> GetSeats(int multiplexId, int showId);
        Task<SeatMaster> GetSeat(int seatId);
    }
}
