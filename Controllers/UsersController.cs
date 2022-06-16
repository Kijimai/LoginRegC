using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LoginAndRegistration.Models;

public class UsersController : Controller
{
  private RegistrationContext _context;

  public UsersController(RegistrationContext context)
  {
    _context = context;
  }

  [HttpGet("/registration")]
  public IActionResult LoginReg()
  {
    return View("LoginReg");
  }

  [HttpPost("/register")]
  public IActionResult Register(User newUser)
  {
    if (ModelState.IsValid)
    {
      if (_context.Users.Any(user => user.Email == newUser.Email))
      {
        ModelState.AddModelError("Email", "is already taken.");
      }
    }
    if (!ModelState.IsValid)
    {
      return LoginReg();
    }

    PasswordHasher<User> HashedPW = new PasswordHasher<User>();
    newUser.Password = HashedPW.HashPassword(newUser, newUser.Password);

    _context.Users.Add(newUser);
    _context.SaveChanges();

    HttpContext.Session.SetInt32("UUID", newUser.UserId);

    return Success();
  }

  [HttpGet("/success")]
  public IActionResult Success()
  {
    int? uid = HttpContext.Session.GetInt32("UUID");
    if (uid == null)
    {
      ViewBag.AuthorizationError = "You do not have permission to view this page!";
      return LoginReg();
    }

    return View("Success");
  }

  [HttpPost("/login")]
  public IActionResult Login(LoginUser loginUser)
  {
    if (!ModelState.IsValid)
    {
      return LoginReg();
    }

    User? foundUser = _context.Users.FirstOrDefault(user => user.Email == loginUser.LoginEmail);

    if (foundUser == null)
    {
      ModelState.AddModelError("LoginEmail", "and Password don't match.");
      return LoginReg();
    }

    PasswordHasher<LoginUser> DeHasher = new PasswordHasher<LoginUser>();
    PasswordVerificationResult pwCompare = DeHasher.VerifyHashedPassword(loginUser, foundUser.Password, loginUser.LoginPassword);

    if (pwCompare == 0)
    {
      ModelState.AddModelError("LoginPassword", "doesn't match this email.");
      return LoginReg();
    }

    HttpContext.Session.SetInt32("UUID", foundUser.UserId);
    return Success();
  }

  [HttpPost("/logout")]
  public IActionResult Logout()
  {
    HttpContext.Session.Clear();
    return RedirectToAction("LoginReg");
  }
}
