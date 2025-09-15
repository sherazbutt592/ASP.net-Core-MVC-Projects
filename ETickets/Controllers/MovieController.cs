using ETickets.Data;
using ETickets.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ETickets.Controllers
{
    public class MovieController : Controller
    {
        private AppDbContext _context;
        private IRepository<Movie> _repository;
        public MovieController(AppDbContext appDbContext, IRepository<Movie> repo)
        {
            _context = appDbContext;
            _repository = repo;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _repository.GetAllAsync(new QueryOptions<Movie> { Includes = "Actors, Cinema, Producer" });
            return View(model);
        }
    }
}
