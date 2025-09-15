using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Repository<Product> _productsRepository;
        private Repository<Order> _ordersRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _productsRepository = new Repository<Product>(context);
            _ordersRepository = new Repository<Order>(context);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
            {
                OrderItems = new List<OrderItemViewModel>(),
                Products = await _productsRepository.GetAllsync()
            };
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddItem(int productId, int productQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) {
                return NotFound();
            }
            //Retrieve or create an OrderViewModel from session or other state management
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
            {
                OrderItems = new List<OrderItemViewModel>(),
                Products = await _productsRepository.GetAllsync()
            };
            //check if product is already in the order
            var existingItem = model.OrderItems.FirstOrDefault(x => x.ProductId == productId);
            //if the product is already in the order, update the quantity
            if (existingItem != null)
            {
                existingItem.Quantity = productQuantity;
            }
            else
            {
                model.OrderItems.Add(new OrderItemViewModel
                {
                    ProductId = product.ProductId,
                    Price = product.Price,
                    Quantity = productQuantity,
                    ProductName = product.Name
                });
            }
            model.TotalAmount = model.OrderItems.Sum(oi => oi.Price * oi.Quantity);
            //save updated OrderViewModel to session
            HttpContext.Session.Set("OrderViewModel", model);
            //Redirect back to Create to show updated order items
            return RedirectToAction("Create", model);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Cart()
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");
            if (model == null || model.OrderItems.Count == 0)
            { return RedirectToAction("Create"); }
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");
            if(model == null || model.OrderItems.Count == 0)
            {
                return RedirectToAction("Create");
            }
            //Create new order entry
            Order order = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = model.TotalAmount,
                UserId = _userManager.GetUserId(User)
            };
            //Add OrderItems to the order entity
            foreach (var item in model.OrderItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                });
            }
            //Save the order entity to the database
            await _ordersRepository.AddAsync(order);
            //Clear the Order View model from session or other state management
            HttpContext.Session.Remove("OrderViewModel");
            //Redirect to the order confirmation page
            return RedirectToAction("ViewOrders");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ViewOrders()
        {
            var userId = _userManager.GetUserId(User);
            var userOrders = await _ordersRepository.GetAllByIDAsync(userId, "UserId", new QueryOptions<Order> { Includes = "OrderItems.Product" });
            return View(userOrders);
        }
    }
}
