using CarlZeiss.Movies.Api.Models;
using CarlZeiss.Movies.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarlZeiss.Movies.Api.Test.Mock
{
    class MovieBookingRepositoryMock : IMovieBookingRepository
    {
        Dictionary<Type, object> dataDictionary = null;

        public MovieBookingRepositoryMock()
        {
            dataDictionary = new Dictionary<Type, object>();

            dataDictionary.Add(typeof(User), DummyData.Users);
            dataDictionary.Add(typeof(City), DummyData.Cities);
            dataDictionary.Add(typeof(Multiplex), DummyData.Multiplexes);
            dataDictionary.Add(typeof(SeatMaster), DummyData.MasterSeats);
            dataDictionary.Add(typeof(Movie), DummyData.Movies);
            dataDictionary.Add(typeof(Show), DummyData.Shows);
            dataDictionary.Add(typeof(Booking), DummyData.Bookings);
            dataDictionary.Add(typeof(BookedSeat), DummyData.BookedSeats);
        }

        public void Add<T>(T entity) where T : class
        {
            Type type = typeof(T);
            List<T> data = (List<T>)dataDictionary[type];
            data.Add(entity);

        }

        public void Delete<T>(T entity) where T : class
        {
            Type type = typeof(T);
            List<T> data = (List<T>)dataDictionary[type];
            data.Remove(entity);
        }

        public async Task<Booking> GetBooking(int bookingId)
        {
            return DummyData.Bookings.AsEnumerable().FirstOrDefault(x => x.Id == bookingId);
        }

        public async Task<IEnumerable<Booking>> GetBookings(int userId)
        {
            return DummyData.Bookings.AsEnumerable().Where(u => u.UserId == userId).ToList();
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            return DummyData.Cities.AsEnumerable();
        }

        public async Task<Multiplex> GetMultiplexe(int multiplexId)
        {
            return DummyData.Multiplexes.AsEnumerable().FirstOrDefault(x => x.Id == multiplexId);
        }

        public async Task<IEnumerable<Multiplex>> GetMultiplexes(int cityId)
        {
            return DummyData.Multiplexes.AsEnumerable().Where(u => u.CityId == cityId).ToList();
        }

        public async Task<SeatMaster> GetSeat(int seatId)
        {
            return DummyData.MasterSeats.AsEnumerable().FirstOrDefault(x => x.Id == seatId);
        }

        public async Task<IEnumerable<SeatMaster>> GetSeats(int multiplexId, int showId)
        {
            return DummyData.MasterSeats.AsEnumerable().Where(u => u.MultiplexId == multiplexId).ToList();
        }

        public async Task<Show> GetShow(int showId)
        {
            return DummyData.Shows.AsEnumerable().FirstOrDefault(x => x.Id == showId);
        }

        public async Task<IEnumerable<Show>> GetShows(int multiplexId)
        {
            return DummyData.Shows.AsEnumerable().Where(u => u.MultiplexId == multiplexId).ToList();
        }

        public async Task<bool> SaveAll()
        {
            return true;
        }
    }
}
