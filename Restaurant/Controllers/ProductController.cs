using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class ProductController : Controller
    {
        private Repository<Product> productsRepository;
        private Repository<Ingredient> ingredientsRepository;
        private Repository<Category> categoriesRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            productsRepository = new Repository<Product>(context);
            ingredientsRepository = new Repository<Ingredient>(context);
            categoriesRepository = new Repository<Category>(context);
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await productsRepository.GetAllsync());
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            ViewBag.Ingredients = await ingredientsRepository.GetAllsync();
            ViewBag.Categories = await categoriesRepository.GetAllsync();
            if (id == 0)
            {
                ViewBag.Operation = "Add";
                return View(new Product());
            }
            else
            {
                Product product = await productsRepository.GetByIDAsync(id, new QueryOptions<Product> { 
                    Includes = "ProductIngredients.Ingredient, Category"});
                ViewBag.Operation = "Edit";
                return View(product);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(Product product, int[] ingredientIds, int catId)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(fileStream);
                    }
                    product.ImageUrl = uniqueFileName;
                }
            }
            if (product.ProductIngredients == null)
            {
                product.ProductIngredients = new List<ProductIngredient>();
            }
            if (product.ProductId == 0)
            {
                ViewBag.Ingredients = await ingredientsRepository.GetAllsync();
                ViewBag.Categories = await categoriesRepository.GetAllsync();
                product.CategoryId = catId;
                foreach (int id in ingredientIds)
                {
                    product.ProductIngredients.Add(new ProductIngredient { IngredientId = id, ProductId = product.ProductId });
                }
                await productsRepository.AddAsync(product);
                return RedirectToAction("Index");
            }
            else
            {
                var existingProduct = await productsRepository.GetByIDAsync(product.ProductId, new QueryOptions<Product> { Includes = "ProductIngredients" });
                if (existingProduct == null)
                {
                    ModelState.AddModelError("", "Product not found.");
                    ViewBag.Ingredients = await ingredientsRepository.GetAllsync();
                    ViewBag.Categories = await categoriesRepository.GetAllsync();
                    return View(product);
                }
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
                existingProduct.CategoryId = catId;

                existingProduct.ProductIngredients.Clear();
                foreach(int id in ingredientIds)
                {
                    existingProduct.ProductIngredients.Add(new ProductIngredient { IngredientId =id, ProductId = product.ProductId });
                }
                try
                {
                    await productsRepository.UpdateAsync(existingProduct);
                }
                catch (Exception ex) { 
                    ModelState.AddModelError("", $"Error: {ex.GetBaseException().Message}");
                    ViewBag.Ingredients = await ingredientsRepository.GetAllsync();
                    ViewBag.Categories = await categoriesRepository.GetAllsync();
                    return View(product);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await productsRepository.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Product not found.");
                return RedirectToAction("Index");
            }
        }
    }
}
