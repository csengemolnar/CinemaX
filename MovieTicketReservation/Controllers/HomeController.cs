using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketReservation.Areas.Identity.Data;
using MovieTicketReservation.Models;
using MovieTicketReservation.Services;
using System.Diagnostics;
using System.Linq;

namespace MovieTicketReservation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MovieTicketReservationContext moviecontext;
        private readonly CSVService _csvService;


        public HomeController(ILogger<HomeController> logger, MovieTicketReservationContext context, CSVService csvservice)
        {
            _logger = logger;
            this.moviecontext = context;
            _csvService = csvservice;

        }


        public IActionResult Index()
        {
            //import data
            _csvService.ImportMoviesFromCsv("CsvFiles/movies.csv");
            _csvService.ImportHallsFromCsv("CsvFiles/halls.csv");
            _csvService.ImportMovieShowsFromCsv("CsvFiles/movieShows.csv");

            return View(moviecontext.Movies.ToList());
        }

        
        public List<MovieShows> GetMovieShowsByMovieId(int movieId)
        {
            return moviecontext.MovieShows
                .Where(ms => ms.MovieId == movieId)
                .ToList();
        }
        [HttpGet]
        public IActionResult Screenings(int movieId)
        {
            var movie = moviecontext.Movies.Find(movieId);
            var movieShows = GetMovieShowsByMovieId(movieId);
            var viewModel = new ScreeningsViewModel
            {
                Movie = movie,
                MovieShows = movieShows
            };

            return View("Screenings", viewModel);
        }

       




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}