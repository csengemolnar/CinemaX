using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketReservation.Areas.Identity.Data;
using MovieTicketReservation.Models;
using MovieTicketReservation.Services;
using MovieTicketReservation.ViewModels;

namespace MovieTicketReservation.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly MovieTicketReservationContext moviecontext;
        private readonly CSVService _csvService;


        public ReservationController(ILogger<ReservationController> logger, MovieTicketReservationContext context, CSVService csvservice)
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

        [HttpGet]
        public IActionResult ReserveSeats(int movieShowId)
        {
            var movieShow = moviecontext.MovieShows.Find(movieShowId);

            var movieShowInfo = from seatRes in moviecontext.SeatReservations
                        join moviecontext in moviecontext.MovieShows on seatRes.MovieShowId equals movieShow.MovieShowId
                        join hall in moviecontext.Halls on movieShow.HallId equals hall.HallId
                        join movie in moviecontext.Movies on movieShow.MovieId equals movie.MovieId
                        where seatRes.MovieShowId == movieShowId
                        select new ReserveSeatsViewModel
                        {
                            SeatReservationId = seatRes.SeatReservationId,
                            ReservationId = seatRes.ReservationId,
                            SeatId = seatRes.SeatId,
                            MovieShowId = seatRes.MovieShowId,
                            Price = seatRes.Price,
                            MovieShowDate = movieShow.Date,
                            MovieShowStart = movieShow.Start,
                            HallCapacity = hall.Capacity,
                            HallRows = hall.Rows,
                            Title = movie.Title
                        };

            return View("ReserveSeats", movieShowInfo);
        }
    }
}


