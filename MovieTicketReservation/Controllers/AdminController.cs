using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketReservation.Areas.Identity.Data;
using MovieTicketReservation.Models;
using MovieTicketReservation.Services;
using System.Data;
using MovieTicketReservation.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
            var viewModel = new EditShowsAndDisplayMoviesViewModel
            { 
                Movies = moviecontext.Movies,
                Halls = moviecontext.Halls
            };

            return View(viewModel);
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
            var movieShows = moviecontext.MovieShows.Find(id);
            var viewModel = new EditShowsAndDisplayMoviesViewModel
            {
                MovieShows = movieShows,
                Movies = moviecontext.Movies,
                Halls=moviecontext.Halls
            };

            return View("EditShows", viewModel);
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditReservations()
        {
            var query = from m1 in moviecontext.SeatReservations
                        join m2 in moviecontext.MovieShows on m1.MovieShowId equals m2.MovieShowId
                        join m3 in moviecontext.Movies on m2.MovieId equals m3.MovieId
                        join m4 in moviecontext.Reservations on m1.ReservationId equals m4.ReservationId
                        join m5 in moviecontext.Users on m4.UserId equals m5.Id
                        group m1 by new
                        {
                            m4.ReservationId,
                            m5.Email,
                            m3.Title,
                            m2.Start,
                            m2.HallId,
                            m4.CurrentState
                        } into grouped
                        select new AllReservationsViewModel
                        {
                            ReservationId = grouped.Key.ReservationId,
                            ReserverEmail = grouped.Key.Email,
                            MovieTitle = grouped.Key.Title,
                            ShowDate = grouped.Key.Start,
                            Hall = grouped.Key.HallId,
                            Seats = grouped.Select(g => new SeatReservationInfo
                            {
                                SeatReservationId = g.SeatReservationId,
                                ReservedSeat = g.Seat
                                
                            }).ToList(),
                            Price = grouped.Sum(g => g.Price), 
                            CurrentState = grouped.Key.CurrentState
                        };


            var queryList = query.ToList();

            return View("EditReservation", queryList);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult UpdateStatus(int reservationId)
        {
            var reservationToUpdate = moviecontext.Reservations
                                            .FirstOrDefault(r => r.ReservationId == reservationId);

            if (reservationToUpdate != null)
            {

                reservationToUpdate.CurrentState = Enum.GetName(typeof(State), State.PURCHASED);

                moviecontext.SaveChanges();

                return Json(new { success = true, message = "Status updated successfully" });

            }

            return Json(new { success = false, message = "Reservation not found" });
        }

        //STATISTICS

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ScreeningStatistics()
        {
            return View("ScreeningStatistics");           
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult ScreeningStatistics([FromBody] ScreeningStatisticsViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            
            DateTime startDate = model.StartDate;
            DateTime endDate = model.EndDate;

            var DataAggregatorService = new DataAggregatorService(moviecontext);
            var screeningsData = DataAggregatorService.GetScreeningsCount(startDate, endDate);
            var formattedData = screeningsData.Select(kvp => new
            {
                name = kvp.Key,
                y = kvp.Value

            }).ToList();

            return Json(formattedData);
        }

        //VIEWERSHIP TRENDS LINE CHART

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ViewershipTrends()
        {
            return View("ViewershipTrends", moviecontext.Movies.ToList());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult ViewershipTrends([FromBody] ViewershipViewModel model)
        {
            var DataAggregatorService = new DataAggregatorService(moviecontext);
            var screeningsData = DataAggregatorService.GetScreeningsByDate(model.MovieId, model.StartDate, model.EndDate);
            var formattedData = screeningsData.Select(kvp => new
            {
                name = kvp.Key,
                y = kvp.Value

            }).ToList();

            return Json(formattedData);
        }
    }
}
