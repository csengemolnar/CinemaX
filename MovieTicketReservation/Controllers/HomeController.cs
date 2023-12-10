using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketReservation.Areas.Identity.Data;
using MovieTicketReservation.Models;
using MovieTicketReservation.Services;
using System.Diagnostics;
using System.Linq;
using MovieTicketReservation.ViewModels;
using System.Text;
using System.Xml.Serialization;

namespace MovieTicketReservation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MovieTicketReservationContext moviecontext;
        private readonly CSVService _csvService;
        private readonly CinemaProgramExporterService _cinemaexpservice;
        private readonly IWebHostEnvironment _environment;


        public HomeController(ILogger<HomeController> logger, MovieTicketReservationContext context, CSVService csvservice, CinemaProgramExporterService cinemaexpservice, IWebHostEnvironment environment)
        {
            _logger = logger;
            this.moviecontext = context;
            _csvService = csvservice;
            _cinemaexpservice = cinemaexpservice;
            _environment = environment;
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


        [HttpPost]
        public async Task<IActionResult> ExportCinemaProgram()
        {

            try
            {
                var weeklyProgram = _cinemaexpservice.GetWeeklyCinemaProgram();
                var xmlSerializer = new XmlSerializer(typeof(ExportWeeklyProgramViewModel));
                var fileName = $"WeeklyCinemaProgram_{DateTime.Now:yyyyMMddHHmm}.xml";
                var folderPath = Path.Combine(_environment.WebRootPath, "exports");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    xmlSerializer.Serialize(fileStream, weeklyProgram);
                }

                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                // Set the file for download
                //return File(fileBytes, "application/xml", fileName);

                var fileUrl = $"{Request.Scheme}://{Request.Host}/exports/{fileName}";
                Response.ContentType = "application/xml";
                Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
                Response.Headers["Pragma"] = "no-cache";
                Response.Headers["Expires"] = "0";

                // Add content disposition for direct download
                Response.Headers.Add("Content-Disposition", $"attachment; filename={fileName}");

                return File(fileBytes, "application/xml", fileName);

                //return Json(new { success = true, url = fileUrl });
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError(ex, "Error exporting cinema program");

                return Json(new { success = false, message = "Error occurred while exporting the cinema program." });
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }



        
    
}