using CarlZeiss.Movies.Api.Dtos;
using CarlZeiss.Movies.Api.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarlZeiss.Movies.Api.Test.Mock
{
    public static class DummyData
    {
        
        public static List<User> Users = new List<User>() {
            new User() { Id=1, Username= "sana", Role= "Admin", PasswordHash=new byte[] { }, PasswordSalt= new byte[] { } },
            new User() { Id=2, Username= "seema", Role= "User", PasswordHash=new byte[] { }, PasswordSalt= new byte[] { } }
        };

        public static List<City> Cities = new List<City>() {
            new City() { Id=1, Name="Chandigarh"},
            new City() { Id=2, Name="Bengaluru"}
        };

        public static List<Multiplex> Multiplexes = new List<Multiplex>() {
            new Multiplex() { Id=1, MultiplexName="InoxA", CityId=1},
            new Multiplex() { Id=2, MultiplexName="WaveA", CityId=1},
            new Multiplex() { Id=3, MultiplexName="InoxB", CityId=2},
            new Multiplex() { Id=4, MultiplexName="WaveB", CityId=2}
        };

        public static List<SeatMaster> MasterSeats = new List<SeatMaster>(){
            new SeatMaster() {Id=1, SeatNo=101, MultiplexId=1 },
            new SeatMaster() {Id=2, SeatNo=102, MultiplexId=1 },
            new SeatMaster() {Id=3, SeatNo=103, MultiplexId=1 },
            new SeatMaster() {Id=4, SeatNo=104, MultiplexId=1 },
            new SeatMaster() {Id=5, SeatNo=105, MultiplexId=1 },
            new SeatMaster() {Id=6, SeatNo=106, MultiplexId=1 },
            new SeatMaster() {Id=7, SeatNo=107, MultiplexId=1 },
            new SeatMaster() {Id=8, SeatNo=108, MultiplexId=1 },
            new SeatMaster() {Id=9, SeatNo=109, MultiplexId=1 },
            new SeatMaster() {Id=10, SeatNo=110, MultiplexId=1 },
            new SeatMaster() {Id=11, SeatNo=201, MultiplexId=2 },
            new SeatMaster() {Id=12, SeatNo=202, MultiplexId=2 },
            new SeatMaster() {Id=13, SeatNo=303, MultiplexId=2 },
            new SeatMaster() {Id=14, SeatNo=204, MultiplexId=2 },
            new SeatMaster() {Id=15, SeatNo=205, MultiplexId=2 },
            new SeatMaster() {Id=16, SeatNo=206, MultiplexId=2 },
            new SeatMaster() {Id=17, SeatNo=207, MultiplexId=2 },
            new SeatMaster() {Id=18, SeatNo=208, MultiplexId=2 },
            new SeatMaster() {Id=19, SeatNo=209, MultiplexId=2 },
            new SeatMaster() {Id=20, SeatNo=210, MultiplexId=2 },
            new SeatMaster() {Id=21, SeatNo=301, MultiplexId=3 },
            new SeatMaster() {Id=22, SeatNo=302, MultiplexId=3 },
            new SeatMaster() {Id=23, SeatNo=303, MultiplexId=3 },
            new SeatMaster() {Id=24, SeatNo=304, MultiplexId=3 },
            new SeatMaster() {Id=25, SeatNo=305, MultiplexId=3 },
            new SeatMaster() {Id=26, SeatNo=306, MultiplexId=4 },
            new SeatMaster() {Id=27, SeatNo=307, MultiplexId=4 },
            new SeatMaster() {Id=28, SeatNo=308, MultiplexId=4 },
            new SeatMaster() {Id=29, SeatNo=309, MultiplexId=5 },
            new SeatMaster() {Id=30, SeatNo=310, MultiplexId=5 },
        };

        public static List<Movie> Movies = new List<Movie>() {
            new Movie() {Id=1, MovieName="Rain", Genre="Action",Language="Hindi" },
            new Movie() {Id=2, MovieName="Rainbow", Genre="Action",Language="English" },
            new Movie() {Id=3, MovieName="Mitti", Genre="Drama",Language="Hindi" },
            new Movie() {Id=4, MovieName="Boat", Genre="Drama",Language="English" },
            new Movie() {Id=5, MovieName="Earth", Genre="Comedy",Language="Hindi" },
            new Movie() {Id=6, MovieName="Sky", Genre="Comedy",Language="English" },
            new Movie() {Id=7, MovieName="Dhol", Genre="Comedy",Language="Punjabi" },
        };

        public static List<Show> Shows = new List<Show>() {
            new Show() { Id=1, MovieId=1, MultiplexId=1, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=2, MovieId=1, MultiplexId=2, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=3, MovieId=1, MultiplexId=3, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=4, MovieId=1, MultiplexId=4, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=5, MovieId=2, MultiplexId=1, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=6, MovieId=2, MultiplexId=2, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=7, MovieId=2, MultiplexId=3, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=8, MovieId=2, MultiplexId=4, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=9, MovieId=3, MultiplexId=1, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=10, MovieId=3, MultiplexId=2, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=11, MovieId=3, MultiplexId=3, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=12, MovieId=3, MultiplexId=4, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=13, MovieId=4, MultiplexId=1, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=14, MovieId=4, MultiplexId=2, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=15, MovieId=4, MultiplexId=3, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=16, MovieId=4, MultiplexId=4, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=17, MovieId=5, MultiplexId=1, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=18, MovieId=5, MultiplexId=2, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=19, MovieId=5, MultiplexId=3, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=20, MovieId=5, MultiplexId=4, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=21, MovieId=6, MultiplexId=1, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=22, MovieId=6, MultiplexId=2, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=23, MovieId=6, MultiplexId=3, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=24, MovieId=6, MultiplexId=4, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=25, MovieId=7, MultiplexId=1, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=26, MovieId=7, MultiplexId=2, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=27, MovieId=7, MultiplexId=3, ShowDate = DateTime.Today.AddDays(5).Date },
            new Show() { Id=28, MovieId=7, MultiplexId=4, ShowDate = DateTime.Today.AddDays(5).Date }
        };
        
        public static List<Booking> Bookings = new List<Booking>() {
            new Booking() { Id=1, UserId=2, ShowId=1, BookingDate=DateTime.Now }
        };

        public static List<BookedSeat> BookedSeats = new List<BookedSeat>();

    }
}
