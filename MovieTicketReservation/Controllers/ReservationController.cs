using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketReservation.Areas.Identity.Data;
using MovieTicketReservation.Models;
using MovieTicketReservation.Services;
using MovieTicketReservation.ViewModels;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet]
        public IActionResult ReserveSeats(int movieShowId)
        {
            

            var reservedSeats = moviecontext.SeatReservations
                          .Where(sr => sr.MovieShowId == movieShowId)
                          .Select(sr => sr.Seat)
                          .ToList();


            var movieShowInfo = (from seatRes in moviecontext.SeatReservations
                                 join movieShowData in moviecontext.MovieShows on seatRes.MovieShowId equals movieShowData.MovieShowId
                                 join hall in moviecontext.Halls on movieShowData.HallId equals hall.HallId
                                 join movie in moviecontext.Movies on movieShowData.MovieId equals movie.MovieId
                                 where seatRes.MovieShowId == movieShowId
                                 select new ReserveSeatsViewModel
                                 {
                                     SeatReservationId = seatRes.SeatReservationId,
                                     ReservationId = seatRes.ReservationId,
                                     Seat = seatRes.Seat,
                                     MovieShowId = movieShowData.MovieShowId,
                                     Price = seatRes.Price,
                                     MovieShowDate = movieShowData.Date,
                                     MovieShowStart = movieShowData.Start,
                                     HallCapacity = hall.Capacity,
                                     HallRows = hall.Rows,
                                     Title = movie.Title,
                                     ReservedSeats = reservedSeats



                                 }).FirstOrDefault();





            if (movieShowInfo == null)
            {
                return StartReservation(movieShowId);


            }


            return View("ReserveSeats", movieShowInfo);
        }

        [Authorize]
        [HttpGet]
        private IActionResult StartReservation(int movieShowId)
        {


            var movieShowEmpty = (from movieShowData in moviecontext.MovieShows
                                  join hall in moviecontext.Halls on movieShowData.HallId equals hall.HallId
                                  join movie in moviecontext.Movies on movieShowData.MovieId equals movie.MovieId
                                  where movieShowData.MovieShowId == movieShowId
                                  select new ReserveSeatsViewModel
                                  {
                                      MovieShowId = movieShowData.MovieShowId,
                                      MovieShowDate = movieShowData.Date,
                                      MovieShowStart = movieShowData.Start,
                                      HallCapacity = hall.Capacity,
                                      HallRows = hall.Rows,
                                      Title = movie.Title

                                  }).FirstOrDefault();

            

            return View("ReserveSeats", movieShowEmpty);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ReserveSeats(IFormCollection form)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var created = Request.Form["Created"];
                var movieShowId = Request.Form["MovieShowId"];
                var selectedSeats = form["Seats"];

                var reservation = new Reservations() { Created = DateTime.Parse(created), UserId = userId };

                moviecontext.Reservations.Add(reservation);
                moviecontext.SaveChanges();

                //itt hozom létre a seatreservationokat amik egy reservationhoz(user) tartoznak
                int reservationId = reservation.ReservationId;

                foreach (var seatNumber in selectedSeats)
                {
                    var seatres = new SeatReservations
                    {
                        ReservationId = reservationId,
                        Seat = int.Parse(seatNumber),
                        MovieShowId = int.Parse(movieShowId),
                        Price = 4

                    };
                    moviecontext.SeatReservations.Add(seatres);
                    moviecontext.SaveChanges();
                }


            }
            catch (Exception ex)
            {

                this._logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public IActionResult ListReservation()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);


            var query = from m1 in moviecontext.SeatReservations
                        join m2 in moviecontext.MovieShows on m1.MovieShowId equals m2.MovieShowId
                        join m3 in moviecontext.Movies on m2.MovieId equals m3.MovieId
                        join m4 in moviecontext.Reservations on m1.ReservationId equals m4.ReservationId
                        where userId == m4.UserId
                        select new UserReservationsViewModel
                        {
                            ReservationDate = m4.Created,
                            MovieTitle = m3.Title,
                            ShowDate = m2.Start,
                            Hall = m2.HallId,
                            ReservedSeat = m1.Seat,
                            Price = m1.Price,
                            CurrentState = m4.CurrentState
                        };
            var queryList = query.ToList();



            return View("ListReservation", queryList);
        }
    }
}


