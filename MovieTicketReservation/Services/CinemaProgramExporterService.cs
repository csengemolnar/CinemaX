using MovieTicketReservation.Areas.Identity.Data;
using MovieTicketReservation.ViewModels;
using System.Text;
using System.Xml.Serialization;

namespace MovieTicketReservation.Services
{
    public class CinemaProgramExporterService
    {
        private readonly MovieTicketReservationContext moviecontext;

        public CinemaProgramExporterService(MovieTicketReservationContext context)
        {
            moviecontext = context;
        }


        public ExportWeeklyProgramViewModel GetWeeklyCinemaProgram()
        {
            var weekStart = DateTime.Now.Date;
            var weekEnd = weekStart.AddDays(7);

            var screenings = from m1 in moviecontext.Movies
                             join m2 in moviecontext.MovieShows on m1.MovieId equals m2.MovieId
                             join m3 in moviecontext.Halls on m2.HallId equals m3.HallId
                             where m2.Start >= weekStart && m2.Start < weekEnd
                             select new { m2.Start, Hall = m3.HallId, m1.Title };

            var screeningsGrouped = screenings
                .AsEnumerable()
                .GroupBy(s => s.Start.Date)
                .Select(group => new ScreeningDay
                {
                    Date = group.Key,
                    Screenings = group.Select(s => new ScreeningViewModel
                    {
                        ShowTime = s.Start,
                        Hall = s.Hall,
                        Title = s.Title
                    }).ToList()
                }).ToList();

            return new ExportWeeklyProgramViewModel
            {
                WeekStarting = weekStart,
                ScreeningsByDay = screeningsGrouped
            };
        }

        public void ExportCinemaProgramToXml(ExportWeeklyProgramViewModel program, string filePath)
        {
            var serializer = new XmlSerializer(typeof(ExportWeeklyProgramViewModel));

            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                serializer.Serialize(writer, program);
            }
        }


    }
}
