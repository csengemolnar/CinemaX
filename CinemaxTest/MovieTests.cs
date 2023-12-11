using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieTicketReservation.Areas.Identity.Data;
using MovieTicketReservation.Controllers;
using MovieTicketReservation.Models;
using MovieTicketReservation.Services;

namespace CinemaxTest
{
    public class MovieTests 
    {
        private  MovieTicketReservationContext _context;
        
        public MovieTests()
        {
            var options = new DbContextOptionsBuilder<MovieTicketReservationContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase") 
            .Options;
          

            MockDbContext.Initialize(_context);
        }

        [Fact]
        public void GetAllMovies_ShouldReturnMovies()
        {
            // Arrange  done in the constructor

            // Act
            var movies = _context.Movies.ToList();

            // Assert
            Assert.NotNull(movies);
            Assert.True(movies.Any(), "No movies were found in the database.");

            
            Assert.True(movies.Any(m => m.Title == "The Trial"), "The movie 'The Trial' was not found.");
            Assert.True(movies.Any(m => m.Title == "Leave the World Behind"), "The movie 'Leave the World Behind' was not found.");
        }
        //[Fact]
        //public void GetAllMovieShows_ShouldReturnMovieShows()
        //{
        //    // Arrange  done in the constructor

        //    // Act
        //    var movieshows = _context.MovieShows.ToList();

        //    // Assert
        //    Assert.NotNull(movieshows);
        //    Assert.True(movieshows.Any(), "No moviesshows were found in the database.");

        //    Assert.True(movieshows.Any(m => m.MovieShowId == 1), "No movie show found with ID 1");
        //    Assert.True(movieshows.Any(m => m.MovieShowId == 2), "No movie show found with ID 2");
        //}

    }
    
}