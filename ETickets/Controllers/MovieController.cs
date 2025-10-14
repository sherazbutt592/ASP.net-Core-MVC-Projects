using ETickets.Data;
using ETickets.Data.Enums;
using ETickets.Data.Statics;
using ETickets.Data.ViewModels;
using ETickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ETickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MovieController : Controller
    {
        private AppDbContext _context;
        private IRepository<Movie> _movieRepository;
        private IRepository<Cinema> _cinemaRepository;
        private IRepository<Actor> _actorRepository;
        private IRepository<Producer> _producerRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MovieController(AppDbContext appDbContext, IRepository<Movie> movie, IRepository<Cinema> cinema, IRepository<Actor> actor, IRepository<Producer> producer, IWebHostEnvironment webHostEnvironment)
        {
            _context = appDbContext;
            _movieRepository = movie;
            _cinemaRepository = cinema;
            _actorRepository = actor;
            _producerRepository = producer;
            _webHostEnvironment = webHostEnvironment;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = await _movieRepository.GetAllAsync(new QueryOptions<Movie> { Includes = "Actors, Cinema, Producer" });
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Search(string searchString)
        {
            ViewBag.SearchString = searchString;
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return RedirectToAction(nameof(Index));
            }
            var model = await _movieRepository.GetAllAsync(new QueryOptions<Movie> { Where = m => m.Name.Contains(searchString) || m.Description.Contains(searchString), Includes = "Actors, Cinema, Producer" });
            return View("Index", model);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Cinemas = await _cinemaRepository.GetAllAsync(new QueryOptions<Cinema>());
            ViewBag.Actors = await _actorRepository.GetAllAsync(new QueryOptions<Actor>());
            ViewBag.Producers = await _producerRepository.GetAllAsync(new QueryOptions<Producer>());
            var movie = await _movieRepository.GetByIdAsync(id, new QueryOptions<Movie> { Includes = "Actors, Cinema, Producer" });
            if (movie == null)
            {
                return View("NotFound");
            }
            var model = new MovieViewModel
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                Price = movie.Price,
                Image = movie.Image,
                CinemaId = movie.CinemaId,
                ProducerId = movie.ProducerId,
                ActorIds = movie.Actors.Select(a => a.Id).ToList(),
                MovieCategory = movie.MovieCategory
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            ViewBag.Cinemas = await _cinemaRepository.GetAllAsync(new QueryOptions<Cinema>());
            ViewBag.Actors = await _actorRepository.GetAllAsync(new QueryOptions<Actor>());
            ViewBag.Producers = await _producerRepository.GetAllAsync(new QueryOptions<Producer>());
            if (id == 0)
            {
                ViewBag.Operation = "Add";
                return View(new MovieViewModel());
            }
            else
            {
                ViewBag.Operation = "Update";
                var movie = await _movieRepository.GetByIdAsync(id, new QueryOptions<Movie> { Includes = "Actors, Cinema, Producer" });
                var model = new MovieViewModel
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    Description = movie.Description,
                    StartDate = movie.StartDate,
                    EndDate = movie.EndDate,
                    Price = movie.Price,
                    Image = movie.Image,
                    CinemaId = movie.CinemaId,
                    ProducerId = movie.ProducerId,
                    ActorIds = movie.Actors.Select(a => a.Id).ToList(),
                    MovieCategory = movie.MovieCategory
                };
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(MovieViewModel model)
        {
            if (model.File != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(fileStream);
                }
                model.Image = uniqueFileName;
            }
            if (model.Id == 0)
            {
                var movie = new Movie
                {
                    Name = model.Name,
                    Description = model.Description,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Price = model.Price,
                    Image = model.Image,
                    CinemaId = model.CinemaId,
                    ProducerId = model.ProducerId,
                    MovieCategory = model.MovieCategory
                };
                foreach (var id in model.ActorIds)
                {
                    var actor = await _actorRepository.GetByIdAsync(id, new QueryOptions<Actor>());
                    if (actor != null)
                    {
                        movie.Actors.Add(actor);
                    }
                }
                await _movieRepository.AddAsync(movie);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var existingMovie = await _movieRepository.GetByIdAsync(model.Id, new QueryOptions<Movie> { Includes = "Actors" });
                if (existingMovie == null)
                {
                    return View("NotFound");
                }
                existingMovie.Name = model.Name;
                existingMovie.Description = model.Description;
                existingMovie.StartDate = model.StartDate;
                existingMovie.EndDate = model.EndDate;
                existingMovie.Price = model.Price;
                if (model.File != null)
                {
                    existingMovie.Image = model.Image;
                }
                existingMovie.CinemaId = model.CinemaId;
                existingMovie.ProducerId = model.ProducerId;
                existingMovie.MovieCategory = model.MovieCategory;
                existingMovie.Actors.Clear();
                foreach (var id in model.ActorIds)
                {
                    var actor = await _actorRepository.GetByIdAsync(id, new QueryOptions<Actor>());
                    existingMovie.Actors.Add(actor);
                }
                await _movieRepository.UpdateAsync(existingMovie);
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id, new QueryOptions<Movie>());
            if (movie == null)
            {
                return View("NotFound");
            }
            await _movieRepository.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }


}


