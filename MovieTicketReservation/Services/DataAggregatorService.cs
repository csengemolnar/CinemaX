using MovieTicketReservation.Areas.Identity.Data;
using MovieTicketReservation.Models;
using System.Numerics;

namespace MovieTicketReservation.Services
{
    public class DataAggregatorService
    {
        private  MovieTicketReservationContext _context;
        public DataAggregatorService(MovieTicketReservationContext context)
        {
            _context = context;
            
        }

        public Dictionary<string, int> GetScreeningsCount(DateTime startDate, DateTime endDate)
        {

            var result = from m1 in _context.Movies
                        join m2 in _context.MovieShows on m1.MovieId equals m2.MovieId
                        where m2.Date >= startDate && m2.Date <= endDate
                        group m2 by m1.Title into grouped
                        select new
                        {
                            MovieTitle = grouped.Key,
                            ScreeningsCount = grouped.Count()
                        };

            return result.ToDictionary(x => x.MovieTitle, x => x.ScreeningsCount);
        }

        public Dictionary<DateTime,int> GetScreeningsByDate(int movieId, DateTime startDate, DateTime endDate)
        {

            //var result = from m1 in _context.Movies
            //             join m2 in _context.MovieShows on m1.MovieId equals m2.MovieId
            //             join m3 in _context.SeatReservations on m2.MovieShowId equals m3.MovieShowId
            //             where m2.Date >= startDate && m2.Date <= endDate && m1.MovieId ==movieId
            //             group m3.Seat by m2.Date into seatResGroup
            //             select new
            //             {
            //                 Date = seatResGroup.Key,
            //                 Count = seatResGroup.Count()
            //             };




            //var queryResult = result.ToList(); 
            //if (!queryResult.Any())
            //{
            //    return new Dictionary<DateTime, int>(); 
            //}
            //return queryResult.ToDictionary(x => x.Date, x => x.Count);


            var result = from m1 in _context.Movies
                         join m2 in _context.MovieShows on m1.MovieId equals m2.MovieId
                         join m3 in _context.SeatReservations on m2.MovieShowId equals m3.MovieShowId
                         where m2.Date.Date >= startDate.Date && m2.Date.Date <= endDate.Date
                               && m1.MovieId == movieId
                         group m3 by m2.Date.Date into seatResGroup 
                         select new
                         {
                             Date = seatResGroup.Key,
                             Count = seatResGroup.Count() 
                         };

            var queryResult = result.ToList();
            return queryResult.Any()
                   ? queryResult.ToDictionary(x => x.Date, x => x.Count)
                   : new Dictionary<DateTime, int>();
        }




    }
}
