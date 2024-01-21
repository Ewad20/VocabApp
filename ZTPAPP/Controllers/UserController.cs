using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekt.Models;
using System.Security.Claims;
using ZTPAPP.Models;

public class UserController : Controller
{
    private readonly WDbContext _context;
    private readonly IConfiguration _configuration;

    public UserController(WDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string name, string email, string password,bool notification)
    {
        var userBuilder = new User.Builder()
            .SetName(name)
            .SetEmail(email)
            .SetPassword(password);
        User user = userBuilder.Build();

        if (ModelState.IsValid)
        {
            _context.Add(user);
            _context.SaveChanges();
            if (notification)
            {
                SubscribeOberver subscribeOberver = new SubscribeOberver(_context,_configuration);
                Subscriber subscriber = new Subscriber();
                subscriber.User = user;
                subscribeOberver.Update(subscriber);
            }
            return RedirectToAction("Login");
        }
        return View();
    }


    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password, string returnUrl = null)
    {
        // Tu powinno nastąpić prawdziwe sprawdzenie w bazie danych
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

        if (user != null)
        {
            // Utwórz listę claimów (identyfikatorów)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Redirect(returnUrl ?? Url.Action("Index", "Home"));
        }

        ModelState.AddModelError(string.Empty, "Nieprawidłowy email lub hasło");
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

}
