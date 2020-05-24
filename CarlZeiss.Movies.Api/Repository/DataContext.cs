using CarlZeiss.Movies.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarlZeiss.Movies.Api.Repository
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Multiplex> Multiplexes { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<BookedSeat> BookedSeats { get; set; }
        public DbSet<SeatMaster> MasterSeats { get; set; }
        public DbSet<Booking> Bookings { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Show>()
            .HasIndex(p => new { p.MovieId, p.MultiplexId, p.ShowDate }).IsUnique();

            builder.Entity<Show>()
                .Property(p => p.ShowDate)
                .HasColumnType("date");

            builder.Entity<BookedSeat>()
                .HasKey(k => k.BookingId);

            builder.Entity<BookedSeat>()
                .HasOne(b => b.Bookings)
                .WithMany(p => p.Seats)
                .HasForeignKey(k => k.BookingId);

            builder.Entity<Booking>()
               .HasOne(b => b.User)
               .WithMany(p => p.Bookings)
               .HasForeignKey(k => k.UserId)
               .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Booking>()
               .HasOne(b => b.Show)
               .WithMany(p => p.Bookings)
               .HasForeignKey(k => k.ShowId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Booking>()
                .Property(p => p.BookingDate)
                .HasColumnType("datetime");

        }
    }

}
