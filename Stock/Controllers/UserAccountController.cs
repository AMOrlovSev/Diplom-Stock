using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Stock.Data;
using Stock.Models.VM;
using Stock.Models;

namespace Stock.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly StockDbContext _context;

        public UserAccountController(StockDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Users.ToList());
        }

        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(RegistrationUser registrationUser)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.FirstName = registrationUser.FirstName;
                user.LastName = registrationUser.LastName;
                user.Login = registrationUser.Login;
                user.Email = registrationUser.Email;
                user.Password = registrationUser.Password;

                try
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{user.Login} зарестрированы успешно. Пожалуйста, войдите.";
                }
                catch (DbUpdateException ex)
                {

                    ModelState.AddModelError("", "Пожалуйста, введите уникальный Email.");
                    return View(registrationUser);
                }

                return View();
            }

            return View(registrationUser);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.Where(x => (x.Login == loginUser.LoginOrEmail || x.Email == loginUser.LoginOrEmail) && x.Password == loginUser.Password).FirstOrDefault();
                if (user != null)
                {
                    //Success, create cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim("Name", user.FirstName),
                        new Claim(ClaimTypes.Role, "User"),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("SecurePage");
                }
                else
                {
                    ModelState.AddModelError("", "Login/Email or Password is not correct");
                }
            }

            return View(loginUser);
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }


        [Authorize]
        public IActionResult SecurePage()
        {
            var userLogin = HttpContext.User.Identity.Name;
            var user = _context.Users.FirstOrDefault(p => p.Email.Equals(userLogin));
            ViewBag.Name = user.Login;
            return View();
        }


    }
}
