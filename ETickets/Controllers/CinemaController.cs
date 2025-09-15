using AspNetCoreGeneratedDocument;
using ETickets.Data;
using ETickets.Models;
using Microsoft.AspNetCore.Mvc;

namespace ETickets.Controllers
{
    public class CinemaController : Controller
    {
        private AppDbContext _context;
        private Repository<Cinema> _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CinemaController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _repository = new Repository<Cinema>(context);
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync(new QueryOptions<Cinema>() { Includes = "Movies"}));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repository.DeleteByIdAsync(id);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Cinema not found");
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            if (id == 0)
            {
                ViewBag.Operation = "Add";
                return View(new Cinema());
            }
            else
            {
                ViewBag.Operation = "Edit";
                var cinema = await _repository.GetByIdAsync(id, new QueryOptions<Cinema>());
                if (cinema == null) return View("NotFound");
                ViewBag.Operation = "Edit";
                return View(cinema);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(Cinema cinema, int id)
        {
            if(!ModelState.IsValid)
            {
                if (id == 0)
                    ViewBag.Operation = "Add";
                else
                    ViewBag.Operation = "Edit";
                return View(cinema);
            }
            if (id == 0)
            {
                if (cinema.File != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + cinema.File.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await cinema.File.CopyToAsync(fileStream);
                    }
                    cinema.Logo = uniqueFileName;
                }
                await _repository.AddAsync(cinema);
                return RedirectToAction("Index");
            }
            else
            {
                var existingCinema = await _repository.GetByIdAsync(id, new QueryOptions<Cinema>());
                if (existingCinema == null) return View("NotFound");
                existingCinema.Name = cinema.Name;
                existingCinema.Description = cinema.Description;
                if (cinema.File != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + cinema.File.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await cinema.File.CopyToAsync(fileStream);
                    }
                    existingCinema.Logo = uniqueFileName;
                }
                await _repository.UpdateAsync(existingCinema);
                return RedirectToAction("Index");
            }
        }
    }
}
