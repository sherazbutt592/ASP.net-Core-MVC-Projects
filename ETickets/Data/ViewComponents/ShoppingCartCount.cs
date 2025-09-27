using ETickets.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace ETickets.Data.ViewComponents
{
    public class ShoppingCartCount:ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartCount(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }
        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            return View(items.Count);
        }
    }
}
