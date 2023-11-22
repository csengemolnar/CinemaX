using Microsoft.AspNetCore.Mvc;

namespace MovieTicketReservation.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
