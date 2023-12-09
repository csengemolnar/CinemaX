using MovieTicketReservation.Areas.Identity.Data;
using MovieTicketReservation.Models;

namespace MovieTicketReservation.Services
{
    public class TableTransformerService
    {
        private MovieTicketReservationContext moviecontext;
        public TableTransformerService(MovieTicketReservationContext context)
        {
            moviecontext=context;
        }



        public async Task updateReservationStatusExpired()
        {
            var currentTime = DateTime.Now;


            var reservationsToUpdate = (from m1 in moviecontext.SeatReservations
                                        join m2 in moviecontext.MovieShows on m1.MovieShowId equals m2.MovieShowId
                                        join m3 in moviecontext.Movies on m2.MovieId equals m3.MovieId
                                        join m4 in moviecontext.Reservations on m1.ReservationId equals m4.ReservationId
                                        where m4.CurrentState == "RESERVED" && currentTime >= m2.Start.AddMinutes(-30)
                                        select m4).ToList();

            foreach (var reservation in reservationsToUpdate)
            {
                reservation.CurrentState = Enum.GetName(typeof(State), State.EXPIRED);
                moviecontext.Update(reservation);
            }
            await moviecontext.SaveChangesAsync();
        }



    }
}
