using DBHosting.DataAccess;
using DBHosting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DBHosting.Controllers
{
    public class UsersController : Controller
    {
        private readonly DBDeployContext _context;

        public UsersController(DBDeployContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/users/new")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

    }
}
