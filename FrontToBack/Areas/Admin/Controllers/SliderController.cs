using FrontToBack.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack.Areas.Admin.Controllers
{
   [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDb _appDb;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SliderController(AppDb appDb, IWebHostEnvironment webHostEnvironment)
        {
            _appDb = appDb;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Slider> sliders = _appDb.Sliders.AsEnumerable();

            ViewBag.Count = sliders.Count();

            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SliderVM sliderVM)
        {
            if (!ModelState.IsValid)
                return View();

            if (sliderVM.Image == null)
            {
                ModelState.AddModelError("Image", "Image bos olmamalidir.");
                return View();
            }
          
            if (sliderVM.Image.Length / 1024 > 100)
            {
                ModelState.AddModelError("Image", "Faylin hecmi 100 kb-dan kicik olmamalidir.");
                return View();
            }
            if (!sliderVM.Image.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image", "Faylin tipi image olmalidir.");
                return View();
            }

            string fileName = $"{Guid.NewGuid()}-{sliderVM.Image.FileName}";
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "admin","images", "faces", fileName);
            using (FileStream stream = new(path, FileMode.Create))
            {
                await sliderVM.Image.CopyToAsync(stream);
            }

            Slider slider = new()
            {
                Name = sliderVM.Name,
                Description = sliderVM.Description,
                Offer = sliderVM.Offer,
                Image = fileName
            };

            await _appDb.Sliders.AddAsync(slider);
            await _appDb.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Read(int id)
        {
            Slider? slider = _appDb.Sliders.AsNoTracking().FirstOrDefault(s => s.Id == id);
            if (slider is null)
                return NotFound();

            return View(slider);
        }

        public IActionResult Delete(int id)
        {
            Slider? slider = _appDb.Sliders.FirstOrDefault(s => s.Id == id);
            if (slider is null)
                return NotFound();

            return View(slider);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Deleteslider(int id)
        {
            Slider? slider = _appDb.Sliders.FirstOrDefault(s => s.Id == id);
            if (slider is null)
                return NotFound();

            _appDb.Sliders.Remove(slider);
            _appDb.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            Slider? slider = _appDb.Sliders.FirstOrDefault(s => s.Id == id);
            if (slider is null)
                return NotFound();

            return View(slider);
        }


        [HttpPost]
        public IActionResult Update(Slider slider, int id)
        {
            Slider? dBslider = _appDb.Sliders.AsNoTracking().FirstOrDefault(s => s.Id == id);
            if (slider is null)
                return NotFound();

            IEnumerable<Slider> sliders = _appDb.Sliders.AsEnumerable();
            foreach (var sliderItem in sliders)
            {
                if (sliderItem.Name == slider.Name)
                {
                    ModelState.AddModelError("Name", "This name is available");
                    return View();
                }
            }

            _appDb.Sliders.Update(slider);
            _appDb.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

