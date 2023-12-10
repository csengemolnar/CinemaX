using MovieTicketReservation.Models;
using System.Xml.Serialization;

namespace MovieTicketReservation.ViewModels
{
    //public class ExportWeeklyProgramViewModel
    //{
    //    public DateTime WeekStarting { get; set; }
    //    public Dictionary<DateTime, List<ScreeningViewModel>> ScreeningsByDay { get; set; }
    //}

    public class ExportWeeklyProgramViewModel
    {
        public DateTime WeekStarting { get; set; }
        public List<ScreeningDay>? ScreeningsByDay { get; set; }
    }

    public class ScreeningDay
    {
        public DateTime Date { get; set; }
        public List<ScreeningViewModel>? Screenings { get; set; }
    }

    public class ScreeningViewModel
    {
        public DateTime ShowTime { get; set; }
        public int Hall { get; set; } 
        public string Title { get; set; }
    }
}
