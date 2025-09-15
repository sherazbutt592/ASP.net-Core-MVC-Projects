using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Restaurant.Data;
using Restaurant.Models;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    public class IngredientController:Controller
    {
        private Repository<Ingredient> repository;
        public IngredientController(ApplicationDbContext context)
        {
            repository = new Repository<Ingredient>(context);
        }
        public async Task<IActionResult> Index()
        {
            return View(await repository.GetAllsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            return View( await repository.GetByIDAsync(id, new QueryOptions<Ingredient>() {Includes = "ProductIngredients.Product" }));
        }
        [HttpGet]
        public IActionResult Create() 
        { 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IngredientId, Name")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                await repository.AddAsync(ingredient);
                return RedirectToAction("Index");
            }
            return View(ingredient);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await repository.GetByIDAsync(id, new QueryOptions<Ingredient> { Includes = "ProductIngredients.Product"}));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Ingredient ingredient)
        {
            await repository.DeleteAsync(ingredient.IngredientId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await repository.GetByIDAsync(id, new QueryOptions<Ingredient> { Includes="ProductIngredients.Product" }));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Ingredient ingredient)
        {
            if(ModelState.IsValid)
            {
                await repository.UpdateAsync(ingredient);
                return RedirectToAction("Index");
            }
            return View(ingredient);
        }

    }
}
