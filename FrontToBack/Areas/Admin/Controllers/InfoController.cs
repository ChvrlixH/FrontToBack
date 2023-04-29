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
            List<Info> faq = _context.FAQ.ToList();

            return View(faq);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Info info)
        {
            _context.FAQ.Add(info);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
