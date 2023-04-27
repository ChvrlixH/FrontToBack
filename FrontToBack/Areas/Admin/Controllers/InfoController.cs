using Microsoft.AspNetCore.Mvc;

namespace FrontToBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InfoController : Controller
    {
        private readonly AppDb _context;

        public InfoController(AppDb context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Info> FAQ = _context.FAQ.ToList();

            return View(FAQ);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(string image, string title, string description)
        {
            return View();
        }
    }
}
