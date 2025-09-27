using ETickets.Data;
using ETickets.Data.Cart;
using ETickets.Data.Services;
using ETickets.Data.ViewModels;
using ETickets.Models;
using Microsoft.AspNetCore.Mvc;

namespace ETickets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IRepository<Movie> _movieRepository;
        private readonly IOrderService _orderService;
        private readonly ShoppingCart _shoppingCart;

        public OrdersController(IRepository<Movie> repository, ShoppingCart shoppingCart, IOrderService orderService )
        {
            _movieRepository = repository;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = ""; // get user id
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return View(orders);
        }
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var modal = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(modal);
        }
        public async Task<RedirectToActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _movieRepository.GetByIdAsync(id, new QueryOptions<Movie>());
            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<RedirectToActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _movieRepository.GetByIdAsync(id, new QueryOptions<Movie>());
            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> CompleteOrder()
        {
            string userId = ""; 
            string userEmailAddress = "";
            var items = _shoppingCart.GetShoppingCartItems();
            await _orderService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCart();
            return View();

        }
    }
}
