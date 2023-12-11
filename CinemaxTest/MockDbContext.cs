using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTicketReservation.Areas.Identity.Data;
using MovieTicketReservation.Models;

namespace CinemaxTest
{
    public static class MockDbContext
    {
        public static void Initialize(MovieTicketReservationContext context)
        {
            context.Database.EnsureCreated();

           
            if (context.Movies.Any())
            {
                return; 
            }
            var halls = new Halls[]
            {
                new Halls{ HallId=1, Capacity=100, Rows=10},
                new Halls{ HallId=2, Capacity=100, Rows=10},
                new Halls{ HallId=3, Capacity=100, Rows=10},
                new Halls{ HallId=4, Capacity=100, Rows=10},
                new Halls{ HallId=5, Capacity=100, Rows=10},
                new Halls{ HallId=6, Capacity=100, Rows=10},
                new Halls{ HallId=7, Capacity=100, Rows=10},
                new Halls{ HallId=8, Capacity=100, Rows=10},
                new Halls{ HallId=9, Capacity=100, Rows=10},
                new Halls{ HallId=10, Capacity=100, Rows=10},
            };

            var movies = new Movies[]
            {
            new Movies {Title="The Trial", Actors="Antony Perkins", MovieLength=120, Genre="Drama",Director="Orson Welles", ReleaseDate=DateTime.Parse("1972-03-15 00:00:00"), Description="An unassuming office worker is arrested and stands trial, but he is never made aware of his charges.", ThumbNail="ThumbNail/MockPhoto.jpg"},
            new Movies {Title="Leave the World Behind", Actors="Julia Roberts", MovieLength=176, Genre="Drama,Mystery",Director="Sam ESmail", ReleaseDate=DateTime.Parse("2023-06-06 00:00:00"), Description="A family's getaway to a luxurious rental home takes an ominous turn when a cyberattack knocks out their devices, and two strangers appear at their door.\r\n", ThumbNail="ThumbNail/MockPhoto.jpg"},

            
            };
            foreach (Movies m in movies)
            {
                context.Movies.Add(m);
            }
            context.SaveChanges();

            var movieShows = new MovieShows[]
            {
            new MovieShows {Date=DateTime.Parse("2023.12.20. 00:00:00"),HallId=1,MovieId=1,Start=DateTime.Parse("2023.12.20. 20:30:00")},
            new MovieShows {Date=DateTime.Parse("2023.12.20. 00:00:00"),HallId=2,MovieId=2,Start=DateTime.Parse("2023.12.20. 21:30:00")},

           
            };
            foreach (MovieShows ms in movieShows)
            {
                context.MovieShows.Add(ms);
            }
            context.SaveChanges();

           
        }
    }
}
