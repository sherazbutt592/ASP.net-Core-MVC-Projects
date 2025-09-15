using ETickets.Data;
using ETickets.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ETickets.Controllers
{
    public class ProducerController : Controller
    {
        AppDbContext _dbContext;
        Repository<Producer> _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProducerController(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _repository = new Repository<Producer>(dbContext);
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync(new QueryOptions<Producer> { Includes = "Movies" }));
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
                ModelState.AddModelError("", "Producer not found");
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var producer = await _repository.GetByIdAsync(id, new QueryOptions<Producer> { Includes = "Movies" });
            if (producer == null) return View("NotFound");
            return View(producer);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            if(!ModelState.IsValid)
            {
                return View("NotFound");
            }
            if (id == 0)
            {
                ViewBag.Operation = "Add";
                return View(new Producer());
            }
            else
            {
                ViewBag.Operation = "Edit";
                var producer = await _repository.GetByIdAsync(id, new QueryOptions<Producer>());
                if (producer == null) return View("NotFound");
                ViewBag.Operation = "Edit";
                return View(producer);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(Producer producer, int id)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Operation = id == 0 ? "Add" : "Edit";
                return View(producer);
            }
            if (id == 0)
            {
                if (producer.File != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + producer.File.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await producer.File.CopyToAsync(fileStream);
                    }
                    producer.ProfilePicture = uniqueFileName;
                }
                await _repository.AddAsync(producer);
                return RedirectToAction("Index");
            }
            else
            {
                var existingProducer = await _repository.GetByIdAsync(id, new QueryOptions<Producer>());
                if (existingProducer == null) return View("NotFound");
                existingProducer.FullName = producer.FullName;
                existingProducer.Bio = producer.Bio;
                if (producer.File != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + producer.File.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await producer.File.CopyToAsync(fileStream);
                    }
                    existingProducer.ProfilePicture = uniqueFileName;
                }
                await _repository.UpdateAsync(existingProducer);
                return RedirectToAction("Index");
            }
        }
    }
}
