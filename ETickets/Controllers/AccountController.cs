using ETickets.Data;
using ETickets.Data.ViewModels;
using ETickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ETickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        public AccountController(AppDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login() => View(new LoginViewModel());
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
                TempData["Error"] = "Wrong Credentials. Please try again!";
                return View(model);
            }
            TempData["Error"] = "Wrong Credentials. Please try again!";
            return View(model);
        }
        public IActionResult Register() => View(new RegisterViewModel());
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(model);
            }
            var newUser = new ApplicationUser()
            {
                FullName = model.FullName,
                Email = model.EmailAddress,
                UserName = model.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, model.Password);
            //if (newUserResponse.Succeeded)
                //await _userManager.AddToRoleAsync(newUser, UserRoles.user);
            return View("RegisterCompleted");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movie");
        }
    }
}
