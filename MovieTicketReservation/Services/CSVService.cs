using Microsoft.CodeAnalysis.Elfie.Serialization;
using MovieTicketReservation.Models;
using System.Globalization;
using System;
using MovieTicketReservation.Areas.Identity.Data;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;

namespace MovieTicketReservation.Services
{
    public class CSVService
    {
        private readonly MovieTicketReservationContext _context;

        public CSVService(MovieTicketReservationContext context)
        {
            _context = context;
        }
        public void ImportMoviesFromCsv(string filePath)
        {
            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvHelper.CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";"
                }))
                {

                    var records = csv.GetRecords<Movies>().ToList();

                    if (!_context.Movies.Any())
                    {
                        //_context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Movies', RESEED, 1)");
                        _context.Movies.AddRange(records);
                        _context.SaveChanges();
                        Console.WriteLine("Import is successful");
                        
                    }
                    else
                    {
                        Console.WriteLine("Table is not empty");


                    }

                }
                

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error importing Movies: {ex.Message}");
            }

        }

        public void ImportHallsFromCsv(string filePath)
        {


            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvHelper.CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";"
                }))
                {

                    var records = csv.GetRecords<Halls>().ToList();

                    if (!_context.Halls.Any())
                    {
                        //_context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Halls', RESEED, 1)");
                        _context.Halls.AddRange(records);
                        _context.SaveChanges();
                        Console.WriteLine("Import is successful");
                    }
                    else
                    {
                        Console.WriteLine("Table is not empty");
                    }

                }


            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error importing Movies: {ex.Message}");
            }
        }

            public void ImportMovieShowsFromCsv(string filePath)
        {
            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvHelper.CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";"
                }))
                {

                    var records = csv.GetRecords<MovieShows>().ToList();

                    if (!_context.MovieShows.Any())
                    {
                        //_context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('MovieShows', RESEED, 0)");
                        _context.MovieShows.AddRange(records);
                        _context.SaveChanges();
                        Console.WriteLine("Import is successful");
                    }
                    else
                    {
                        Console.WriteLine("Table is not empty");
                    }

                }


            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error importing Movies: {ex.Message}");
            }
        }
    }
}
