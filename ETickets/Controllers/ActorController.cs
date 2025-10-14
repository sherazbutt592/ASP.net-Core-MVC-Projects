using ETickets.Data;
using ETickets.Data.Statics;
using ETickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ETickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ActorController : Controller
    {
        private AppDbContext _dbContext;
        private Repository<Actor> _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ActorController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = context;
            _repository = new Repository<Actor>(context);
            _webHostEnvironment = webHostEnvironment;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync(new QueryOptions<Actor> { Includes = "Movies" }));
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
                ModelState.AddModelError("", "Actor not found");
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var actor = await _repository.GetByIdAsync(id, new QueryOptions<Actor> { Includes = "Movies" });
            if (actor == null) return View("NotFound");
            return View(actor);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            if (id == 0)
            {
                ViewBag.Operation = "Add";
                return View(new Actor());
            }
            else
            {
                ViewBag.Operation = "Edit";
                var actor = await _repository.GetByIdAsync(id, new QueryOptions<Actor>());
                if (actor == null) return View("NotFound");
                ViewBag.Operation = "Edit";
                return View(actor);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(Actor actor, int id)
        {
            if (id == 0)
            {
                if (actor.File != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + actor.File.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await actor.File.CopyToAsync(fileStream);
                    }
                    actor.ProfilePicture = uniqueFileName;
                }
                await _repository.AddAsync(actor);
                return RedirectToAction("Index");
            }
            else
            {
                var existingActor = await _repository.GetByIdAsync(id, new QueryOptions<Actor>());
                if (existingActor == null) return View("NotFound");
                existingActor.FullName = actor.FullName;
                existingActor.Bio = actor.Bio;
                if (actor.File != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + actor.File.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await actor.File.CopyToAsync(fileStream);
                    }
                    existingActor.ProfilePicture = uniqueFileName;
                }
                await _repository.UpdateAsync(existingActor);
                return RedirectToAction("Index");
            }
        }
    }

}
