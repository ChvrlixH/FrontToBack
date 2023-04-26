using FrontToBack.Database;
using FrontToBack.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBack.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDb _context;

        public HomeController(AppDb context)
        {
            _context = context;
        }
    
        public IActionResult Index()
        {
            List<Info> FAQ = _context.FAQ.ToList();
            return View(FAQ);
        }
    }
}
