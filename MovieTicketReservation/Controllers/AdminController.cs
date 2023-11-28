using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketReservation.Areas.Identity.Data;
using MovieTicketReservation.Models;
using MovieTicketReservation.Services;
using System.Data;

namespace MovieTicketReservation.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly MovieTicketReservationContext moviecontext;
        private readonly CSVService _csvService;

        public AdminController(ILogger<AdminController> logger, MovieTicketReservationContext context, CSVService csvservice)
        {
            _logger = logger;
            this.moviecontext = context;
            _csvService = csvservice;

        }

        public IActionResult Index()
        {
            return View();
        }
        
        //MOVIE SHOWS

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditMovieShows()
        {
            var query = from t1 in moviecontext.Movies
                        join t2 in moviecontext.MovieShows on t1.MovieId equals t2.MovieId
                        select new MovieAndScreeningsViewModel
                        {
                            MovieShowId = t2.MovieShowId,
                            MovieTitle = t1.Title,
                            ShowDate = t2.Start,
                            Hall = t2.HallId
                        };

            var queryList = query.ToList();
            return View(queryList);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddShows()
        {


            return View();
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddShows(MovieShows movieshows)
        {
            moviecontext.MovieShows.Add(movieshows);
            moviecontext.SaveChanges();
            return RedirectToAction("EditMovieShows");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteShows(int id)
        {
            var movieshow = moviecontext.MovieShows.Find(id);
            moviecontext.MovieShows.Remove(movieshow);
            moviecontext.SaveChanges();
            return RedirectToAction("EditMovieShows");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditShows(int id)
        {
            var movie = moviecontext.MovieShows.Find(id);
            return View("EditShows", movie);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditShows(MovieShows movieshows)
        {
            moviecontext.Entry(movieshows).State = EntityState.Modified;
            moviecontext.SaveChanges();
            return RedirectToAction("EditShows");
        }

        //MOVIES

        [Authorize(Roles = "Admin")]
        public IActionResult EditMovies()
        {
            return View(moviecontext.Movies.ToList());
        }



        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddMovies()
        {


            return View();
        }

 

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddMovies(Movies movies)
        {
            moviecontext.Movies.Add(movies);
            moviecontext.SaveChanges();
            return RedirectToAction("EditMovies");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var movie = moviecontext.Movies.Find(id);
            return View("Edit", movie);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Movies movie)
        {
            moviecontext.Entry(movie).State = EntityState.Modified;
            moviecontext.SaveChanges();
            return RedirectToAction("EditMovies");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var movie = moviecontext.Movies.Find(id);
            moviecontext.Movies.Remove(movie);
            moviecontext.SaveChanges();
            return RedirectToAction("EditMovies");
        }
    }

    
}
